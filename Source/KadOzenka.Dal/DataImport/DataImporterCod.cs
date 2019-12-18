using Core.ErrorManagment;
using Core.Main.FileStorages;
using Core.Shared.Extensions;
using GemBox.Spreadsheet;
using ObjectModel.Commission;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core.SRD;
using ObjectModel.Common;
using System.Xml;

namespace KadOzenka.Dal.DataImport
{
    public static class DataImporterCod
    {
        /// <summary>
        /// Импорт классификатора ЦОД
        /// </summary>
        public static Stream ImportDataCodFromXml(XmlDocument xml, long CodJobId, bool deleteOld)
        {
            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            ParallelOptions options = new ParallelOptions
            {
                CancellationToken = cancelTokenSource.Token,
                MaxDegreeOfParallelism = 10
            };
            Console.WriteLine("Получение данных классификатора");
            List<ObjectModel.KO.OMCodDictionary> objs = ObjectModel.KO.OMCodDictionary.Where(x => x.IdCodjob== CodJobId).SelectAll().Execute();
            if (deleteOld)
            {
                Console.WriteLine("Удаление старых данных");
                Parallel.ForEach(objs, options, obj =>
                {
                    obj.Destroy();
                });
                objs.Clear();
            }

            Console.WriteLine("Чтение Xml");
            List<XmlNode> Items = new List<XmlNode>();
            XmlNodeList Nodes = xml.GetElementsByTagName("ItemClsGBU");
            foreach(XmlNode node in Nodes)
            {
                Items.Add(node);
            }
            int curIndex = 0;

            Console.WriteLine("Запись данных");
            Parallel.ForEach(Items, options, row =>
            {
                curIndex++;
                if (curIndex % 50 == 0)
                    Console.WriteLine(curIndex);

                string value = "";
                string code = "";
                string expert = "";
                string source = "";

                foreach(XmlNode node in row.ChildNodes)
                {
                    if (node.Name == "NameCls") value = node.InnerText;
                    else
                    if (node.Name == "GroupCls") code = node.InnerText;
                    else
                    if (node.Name == "StatusCls") source = node.InnerText;
                    else
                    if (node.Name == "ExpertCls") expert = node.InnerText;
                }


                ObjectModel.KO.OMCodDictionary existObject = null;
                if (!deleteOld)
                    existObject = objs.Find(x => x.IdCodjob == CodJobId && x.Value.ToUpper() == value.ToUpper());
                if (existObject == null)
                {
                    existObject = new ObjectModel.KO.OMCodDictionary
                    {
                        Id = -1,
                        Code=code,
                        Value = value,
                        Expert=expert,
                        Source=source,
                        IdCodjob=CodJobId
                    };
                    existObject.Save();
                }
                else
                {
                    existObject.Code = code;
                    existObject.Expert = expert;
                    existObject.Source = source;
                    existObject.Save();
                }
            });
            Console.WriteLine(curIndex);
            MemoryStream streamResult = new MemoryStream();
            xml.Save(streamResult);
            streamResult.Seek(0, SeekOrigin.Begin);
            return streamResult;
        }
        /// <summary>
        /// Импорт классификатора ЦОД
        /// </summary>
        public static Stream ImportDataCodFromXml(FileStream stream, long CodJobId, bool deleteOld)
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(stream);
            return ImportDataCodFromXml(xml, CodJobId, deleteOld);
        }

    }
}

