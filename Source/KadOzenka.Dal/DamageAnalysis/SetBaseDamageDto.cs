namespace CIPJS.DAL.DamageAnalysis
{
    public class SetBaseDamageDto
    {
        public long DamageId { get; set; }
        public string DamageNomDoc { get; set; }
        public long? BaseDamageId { get; set; }
        public string BaseDamageNomDoc { get; set; }
        public bool IsSetValueType { get; set; }
    }
}
