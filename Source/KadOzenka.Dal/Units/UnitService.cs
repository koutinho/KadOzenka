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
			var tourRegisterId = TourFactorService.GetTourRegister(unit.TourId.GetValueOrDefault(),
				unit.PropertyType_Code == PropertyTypes.Stead ? ObjectType.ZU : ObjectType.Oks)?.RegisterId;
			if (tourRegisterId == null)
				throw new Exception($"Не найден реестр факторов для тура с ИД {unit.TourId} для типа объекта {unit.PropertyType_Code.GetEnumDescription()}");

			var tourAttributes = RegisterAttributeService.GetActiveRegisterAttributes(tourRegisterId.Value, attributes);
			if (tourAttributes.IsEmpty())
				return new List<UnitFactor>();

			var query = GetUnitFactorsQuery(unit.Id, tourRegisterId.Value);
			foreach (var factor in tourAttributes)
			{
				if (factor.IsPrimaryKey != null && factor.IsPrimaryKey.Value)
					continue;

				query.AddColumn(factor.Id, factor.Id.ToString());
			}

			var results = new List<UnitFactor>();
			var table = query.ExecuteQuery();
			foreach (var factor in tourAttributes)
			{
				if (factor.IsPrimaryKey != null && factor.IsPrimaryKey.Value)
					continue;

				var attr = new UnitFactor(factor.Id);
				if (table.Rows.Count > 0)
				{
					attr.SetFactorValue(table.Rows[0][factor.Id.ToString()].ParseToStringNullable());
				}

				results.Add(attr);
			}

			return results;
		}


		#region Support Methods

		private QSQuery GetUnitFactorsQuery(long unitId, long tourRegisterId)
        {
	        var tourRegisterPrimaryKeyId = RegisterCache.RegisterAttributes.Values
		        .FirstOrDefault(x => x.RegisterId == tourRegisterId && x.IsPrimaryKey)?.Id;
	        
	        var qsConditionGroup = new QSConditionGroup(QSConditionGroupType.And);
	        qsConditionGroup.Add(new QSConditionSimple
	        {
		        ConditionType = QSConditionType.Equal,
		        LeftOperand = new QSColumnSimple(tourRegisterPrimaryKeyId.GetValueOrDefault()),
		        RightOperand = new QSColumnConstant(unitId)
	        });

	        var query = new QSQuery
	        {
		        MainRegisterID = (int)tourRegisterId,
		        Condition = qsConditionGroup
	        };

	        return query;
        }

		#endregion
	}
}
