using HydroModTools.Contracts.Models;
using HydroModTools.WinForms.Extensions;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HydroModTools.WinForms.Data
{
    public sealed class ApplicationStore : ReactiveObject
    {
        internal static ApplicationStore Store;

        public static async Task RefreshStore()
        {
            // TODO: implement
        } 

        private IReadOnlyCollection<ProjectStore> _projects;
        public IReadOnlyCollection<ProjectStore> Projects
        {
            get => _projects;

            set
            {
                this.RaiseAndSetIfChanged(ref _projects, value);
            }
        }

        private Guid? _defaultProject;
        public Guid? DefaultProject
        {
            get => _defaultProject;

            set
            {
                this.RaiseAndSetIfChanged(ref _defaultProject, value);
            }
        }

        private IReadOnlyCollection<GuidItemStore> _guids;
        public IReadOnlyCollection<GuidItemStore> Guids
        {
            get => _guids;

            set
            {
                this.RaiseAndSetIfChanged(ref _guids, value);
            }
        }

        public static void SetupStore(AppConfigModel appConfigModel)
        {
            Store = appConfigModel.ToStore();
        }
    }

    public sealed class GuidItemStore : ReactiveObject
    {
        private Guid _id;
        public Guid Id
        {
            get => _id;

            set
            {
                this.RaiseAndSetIfChanged(ref _id, value);
            }
        }

        private string _name;
        public string Name
        {
            get => _name;

            set
            {
                this.RaiseAndSetIfChanged(ref _name, value);
            }
        }

        private Guid _moddedGuid;
        public Guid ModdedGuid
        {
            get => _moddedGuid;

            set
            {
                this.RaiseAndSetIfChanged(ref _moddedGuid, value);
            }
        }

        private Guid _originalGuid;
        public Guid OriginalGuid
        {
            get => _originalGuid;

            set
            {
                this.RaiseAndSetIfChanged(ref _originalGuid, value);
            }
        }
    }

    public sealed class ProjectStore : ReactiveObject
    {
        private Guid _id;
        public Guid Id
        {
            get => _id;

            set
            {
                this.RaiseAndSetIfChanged(ref _id, value);
            }
        }

        private string _name;
        public string Name
        {
            get => _name;

            set
            {
                this.RaiseAndSetIfChanged(ref _name, value);
            }
        }

        private string _path;
        public string Path
        {
            get => _path;

            set
            {
                this.RaiseAndSetIfChanged(ref _path, value);
            }
        }

        private string _outputPath;
        public string OutputPath
        {
            get => _outputPath;

            set
            {
                this.RaiseAndSetIfChanged(ref _outputPath, value);
            }
        }

        private IReadOnlyCollection<ProjectItemStore> _items;
        public IReadOnlyCollection<ProjectItemStore> Items
        {
            get => _items;

            set
            {
                this.RaiseAndSetIfChanged(ref _items, value);
            }
        }
    }

    public sealed class ProjectItemStore : ReactiveObject
    {
        public ProjectItemStore(Guid id, string name, string path)
        {
            Id = id;
            Name = name;
            Path = path;
        }

        private Guid _id;
        public Guid Id
        {
            get => _id;

            set
            {
                this.RaiseAndSetIfChanged(ref _id, value);
            }
        }

        private string _name;
        public string Name
        {
            get => _name;

            set
            {
                this.RaiseAndSetIfChanged(ref _name, value);
            }
        }

        private string _path;
        public string Path
        {
            get => _path;

            set
            {
                this.RaiseAndSetIfChanged(ref _path, value);
            }
        }
    }
}
