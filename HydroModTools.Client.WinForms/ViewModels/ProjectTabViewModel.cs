using ReactiveUI;

namespace HydroModTools.Client.WinForms.ViewModels
{
    public sealed class ProjectTabViewModel : ReactiveObject
    {
        public ProjectTabViewModel()
        {
            Tab1Title = "Projects";
            Tab2Title = "GUIDs";
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
    }
}
