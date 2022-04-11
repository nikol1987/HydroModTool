using HydroModTools.Client.Wpf.ControlModels;
using HydroModTools.Client.Wpf.DI;
using ReactiveUI;
using System.Windows.Controls;
using ReactiveMarbles.ObservableEvents;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Disposables;
using System;
using HydroModTools.Client.Wpf.Structs;
using System.Windows;
using System.Linq;

namespace HydroModTools.Client.Wpf.Controls
{
    internal partial class ProjectsTabControl : UserControl, IViewFor<ProjectsTabControlModel>
    {
        public ProjectsTabControl()
        {
            ViewModel = WpfFactory.CreateViewModel<ProjectsTabControlModel>();

            InitializeComponent();

            this.WhenActivated(d => {
                LaunchGameBtn
                    .Events()
                    .Click
                    .Select(e => Unit.Default)
                    .InvokeCommand(ViewModel.StartGameCommand)
                    .DisposeWith(d);

                GameVersionSelectorCombo
                    .Events()
                    .SelectionChanged
                    .Select(e => e.Source as ComboBox)
                    .Select(c => c.SelectedIndex)
                    .InvokeCommand(ViewModel.SetGameVersionCommand)
                    .DisposeWith(d);

                ViewModel
                    .WhenAnyValue(vm => vm.SelectedGame)
                    .Select(gameVersion => (int)gameVersion)
                    .BindTo(this, v => v.GameVersionSelectorCombo.SelectedIndex)
                    .DisposeWith(d);

                ProjectsList
                    .Events()
                    .SelectionChanged
                    .Select(e => e.Source as ListBox)
                    .Select(listBox => listBox.SelectedItem)
                    .Subscribe(item => {
                        MessageBox.Show(((ListItem<Guid, string>)item).Key.ToString());
                    })
                    .DisposeWith(d);

                ViewModel
                    .WhenAnyValue(vm => vm.ProjectList)
                    .Select(list => list
                        .ToList()
                        .Select(project => new ListItem<Guid, string>(project.Id, project.Name)))
                    .SubscribeOn(Dispatcher)
                    .Subscribe(items => {
                        ProjectsList.ItemsSource = items;
                        if (items.Count() > 0)
                        {
                            ProjectsList.SelectedIndex = 0;
                        }
                    })
                    .DisposeWith(d);
            });
        }

        object? IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (ProjectsTabControlModel)value!;
        }

        public ProjectsTabControlModel? ViewModel { get; set; }
    }
}
