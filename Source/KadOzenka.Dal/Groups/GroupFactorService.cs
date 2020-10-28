using System;
using System.Collections.Generic;
using System.Linq;
using Core.Register.QuerySubsystem;
using KadOzenka.Dal.Groups.Dto;
using ObjectModel.Core.Register;
using ObjectModel.KO;

namespace KadOzenka.Dal.Groups
{
	public class GroupFactorService
	{
		public List<GroupFactorDto> GetGroupFactors(long groupId)
		{
			var query = GetQueryForGroupFactor(new List<QSCondition> { new QSConditionSimple(OMGroupFactor.GetColumn(x => x.GroupId), QSConditionType.Equal, groupId) });
			return query.ExecuteQuery<GroupFactorDto>();
		}

		public GroupFactorDto GetGroupFactor(long id)
		{
			var query = GetQueryForGroupFactor(new List<QSCondition> { new QSConditionSimple(OMGroupFactor.GetColumn(x => x.Id), QSConditionType.Equal, id) });
			var result = query.ExecuteQuery<GroupFactorDto>();

			return result.First();
		}

		public long CreateGroupFactor(GroupFactorDto dto)
		{
			if (!OMGroup.Where(x => x.Id == dto.GroupId).ExecuteExists())
			{
				throw new Exception($"Не найден группа с ИД {dto.GroupId}");
			}
			if (!OMAttribute.Where(x => x.Id == dto.FactorId).ExecuteExists())
			{
				throw new Exception($"Не найден фактор с ИД {dto.FactorId}");
			}

			var entity = new OMGroupFactor
			{
				GroupId = dto.GroupId,
				FactorId = dto.FactorId,
				SignMarket = dto.SignMarket
			};

			return entity.Save();
		}

		public void UpdateGroupFactor(GroupFactorDto dto)
		{
			var entity = OMGroupFactor.Where(x => x.Id == dto.Id).SelectAll().ExecuteFirstOrDefault();
			if (entity == null)
			{
				throw new Exception($"Не найден фактор группы с ИД {dto.Id}");
			}

			entity.FactorId = dto.FactorId;
			entity.SignMarket = dto.SignMarket;
			entity.Save();
		}

		public void DeleteGroupFactor(long id)
		{
			var entity = OMGroupFactor.Where(x => x.Id == id).ExecuteFirstOrDefault();
			if(entity == null)
			{
				throw new Exception($"Не найден фактор группы с ИД {id}");
			}

			entity.Destroy();
		}

		private QSQuery GetQueryForGroupFactor(List<QSCondition> conditions = null)
		{
			var query = new QSQuery
			{
				MainRegisterID = OMGroupFactor.GetRegisterId(),
				Condition = new QSConditionGroup
				{
					Type = QSConditionGroupType.And,
					Conditions = conditions
				},
				Joins = new List<QSJoin>
				{
					new QSJoin
					{
						RegisterId = OMAttribute.GetRegisterId(),
						JoinCondition = new QSConditionSimple
						{
							ConditionType = QSConditionType.Equal,
							LeftOperand = OMGroupFactor.GetColumn(x => x.FactorId),
							RightOperand = OMAttribute.GetColumn(x => x.Id)
						},
						JoinType = QSJoinType.Inner
					}
				},
				OrderBy = new List<QSOrder>
				{
					new QSOrder
					{
						Column = OMAttribute.GetColumn(x => x.Name),
						Order = QSOrderType.ASC
					}
				}
			};
			query.AddColumn(OMGroupFactor.GetColumn(x => x.GroupId, nameof(GroupFactorDto.GroupId)));
			query.AddColumn(OMGroupFactor.GetColumn(x => x.FactorId, nameof(GroupFactorDto.FactorId)));
			query.AddColumn(OMAttribute.GetColumn(x => x.Name, nameof(GroupFactorDto.FactorName)));
			query.AddColumn(OMGroupFactor.GetColumn(x => x.SignMarket, nameof(GroupFactorDto.SignMarket)));

			return query;
		}
	}
}
