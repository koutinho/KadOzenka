using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using Core.RefLib;
using Core.Register;
using Core.Register.QuerySubsystem;
using Core.Register.RegisterEntities;
using Core.Shared.Exceptions;
using Core.Shared.Extensions;
using ObjectModel.Core.TD;
using Serilog;
using SerilogTimings.Extensions;

namespace KadOzenka.Dal.DataExport
{
    public static class CoreGetAttributesTrimmed
    {
        static ILogger log = Log.ForContext("SourceContext", nameof(CoreGetAttributesTrimmed));

        public static DataTable GetAttributes(int iObjectID, int iRegisterID, DateTime dtStart = new DateTime(),
            DateTime dtEnd = new DateTime(), long[] aAttributeIDs = null, int iChangeID = -1)
        {
            switch (RegisterCache.Registers[iRegisterID].StorageType)
            {
                case StorageType.Type2:
                case StorageType.Type4:
                    return GetLifeByQuant(iObjectID, iRegisterID, aAttributeIDs, dtStart, dtEnd);
                default:
                    throw new RegisterStorageException(string.Format(
                        "Метод GetAttributes не реализован для {0} типа реестра",
                        RegisterCache.Registers[iRegisterID].StorageType));
            }
        }

        private static DataTable GetLifeByQuant(int objectId, int registerId, long[] attributes, DateTime dtStart,
            DateTime dtEnd)
        {
            QSQuery query;
            RegisterData registerData;
            // Получаем всю историю изменения объекта, кроме черновых записей
            query = new QSQuery
            {
                MainRegisterID = registerId,
                Condition = new QSConditionGroup
                {
                    Type = QSConditionGroupType.And,
                    Conditions = new List<QSCondition>
                    {
                        new QSConditionSimple(RegisterCache.GetPKAttributeID(registerId), QSColumnSimpleType.Value,
                            QSConditionType.Equal, objectId),
                        new QSConditionSimple
                        {
                            ConditionType = QSConditionType.NotEqual,
                            LeftOperand = new QSColumnSpecial
                            {
                                RegisterID = registerId,
                                Type = QSColumnSpecialType.STATUS
                            },
                            RightOperand = new QSColumnConstant(1)
                        }
                    }
                },
                Joins = new List<QSJoin>
                {
                    new QSJoin
                    {
                        RegisterId = registerId,
                        ActualDate = null
                    }
                }
            };

            if (attributes == null)
            {
                attributes = RegisterCache.RegisterAttributes.Values.Where(cur => cur.RegisterId == registerId)
                    .Select(cur => cur.Id).ToArray();
            }

            foreach (long attributeId in attributes)
            {
                RegisterAttribute attributeData = RegisterCache.GetAttributeData(attributeId);

                if (!String.IsNullOrEmpty(attributeData.ValueField))
                {
                    query.Columns.Add(new QSColumnSimple(attributeId, attributeData.ValueField));
                }

                if (!String.IsNullOrEmpty(attributeData.CodeField))
                {
                    query.Columns.Add(new QSColumnSimple(attributeId, attributeData.CodeField,
                        QSColumnSimpleType.Code));
                }
            }

            registerData = RegisterCache.GetRegisterData(registerId);

            if (registerData.TrackChangesColumn.IsNotEmpty())
            {
                query.Columns.Add(new QSColumnQuery
                {
                    Alias = "CHANGE_ID",
                    SubQuery = new QSQuery
                    {
                        MainRegisterID = OMChange.GetRegisterId(),
                        Columns = new List<QSColumn>
                        {
                            new QSColumnFunction
                            {
                                FunctionType = QSColumnFunctionType.Min,
                                Operands = new List<QSColumn>
                                {
                                    OMChange.GetColumn(x => x.Id)
                                }
                            }
                        },
                        Condition = new QSConditionGroup
                        {
                            Type = QSConditionGroupType.And,
                            Conditions = new List<QSCondition>
                            {
                                OMChange.GetCondition(x => x.ObjectId == objectId && x.RegisterId == registerId),
                                new QSConditionSimple
                                {
                                    ConditionType = QSConditionType.Equal,
                                    LeftOperand = OMChange.GetColumn(x => x.QuantId),
                                    RightOperand = new QSColumnSpecial(QSColumnSpecialType.ID, registerId),
                                    RightOperandLevel = 1
                                }
                            }
                        }
                    }
                });
            }
            else
            {
                query.Columns.Add(new QSColumnConstant {Value = null, Alias = "CHANGE_ID"});
            }

            if (registerData.TrackChangesUserId.IsNotEmpty())
            {
                query.Columns.Add(new QSColumnSpecial(QSColumnSpecialType.CHANGE_USER_ID, registerId,
                    "CHANGE_USER_ID"));
            }
            else
            {
                query.Columns.Add(new QSColumnConstant {Value = null, Alias = "CHANGE_USER_ID"});
            }

            if (registerData.TrackChangesDate.IsNotEmpty())
            {
                query.Columns.Add(new QSColumnSpecial(QSColumnSpecialType.CHANGE_DATE, registerId, "CHANGE_DATE"));
                query.Columns.Add(new QSColumnSpecial(QSColumnSpecialType.CHANGE_DATE, registerId, "S_"));
            }
            else
            {
                query.Columns.Add(new QSColumnConstant {Value = null, Alias = "CHANGE_DATE"});
                query.Columns.Add(new QSColumnSpecial(QSColumnSpecialType.S, registerId, "S_"));
            }

            query.Columns.Add(new QSColumnSpecial(QSColumnSpecialType.PO, registerId, "PO_"));

            // Обязательна сортировка по дате "С"
            query.OrderBy = new List<QSOrder>
            {
                new QSOrder
                {
                    ColumnAlias = "S_"
                }
            };

            DataTable dtQuant;
            using (log.ForContext("SQL", query.GetSql()).TimeOperation("Выполнение ExecuteQuery"))
            {
                dtQuant = query.ExecuteQuery();
            }

            // Формируем отрезки Life.
            // Ключ - идентификатор типа атрибута. Значение - связанная цепь отрезков Life.
            var lifeDictionary = new Dictionary<long, Life>();
            var cachedAttributes = RegisterCache.RegisterAttributes.Values;
            var stringCulture = CultureInfo.CurrentCulture.CompareInfo;
            using (log.ForContext("Rows", dtQuant.Rows.Count)
                .ForContext("Columns", dtQuant.Columns.Count)
                .TimeOperation("Формирование отрезков life"))
            {
                foreach (DataRow row in dtQuant.Rows)
                {
                    foreach (DataColumn dataColumn in dtQuant.Columns)
                    {
                        RegisterAttribute attribute;
                        attribute = cachedAttributes.FirstOrDefault(p =>
                            (stringCulture.Compare(p.CodeField, dataColumn.ColumnName,
                                 CompareOptions.IgnoreCase) == 0 ||
                             stringCulture.Compare(p.ValueField, dataColumn.ColumnName,
                                 CompareOptions.IgnoreCase) == 0) && p.RegisterId == registerId);


                        if (attribute == null)
                        {
                            continue;
                        }

                        if (attribute.IsPrimaryKey &&
                            RegisterCache.Registers[registerId].StorageType == StorageType.Type4)
                        {
                            continue;
                        }

                        long attributeId = attribute.Id;
                        Life life;

                        DateTime dateTimeS = row["S_"].ParseTo<DateTime>();
                        DateTime dateTimePo = row["PO_"].ParseTo<DateTime>();

                        // Если для данного показателя еще нет ни одного отрезка Life
                        if (!lifeDictionary.TryGetValue(attributeId, out life))
                        {
                            if ((attribute.ValueField.IsNotEmpty() && row[attribute.ValueField] != DBNull.Value) ||
                                (attribute.CodeField.IsNotEmpty() && row[attribute.CodeField] != DBNull.Value))
                            {
                                life = new Life
                                {
                                    Attribute = attribute,
                                    S = dateTimeS,
                                    PO = dateTimePo,
                                    QuantRow = row
                                };
                                lifeDictionary.Add(attributeId, life);
                            }

                            continue;
                        }

                        // Если для данного показателя уже существует история
                        if (!IsDataRowsEqual(life.QuantRow, row, attribute))
                        {
                            lifeDictionary[attributeId] = new Life
                            {
                                Attribute = attribute,
                                S = dateTimeS,
                                PO = dateTimePo,
                                QuantRow = row,
                                PrevLife = life
                            };
                        }
                        else
                        {
                            lifeDictionary[attributeId].PO = dateTimePo;
                        }
                    }
                }
            }

            DataTable dtLife = new DataTable();
            dtLife.Columns.Add("OBJECT_ID", typeof(int));
            dtLife.Columns.Add("ATTRIBUTE_ID", typeof(int)); // 1
            dtLife.Columns.Add("OT", typeof(DateTime));
            dtLife.Columns.Add("S", typeof(DateTime));
            dtLife.Columns.Add("PO", typeof(DateTime));
            dtLife.Columns.Add("REF_ITEM_ID", typeof(int));
            dtLife.Columns.Add("TEXT_VALUE", typeof(string)); //6
            dtLife.Columns.Add("NUMBER_VALUE", typeof(decimal)); //7
            dtLife.Columns.Add("DATE_VALUE", typeof(DateTime));
            dtLife.Columns.Add("CHANGE_ID", typeof(int));
            dtLife.Columns.Add("CHANGE_USER_ID", typeof(int));
            dtLife.Columns.Add("CHANGE_DATE", typeof(DateTime));

            // Имя показателя
            dtLife.Columns.Add("NAME", typeof(string));

            // Идентификатор родительского показателя
            dtLife.Columns.Add("PARENT_ID", typeof(int));

            foreach (Life life in lifeDictionary.Values)
            {
                if (life.PrevLife == null)
                {
                    if (!string.IsNullOrEmpty(life.Attribute.ValueField))
                    {
                        if (life.QuantRow[life.Attribute.ValueField] is DBNull)
                        {
                            if (!string.IsNullOrEmpty(life.Attribute.CodeField))
                            {
                                if (life.QuantRow[life.Attribute.CodeField] is DBNull)
                                {
                                    continue;
                                }
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }
                    else if (!string.IsNullOrEmpty(life.Attribute.CodeField) &&
                             life.QuantRow[life.Attribute.CodeField] is DBNull)
                    {
                        continue;
                    }
                }

                AddRowToLife(dtLife, life, objectId);
            }


            if (registerData.AllpriTable.IsNotEmpty())
            {
                string sqlAllpri = $"select * from {registerData.AllpriTable} where OBJECT_ID = {objectId}";

                if (attributes != null)
                {
                    sqlAllpri += " and attribute_id in (" + String.Join(",", attributes) + ")";
                }

                var op = log.BeginOperation("ExecuteDataset");
                DbCommand dbCommand = RegisterStorage.GetDatabase(registerId).GetSqlStringCommand(sqlAllpri);
                DataTable allpriDt = RegisterStorage.GetDatabase(registerId).ExecuteDataSet(dbCommand, null, false)
                    .Tables[0];
                op.Complete();

                DateTime? minDate = null;
                bool nullExists = false;

                Dictionary<int, DateTime> historyDatesPo = new Dictionary<int, DateTime>();

                var op1 = log.BeginOperation("обработка 1");
                foreach (DataRow rowAllpri in allpriDt.Rows)
                {
                    var attributeId = rowAllpri["ATTRIBUTE_ID"].ParseToInt();
                    var datePo = rowAllpri["PO"].ParseToDateTime();

                    if (!historyDatesPo.ContainsKey(attributeId)) historyDatesPo.Add(attributeId, datePo);
                    else
                    {
                        if (historyDatesPo[attributeId] < datePo) historyDatesPo[attributeId] = datePo;
                    }

                    if (nullExists) continue;

                    if (rowAllpri["S"] == DBNull.Value)
                    {
                        minDate = null;
                        nullExists = true;
                    }

                    if (minDate == null || minDate > rowAllpri["S"].ParseToDateTime())
                    {
                        minDate = rowAllpri["S"].ParseToDateTime();
                    }
                }

                op1.Complete();

                var op2 = log.BeginOperation("обработка 2");
                // Установка даты С для показателей, которые есть только в Quant (нет истории), на основе минимальной даты с из AllPri
                foreach (DataRow row in dtLife.Rows)
                {
                    var attributeId = row["ATTRIBUTE_ID"].ParseToInt();

                    if (historyDatesPo.ContainsKey(attributeId))
                    {
                        row["OT"] = historyDatesPo[attributeId].AddSeconds(1);
                        row["S"] = historyDatesPo[attributeId].AddSeconds(1);
                        continue;
                    }

                    row["OT"] = minDate ?? (Object) DBNull.Value;
                    row["S"] = minDate ?? (Object) DBNull.Value;
                }

                op2.Complete();

                var op3 = log.BeginOperation("обработка 3");
                foreach (DataRow rowAllpri in allpriDt.Rows)
                {
                    DataRow row = dtLife.NewRow();

                    var attributeData = RegisterCache.GetAttributeData(rowAllpri["ATTRIBUTE_ID"].ParseToInt());

                    row["OBJECT_ID"] = objectId;
                    row["ATTRIBUTE_ID"] = rowAllpri["ATTRIBUTE_ID"];
                    row["OT"] = rowAllpri["S"];
                    row["S"] = rowAllpri["S"];
                    row["PO"] = rowAllpri["PO"];

                    row["REF_ITEM_ID"] = rowAllpri["REF_ITEM_ID"];
                    row["NUMBER_VALUE"] = rowAllpri["NUMBER_VALUE"];
                    row["DATE_VALUE"] = rowAllpri["DATE_VALUE"];
                    row["TEXT_VALUE"] = rowAllpri["TEXT_VALUE"];
                    row["CHANGE_USER_ID"] = rowAllpri["CHANGE_USER_ID"];
                    row["CHANGE_DATE"] = rowAllpri["S"];
                    row["NAME"] = attributeData.Name;
                    if (attributeData.ParentId.HasValue)
                    {
                        row["PARENT_ID"] = attributeData.ParentId;
                    }

                    if (attributeData.ValueField.IsNullOrEmpty() && attributeData.CodeField.IsNotEmpty() &&
                        attributeData.ReferenceId > 0)
                    {
                        row["TEXT_VALUE"] = ReferencesCommon.GetItems(attributeData.ReferenceId.Value, false)
                            .FirstOrDefault(x => x.ItemId == rowAllpri["REF_ITEM_ID"].ParseToInt())
                            ?.Value;
                    }

                    dtLife.Rows.Add(row);
                }

                op3.Complete();
            }

            string sortExpression = "";

            using (log.TimeOperation("Сортировка"))
            {
                if (dtStart != DateTime.MinValue)
                {
                    sortExpression += string.Format("PO > #{0}#", dtStart.ToString(CultureInfo.InvariantCulture));
                }

                if (dtEnd != DateTime.MinValue)
                {
                    sortExpression += (sortExpression.IsNotEmpty() ? " and " : "") +
                                      string.Format("S <= #{0}# OR S IS NULL",
                                          dtEnd.ToString(CultureInfo.InvariantCulture));
                }

                if (sortExpression.IsNotEmpty())
                {
                    dtLife = dtLife.FilteringAndSortingTable(sortExpression);
                }
            }

            return dtLife;
        }

        internal class Life
        {
            /// <summary>
            /// Дата "С" отрезка Life, может отличаться от даты значения AllPri
            /// </summary>
            public DateTime S;

            /// <summary>
            /// Дата "По" отрезка Life, может отличаться от даты значения AllPri
            /// </summary>
            public DateTime PO;

            /// <summary>
            /// Соответствующая запись в AllPri
            /// </summary>
            public DataRow AllPriRow;

            /// <summary>
            /// Соответствующая запись в Quant
            /// </summary>
            public DataRow QuantRow;

            /// <summary>
            /// Показатель
            /// </summary>
            public RegisterAttribute Attribute;

            /// <summary>
            /// Ссылка на предыдущий отрезок Life (с более ранним значением "С" и "По").
            /// </summary>
            public Life PrevLife;

            public int ID
            {
                get { return AllPriRow["ID"].ParseTo<int>(); }
            }

            public DateTime OT
            {
                get { return AllPriRow["OT"].ParseTo<DateTime>(); }
            }

            public Life()
            {
            }

            public Life(DateTime dtS, DateTime dtPO, DataRow drAllPriRow, Life prevLife)
            {
                S = dtS;
                PO = dtPO;
                AllPriRow = drAllPriRow;
                PrevLife = prevLife;
            }
        }

        private static void AddRowToLife(DataTable dtLife, Life life, int objectId)
        {
            while (life != null)
            {
                DataRow row = dtLife.NewRow();

                row["OBJECT_ID"] = objectId;
                row["ATTRIBUTE_ID"] = life.Attribute.Id;
                row["OT"] = life.S;
                row["S"] = life.S;
                row["PO"] = life.PO;

                if (life.Attribute.CodeField.IsNotEmpty())
                {
                    row["REF_ITEM_ID"] = life.QuantRow[life.Attribute.CodeField];
                }

                Object value = null;

                if (life.Attribute.ValueField.IsNotEmpty())
                {
                    value = life.QuantRow[life.Attribute.ValueField];
                }
                else if (life.Attribute.CodeField.IsNotEmpty())
                {
                    var attributeData = RegisterCache.GetAttributeData(life.Attribute.Id);

                    if (attributeData.ReferenceId > 0)
                    {
                        value = ReferencesCommon.GetItems(attributeData.ReferenceId.Value, false)
                            .FirstOrDefault(x => x.ItemId == life.QuantRow[life.Attribute.CodeField].ParseToInt())
                            ?.Value;
                    }
                }

                switch (life.Attribute.Type)
                {
                    case RegisterAttributeType.BOOLEAN:
                    case RegisterAttributeType.DECIMAL:
                    case RegisterAttributeType.INTEGER:
                        row["NUMBER_VALUE"] = value.ParseToDecimalNullable();
                        break;
                    case RegisterAttributeType.DATE:
                        row["DATE_VALUE"] = value.ParseToDateTimeNullable();
                        break;
                    case RegisterAttributeType.STRING:
                        row["TEXT_VALUE"] = value;
                        break;
                }

                row["CHANGE_ID"] = life.QuantRow["CHANGE_ID"];
                row["CHANGE_USER_ID"] = life.QuantRow["CHANGE_USER_ID"];
                row["CHANGE_DATE"] = life.QuantRow["CHANGE_DATE"];
                row["NAME"] = life.Attribute.Name;
                if (life.Attribute.ParentId.HasValue)
                {
                    row["PARENT_ID"] = life.Attribute.ParentId;
                }

                dtLife.Rows.Add(row);

                life = life.PrevLife;
            }
        }

        private static bool IsDataRowsEqual(DataRow dataRow1, DataRow dataRow2, RegisterAttribute attribute)
        {
            bool isDataRowsEqual = true;
            if (attribute.ValueField.IsNotEmpty())
            {
                if (dataRow1[attribute.ValueField].ToString() != dataRow2[attribute.ValueField].ToString())
                {
                    isDataRowsEqual = false;
                }
            }

            if (attribute.CodeField.IsNotEmpty())
            {
                if (dataRow1[attribute.CodeField].ToString() != dataRow2[attribute.CodeField].ToString())
                {
                    isDataRowsEqual = false;
                }
            }

            return isDataRowsEqual;
        }
    }
}