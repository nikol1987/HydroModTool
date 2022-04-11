using HydroModTools.Contracts.Services;
using ReactiveUI;

namespace HydroModTools.Client.WinForms.ViewModels
{
    public sealed class AboutTabViewModel : ReactiveObject
    {
        private readonly IAppService _appService;

        public AboutTabViewModel(IAppService appService)
        {
            _appService = appService;

            LoadAboutHtml();
        }

        private async void LoadAboutHtml()
        {
            AboutHtml = await _appService.LoadAboutHtml();
        }

        private string _aboutHtml = "Loading...";
        internal string AboutHtml
        {
            get => _aboutHtml;
            set => this.RaiseAndSetIfChanged(ref _aboutHtml, value);
        }
    }
}
