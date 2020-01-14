using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Shared.Extensions;
using DeepMorphy;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
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
			if (string.IsNullOrWhiteSpace(text))
			{
				return string.Empty;
			}

			var result = new StringBuilder();
			var words = text.Split(' ').ToList();
			var results = MorphAnalyzer.Parse(words).ToArray();
			for (var i = 0; i < words.Count() - 1; i++)
			{
				result.Append(words[i]);
				if (results[i].BestTag["чр"] == "предл" || results[i].BestTag["чр"] == "союз" ||
				    results[i].BestTag["чр"] == "част" || results[i].BestTag["чр"] == "мест")
				{
					result.Append("&nbsp;");
				}
				else
				{
					result.Append(" ");
				}
			}
			result.Append(words.Last());

			return result.ToString();
		}

		public string FormAddress(OMSubject subject)
		{
			if (subject == null)
			{
				return string.Empty;
			}

			var addressParts = new List<string>
			{
				subject.Zip,
				subject.City.Replace(" ", "&nbsp;"),
				subject.Street.Replace(" ", "&nbsp;"),
				subject.House.Replace(" ", "&nbsp;"),
				subject.Building.Replace(" ", "&nbsp;"),
				subject.Flat.Replace(" ", "&nbsp;")
			};

			return string.Join(", ", addressParts.Where(x => !string.IsNullOrWhiteSpace(x)));
		}
	}
}
