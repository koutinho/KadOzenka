namespace KadOzenka.Dal.Registers.Entities
{
    public class AttributePure
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public AttributePure()
        {
            
        }

        public AttributePure(long id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
