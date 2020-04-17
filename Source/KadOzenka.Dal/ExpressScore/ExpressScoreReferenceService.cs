using System;
using System.Transactions;
using Core.Shared.Extensions;
using KadOzenka.Dal.ExpressScore.Dto;
using ObjectModel.Directory.ES;
using ObjectModel.ES;

namespace KadOzenka.Dal.ExpressScore
{
    public class ExpressScoreReferenceService
    {
        public long CreateReference(string name)
        {
            var isExistsReferencesWithTheSameName = OMEsReference.Where(x => x.Name == name).ExecuteExists();
            if (isExistsReferencesWithTheSameName)
            {
                throw new Exception($"Справочник '{name}' уже существует");
            }

            var reference = new OMEsReference
            {
                Name = name
            };
            var id = reference.Save();

            return id;
        }

        public void UpdateReference(long id, string name)
        {
            var reference = OMEsReference.Where(x => x.Id == id).SelectAll().ExecuteFirstOrDefault();
            if (reference == null)
            {
                throw new Exception($"Не найден справочник с ИД {id}");
            }

            var isExistsReferenceWithTheSameName = OMEsReference.Where(x => x.Name == name).ExecuteExists();
            if (isExistsReferenceWithTheSameName)
            {
                throw new Exception($"Справочник '{name}' уже существует");
            }

            reference.Name = name;
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

            if (dto.Value != null && (dto.ValueType == ReferenceItemCodeType.Number && !decimal.TryParse(dto.Value, out var decimalResult)
                    || dto.ValueType == ReferenceItemCodeType.Date && !DateTime.TryParse(dto.Value, out var dateResult)))
            {
                throw new Exception($"Значение '{dto.Value}' не может быть приведено к типу '{dto.ValueType.GetEnumDescription()}'");
            }

            var item = new OMEsReferenceItem();
            item.ReferenceId = dto.ReferenceId;
            item.Value = dto.Value;
            item.ValueType_Code = dto.ValueType;
            item.CalculationValue = dto.CalcValue;
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

            if (dto.Value != null && (dto.ValueType == ReferenceItemCodeType.Number && !decimal.TryParse(dto.Value, out var decimalResult)
                || dto.ValueType == ReferenceItemCodeType.Date && !DateTime.TryParse(dto.Value, out var dateResult)))
            {
                throw new Exception($"Значение '{dto.Value}' не может быть приведено к типу '{dto.ValueType.GetEnumDescription()}'");
            }

            item.Value = dto.Value;
            item.ValueType_Code = dto.ValueType;
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
    }
}
