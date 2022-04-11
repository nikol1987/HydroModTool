using HandyControl.Controls;
using HydroModTools.Wpf.ViewModels;
using ReactiveUI;

namespace HydroModTools.Wpf.Views
{
    public partial class ApplicationView : Window, IViewFor<ApplicationViewModel>
    {
        public ApplicationView()
        {
            InitializeComponent();
        }

        object? IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (ApplicationViewModel)value!;
        }

        public ApplicationViewModel? ViewModel { get; set; }
    }
}
