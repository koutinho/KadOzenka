using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CIPJS.DAL.HierarchicalGridAnalysisDamage.ModelBuilder
{
    public class GroupSelect<T, K>
    {
        private readonly Dictionary<GroupSelectEl<T>, K> boof;

        public GroupSelect()
        {
            boof = new Dictionary<GroupSelectEl<T>, K>();
        }

        public void Add(GroupSelectEl<T> el, K val)
        {
            boof.Add(el, val);
        }        

        public K Serch(IEnumerable<T> colection)
        {
            var resCol = new List<GroupSelectEl<T>>();

            foreach (var i in boof)
            {
                var key = false;

                foreach (var j in i.Key.Values)
                {
                    if (!colection.Contains(j))
                    {
                        key = true;
                        break;
                    }
                }

                if (!key)
                {
                    if (resCol.Any())
                        foreach (var k in i.Key.Groups)
                        {
                            var kkey = false;

                            foreach (var l in resCol)
                            {
                                if (!l.Groups.Contains(k))
                                {
                                    kkey = true;
                                    break;
                                }
                            }

                            if (!kkey)
                            {
                                resCol.Add(i.Key);
                                break;
                            }
                            else
                                throw new Exception("Некорректная комбинация значений");
                        }
                    else
                        resCol.Add(i.Key);
                }
            }

            if (!resCol.Any())
                throw new Exception("Значения не найдены");

            var maxL = resCol.Max(y => y.Values.Length);
            var res = resCol.Where(x => x.Values.Length == maxL).FirstOrDefault();

            return boof[res];
        }
    }

    public class GroupSelectEl<T>
    {
        public GroupSelectEl(T[] vals, int[] groups)
        {
            Values = vals;
            Groups = groups;
        }

        public T[] Values { get; }
        public int[] Groups { get; }
    }
}
