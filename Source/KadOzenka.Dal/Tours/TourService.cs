using System;
using System.Collections.Generic;
using System.Data;
using Core.Register.QuerySubsystem;
using KadOzenka.Dal.Tours.Dto;
using ObjectModel.Directory;
using ObjectModel.KO;
namespace KadOzenka.Dal.Tours
{
    public class TourService
    {
        #region Tours

        public TourDto GetTourById(long? tourId)
        {
            if(tourId == null)
                throw new Exception("Не передан идентификатор Тура для поиска");

            var tour = OMTour.Where(x => x.Id == tourId).SelectAll().ExecuteFirstOrDefault();
            if(tour == null)
                throw new Exception($"Не найден Тур с id='{tourId}'");

            return new TourDto
            {
                Id = tour.Id,
                Year = tour.Year
            };
        }

        #endregion


        #region Groups

        public GroupDto GetGroupById(long? groupId)
        {
            if (groupId == null)
                throw new Exception("Не передан идентификатор Группы для поиска");

            var group = OMGroup.Where(x => x.Id == groupId).SelectAll().ExecuteFirstOrDefault();
            if (group == null)
                throw new Exception($"Не найдена Группа с id='{groupId}'");

            return new GroupDto
            {
                Id = group.Id,
                Name = group.GroupName
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
                GroupName = "Основная группа ОКС"
            };
            groups.Add(oks);

            var parcel = new GroupTreeDto
            {
                Id = (long)KoGroupAlgoritm.MainParcel,
                ParentId = mainParentId,
                GroupName = "Основная группа Участки"
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
                }
                else
                {
                    str.ParentId = parent;
                }

                str.GroupName = row["GroupName"].ToString();
                str.TourId = long.Parse(row["TourId"].ToString());

                groups.Add(str);
            }

            return groups;
        }

        #endregion
    }
}
