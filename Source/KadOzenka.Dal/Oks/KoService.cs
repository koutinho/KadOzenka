using System.Collections.Generic;
using KadOzenka.Dal.Tours;
using ObjectModel.Core.Register;

namespace KadOzenka.Dal.Oks
{
    public class KoService
    {
        public TourService TourService { get; set; }

        private static Dictionary<long, TourRegisters> _tourRegisters;
        private const long Oks2016RegisterId = 252;
        private const long Zu2016RegisterId = 253;
        private const long Oks2018RegisterId = 250;
        private const long Zu2018RegisterId = 251;

        public KoService()
        {
            TourService = new TourService();

            if (_tourRegisters != null)
                return;

            var year2016Id = TourService.GetTourByYear(2016).Id;
            var year2018Id = TourService.GetTourByYear(2018).Id;

            _tourRegisters = new Dictionary<long, TourRegisters>
            {
                {year2016Id, new TourRegisters(Oks2016RegisterId, Zu2016RegisterId)},
                {year2018Id, new TourRegisters(Oks2018RegisterId, Zu2018RegisterId)}
            };
        }

        public List<OMAttribute> GetKoAttributes(long tourId, ObjectType objectType)
        {
            if (!_tourRegisters.TryGetValue(tourId, out var registersIds))
               return new List<OMAttribute>();

            var registerId = objectType == ObjectType.Oks ? registersIds.OksId : registersIds.ZuId;

            return OMAttribute.Where(x => x.RegisterId == registerId).OrderBy(x => x.Name).SelectAll().Execute();
        }

        
        #region Support

        private class TourRegisters
        {
            public long OksId { get; }
            public long ZuId { get; }

            public TourRegisters(long oksId, long zuId)
            {
                OksId = oksId;
                ZuId = zuId;
            }
        }

        #endregion
    }
}
