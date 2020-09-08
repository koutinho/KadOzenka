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
    public class OksReport : StatisticalDataReport
    {
        protected override string TemplateName(NameValueCollection query)
        {
            return "PricingFactorsCompositionForOksReport";
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
            var units = GetNotSteadUnits(taskIds);
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

                item.ResultPurpose = GetPurpose(unit.PropertyType_Code, item);

                result.Add(item);
            });

            return result;
        }

        protected List<OMUnit> GetNotSteadUnits(List<long> taskIds)
        {
            return OMUnit.Where(x => taskIds.Contains((long)x.TaskId) &&
                                     x.PropertyType_Code != PropertyTypes.Stead &&
                                     x.ObjectId != null)
                .SelectAll()
                .Execute();
        }

        private string GetPurpose(PropertyTypes objectType, ReportItem item)
        {
            switch (objectType)
            {
                case PropertyTypes.Building:
                    return item.BuildingPurpose;
                case PropertyTypes.Pllacement:
                    return item.PlacementPurpose;
                case PropertyTypes.Construction:
                    return item.ConstructionPurpose;
            }

            return null;
        }

        private Dictionary<string, RegisterAttribute> GetAttributesForReport(long tourId)
        {
            var attributesDictionary = new Dictionary<string, RegisterAttribute>();
            attributesDictionary.Add(nameof(ReportItem.CommissioningYear), RosreestrRegisterService.GetRosreestrCommissioningYearAttribute());
            attributesDictionary.Add(nameof(ReportItem.BuildYear), RosreestrRegisterService.GetRosreestrBuildYearAttribute());
            attributesDictionary.Add(nameof(ReportItem.FormationDate), RosreestrRegisterService.GetRosreestrFormationDateAttribute());
            attributesDictionary.Add(nameof(ReportItem.UndergroundFloorsNumber), RosreestrRegisterService.GetRosreestrUndergroundFloorsNumberAttribute());
            attributesDictionary.Add(nameof(ReportItem.FloorsNumber), RosreestrRegisterService.GetRosreestrFloorsNumberAttribute());
            attributesDictionary.Add(nameof(ReportItem.WallMaterial), RosreestrRegisterService.GetRosreestrWallMaterialAttribute());
            attributesDictionary.Add(nameof(ReportItem.Location), RosreestrRegisterService.GetRosreestrLocationAttribute());
            attributesDictionary.Add(nameof(ReportItem.Address), RosreestrRegisterService.GetRosreestrAddressAttribute());
            attributesDictionary.Add(nameof(ReportItem.BuildingPurpose), RosreestrRegisterService.GetRosreestrBuildingPurposeAttribute());
            attributesDictionary.Add(nameof(ReportItem.PlacementPurpose), RosreestrRegisterService.GetRosreestrPlacementPurposeAttribute());
            attributesDictionary.Add(nameof(ReportItem.ConstructionPurpose), RosreestrRegisterService.GetRosreestrConstructionPurposeAttribute());
            attributesDictionary.Add(nameof(ReportItem.ObjectName), RosreestrRegisterService.GetRosreestrObjectNameAttribute());

            var generalAttributes = GetAttributesFromTourSettingsForReport(tourId);
            var result = attributesDictionary.Concat(generalAttributes).ToDictionary(x => x.Key, x => x.Value);

            return result;
        }

        private DataTable GetItemDataTable(List<ReportItem> operations)
        {
            var dataTable = new DataTable("Item");

            dataTable.Columns.Add("Number");

            dataTable.Columns.Add("CadastralNumber");

            dataTable.Columns.Add("CommissioningYear");
            dataTable.Columns.Add("BuildYear");
            dataTable.Columns.Add("FormationDate");
            dataTable.Columns.Add("UndergroundFloorsNumber");
            dataTable.Columns.Add("FloorsNumber");
            dataTable.Columns.Add("WallMaterial");
            dataTable.Columns.Add("Location");
            dataTable.Columns.Add("Address");
            dataTable.Columns.Add("Purpose");
            dataTable.Columns.Add("ObjectName");

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
                    operations[i].CommissioningYear,
                    operations[i].BuildYear,
                    operations[i].FormationDate,
                    operations[i].UndergroundFloorsNumber,
                    operations[i].FloorsNumber,
                    operations[i].WallMaterial,
                    operations[i].Location,
                    operations[i].Address,
                    operations[i].ResultPurpose,
                    operations[i].ObjectName,
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
            public string CommissioningYear { get; set; }
            public string BuildYear { get; set; }
            public string FormationDate { get; set; }
            public string UndergroundFloorsNumber { get; set; }
            public string FloorsNumber { get; set; }
            public string WallMaterial { get; set; }
            public string Location { get; set; }
            public string Address { get; set; }
            public string BuildingPurpose { get; set; }
            public string PlacementPurpose { get; set; }
            public string ConstructionPurpose { get; set; }
            public string ResultPurpose { get; set; }
            public string ObjectName { get; set; }

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
