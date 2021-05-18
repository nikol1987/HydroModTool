
using HydroneerStager.WinForms.Views;
using ReactiveUI;
using System.Reactive;

namespace HydroneerStager.WinForms.ViewModels
{
    public sealed class SpashViewModel : ReactiveObject
    {
        private ApplicationView _applicationView;

        public SpashViewModel(
            ApplicationView applicationView)
        {
            _applicationView = applicationView;

            ShowAppCommand = ReactiveCommand.Create(ShowApp);
        }

        public ReactiveCommand<Unit, Unit> ShowAppCommand { get; }
        private void ShowApp()
        {
            _applicationView.Show();
        }
    }
}
