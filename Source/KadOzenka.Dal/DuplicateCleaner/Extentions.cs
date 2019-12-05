using System;
using ObjectModel.Directory;
using ObjectModel.Market;

namespace KadOzenka.Dal.DuplicateCleaner
{
	public static class Extensions
	{
		private sealed class OMCoreObjectWrapper : IMarketObject
		{
			private readonly OMCoreObject _omCoreObject;

			internal OMCoreObjectWrapper(OMCoreObject omCoreObject)
			{
				_omCoreObject = omCoreObject;
			}

			public string CadastralNumber
			{
				get => _omCoreObject.CadastralNumber;
				set => _omCoreObject.CadastralNumber = value;
			}
			public DealType DealType_Code
			{
				get => _omCoreObject.DealType_Code;
				set => _omCoreObject.DealType_Code = value;
			}
			public PropertyTypes PropertyType_Code
			{
				get => _omCoreObject.PropertyType_Code;
				set => _omCoreObject.PropertyType_Code = value;
			}
			public string Subcategory
			{
				get => _omCoreObject.Subcategory;
				set => _omCoreObject.Subcategory = value;
			}
			public decimal? Area
			{
				get => _omCoreObject.Area;
				set => _omCoreObject.Area = value;
			}
			public long? Price
			{
				get => _omCoreObject.Price;
				set => _omCoreObject.Price = value;
			}
			public DateTime? ParserTime
			{
				get => _omCoreObject.ParserTime;
				set => _omCoreObject.ParserTime = value;
			}
			public ProcessStep ProcessType_Code
			{
				get => _omCoreObject.ProcessType_Code;
				set => _omCoreObject.ProcessType_Code = value;
			}
			public ExclusionStatus ExclusionStatus_Code
			{
				get => _omCoreObject.ExclusionStatus_Code;
				set => _omCoreObject.ExclusionStatus_Code = value;
			}

			public int Save()
			{
				return _omCoreObject.Save();
			}
		}

		private sealed class OMCoreObjectTestWrapper : IMarketObject
		{
			private readonly OMCoreObjectTest _omCoreObjectTest;

			internal OMCoreObjectTestWrapper(OMCoreObjectTest omCoreObjectTestTest)
			{
				_omCoreObjectTest = omCoreObjectTestTest;
			}

			public string CadastralNumber
			{
				get => _omCoreObjectTest.CadastralNumber;
				set => _omCoreObjectTest.CadastralNumber = value;
			}
			public DealType DealType_Code
			{
				get => _omCoreObjectTest.DealType_Code;
				set => _omCoreObjectTest.DealType_Code = value;
			}
			public PropertyTypes PropertyType_Code
			{
				get => _omCoreObjectTest.PropertyType_Code;
				set => _omCoreObjectTest.PropertyType_Code = value;
			}
			public string Subcategory
			{
				get => _omCoreObjectTest.Subcategory;
				set => _omCoreObjectTest.Subcategory = value;
			}
			public decimal? Area
			{
				get => _omCoreObjectTest.Area;
				set => _omCoreObjectTest.Area = value;
			}
			public long? Price
			{
				get => _omCoreObjectTest.Price;
				set => _omCoreObjectTest.Price = value;
			}
			public DateTime? ParserTime
			{
				get => _omCoreObjectTest.ParserTime;
				set => _omCoreObjectTest.ParserTime = value;
			}
			public ProcessStep ProcessType_Code
			{
				get => _omCoreObjectTest.ProcessType_Code;
				set => _omCoreObjectTest.ProcessType_Code = value;
			}
			public ExclusionStatus ExclusionStatus_Code
			{
				get => _omCoreObjectTest.ExclusionStatus_Code;
				set => _omCoreObjectTest.ExclusionStatus_Code = value;
			}

			public int Save()
			{
				return _omCoreObjectTest.Save();
			}
		}

		public static IMarketObject AsIMarketObject(this OMCoreObject omCoreObject)
		{
			return new OMCoreObjectWrapper(omCoreObject);
		}

		public static IMarketObject AsIMarketObject(this OMCoreObjectTest omCoreObjectTest)
		{
			return new OMCoreObjectTestWrapper(omCoreObjectTest);
		}
	}
}
