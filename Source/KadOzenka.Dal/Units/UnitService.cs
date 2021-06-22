using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Register;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using KadOzenka.Dal.CommonFunctions;
using KadOzenka.Dal.Modeling;
using KadOzenka.Dal.Modeling.Repositories;
using KadOzenka.Dal.Registers;
using KadOzenka.Dal.Tours;
using KadOzenka.Dal.Units.Repositories;
using ObjectModel.Directory;
using ObjectModel.KO;
using KadOzenka.Dal.Oks;
using ObjectModel.Core.Register;

namespace KadOzenka.Dal.Units
{
	public class UnitService : IUnitService
	{
		private ITourFactorService TourFactorService { get; }
		private IRegisterAttributeService RegisterAttributeService { get; }
		private IModelingService ModelingService { get; }
		private IUnitRepository UnitRepository { get; }
		public IRegisterCacheWrapper RegisterCacheWrapper { get; }


		public UnitService(ITourFactorService tourFactorService = null, 
			IRegisterAttributeService registerAttributeService = null,
			IModelingService modelingService = null,
			IUnitRepository unitRepository = null, 
			IRegisterCacheWrapper registerCacheWrapper = null)
		{
			TourFactorService = tourFactorService ?? new TourFactorService();
			RegisterAttributeService = registerAttributeService ?? new RegisterAttributeService();
			ModelingService = modelingService ?? new ModelingService();
			UnitRepository = unitRepository ?? new UnitRepository();
			RegisterCacheWrapper = registerCacheWrapper ?? new RegisterCacheWrapper();
		}


		public List<UnitFactor> GetUnitModelFactors(OMUnit unit)
		{
			var factorsValues = new List<UnitFactor>();
			var model = ModelingService.GetActiveModelEntityByGroupId(unit.GroupId);
			if (model != null)
			{
				var modelFactorIds = OMModelFactor.Where(x => x.ModelId == model.Id && x.FactorId != null)
					.Select(x => x.FactorId)
					.Execute()
					.Select(x => x.FactorId.GetValueOrDefault()).ToList();
				if (!modelFactorIds.IsEmpty())
					factorsValues = GetUnitFactors(unit, modelFactorIds);
			}

			return factorsValues;
		}

		public List<UnitFactor> GetUnitGroupFactors(OMUnit unit)
		{
			var factorsValues = new List<UnitFactor>();
			var groupFactorIds = OMGroupFactor.Where(x => x.GroupId == unit.GroupId && x.FactorId != null)
				.Select(x => x.FactorId)
				.Execute()
				.Select(x => x.FactorId.GetValueOrDefault()).ToList();
			if (!groupFactorIds.IsEmpty())
				factorsValues = GetUnitFactors(unit, groupFactorIds);

			return factorsValues;
		}

		public List<UnitFactor> GetUnitFactors(OMUnit unit, List<long> attributes = null)
		{
			var tourRegister = TourFactorService.GetTourRegister(unit.TourId.GetValueOrDefault(),
				unit.PropertyType_Code == PropertyTypes.Stead ? ObjectType.ZU : ObjectType.Oks);
			if (tourRegister == null)
				throw new Exception(
					$"Не найден реестр факторов для тура с ИД {unit.TourId} для типа объекта {unit.PropertyType_Code.GetEnumDescription()}");

			var tourAttributes = RegisterAttributeService.GetActiveRegisterAttributes(tourRegister.RegisterId, attributes);
			if (tourAttributes.IsEmpty())
				return new List<UnitFactor>();

			var query = GetUnitFactorsQuery(unit.Id, tourRegister);
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

		private QSQuery GetUnitFactorsQuery(long unitId, OMRegister tourRegister)
        {
	        var tourRegisterPrimaryKeyId = RegisterCache.RegisterAttributes.Values
		        .FirstOrDefault(x => x.RegisterId == tourRegister.RegisterId && x.IsPrimaryKey)?.Id;
	        var qsConditionGroup = new QSConditionGroup(QSConditionGroupType.And);
	        qsConditionGroup.Add(new QSConditionSimple
	        {
		        ConditionType = QSConditionType.Equal,
		        LeftOperand = new QSColumnSimple(tourRegisterPrimaryKeyId.GetValueOrDefault()),
		        RightOperand = new QSColumnConstant(unitId)
	        });
	        var query = new QSQuery
	        {
		        MainRegisterID = (int)tourRegister.RegisterId,
		        Condition = qsConditionGroup
	        };

	        return query;
        }


		#endregion
	}
}
