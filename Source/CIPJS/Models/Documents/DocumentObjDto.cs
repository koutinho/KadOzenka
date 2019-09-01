using ObjectModel.Directory;

namespace CIPJS.Models.Documents
{
    public class DocumentObjDto
    {
        /// <summary>
        /// Ссылка на уникальный номер записи
        /// </summary>
        public long? ObjId { get; set; }

        /// <summary>
        /// Номер реестра записи
        /// </summary>
        public long? ReestrId { get; set; }

        /// <summary>
        /// Тип договора
        /// </summary>
        public ContractType? ContractType { get; set; }
    }
}
