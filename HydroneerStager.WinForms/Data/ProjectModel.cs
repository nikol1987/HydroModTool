using ReactiveUI;
using System;
using System.Collections.Generic;

namespace HydroneerStager.WinForms.Data
{
    public sealed class ProjectModel : ReactiveObject
    {
        public ProjectModel(Guid id,
                            string name,
                            string path,
                            string outputPath,
                            IReadOnlyCollection<ProjectItemModel> items)
        {
            Id = id;
            Name = name;
            Path = path;
            OutputPath = outputPath;
            Items = items;
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Path { get; set; }

        public string OutputPath { get; set; }

        private IReadOnlyCollection<ProjectItemModel> _items;
        public IReadOnlyCollection<ProjectItemModel> Items
        {
            get => _items;
            set
            {
                this.RaiseAndSetIfChanged(ref _items, value);
                this.RaisePropertyChanged();
            }
        }
    }
}