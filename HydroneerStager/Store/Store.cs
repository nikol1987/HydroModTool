using HydroneerStager.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HydroneerStager
{
    internal sealed class Store : AppStoreBase
    {
        private Store() { }

        private Guid? _selectedProject;

        public Guid? SelectedProject
        {
            get
            {
                return _selectedProject;
            }
            set
            {
                AppConfiguration.DefaultProject = value;
                SetValue(ref _selectedProject, value, "SelectedProject");
            }
        }

        private IList<Project> _projects = new List<Project>();

        public IReadOnlyCollection<Project> Projects
        {
            get
            {
                return _projects.ToList();
            }
        }

        private IList<GuidItem> _guids = new List<GuidItem>();

        public IReadOnlyCollection<GuidItem> Guids
        {
            get
            {
                return _guids.ToList();
            }
        }

        public void AddProject(Project newProject)
        {
            _projects.Add(newProject);
            AppConfiguration.Projects = _projects.ToList();
            OnChange("Projects");
        }

        public void DeleteProject(Guid id)
        {
            _projects = _projects.Where(e => e.Id != id).ToList();
            OnChange("Projects");
            AppConfiguration.Projects = _projects.ToList();
            Configuration.Save(AppConfiguration);
        }

        public void RemoveItems(Guid id,IReadOnlyCollection<Guid> guids)
        {
            var project = _projects.First(e => e.Id == id);

            project.RemoveItems(guids);
            OnChange("ProjectItems");
        }

        public void AddItems(Guid id, List<string> selectedFiles)
        {
            var project = _projects.First(e => e.Id == id);

            var items = selectedFiles
                    .Select(e => new ProjectItem()
                    {
                        Id = Guid.NewGuid(),
                        Path = e.Replace(project.Path, ""),
                        Name = Path.GetFileName(e)
                    })
                    .ToList();

            project.AddItems(items);


            OnChange("ProjectItems");
            AppConfiguration.Projects = _projects.ToList();
            Configuration.Save(AppConfiguration);
        }

        internal void SaveGuids(IReadOnlyCollection<GuidItem> guids)
        {
            AppConfiguration.Guids = guids.ToList();
            SetValue(ref _guids, guids.ToList(), "Guids");
        }

        internal override async Task InitAsync()
        {
            var AppConfiguration = await Configuration.GetConfigurationAsync();

            _instance = new Store()
            {
                AppConfiguration = AppConfiguration,
                _selectedProject = AppConfiguration.DefaultProject,
                _projects = AppConfiguration.Projects,
                _guids = AppConfiguration.Guids
            };
        }
    
        public static Store GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Store();
            }

            return (Store)_instance;
        }
    }
}
