using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HydroModTool.Views
{
    public class ProjectViewModel : ViewModel
    {
        public ProjectListing Project { get; }
        public string FriendlyName
        {
            get => Project.FriendlyName;
            set
            {
                Project.FriendlyName = value;
                OnPropertyChanged();
            }
        }

        public string OutputPath
        {
            get => Project.OutputPath;
            set
            {
                Project.OutputPath = value;
                OnPropertyChanged();
            }
        }

        public string InputPath
        {
            get => Project.InputPath;
            set
            {
                Project.InputPath = value;
                OnPropertyChanged();
            }
        }

        public List<string> ScannedFiles => Project.ScannedFiles;
        public List<string> IncludedFiles => Project.IncludedFiles;

        public override string ToString() => FriendlyName ?? OutputPath;

        public ProjectViewModel()
        {
            Project = new ProjectListing();
        }
        public ProjectViewModel(ProjectListing project)
        {
            Project = project;
        }
    }
}
