using KadOzenka.Web.Models.MarketObject;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using ObjectModel.Market;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace KadOzenka.Web.SignalR.AnalogCheck
{

    public class ActivateDistrictsRegionsZones : Hub
    {
        private const string CHECK_HISTORY_DEFAULT = "Присвоение округов, районов, зон ещё не было запущено";

        private static ButtonState _button_lock = ButtonState.Avaliable;
        private static object _lockObject = new object();
        private static decimal _current_progress = 0;

        private string GetCheckingHistory()
        {
            int historyCount = OMDuplicatesHistory
                .Where(x => x.MarketSegment == "Присвоение округов, районов, зон")
                .SelectAll()
                .ExecuteCount();
            if (historyCount == 0) return CHECK_HISTORY_DEFAULT;
            else
            {
                OMDuplicatesHistory history = OMDuplicatesHistory
                    .Where(x => x.MarketSegment == "Присвоение округов, районов, зон")
                    .Select(x => new { x.CheckDate, x.CommonCount, x.InProgressCount, x.DuplicateObjects })
                    .OrderByDescending(x => x.CheckDate)
                    .ExecuteFirstOrDefault();
                return $"Последняя проверка: {history.CheckDate}. Всего проверено: {history.CommonCount}, корректно: {history.InProgressCount}, без зон: {history.DuplicateObjects}";
            }
        }

        private void SetCheckingHistory(DateTime? creationDate, long? commonCount, long? correctCount, long? errorCount)
        {
            OMDuplicatesHistory history = new OMDuplicatesHistory();
            history.CheckDate = creationDate;
            history.MarketSegment = "Присвоение округов, районов, зон";
            history.CommonCount = commonCount;
            history.InProgressCount = correctCount;
            history.DuplicateObjects = errorCount;
            history.Save();
        }

        private object FormCurrentWidgetState()
        {
            return new
            {
                buttonState = _button_lock,
                currentProgress = _current_progress,
                checkHistory = GetCheckingHistory()
            };
        }

        private async Task LoadButton()
        {
            _button_lock = ButtonState.Loading;
            await BroadcastCurrentWidgetState();
        }

        private async Task UnlockButton()
        {
            _button_lock = ButtonState.Avaliable;
            await BroadcastCurrentWidgetState();
        }

        private async Task LockButton()
        {
            _button_lock = ButtonState.Locked;
            await BroadcastCurrentWidgetState();
        }

        ///// <summary>
        ///// Присвоить округа, районы, зоны
        ///// </summary>
        ///// <returns></returns>
        //public async Task InitializeDistrictsRegionsZonesCheck()
        //{
        //    await LoadButton();
        //    lock (_lockObject) { new DistrictsRegionsZonesJoiner(RefreshCurrentProgress, LockButton, SetCheckingHistory).ManageData(); }
        //    await UnlockButton();
        //}

        public async Task GetCurrentWidgetState() => await Clients.Caller.SendAsync("SetCurrentWidgetState", JsonConvert.SerializeObject(FormCurrentWidgetState()));

        public async Task BroadcastCurrentWidgetState() => await Clients.All.SendAsync("SetCurrentWidgetState", JsonConvert.SerializeObject(FormCurrentWidgetState()));

        public async Task RefreshCurrentProgress(decimal currentProgress)
        {
            _current_progress = currentProgress;
            await Clients.All.SendAsync("SetCurrentProgress", currentProgress.ToString("0.##", new CultureInfo("en-US")));
        }
    }

}
