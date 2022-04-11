using ReactiveUI;

namespace HydroModTools.Client.WinForms.ViewModels
{
    public sealed class ApplicationViewModel : ReactiveObject
    {
        public ApplicationViewModel()
        {
            ApplicationTitle = "Hydroneer Modding Toolchain";
            Tab1Title = "Install Mods";
            Tab2Title = "Create Mods";
            Tab3Title = "About";
        }

        private string _applicationTitle;
        public string ApplicationTitle
        {
            get => _applicationTitle;
            set => this.RaiseAndSetIfChanged(ref _applicationTitle, value);
        }

        private string _tab1Title;
        public string Tab1Title
        {
            get => _tab1Title;
            set => this.RaiseAndSetIfChanged(ref _tab1Title, value);
        }

        private string _tab2Title;
        public string Tab2Title
        {
            get => _tab2Title;
            set => this.RaiseAndSetIfChanged(ref _tab2Title, value);
        }

        private string _tab3Title;
        public string Tab3Title
        {
            get => _tab3Title;
            set => this.RaiseAndSetIfChanged(ref _tab3Title, value);
        }
    }
}
