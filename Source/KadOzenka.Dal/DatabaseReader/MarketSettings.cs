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
        public int DealId { get; set; }
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
            InitialTimeTemplate = settings.ElementAt(1).SettingValue;
            LastSuccesfulUpdateDate = DateTime.ParseExact(settings.ElementAt(0).SettingValue, InitialTimeTemplate, CultureInfo.InvariantCulture);
            Login = settings.ElementAt(2).SettingValue;
            Token = settings.ElementAt(3).SettingValue;
            TimeDelta = int.Parse(settings.ElementAt(4).SettingValue);
            Link = settings.ElementAt(5).SettingValue;
            DealId = int.Parse(settings.ElementAt(6).SettingValue);
            RegionIDs = settings.ElementAt(7).SettingValue.Split(",").Select(x => int.Parse(x.Trim())).ToList();
            Template = settings.ElementAt(8).SettingValue;
            Console.WriteLine(this.ToString());
        }

        public void UpdateLastSuccesfulUpdateDate()
        {
            OMSettings settings = new OMSettings{ Id = 1, SettingValue = DateTime.Now.Date.ToString(InitialTimeTemplate) };
            settings.Save();
        }

        public override string ToString() => $"Дата последнего успешного обновления: {LastSuccesfulUpdateDate}\nЛогин: {Login}\nТокен: {Token}\nШаг в минутах: {TimeDelta}\nШаблон ссылки: {Link}" + 
                                             $"\nТип сделки: {DealId}\nИдентификаторы регионов: [{string.Join(",", RegionIDs)}]\nШаблон времени: {Template}";
        

    }
}
