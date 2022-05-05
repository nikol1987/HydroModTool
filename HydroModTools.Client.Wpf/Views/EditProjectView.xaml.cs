using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using HydroModTools.Client.Wpf.DI;
using HydroModTools.Client.Wpf.Enums;
using HydroModTools.Client.Wpf.ViewModels;
using ReactiveMarbles.ObservableEvents;
using ReactiveUI;

namespace HydroModTools.Client.Wpf.Views
{
    internal partial class EditProjectView : IViewFor<EditProjectViewModel>
    {
        public EditProjectView()
        {
            ViewModel = WpfFactory.CreateViewModel<EditProjectViewModel>();
            ViewModel.OnRequestClose += () =>
            {
                Dispatcher.Invoke(Close);
            };
            
            InitializeComponent();

            this.WhenActivated(d =>
            {
                ViewModel
                    .WhenAnyValue(vm => vm.ProjectName)
                    .Subscribe(projectName =>
                    {
                        var isNewProject = ViewModel.ProjectId == Guid.Empty;

                        Title = $"{(isNewProject ? "New" : "Edit")} Project {projectName}";
                    })
                    .DisposeWith(d);
                
                this
                    .Bind(ViewModel, vm => vm.ProjectName, v => v.ProjectNameTextBox.Text);
                this
                    .Bind(ViewModel, vm => vm.ProjectIndex, v => v.ModIndexTextBox.Text);
                this
                    .Bind(ViewModel, vm => vm.CookedAssetsDir, v => v.CookedAssetsDirTextBox.Text);
                this
                    .Bind(ViewModel, vm => vm.DistDir, v => v.OutputDirTextBox.Text);

                CancelBtn
                    .Events()
                    .Click
                    .Subscribe(e =>
                    {
                        Close();
                    })
                    .DisposeWith(d);

                SaveBtn
                    .Events()
                    .Click
                    .Select(e => Unit.Default)
                    .InvokeCommand(ViewModel.SaveProjectInfoCommand)
                    .DisposeWith(d);
                
                SelectCookedAssetsDirBtn
                    .Events()
                    .Click
                    .Select(e => SelectDirMode.SelectCookedAssetsDir)
                    .InvokeCommand(ViewModel.SelectAssetsDirCommand)
                    .DisposeWith(d);
                
                SelectOutputDirBtn
                    .Events()
                    .Click
                    .Select(e => SelectDirMode.SelectOutputDir)
                    .InvokeCommand(ViewModel.SelectAssetsDirCommand)
                    .DisposeWith(d);
            });
        }

        public void SetProject(Guid projectId)
        {
            ViewModel.SetProject(projectId);
        }
        
        object? IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (EditProjectViewModel)value!;
        }

        public EditProjectViewModel? ViewModel { get; set; }
    }
}