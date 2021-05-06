using System.Globalization;
using System.IO;
using System.Windows;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace HydroModTool.Views
{
    /// <summary>
    /// Interaction logic for ProjectEditor.xaml
    /// </summary>
    public partial class ProjectEditor : Window
    {
        public ProjectEditor()
        {
            InitializeComponent();
        }

        public ProjectViewModel Project;
        public void Open(ProjectListing project)
        {
            Project = new ProjectViewModel(project);
            this.DataContext = Project;

            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            ShowDialog();
        }

        private void InputButton_Click(object sender, RoutedEventArgs e)
        {
            string packagePath = null;
            var diag = new CommonOpenFileDialog("Select UE4 Package Directory");
            diag.IsFolderPicker = true;
            diag.Multiselect = false;

            Show:
            var res = diag.ShowDialog(this);
            if (res != CommonFileDialogResult.Ok)
                return;

            var attr = File.GetAttributes(diag.FileName);
            if ((attr & FileAttributes.Directory) == 0)
            {
                MessageBox.Show("Please select a directory.", "Error");
                goto Show;
            }

            if (!diag.FileName.EndsWith("Mining\\Content", true, CultureInfo.CurrentCulture))
            {
                //make sure that /Mining/Content exists in the file tree, then reset to Mining
                packagePath = SearchDirectories(diag.FileName);

                if (string.IsNullOrEmpty(packagePath))
                {
                    MessageBox.Show("Could not find the \\Mining\\Content directory. Make sure this is your UE4 package output directory!", "Error");
                    goto Show;
                }
            }

            if (packagePath != null)
                Project.InputPath = packagePath;
        }

        private void OutputButton_Click(object sender, RoutedEventArgs e)
        {
            string path = null;

            var diag = new CommonOpenFileDialog("Select output directory");
            diag.IsFolderPicker = true;
            diag.Multiselect = false;

            var res = diag.ShowDialog(this);

            if (res == CommonFileDialogResult.Ok)
                path = diag.FileName;

            if (path != null)
                Project.OutputPath = path;
        }

        private string SearchDirectories(string path)
        {
            string output = null;

            foreach (var dir in Directory.EnumerateDirectories(path))
            {
                if (dir.EndsWith("Engine"))
                    continue;
                if (dir.EndsWith("Mining") && Directory.Exists(System.IO.Path.Combine(dir, "Content")))
                {
                    output = dir;
                    break;
                }

                output = SearchDirectories(dir);
                if (output != null)
                    break;
            }

            return output;
        }

        private void DoneButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
