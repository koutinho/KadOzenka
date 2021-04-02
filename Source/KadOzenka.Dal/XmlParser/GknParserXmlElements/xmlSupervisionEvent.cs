using System;

namespace KadOzenka.Dal.XmlParser
{
	/// <summary>
	/// Сведения о результатах проведения государственного земельного надзора
	/// </summary>
	public class xmlSupervisionEvent
	{
		/// <summary>
		/// Наименование органа, проводившего мероприятие по государственному земельному надзору
		/// </summary>
		public string Agency;
		/// <summary>
		/// Мероприятие государственного земельного надзора по соблюдению требований законодательства (плановая, внеплановая проверка, административное обследование)
		/// </summary>
		public xmlCodeName EventName;
		/// <summary>
		/// Форма проведения плановой или внеплановой проверки
		/// </summary>
		public xmlCodeName EventForm;
		/// <summary>
		/// Дата окончания проверки
		/// </summary>
		public DateTime InspectionEnd;
		/// <summary>
		/// Наличие нарушения: правонарушение выявлено (1-true)/не выявлено (0-false)
		/// </summary>
		public bool AvailabilityViolations;
		/// <summary>
		/// Выявленное правонарушение
		/// </summary>
		public xmlIdentifiedViolations IdentifiedViolations;
		/// <summary>
		/// Реквизиты оформленных документов
		/// </summary>
		public xmlDocument DocRequisites;
		/// <summary>
		/// Сведения об устранении выявленного нарушения
		/// </summary>
		public xmlElimination Elimination;
		/// <summary>
		/// Реквизиты документа, содержащего сведения об устранении правонарушения
		/// </summary>
		public xmlDocument EliminationDocRequisites;
	}
}