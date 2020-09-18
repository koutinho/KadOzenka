using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using Core.Register.QuerySubsystem;
using Core.Register.RegisterEntities;
using Core.Shared.Extensions;
using KadOzenka.Dal.FastReports.StatisticalData.Common;
using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData;
using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData.Entities;
using ObjectModel.Directory;
using ObjectModel.KO;

namespace KadOzenka.Dal.FastReports.StatisticalData.PricingFactorsComposition
{
    public class ZuReport : StatisticalDataReport
    {
        protected override string TemplateName(NameValueCollection query)
        {
            return "PricingFactorsCompositionForZuReport";
        }

        protected override DataSet GetData(NameValueCollection query, HashSet<long> objectList = null)
        {
            var taskIds = GetTaskIdList(query)?.ToList();
            var tourId = GetTourId(query);

            var operations = GetOperations(tourId, taskIds);

            var dataSet = new DataSet();
            var itemTable = GetItemDataTable(operations);
            dataSet.Tables.Add(itemTable);

            return dataSet;
        }


        #region Support Methods

        private List<ReportItem> GetOperations(long tourId, List<long> taskIds)
        {
            var units = GetUnitsNew(taskIds);
            
            if (units == null || units.Count == 0)
                return new List<ReportItem>();

            var attributesDictionary = GetAttributesForReport(tourId);
            var gbuAttributes = GbuObjectService.GetAllAttributes(
                units.Select(x => x.ObjectId).Distinct().ToList(),
                attributesDictionary.Values.Select(x => (long)x.RegisterId).Distinct().ToList(),
                attributesDictionary.Values.Select(x => x.Id).Distinct().ToList(),
                DateTime.Now.GetEndOfTheDay(), isLight: true);

            var result = new List<ReportItem>();
            units.ToList().ForEach(unit =>
            {
                var item = new ReportItem
                {
                    UnitIfo = unit
                };

                SetAttributes(unit.ObjectId, gbuAttributes, attributesDictionary, item);

                result.Add(item);
            });

            return result;
        }

        protected List<UnitIfo> GetUnitsNew(List<long> taskIds)
        {
            var query = new QSQuery
            {
                MainRegisterID = OMUnit.GetRegisterId(),
                Condition = new QSConditionGroup
                {
                    Type = QSConditionGroupType.And,
                    Conditions = new List<QSCondition>
                    {
                        new QSConditionSimple(OMUnit.GetColumn(x => x.TaskId), QSConditionType.In, taskIds.Select(x => (double) x).ToList()),
                        new QSConditionSimple(OMUnit.GetColumn(x => x.PropertyType_Code), QSConditionType.Equal, (int)PropertyTypes.Stead),
                        new QSConditionSimple(OMUnit.GetColumn(x => x.ObjectId), QSConditionType.IsNotNull)
                    }
                },
                Joins = new List<QSJoin>
                {
                    new QSJoin
                    {
                        RegisterId = OMCostRosreestr.GetRegisterId(),
                        JoinCondition = new QSConditionSimple
                        {
                            ConditionType = QSConditionType.Equal,
                            LeftOperand = OMUnit.GetColumn(x => x.Id),
                            RightOperand = OMCostRosreestr.GetColumn(x => x.IdObject)
                        },
                        JoinType = QSJoinType.Left
                    }
                }
            };
            query.AddColumn(OMUnit.GetColumn(x => x.ObjectId, nameof(UnitIfo.ObjectId)));
            query.AddColumn(OMUnit.GetColumn(x => x.CadastralNumber, nameof(UnitIfo.CadastralNumber)));
            query.AddColumn(OMUnit.GetColumn(x => x.Square, nameof(UnitIfo.Square)));

            query.AddColumn(OMCostRosreestr.GetColumn(x => x.Costvalue, nameof(UnitIfo.CostValue)));
            query.AddColumn(OMCostRosreestr.GetColumn(x => x.Datevaluation, nameof(UnitIfo.DateValuation)));
            query.AddColumn(OMCostRosreestr.GetColumn(x => x.Dateentering, nameof(UnitIfo.DateEntering)));
            query.AddColumn(OMCostRosreestr.GetColumn(x => x.Dateapproval, nameof(UnitIfo.DateApproval)));
            query.AddColumn(OMCostRosreestr.GetColumn(x => x.Docnumber, nameof(UnitIfo.DocNumber)));
            query.AddColumn(OMCostRosreestr.GetColumn(x => x.Docdate, nameof(UnitIfo.DocDate)));
            query.AddColumn(OMCostRosreestr.GetColumn(x => x.Docname, nameof(UnitIfo.DocName)));
            query.AddColumn(OMCostRosreestr.GetColumn(x => x.Applicationdate, nameof(UnitIfo.ApplicationDate)));
            query.AddColumn(OMCostRosreestr.GetColumn(x => x.Revisalstatementdate, nameof(UnitIfo.RevisalStatementDate)));

            return query.ExecuteQuery<UnitIfo>();
        }

