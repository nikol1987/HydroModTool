using HydroModTools.DataAccess.Contracts.Services;
using ReactiveUI;
using System.Reactive;

namespace HydroModTools.Client.Wpf.ControlModels
{
    internal class ModSegmentControlModel : ReactiveObject
    {
        private readonly IBridgepourService _bridgepourService;

        public ModSegmentControlModel(IBridgepourService bridgepourService)
        {
            _bridgepourService = bridgepourService;

            DownloadModCommand = ReactiveCommand.Create(DownloadMod);
            RemoveModCommand = ReactiveCommand.Create(RemoveMod);
        }

        public string _author = string.Empty;
        public string Author
        {
            get => _author;
            set
            {
                this.RaiseAndSetIfChanged(ref _author, value);
            }
        }

        public string _description = string.Empty;
        public string Description
        {
            get => _description;
            set
            {
                this.RaiseAndSetIfChanged(ref _description, value);
            }
        }

        public string _name = string.Empty;
        public string Name
        {
            get => _name;
            set
            {
                this.RaiseAndSetIfChanged(ref _name, value);
            }
        }

        public string _ribbonColor = string.Empty;
        public string RibbonColor
        {
            get => _ribbonColor;
            set
            {
                this.RaiseAndSetIfChanged(ref _ribbonColor, value);
            }
        }

        public string _ribbonText = string.Empty;
        public string RibbonText
        {
            get => _ribbonText;
            set
            {
                this.RaiseAndSetIfChanged(ref _ribbonText, value);
            }
        }

        public string _status = string.Empty;
        public string Status
        {
            get => _status;
            set
            {
                this.RaiseAndSetIfChanged(ref _status, value);
            }
        }

        public string _url = string.Empty;
        public string Url
        {
            get => _url;
            set
            {
                this.RaiseAndSetIfChanged(ref _url, value);
            }
        }

        public bool _canDownload = false;
        public bool CanDownload
        {
            get => _canDownload;
            set
            {
                this.RaiseAndSetIfChanged(ref _canDownload, value);
            }
        }

        public bool _canRemove = false;
        public bool CanRemove
        {
            get => _canDownload;
            set
            {
                this.RaiseAndSetIfChanged(ref _canRemove, value);
            }
        }

        private bool _lockedButtons = false;
        internal bool LockedButtons
        {
            get => _lockedButtons;
            set
            {
                this.RaiseAndSetIfChanged(ref _lockedButtons, value);
            }
        }

        internal ReactiveCommand<Unit, Unit> DownloadModCommand;
        private async void DownloadMod()
        {
            if (!CanDownload || LockedButtons)
            {
                return;
            }

            LockedButtons = true;

            await _bridgepourService.DownloadMod(Url);

            LockedButtons = false;
            CanDownload = false;
            CanRemove = true;
        }

        internal ReactiveCommand<Unit, Unit> RemoveModCommand;
        private async void RemoveMod()
        {
            if (CanDownload || LockedButtons)
            {
                return;
            }

            LockedButtons = true;

            await _bridgepourService.DeleteMod(Url);
            CanDownload = true;
            CanRemove = false;
            LockedButtons = false;
        }

        public void SetModInfo(
            string author,
            string description,
            string name,
            string ribbonColor,
            string ribbonText,
            string status,
            string url)
        {
            Author = author;
            Description = description;
            Name = name;
            RibbonColor = ribbonColor;
            RibbonText = ribbonText;
            Status = status;
            Url = url;
        }
    }
}
