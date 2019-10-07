using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Linq;
using System.Globalization;

using Microsoft.Practices.EnterpriseLibrary.Data;
using ObjectModel.Market;
using ObjectModel.Core.LongProcess;
using Core.Register.LongProcessManagment;

namespace OuterMarketParser.DatabaseReader
{
    class OuterMarketSettings
    {
        public DateTime LastSuccesfulUpdateDate { get; set; }
        public string InitialTimeTemplate { get; set; }
        public string Login { get; set; }
        public string Token { get; set; }
        public int TimeDelta { get; set; }
        public string Link { get; set; }
        public List<int> DealIds { get; set; }
        public List<int> RegionIDs { get; set; }
        public string Template { get; set; }

        public OuterMarketSettings()
        {
            //var lastlog = OMQueue.Where(x => x.ProcessTypeId == 1 && (x.Status == (long)OMQueueStatus.Completed || 
            //                                                          x.Status == (long)OMQueueStatus.Added || 
            //                                                          x.Status == (long)OMQueueStatus.Running))
            //    .Select(x => x.StartDate)
            //    .OrderByDescending(x => x.StartDate)
            //    .SetPackageSize(1)
            //    .ExecuteFirstOrDefault();

            List<OMSettings> settings = OMSettings.Where(x => true)
                                                  .Select(x => x.SettingValue)
                                                  .OrderBy(x => x.Id)
                                                  .Execute();
            OMCoreObject obj = OMCoreObject.Where(x => true).Select(x => x.ParserTime).OrderByDescending(x => x.ParserTime).ExecuteFirstOrDefault();
            if(obj != null) Console.WriteLine("DateTime ========> " + obj.ParserTime);
            InitialTimeTemplate = settings.ElementAt(1).SettingValue;
            LastSuccesfulUpdateDate = obj == null ? DateTime.ParseExact(settings.ElementAt(0).SettingValue, InitialTimeTemplate, CultureInfo.InvariantCulture) : obj.ParserTime.GetValueOrDefault();
            Login = settings.ElementAt(2).SettingValue;
            Token = settings.ElementAt(3).SettingValue;
            TimeDelta = int.Parse(settings.ElementAt(4).SettingValue);
            Link = settings.ElementAt(5).SettingValue;
            DealIds = settings.ElementAt(6).SettingValue.Split(",").Select(x => int.Parse(x.Trim())).ToList();
            RegionIDs = settings.ElementAt(7).SettingValue.Split(",").Select(x => int.Parse(x.Trim())).ToList();
            Template = settings.ElementAt(8).SettingValue;
        }

        public void UpdateLastSuccesfulUpdateDate()
        {
            OMSettings settings = new OMSettings{ Id = 1, SettingValue = DateTime.Now.Date.ToString(InitialTimeTemplate) };
            settings.Save();
        }

        public override string ToString() => $"Дата последнего успешного обновления: {LastSuccesfulUpdateDate}\nЛогин: {Login}\nТокен: {Token}\nШаг в минутах: {TimeDelta}\nШаблон ссылки: {Link}" + 
                                             $"\nТипы сделки: [{string.Join(",", DealIds)}]\nИдентификаторы регионов: [{string.Join(",", RegionIDs)}]\nШаблон времени: {Template}";
        

    }
}
