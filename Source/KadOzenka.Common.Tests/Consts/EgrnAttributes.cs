using Core.Register;

namespace KadOzenka.Common.Tests.Consts
{
	public static class EgrnAttributes
	{
		public static AttributeInfoForTests Address { get; }
		public static AttributeInfoForTests CadastralNumber { get; }
		public static AttributeInfoForTests Square { get; }

		static EgrnAttributes()
		{
			Address = new AttributeInfoForTests(600, "Адрес", RegisterAttributeType.STRING);
			CadastralNumber = new AttributeInfoForTests(1416, "Кадастровый номер", RegisterAttributeType.STRING);
			Square = new AttributeInfoForTests(2, "Площадь", RegisterAttributeType.DECIMAL);
		}
	}

	public class AttributeInfoForTests
	{
		public long Id { get; }
		public long RegisterId { get; }
		public string Name { get; }
		public RegisterAttributeType Type { get; }

		internal AttributeInfoForTests(long id, string name, RegisterAttributeType type)
		{
			Id = id;
			Name = name;
			Type = type;
			RegisterId = 2;
		}
	}
}
