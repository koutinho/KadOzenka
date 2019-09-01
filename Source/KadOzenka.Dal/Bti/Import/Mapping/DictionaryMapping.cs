using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace CIPJS.DAL.Bti.Import.Mapping
{
    public class DictionaryMapping
    {
        public XDocument Doc { get; private set; }
        ConcurrentDictionary<int, int> _cache;
        public DictionaryMapping()
        {
            Init();
        }

        void Init()
        {
            _cache = new ConcurrentDictionary<int, int>();

			string dictionaryMapping = Core.ConfigParam.Configuration.GetParam("DictionaryMapping", "\\Bti\\MappingConfiguration\\");
			
			Doc =  XDocument.Parse(dictionaryMapping);
            foreach (var node in Doc.Root.Elements("item"))
            {
                var outerId = (int)node.Attribute("outerId");
                var innerId = (int)node.Attribute("innerId");
                _cache.TryAdd(outerId, innerId);
            }
        }

        /// <summary>
        /// пытаемся получить значение по передаваемому ключу
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public int? GetValue(int key)
        {
            int res;
            if (_cache.TryGetValue(key, out res))
                return res;
            //  System.Diagnostics.Trace.WriteLine("Ключ " + key + " не найден в словаре"); 
            return null;
        }

        public ICollection<int> GetOuterIds()
        {
            return _cache.Keys.ToList();
        }
    }
}
