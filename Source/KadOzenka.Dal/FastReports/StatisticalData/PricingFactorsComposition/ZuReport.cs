using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using Core.Register.RegisterEntities;
using Core.Shared.Extensions;
using KadOzenka.Dal.FastReports.StatisticalData.Common;
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
            var units = GetUnits(taskIds, PropertyTypes.Stead);
            ////для тестирования
            //units = new List<OMUnit>
            //{
            //    new OMUnit{CadastralNumber = "test 1", Id = 14974931, ObjectId = 11188991 },
            //    new OMUnit{CadastralNumber = "test 2", Id = 12402753, ObjectId = 11251387 }
            //};
            if (units == null || units.Count == 0)
                return new List<ReportItem>();

            var unitIds = units.Select(x => x.Id).Distinct();
            var unitCosts = OMCostRosreestr.Where(x => unitIds.Contains(x.IdObject)).SelectAll().Execute();

            var attributesDictionary = GetAttributesForReport(tourId);
            var gbuAttributes = GbuObjectService.GetAllAttributes(
                units.Select(x => x.ObjectId.GetValueOrDefault()).Distinct().ToList(),
                attributesDictionary.Values.Select(x => (long)x.RegisterId).Distinct().ToList(),
                attributesDictionary.Values.Select(x => x.Id).Distinct().ToList(),
                DateTime.Now.GetEndOfTheDay());

            var result = new List<ReportItem>();
            units.ToList().ForEach(unit =>
            {
                var rosreestrCost = unitCosts.FirstOrDefault(x => x.IdObject == unit.Id);
                var item = new ReportItem
                {
                    CadastralNumber = unit.CadastralNumber,
                    Square = unit.Square,
                    CostValue = rosreestrCost?.Costvalue,
                    DateValuation = rosreestrCost?.Datevaluation,
                    DateEntering = rosreestrCost?.Dateentering,
                    DateApproval = rosreestrCost?.Dateapproval,
                    DocNumber = rosreestrCost?.Docnumber,
                    DocDate = rosreestrCost?.Docdate,
                    DocName = rosreestrCost?.Docname,
                    ApplicationDate = rosreestrCost?.Applicationdate,
                    RevisalStatementDate = rosreestrCost?.Revisalstatementdate,
                };

                SetAttributes(unit.ObjectId, gbuAttributes, attributesDictionary, item);

                result.Add(item);
            });

            return result;
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
                    operations[i].CadastralNumber,
                    operations[i].TypeOfUseByDocuments,
                    operations[i].TypeOfUseByClassifier,
                    operations[i].FormationDate,
                    operations[i].ParcelCategory,
                    operations[i].Location,
                    operations[i].Address,
                    operations[i].ParcelName,
                    operations[i].Square,
                    operations[i].ObjectType,
                    operations[i].CadastralQuartal,
                    operations[i].CostValue?.ToString(DecimalFormat),
                    operations[i].DateValuation?.ToString(DateFormat),
                    operations[i].DateEntering?.ToString(DateFormat),
                    operations[i].DateApproval?.ToString(DateFormat),
                    operations[i].DocNumber,
                    operations[i].DocDate?.ToString(DateFormat),
                    operations[i].DocName,
                    operations[i].ApplicationDate?.ToString(DateFormat),
                    operations[i].RevisalStatementDate?.ToString(DateFormat));
            }

            return dataTable;
        }

        #endregion

        #region Entities

        private class ReportItem : InfoFromTourSettings
        {
            //From Unit
            public string CadastralNumber { get; set; }
            public decimal? Square { get; set; }

            //From Rosreestr
            public string TypeOfUseByDocuments { get; set; }
            public string TypeOfUseByClassifier { get; set; }
            public string FormationDate { get; set; }
            public string ParcelCategory { get; set; }
            public string Location { get; set; }
            public string Address { get; set; }
            public string ParcelName { get; set; }

            //From Tour Settings


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
