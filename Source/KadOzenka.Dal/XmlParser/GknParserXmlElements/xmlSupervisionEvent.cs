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
		public string Agency { get; set; }
		/// <summary>
		/// Мероприятие государственного земельного надзора по соблюдению требований законодательства (плановая, внеплановая проверка, административное обследование)
		/// </summary>
		public xmlCodeName EventName { get; set; }
		/// <summary>
		/// Форма проведения плановой или внеплановой проверки
		/// </summary>
		public xmlCodeName EventForm { get; set; }
		/// <summary>
		/// Дата окончания проверки
		/// </summary>
		public DateTime? InspectionEnd { get; set; }
		/// <summary>
		/// Наличие нарушения: правонарушение выявлено (1-true)/не выявлено (0-false)
		/// </summary>
		public bool? AvailabilityViolations { get; set; }
		/// <summary>
		/// Выявленное правонарушение
		/// </summary>
		public xmlIdentifiedViolations IdentifiedViolations { get; set; }
		/// <summary>
		/// Реквизиты оформленных документов
		/// </summary>
		public xmlDocument DocRequisites { get; set; }
		/// <summary>
		/// Сведения об устранении выявленного нарушения
		/// </summary>
		public xmlElimination Elimination { get; set; }
		/// <summary>
		/// Реквизиты документа, содержащего сведения об устранении правонарушения
		/// </summary>
		public xmlDocument EliminationDocRequisites { get; set; }


		public xmlSupervisionEvent()
		{
			EventName = new xmlCodeName();
			EventForm = new xmlCodeName();
			IdentifiedViolations = new xmlIdentifiedViolations();
			DocRequisites = new xmlDocument();
			Elimination = new xmlElimination();
			EliminationDocRequisites = new xmlDocument();
		}
	}
}