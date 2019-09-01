using CIPJS.DAL.Fsp;
using CIPJS.DAL.InputFile;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using Core.Shared.Misc;
using Core.SRD;
using ObjectModel.Directory;
using ObjectModel.Insur;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace CIPJS.DAL.InputPlat
{
    /// <summary>
    /// Сервис работы с объектами типа ObjectModel.Insur.OMInputPlat
    /// </summary>
    public class InputPlatService
    {
        private InputFileService _inputFileService;

        public InputPlatService()
        {
            _inputFileService = new InputFileService();
        }

        /// <summary>
        /// Получить платеж (зачисление) по его идентификатору
        /// </summary>
        /// <param name="id">Идентификатор платежа</param>
        /// <returns>Платеж</returns>
        public OMInputPlat Get(long? id)
        {
            var entity = OMInputPlat.Where(x => x.EmpId == id).SelectAll().Execute().FirstOrDefault();
            return entity;
        }

        /// <summary>
        /// Получить список платежей (зачислений) по идентификатору ФСП
        /// </summary>
        /// <param name="fspId">Идентификатор ФСП</param>
        /// <returns>Список платежей</returns>
        public List<OMInputPlat> GetByFsp(long? fspId)
        {
            return OMInputPlat.Where(x => x.FspId == fspId)
                .SelectAll()
                .Select(x => x.ParentFsp.OplKodpl)
                .Select(x => x.ParentBankPlat.Period)
                .Select(x => x.ParentBankPlat.DocPeriod)
                .Select(x => x.ParentBankPlat.ParentSvodBank.BankDay)
                .Execute();
        }

        /// <summary>
        /// Получить список платежей по списку их идентификаторов
        /// </summary>
        /// <param name="ids">список идентификаторов платежей</param>
        /// <returns>Список платежей</returns>
        public List<OMInputPlat> Get(List<long> ids)
        {
            var result = new List<OMInputPlat>();

            if (ids.IsNotEmpty())
            {
                result = OMInputPlat.Where(x => ids.Contains(x.EmpId))
                               .SelectAll()
                               .Execute();
            }

            return result;
        }

        /// <summary>
        /// Возвращает список платежей по номеру договора страхования и дате начала действия
        /// </summary>
        /// <param name="ndog">Номер договор страхования</param>
        /// <param name="ndogdate">Дата начала действия</param>
        public List<OMInputPlat> Get(string ndog, DateTime? ndogdate)
        {
            if (ndog.IsNullOrEmpty() && !ndogdate.HasValue) return new List<OMInputPlat>();

            return OMInputPlat.Where(x => x.Ndog == ndog && x.Ndogdat == ndogdate).SelectAll().Execute();
        }

        /// <summary>
        /// Возвращает список платежей по номеру договора страхования
        /// </summary>
        /// <param name="allPropertyId">Идентификатор договора</param>
        public List<OMInputPlat> GetByAllPropertyId(long allPropertyId)
        {
            return OMInputPlat.Where(x => x.LinkAllPropertyId == allPropertyId).SelectAll().Execute();
        }

        /// <summary>
        /// Ручная идентификация зачислений (bankPlatId известен)
        /// также включает в себя зачисление и перерасчет на ФСП
        /// </summary>
        /// <param name="inputPlatId">Идентифкатор зачисления</param>
        /// <param name="bankPlatId">Идентифкатор банковской оплаты</param>
        /// <param name="reason">Причина изменения</param>
        /// <param name="rewrite">Флаг, если банковская оплата уже связана, то необходимо ее отвязать, инчае выдать ошибку</param>
        public void LinkPlat(long inputPlatId, long bankPlatId, string reason, bool rewrite = false)
        {
            OMInputPlat inputPlat = OMInputPlat.Where(x => x.EmpId == inputPlatId).SelectAll().ExecuteFirstOrDefault();

            if (inputPlat == null)
            {
                throw new Exception($"Не удалось определить запись зачисления с идентификатором {inputPlatId}");
            }

            FspService fspService = new FspService();

            using (TransactionScope ts = TransactionScopeWrapper.OpenTransaction())
            {
                if (rewrite)
                {
                    List<OMInputPlat> oldInputPlats = OMInputPlat.Where(x => x.LinkBankId == bankPlatId).SelectAll().Execute();
                    foreach (OMInputPlat oldInputPlat in oldInputPlats)
                    {
                        new OMChangesLog
                        {
                            ObjectId = oldInputPlat.EmpId,
                            LoadDate = DateTime.Now,
                            ReestrId = OMInputPlat.GetRegisterId(),
                            OperationType_Code = ChangeOperationType.StatusIdentif,
                            Reason = reason,
                            OldValue = inputPlat.StatusIdentif_Code.GetEnumDescription(),
                            NewValue = StatusIdentifikacii.NotIdentified.GetEnumDescription(),
                            UserId = SRDSession.GetCurrentUserId()
                        }.Save();

                        new OMChangesLog
                        {
                            ObjectId = inputPlat.EmpId,
                            LoadDate = DateTime.Now,
                            ReestrId = OMInputPlat.GetRegisterId(),
                            OperationType_Code = ChangeOperationType.LinkBankPlat,
                            Reason = reason,
                            OldValue = inputPlat.LinkBankId.HasValue ? inputPlat.LinkBankId.Value.ToString() : null,
                            NewValue = null,
                            UserId = SRDSession.GetCurrentUserId()
                        }.Save();

                        oldInputPlat.LinkBankId = null;
                        oldInputPlat.StatusIdentif_Code = StatusIdentifikacii.NotIdentified;
                        oldInputPlat.Save();

                        //CIPJS-305 Сразу зачислять платеж на ФСП , искать ФСП по коду плательщика, не нашли- создали ФСП и учли ( стандартный функционал)
                        if (oldInputPlat.FspId.HasValue && oldInputPlat.PeriodRegDate.HasValue)
                        {
                            fspService.AccountFsp(oldInputPlat.FspId.Value, oldInputPlat.PeriodRegDate.Value);
                            fspService.CalcBalanceSumFromPeriod(oldInputPlat.FspId.Value, oldInputPlat.PeriodRegDate.Value);
                        }
                    }
                }
                else
                {
                    if (new QSQuery
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
                            RightOperand = new QSColumnConstant(bankPlatId),
                            RightOperandLevel = 1
                        }
                    }.GetCountQuery().ExecuteQuery().Rows[0]["Count"].ParseToInt() > 0)
                    {
                        throw new Exception("Для данной строки банка уже есть связь с зачислением");
                    }
                }

                new OMChangesLog
                {
                    ObjectId = inputPlat.EmpId,
                    LoadDate = DateTime.Now,
                    ReestrId = OMInputPlat.GetRegisterId(),
                    OperationType_Code = ChangeOperationType.StatusIdentif,
                    Reason = reason,
                    OldValue = inputPlat.StatusIdentif_Code.GetEnumDescription(),
                    NewValue = StatusIdentifikacii.Identified.GetEnumDescription(),
                    UserId = SRDSession.GetCurrentUserId()
                }.Save();


                new OMChangesLog
                {
                    ObjectId = inputPlat.EmpId,
                    LoadDate = DateTime.Now,
                    ReestrId = OMInputPlat.GetRegisterId(),
                    OperationType_Code = ChangeOperationType.LinkBankPlat,
                    Reason = reason,
                    OldValue = inputPlat.LinkBankId.HasValue ? inputPlat.LinkBankId.Value.ToString() : null,
                    NewValue = bankPlatId.ToString(),
                    UserId = SRDSession.GetCurrentUserId()
                }.Save();

                //CIPJS-305 Сразу зачислять платеж на ФСП , искать ФСП по коду плательщика, не нашли- создали ФСП и учли ( стандартный функционал)
                if (!inputPlat.FspId.HasValue)
                {
                    List<string> errors;
                    fspService.SetPlatFsp(inputPlat, out errors);
                    //CIPJS-674 если не удалось связать с ФСП выдаем ошибку
                    if (errors != null && errors.Count > 0 && !inputPlat.FspId.HasValue)
                    {
                        throw new Exception(string.Join(Environment.NewLine, errors));
                    }
                }

                inputPlat.LinkBankId = bankPlatId;
                inputPlat.StatusIdentif_Code = StatusIdentifikacii.Identified;
                inputPlat.Save();

                if (inputPlat.FspId.HasValue && inputPlat.PeriodRegDate.HasValue)
                {
                    fspService.AccountFsp(inputPlat.FspId.Value, inputPlat.PeriodRegDate.Value);
                    fspService.CalcBalanceSumFromPeriod(inputPlat.FspId.Value, inputPlat.PeriodRegDate.Value);
                }

                //CIPJS-504 если для этого файла, с которым связана строка нет записей в статусе "Не идентифицирован" 
                //(смотреть по коду статуса) то следовательно статус файла заменить на "Обработан полностью"
                if (inputPlat.LinkIdFile.HasValue &&
                    _inputFileService.IsStrahFileProcessedCompletely(inputPlat.LinkIdFile.Value))
                {
                    OMInputFile inputFile = OMInputFile.Where(x => x.EmpId == inputPlat.LinkIdFile.Value)
                        .Select(x => x.Status)
                        .Select(x => x.Status_Code)
                        .ExecuteFirstOrDefault();

                    if (inputFile != null)
                    {
                        inputFile.Status_Code = UFKFileProcessingStatus.ProcessedCompletely;
                        inputFile.Save();
                    }
                }

                ts.Complete();
            }
        }

        public void NotConfirmedByBank(long inputPlatId, string reason)
        {
            OMInputPlat inputPlat = OMInputPlat.Where(x => x.EmpId == inputPlatId).SelectAll().ExecuteFirstOrDefault();

            if (inputPlat == null)
            {
                throw new Exception($"Не удалось определить запись зачисления с идентификатором {inputPlatId}");
            }

            using (TransactionScope ts = TransactionScopeWrapper.OpenTransaction())
            {
                new OMChangesLog
                {
                    ObjectId = inputPlat.EmpId,
                    ReestrId = OMInputPlat.GetRegisterId(),
                    OperationType_Code = ChangeOperationType.StatusIdentif,
                    UserId = SRDSession.GetCurrentUserId(),
                    OldValue = inputPlat.StatusIdentif_Code.GetEnumDescription(),
                    NewValue = StatusIdentifikacii.NotConfirmedByBank.GetEnumDescription(),
                    Reason = reason
                }.Save();

                if (inputPlat.LinkBankId.HasValue)
                {
                    new OMChangesLog
                    {
                        ObjectId = inputPlat.EmpId,
                        ReestrId = OMInputPlat.GetRegisterId(),
                        UserId = SRDSession.GetCurrentUserId(),
                        OperationType_Code = ChangeOperationType.LinkBankPlat,
                        OldValue = inputPlat.LinkBankId.Value.ToString(),
                        NewValue = null,
                        Reason = reason
                    }.Save();
                }

                inputPlat.LinkBankId = null;
                inputPlat.StatusIdentif_Code = StatusIdentifikacii.NotConfirmedByBank;
                inputPlat.Save();

                //CIPJS-504 если для этого файла, с которым связана строка нет записей в статусе "Не идентифицирован" 
                //(смотреть по коду статуса) то следовательно статус файла заменить на "Обработан полностью"
                if (inputPlat.LinkIdFile.HasValue &&
                    _inputFileService.IsStrahFileProcessedCompletely(inputPlat.LinkIdFile.Value))
                {
                    OMInputFile inputFile = OMInputFile.Where(x => x.EmpId == inputPlat.LinkIdFile.Value)
                        .Select(x => x.Status)
                        .Select(x => x.Status_Code)
                        .ExecuteFirstOrDefault();

                    if (inputFile != null)
                    {
                        inputFile.Status_Code = UFKFileProcessingStatus.ProcessedCompletely;
                        inputFile.Save();
                    }
                }

                ts.Complete();
            }
        }

        /// <summary>
        /// Идентифкация зачисления в автоматическом режиме (bankPlatId неизвестен)
        /// </summary>
        /// <param name="inputPlat">Сущность зачисления</param>
        /// <returns></returns>
        public OMInputPlat IdentifyPlat(OMInputPlat inputPlat)
        {
            //идентификация не нужна
            if (inputPlat.StatusIdentif_Code == StatusIdentifikacii.PartiallyIdentified
                        || inputPlat.StatusIdentif_Code == StatusIdentifikacii.Identified
                        || inputPlat.StatusIdentif_Code == StatusIdentifikacii.NotConfirmedByBank)
            {
                return inputPlat;
            }

            //идентификация для пустой суммы не выполняется
            if (!inputPlat.SumOpl.HasValue)
            {
                inputPlat.StatusIdentif_Code = StatusIdentifikacii.NotIdentified;
                inputPlat.Save();

                return inputPlat;
            }

            OMBankPlat bankPlat = new QSQuery
            {
                MainRegisterID = OMBankPlat.GetRegisterId(),
                Condition = new QSConditionGroup
                {
                    Type = QSConditionGroupType.And,
                    Conditions = new List<QSCondition>
                    {
                        //только не связанные
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
                                        RightOperand = OMBankPlat.GetColumn(x => x.EmpId),
                                        RightOperandLevel = 1
                                    }
                                }
                            }
                        },
                        //код плательщика
                        new QSConditionSimple
                        {
                            ConditionType = QSConditionType.Equal,
                            LeftOperand = OMBankPlat.GetColumn(x => x.Kodpl),
                            RightOperand = new QSColumnConstant(inputPlat.Kodpl)
                        },
                        //дата платежа
                        new QSConditionSimple
                        {
                            ConditionType = QSConditionType.Equal,
                            LeftOperand = OMBankPlat.GetColumn(x => x.DataPp),
                            RightOperand = new QSColumnConstant(inputPlat.PmtDate)
                        },
                        //период учета
                        new QSConditionSimple
                        {
                            ConditionType = QSConditionType.Equal,
                            LeftOperand = OMBankPlat.GetColumn(x => x.PeriodRegDate),
                            RightOperand = new QSColumnConstant(inputPlat.PeriodRegDate)
                        },
                        new QSConditionGroup
                        {
                            Type = QSConditionGroupType.Or,
                            Conditions = new List<QSCondition>
                            {
                                //cумма оплаты
                                new QSConditionSimple
                                {
                                    ConditionType = QSConditionType.Equal,
                                    LeftOperand = OMBankPlat.GetColumn(x => x.SumByCode),
                                    RightOperand = new QSColumnConstant(inputPlat.SumOpl.Value)
                                },
                                //если возврат
                                new QSConditionGroup
                                {
                                    Type = QSConditionGroupType.And,
                                    Conditions = new List<QSCondition>
                                    {
                                        new QSConditionSimple
                                        {
                                            ConditionType = QSConditionType.Equal,
                                            LeftOperand = OMBankPlat.GetColumn(x => x.FlagVozvr),
                                            RightOperand = new QSColumnConstant(1)
                                        },
                                        new QSConditionSimple
                                        {
                                            ConditionType = QSConditionType.Equal,
                                            LeftOperand = new QSColumnFunction
                                            {
                                                FunctionType = QSColumnFunctionType.Abs,
                                                Operands = new List<QSColumn>
                                                {
                                                    OMBankPlat.GetColumn(x => x.SumByCode)
                                                }
                                            },
                                            RightOperand = new QSColumnConstant(Math.Abs(inputPlat.SumOpl.Value))
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }.ExecuteQuery<OMBankPlat>().FirstOrDefault();


            if (bankPlat != null)
            {
                inputPlat.LinkBankId = bankPlat.EmpId;
                inputPlat.StatusIdentif_Code = StatusIdentifikacii.PartiallyIdentified;

                inputPlat.Save();
            }
            else
            {
                inputPlat.StatusIdentif_Code = StatusIdentifikacii.NotIdentified;

                inputPlat.Save();
            }

            return inputPlat;
        }

        /// <summary>
        /// Проверяет наличие связи платежей с договорами. Возвращает список строк с описанием связи.
        /// </summary>
        /// <param name="ids">Список идентикаторов платежей</param>
        public List<string> CheckHasLinkToContract(List<long> ids)
        {
            if (ids.IsEmpty()) return null;

            List<string> messages = new List<string>();
            List<OMInputPlat> omInputPlats = OMInputPlat.Where(x => ids.Contains(x.EmpId))
                .Select(x => x.Ndog)
                .Select(x => x.ParentAllProperty.Ndog)
                .Select(x => x.ParentAllProperty.Ndogdat)
                .Execute();

            omInputPlats.Where(x => x.ParentAllProperty != null).ToList()
                .ForEach(x =>
                {
                    messages.Add($"Платеж №{x.Ndog} уже связан с договором №{x.ParentAllProperty.Ndog} от {x.ParentAllProperty.Ndogdat?.ToString("dd.MM.yyyy")}");
                });

            return messages;
        }

        /// <summary>
        /// Устанавливает связь с договором для списка платежей
        /// </summary>
        /// <param name="ids">Список идентификаторов платежей</param>
        /// <param name="contractId">Идентификатор договора</param>
        public void SetLinkToContract(List<long> ids, long contractId)
        {
            if (ids.IsEmpty()) return;

            List<OMInputPlat> omInputPlats = OMInputPlat.Where(x => ids.Contains(x.EmpId)).Execute();

            using (TransactionScope ts = TransactionScopeWrapper.OpenTransaction())
            {
                foreach (OMInputPlat omInputPlat in omInputPlats)
                {
                    omInputPlat.LinkAllPropertyId = contractId;
                    omInputPlat.Save();
                }

                ts.Complete();
            }
        }
    }
}