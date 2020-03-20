using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using Core.UI.Registers.Reports.Model;
using Platform.Reports;
using EP.Morph;
using ObjectModel.Directory;
using ObjectModel.Market;

namespace KadOzenka.Dal.FastReports
{
	public class CorrectionByDateReport : FastReportBase
	{
        static CorrectionByDateReport()
		{
			Morphology.Initialize(MorphLang.RU);
		}

        protected override string TemplateName(NameValueCollection query)
        {
            return nameof(CorrectionByDateReport);
        }

        public override string GetTitle(long? objectId)
        {
            return ReportType.Title;
        }

        public override void InitializeFilterValues(long objId, string senderName, bool initialisation, List<FilterValue> filterValues)
		{
            if (!initialisation)
                return;

            var quartalFilterValue = filterValues.FirstOrDefault(f => f.ParamName == "Quartal");
            if (quartalFilterValue == null)
                return;

            quartalFilterValue.ReportParameters = new List<ReportParameter>();
            var quartals = OMQuartalDictionary.Where(x => true).SelectAll().Execute();
            quartalFilterValue.ReportParameters.AddRange(quartals.Select(x => new ReportParameter { Value = x.CadastralQuartal, Key = x.CadastralQuartal }));
        }

		protected override DataSet GetData(NameValueCollection query, HashSet<long> objectList = null)
		{
            //var dateFrom = GetQueryParam<DateTime>("DateFrom", query);

            var operations = GetOperations();

            var dataSet = new DataSet();
            dataSet.Tables.Add(GetItemDataTable(operations));

            return dataSet;
        }

        
        #region Support Methods

        private DataTable GetItemDataTable(List<CorrectionByDateItem> operations)
        {
            var dataTable = new DataTable("ITEM");

            dataTable.Columns.Add("CadastrNumber");
            
            foreach (var operation in operations)
            {
                dataTable.Rows.Add(operation.CadastralNumber);
            }

            return dataTable;
        }

        private List<CorrectionByDateItem> GetOperations()
        {
            var marketObjects = OMCoreObject
                .Where(x => x.DealType_Code == DealType.SaleSuggestion || x.DealType_Code == DealType.SaleDeal)
                .SetPackageSize(10).SetPackageIndex(0)
                .SelectAll().Execute();

            var list = new List<CorrectionByDateItem>();
            marketObjects.ForEach(x =>
            {
                list.Add(new CorrectionByDateItem
                {
                    CadastralNumber = x.CadastralNumber,
                    Address = x.Address
                });
            });
            

            return list;
        }

        private class CorrectionByDateItem
        {
            public string CadastralNumber { get; set; }
            public string Address { get; set; }
            public decimal? Segment { get; set; }
            public decimal? Price { get; set; }
            public decimal? PriceAfterCorrectionByDate { get; set; }
            public decimal? Type { get; set; }
            public decimal? LastUpdateDate { get; set; }
            public decimal? ParserDate { get; set; }
            public string Status { get; set; }
        }

        #endregion
    }
}
