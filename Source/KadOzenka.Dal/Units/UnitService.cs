using System;
using System.Collections.Generic;
using System.Linq;
using Core.Register;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using KadOzenka.Dal.CommonFunctions;
using KadOzenka.Dal.Groups;
using KadOzenka.Dal.Modeling;
using KadOzenka.Dal.Registers;
using KadOzenka.Dal.Tours;
using KadOzenka.Dal.Units.Repositories;
using ObjectModel.Directory;
using ObjectModel.KO;
using KadOzenka.Dal.Oks;

namespace KadOzenka.Dal.Units
{
	public class UnitService : IUnitService
	{
		//TODO подумать - как убрать зависимости от сервисов, но без копи-пасты
		private ITourFactorService TourFactorService { get; }
		private IRegisterAttributeService RegisterAttributeService { get; }
		private IModelingService ModelingService { get; }
		private IModelFactorsService ModelFactorsService { get; }
		private IGroupFactorService GroupFactorService { get; }
		private IUnitRepository UnitRepository { get; }
		public IRegisterCacheWrapper RegisterCacheWrapper { get; }


		public UnitService(ITourFactorService tourFactorService = null, 
			IRegisterAttributeService registerAttributeService = null,
			IModelingService modelingService = null,
			IModelFactorsService modelFactorsService = null,
			IGroupFactorService groupFactorService = null,
			IUnitRepository unitRepository = null, 
			IRegisterCacheWrapper registerCacheWrapper = null)
		{
			TourFactorService = tourFactorService ?? new TourFactorService();
			RegisterAttributeService = registerAttributeService ?? new RegisterAttributeService();
			ModelingService = modelingService ?? new ModelingService();
			ModelFactorsService = modelFactorsService ?? new ModelFactorsService();
			GroupFactorService = groupFactorService ?? new GroupFactorService();
			UnitRepository = unitRepository ?? new UnitRepository();
			RegisterCacheWrapper = registerCacheWrapper ?? new RegisterCacheWrapper();
		}


		public List<UnitFactor> GetUnitModelFactors(OMUnit unit)
		{
			var factorsValues = new List<UnitFactor>();
			var model = ModelingService.GetActiveModelEntityByGroupId(unit.GroupId);
			if (model != null)
			{
				var modelFactorIds = ModelFactorsService.GetGeneralModelAttributes(model.Id).Select(x => x.AttributeId).ToList();
				if (!modelFactorIds.IsEmpty())
					factorsValues = GetUnitFactors(unit, modelFactorIds);
			}

			return factorsValues;
		}

		public List<UnitFactor> GetUnitGroupFactors(OMUnit unit)
		{
			var factorsValues = new List<UnitFactor>();
			var groupFactorIds = GroupFactorService.GetGroupFactors(unit.GroupId).Select(x => x.FactorId).ToList();
			if (!groupFactorIds.IsEmpty())
				factorsValues = GetUnitFactors(unit, groupFactorIds);

			return factorsValues;
		}

		public List<UnitFactor> GetUnitFactors(OMUnit unit, List<long> attributes = null)
		{
			var isParcel = unit.PropertyType_Code == PropertyTypes.Stead;
			var tourAttributesInfo = GetTourRegisterInfo(unit.TourId, isParcel, attributes);

			var unitFactors = GetUnitFactors(unit.Id, tourAttributesInfo.RegisterId, tourAttributesInfo.AttributeIds);

			if (!unitFactors.TryGetValue(unit.Id, out var downloadedFactors))
			{
				return tourAttributesInfo.AttributeIds.Select(x => new UnitFactor(x)).ToList();
			}

			return downloadedFactors;
		}

		public Dictionary<long, List<UnitFactor>> GetUnitsFactors(List<long> unitIds, long tourId, bool isParcel, List<long> attributes = null)
		{
			var tourAttributesInfo = GetTourRegisterInfo(tourId, isParcel, attributes);

			return GetUnitFactors(unitIds, tourAttributesInfo.RegisterId, tourAttributesInfo.AttributeIds);
		}


