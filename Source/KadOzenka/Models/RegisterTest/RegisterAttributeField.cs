using Core.Register;

namespace CIPJS.Models.RegisterTest
{
    public class RegisterAttributeField
    {
        public long AttributeId { get; set; }

		public bool IsPrimaryKey { get; set; }

        public string Title { get; set; }

        public long? ReferenceId { get; set; }

        public RegisterAttributeType Type { get; set; }

        public object Value { get; set; }

        public long? ParentRegisterId { get; set; }

        public string ParentValueString { get; set; }
    }
}