        private Dictionary<string, RegisterAttribute> GetAttributesForReport(long tourId)
        {
            var attributesDictionary = new Dictionary<string, RegisterAttribute>();
            attributesDictionary.Add(nameof(ReportItem.TypeOfUseByDocuments), RosreestrRegisterService.GetTypeOfUseByDocumentsAttribute());
            attributesDictionary.Add(nameof(ReportItem.TypeOfUseByClassifier), RosreestrRegisterService.GetTypeOfUseByClassifierAttribute());
            attributesDictionary.Add(nameof(ReportItem.FormationDate), RosreestrRegisterService.GetFormationDateAttribute());
            attributesDictionary.Add(nameof(ReportItem.ParcelCategory), RosreestrRegisterService.GetParcelCategoryAttribute());
            attributesDictionary.Add(nameof(ReportItem.Location), RosreestrRegisterService.GetLocationAttribute());
            attributesDictionary.Add(nameof(ReportItem.Address), RosreestrRegisterService.GetAddressAttribute());
            attributesDictionary.Add(nameof(ReportItem.ParcelName), RosreestrRegisterService.GetParcelNameAttribute());

            var generalAttributes = GetAttributesFromTourSettingsForReport(tourId);
            var result = attributesDictionary.Concat(generalAttributes).ToDictionary(x => x.Key, x => x.Value);

            return result;
        }

        private DataTable GetItemDataTable(List<ReportItem> operations)
        {
            var dataTable = new DataTable("Item");

            dataTable.Columns.Add("Number");

            dataTable.Columns.Add("CadastralNumber");

            dataTable.Columns.Add("TypeOfUseByDocuments");
            dataTable.Columns.Add("TypeOfUseByClassifier");
            dataTable.Columns.Add("FormationDate");
            dataTable.Columns.Add("ParcelCategory");
            dataTable.Columns.Add("Location");
            dataTable.Columns.Add("Address");
            dataTable.Columns.Add("ParcelName");

            dataTable.Columns.Add("Square");

            dataTable.Columns.Add("ObjectType");
            dataTable.Columns.Add("CadastralQuartal");

            dataTable.Columns.Add("CostValue");
            dataTable.Columns.Add("DateValuation");
            dataTable.Columns.Add("DateEntering");
            dataTable.Columns.Add("DateApproval");
            dataTable.Columns.Add("DocNumber");
            dataTable.Columns.Add("DocDate");
            dataTable.Columns.Add("DocName");
            dataTable.Columns.Add("ApplicationDate");
            dataTable.Columns.Add("RevisalStatementDate");

            for (var i = 0; i < operations.Count; i++)
            {
                dataTable.Rows.Add(i + 1,
                    operations[i].UnitIfo.CadastralNumber,
                    operations[i].TypeOfUseByDocuments,
                    operations[i].TypeOfUseByClassifier,
                    operations[i].FormationDate,
                    operations[i].ParcelCategory,
                    operations[i].Location,
                    operations[i].Address,
                    operations[i].ParcelName,
                    operations[i].UnitIfo.Square,
                    operations[i].ObjectType,
                    operations[i].CadastralQuartal,
                    operations[i].UnitIfo.CostValue?.ToString(DecimalFormat),
                    operations[i].UnitIfo.DateValuation?.ToString(DateFormat),
                    operations[i].UnitIfo.DateEntering?.ToString(DateFormat),
                    operations[i].UnitIfo.DateApproval?.ToString(DateFormat),
                    operations[i].UnitIfo.DocNumber,
                    operations[i].UnitIfo.DocDate?.ToString(DateFormat),
                    operations[i].UnitIfo.DocName,
                    operations[i].UnitIfo.ApplicationDate?.ToString(DateFormat),
                    operations[i].UnitIfo.RevisalStatementDate?.ToString(DateFormat));
            }

            return dataTable;
        }

        #endregion

        #region Entities

        private class ReportItem : InfoFromTourSettings
        {
            public UnitIfo UnitIfo { get; set; }

            //From Rosreestr
            public string TypeOfUseByDocuments { get; set; }
            public string TypeOfUseByClassifier { get; set; }
            public string FormationDate { get; set; }
            public string ParcelCategory { get; set; }
            public string Location { get; set; }
            public string Address { get; set; }
            public string ParcelName { get; set; }

            //From Tour Settings

            public ReportItem()
            {
                UnitIfo = new UnitIfo();
            }
        }

        public class UnitIfo
        {
            //From Unit
            public long Id { get; set; }
            public long ObjectId { get; set; }
            public string CadastralNumber { get; set; }
            public decimal? Square { get; set; }

            //From KO.CostRosreestr (KO_COST_ROSREESTR)
            /// <summary>
            /// Значение кадастровой стоимости
            /// </summary>
            public decimal? CostValue { get; set; }
            /// <summary>
            /// Дата определения кадастровой стоимости
            /// </summary>
            public DateTime? DateValuation { get; set; }
            /// <summary>
            /// Дата внесения сведений о кадастровой стоимости в ЕГРН
            /// </summary>
            public DateTime? DateEntering { get; set; }
            /// <summary>
            /// Дата утверждения кадастровой стоимости
            /// </summary>
            public DateTime? DateApproval { get; set; }
            /// <summary>
            /// Номер акта об утверждении кадастровой стоимости
            /// </summary>
            public string DocNumber { get; set; }
            /// <summary>
            /// Дата акта об утверждении кадастровой стоимости
            /// </summary>
            public DateTime? DocDate { get; set; }
            /// <summary>
            /// Наименование документа об утверждении кадастровой стоимости
            /// </summary>
            public string DocName { get; set; }
            /// <summary>
            /// Дата начала применения кадастровой стоимости
            /// </summary>
            public DateTime? ApplicationDate { get; set; }
            /// <summary>
            /// Дата подачи заявления о пересмотре кадастровой стоимости
            /// </summary>
            public DateTime? RevisalStatementDate { get; set; }
        }

        #endregion
    }
}
