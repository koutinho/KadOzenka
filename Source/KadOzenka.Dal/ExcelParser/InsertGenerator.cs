using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Text.RegularExpressions;
using GemBox.Spreadsheet;
using KadOzenka.Dal.CommonFunctions;

namespace KadOzenka.Dal.ExcelParser
{

    public class InsertGenerator
    {

        public void GenerateInsertData(string sql, int startItemId, int startCode, int referenceId)
        {
            ExcelFile excelFile = ExcelFile.Load(ConfigurationManager.AppSettings["WallMaterialFile"]);
            ExcelWorksheet ws = excelFile.Worksheets[0];
            Transliterator transliterator = new Transliterator();
            foreach (var row in ws.Rows)
            {
                string translit =
                    Regex.Replace(
                        Regex.Replace(
                            Regex.Replace(
                                transliterator.Transliterate(row.Cells[0].Value.ToString()), @"[\s\,\.\-\\(\\/][a-z]", l => l.ToString().ToUpper()
                            ), @"[\s\,\.\-\\(\\)\\/]", string.Empty
                        ), "\"", string.Empty
                    );
                Console.WriteLine(string.Format($"{sql}\n", startItemId++, referenceId, startCode++, row.Cells[0].Value.ToString(), translit));
            }
        }

    }

}