		#region Support Methods

		private TourInfo GetTourRegisterInfo(long? tourId, bool isParcel, List<long> attributes = null)
		{
			var type = isParcel ? ObjectType.ZU : ObjectType.Oks;
			var tourRegisterId = TourFactorService.GetTourRegister(tourId.GetValueOrDefault(), type)?.RegisterId;
			if (tourRegisterId == null)
				throw new Exception($"Не найден реестр факторов для тура с ИД {tourId} для типа объекта {type.GetEnumDescription()}");

			var tourAttributes = RegisterAttributeService.GetActiveRegisterAttributes(tourRegisterId.Value, attributes)
				.Where(x => !x.IsPrimaryKey.GetValueOrDefault())
				.Select(x => x.Id)
				.ToList();

			return new TourInfo(tourRegisterId.Value, tourAttributes);
		}

		private Dictionary<long, List<UnitFactor>> GetUnitFactors(long unitId, long tourRegisterId, List<long> tourAttributeIds)
		{
			var query = GetUnitFactorsQuery(unitId, tourRegisterId);

			return GetUnitFactors(query, tourAttributeIds);
		}

		private Dictionary<long, List<UnitFactor>> GetUnitFactors(List<long> unitIds, long tourRegisterId, List<long> tourAttributeIds)
		{
			var query = GetUnitFactorsQuery(unitIds, tourRegisterId);

			return GetUnitFactors(query, tourAttributeIds);
		}

		private Dictionary<long, List<UnitFactor>> GetUnitFactors(QSQuery query, List<long> tourAttributeIds)
		{
			foreach (var factorId in tourAttributeIds)
			{
				query.AddColumn(factorId, factorId.ToString());
			}

			var result = new Dictionary<long, List<UnitFactor>>();
			var table = query.ExecuteQuery();
			for (var i = 0; i < table.Rows.Count; i++)
			{
				var row = table.Rows[i];
				var unitId = row["id"].ParseToLong();

				var currentUnitFactors = new List<UnitFactor>();
				tourAttributeIds.ForEach(factorId =>
				{
					var attr = new UnitFactor(factorId);

					var value = row[factorId.ToString()].ParseToStringNullable();
					attr.SetFactorValue(value);

					currentUnitFactors.Add(attr);
				});

				result.Add(unitId, currentUnitFactors);
			}

			return result;
		}

		private QSQuery GetUnitFactorsQuery(List<long> unitIds, long tourRegisterId)
		{
			return GetUnitFactorsQuery(tourRegisterId, new QSColumnConstant(unitIds), QSConditionType.In);
		}

		private QSQuery GetUnitFactorsQuery(long unitId, long tourRegisterId)
        {
	        return GetUnitFactorsQuery(tourRegisterId, new QSColumnConstant(unitId), QSConditionType.Equal);
		}

		private QSQuery GetUnitFactorsQuery(long tourRegisterId, QSColumnConstant rightOperand,
			QSConditionType conditionType)
		{
			var tourRegisterPrimaryKeyId = RegisterCache.RegisterAttributes.Values
				.FirstOrDefault(x => x.RegisterId == tourRegisterId && x.IsPrimaryKey)?.Id;

			var qsConditionGroup = new QSConditionGroup(QSConditionGroupType.And);
			qsConditionGroup.Add(new QSConditionSimple
			{
				ConditionType = conditionType,
				LeftOperand = new QSColumnSimple(tourRegisterPrimaryKeyId.GetValueOrDefault()),
				RightOperand = rightOperand
			});

			var query = new QSQuery
			{
				MainRegisterID = (int) tourRegisterId,
				Condition = qsConditionGroup
			};

			return query;
		}

		#endregion


		#region Entities

		private class TourInfo
		{
			public long RegisterId { get; set; }
			public List<long> AttributeIds { get; set; }

			public TourInfo(long tourRegisterId, List<long> tourAttributes)
			{
				RegisterId = tourRegisterId;
				AttributeIds = tourAttributes;
			}
		}

		#endregion
	}
}
