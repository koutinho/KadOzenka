using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using ObjectModel.Directory;
using ObjectModel.Insur;
using System;
using System.Collections.Generic;
using System.Data;

namespace CIPJS.Models.InputFilePackage
{
    public class InputFilePackageProcessOkrugDto
    {
        public long Id { get; set; }

        public string OkrugName { get; set; }

        public long LoadNachCount { get; set; }

        public long LoadPlatCount { get; set; }

        public static InputFilePackageProcessOkrugDto OMMap(long inputFilePackageId)
        {
            QSQuery query = new QSQuery
            {
                MainRegisterID = OMInputFilePackage.GetRegisterId(),
                Columns = new List<QSColumn>
                {
                    OMOkrug.GetColumn(x => x.ShortName, "OkrugName"),
                    new QSColumnQuery
                    {
                        Alias = "LoadNachCount",
                        SubQuery = new QSQuery
                        {
                            MainRegisterID = OMInputFile.GetRegisterId(),
                            Columns = new List<QSColumn>()
                            {
                                new QSColumnFunction
                                {
                                    FunctionType = QSColumnFunctionType.Count,
                                    Operands = new List<QSColumn>
                                    {
                                        new QSColumnConstant(1)
                                    }
                                }
                            },
                            Condition = new QSConditionGroup
                            {
                                Type = QSConditionGroupType.And,
                                Conditions = new List<QSCondition>
                                {
                                    OMInputFile.GetCondition(x => x.TypeFile_Code == TypeFile.Nach),
                                    OMInputFile.GetCondition(x => x.Status_Code == UFKFileProcessingStatus.Loaded),
                                    new QSConditionSimple
                                    {
                                        ConditionType = QSConditionType.Equal,
                                        LeftOperand = OMInputFile.GetColumn(x => x.LinkPackage),
                                        RightOperand = OMInputFilePackage.GetColumn(x => x.Id),
                                        RightOperandLevel = 1
                                    }
                                }
                            }
                        }
                    },
                    new QSColumnQuery
                    {
                        Alias = "LoadPlatCount",
                        SubQuery = new QSQuery
                        {
                            MainRegisterID = OMInputFile.GetRegisterId(),
                            Columns = new List<QSColumn>()
                            {
                                new QSColumnFunction
                                {
                                    FunctionType = QSColumnFunctionType.Count,
                                    Operands = new List<QSColumn>
                                    {
                                        new QSColumnConstant(1)
                                    }
                                }
                            },
                            Condition = new QSConditionGroup
                            {
                                Type = QSConditionGroupType.And,
                                Conditions = new List<QSCondition>
                                {
                                    OMInputFile.GetCondition(x => x.TypeFile_Code == TypeFile.Strah),
                                    OMInputFile.GetCondition(x => x.Status_Code == UFKFileProcessingStatus.LinkedBankCompletely || x.Status_Code == UFKFileProcessingStatus.LinkedBankPartially || x.Status_Code == UFKFileProcessingStatus.Loaded),
                                    new QSConditionSimple
                                    {
                                        ConditionType = QSConditionType.Equal,
                                        LeftOperand = OMInputFile.GetColumn(x => x.LinkPackage),
                                        RightOperand = OMInputFilePackage.GetColumn(x => x.Id),
                                        RightOperandLevel = 1
                                    }
                                }
                            }
                        }
                    }
                },
                Joins = new List<QSJoin>
                {
                    new QSJoin
                    {
                        RegisterId = OMOkrug.GetRegisterId(),
                        JoinType = QSJoinType.Inner,
                        JoinCondition = new QSConditionGroup
                        {
                            Type = QSConditionGroupType.And,
                            Conditions = new List<QSCondition>
                            {
                                OMInputFilePackage.GetCondition(x => x.OkrugId == x.ParentOkrug.Id)
                            }
                        }
                    }
                },
                Condition = new QSConditionGroup
                {
                    Type = QSConditionGroupType.And,
                    Conditions = new List<QSCondition>
                    {
                        OMInputFilePackage.GetCondition(x => x.Id == inputFilePackageId)
                    }
                }
            };

            DataTable dt = query.ExecuteQuery();

            if (dt.Rows.Count == 0)
            {
                throw new Exception($"Не удалось определить пакета загрузки по идентификатору {inputFilePackageId}");
            }

            return new InputFilePackageProcessOkrugDto
            {
                Id = dt.Rows[0]["ID"].ParseToLong(),
                OkrugName = dt.Rows[0]["OkrugName"] != DBNull.Value ? dt.Rows[0]["OkrugName"].ToString() : null,
                LoadNachCount = dt.Rows[0]["LoadNachCount"].ParseToLong(),
                LoadPlatCount = dt.Rows[0]["LoadPlatCount"].ParseToLong()
            };
        }
    }
}
