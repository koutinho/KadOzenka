using Core.SRD;
using ObjectModel.Directory;
using ObjectModel.Insur;
using System;
using System.Collections.Generic;

namespace CIPJS.Models.InputPlat
{
    public class InputPlatDto
    {
        public long Id { get; set; }
        public DateTime? PeriodRegDate { get; set; }
        public DateTime? Period { get; set; }
        public DateTime? PmtDate { get; set; }
        public string Kodpl { get; set; }
        public decimal? SumOpl { get; set; }
        public string Adres { get; set; }
        public string Nom { get; set; }
        public long? Unom { get; set; }
        public string TxId { get; set; }
        public decimal? Opl { get; set; }

        public static InputPlatDto OMMap(OMInputPlat entity)
        {
            return new InputPlatDto
            {
                Id = entity.EmpId,
                Kodpl = entity.Kodpl,
                PeriodRegDate = entity.PeriodRegDate,
                PmtDate = entity.PmtDate,
                Nom = entity.Nom,
                SumOpl = entity.SumOpl,
                Adres = entity.Adres,
                Opl = entity.Opl,
                Period = entity.Period,
                Unom = entity.Unom,
                TxId = entity.TxId
            };
        }

        public static OMInputPlat OMMap(InputPlatDto platDto, List<OMChangesLog> changesLog = null)
        {
            OMInputPlat inputPlat;

            if (platDto.Id > 0)
            {
                inputPlat = OMInputPlat.Where(x => x.EmpId == platDto.Id)
                    .SelectAll()
                    .ExecuteFirstOrDefault();

                if (inputPlat == null)
                {
                    throw new Exception($"Не удалось определить зачисление с идентификатором");
                }
            }
            else
            {
                inputPlat = new OMInputPlat();
                inputPlat.TypeSource_Code = InsuranceSourceType.Mfc;
                inputPlat.StatusIdentif_Code = StatusIdentifikacii.NotIdentified;
            }

            inputPlat.PeriodRegDate = platDto.PeriodRegDate;
            inputPlat.PmtDate = platDto.PmtDate;
            inputPlat.Kodpl = platDto.Kodpl;

            if (inputPlat.SumOpl != platDto.SumOpl)
            {
                if (platDto.Id > 0 && changesLog != null)
                {
                    changesLog.Add(new OMChangesLog
                    {
                        ObjectId = inputPlat.EmpId,
                        LoadDate = DateTime.Now,
                        ReestrId = OMInputPlat.GetRegisterId(),
                        OperationType_Code = ChangeOperationType.SumOpl,
                        OldValue = inputPlat.SumOpl.HasValue ? inputPlat.SumOpl.Value.ToString() : null,
                        NewValue = platDto.SumOpl.HasValue ? platDto.SumOpl.Value.ToString() : null,
                        UserId = SRDSession.GetCurrentUserId()
                    });
                }

                inputPlat.SumOpl = platDto.SumOpl;
            }

            inputPlat.Adres = platDto.Adres;
            inputPlat.Nom = platDto.Nom;
            inputPlat.Opl = platDto.Opl;
            inputPlat.Period = platDto.Period;
            inputPlat.Unom = platDto.Unom;
            inputPlat.TxId = platDto.TxId;

            return inputPlat;
        }
    }
}
