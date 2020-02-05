using System.Collections.Generic;
using Core.Register;
using Core.Shared.Extensions;
using Core.Shared.Misc;
using ObjectModel.Declarations;
using ObjectModel.Directory.Declarations;

namespace KadOzenka.Dal.DataImport.Helpers
{
	public static class DataImporterDeclarationsValidator
	{
		public static void ValidateBookModification(SerializableDictionary<int, RegisterAttributeValue> attrValues,
			List<string> result)
		{
			var bookIdAttr = OMDeclaration.GetAttributeData(x => x.Book_Id);
			if (attrValues.ContainsKey(bookIdAttr.Id))
			{
				var bookId = attrValues[bookIdAttr.Id].Value.ParseToLongNullable();
				if (!bookId.HasValue)
				{
					result.Add($"атрибут {bookIdAttr.Name} обязательный");
				}
				else
				{
					var book = OMBook
						.Where(x => x.Id == bookId.Value)
						.SelectAll()
						.ExecuteFirstOrDefault();
					if (book == null)
					{
						result.Add($"книга с указанным ID {bookId.Value.ToString()} не найдена");
					}
				}
			}
		}

		public static void ValidateObjTypeModification(SerializableDictionary<int, RegisterAttributeValue> attrValues, List<string> result)
		{
			var objTypeAttr = OMDeclaration.GetAttributeData(x => x.TypeObj);
			if (attrValues.ContainsKey(objTypeAttr.Id))
			{
				var objTypeVal = attrValues[objTypeAttr.Id].Value.ParseToStringNullable();
				if (string.IsNullOrEmpty(objTypeVal))
				{
					result.Add($"атрибут {objTypeAttr.Name} обязательный");
				}
			}
		}

		public static void ValidateNumberInModification(SerializableDictionary<int, RegisterAttributeValue> attrValues, List<string> result)
		{
			var numInAttr = OMDeclaration.GetAttributeData(x => x.NumIn);
			if (attrValues.ContainsKey(numInAttr.Id))
			{
				var numInVal = attrValues[numInAttr.Id].Value.ParseToStringNullable();
				if (string.IsNullOrEmpty(numInVal))
				{
					result.Add($"атрибут {numInAttr.Name} обязательный");
				}
			}
		}

		public static void ValidateCadastralNumberModification(SerializableDictionary<int, RegisterAttributeValue> attrValues, List<string> result)
		{
			var cadNumAttr = OMDeclaration.GetAttributeData(x => x.CadastralNumObj);
			if (attrValues.ContainsKey(cadNumAttr.Id))
			{
				var cadNumVal = attrValues[cadNumAttr.Id].Value.ParseToStringNullable();
				if (string.IsNullOrEmpty(cadNumVal))
				{
					result.Add($"атрибут {cadNumAttr.Name} обязательный");
				}
			}
		}

