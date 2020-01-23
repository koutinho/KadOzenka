using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Text;
using Core.Shared.Extensions;
using Core.SRD;
using Core.UI.Registers.Reports.Model;
using DeepMorphy;
using NPetrovich;
using ObjectModel.Core.SRD;
using ObjectModel.Declarations;
using ObjectModel.Directory.Declarations;
using Platform.Reports;

namespace KadOzenka.Dal.FastReports
{
	public abstract class DeclarationNotificationReport : FastReportBase
	{
		protected MorphAnalyzer MorphAnalyzer {get; set; }

		protected DeclarationNotificationReport()
		{
			MorphAnalyzer = new MorphAnalyzer();
		}

		public override void InitializeFilterValues(long objId, string senderName, bool initialisation, List<FilterValue> filterValues)
		{
			if (initialisation)
			{
				var podpisantFilterValue = filterValues.FirstOrDefault(f => f.ParamName == "Podpisant");
				if (podpisantFilterValue != null)
				{
					podpisantFilterValue.ReportParameters = new List<ReportParameter>();
					var signatories = OMSignatory.Where(x => true).SelectAll().Execute();
					podpisantFilterValue.ReportParameters.AddRange(signatories.Select(s => new ReportParameter { Value = $"{s.FullName}, {s.Position}", Key = s.Id.ToString() }));
				}
			}
		}

		protected override DataSet GetData(NameValueCollection query, HashSet<long> objectList = null)
		{
			DataSet dataSet = new DataSet();
			dataSet.Tables.Add("Data");
			dataSet.Tables[0].Columns.Add("OwnerName");
			dataSet.Tables[0].Columns.Add("OwnerAddress");
			dataSet.Tables[0].Columns.Add("DeclarationDateIn");
			dataSet.Tables[0].Columns.Add("DeclarationNumber");
			dataSet.Tables[0].Columns.Add("UserIspName");
			dataSet.Tables[0].Columns.Add("MainData");
			dataSet.Tables[0].Columns.Add("AnnexLabel");
			dataSet.Tables[0].Columns.Add("Annex");
			dataSet.Tables[0].Columns.Add("SignatoryPosition");
			dataSet.Tables[0].Columns.Add("SignatoryName");

			var notification = OMUved
				.Where(x => x.Id == ObjectId)
				.SelectAll()
				.Execute().FirstOrDefault();
			if (notification == null)
			{
				throw new Exception($"Уведомление с ИД {ObjectId} не найдено");
			}

			var declaration = OMDeclaration
				.Where(x => x.Id == notification.Declaration_Id)
				.SelectAll()
				.Execute().FirstOrDefault();

			if (declaration == null)
			{
				throw new Exception($"Декларация не найдена");
			}

			OMSubject subject = null;
			SendUvedType uvedType = SendUvedType.None;
			var agent = OMSubject
				.Where(x => x.Id == declaration.Agent_Id)
				.SelectAll()
				.Execute().FirstOrDefault();
			var owner = OMSubject
				.Where(x => x.Id == declaration.Owner_Id)
				.SelectAll()
				.Execute().FirstOrDefault();
			if (owner != null && agent != null)
			{
				subject = agent;
				uvedType = declaration.UvedTypeAgent_Code;
			}
			else if (owner != null || agent != null)
			{
				if (owner != null)
				{
					subject = owner;
					uvedType = declaration.UvedTypeOwner_Code;
				}
				if (agent != null)
				{
					subject = agent;
					uvedType = declaration.UvedTypeAgent_Code;
				}
			}
			else
			{
				throw new Exception($"В декларации отсутствуют Заявитель и Представитель заявителя");
			}

			var subjectName = string.Empty;
			if (subject.Type_Code == SubjectType.Fl)
			{
				var petrovich = new Petrovich
				{
					FirstName = subject.I_Name,
					LastName = subject.F_Name,
					MiddleName = subject.O_Name,
					AutoDetectGender = true
				};
				var inflected = petrovich.InflectTo(Case.Dative);
				subjectName = inflected.LastName;
				if (!string.IsNullOrWhiteSpace(inflected.FirstName) &&
					!string.IsNullOrWhiteSpace(inflected.MiddleName))
				{
					subjectName += $" {inflected.FirstName.Trim()[0]}.{inflected.MiddleName.Trim()[0]}.";
				}
			}
			else
			{
				subjectName = subject.Name;
			}

			var book = OMBook
				.Where(x => x.Id == declaration.Book_Id)
				.SelectAll()
				.Execute().FirstOrDefault();

			var userIsp = OMUser
				.Where(x => x.Id == SRDSession.GetCurrentUserId())
				.SelectAll()
				.ExecuteFirstOrDefault();

			var userIspName = userIsp.Name;
			if (!string.IsNullOrWhiteSpace(userIsp.Surname) && !string.IsNullOrWhiteSpace(userIsp.Patronymic))
			{
				userIspName += $" {userIsp.Surname.Trim()[0]}.{userIsp.Patronymic.Trim()[0]}.";
			}

			var signatoryId = GetQueryParam<long>("Podpisant", query);
			var signatory = OMSignatory.Where(x => x.Id == signatoryId).SelectAll().Execute().FirstOrDefault();
			if (signatory == null)
			{
				throw new Exception($"Подписант не найден");
			}

			dataSet.Tables[0].Rows.Add(
				subjectName,
				FormAddress(subject, uvedType),
				declaration.DateIn?.ToString("dd.MM.yyyy"),
				$"{declaration.NumIn}/{book?.Prefics}",
				userIspName,
				GetMainData(declaration, notification),
				"\u0009Приложение:",
				GetAnnex(notification),
				signatory.Position,
				signatory.FullName);

			return dataSet;
		}

