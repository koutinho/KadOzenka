using System;
using System.Collections.Generic;
using System.Text;

namespace CIPJS.DAL.HierarchicalGridAnalysisDamage.Models
{
    public class GroupEl<T>
    {
        public string GroupVal { get; set; }

        public List<T> Value { get; set; }

        public int Count { get; set; }
    }
}
