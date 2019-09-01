using CIPJS.DAL.FileStorage;
using Core.Shared.Extensions;
using Microsoft.AspNetCore.Http;
using ObjectModel.Directory;
using ObjectModel.Insur;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CIPJS.DAL.Documents
{
    public class DocumentsService
    {
        /// <summary>
        /// Получение списка документов
        /// </summary>
        /// <param name="objId"></param>
        /// <param name="reestrId"></param>
        /// <returns></returns>
        public List<OMDocuments> GetDocumentData(long objId, long reestrId, ContractType? type = null)
        {

            List<OMDocuments> documents = OMDocuments.Where(x => x.ObjId == objId && x.ReestrId == reestrId)
                .SelectAll()
                .Select(x => x.ParentDocBaseType.DocumentBase)
                .Select(x => x.ParentDocBaseType.Order)
                .Select(x => x.ParentDocBaseType.Id)
                .Select(x => x.ParentDocBaseType.NeedSetDate)
                .Select(x => x.ParentInsuranceOrganization.ShortName)
                .Select(x => x.ParentUser.FullNameForDoc)
                .Execute()
                .OrderBy(x => x.ParentDocBaseType.Order)
                .ToList();

            if (type == ContractType.CommonOwnership)
            {
                if (documents.Count > 10)
                {
                    var sortDocuments = new List<OMDocuments>()
                   {
                       documents[9],
                       documents[3],
                       documents[4],
                       documents[10],
                       documents[0],
                       documents[1],
                       documents[5],
                       documents[6]
                   };

                    sortDocuments.AddRange(documents.Where(x => !sortDocuments.Contains(x)));
                    documents = sortDocuments;
                }
            }
            documents.Add(new OMDocuments { EmpId = -1 });

            return documents;
        }

        /// <summary>
        /// Создание документа
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public OMDocuments CreatetDocument(OMDocuments entity)
        {
            if (entity == null)
            {
                throw new Exception("Объект не найден");
            }

            entity.Save();

            return entity;
        }

        /// <summary>
        /// Сохранение изменений по документам
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public OMDocuments UpdateDocument(OMDocuments entity)
        {
            if (entity == null)
            {
                throw new Exception("Объект не найден");
            }
            entity.Save();
            return entity;
        }

        /// <summary>
        /// Удаление документа
        /// </summary>
        /// <param name="entity"></param>
        public void DestroyDocument(OMDocuments entity)
        {
            if (entity != null)
            {
                entity.Destroy();
                return;
            }
            throw new Exception("Объект не найден");
        }

        /// <summary>
        /// Загрузка файла к документу
        /// </summary>
        /// <param name="documentId"></param>
        /// <param name="files"></param>
        public long UploadFiles(long? documentId, List<IFormFile> files)
        {
            if (!documentId.HasValue)
            {
                throw new Exception("Не указан документ для привязки");
            }
            long fileStorageId = 0;

            FileStorageService _fileStorageService = new FileStorageService();
            foreach (IFormFile file in files)
            {
                using (Stream stream = file.OpenReadStream())
                    fileStorageId = _fileStorageService.Save(stream, file.FileName);
                break;
            }

            if (documentId > 0)
            {
                OMDocuments oMDocument = OMDocuments.Where(x => x.EmpId == documentId).SelectAll(false).Execute().FirstOrDefault();
                oMDocument.FileStorageId = fileStorageId;
                oMDocument.DocIsHave = true;
                oMDocument.Save();
            }
            return fileStorageId;
        }

        /// <summary>
        /// Получение файла документа
        /// </summary>
        /// <param name="documentId"></param>
        /// <returns></returns>
        public DocumentFileDto GetFile(long? documentId)
        {
            if (!documentId.HasValue)
            {
                throw new Exception("Не указан документ");
            }
            OMDocuments oMDocument = OMDocuments.Where(x => x.EmpId == documentId).Select(x => x.FileStorageId).Execute().FirstOrDefault();
            if (!oMDocument.FileStorageId.HasValue)
            {
                throw new Exception("У документа нет файла");
            }
            FileStorageService _fileStorageService = new FileStorageService();
            OMFileStorage file = _fileStorageService.Get(oMDocument.FileStorageId.Value);
            if (file == null)
            {
                throw new Exception("Файл не найден");
            }
            try
            {
                DocumentFileDto model = new DocumentFileDto
                {
                    Name = file.Filename,
                    StreamFile = _fileStorageService.GetFileStream(file)
                };
                return model;
            }
            catch (Exception ex)
            {
                if (ex.Message.StartsWith("Файл отстуствует в каталоге загрузки"))
                {
                    oMDocument.FileStorageId = null;
                    oMDocument.Save();
                }
                throw;
            }
        }

        public DocumentFileDto GetFileFromStorage(long fileStorageId)
        {
            FileStorageService _fileStorageService = new FileStorageService();
            OMFileStorage file = _fileStorageService.Get(fileStorageId);
            if (file == null)
                throw new Exception("Файл не найден");
            return new DocumentFileDto
            {
                Name = file.Filename,
                StreamFile = _fileStorageService.GetFileStream(file)
            };
        }

        public OMDocBaseType GetOrCreateBaseDocument(long? id, string contractType = null)
        {
            if (id.HasValue && id.Value > -1)
            {
                OMDocBaseType baseDocument = OMDocBaseType.Where(x => x.Id == id.Value).SelectAll().ExecuteFirstOrDefault();

                if (baseDocument == null)
                {
                    throw new ArgumentException("Не удалось определить документ c переданным идентификатором", "id");
                }

                return baseDocument;
            }
            else
            {
                return new OMDocBaseType
                {
                    Type = contractType.IsNotEmpty() ? contractType : "ЖП",
                    Order = 100,
                    NeedSetDate = false
                };
            }
        }

        public void UpdateBaseDocument(OMDocBaseType docBaseType)
        {
            docBaseType.Save();
        }
    }
}
