using System.Linq;
using System.Text;
using Core.Shared.Extensions;
using DeepMorphy;
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
	}
}
