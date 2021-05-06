using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using KadOzenka.Web.Models.MarketObject;
using System.Globalization;
using ObjectModel.Market;

namespace KadOzenka.Web.SignalR.AnalogCheck
{
	enum ButtonState
    {
        Avaliable, 
        Loading, 
        Locked
    }

    public class ActivateCoordinates : Hub
    {
        private const string KEY = "364a064a-ad89-403c-9e2b-abe8c3b8715b";
        private const string CHECK_HISTORY_DEFAULT = "Присвоение координат ещё не было запущено";

        private static ButtonState _button_lock = ButtonState.Avaliable;
        private static object _lockObject = new object();
        private static decimal _current_progress = 0;

        private string GetCheckingHistory()
        {
            int historyCount = OMDuplicatesHistory
                .Where(x => x.MarketSegment == "Присвоение координат")
                .SelectAll()
                .ExecuteCount();
            if (historyCount == 0) return CHECK_HISTORY_DEFAULT;
            else
            {
                OMDuplicatesHistory history = OMDuplicatesHistory
                    .Where(x => x.MarketSegment == "Присвоение координат")
                    .Select(x => new { x.CheckDate, x.CommonCount, x.InProgressCount, x.DuplicateObjects })
                    .OrderByDescending(x => x.CheckDate)
                    .ExecuteFirstOrDefault();
                return $"Последняя проверка проводилась: {history.CheckDate}. Всего объектов проверено: {history.CommonCount}";
            }
        }

        private void SetCheckingHistory(DateTime? creationDate, long? commonCount, long? correctCount, long? errorCount)
        {
            OMDuplicatesHistory history = new OMDuplicatesHistory();
            history.MarketSegment = "Присвоение координат";
            history.CheckDate = creationDate;
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

        /// <summary>
        /// Присвоение координат
        /// </summary>
        /// <returns></returns>
        public async Task InitializeCoordinatesCheck()
        {
            await LoadButton();
            lock (_lockObject) { new CoordinatesJoiner(RefreshCurrentProgress, LockButton, SetCheckingHistory).ManageData(KEY); }
            await UnlockButton();
        }

        public async Task GetCurrentWidgetState() => await Clients.Caller.SendAsync("SetCurrentWidgetState", JsonConvert.SerializeObject(FormCurrentWidgetState()));

        public async Task BroadcastCurrentWidgetState() => await Clients.All.SendAsync("SetCurrentWidgetState", JsonConvert.SerializeObject(FormCurrentWidgetState()));

        public async Task RefreshCurrentProgress(decimal currentProgress)
        {
            _current_progress = currentProgress;
            await Clients.All.SendAsync("SetCurrentProgress", currentProgress.ToString("0.##", new CultureInfo("en-US")));
        }
    }
}
