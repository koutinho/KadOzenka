using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Core.Main.FileStorages;
using Core.Messages;
using Core.Shared.Extensions;
using Core.SRD;
using GemBox.Spreadsheet;
using KadOzenka.Dal.DataImport;
using KadOzenka.Dal.ExpressScore.Dto;
using ObjectModel.Directory.ES;
using ObjectModel.ES;

namespace KadOzenka.Dal.ExpressScore
{
    public class ExpressScoreReferenceService
    {
        public const string DateCreatedStringFormat = "yyyyMMddHHmmss";

        public long CreateReference(string name, ReferenceItemCodeType valueType)
        {
            var isExistsReferencesWithTheSameName = OMEsReference.Where(x => x.Name == name).ExecuteExists();
            if (isExistsReferencesWithTheSameName)
            {
                throw new Exception($"Справочник '{name}' уже существует");
            }

            var reference = new OMEsReference
            {
                Name = name, 
                ValueType_Code = valueType
            };
            var id = reference.Save();

            return id;
        }

        public void UpdateReference(long id, string name, ReferenceItemCodeType valueType)
        {
            var reference = OMEsReference.Where(x => x.Id == id).SelectAll().ExecuteFirstOrDefault();
            if (reference == null)
            {
                throw new Exception($"Не найден справочник с ИД {id}");
            }

            var isExistsReferenceWithTheSameName = OMEsReference.Where(x => x.Name == name && x.Id != id).ExecuteExists();
            if (isExistsReferenceWithTheSameName)
            {
                throw new Exception($"Справочник '{name}' уже существует");
            }

            if (reference.ValueType_Code != valueType)
            {
                var isReferenceNotEmpty = OMEsReferenceItem.Where(x => x.ReferenceId == id).ExecuteExists();
                if (isReferenceNotEmpty)
                {
                    throw new Exception($"Нельзя изменить тип для непустого справочника");
                }
            }

            reference.Name = name;
            reference.ValueType_Code = valueType;
            reference.Save();
        }

        public void DeleteReference(long id)
        {
            var reference = OMEsReference.Where(x => x.Id == id).SelectAll().ExecuteFirstOrDefault();
            if (reference == null)
            {
                throw new Exception($"Не найден справочник с ИД {id}");
            }

            using (var ts = new TransactionScope())
            {
                var referenceItems = OMEsReferenceItem.Where(x => x.ReferenceId == id).Execute();
                foreach (var referenceItem in referenceItems)
                {
                    referenceItem.Destroy();
                }
                reference.Destroy();

                ts.Complete();
            }
        }

        public long CreateReferenceItem(ReferenceItemDto dto)
        {
            var reference = OMEsReference.Where(x => x.Id == dto.ReferenceId).SelectAll().ExecuteFirstOrDefault();
            if (reference == null)
            {
                throw new Exception($"Не найден справочник с ИД {dto.ReferenceId}");
            }

            if (dto.Value != null && (reference.ValueType_Code == ReferenceItemCodeType.Number && !decimal.TryParse(dto.Value, out var decimalResult)
                    || reference.ValueType_Code == ReferenceItemCodeType.Date && !DateTime.TryParse(dto.Value, out var dateResult)))
            {
                throw new Exception($"Значение '{dto.Value}' не может быть приведено к типу '{reference.ValueType_Code.GetEnumDescription()}'");
            }

            var isExistsTheSameReferenceItem = OMEsReferenceItem.Where(x => x.ReferenceId == dto.ReferenceId && x.Value == dto.Value).ExecuteExists();
            if (isExistsTheSameReferenceItem)
            {
                throw new Exception($"Значение '{dto.Value}' в справочнике '{reference.Name}' уже существует");
            }

            var item = new OMEsReferenceItem
            {
                ReferenceId = dto.ReferenceId,
                Value = dto.Value,
                CalculationValue = dto.CalcValue
            };
            var id = item.Save();

            return id;
        }

