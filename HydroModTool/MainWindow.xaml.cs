using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.IO;
using System.Windows.Controls;
using HydroModTool.Views;
using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace HydroModTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static ConfigViewModel Config { get; private set; }
        private ProjectViewModel _current;
        private static readonly string ConfigPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "hydro.cfg");
        public MainWindow()
        {
            InitializeComponent();
            
           var config = Serialization.LoadMain(ConfigPath);
            if (config == null)
            {
                config = new MainConfig();
                config.Projects = new List<ProjectListing>();
                config.Guids = new List<GuidEntry>();
                int xl = Serialization.BaseGuids.Length;
                for (int i = 0; i < xl; i++)
                {
                    var set = Serialization.BaseGuids[i];
                    config.Guids.Add(new GuidEntry() {Modified = set[0], Retail = set[1], Name = set[2]});
                }

                Serialization.SaveMain(ConfigPath, config);
            }

            Config = new ConfigViewModel(config);
            //GuidGrid.DataContext = Config.Guids;
            
            this.DataContext = Config;

            ProjectList.ItemsSource = Config.Projects;
            ProjectList.SelectionChanged += ProjectList_SelectionChanged;

            var d = config.Guids[0];
        }

        private void SaveConfig()
        {
            Config.Save(ConfigPath);
        }

        private void ProjectList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ProjectList.SelectedItem == null)
            {
                ((List<TreeViewItem>)FileTree.ItemsSource)?.Clear();
                ToggleButtons(false);
                return;
            }
            
            _current = (ProjectViewModel)ProjectList.SelectedItem;

            ToggleButtons(true);

            RefreshFiles();
        }

        private void RefreshFiles()
        {
            var files = _current?.IncludedFiles;

            if (files == null)
            {
                FileTree.Items.Clear();
                return;
            }


            var pathd = new Dictionary<string, TreeViewItem>();
            TreeViewItem root = null;
            
            foreach (var file in files)
            {
                FindAndParentItem(file, pathd, out var r);
                root = r ?? root;
            }

            FileTree.ItemsSource = new List<TreeViewItem>(){root};
            FileTree.UpdateLayout();
        }

        private void FindAndParentItem(string path, Dictionary<string, TreeViewItem> pathd, out TreeViewItem root)
        {
            string fullPath = path;
            var idx = path.IndexOf("Mining\\Content", StringComparison.CurrentCultureIgnoreCase);
            if (idx > 0)
            {
                path = path.Substring(idx + 7);
            }
            root = null;
            //Console.WriteLine(path);

            

            var parts = path.Split('\\');
            if(parts.Length <=1)
                throw new Exception("Couldn't process file tree");
            
            var stack = new Stack<TreeViewItem>(parts.Length);

            //build out the hierarchy for this path
            for (var i = 0; i < parts.Length; i++)
            {
                string part = parts[i];
                string rootPath = Utilities.GetPathAt(i, path);
                //if(part == "StoreItems")
                //    Debugger.Break();
                
                //see if this node has been added to the tree
                if (!pathd.TryGetValue(rootPath, out var it))
                {
                    it = new TreeViewItem();
                    it.Header = part;
                    it.ItemsSource = new List<TreeViewItem>();
                    it.IsExpanded = true;

                    //try to add this to children of previous node in the stack
                    if (stack.Count > 0)
                    {
                        var p = stack.Peek();
                        //if (p.Header.Equals("StoreItems"))
                        //    Console.WriteLine(fullPath);
                        ((List<TreeViewItem>) p.ItemsSource).Add(it);
                    }
                    else
                        root = it;

                    pathd.Add(rootPath, it);
                }

                //we either found or created a node, push it to the stack
                stack.Push(it);

                //final node is a leaf, store the full file URI
                if (i == parts.Length - 1)
                    it.Tag = fullPath;
            }
            
            //return null;
        }

        private void ToggleButtons(bool state)
        {
            Bt_FileAdd.IsEnabled = state;
            //hot mess. Disable the remove button if disable requested, or selected node is not a leaf (or no selection)
            Bt_FileRemove.IsEnabled = !(!state||(FileTree.SelectedItem as TreeViewItem)?.Tag == null);
        }

        private void Bt_ProjectAdd_OnClick(object sender, RoutedEventArgs e)
        {
            var project = new ProjectListing();
            var diag = new ProjectEditor();
            diag.Open(project);
            var view = new ProjectViewModel(project);
            Config.Projects.Add(view);
            SaveConfig();
        }
        private void Bt_ProjectRemove_OnClick(object sender, RoutedEventArgs e)
        {
            if (_current == null)
                return;

            Config.Projects.Remove(_current);
            SaveConfig();
        }

        private void Bt_ProjectPackage_OnClick(object sender, RoutedEventArgs e)
        {
            ConsoleAllocator.ShowConsoleWindow();
            Packaging.PackageProject(_current.Project, Config);
            Console.WriteLine("Patching is done, but compiling to a PAK file isn't supported yet. Please continue to use the asset editor.");
            Console.WriteLine($"I've made a clean folder to build your PAK from, at {_current.OutputPath}\\Staging");
            Console.WriteLine("Press any key to close this terminal. Clicking the x will close the main program.");
            Console.ReadKey(true);
            ConsoleAllocator.HideConsoleWindow();
        }

        private void Bt_FileAdd_OnClick(object sender, RoutedEventArgs e)
        {
            if (_current == null)
                return;

            var diag = new CommonOpenFileDialog("Select files to track");
            diag.Multiselect = true;
            diag.InitialDirectory = _current.InputPath;
            diag.EnsureFileExists = true;
            diag.IsFolderPicker = false;
            diag.Filters.Add(new CommonFileDialogFilter("Assets",".uasset, .uexp"));
            var res = diag.ShowDialog(this);

            if (res != CommonFileDialogResult.Ok)
                return;

            var it = diag.FileNames;
            if (!it.Any())
                return;

            foreach(var file in diag.FileNames)
                _current.IncludedFiles.Add(file);

            _current.IncludedFiles.Sort();
            RefreshFiles();
            SaveConfig();
        }

        private void Bt_FileRemove_OnClick(object sender, RoutedEventArgs e)
        {
            var item = FileTree.SelectedItem as TreeViewItem;
            var path = item?.Tag as string;
            if (path == null)
                return;

            _current.IncludedFiles.Remove(path);
            RefreshFiles();
            SaveConfig();
        }

        private void FileTree_OnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (FileTree.SelectedItem == null)
            {
                ToggleButtons(false);
                return;
            }

            ToggleButtons(true);
        }
    }
}
