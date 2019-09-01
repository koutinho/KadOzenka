using System;
using System.Collections.Concurrent;
using System.IO;
using System.Xml.Linq;

namespace CIPJS.DAL.Bti.Import.Mapping
{
    public class DictionarySqlMapping
    {
        public XDocument Doc { get; private set; }
        ConcurrentDictionary<string, int> _cache;
        public DictionarySqlMapping()
        {
            Init();
        }

        void Init()
        {
            _cache = new ConcurrentDictionary<string, int>();

			string dictionarySqlMapping = Core.ConfigParam.Configuration.GetParam("DictionarySqlMapping", "\\Bti\\MappingConfiguration\\");
			
            Doc = XDocument.Parse(dictionarySqlMapping);

            foreach (var node in Doc.Root.Elements("item"))
            {
                var outerId = (int)node.Attribute("outerId");
                var selectField = node.Attribute("selectField").Value;
                _cache.TryAdd(selectField, outerId);
            }
        }

        /// <summary>
        /// пытаемся получить значение по передаваемому ключу
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public int? GetValue(string key)
        {
            int res;
            if (_cache.TryGetValue(key, out res))
                return res;
            //  System.Diagnostics.Trace.WriteLine("Ключ " + key + " не найден в словаре"); 
            return null;
        }
    }
}
