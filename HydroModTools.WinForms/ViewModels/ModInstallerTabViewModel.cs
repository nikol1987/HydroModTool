using HydroModTools.DataAccess.Contracts.Models;
using HydroModTools.DataAccess.Contracts.Services;
using ReactiveUI;
using System.Collections.Generic;
using System.Reactive;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HydroModTools.WinForms.ViewModels
{
    public sealed class ModInstallerTabViewModel : ReactiveObject
    {
        private readonly IBridgepourService _bridgepourService;
        private System.Timers.Timer _timer;

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

        private IReadOnlyCollection<string> _loadedMods = new List<string>();
        internal IReadOnlyCollection<string> LoadedMods
        {
            get => _loadedMods;
            set => this.RaiseAndSetIfChanged(ref _loadedMods, value);
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
                case "clearMods":

                    await _bridgepourService.ClearMods();
                    MessageBox.Show("Cleared mods folder", "Mods Removed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await RefreshModList(true);
                    break;
            }
        }

        public async Task RefreshModList(bool bypassTime)
        {
            if (LockedRefresh.Locked && bypassTime == false)
            {
                return;
            }

            ModList =  await _bridgepourService.GetModList();
            LockedRefresh = new LockedRefreshModel(0, true);
            LoadedMods = await _bridgepourService.LoadedMods();

            var bypassed = false;

            if (_timer == null)
            {
                _timer = new System.Timers.Timer()
                {
                    Interval = 1000,
                    Enabled = true
                };

                _timer.Elapsed += (sender, e) => {
                    if (LockedRefresh.Seconds >= 10)
                    {
                        _timer.Stop();
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
                    _timer.Start();
                };
                _timer.Start();
            } else
            {
                if (bypassTime && !bypassed)
                {
                    _timer.Stop();
                    _timer.Start();
                    bypassed = true;
                }
            }

           
            
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
