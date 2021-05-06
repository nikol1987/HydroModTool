using HydroneerStager.Models;
using System;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace HydroneerStager
{
    public partial class AddProjectPage : UserControl
    {
        private Project _project = new Project() {
            Id = Guid.NewGuid(),
            Path = "",
            OutputPath = "",
            Name = ""
        };
        private readonly Action<Project> action;

        public AddProjectPage(Action<Project> action)
        {
            InitializeComponent();

            projectNameTextBox.DataBindings.Add("Text", _project, "Name", false, DataSourceUpdateMode.OnPropertyChanged);
            cookedAssetsDirTextBox.DataBindings.Add("Text", _project, "Path", false, DataSourceUpdateMode.OnPropertyChanged);
            outputPathDirTextBox.DataBindings.Add("Text", _project, "OutputPath", false, DataSourceUpdateMode.OnPropertyChanged);
            this.action = action;
        }

        private void selectCookedDirBtn_Click(object sender, System.EventArgs e)
        {
            ChooseFolderHelper("Select Cooked assets folder", (path) => {
                cookedAssetsDirTextBox.Text = path;
            });
        }

        private void outputPathDirBtn_Click(object sender, EventArgs e)
        {
            ChooseFolderHelper("Select output assets folder", (path) => {
                outputPathDirTextBox.Text = path;
            });
        }

        private static void ChooseFolderHelper(string title, Action<string> action)
        {
            var result = new StringBuilder();
            var thread = new Thread(obj =>
            {
                var builder = (StringBuilder)obj;
                using (var dialog = new FolderBrowserDialog())
                {
                    dialog.Description = title;
                    dialog.RootFolder = Environment.SpecialFolder.MyComputer;
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        builder.Append(dialog.SelectedPath);
                    }
                }
            });

            thread.SetApartmentState(ApartmentState.STA);
            thread.Start(result);

            while (thread.IsAlive)
            {
                Thread.Sleep(100);
            }

            action.Invoke(result.ToString()); ;
        }

        private void submit_Click(object sender, EventArgs e)
        {
            action.Invoke(_project);
        }
    }
}
