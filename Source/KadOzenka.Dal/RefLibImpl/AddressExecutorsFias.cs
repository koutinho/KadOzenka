using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Core.RefLib;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using System.ComponentModel.Composition;
using ObjectModel.Fias;

namespace CIPJS.DAL.RefLibImpl
{
    [Serializable]
    [Export(typeof(IReferenceExecutor))]
    public class AddressExecutorsFias : IReferenceExecutor
    {
        private const int MaxItemCount = 20000;

        public DataTable CreateItemList(int referenceId, RefParameters parameters, string additonalParams = "")
        {
            additonalParams = additonalParams.Replace("LIKE '%'", "");
            if (additonalParams.Length > 0)
            {
                additonalParams = " Upper(Value) " + additonalParams.ToUpper();
            }

            string parentAoGuid = null;

            if (parameters != null)
            {
                foreach (RefParameter parameter in parameters.GetParameters().OrderBy(x => x.ReferenceId.ParseToInt()))
                {
                    if (parameter.ItemId.IsNotEmpty())
                    {
                        parentAoGuid = parameter.ItemId;
                    }
                }
            }

            if (parentAoGuid != null)
            {
                parentAoGuid = parentAoGuid.ToLower();
            }

            QSQuery query = new QSQuery
            {
                MainRegisterID = OMAddrObj.GetRegisterId(),
                Columns = new List<QSColumn>
                {
                    new QSColumnFunction
                    {
                        Alias = "VALUE",
                        FunctionType = QSColumnFunctionType.Concatenation,
                        Operands = new List<QSColumn>
                        {
                            OMAddrObj.GetColumn(x => x.FormalName),
                            new QSColumnConstant(" "),
                            OMAddrObj.GetColumn(x => x.ShortName),
                        }
                    },
                    new QSColumnFunction
                    {
                        Alias = "SName",
                        FunctionType = QSColumnFunctionType.Concatenation,
                        Operands = new List<QSColumn>
                        {
                            OMAddrObj.GetColumn(x => x.FormalName),
                            new QSColumnConstant(" "),
                            OMAddrObj.GetColumn(x => x.ShortName),
                        }
                    },
                    new QSColumnFunctionExternal
                    {
                        Alias = "ItemId",
                        FunctionName = "Upper",
                        Operands = new List<QSColumn>
                        {
                            OMAddrObj.GetColumn(x => x.AoGuid, "ItemId")
                        }
                    }
                },
                Condition = OMAddrObj.GetCondition(x => x.ActStatus == 1 && x.ParentGuid == parentAoGuid),
                PackageSize = MaxItemCount,
                OrderBy = new List<QSOrder>
                {
                    new QSOrder
                    {
                        Column = OMAddrObj.GetColumn(x => x.FormalName)
                    }
                }
            };

            switch (referenceId)
            {
                case 401: //Субъект РФ
                    query.Condition = query.Condition.And(OMAddrObj.GetCondition(x => x.AoLevel == 1));
                    break;
                case 402: //Округ                        
                    query.Condition = query.Condition.And(OMAddrObj.GetCondition(x => 1 == 0));
                    break;
                case 403: //Район
                    query.Condition = query.Condition.And(OMAddrObj.GetCondition(x => x.AoLevel == 3));
                    break;
                case 404: //Город
                    query.Condition = query.Condition.And(OMAddrObj.GetCondition(x => x.AoLevel == 4));
                    break;
                case 405: //НП
                    query.Condition = query.Condition.And(OMAddrObj.GetCondition(x => x.AoLevel == 5));
                    break;
                case 406: //ЭПС
                    query.Condition = query.Condition.And(OMAddrObj.GetCondition(x => x.AoLevel == 6));
                    break;
                case 407: //Улицы
                    query.Condition = query.Condition.And(OMAddrObj.GetCondition(x => x.AoLevel == 7));
                    break;
                case 408: //Дома
                    List<QSColumn> operands = new List<QSColumn>
                    {
                        new QSColumnIf
                        {
                            Blocks = new List<QSColumnIfBlock>
                            {
                                new QSColumnIfBlock
                                {
                                    Condition = OMHouse.GetCondition(x => x.HouseNum != null),
                                    Result = new QSColumnFunction
                                    {
                                        FunctionType = QSColumnFunctionType.Concatenation,
                                        Operands = new List<QSColumn>
                                        {
                                            new QSColumnSwitch
                                            {
                                                ValueToCompare = OMHouse.GetColumn(x => x.Estatus),
                                                Blocks = new List<QSColumnSwitchBlock>
                                                {
                                                    new QSColumnSwitchBlock
                                                    {
                                                        ValueToCompare = new QSColumnConstant(1),
                                                        Result = new QSColumnConstant("Владение"),
                                                    },
                                                    new QSColumnSwitchBlock
                                                    {
                                                        ValueToCompare = new QSColumnConstant(2),
                                                        Result = new QSColumnConstant("Дом"),
                                                    },
                                                    new QSColumnSwitchBlock
                                                    {
                                                        ValueToCompare = new QSColumnConstant(3),
                                                        Result = new QSColumnConstant("Домовладение"),
                                                    },
                                                    new QSColumnSwitchBlock
                                                    {
                                                        ValueToCompare = new QSColumnConstant(4),
                                                        Result = new QSColumnConstant("Участок"),
                                                    },
                                                    new QSColumnSwitchBlock
                                                    {
                                                        Result = new QSColumnConstant("Дом"),
                                                    },
                                                }
                                            },
                                            new QSColumnConstant(" "),
                                            OMHouse.GetColumn(x => x.HouseNum)
                                        }
                                    }
                                }
                            }
                        },
                        new QSColumnIf
                        {
                            Blocks = new List<QSColumnIfBlock>
                            {
                                new QSColumnIfBlock
                                {
                                    Condition = OMHouse.GetCondition(x => x.BuildNum != null || x.StrucNum != null),
                                    Result = new QSColumnConstant("; "),
                                }
                            }
                        },
                        new QSColumnIf
                        {
                            Blocks = new List<QSColumnIfBlock>
                            {
                                new QSColumnIfBlock
                                {
                                    Condition = OMHouse.GetCondition(x => x.BuildNum != null),
                                    Result = new QSColumnFunction
                                    {
                                        FunctionType = QSColumnFunctionType.Concatenation,
                                        Operands = new List<QSColumn>
                                        {
                                            new QSColumnConstant("Корпус "),
                                            OMHouse.GetColumn(x => x.BuildNum),
                                        }
                                    }
                                }
                            }
                        },
                        new QSColumnIf
                        {
                            Blocks = new List<QSColumnIfBlock>
                            {
                                new QSColumnIfBlock
                                {
                                    Condition = OMHouse.GetCondition(x => x.BuildNum != null && x.StrucNum != null),
                                    Result = new QSColumnConstant("; "),
                                }
                            }
                        },
                        new QSColumnIf
                        {
                            Blocks = new List<QSColumnIfBlock>
                            {
                                new QSColumnIfBlock
                                {
                                    Condition = OMHouse.GetCondition(x => x.StrucNum != null),
                                    Result = new QSColumnFunction
                                    {
                                        FunctionType = QSColumnFunctionType.Concatenation,
                                        Operands = new List<QSColumn>
                                        {
                                            new QSColumnSwitch
                                            {
                                                ValueToCompare = OMHouse.GetColumn(x => x.StrStatus),
                                                Blocks = new List<QSColumnSwitchBlock>
                                                {
                                                    new QSColumnSwitchBlock
                                                    {
                                                        ValueToCompare = new QSColumnConstant(1),
                                                        Result = new QSColumnConstant("Строение"),
                                                    },
                                                    new QSColumnSwitchBlock
                                                    {
                                                        ValueToCompare = new QSColumnConstant(2),
                                                        Result = new QSColumnConstant("Сооружение"),
                                                    },
                                                    new QSColumnSwitchBlock
                                                    {
                                                        ValueToCompare = new QSColumnConstant(3),
                                                        Result = new QSColumnConstant("Литер"),
                                                    },
                                                    new QSColumnSwitchBlock
                                                    {
                                                        Result = new QSColumnConstant("Строение"),
                                                    },
                                                }
                                            },
                                            new QSColumnConstant(" "),
                                            OMHouse.GetColumn(x => x.StrucNum)
                                        }
                                    }
                                }
                            }
                        },
                    };

                    query = new QSQuery
                    {
                        MainRegisterID = OMHouse.GetRegisterId(),
                        Columns = new List<QSColumn>
                        {
                            new QSColumnFunction
                            {
                                Alias = "VALUE",
                                FunctionType = QSColumnFunctionType.Concatenation,
                                Operands = operands
                            },
                            new QSColumnFunction
                            {
                                Alias = "SNAME",
                                FunctionType = QSColumnFunctionType.Concatenation,
                                Operands = operands
                            },
                            new QSColumnFunctionExternal
                            {
                                Alias = "ItemId",
                                FunctionName = "Upper",
                                Operands = new List<QSColumn>
                                {
                                    OMHouse.GetColumn(x => x.HouseGuid, "ItemId")
                                }
                            }
                        },
                        Condition = OMHouse.GetCondition(x => x.AoGuid == parentAoGuid),
                        PackageSize = MaxItemCount
                    };
                    break;
                default:
                    throw new Exception("Неверный идентификатор справочника");
            }

            DataTable dataTable = query.ExecuteQuery();


            return dataTable;
        }

