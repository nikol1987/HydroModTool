using HandyControl.Controls;
using HandyControl.Data;
using HydroModTools.DataAccess.Contracts.Models;
using HydroModTools.DataAccess.Contracts.Services;
using ReactiveUI;
using System.Collections.Generic;
using System.Reactive;
using System.Threading.Tasks;

namespace HydroModTools.Client.Wpf.ControlModels
{
    internal sealed class InstallModsTabControlModel : ReactiveObject
    {
        private readonly IBridgepourService _bridgepourService;

        public InstallModsTabControlModel(IBridgepourService bridgepourService)
        {
            _bridgepourService = bridgepourService;

            RefreshCommand = ReactiveCommand.Create(Refresh);
            ClearModsCommand = ReactiveCommand.Create(ClearMods);
            OpenModsFolderCommand = ReactiveCommand.Create(OpenModsFolder);

            UpdateModList();
        }

        public ReactiveCommand<Unit, Unit> RefreshCommand;
        private void Refresh()
        {
            if (SecondsTillRefresh > 0)
            {
                return;
            }

            UpdateModList();

            SecondsTillRefresh = Constants.RefreshTime;

            Task.Run(async () => {
                while (SecondsTillRefresh > 0)
                {
                    await Task.Delay(1000);
                    SecondsTillRefresh -= 1;
                }
            });
        }

        public ReactiveCommand<Unit, Unit> OpenModsFolderCommand;
        private void OpenModsFolder()
        {
            MessageBox.Show(new MessageBoxInfo()
            {
                Message = "Opening Mod Folder"
            });
        }

        public ReactiveCommand<Unit, Unit> ClearModsCommand;
        private void ClearMods()
        {
            MessageBox.Show(new MessageBoxInfo()
            {
                Message = "Clearing mods!"
            });
        }

        private IReadOnlyCollection<BridgepourModModel> _modList = new List<BridgepourModModel>();
        public IReadOnlyCollection<BridgepourModModel> ModList
        {
            get => _modList;
            set => this.RaiseAndSetIfChanged(ref _modList, value);
        }

        private int _secondsTillRefresh = 0;
        public int SecondsTillRefresh
        {
            get => _secondsTillRefresh;
            set => this.RaiseAndSetIfChanged(ref _secondsTillRefresh, value);
        }

        private void UpdateModList()
        {
            _bridgepourService
                .GetModList()
                .ContinueWith(modListTask =>
                {
                    ModList = modListTask.Result;
                });
        }
    }
}
