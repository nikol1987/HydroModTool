using HydroModTools.Contracts.Services;
using ReactiveUI;

namespace HydroModTools.Client.Wpf.ControlModels
{
    internal sealed class AboutTabControlModel : ReactiveObject
    {
        private readonly IAppService _appService;

        public AboutTabControlModel(IAppService appService)
        {
            _appService = appService;

            _appService
                .LoadAboutString()
                .ContinueWith(aboutHtmlTask => {
                    var aboutHtml = aboutHtmlTask.Result;

                    AboutMd = aboutHtml;
                });
        }

        private string _aboutMd = string.Empty;

        public string AboutMd
        {
            get => _aboutMd;
            set
            {
                this.RaiseAndSetIfChanged(ref _aboutMd, value);
            }
        }

    }
}
