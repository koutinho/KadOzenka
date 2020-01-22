using KadOzenka.Dal.Tours.Dto;

namespace KadOzenka.Web.Models.Tour
{
	public class GroupTreeModel
	{
		public long? Id { get; set; }
		public long? ParentId { get; set; }
		public string GroupName { get; set; }
		public long? TourId { get; set; }
        public string UrlForEdit { get; set; }


        public static GroupTreeModel ToModel(GroupTreeDto tree)
        {
            return new GroupTreeModel
            {
                Id = tree.Id,
                ParentId = tree.ParentId,
                GroupName = tree.GroupName,
                TourId = tree.TourId
            };
        }
    }
}