        public void UpdateReferenceItem(ReferenceItemDto dto)
        {
            var item = OMEsReferenceItem.Where(x => x.Id == dto.Id).SelectAll().ExecuteFirstOrDefault();
            if (item == null)
            {
                throw new Exception($"Не найдено значение справочника с ИД {dto.Id}");
            }

            var reference = OMEsReference.Where(x => x.Id == dto.ReferenceId).SelectAll().ExecuteFirstOrDefault();
            if (reference == null)
            {
                throw new Exception($"Не найден справочник с ИД {dto.ReferenceId}");
            }

            var isExistsTheSameReferenceItem = OMEsReferenceItem.Where(x => x.Value == dto.Value && x.Id != dto.Id).ExecuteExists();
            if (isExistsTheSameReferenceItem)
            {
                throw new Exception($"Значение '{dto.Value}' в справочнике '{reference.Name}' уже существует");
            }

            if (dto.Value != null && (reference.ValueType_Code == ReferenceItemCodeType.Number && !decimal.TryParse(dto.Value, out var decimalResult)
                || reference.ValueType_Code == ReferenceItemCodeType.Date && !DateTime.TryParse(dto.Value, out var dateResult)))
            {
                throw new Exception($"Значение '{dto.Value}' не может быть приведено к типу '{reference.ValueType_Code.GetEnumDescription()}'");
            }

            item.Value = dto.Value;
            item.CalculationValue = dto.CalcValue;
            item.Save();
        }

        public void DeleteReferenceItem(long id)
        {
            var item = OMEsReferenceItem.Where(x => x.Id == id).SelectAll().ExecuteFirstOrDefault();
            if (item == null)
            {
                throw new Exception($"Не найдено значение справочника с ИД {id}");
            }

            item.Destroy();
        }

        public void UpdateReferenceFromExcel(Stream fileStream, ImportReferenceFileInfoDto fileImportInfo, long referenceId, bool deleteOldValues)
        {
            var reference = OMEsReference.Where(x => x.Id == referenceId).SelectAll().ExecuteFirstOrDefault();
            if (reference == null)
            {
                throw new Exception($"Не найден справочник с ИД {referenceId}");
            }

            if (reference.ValueType_Code != fileImportInfo.ValueType && !deleteOldValues)
            {
                throw new Exception($"Нельзя изменить тип справочника без удаления старых значений");
            }

            using (var ts = new TransactionScope())
            {
                if (deleteOldValues)
                {
                    var referenceItems = OMEsReferenceItem.Where(x => x.ReferenceId == referenceId).Execute();
                    foreach (var referenceItem in referenceItems)
                    {
                        referenceItem.Destroy();
                    }

                    reference.ValueType_Code = fileImportInfo.ValueType;
                    reference.Save();
                }
                ImportReferenceItemsFromExcel(fileStream, reference, fileImportInfo);

                ts.Complete();
            }
        }

        public long CreateReferenceFromExcel(Stream fileStream, ImportReferenceFileInfoDto fileImportInfo, string referenceName)
        {
            long referenceId;
            using (var ts = new TransactionScope())
            {
                referenceId = CreateReference(referenceName, fileImportInfo.ValueType);
                var reference = OMEsReference.Where(x => x.Id == referenceId).SelectAll().ExecuteFirstOrDefault();
                ImportReferenceItemsFromExcel(fileStream, reference, fileImportInfo);

                ts.Complete();
            }

            return referenceId;
        }

