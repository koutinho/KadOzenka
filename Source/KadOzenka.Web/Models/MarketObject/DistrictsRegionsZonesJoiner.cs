//using ObjectModel.Market;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading;
//using System.Threading.Tasks;
//using MarketPlaceBusiness;
//using MarketPlaceBusiness.Interfaces;

//namespace KadOzenka.Web.Models.MarketObject
//{
//    /// <summary>
//    /// Присвоить округа, районы, зоны
//    /// </summary>
//    public class DistrictsRegionsZonesJoiner
//    {
//        private Func<decimal, System.Threading.Tasks.Task> _progress_delegate;
//        private Func<System.Threading.Tasks.Task> _button_state_delegate;
//        private Action<DateTime?, long?, long?, long?> _checking_history_delegate;
//        private IMarketObjectService MarketObjectService { get; }

//        public DistrictsRegionsZonesJoiner(Func<decimal, System.Threading.Tasks.Task> progressDelegate, Func<System.Threading.Tasks.Task> buttonStateDelegate, Action<DateTime?, long?, long?, long?> checkingHistoryDelegate)
//        {
//            _progress_delegate = progressDelegate;
//            _button_state_delegate = buttonStateDelegate;
//            _checking_history_delegate = checkingHistoryDelegate;
//            MarketObjectService = new MarketObjectService();
//        }

//        public void ManageData()
//        {
//            var AllObjects = MarketObjectService.GetObjectsToAssignDistrictsRegionsAndZones();
//            List<OMQuartalDictionary> QuartalDictionary = OMQuartalDictionary.Where(x => true).SelectAll().Execute().ToList();
//            int allCount = AllObjects.Count, current = 0, correct = 0, error = 0;
//            _button_state_delegate.Invoke();
//            AllObjects.ForEach(x =>
//            {
//                OMQuartalDictionary dictionary = QuartalDictionary.Where(y => x.CadastralQuartal != null && x.CadastralQuartal.Equals(y.CadastralQuartal)).FirstOrDefault();
//                if (dictionary != null)
//                {
//                    //x.District_Code = dictionary.District_Code;
//                    //x.Neighborhood_Code = dictionary.Region_Code;
//                    //x.ZoneRegion = dictionary.ZoneRegion;
//                    //x.Zone = dictionary.Zone;
//                    //x.Save();
//                    correct++;
//                }
//                else error++;
//                _progress_delegate.Invoke((decimal)++current / allCount * 100);
//                //WorkerCommon.SetProgress(processQueue, (int)((double)++current / allCount * 100));
//            });
//            _checking_history_delegate.Invoke(DateTime.Now, allCount, correct, error);
//        }

//    }

//}