        #region NotImplementedException

        public int GetItemCount(int referenceId, string strFilter, string fk = "")
        {
            throw new NotImplementedException();
        }

        public int GetItemParent(int referenceId, int itemId, string valueEx = "")
        {
            throw new NotImplementedException();
        }

        public int GetItemId(int referenceId, string itemValue)
        {
            throw new NotImplementedException();
        }

        public string GetItemCode(int referenceId, int itemId)
        {
            throw new NotImplementedException();
        }

        public string GetItemValue(int referenceId, int itemId)
        {
            throw new NotImplementedException();
        }

        public string GetItemShortTitle(int referenceId, int itemId)
        {
            throw new NotImplementedException();
        }

        public bool IsItemCountToMatch(int referenceId, string filter)
        {
            throw new NotImplementedException();
        }

        public bool DestroyItem(int itemCode, bool unDestroy)
        {
            throw new NotImplementedException();
        }

        public int AddItem(int refernceId, string itemValue, string itemCode, string shortTitle = "", int parentId = 0, int parentReferenceId = 0)
        {
            throw new NotImplementedException();
        }

        public bool UpdateItem(int itemId, int refernceId, string itemValue, string itemCode, string shortTitle = "")
        {
            throw new NotImplementedException();
        }

        public bool IsItemId(int refernceId, int itemId)
        {
            throw new NotImplementedException();
        }

        public bool IsItemCode(int refernceId, int code)
        {
            throw new NotImplementedException();
        }

        public bool IsItemValue(int refernceId, object value)
        {
            throw new NotImplementedException();
        }

        public int ParentLevel(int refernceId, int parentLevel)
        {
            throw new NotImplementedException();
        }

        public DataTable GetParentDataTable(int referenceId, int itemId, string strValueEx)
        {
            throw new NotImplementedException();
        }

        public DataTable GetDataFromString(int referenceId, string value)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
