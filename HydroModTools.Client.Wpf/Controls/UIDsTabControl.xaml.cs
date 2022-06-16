using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Windows.Controls;
using HydroModTools.Client.Wpf.ControlModels;
using HydroModTools.Client.Wpf.DI;
using HydroModTools.Client.Wpf.Models;
using ReactiveUI;

namespace HydroModTools.Client.Wpf.Controls
{
    internal partial class UIDsTabControl : IViewFor<UIDsTabControlModel>
    {
        public UIDsTabControl()
        {
            ViewModel = WpfFactory.CreateViewModel<UIDsTabControlModel>();
            
            InitializeComponent();

            this.WhenActivated(d =>
            {
                SaveUIDsBtn
                    .Events()
                    .Click
                    .Select(_ => Unit.Default)
                    .InvokeCommand(ViewModel.SaveUIDsCommand)
                    .DisposeWith(d);
                
                AddUIDBtn
                    .Events()
                    .Click
                    .Select(_ => Unit.Default)
                    .InvokeCommand(ViewModel.AddUIDCommand)
                    .DisposeWith(d);
                
                RemoveUIDBtn
                    .Events()
                    .Click
                    .Select(_ => Unit.Default)
                    .InvokeCommand(ViewModel.RemoveUIDCommand)
                    .DisposeWith(d);
                    
            });

            GuidDataGrid.ItemsSource = ViewModel.UIDList;
            GuidDataGrid.SelectionChanged += (_, _) =>
            {
                ViewModel.SelectedItem = GuidDataGrid.SelectedItem as UIDTableRowItem;
            };
        }

        object? IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (UIDsTabControlModel)value!;
        }

        public UIDsTabControlModel? ViewModel { get; set; }
    }
}