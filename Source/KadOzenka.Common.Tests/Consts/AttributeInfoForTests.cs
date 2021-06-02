using Core.Register;

namespace KadOzenka.Common.Tests.Consts
{
	public class AttributeInfoForTests
	{
		public long Id { get; }
		public string Name { get; }
		public RegisterAttributeType Type { get; }


		internal AttributeInfoForTests(long id, string name, RegisterAttributeType type)
		{
			Id = id;
			Name = name;
			Type = type;
		}
	}
}
