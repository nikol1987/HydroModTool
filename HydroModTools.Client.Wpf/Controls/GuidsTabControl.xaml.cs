using HydroModTools.Client.Wpf.ControlModels;
using HydroModTools.Client.Wpf.DI;
using ReactiveUI;
using System.Windows.Controls;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Disposables;
using System.Windows;
using HydroModTools.Client.Wpf.Models;

namespace HydroModTools.Client.Wpf.Controls
{
    internal partial class GuidsTabControl : IViewFor<GuidsTabControlModel>
    {
        public GuidsTabControl()
        {
            ViewModel = WpfFactory.CreateViewModel<GuidsTabControlModel>();
            
            InitializeComponent();

            this.WhenActivated(d =>
            {
                SaveGuidsBtn
                    .Events()
                    .Click
                    .Select(_ => Unit.Default)
                    .InvokeCommand(ViewModel.SaveGuidsCommand)
                    .DisposeWith(d);
                
                AddGuidBtn
                    .Events()
                    .Click
                    .Select(_ => Unit.Default)
                    .InvokeCommand(ViewModel.AddGuidCommand)
                    .DisposeWith(d);
                
                RemoveGuidBtn
                    .Events()
                    .Click
                    .Select(_ => Unit.Default)
                    .InvokeCommand(ViewModel.RemoveGuidCommand)
                    .DisposeWith(d);
                    
            });

            GuidDataGrid.ItemsSource = ViewModel.GuidList;
            GuidDataGrid.SelectionChanged += (_, _) =>
            {
                ViewModel.SelectedItem = GuidDataGrid.SelectedItem as GuidTableRowItem;
            };

            
        }

        object? IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (GuidsTabControlModel)value!;
        }

        public GuidsTabControlModel? ViewModel { get; set; }
    }
}