        private void ImportReferenceItemsFromExcel(Stream fileStream, OMEsReference reference,
            ImportReferenceFileInfoDto fileImportInfo)
        {
            var currentTime = DateTime.Now;
            var fileSavedName = $"{fileImportInfo.FileName} ({currentTime.GetString().Replace(":","_")})";
            var fileResultSavedName = $"{fileSavedName}_Result";

            var excelFile = ExcelFile.Load(fileStream, LoadOptions.XlsxDefault);
            var mainWorkSheet = excelFile.Worksheets[0];

            SaveFileToStorage(excelFile, currentTime, fileSavedName);

            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            ParallelOptions options = new ParallelOptions
            {
                CancellationToken = cancelTokenSource.Token,
                MaxDegreeOfParallelism = 10
            };
            object locked = new object();

            var maxColumns = mainWorkSheet.CalculateMaxUsedColumns();
            var columnNames = new List<string>();
            for (var i = 0; i < maxColumns; i++)
            {
                columnNames.Add(mainWorkSheet.Rows[0].Cells[i].Value.ToString());
            }
            
            var errorCount = 0;
            mainWorkSheet.Rows[0].Cells[maxColumns].SetValue($"Результат сохранения");
            var dataRows = mainWorkSheet.Rows.Where(x => x.Index > 0);

            Parallel.ForEach(dataRows, options, row =>
            {
                try
                {
                    var cellValue = mainWorkSheet.Rows[row.Index]
                        .Cells[columnNames.IndexOf(fileImportInfo.ValueColumnName)];
                    var cellCalcValue = mainWorkSheet.Rows[row.Index]
                        .Cells[columnNames.IndexOf(fileImportInfo.CalcValueColumnName)];

                    if (!cellCalcValue.Value.TryParseToDecimal(out var calcValue))
                    {
                        throw new Exception(
                            $"Значение '{cellValue.Value.ToString()}' не может быть приведено к типу '{ReferenceItemCodeType.Number.GetEnumDescription()}'");
                    }

                    string valueString = null;
                    switch (fileImportInfo.ValueType)
                    {
                        case ReferenceItemCodeType.Number:
                            if (!cellValue.Value.TryParseToDecimal(out var number))
                            {
                                throw new Exception(
                                    $"Значение '{cellValue.Value.ToString()}' не может быть приведено к типу '{ReferenceItemCodeType.Number.GetEnumDescription()}'");
                            }

                            valueString = number.ToString();
                            break;
                        case ReferenceItemCodeType.String:
                            valueString = cellValue.Value.ToString();
                            break;
                        case ReferenceItemCodeType.Date:
                            if (!cellValue.Value.TryParseToDateTime(out var date))
                            {
                                throw new Exception(
                                    $"Значение '{cellValue.Value.ToString()}'не может быть приведено к типу '{ReferenceItemCodeType.Date.GetEnumDescription()}'");
                            }

                            valueString = date.ToString(CultureInfo.CurrentCulture);
                            break;
                    }

                    OMEsReferenceItem obj;
                    lock (locked)
                    {
                        obj = OMEsReferenceItem.Where(x => x.ReferenceId == reference.Id && x.Value == valueString).SelectAll().ExecuteFirstOrDefault();
                    }
                    
                    if (obj != null)
                    {
                        obj.CalculationValue = calcValue;
                        lock (locked)
                        {
                            obj.Save();
                        }
                        mainWorkSheet.Rows[row.Index].Cells[maxColumns].SetValue("Значение успешно обновлено");
                    }
                    else
                    {
                        lock (locked)
                        {
                            new OMEsReferenceItem
                            {
                                ReferenceId = reference.Id,
                                Value = valueString,
                                CalculationValue = calcValue
                            }.Save();
                        }
                        mainWorkSheet.Rows[row.Index].Cells[maxColumns].SetValue("Значение успешно создано");
                    }
                }
                catch (Exception ex)
                {
                    mainWorkSheet.Rows[row.Index].Cells[maxColumns].SetValue($"Ошибка: {ex.Message}");
                    lock (locked)
                    {
                        errorCount++;
                    }
                }
            });

            SaveFileToStorage(excelFile, currentTime, fileResultSavedName);
            SendImportResultNotification(reference, fileImportInfo, currentTime, errorCount, fileResultSavedName, fileSavedName);
        }

        private void SaveFileToStorage(ExcelFile excelFile, DateTime currentTime, string fileSavedName)
        {
            MemoryStream stream;
            stream = new MemoryStream();
            excelFile.Save(stream, SaveOptions.XlsxDefault);
            stream.Seek(0, SeekOrigin.Begin);
            FileStorageManager.Save(stream, DataImporterCommon.FileStorageName, currentTime, fileSavedName);
        }

        private void SendImportResultNotification(OMEsReference reference, ImportReferenceFileInfoDto fileImportInfo,
            DateTime currentTime, int errorCount, string fileResultSavedName, string fileSavedName)
        {
            new MessageService().SendMessages(new MessageDto
            {
                Addressers =
                    new MessageAddressersDto {UserIds = new long[] {SRDSession.GetCurrentUserId().GetValueOrDefault()}},
                Subject = $"Результат загрузки данных в справочник: {reference.Name} от ({currentTime.GetString()})",
                Message = $@"Загрузка файла ""{fileImportInfo.FileName}"" была завершена.
Статус загрузки: {(errorCount > 0 ? "С ошибками" : "Успешно")}
<a href=""/ExpressScopeReference/DownloadImportedFile?downloadResult=true&fileName={fileResultSavedName}&dateCreatedString={currentTime.ToString(DateCreatedStringFormat)}"">Скачать результат</a>
<a href=""/ExpressScopeReference/DownloadImportedFile?downloadResult=false&fileName={fileSavedName}&dateCreatedString={currentTime.ToString(DateCreatedStringFormat)}"">Скачать исходный файл</a>",
                IsUrgent = true,
                IsEmail = true
            });
        }
    }
}
