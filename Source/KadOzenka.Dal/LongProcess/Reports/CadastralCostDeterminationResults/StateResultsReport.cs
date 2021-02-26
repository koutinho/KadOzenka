using System.Collections.Generic;
using System.Linq;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.LongProcess.Reports.CadastralCostDeterminationResults.Entities;
using KadOzenka.Dal.LongProcess.Reports.Entities;
using ObjectModel.KO;

namespace KadOzenka.Dal.LongProcess.Reports.CadastralCostDeterminationResults
{
    public class StateResultsReport : ICadastralCostDeterminationResultsReport
    {
	    public string ReportName => "Результаты государственной кадастровой оценки объектов недвижимости";


	    public List<long?> GetAvailableGroupIds()
        {
            return OMGroup
	            .Where(x => !x.GroupName.ToLower()
		            .Contains(CadastralCostDeterminationResultsBaseReportLongProcess.IndividuallyResultsGroupNamePhrase)).Execute()
	            .Select(x => (long?) x.Id).ToList();
        }

	    public List<object> GenerateReportReportRow(int index, ReportItem item)
	    {
		    return new List<object>
		    {
			    (index + 1).ToString(), item.CadastralDistrict, item.CadastralNumber, 
			    item.Type, item.Square, item.Upks, item.Cost
		    };
	    }

	    public List<Column> GenerateReportHeaders()
	    {
		    var columns = new List<Column>
		    {
			    new Column {Header = "№ п/п", Width = 4},
			    new Column {Header = "Кадастровый район", Width = 3},
			    new Column {Header = "Кадастровый номер объекта недвижимости", Width = 6},
			    new Column {Header = "Вид объекта недвижимости", Width = 5},
			    new Column {Header = "Общая площадь объекта недвижимости, кв.м.", Width = 4},
			    new Column {Header = "УПКС объекта недвижимости, руб./кв.м.", Width = 4},
			    new Column {Header = "Кадастровая стоимость объекта недвижимости, руб.", Width = 4}
			};

		    var counter = 0;
		    columns.ForEach(x => x.Index = counter++);

			return columns;
	    }
    }
}
