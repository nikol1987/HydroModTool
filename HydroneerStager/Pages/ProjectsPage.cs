﻿using HydroneerStager.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace HydroneerStager
{
    public partial class ProjectsPage : UserControl
    {
        private Store Store = Store.Instance;

        public ProjectsPage()
        {
            Store.PropertyChanged += Store_PropertyChanged;

            InitializeComponent();

            projectSettings.Items["addProject"].Click += AddProject_Click;
            projectSettings.Items["stageProject"].Click += StageProject_Click; ;
            projectSettings.Items["pakageProject"].Click += PackageProject_Click;
            projectSettings.Items["refreshPage"].Click += RefreshPage_Click; ;


            stagerWorker.DoWork += (object sender, DoWorkEventArgs e) => {
                var project = Store.Projects.FirstOrDefault(e => e.Id == Store.SelectedProject);

                if (project == null)
                {
                    MessageBox.Show("No Project selected");
                    return;
                }

                Stager.Stage(stagerWorker, project);
            };

            stagerWorker.ProgressChanged += (object sender, ProgressChangedEventArgs e) => {
                stageProgressbar.Value = e.ProgressPercentage;
                progressLabel.Text = $"Copying {((ProjectItem)e.UserState).Name}";
            };
            stagerWorker.RunWorkerCompleted += (object sender, RunWorkerCompletedEventArgs e) => {
                var project = Store.Projects.FirstOrDefault(e => e.Id == Store.SelectedProject);

                stageProgressbar.Value = 100;
                progressLabel.Text = "Completed";
                MessageBox.Show($"Project {project.Name} staged");
            };
            stagerWorker.WorkerReportsProgress = true;

            loadProjects();
            loadProjectItems();
        }

        private void PackageProject_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Soon™");
        }

        private async void RefreshPage_Click(object sender, EventArgs e)
        {
            await Store.InitAsync();

            Store = Store.Instance;

            loadProjects();
            loadProjectItems();
        }

        private void StageProject_Click(object sender, EventArgs e)
        {
            stagerWorker.RunWorkerAsync();
        }

        private void Store_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Projects")
            {
                loadProjects();
            }

            if (e.PropertyName == "SelectedProject" || e.PropertyName == "ProjectItems")
            {
                loadProjectItems();
            }
        }

        private void AddProject_Click(object sender, EventArgs e)
        {
            var selectFolderForm = new Form
            {
                FormBorderStyle = FormBorderStyle.FixedDialog
            };

            var selectFolderPage = new AddProjectPage((project) =>
            {
                Store.Instance.AddProject(project);
                selectFolderForm.Close();

                projectListBox.SelectedItem = project.Name;
                Store.Instance.SelectedProject = project.Id;
            })
            {
                Dock = DockStyle.Fill
            };

            selectFolderForm.Size = selectFolderPage.Size;
            selectFolderForm.Controls.Add(selectFolderPage);
            selectFolderForm.ShowDialog();
        }

        private void projectListBox_Change(object sender, EventArgs e)
        {
            var newSelectedItem = projectListBox.SelectedItem;

            if (newSelectedItem == null)
            {
                return;
            }

            Store.Instance.SelectedProject = Store.Instance.Projects.FirstOrDefault(e => e.Name == ((KeyValuePair<string, Project>)newSelectedItem).Key).Id;
        }

        private void loadProjects()
        {
            projectListBox.DataSource = new BindingSource(
                Store.Projects
                .Select(e => KeyValuePair.Create(e.Name, e))
                .ToList(), null);
            projectListBox.ValueMember = "Value";
            projectListBox.DisplayMember = "Key";

            if (projectListBox.Items.Count > 0)
            {
                projectListBox.SelectedIndex = 0;
            }
        }

        private void loadProjectItems()
        {
            projectItemsView.Nodes.Clear();

            var project = Store.Projects.FirstOrDefault(e => e.Id == Store.SelectedProject); ;

            if (project == null || project.Items == null || project.Items.Count == 0)
            {
                return;
            }

            var sortedItems = project.Items.OrderBy(e => e.Path).ToList();

            var assetTree = Utilities.BuildFileStruture(contextMenuStrip1, sortedItems);
            projectItemsView.Nodes.Add(assetTree.FirstNode);
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            var sourceControl = contextMenuStrip1.SourceControl;
            contextMenuStrip1.Items.Clear();

            if (sourceControl == null)
            {
                e.Cancel = true;
                return;
            }

            if (sourceControl.Name == projectListBox.Name)
            {
                var deleteProjectStripItem = new ToolStripButton() {
                    Name = "deleteProjectStripItem",
                    Text = "Delete Project",
                };

                deleteProjectStripItem.Click += (object sender, EventArgs e) => {
                    var project = ((KeyValuePair<string, Project>)projectListBox.SelectedItem).Value;

                    Store.DeleteProject(project.Id);
                };

                contextMenuStrip1.Items.Add(deleteProjectStripItem);
            }
            else if (sourceControl.Name == projectItemsView.Name)
            {
                var selectedNode = projectItemsView.SelectedNode;
                if (selectedNode != null && !selectedNode.Name.StartsWith("node-"))
                {
                    var deleteItemStripItem = new ToolStripButton()
                    {
                        Name = "deleteProjectItemStrip",
                        Text = "Remove Asset",
                    };

                    deleteItemStripItem.Click += (object sender, EventArgs e) => {
                        var project = ((KeyValuePair<string, Project>)projectListBox.SelectedItem).Value;

                        Store.RemoveItems(project.Id, new[] { new Guid(selectedNode.Name) });
                    };

                    contextMenuStrip1.Items.Add(deleteItemStripItem);
                }
                else
                {
                    var addProjectItemStripItem = new ToolStripButton()
                    {
                        Name = "addProjectItem",
                        Text = "Add Assets",
                    };

                    addProjectItemStripItem.Click += (object sender, EventArgs e) => {
                        var project = ((KeyValuePair<string, Project>)projectListBox.SelectedItem).Value;

                        ChooseFilesHelper("Select assets", project, (selectedFiles) =>
                        {
                            Store.AddItems(project.Id, selectedFiles);
                            loadProjectItems();
                        });
                    };

                    contextMenuStrip1.Items.Add(addProjectItemStripItem);
                }
            }
            else
            {
                contextMenuStrip1.Items.Add("Source: " + sourceControl.Name);
            }


            e.Cancel = false;
        }

        private void projectListBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var selectedIndex = projectListBox.IndexFromPoint(e.Location);

                if (selectedIndex != -1)
                {
                    projectListBox.SelectedIndex = selectedIndex;
                    contextMenuStrip1.Show(projectListBox, e.Location);
                }
            }
        }

        private static void ChooseFilesHelper(string title, Project project, Action<List<string>> action)
        {
            var result = new List<string>();
            var thread = new Thread(obj =>
            {
                var files = (List<string>)obj;
                using (var dialog = new OpenFileDialog())
                {
                    dialog.Title = title;
                    dialog.Multiselect = true;
                    dialog.Filter = "UE Assets|*.uasset;*.uexp";
                    dialog.InitialDirectory = project.Path;
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        files.AddRange(dialog.FileNames);
                    }
                }
            });

            thread.SetApartmentState(ApartmentState.STA);
            thread.Start(result);

            while (thread.IsAlive)
            {
                Thread.Sleep(100);
            }

            action.Invoke(result); ;
        }

        private void projectItemsView_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(projectItemsView, e.Location);
            }
        }
    }
}