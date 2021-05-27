using ReactiveUI;
using System.Windows.Forms;

namespace HydroModTools.WinForms.Abstractions
{
    public partial class ViewBase<TViewModel> : UserControl, IViewFor<TViewModel> where TViewModel : ReactiveObject
    {
        public ViewBase(TViewModel viewModel)
        {
            InitializeComponent();

            ViewModel = viewModel;
        }

        public TViewModel ViewModel { get; set; }

        object IViewFor.ViewModel { get => ViewModel; set => ViewModel = (TViewModel)value; }
    }
}
