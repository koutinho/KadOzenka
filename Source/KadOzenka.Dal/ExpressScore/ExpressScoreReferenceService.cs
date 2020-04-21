using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Core.Shared.Extensions;
using GemBox.Spreadsheet;
using KadOzenka.Dal.ExpressScore.Dto;
using ObjectModel.Directory.ES;
using ObjectModel.ES;

namespace KadOzenka.Dal.ExpressScore
{
    public class ExpressScoreReferenceService
    {
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
                ImportReferenceItemsFromExcel(fileStream, reference.Id, fileImportInfo);

                ts.Complete();
            }
        }

        public long CreateReferenceFromExcel(Stream fileStream, ImportReferenceFileInfoDto fileImportInfo, string referenceName)
        {
            long referenceId;
            using (var ts = new TransactionScope())
            {
                referenceId = CreateReference(referenceName, fileImportInfo.ValueType);
                ImportReferenceItemsFromExcel(fileStream, referenceId, fileImportInfo);

                ts.Complete();
            }

            return referenceId;
        }

        private void ImportReferenceItemsFromExcel(Stream fileStream, long referenceId,
            ImportReferenceFileInfoDto fileImportInfo)
        {
            var excelFile = ExcelFile.Load(fileStream, LoadOptions.XlsxDefault);
            var mainWorkSheet = excelFile.Worksheets[0];
            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            ParallelOptions options = new ParallelOptions
            {
                CancellationToken = cancelTokenSource.Token,
                MaxDegreeOfParallelism = 10
            };

            var maxColumns = mainWorkSheet.CalculateMaxUsedColumns();
            var columnNames = new List<string>();
            for (var i = 0; i < maxColumns; i++)
            {
                columnNames.Add(mainWorkSheet.Rows[0].Cells[i].Value.ToString());
            }

            var dataRows = mainWorkSheet.Rows.Where(x => x.Index > 0);
            
            Parallel.ForEach(dataRows, options, row =>
            {
                var cellValue = mainWorkSheet.Rows[row.Index].Cells[columnNames.IndexOf(fileImportInfo.ValueColumnName)];
                var cellCalcValue = mainWorkSheet.Rows[row.Index].Cells[columnNames.IndexOf(fileImportInfo.CalcValueColumnName)];
                
                if (!cellCalcValue.Value.TryParseToDecimal(out var calcValue))
                {
                    throw new Exception(
                        $"Значение '{cellValue.Value.ToString()}' (столбец '{fileImportInfo.CalcValueColumnName}' строка {row.Index}) не может быть приведено к типу '{ReferenceItemCodeType.Number.GetEnumDescription()}'");
                }

                string valueString = null;
                switch (fileImportInfo.ValueType)
                {
                    case ReferenceItemCodeType.Number:
                        if (!cellValue.Value.TryParseToDecimal(out var number))
                        {
                            throw new Exception(
                                $"Значение '{cellValue.Value.ToString()}' (столбец '{fileImportInfo.ValueColumnName}' строка {row.Index}) не может быть приведено к типу '{ReferenceItemCodeType.Number.GetEnumDescription()}'");
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
                                $"Значение '{cellValue.Value.ToString()}' (столбец '{fileImportInfo.ValueColumnName}' строка {row.Index}) не может быть приведено к типу '{ReferenceItemCodeType.Date.GetEnumDescription()}'");
                        }
                        valueString = date.ToString(CultureInfo.CurrentCulture);
                        break;
                }

                var obj = OMEsReferenceItem.Where(x => x.ReferenceId == referenceId && x.Value == valueString)
                    .SelectAll().ExecuteFirstOrDefault();
                if (obj != null)
                {
                    obj.CalculationValue = calcValue;
                    obj.Save();
                }
                else
                {
                    new OMEsReferenceItem
                    {
                        ReferenceId = referenceId,
                        Value = valueString,
                        CalculationValue = calcValue
                    }.Save();
                }
            });
        }
    }
}
