//using ObjectModel.Market;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using MarketPlaceBusiness;
//using MarketPlaceBusiness.Interfaces;

//namespace KadOzenka.Web.Models.MarketObject
//{
//	public class CoordinatesJoiner
//    {
//        private Func<decimal, System.Threading.Tasks.Task> _progress_delegate;
//        private Func<System.Threading.Tasks.Task> _button_state_delegate;
//        private Action<DateTime?, long?, long?, long?> _checking_history_delegate;
//        private IMarketObjectService MarketObjectService { get; }

//        public CoordinatesJoiner(Func<decimal, System.Threading.Tasks.Task> progressDelegate, Func<System.Threading.Tasks.Task> buttonStateDelegate, Action<DateTime?, long?, long?, long?> checkingHistoryDelegate) 
//        {
//            _progress_delegate = progressDelegate;
//            _button_state_delegate = buttonStateDelegate;
//            _checking_history_delegate = checkingHistoryDelegate;
//            MarketObjectService = new MarketObjectService();
//        }

//        public void ManageData(string key)
//        {
//            var AllObjects = MarketObjectService.GetObjectsToAssignCoordinates();
//            int goods = 0, errors = 0, current = 0, allCount = AllObjects.Count;
//            _button_state_delegate.Invoke();
//            AllObjects.ForEach(x =>
//            {
//                try
//                {
//                    OMYandexAddress address = 
//                        new Dal.JSONParser.YandexGeocoder().ParseYandexAddress(
//                            new Dal.WebRequest.YandexGeocoder().GetDataByAddress(
//                                Dal.JSONParser.YandexGeocoder.getFormalizedAddress(x.Address), key)
//                            );
//                    //x.Lng = address.Lng;
//                    //x.Lat = address.Lat;
//                    //x.ProcessType_Code = ObjectModel.Directory.ProcessStep.Dealed;
//                    goods++;
//                }
//                catch (Exception)
//                {
//                    //x.ProcessType_Code = ObjectModel.Directory.ProcessStep.Excluded;
//                    //x.ExclusionStatus_Code = ObjectModel.Directory.ExclusionStatus.NoAddress;
//                    errors++;
//                }
//                //x.Save();
//                _progress_delegate.Invoke((decimal)++current / allCount * 100);
//            });
//            _checking_history_delegate.Invoke(DateTime.Now, allCount, goods, errors);
//        }
//    }
//}
