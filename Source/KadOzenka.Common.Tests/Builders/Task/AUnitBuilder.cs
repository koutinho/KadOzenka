using System;
using Core.Shared.Extensions;
using ObjectModel.Directory;
using ObjectModel.Gbu;
using ObjectModel.KO;

namespace KadOzenka.Common.Tests.Builders.Task
{
	public abstract class AUnitBuilder
	{
		protected readonly OMUnit _unit;

		protected AUnitBuilder()
		{
			var status = KoUnitStatus.New;
			var statusResultCalc = KoStatusResultCalc.None;
			var statusRepeatCalc = KoStatusRepeatCalc.New;
			var propertyType = PropertyTypes.Building;
			_unit = new OMUnit
			{
				ObjectId = RandomGenerator.GenerateRandomInteger(),
				TourId = RandomGenerator.GenerateRandomInteger(),
				TaskId = RandomGenerator.GenerateRandomInteger(),
				GroupId = RandomGenerator.GenerateRandomInteger(),
				Status_Code = status,
				Status = status.GetEnumDescription(),
				CreationDate = DateTime.Now,
				CadastralCost = RandomGenerator.GenerateRandomDecimal(),
				Upks = RandomGenerator.GenerateRandomDecimal(),
				CadastralCostPre = RandomGenerator.GenerateRandomDecimal(),
				UpksPre = RandomGenerator.GenerateRandomDecimal(),
				StatusResultCalc_Code = statusResultCalc,
				StatusResultCalc = statusResultCalc.GetEnumDescription(),
				StatusRepeatCalc_Code = statusRepeatCalc,
				StatusRepeatCalc = statusRepeatCalc.GetEnumDescription(),
				ParentCalcType_Code = KoParentCalcType.None,
				Square = RandomGenerator.GenerateRandomDecimal(),
				CadastralNumber = RandomGenerator.GetRandomString(),
				CadastralBlock = RandomGenerator.GetRandomString(),
				PropertyType_Code = propertyType,
				PropertyType = propertyType.GetEnumDescription(),
				DegreeReadiness = RandomGenerator.GenerateRandomInteger(),
				ParentCalcNumber = RandomGenerator.GetRandomString(),
				UseAsPrototype = false,
				ResponseDocId = RandomGenerator.GenerateRandomInteger(),
				BuildingCadastralNumber = RandomGenerator.GetRandomString(),
				AssessmentDate = DateTime.Now
			};
		}

		protected AUnitBuilder(OMUnit unit)
		{
			_unit = unit.ShallowCopy();
		}


		public AUnitBuilder Task(OMTask task)
		{
			_unit.TaskId = task.Id;
			_unit.TourId = task.TourId;
			return this;
		}

		public AUnitBuilder Object(OMMainObject gbuObject)
		{
			_unit.ObjectId = gbuObject.Id;
			_unit.CadastralNumber = gbuObject.CadastralNumber;
			return this;
		}

		public AUnitBuilder Type(PropertyTypes type)
		{
			_unit.PropertyType_Code = type;
			_unit.PropertyType = type.GetEnumDescription();
			return this;
		}

		public AUnitBuilder CreationDate(DateTime date)
		{
			_unit.CreationDate = date;
			return this;
		}


		public abstract OMUnit Build();
		public abstract AUnitBuilder ShallowCopy();
	}
}
