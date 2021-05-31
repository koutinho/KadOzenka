using Core.Register;

namespace KadOzenka.Common.Tests.Consts
{
	public static class EgrnAttributes
	{
		public static EgrnAttributeForTest Address { get; }
		public static EgrnAttributeForTest CadastralNumber { get; }
		public static EgrnAttributeForTest Square { get; }

		static EgrnAttributes()
		{
			Address = new EgrnAttributeForTest(600, "Адрес", RegisterAttributeType.STRING);
			CadastralNumber = new EgrnAttributeForTest(1416, "Кадастровый номер", RegisterAttributeType.STRING);
			Square = new EgrnAttributeForTest(2, "Площадь", RegisterAttributeType.DECIMAL);
		}
	}

	
	public class EgrnAttributeForTest : AttributeInfoForTests
	{
		public long RegisterId => 2;
		public string TableName { get; }

		internal EgrnAttributeForTest(long attributeId, string name, RegisterAttributeType type)
			: base(attributeId, name, type)
		{
			TableName = $"gbu_source2_a_{attributeId}";
		}
	}
}
