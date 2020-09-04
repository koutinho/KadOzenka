using System.Linq;

namespace KadOzenka.Dal.Groups.Dto
{
    public class GroupCalculationSettingsDto
    {
	    public long GroupId { get; set; }
	    public string GroupNumber { get; set; }

	    public int? GroupNumberInt
	    {
		    get
		    {
			    var subGroupNumberStr = GroupNumber?.Split('.')?.ElementAtOrDefault(1);
			    if (int.TryParse(subGroupNumberStr, out var result))
			    {
				    return result;
			    }
			    return null;
			}
	    }

        public int? ParentGroupNumberInt
        {
	        get
	        {
		        var groupNumberStr = GroupNumber?.Split('.')?.ElementAtOrDefault(0);
		        if (int.TryParse(groupNumberStr, out var result))
		        {
			        return result;
		        }
		        return null;
	        }
        }

		public long Id { get; set; }
        public string GroupName { get; set; }
        public int Priority { get; set; }
        public bool Stage1 { get; set; }
        public bool Stage2 { get; set; }
        public bool Stage3 { get; set; }
    }
}
