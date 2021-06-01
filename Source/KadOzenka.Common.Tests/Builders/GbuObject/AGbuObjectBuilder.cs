using Core.Shared.Extensions;
using ObjectModel.Directory;
using ObjectModel.Gbu;

namespace KadOzenka.Common.Tests.Builders.GbuObject
{
	public abstract class AGbuObjectBuilder
	{
		protected readonly OMMainObject _gbuObject;

		protected AGbuObjectBuilder()
		{
			var type = PropertyTypes.Building;
			_gbuObject = new OMMainObject
			{
				CadastralNumber = RandomGenerator.GetRandomString(),
				ObjectType_Code = type,
				ObjectType = type.GetEnumDescription(),
				IsActive = true,
				KadastrKvartal = RandomGenerator.GetRandomString()
			};
		}

		protected AGbuObjectBuilder(OMMainObject gbuObject)
		{
			_gbuObject = gbuObject.ShallowCopy();
		}



		public AGbuObjectBuilder Type(PropertyTypes type)
		{
			_gbuObject.ObjectType_Code = type;
			_gbuObject.ObjectType = type.GetEnumDescription();
			return this;
		}


		public abstract OMMainObject Build();
		public abstract AGbuObjectBuilder ShallowCopy();
	}
}
