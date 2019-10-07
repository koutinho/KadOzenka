using System;
using System.Collections.Generic;
using System.Text;
using IronXL;
using IronXL.Layout;
using System.Linq;

namespace KadOzenka.Dal.Model.ExceleParser
{
    class ParceExceleFile
    {
        public void ReadDataFromExcel()
        {
            WorkBook workbook = WorkBook.Load("test.xlsx");
            WorkSheet sheet = workbook.WorkSheets.First();
            //Select cells easily in Excel notation and return the calculated value
            int cellValue = sheet["A2"].IntValue;
            // Read from Ranges of cells elegantly.
            foreach (var cell in sheet["A2:A10"])
            {
                Console.WriteLine("Cell {0} has value '{1}'", cell.AddressString, cell.Text);
            }
            ///Advanced Operations
            //Calculate aggregate values such as Min, Max and Sum
            decimal sum = sheet["A2:A10"].Sum();
            //Linq compatible
            decimal max = sheet["A2:A10"].Max(c => c.DecimalValue);
        }
    }
}