		public static void ValidateOwnerAndAgentModification(
			SerializableDictionary<int, RegisterAttributeValue> attrValues, OMDeclaration declaration,
			List<string> result)
		{
			var ownerIdAttr = OMDeclaration.GetAttributeData(x => x.Owner_Id);
			var agentIdAttr = OMDeclaration.GetAttributeData(x => x.Agent_Id);
			if (attrValues.ContainsKey(ownerIdAttr.Id) && attrValues.ContainsKey(agentIdAttr.Id))
			{
				var newOwnerId = attrValues[ownerIdAttr.Id].Value.ParseToLongNullable();
				var newAgentId = attrValues[agentIdAttr.Id].Value.ParseToLongNullable();
				if (!newOwnerId.HasValue && !newAgentId.HasValue)
				{
					result.Add($"должен быть заполнен хотя бы один атрибут: {ownerIdAttr.Name} или {agentIdAttr.Name}");
				}
				else
				{
					var newOwner = OMSubject
						.Where(x => x.Id == newOwnerId)
						.SelectAll()
						.ExecuteFirstOrDefault();
					var newAgent = OMSubject
						.Where(x => x.Id == newAgentId)
						.SelectAll()
						.ExecuteFirstOrDefault();
					if (newOwner == null && newAgent == null)
					{
						result.Add(
							$"не найден хотя бы один объект по указанным атрибутам: {ownerIdAttr.Name} или {agentIdAttr.Name}");
					}
					else
					{
						if (newAgent != null)
						{
							var sendUvedTypeAttr = OMDeclaration.GetAttributeData(x => x.UvedTypeAgent);
							var sendUvedType = attrValues.ContainsKey(sendUvedTypeAttr.Id)
								? attrValues[sendUvedTypeAttr.Id].Value.ParseToStringNullable()
								: declaration.UvedTypeAgent_Code.GetEnumDescription();
							var validationResult =
								ValidateSubjectNotificationType(newAgent, sendUvedType, "Представителя");
							if (!string.IsNullOrEmpty(validationResult))
							{
								result.Add(validationResult);
							}
						}
						else
						{
							if (!declaration.Owner_Id.HasValue)
							{
								result.Add(
									$"должен быть заполнен хотя бы один атрибут: {ownerIdAttr.Name} или {agentIdAttr.Name}");
							}
						}

						if (newOwner != null)
						{
							var sendUvedTypeAttr = OMDeclaration.GetAttributeData(x => x.UvedTypeOwner);
							var sendUvedType = attrValues.ContainsKey(sendUvedTypeAttr.Id)
								? attrValues[sendUvedTypeAttr.Id].Value.ParseToStringNullable()
								: declaration.UvedTypeOwner_Code.GetEnumDescription();
							var validationResult = ValidateSubjectNotificationType(newOwner, sendUvedType, "Заявителя");
							if (!string.IsNullOrEmpty(validationResult))
							{
								result.Add(validationResult);
							}
						}
						else
						{
							if (!declaration.Agent_Id.HasValue)
							{
								result.Add(
									$"должен быть заполнен хотя бы один атрибут: {ownerIdAttr.Name} или {agentIdAttr.Name}");
							}
						}
					}
				}
			}
			else if (attrValues.ContainsKey(ownerIdAttr.Id))
			{
				var newOwnerId = attrValues[ownerIdAttr.Id].Value.ParseToLongNullable();
				if (!newOwnerId.HasValue && !declaration.Agent_Id.HasValue)
				{
					result.Add($"должен быть заполнен хотя бы один атрибут: {ownerIdAttr.Name} или {agentIdAttr.Name}");
				}
				else
				{
					var newOwner = OMSubject
						.Where(x => x.Id == newOwnerId)
						.SelectAll()
						.ExecuteFirstOrDefault();
					if (newOwner == null)
					{
						result.Add(
							$"не найден объект по указанному атрибуту: {ownerIdAttr.Name}");
					}
					else 
					{
						var sendUvedTypeAttr = OMDeclaration.GetAttributeData(x => x.UvedTypeOwner);
						var sendUvedType = attrValues.ContainsKey(sendUvedTypeAttr.Id)
							? attrValues[sendUvedTypeAttr.Id].Value.ParseToStringNullable()
							: declaration.UvedTypeOwner_Code.GetEnumDescription();
						var validationResult = ValidateSubjectNotificationType(newOwner, sendUvedType, "Заявителя");
						if (!string.IsNullOrEmpty(validationResult))
						{
							result.Add(validationResult);
						}
					}
				}
			}
			else if (attrValues.ContainsKey(agentIdAttr.Id))
			{
				var newAgentId = attrValues[agentIdAttr.Id].Value.ParseToLongNullable();
				if (!newAgentId.HasValue && !declaration.Owner_Id.HasValue)
				{
					result.Add($"должен быть заполнен хотя бы один атрибут: {ownerIdAttr.Name} или {agentIdAttr.Name}");
				}
				else
				{
					var newAgent = OMSubject
						.Where(x => x.Id == newAgentId)
						.SelectAll()
						.ExecuteFirstOrDefault();
					if (newAgent == null)
					{
						result.Add(
							$"не найден объект по указанному атрибуту: {agentIdAttr.Name}");
					}
					else 
					{
						var sendUvedTypeAttr = OMDeclaration.GetAttributeData(x => x.UvedTypeAgent);
						var sendUvedType = attrValues.ContainsKey(sendUvedTypeAttr.Id)
							? attrValues[sendUvedTypeAttr.Id].Value.ParseToStringNullable()
							: declaration.UvedTypeAgent_Code.GetEnumDescription();
						var validationResult = ValidateSubjectNotificationType(newAgent, sendUvedType, "Представителя");
						if (!string.IsNullOrEmpty(validationResult))
						{
							result.Add(validationResult);
						}
					}
				}
			}
		}

