using HydroModTools.Client.Wpf.ControlModels;
using HydroModTools.Client.Wpf.DI;
using ReactiveUI;
using System.Windows.Controls;
using ReactiveMarbles.ObservableEvents;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Disposables;
using System;
using System.Linq;
using HydroModTools.Client.Wpf.Structs;
using System.Windows;
using System.Windows.Input;
using System.Collections.Generic;
using HydroModTools.Client.Wpf.ExtendedControls;
using System.Windows.Media;
using DynamicData.Kernel;
using HydroModTools.Client.Wpf.ExtendedControls.TreeView;

namespace HydroModTools.Client.Wpf.Controls
{
    internal partial class ProjectsTabControl : UserControl, IViewFor<ProjectsTabControlModel>
    {
        public ProjectsTabControl()
        {
            ViewModel = WpfFactory.CreateViewModel<ProjectsTabControlModel>();
            ViewModel.OnAssetsUpdate += () =>
            {
                Dispatcher.Invoke(() =>
                {
                    RenderFiles(ViewModel.SelectedProject);
                });
            };
            
            InitializeComponent();

            this.WhenActivated(d => {
                //Refresh
                
                AddProjectBtn
                    .Events()
                    .Click
                    .Select(e => Unit.Default)
                    .InvokeCommand(ViewModel.AddProjectCommand)
                    .DisposeWith(d);
                
                //Stage
                //Package
                //Copy
                
                LaunchGameBtn
                    .Events()
                    .Click
                    .Select(e => Unit.Default)
                    .InvokeCommand(ViewModel.StartGameCommand)
                    .DisposeWith(d);
                
                //Express

                /*GameVersionSelectorCombo
                    .Events()
                    .SelectionChanged
                    .Select(e => e.Source as ComboBox)
                    .Select(c => c.SelectedIndex)
                    .InvokeCommand(ViewModel.SetGameVersionCommand)
                    .DisposeWith(d);*/

                /*ViewModel
                    .WhenAnyValue(vm => vm.SelectedGame)
                    .Select(gameVersion => (int)gameVersion)
                    .BindTo(this, v => v.GameVersionSelectorCombo.SelectedIndex)
                    .DisposeWith(d);*/

                ProjectsList
                    .Events()
                    .SelectionChanged
                    .Select(e => e.Source as ListBox)
                    .WhereNotNull()
                    .Select(listBox => ((ListItem<Guid, string>)listBox.SelectedItem).Key)
                    .BindTo(ViewModel, vm => vm.SelectedProject)
                    .DisposeWith(d);

                ViewModel
                    .WhenAnyValue(vm => vm.SelectedProject)
                    .WhereNotNull()
                    .Where(v => v != Guid.Empty)
                    .Subscribe(RenderFiles)
                    .DisposeWith(d);

                ViewModel
                    .WhenAnyValue(vm => vm.ProjectList)
                    .Select(list => list
                        .ToList()
                        .Select(project => new ListItem<Guid, string>(project.Id, project.Name)))
                    .Subscribe(items => {
                        ProjectsList.Dispatcher.InvokeAsync(() => {
                            ProjectsList.ItemsSource = items;

                            if (items.Any() && ViewModel.SelectedProject == Guid.Empty)
                            {
                                ProjectsList.SelectedIndex = 0;
                            }
                        });                        
                    })
                    .DisposeWith(d);

                TreeViewAddContextBtn
                    .Events()
                    .Click
                    .Select(e => Unit.Default)
                    .InvokeCommand(ViewModel.AddProjectFilesCommand)
                    .DisposeWith(d);

                TreeViewDeleteContextBtn
                    .Events()
                    .Click
                    .Select(e =>
                    {
                        var selectedItems = FilesTreeView.SelectedItems
                            .Cast<FileTreeViewNode>()
                            .ToList();

                        return selectedItems
                            .Select(item => item.FileId)
                            .ToList() as IReadOnlyCollection<Guid>;
                    })
                    .InvokeCommand(ViewModel.DeleteAssetsCommand)
                    .DisposeWith(d);

                ListBoxDeleteContextBtn
                    .Events()
                    .Click
                    .Select(e => Unit.Default)
                    .InvokeCommand(ViewModel.DeleteProjectCommand)
                    .DisposeWith(d);
                
                ListBoxEditContextBtn
                    .Events()
                    .Click
                    .Select(e => Unit.Default)
                    .InvokeCommand(ViewModel.EditProjectCommand)
                    .DisposeWith(d);
            });

            FilesTreeView.SelectedItemChanged += FilesTreeView_SelectedItemChanged;
            FilesTreeView.Focusable = true;
        }

        // a set of all selected items
        readonly List<FileTreeViewNode> selectedItems = new List<FileTreeViewNode>();


        private void RenderFiles(Guid projectId)
        {
            var files = ViewModel
                .ProjectList
                .FirstOrDefault(p => p.Id == projectId)
                ?.Items;

            if (files == null)
            {
                ProjectsList.SelectedIndex = 0;
                return;
            }
            
            FilesTreeView
                .Items
                .Clear();

            var items = Utilities.BuildFileStruture(files);

            if (items == null)
            {
                return;
            }

            FilesTreeView
                .Items
                .Add(items);
        }

        private void FilesTreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var treeViewItems = FilesTreeView.SelectedItems
                .Cast<FileTreeViewNode>()
                .ToList();

            if (!treeViewItems.Any())
                return;

            for (var i = 0; i < treeViewItems.Count(); i++)
            {
                var treeViewItem = treeViewItems.ElementAt(i);
                
                // prevent the WPF tree item selection 
                treeViewItem.IsSelected = false;
            }

            if (!CtrlPressed)
            {
                var selectedTreeViewItemList = selectedItems.ToList();
                foreach (FileTreeViewNode treeViewItem1 in selectedTreeViewItemList)
                {
                    Deselect(treeViewItem1);
                }
            }

            ChangeSelectedState(selectedItems, treeViewItems);
        }

        private void Deselect(FileTreeViewNode treeViewItem)
        {
            treeViewItem.Background = null;
            treeViewItem.Foreground = Brushes.Black;
            selectedItems.Remove(treeViewItem);
        }
        
        private void Select(FileTreeViewNode treeViewItem)
        {
            treeViewItem.Background = Brushes.Black;
            treeViewItem.Foreground = Brushes.White;
            selectedItems.Add(treeViewItem);
        }
        
        

        private void ChangeSelectedState(IReadOnlyCollection<FileTreeViewNode> highlightedItems, IReadOnlyCollection<FileTreeViewNode> selectedList)
        {
            foreach (var treeViewItem in selectedList)
            {
                if (!selectedItems.Contains(treeViewItem))
                {
                    Select(treeViewItem);
                }
            }

            foreach (var highlightedItem in highlightedItems.ToList())
            {
                if (!selectedList.Contains(highlightedItem))
                {
                    Deselect(highlightedItem);
                }
            }
        }

        private static bool CtrlPressed => Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl);

        object? IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (ProjectsTabControlModel)value!;
        }

        public ProjectsTabControlModel? ViewModel { get; set; }
    }
}