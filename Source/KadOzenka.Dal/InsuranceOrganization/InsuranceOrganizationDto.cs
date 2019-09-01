namespace CIPJS.DAL.InsuranceOrganization
{
    public class InsuranceOrganizationDto
    {
        public long Id { get; set; }

        /// <summary>
        /// Наименование страховой организации
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Сокращенное наименование страховой компании
        /// </summary>
        public string ShortName { get; set; }

        /// <summary>
        /// Код
        /// </summary>
        public long? Code { get; set; }
    }
}
