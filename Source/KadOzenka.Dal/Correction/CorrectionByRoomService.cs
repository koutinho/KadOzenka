using System;
using System.Collections.Generic;
using System.Linq;
using ObjectModel.Directory;
using ObjectModel.Market;

namespace KadOzenka.Dal.Correction
{
    public class CorrectionByRoomService
    {
        private const int PrecisionForPrice = 2;
        private const int PrecisionForCoefficients = 4;

        public void UpdateMarketObjectsPrice()
        {
            var numberOfRooms = new long?[] { 1, 2, 3 };
            var oneRoomCoefficients = new List<decimal>();
            var threeRoomsCoefficients = new List<decimal>();

            var groupedObjects = OMCoreObject.Where(x =>
                    x.BuildingCadastralNumber != null &&
                    x.RoomsCount != null && numberOfRooms.Contains(x.RoomsCount) &&
                    x.DealType_Code == DealType.SaleSuggestion || x.DealType_Code == DealType.SaleDeal)
                .SelectAll(false)
                //TODO remove
                .SetPackageSize(10000).SetPackageIndex(0)
                .Execute()
                .GroupBy(x => new {x.BuildingCadastralNumber, x.PropertyMarketSegment_Code}).ToList();
            
            groupedObjects.ForEach(group =>
            {
                var objectsInBuilding = group.ToList();

                if (IsBuildingContainAllRoomsTypes(objectsInBuilding))
                {
                    var oneRoomAveragePricePerMeter = GetAveragePricePerMeter(objectsInBuilding, 1);
                    var twoRoomsAveragePricePerMeter = GetAveragePricePerMeter(objectsInBuilding, 2);
                    var threeRoomsAveragePricePerMeter = GetAveragePricePerMeter(objectsInBuilding, 3);

                    var oneRoomCoefficient = Math.Round(twoRoomsAveragePricePerMeter / oneRoomAveragePricePerMeter, PrecisionForCoefficients);
                    var threeRoomsCoefficient = Math.Round(twoRoomsAveragePricePerMeter / threeRoomsAveragePricePerMeter, PrecisionForCoefficients);

                    oneRoomCoefficients.Add(oneRoomCoefficient);
                    threeRoomsCoefficients.Add(threeRoomsCoefficient);

                    SaveHistory(group.Key.BuildingCadastralNumber, group.Key.PropertyMarketSegment_Code, oneRoomCoefficient, threeRoomsCoefficient);
                }
            });

            CalculatePriceAfterCorrectionByRooms(oneRoomCoefficients, threeRoomsCoefficients);
        }


        #region Support Methods

        private bool IsBuildingContainAllRoomsTypes(List<OMCoreObject> objectsInBuilding)
        {
            bool haveOneRoomApartment = false, haveTwoRoomsApartment = false, haveThreeRoomsApartment = false;

            objectsInBuilding.ForEach(obj =>
            {
                switch (obj.RoomsCount)
                {
                    case 1:
                        haveOneRoomApartment = true;
                        break;
                    case 2:
                        haveTwoRoomsApartment = true;
                        break;
                    case 3:
                        haveThreeRoomsApartment = true;
                        break;
                }
            });

            return haveOneRoomApartment && haveTwoRoomsApartment && haveThreeRoomsApartment;
        }

        private void CalculatePriceAfterCorrectionByRooms(List<decimal> oneRoomCoefficients, List<decimal> threeRoomsCoefficient)
        {
            var oneRoomAverageCoefficient = oneRoomCoefficients.Average();
            var threeRoomAverageCoefficient = threeRoomsCoefficient.Average();

            var objects = OMCoreObject.Where(x => x.RoomsCount == 1 || x.RoomsCount == 3).SelectAll().Execute();
            objects.ForEach(x =>
            {
                var coefficient = 0m;
                switch (x.RoomsCount)
                {
                    case 1:
                        coefficient = oneRoomAverageCoefficient;
                        break;
                    case 3:
                        coefficient = threeRoomAverageCoefficient;
                        break;
                }

                x.PriceAfterCorrectionByRooms = Math.Round(x.Price.GetValueOrDefault() * coefficient, PrecisionForPrice);
                x.Save();
            });
        }

        private decimal GetAveragePricePerMeter(IEnumerable<OMCoreObject> objects, int numberOfRooms)
        {
            return objects.Where(x => x.RoomsCount == numberOfRooms).Select(x => x.PricePerMeter.GetValueOrDefault()).Average();
        }

        private void SaveHistory(string buildingCadastralNumber, MarketSegment segment, decimal oneRoomCoefficient, decimal threeRoomsCoefficient)
        {
            new OMPriceCorrectionByRoomsHistory
            {
                BuildingCadastralNumber = buildingCadastralNumber,
                MarketSegment_Code = segment,
                ChangingDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1),
                OneRoomCoefficient = oneRoomCoefficient,
                ThreeRoomsCoefficient = threeRoomsCoefficient
            }.Save();
        }

        #endregion
    }
}
