using ComponentFactory.Krypton.Toolkit;
using HydroModTools.Client.WinForms.ViewModels;
using HydroModTools.Contracts.Services;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows.Forms;

namespace HydroModTools.Winforms.Client.Views.ApplicationTabs.ProjectTabs
{
    public partial class ProjectsView : UserControl, IViewFor<ProjectsViewModel>
    {
        public ProjectsView(IServiceProvider services, IProjectsService projectsService, IConfigurationService configurationService)
        {
            ViewModel = new ProjectsViewModel(services, projectsService, configurationService);

            InitializeComponent();

            this.WhenActivated(d =>
            {
                d(this.Bind(ViewModel, vm => vm.Projects, v => v.projectListBox.DataSource));

                d(ViewModel
                    .WhenAnyValue(vm => vm.ProgressBarState)
                    .Subscribe(progress =>
                    {
                        progressBar.Value = progress.Value;
                        progressBarLabel.Text = progress.Label;
                    }));

                d(ViewModel
                    .WhenAnyValue(vm => vm.ProjectItems)
                    .Subscribe(x =>
                    {
                        projectItemsTree.SelectedNode = null;
                        projectItemsTree.SelectedNodes = null;

                        if (x == null)
                        {
                            projectItemsTree.Nodes.Clear();
                            return;
                        }

                        projectItemsTree.Nodes.Clear();
                        projectItemsTree.Nodes.Add(x);
                        projectItemsTree.ExpandAll();
                    }));


                d(this.projectListBox.ListBox.Events()
                    .SelectedValueChanged
                    .Select(ea => (Guid?)this.projectListBox.ListBox.SelectedValue ?? Guid.Empty)
                    .Where(val => !val.Equals(ViewModel.SelectedProject))
                    .InvokeCommand(ViewModel.SelectProjectCommand));

                d(menuStrip.Events()
                    .ItemClicked
                    .Select(e => e.ClickedItem.Name)
                    .InvokeCommand(ViewModel, vm => vm.ExecuteStripMenuCommand));


                projectListBox.SelectedValue = ViewModel.SelectedProject;
            });


            projectItemsTree.MouseUp += (sender, ea) =>
            {
                if (ea.Button == MouseButtons.Right)
                {
                    contextMenu.Show(projectItemsTree);
                }
            };

            projectListBox.ListBox.MouseUp += (sender, ea) =>
            {
                if (ea.Button == MouseButtons.Right)
                {
                    var selectedIndex = projectListBox.IndexFromPoint(ea.Location);

                    if (selectedIndex != -1 && selectedIndex != 65535)
                    {
                        projectListBox.SelectedIndex = selectedIndex;
                        contextMenu.Show(projectListBox);
                    }
                }
            };

            contextMenu.Opening += ContextMenu_Opening;
        }

        private void ContextMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var sourceControl = (Control)contextMenu.Caller;
            contextMenu.Items.Clear();

            if (sourceControl == null)
            {
                e.Cancel = true;
                return;
            }

            var items = new KryptonContextMenuItems();

            if (sourceControl.Name == projectListBox.Name)
            {
                var deleteProjectStripItem = new KryptonContextMenuItem("Delete Project");

                var deleteClick = Observable.FromEventPattern<EventArgs>(deleteProjectStripItem, "Click");
                deleteClick
                  .Select(ea => (Guid)projectListBox.SelectedValue)
                  .InvokeCommand(ViewModel.DeleteProjectCommand);

                items.Items.Add(deleteProjectStripItem);

                var editProjectStripItem = new KryptonContextMenuItem("Edit Project");

                var editClick = Observable.FromEventPattern<EventArgs>(editProjectStripItem, "Click");
                editClick
                  .Select(ea => (Guid)projectListBox.SelectedValue)
                  .InvokeCommand(ViewModel.EditProjectCommand);

                items.Items.Add(editProjectStripItem);
            }
            else if (sourceControl.Name == projectItemsTree.Name)
            {
                var selectedNode = projectItemsTree.SelectedNodes.Count > 1 ?
                    projectItemsTree.SelectedNodes.Where(a => !a.Name.StartsWith("node-")).FirstOrDefault() ?? projectItemsTree.SelectedNode : projectItemsTree.SelectedNode;
                if (selectedNode != null && !selectedNode.Name.StartsWith("node-"))
                {
                    var selectedItems = projectItemsTree.SelectedNodes
                        .Where(a => !a.Name.StartsWith("node-"))
                        .ToList();

                    if (selectedItems.Count() != 0)
                    {
                        var deleteItemStripItem = new KryptonContextMenuItem($"Remove Asset{(selectedItems.Count() == 1 ? "" : "s")}");

                        var deleteClick = Observable.FromEventPattern<EventArgs>(deleteItemStripItem, "Click");
                        deleteClick
                          .Select(ea => selectedItems.Select(i => new Guid(i.Name)).ToList())
                          .InvokeCommand<List<Guid>>(ViewModel.DeleteAssetsCommand);

                        deleteClick
                          .Subscribe(ea => {
                              projectItemsTree.SelectedNode = null;
                              projectItemsTree.SelectedNodes = null;
                          });

                        items.Items.Add(deleteItemStripItem);
                    }
                }
                else
                {
                    var addAssetsItemStripItem = new KryptonContextMenuItem("Add Assets");

                    var deleteClick = Observable.FromEventPattern<EventArgs>(addAssetsItemStripItem, "Click");
                    deleteClick
                       .Select(ea => Unit.Default)
                      .InvokeCommand(ViewModel.AddAssetsCommand);

                    items.Items.Add(addAssetsItemStripItem);
                }
            }
            else
            {
                items.Items.Add(new KryptonContextMenuLinkLabel("Source: " + sourceControl.Name));
            }

            contextMenu.Items.Add(items);

            e.Cancel = false;
        }

        public ProjectsViewModel ViewModel { get; set; }

        object IViewFor.ViewModel { get => ViewModel; set => ViewModel = (ProjectsViewModel)value; }
    }
}
