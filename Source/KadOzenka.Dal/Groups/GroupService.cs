using System;
using System.Collections.Generic;
using System.Data;
using System.Transactions;
using Core.Register.QuerySubsystem;
using KadOzenka.Dal.Groups.Dto;
using KadOzenka.Dal.Groups.Dto.Consts;
using KadOzenka.Dal.Tours;
using ObjectModel.Directory;
using ObjectModel.KO;

namespace KadOzenka.Dal.Groups
{
    public class GroupService
    {
        private TourService TourService { get; set; }

        public GroupService()
        {
            TourService = new TourService();
        }


        public GroupDto GetGroupById(long? groupId)
        {
            var group = GetGroupByIdInternal(groupId);
            
			return new GroupDto
            {
                Id = group.Id,
                Name = group.GroupName,
				ParentGroupId = group.ParentId,
				GroupAlgorithmCode = group.GroupAlgoritm_Code,
				GroupingAlgorithmId = (long)group.GroupAlgoritm_Code
			};
        }

        public List<GroupTreeDto> GetGroups(long? mainParentId = null)
        {
            var query = new QSQuery
            {
                MainRegisterID = OMGroup.GetRegisterId(),
                Joins = new List<QSJoin>
                {
                    new QSJoin
                    {
                        RegisterId = OMTourGroup.GetRegisterId(),
                        JoinCondition = new QSConditionSimple
                        {
                            ConditionType = QSConditionType.Equal,
                            LeftOperand = OMGroup.GetColumn(x => x.Id),
                            RightOperand = OMTourGroup.GetColumn(x => x.GroupId)
                        },
                        JoinType = QSJoinType.Inner
                    }
                }
            };

            query.AddColumn(OMGroup.GetColumn(x => x.ParentId, "ParentId"));
            query.AddColumn(OMGroup.GetColumn(x => x.GroupAlgoritm_Code, "GroupAlgoritm"));
            query.AddColumn(OMGroup.GetColumn(x => x.GroupName, "GroupName"));
            query.AddColumn(OMTourGroup.GetColumn(x => x.TourId, "TourId"));

            var table = query.ExecuteQuery();
            var groups = new List<GroupTreeDto>();

            var oks = new GroupTreeDto
            {
                Id = (long)KoGroupAlgoritm.MainOKS,
                ParentId = mainParentId,
                GroupName = "Основная группа ОКС",
                GroupType = GroupType.Main
            };
            groups.Add(oks);

            var parcel = new GroupTreeDto
            {
                Id = (long)KoGroupAlgoritm.MainParcel,
                ParentId = mainParentId,
                GroupName = "Основная группа Участки",
                GroupType = GroupType.Main
            };
            groups.Add(parcel);

            foreach (DataRow row in table.Rows)
            {
                var str = new GroupTreeDto();

                str.Id = long.Parse(row["Id"].ToString());

                long? parent = long.Parse(row["ParentId"].ToString());
                var groupAlgorithm = long.Parse(row["GroupAlgoritm"].ToString());
                if (parent == -1)
                {
                    if (groupAlgorithm == (long)KoGroupAlgoritm.MainOKS)
                    {
                        str.ParentId = oks.Id;
                    }
                    else if (groupAlgorithm == (long)KoGroupAlgoritm.MainParcel)
                    {
                        str.ParentId = parcel.Id;
                    }

                    str.GroupType = GroupType.Group;
                }
                else
                {
                    str.ParentId = parent;
                    str.GroupType = GroupType.SubGroup;
                }

                str.GroupName = row["GroupName"].ToString();
                str.TourId = long.Parse(row["TourId"].ToString());

                groups.Add(str);
            }

            return groups;
        }

        public int AddGroup(GroupDto groupDto)
        {
            var tour = TourService.GetTourById(groupDto.RatingTourId);

            int groupId;
			var group = new OMGroup();
            var tourGroup = new OMTourGroup();
            using (var ts = new TransactionScope())
			{
				group.GroupName = groupDto.Name;
				group.ParentId = groupDto.ParentGroupId ?? -1;
				group.GroupAlgoritm_Code = (KoGroupAlgoritm)groupDto.GroupingAlgorithmId;
				groupId = group.Save();

				tourGroup.GroupId = group.Id;
				tourGroup.TourId = tour.Id;
				tourGroup.Save();

				ts.Complete();
			}

			return groupId;
		}

        public int UpdateGroup(GroupDto groupDto)
        {
	        var group = GetGroupByIdInternal(groupDto.Id);

            int groupId;
			using (var ts = new TransactionScope())
			{
				group.GroupName = groupDto.Name;
				group.ParentId = groupDto.ParentGroupId ?? -1;
				group.GroupAlgoritm_Code = (KoGroupAlgoritm)groupDto.GroupingAlgorithmId;
				groupId = group.Save();

				ts.Complete();
			}

			return groupId;
		}


        #region Support Methods


        private OMGroup GetGroupByIdInternal(long? groupId)
        {
            if (groupId == null)
                throw new Exception("Не передан идентификатор Группы для поиска");

            var group = OMGroup.Where(x => x.Id == groupId).SelectAll().ExecuteFirstOrDefault();
            if (group == null)
                throw new Exception($"Не найдена Группа с id='{groupId}'");

            return group;
        }

        #endregion
    }
}
