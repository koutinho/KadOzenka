using CIPJS.DAL.User;
using Core.SRD;
using ObjectModel.Directory;
using ObjectModel.Insur;
using System;

namespace CIPJS.Models.Documents
{
    public class DocumentDto
    {
        public long EmpId { get; set; }

        /// <summary>
        /// Вид документа-основания (выбор из справочника, справочник «Виды документов-оснований»)
        /// </summary>
        public long? DocTypeId { get; set; }

        /// <summary>
        /// Вид документа-основания (выбор из справочника, справочник «Виды документов-оснований»)
        /// </summary>
        public string DocTypeName { get; set; }

        /// <summary>
        /// Признак о предоставлении (Да/Нет)
        /// </summary>
        public bool? DocIsHave { get; set; }

        /// <summary>
        /// Тип (бумажная/электронная)
        /// </summary>
        public string DocTypeM { get; set; }

        /// <summary>
        /// Тип (бумажная/электронная)
        /// </summary>
        public TypeDocBaseCase DocTypeM_Code { get; set; }

        /// <summary>
        /// Номер
        /// </summary>
        public string DocNumber { get; set; }

        /// <summary>
        /// Дата
        /// </summary>
        public DateTime? DocDate { get; set; }

        /// <summary>
        /// Организация (выбор из справочника, справочник  «Страховые организации)
        /// </summary>
        public long? DocOrgId { get; set; }

        /// <summary>
        /// Организация (выбор из справочника, справочник  «Страховые организации)
        /// </summary>
        public string DocOrgName { get; set; }

        /// <summary>
        /// ФИО сотрудника загрузившего документ
        /// </summary>
        public string FIOScan { get; set; }

        /// <summary>
        /// Ссылка на уникальный номер записи
        /// </summary>
        public long? ObjId { get; set; }

        /// <summary>
        /// Номер реестра записи
        /// </summary>
        public long? ReestrId { get; set; }

        /// <summary>
        /// Ссылка на хранилище документов
        /// </summary>
        public long? FileStorageId { get; set; }

        /// <summary>
        /// Пользователь, создавший запись
        /// </summary>
        public long? UserId { get; set; }

        /// <summary>
        /// Пользователь, создавший запись
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Дата ввода
        /// </summary>
        public DateTime? DateCreate { get; set; }

        /// <summary>
        /// Признак, что дата обязательная для заполнения
        /// </summary>
        public bool? NeedSetDate { get; set; }

        public static DocumentDto OMMap(OMDocuments entity)
        {
            DocumentDto doc = new DocumentDto
            {
                EmpId = entity?.EmpId ?? -1,
                ObjId = entity.ObjId,
                ReestrId = entity.ReestrId,
                FileStorageId = entity.FileStorageId,
                DocTypeId = entity.DocTypeId,
                DocTypeName = entity.ParentDocBaseType?.DocumentBase ?? "",
                NeedSetDate = entity.ParentDocBaseType?.NeedSetDate,
                DocIsHave = entity.DocIsHave,
                DocTypeM = entity.DocTypeM,
                DocTypeM_Code = entity.DocTypeM_Code,
                DocNumber = entity.DocNumber,
                DocDate = entity.DocDate,
                DocOrgId = entity.DocOrgId,
                DocOrgName = entity.ParentInsuranceOrganization?.ShortName ?? "",
                FIOScan = entity.FIOScan,
                UserName = entity.ParentUser?.FullNameForDoc ?? entity.ParentUser?.FullName ?? entity.ParentUser?.Name ?? new UserService().GetUserNameById(entity.UserId),
                DateCreate = entity.DateCreate
            };

            return doc;
        }

        public static OMDocuments OMMap(DocumentDto model)
        {
            OMDocuments doc = new OMDocuments
            {
                EmpId = model.EmpId,
                ObjId  = model.ObjId,
                ReestrId = model.ReestrId,
                DocTypeId = model.DocTypeId,
                DocIsHave = model.DocIsHave,
                DocTypeM_Code = model.DocTypeM_Code,
                DocNumber = model.DocNumber,
                DocDate = model.DocDate,
                DocOrgId = model.DocOrgId,
                FIOScan = model.FIOScan,
                FileStorageId = model.FileStorageId,
                UserId = model.DocIsHave.HasValue && model.DocIsHave.Value && model.UserId.HasValue ? model.UserId : SRDSession.GetCurrentUserId(),
                DateCreate = model.DocIsHave.HasValue && model.DocIsHave.Value && !model.UserId.HasValue ? DateTime.Now : (DateTime?)null
            };

            return doc;
        }
    }
}
