namespace CIPJS.DAL.DocBaseType
{
    public class DocBaseTypeDto
    {
        public long Id { get; set; }

        /// <summary>
        /// Документ-основание
        /// </summary>
        public string DocumentBase { get; set; }

        /// <summary>
        /// Вид
        /// </summary>
        public string Type { get; set; }
    }
}