		public static void ValidateOwnerUvedTypeModification(SerializableDictionary<int, RegisterAttributeValue> attrValues, OMDeclaration declaration, List<string> result)
		{
			var ownerIdAttr = OMDeclaration.GetAttributeData(x => x.Owner_Id);
			var sendUvedOwnerTypeAttr = OMDeclaration.GetAttributeData(x => x.UvedTypeOwner);
			if (attrValues.ContainsKey(sendUvedOwnerTypeAttr.Id))
			{
				var sendUvedOwnerType = attrValues[sendUvedOwnerTypeAttr.Id].Value.ParseToStringNullable();
				var ownerId = attrValues.ContainsKey(ownerIdAttr.Id)
					? attrValues[ownerIdAttr.Id].Value.ParseToLongNullable()
					: declaration.Owner_Id;
				var owner = OMSubject
					.Where(x => x.Id == ownerId)
					.SelectAll()
					.ExecuteFirstOrDefault();
				if (owner != null)
				{
					var validationResult =
						ValidateSubjectNotificationType(owner, sendUvedOwnerType, "Заявителя");
					if (!string.IsNullOrEmpty(validationResult))
					{
						result.Add(validationResult);
					}
				}
			}
		}

		public static void ValidateAgentUvedTypeModification(SerializableDictionary<int, RegisterAttributeValue> attrValues, OMDeclaration declaration, List<string> result)
		{
			var agentIdAttr = OMDeclaration.GetAttributeData(x => x.Agent_Id);
			var sendUvedAgentTypeAttr = OMDeclaration.GetAttributeData(x => x.UvedTypeAgent);
			if (attrValues.ContainsKey(sendUvedAgentTypeAttr.Id))
			{
				var sendUvedAgentType = attrValues[sendUvedAgentTypeAttr.Id].Value.ParseToStringNullable();
				var agentId = attrValues.ContainsKey(agentIdAttr.Id)
					? attrValues[agentIdAttr.Id].Value.ParseToLongNullable()
					: declaration.Agent_Id;
				var agent = OMSubject
					.Where(x => x.Id == agentId)
					.SelectAll()
					.ExecuteFirstOrDefault();
				if (agent != null)
				{
					var validationResult =
						ValidateSubjectNotificationType(agent, sendUvedAgentType, "Представителя");
					if (!string.IsNullOrEmpty(validationResult))
					{
						result.Add(validationResult);
					}
				}
			}
		}

		public static void ValidateBookInitialFilling(SerializableDictionary<int, RegisterAttributeValue> attrValues, List<string> result)
		{
			var bookIdAttr = OMDeclaration.GetAttributeData(x => x.Book_Id);
			var bookId = attrValues.ContainsKey(bookIdAttr.Id) ? attrValues[bookIdAttr.Id].Value.ParseToLongNullable() : (long?)null;
			if (!bookId.HasValue)
			{
				result.Add($"атрибут {bookIdAttr.Name} обязательный");
			}
			else
			{
				var book = OMBook
					.Where(x => x.Id == bookId.Value)
					.SelectAll()
					.ExecuteFirstOrDefault();
				if (book == null)
				{
					result.Add($"книга с указанным ID {bookId.Value.ToString()} не найдена");
				}
			}
		}

		public static void ValidateObjTypeInitialFilling(SerializableDictionary<int, RegisterAttributeValue> attrValues, List<string> result)
		{
			var objTypeAttr = OMDeclaration.GetAttributeData(x => x.TypeObj);
			var objTypeVal = attrValues.ContainsKey(objTypeAttr.Id) ? attrValues[objTypeAttr.Id].Value.ParseToStringNullable() : null;
			if (string.IsNullOrEmpty(objTypeVal))
			{
				result.Add($"атрибут {objTypeAttr.Name} обязательный");
			}
		}

		public static void ValidateNumberInInitialFilling(SerializableDictionary<int, RegisterAttributeValue> attrValues, List<string> result)
		{
			var numInAttr = OMDeclaration.GetAttributeData(x => x.NumIn);
			var numInVal = attrValues.ContainsKey(numInAttr.Id) ? attrValues[numInAttr.Id].Value.ParseToStringNullable() : null;
			if (string.IsNullOrEmpty(numInVal))
			{
				result.Add($"атрибут {numInAttr.Name} обязательный");
			}
		}

