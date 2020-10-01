using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using ObjectModel.Directory;
using ObjectModel.KO;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using KadOzenka.Dal.FastReports.StatisticalData.Common;

namespace KadOzenka.Dal.FastReports.StatisticalData
{
    public class InfoAboutCadastralCostDeterminingMethodReport : StatisticalDataReport
    {
        protected override string TemplateName(NameValueCollection query)
        {
            return "InfoAboutCadastralCostDeterminingMethodReport";
        }

        protected override DataSet GetData(NameValueCollection query, HashSet<long> objectList = null)
        {
            var taskIds = GetTaskIdList(query).ToList();
            var operations = GetOperations(taskIds);

            var dataSet = new DataSet();
            var itemTable = GetItemDataTable(operations);
            dataSet.Tables.Add(itemTable);

            return dataSet;
        }


        #region Support Methods

        private List<ReportItem> GetOperations(List<long> taskIds)
        {
            var query = new QSQuery
            {
                MainRegisterID = OMUnit.GetRegisterId(),
                Condition = new QSConditionGroup
                {
                    Type = QSConditionGroupType.And,
                    Conditions = new List<QSCondition>
                    {
                        new QSConditionSimple(OMTask.GetColumn(x => x.Id), QSConditionType.In, taskIds.Select(x => (double)x).ToList())
                    }
                },
                Joins = new List<QSJoin>
                {
                    new QSJoin
                    {
                        RegisterId = OMGroup.GetRegisterId(),
                        JoinCondition = new QSConditionSimple
                        {
                            ConditionType = QSConditionType.Equal,
                            LeftOperand = OMUnit.GetColumn(x => x.GroupId),
                            RightOperand = OMGroup.GetColumn(x => x.Id)
                        },
                        JoinType = QSJoinType.Left
                    },
                    new QSJoin
                    {
                        RegisterId = OMModel.GetRegisterId(),
                        JoinCondition = new QSConditionSimple
                        {
                            ConditionType = QSConditionType.Equal,
                            LeftOperand = OMUnit.GetColumn(x => x.GroupId),
                            RightOperand = OMModel.GetColumn(x => x.GroupId)
                        },
                        JoinType = QSJoinType.Left
                    }
                }
            };

            query.AddColumn(OMUnit.GetColumn(x => x.PropertyType_Code, nameof(ReportItem.ObjectPropertyType)));
            query.AddColumn(OMUnit.GetColumn(x => x.CadastralNumber, nameof(ReportItem.CadastralNumber)));
            query.AddColumn(OMGroup.GetColumn(x => x.GroupName, nameof(ReportItem.GroupName)));
            query.AddColumn(OMGroup.GetColumn(x => x.Number, nameof(ReportItem.GroupNumber)));
            query.AddColumn(OMModel.GetColumn(x => x.CalculationType_Code, nameof(ReportItem.ModelCalculationType)));
            query.AddColumn(OMModel.GetColumn(x => x.CalculationMethod_Code, nameof(ReportItem.ModelCalculationMethod)));
            query.AddColumn(OMModel.GetColumn(x => x.Name, nameof(ReportItem.ModelName)));

            var operations = new List<ReportItem>();
            var table = query.ExecuteQuery();
            foreach (DataRow row in table.Rows)
            {
                var objectPropertyType = (PropertyTypes) row[nameof(ReportItem.ObjectPropertyType)].ParseToLong();
                var cadastralNumber = row[nameof(ReportItem.CadastralNumber)].ParseToString();
                var groupName = row[nameof(ReportItem.GroupName)].ParseToString();
                var groupNumber = row[nameof(ReportItem.GroupNumber)].ParseToString();
                var modelCalculationType = (KoCalculationType)row[nameof(ReportItem.ModelCalculationType)].ParseToLong();
                var modelCalculationMethod = (KoCalculationMethod)row[nameof(ReportItem.ModelCalculationMethod)].ParseToLong();
                var modelName = row[nameof(ReportItem.ModelName)].ParseToString();

                operations.Add(new ReportItem
                {
                    ObjectPropertyType =  objectPropertyType,
                    CadastralNumber = cadastralNumber,
                    GroupName = groupName,
                    GroupNumber = groupNumber,
                    ModelCalculationType = modelCalculationType,
                    ModelCalculationMethod = modelCalculationMethod,
                    ModelName = modelName
                });
            }

            return operations;
        }

        private DataTable GetItemDataTable(List<ReportItem> operations)
        {
            var dataTable = new DataTable("ITEM");

            dataTable.Columns.Add("Number");
            dataTable.Columns.Add("ObjectPropertyType");
            dataTable.Columns.Add("CadastralNumber");
            dataTable.Columns.Add("GroupName");
            dataTable.Columns.Add("ModelingWay");
            dataTable.Columns.Add("ModelCalculationType");
            dataTable.Columns.Add("ModelCalculationMethod");
            dataTable.Columns.Add("ModelName");

            for (var i = 0; i < operations.Count; i++)
            {
                dataTable.Rows.Add(i + 1,
                    operations[i].ObjectPropertyType == PropertyTypes.None 
                        ? null 
                        : operations[i].ObjectPropertyType.GetEnumDescription(),
                    operations[i].CadastralNumber,
                    $"{operations[i].GroupNumber}. {operations[i].GroupName}",
                    operations[i].ModelingWay.GetEnumDescription(),
                    operations[i].ModelCalculationType == KoCalculationType.None 
                        ? null 
                        : operations[i].ModelCalculationType.GetEnumDescription(),
                    operations[i].ModelCalculationMethod == KoCalculationMethod.None 
                        ? null
                        : operations[i].ModelCalculationMethod.GetEnumDescription(),
                    operations[i].ModelName);
            }

            return dataTable;
        }

        #endregion

        #region Entities

        private class ReportItem
        {
            public PropertyTypes ObjectPropertyType { get; set; }
            public string CadastralNumber { get; set; }
            public string GroupName { get; set; }
            public string GroupNumber { get; set; }

            public KoModelingWay ModelingWay =>
                ModelCalculationMethod == KoCalculationMethod.IndividualCalculation
                    ? KoModelingWay.IndividualCalculation
                    : KoModelingWay.MassCalculation;

            public KoCalculationType ModelCalculationType { get; set; }
            public KoCalculationMethod ModelCalculationMethod { get; set; }
            public string ModelName { get; set; }
        }

        #endregion
    }
}
