using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using ObjectModel.Insur;
using ObjectModel.Fias;

namespace CIPJS.DAL.Fias
{
    public class FiasSearch
    {
        private static readonly List<string> AddrObjParams = new List<string>
        {
            "FiasAddrObjLevel1",
            "FiasAddrObjLevel3",
            "FiasAddrObjLevel4",
            "FiasAddrObjLevel5",
            "FiasAddrObjLevel6",
            "FiasAddrObjLevel7",
        };

        private const string HouseParam = "FiasHouse";

        public static QSCondition BuildFiasCondition(NameValueCollection navigationParameters)
        {
            string parentAoGuid = null;

            foreach (string addrObjParam in AddrObjParams)
            {
                if (navigationParameters.AllKeys.Contains(addrObjParam) &&
                    navigationParameters[addrObjParam].IsNotEmpty())
                {
                    parentAoGuid = navigationParameters[addrObjParam].ToLower();
                }
            }

            string houseGuid = null;
            if (navigationParameters.AllKeys.Contains(HouseParam))
            {
                houseGuid = navigationParameters[HouseParam];
            }

            if (parentAoGuid.IsNullOrEmpty() && houseGuid.IsNullOrEmpty())
            {
                return null;
            }

            if (houseGuid.IsNotEmpty())
            {
                return new QSConditionSimple
                {
                    ConditionType = QSConditionType.In,
                    LeftOperand = OMBuilding.GetColumn(x => x.GuidFiasMkd.ToString()),
                    RightOperand = new QSColumnQuery
                    {
                        SubQuery = new QSQuery
                        {
                            MainRegisterID = OMBuilding.GetRegisterId(),
                            Condition = OMBuilding.GetCondition(x => x.GuidFiasMkd.ToString() == houseGuid)
                        }
                    }
                };
            }

            OMAddrObj addrObj =
                OMAddrObj.Where(x => x.ActStatus == 1 && x.AoGuid == parentAoGuid)
                    .Select(x => x.Code)
                    .Select(x => x.AoLevel)

                    .Select(x => x.RegionCode)
                    .Select(x => x.AutoCode)
                    .Select(x => x.AreaCode)
                    .Select(x => x.CityCode)
                    .Select(x => x.CtarCode)
                    .Select(x => x.PlaceCode)
                    .Select(x => x.StreetCode)

                    .Execute()
                    .FirstOrDefault();

            if (addrObj == null)
            {
                throw new Exception("Не найден адрес ФИАС с ГУИД " + parentAoGuid);
            }

            string code = "";

            string[] codeFacets =
            {
                addrObj.RegionCode, addrObj.AutoCode, addrObj.AreaCode, addrObj.CityCode, addrObj.CtarCode, addrObj.PlaceCode, addrObj.StreetCode
            };

            for (int i = 0; i < addrObj.AoLevel; i++)
            {
                code += codeFacets[i];
            }

            return new QSConditionSimple
            {
                ConditionType = QSConditionType.In,
                LeftOperand = OMBuilding.GetColumn(x => x.GuidFiasMkd.ToString()),
                RightOperand = new QSColumnQuery
                {
                    SubQuery = new QSQuery
                    {
                        MainRegisterID = OMBuilding.GetRegisterId(),
                        Columns = new List<QSColumn>
                        {
                            OMBuilding.GetColumn(x => x.GuidFiasMkd.ToString())
                        },
                        ManualJoin = true,
                        Joins = new List<QSJoin>
                        {
                            new QSJoin
                            {
                                RegisterId = OMHouse.GetRegisterId(),
                                JoinCondition = new QSConditionSimple
                                {
                                    ConditionType = QSConditionType.EqualNonCaseSensitive,
                                    LeftOperand = OMHouse.GetColumn(x => x.HouseGuid),
                                    RightOperand = OMBuilding.GetColumn(x => x.GuidFiasMkd.ToString())
                                }
                            },
                            new QSJoin
                            {
                                RegisterId = OMAddrObj.GetRegisterId(),
                                JoinCondition = new QSConditionGroup
                                {
                                    Type = QSConditionGroupType.And,
                                    Conditions = new List<QSCondition>
                                    {
                                        OMAddrObj.GetCondition(x => x.ActStatus == 1),
                                        new QSConditionSimple
                                        {
                                            ConditionType = QSConditionType.Equal,
                                            LeftOperand = OMAddrObj.GetColumn(x => x.AoGuid),
                                            RightOperand = OMHouse.GetColumn(x => x.AoGuid)
                                        }
                                    }
                                }
                            }
                        },
                        Condition = new QSConditionSimple
                        {
                            ConditionType = QSConditionType.Like,
                            LeftOperand = new QSColumnFunction
                            {
                                FunctionType = QSColumnFunctionType.Concatenation,
                                Operands = new List<QSColumn>
                                {
                                    OMAddrObj.GetColumn(x => x.RegionCode),
                                    OMAddrObj.GetColumn(x => x.AutoCode),
                                    OMAddrObj.GetColumn(x => x.AreaCode),
                                    OMAddrObj.GetColumn(x => x.CityCode),
                                    OMAddrObj.GetColumn(x => x.CtarCode),
                                    OMAddrObj.GetColumn(x => x.PlaceCode),
                                    OMAddrObj.GetColumn(x => x.StreetCode),
                                },
                            },
                            RightOperand = new QSColumnConstant(code + "%")
                        }
                    }
                }
            };
        }
    }
}
