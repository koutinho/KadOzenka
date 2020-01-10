using System.Collections.Generic;
using System.Linq;
using Core.Shared.Extensions;
using ObjectModel.Directory.Declarations;
using Platform.Reports;

namespace KadOzenka.Dal.FastReports
{
	public abstract class DeclarationNotificationReport : FastReportBase
	{
		const int MaxLineWidth = 80;

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

		public List<string> GetTextLines(string text)
		{
			//List<string> lines = new List<string>();
			//StringBuilder line = new StringBuilder();
			//foreach (Match word in Regex.Matches(text, @"\S+", RegexOptions.ECMAScript))
			//{
			//	if (word.Value.Length + line.Length + 1 > MAX_WIDTH)
			//	{
			//		lines.Add(line.ToString());
			//		line.Length = 0;
			//	}
			//	line.Append(String.Format("{0} ", word.Value));

			//	if (line.Length > 0)
			//		line.Append(word.Value);
			//}

			var words = text.Split(' ').Concat(new[] { "" });
			return
				words
					.Skip(1)
					.Aggregate(
						words.Take(1).ToList(),
						(a, w) =>
						{
							var last = a.Last();
							while (last.Length > MaxLineWidth)
							{
								a[a.Count() - 1] = last.Substring(0, MaxLineWidth);
								last = last.Substring(MaxLineWidth);
								a.Add(last);
							}
							var test = last + " " + w;
							if (test.Length > MaxLineWidth)
							{
								a.Add(w);
							}
							else
							{
								a[a.Count() - 1] = test;
							}
							return a;
						});
		}

		//public string WrapText(SpriteFont spriteFont, string text, float maxLineWidth)
		//{
		//	string[] words = text.Split(' ');
		//	StringBuilder sb = new StringBuilder();
		//	float lineWidth = 0f;
		//	float spaceWidth = spriteFont.MeasureString(" ").X;

		//	foreach (string word in words)
		//	{
		//		Vector2 size = spriteFont.MeasureString(word);

		//		if (lineWidth + size.X < maxLineWidth)
		//		{
		//			sb.Append(word + " ");
		//			lineWidth += size.X + spaceWidth;
		//		}
		//		else
		//		{
		//			sb.Append("\n" + word + " ");
		//			lineWidth = size.X + spaceWidth;
		//		}
		//	}

		//	return sb.ToString();
		//}
	}
}
