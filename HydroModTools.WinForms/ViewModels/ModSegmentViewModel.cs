using HydroModTools.DataAccess.Contracts.Models;
using HydroModTools.DataAccess.Contracts.Services;
using ReactiveUI;
using System.Reactive;
using System.Windows.Forms;

namespace HydroModTools.WinForms.ViewModels
{
    public sealed class ModSegmentViewModel : ReactiveObject
    {
        private readonly IBridgepourService _bridgepourService;

        public ModSegmentViewModel(BridgepourModModel bridgepourModModel, IBridgepourService bridgepourService)
        {
            _bridgepourModModel = bridgepourModModel;
            _bridgepourService = bridgepourService;
            DownloadModCommand = ReactiveCommand.Create(DownloadMod);
            RemoveModCommand = ReactiveCommand.Create(RemoveMod);
        }

        private bool _canDownload = true;
        internal bool CanDownload
        {
            get => _canDownload;
            set {
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
            if (!CanDownload)
            {
                return;
            }

            CanDownload = false;
            await _bridgepourService.DownloadMod(_bridgepourModModel.Url);
            MessageBox.Show($"Installed mod '{BridgepourModModel.Name}'", "Mod installed", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        internal ReactiveCommand<Unit, Unit> RemoveModCommand;
        private async void RemoveMod()
        {
            if (CanDownload)
            {
                return;
            }

            await _bridgepourService.DeleteMod(_bridgepourModModel.Url);
            MessageBox.Show($"Removed mod '{BridgepourModModel.Name}'", "Mod removed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            CanDownload = true;
        }
    }
}
