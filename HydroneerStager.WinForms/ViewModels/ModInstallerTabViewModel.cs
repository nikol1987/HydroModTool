using HydroModTools.DataAccess.Contracts.Models;
using HydroModTools.DataAccess.Contracts.Services;
using ReactiveUI;
using System.Collections.Generic;
using System.Reactive;
using System.Threading.Tasks;
using System.Timers;

namespace HydroModTools.WinForms.ViewModels
{
    public sealed class ModInstallerTabViewModel : ReactiveObject
    {
        private readonly IBridgepourService _bridgepourService;

        public ModInstallerTabViewModel(IBridgepourService bridgepourService)
        {
            _bridgepourService = bridgepourService;

            RefreshModList(true);

            ExecuteStripMenuCommand = ReactiveCommand.Create<string>(ExecuteStripMenu);
        }

        private LockedRefreshModel _lockedRefresh = new LockedRefreshModel(0, false);
        internal LockedRefreshModel LockedRefresh
        {
            get => _lockedRefresh;
            set => this.RaiseAndSetIfChanged(ref _lockedRefresh, value);
        }

        private IReadOnlyCollection<BridgepourModModel> _modList = new List<BridgepourModModel>();
        internal IReadOnlyCollection<BridgepourModModel> ModList
        {
            get => _modList;
            set => this.RaiseAndSetIfChanged(ref _modList, value);
        }

        internal ReactiveCommand<string, Unit> ExecuteStripMenuCommand;
        private async void ExecuteStripMenu(string stripItemName)
        {
            switch (stripItemName)
            {
                case "refreshMods":
                    if (LockedRefresh.Locked)
                    {
                        return;
                    }

                    await RefreshModList(false);
                    break;
            }
        }

        public async Task RefreshModList(bool bypassTime)
        {
            if (LockedRefresh.Locked && bypassTime == true)
            {
                return;
            }

            ModList =  await _bridgepourService.GetModList();
            LockedRefresh = new LockedRefreshModel(0, true);

            var bypassed = false;
            
            var timer = new Timer() {
                Interval = 1000,
                Enabled = true
            };
            timer.Elapsed += (sender, e) => {
                if (LockedRefresh.Seconds >= 10)
                {
                    timer.Stop();
                    LockedRefresh = new LockedRefreshModel(0, false);
                    return;
                }

                var seconds = LockedRefresh.Seconds + 1;

                if (bypassTime && !bypassed && seconds > 0)
                {
                    seconds = 0;
                    bypassed = true;
                }

                LockedRefresh = new LockedRefreshModel(seconds, true);
                timer.Start();
            };
            timer.Start();
        }

        internal class LockedRefreshModel
        {
            public LockedRefreshModel(int seconds, bool locked)
            {
                Seconds = seconds;
                Locked = locked;
            }

            public int Seconds { get; }

            public bool Locked { get; }
        }
    }
}
