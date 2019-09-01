using System;
using System.Collections.Generic;
using System.Text;

namespace CIPJS.DAL.InputFilePackage
{
    public class InputFilePackageCriteriaDto
    {
        public string Name { get; set; }

        public string Url { get; set; }

        public long Count { get; set; }

        public bool IsGroup { get; set; }
    }
}