		public static void ValidateCadastralNumberInitialFilling(SerializableDictionary<int, RegisterAttributeValue> attrValues, List<string> result)
		{
			var cadNumAttr = OMDeclaration.GetAttributeData(x => x.CadastralNumObj);
			var cadNumVal = attrValues.ContainsKey(cadNumAttr.Id) ? attrValues[cadNumAttr.Id].Value.ParseToStringNullable() : null;
			if (string.IsNullOrEmpty(cadNumVal))
			{
				result.Add($"атрибут {cadNumAttr.Name} обязательный");
			}
		}

		public static void ValidateOwnerAndAgentInitialFilling(
			SerializableDictionary<int, RegisterAttributeValue> attrValues, List<string> result)
		{
			var ownerIdAttr = OMDeclaration.GetAttributeData(x => x.Owner_Id);
			var ownerId = attrValues.ContainsKey(ownerIdAttr.Id)
				? attrValues[ownerIdAttr.Id].Value.ParseToLongNullable()
				: (long?) null;

			var agentIdAttr = OMDeclaration.GetAttributeData(x => x.Agent_Id);
			var agentId = attrValues.ContainsKey(agentIdAttr.Id)
				? attrValues[agentIdAttr.Id].Value.ParseToLongNullable()
				: (long?) null;

			if (!ownerId.HasValue && !agentId.HasValue)
			{
				result.Add($"должен быть указан хотя бы один атрибут: {ownerIdAttr.Name} или {agentIdAttr.Name}");
			}
			else
			{
				var owner = OMSubject
					.Where(x => x.Id == ownerId)
					.SelectAll()
					.ExecuteFirstOrDefault();
				var agent = OMSubject
					.Where(x => x.Id == agentId)
					.SelectAll()
					.ExecuteFirstOrDefault();
				if (owner == null && agent == null)
				{
					result.Add($"не найдены объекты по указанным атрибутам: {ownerIdAttr.Name} или {agentIdAttr.Name}");
				}
				else
				{
					if (agent != null)
					{
						var sendUvedTypeAttr = OMDeclaration.GetAttributeData(x => x.UvedTypeAgent);
						var sendUvedType = attrValues.ContainsKey(sendUvedTypeAttr.Id)
							? attrValues[sendUvedTypeAttr.Id].Value.ParseToStringNullable()
							: null;
						var validationResult = ValidateSubjectNotificationType(agent, sendUvedType, "Представителя");
						if (!string.IsNullOrEmpty(validationResult))
						{
							result.Add(validationResult);
						}
					}

					if (owner != null)
					{
						var sendUvedTypeAttr = OMDeclaration.GetAttributeData(x => x.UvedTypeOwner);
						var sendUvedType = attrValues.ContainsKey(sendUvedTypeAttr.Id)
							? attrValues[sendUvedTypeAttr.Id].Value.ParseToStringNullable()
							: null;
						var validationResult = ValidateSubjectNotificationType(owner, sendUvedType, "Заявителя");
						if (!string.IsNullOrEmpty(validationResult))
						{
							result.Add(validationResult);
						}
					}
				}
			}
		}

		private static string ValidateSubjectNotificationType(OMSubject subject, string subjectUvedType, string subjectName)
		{
			if (subjectUvedType == SendUvedType.Email.GetEnumDescription() || subjectUvedType == SendUvedType.Email.GetEnumCode())
			{
				if (string.IsNullOrWhiteSpace(subject?.Mail))
				{
					return
						$"У выбранного {subjectName} отсутствуют необходимые данные для данного Способа получения уведомления: Адрес электронной почты";
				}
			}
			else if (string.IsNullOrWhiteSpace(subject?.Zip) || string.IsNullOrWhiteSpace(subject?.City) ||
					 string.IsNullOrWhiteSpace(subject?.Street) || string.IsNullOrWhiteSpace(subject?.House))
			{
				var emptyAddressParts = new List<string>();
				if (string.IsNullOrWhiteSpace(subject?.Zip))
				{
					emptyAddressParts.Add("Индекс");
				}

				if (string.IsNullOrWhiteSpace(subject?.City))
				{
					emptyAddressParts.Add("Город");
				}

				if (string.IsNullOrWhiteSpace(subject?.Street))
				{
					emptyAddressParts.Add("Улица");
				}

				if (string.IsNullOrWhiteSpace(subject?.House))
				{
					emptyAddressParts.Add("Дом");
				}

				return
					$"У выбранного {subjectName} отсутствуют необходимые данные для данного Способа получения уведомления: {string.Join(", ", emptyAddressParts)}";
			}

			return string.Empty;
		}
	}
}
