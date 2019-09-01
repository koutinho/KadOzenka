using CIPJS.Models.BankPlat;
using CIPJS.Models.ChangesLog;
using Core.Register.QuerySubsystem;
using ObjectModel.Directory;
using ObjectModel.Insur;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CIPJS.Models.InputPlat
{
    public class LinkPlatDto
    {
        public long Id { get; set; }
        public bool LinkExists { get; set; }
        public bool CreateMode { get; set; }
        public DateTime? PeriodRegDate { get; set; }
        public DateTime? PmtDate { get; set; }
        public string Kodpl { get; set; }
        public decimal? SumOpl { get; set; }
        public string Adres { get; set; }
        public string Nom { get; set; }
        public long? Unom { get; set; }
        public string TxId { get; set; }
        public DateTime? Period { get; set; }
        public decimal? Opl { get; set; }
        public List<BankPlatDto> BankPlats { get; set; }

        public static LinkPlatDto OMMap(OMInputPlat entity)
        {
            LinkPlatDto platDto = new LinkPlatDto
            {
                Id = entity.EmpId,
                LinkExists = entity.LinkBankId.HasValue,
                CreateMode = !entity.LinkBankId.HasValue && entity.StatusIdentif_Code != StatusIdentifikacii.NotConfirmedByBank,
                Kodpl = entity.Kodpl,
                PeriodRegDate = entity.PeriodRegDate,
                PmtDate = entity.PmtDate,
                Nom = entity.Nom,
                SumOpl = entity.SumOpl,
                Adres = entity.Adres,
                Unom = entity.Unom,
                TxId = entity.TxId,
                Period = entity.Period,
                Opl = entity.Opl
            };

            if (platDto.LinkExists)
            {
                platDto.BankPlats = OMBankPlat.Where(x => x.EmpId == entity.LinkBankId)
                    .SelectAll()
                    .Execute()
                    .Select(x => BankPlatDto.OMMap(x))
                    .ToList();
            }
            else if (entity.StatusIdentif_Code != StatusIdentifikacii.NotConfirmedByBank)
            {
                platDto.BankPlats = new QSQuery
                {
                    MainRegisterID = OMBankPlat.GetRegisterId(),
                    Columns = new List<QSColumn>
                    {
                        OMBankPlat.GetColumn(x => x.PeriodRegDate),
                        OMBankPlat.GetColumn(x => x.DataPp),
                        OMBankPlat.GetColumn(x => x.Kodpl),
                        OMBankPlat.GetColumn(x => x.SumByCode),
                        OMBankPlat.GetColumn(x => x.NomDoc),
                        OMBankPlat.GetColumn(x => x.CodDoc),
                        OMBankPlat.GetColumn(x => x.Period),
                        OMBankPlat.GetColumn(x => x.FlagVozvr)
                    },
                    Condition = new QSConditionGroup
                    {
                        Type = QSConditionGroupType.And,
                        Conditions = new List<QSCondition>
                        {
                            new QSConditionSimple
                            {
                                ConditionType = QSConditionType.Equal,
                                LeftOperand = OMBankPlat.GetColumn(x => x.Kodpl),
                                RightOperand = new QSColumnConstant(entity.Kodpl)
                            },
                            new QSConditionSimple
                            {
                                ConditionType = QSConditionType.Equal,
                                LeftOperand = OMBankPlat.GetColumn(x => x.PeriodRegDate),
                                RightOperand = new QSColumnConstant(entity.PeriodRegDate)
                            },
                            new QSConditionSimple
                            {
                                ConditionType = QSConditionType.NotExists,
                                LeftOperand = new QSColumnQuery
                                {
                                    SubQuery = new QSQuery
                                    {
                                        MainRegisterID = OMInputPlat.GetRegisterId(),
                                        Columns = new List<QSColumn>
                                        {
                                            new QSColumnConstant(1)
                                        },
                                        Condition = new QSConditionSimple
                                        {
                                            ConditionType = QSConditionType.Equal,
                                            LeftOperand = OMInputPlat.GetColumn(x => x.LinkBankId),
                                            LeftOperandLevel = 0,
                                            RightOperand = OMBankPlat.GetColumn(x => x.EmpId),
                                            RightOperandLevel = 1
                                        }
                                    }
                                },
                                LeftOperandLevel = 0
                            }
                        }
                    }
                }
                .ExecuteQuery<OMBankPlat>()
                .Select(x => new BankPlatDto
                {
                    Id = x.EmpId.Value,
                    PeriodRegDate = x.PeriodRegDate,
                    DataPp = x.DataPp,
                    Kodpl = x.Kodpl,
                    SumByCode = x.SumByCode,
                    NomDoc = x.NomDoc,
                    CodDoc = x.CodDoc,
                    Period = x.Period,
                    FlagVozvr = x.FlagVozvr
                })
                .ToList();
            }

            return platDto;
        }
    }
}
