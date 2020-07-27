namespace KadOzenka.Dal.Groups.Dto
{
    public class GroupCalculationSettingsDto
    {
        public long Id { get; set; }
        public string GroupName { get; set; }
        public int Priority { get; set; }
        public bool Stage1 { get; set; }
        public bool Stage2 { get; set; }
        public bool Stage3 { get; set; }
    }
}