		public virtual string GetAnnex(OMUved notification)
		{
			return null;
		}

		public abstract string GetMainData(OMDeclaration declaration, OMUved notification);

		public string GetObjectTypeString(ObjectType objectType)
		{
			string result = null;
			switch (objectType)
			{
				case ObjectType.None:
				{
					result = string.Empty;
					break;
				}
				case ObjectType.Ons:
				{
					result = objectType.GetEnumDescription();
					break;
				}
				default:
				{
					result = objectType.GetEnumDescription().ToLower();
					break;
				}
			}

			return result;
		}

		public string PrepareText(string text)
		{
			if (string.IsNullOrWhiteSpace(text.Trim()))
			{
				return string.Empty;
			}

			var result = new StringBuilder();
			var words = text.Trim().Split(' ').Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
			var results = MorphAnalyzer.Parse(words).ToArray();
			for (var i = 0; i < words.Count() - 1; i++)
			{
				result.Append(words[i]);
				if (results[i].BestTag["чр"] == "предл" || results[i].BestTag["чр"] == "союз" ||
				    results[i].BestTag["чр"] == "част" || results[i].BestTag["чр"] == "мест" || results[i].BestTag["чр"] == "межд")
				{
					result.Append("\u00A0");
				}
				else
				{
					result.Append(" ");
				}
			}
			result.Append(words.Last());

			return result.ToString();
		}

		public string FormAddress(OMSubject subject, SendUvedType uvedType)
		{
			if (subject == null)
			{
				return string.Empty;
			}

			string address = string.Empty;
			if (uvedType == SendUvedType.Email)
			{
				address = subject.Mail;
			} else
			{
				var addressParts = new List<string>
				{
					subject.Street?.Replace(" ", "\u00A0"),
					subject.House?.Replace(" ", "\u00A0"),
					subject.Building?.Replace(" ", "\u00A0"),
					subject.Flat?.Replace(" ", "\u00A0"),
					subject.City?.Replace(" ", "\u00A0"),
					subject.Zip
				};
				address = string.Join(", ", addressParts.Where(x => !string.IsNullOrWhiteSpace(x)));
			}

			return address;
		}
	}
}
