
using HydroneerStager.WinForms.Views;
using ReactiveUI;
using System.Reactive;

namespace HydroneerStager.WinForms.ViewModels
{
    public sealed class SpashViewModel : ReactiveObject
    {
        private string _applicationTitle;

        private ApplicationView _applicationView;

        public SpashViewModel(
            ApplicationView applicationView)
        {
            _applicationView = applicationView;

            ApplicationTitle = "Where are you looking at?";
            ShowAppCommand = ReactiveCommand.Create(ShowApp);
        }

        public string ApplicationTitle
        {
            get => _applicationTitle;
            set => this.RaiseAndSetIfChanged(ref _applicationTitle, value);
        }


        public ReactiveCommand<Unit, Unit> ShowAppCommand { get; }
        private void ShowApp()
        {
            _applicationView.Show();
        }
    }
}
