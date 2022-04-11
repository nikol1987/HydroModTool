using HydroModTools.DataAccess.Contracts.Models;
using HydroModTools.DataAccess.Contracts.Services;
using ReactiveUI;
using System;
using System.Reactive;

namespace HydroModTools.Client.WinForms.ViewModels
{
    public sealed class ModSegmentViewModel : ReactiveObject
    {
        private readonly IBridgepourService _bridgepourService;

        public event Action OnModDownload;

        public ModSegmentViewModel(BridgepourModModel bridgepourModModel, IBridgepourService bridgepourService)
        {
            _bridgepourModModel = bridgepourModModel;
            _bridgepourService = bridgepourService;
            DownloadModCommand = ReactiveCommand.Create(DownloadMod);
            RemoveModCommand = ReactiveCommand.Create(RemoveMod);
        }

        private bool _lockedButtons = false;
        internal bool LockedButtons
        {
            get => _lockedButtons;
            set
            {
                this.RaiseAndSetIfChanged(ref _lockedButtons, value);
                this.RaisePropertyChanged("LockedButtons");
            }
        }

        private bool _canDownload = true;
        internal bool CanDownload
        {
            get => _canDownload;
            set
            {
                this.RaiseAndSetIfChanged(ref _canDownload, value);
                this.RaisePropertyChanged("CanRemove");
            }
        }

        internal bool CanRemove
        {
            get => !_canDownload;
        }

        private BridgepourModModel _bridgepourModModel;
        internal BridgepourModModel BridgepourModModel
        {
            get => _bridgepourModModel;
            set => this.RaiseAndSetIfChanged(ref _bridgepourModModel, value);
        }

        internal ReactiveCommand<Unit, Unit> DownloadModCommand;
        private async void DownloadMod()
        {
            if (!CanDownload || LockedButtons)
            {
                return;
            }

            LockedButtons = true;

            await _bridgepourService.DownloadMod(_bridgepourModModel.Url);

            if (OnModDownload != null)
            {
                OnModDownload.Invoke();
            }

            LockedButtons = false;
            CanDownload = false;
        }

        internal ReactiveCommand<Unit, Unit> RemoveModCommand;
        private async void RemoveMod()
        {
            if (CanDownload || LockedButtons)
            {
                return;
            }

            LockedButtons = true;

            await _bridgepourService.DeleteMod(_bridgepourModModel.Url);
            CanDownload = true;
            LockedButtons = false;
        }
    }
}
