using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace HydroModTool.Views
{
    public class ConfigViewModel : ViewModel
    {
        private ObservableCollection<ProjectViewModel> _projects;
        private ObservableCollection<GuidEntry> _guids;
        private MainConfig _config;
        private HashSet<byte> _firstBytes;

        public ObservableCollection<ProjectViewModel> Projects
        {
            get => _projects;
            set => SetValue(ref _projects, value);
        }

        public ObservableCollection<GuidEntry> Guids
        {
            get => _guids;
            set => SetValue(ref _guids, value);
        }

        public HashSet<byte> FirstBytes
        {
            get
            {
                if (_firstBytes?.Count == 0)
                    UpdateBytes();
                return _firstBytes;
            }
        }

        public bool AutoPackage
        {
            get => _config.AutoPackage;
            set
            {
                _config.AutoPackage = value;
                OnPropertyChanged(nameof(AutoPackage));
            }
        }

        public bool ShowConsole
        {
            get => _config.ShowConsole;
            set
            {
                _config.ShowConsole = value;
                OnPropertyChanged(nameof(ShowConsole));
            }
        }

        private void UpdateBytes()
        {
            if (_firstBytes == null)
                _firstBytes = new HashSet<byte>();
            
            _firstBytes.Clear();
            foreach (var entry in Guids)
                if (entry.ModifiedBytes?.Length > 0)
                    _firstBytes.Add(entry.ModifiedBytes[0]);
        }

        public ConfigViewModel()
        {
        }
        public ConfigViewModel(MainConfig config)
        {
            _config = config;
            Projects = new ObservableCollection<ProjectViewModel>(config.Projects.Select(p => new ProjectViewModel(p)));
            Projects.CollectionChanged += (sender, args) => OnCollectionChanged(args);
            Guids = new ObservableCollection<GuidEntry>(config.Guids);
            Guids.CollectionChanged += (sender, args) => OnCollectionChanged(args);
            Guids.CollectionChanged += (sender, args) => UpdateBytes();
        }

        public void Save(string path)
        {
            _config.Projects = Projects.Select(p=>p.Project).ToList();
            _config.Guids = Guids.Where(g => g.ModifiedBytes != null && g.RetailBytes != null).ToList();
            Serialization.SaveMain(path, _config);
        }
    }
}
