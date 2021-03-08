using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HydroModTool.Views
{
    public class ConfigViewModel : ViewModel
    {
        private ObservableCollection<ProjectViewModel> _projects;
        private ObservableCollection<GuidEntry> _guids;
        private bool _autoPackage = false;
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
            set  {
            SetValue(ref _guids, value);

        }
        }

        public HashSet<byte> FirstBytes
        {
            get
            {
                if((_firstBytes?.Count?? 0) == 0)
                    UpdateBytes();
                return _firstBytes;
            }
        }

        public bool AutoPackage
        {
            get => _autoPackage;
            set => SetValue(ref _autoPackage, value);
        }

        private void UpdateBytes()
        {
            if (_firstBytes == null)
                _firstBytes = new HashSet<byte>();

            _firstBytes.Clear();
            foreach (var entry in Guids)
                _firstBytes.Add(entry.ModifiedBytes[0]);
        }
        public ConfigViewModel(MainConfig config)
        {
            Projects = new ObservableCollection<ProjectViewModel>(config.Projects.Select(p => new ProjectViewModel(p)));
            Projects.CollectionChanged += (sender, args) => OnCollectionChanged(args);
            Guids = new ObservableCollection<GuidEntry>(config.Guids);
            Guids.CollectionChanged += (sender, args) => OnCollectionChanged(args);
            Guids.CollectionChanged += (sender, args) => UpdateBytes();
            AutoPackage = config.AutoPackage;
            _config = config;
        }

        public void Save(string path)
        {
            _config.AutoPackage = _autoPackage;
            _config.Projects = Projects.Select(p=>p.Project).ToList();
            _config.Guids = Guids.ToList();
            Serialization.SaveMain(path, _config);
        }
    }
}
