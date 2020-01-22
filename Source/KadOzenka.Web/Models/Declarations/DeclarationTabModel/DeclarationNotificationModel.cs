using System;
using System.ComponentModel.DataAnnotations;
using ObjectModel.Declarations;
using ObjectModel.Directory.Declarations;

namespace KadOzenka.Web.Models.Declarations.DeclarationTabModel
{
	public class DeclarationNotificationModel
	{

		/// <summary>
		/// Идентификатор декларации (Declaration_Id)
		/// </summary>
		public long? DeclarationId { get; set; }

		public bool IsEditApproveNotifications { get; set; }
		public bool IsEditOtherNotifications { get; set; }

		public static DeclarationNotificationModel FromEntity(OMDeclaration declaration)
		{
			return new DeclarationNotificationModel
			{
				DeclarationId = declaration != null ? declaration.Id : -1,
			};
		}
	}
}
