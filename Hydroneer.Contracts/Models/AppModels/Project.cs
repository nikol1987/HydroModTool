using System;
using System.Collections.Generic;
using System.Linq;

namespace HydroneerStager.Contracts.Models.AppModels
{
    public sealed class Project
    {
        public Project() { }

        public Project(Guid id,
                       string name,
                       string path,
                       string outputPath,
                       IList<ProjectItem> items)
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

        public IList<ProjectItem> Items { get; set; } = new List<ProjectItem>();

        public void AddItems(List<ProjectItem> items)
        {
            var newItems = Items
                .ToList();

            newItems.AddRange(items);

            Items = newItems;
        }

        public void RemoveItems(IReadOnlyCollection<Guid> guids)
        {
            Items = Items.Where(e => !guids.Contains(e.Id)).ToList();
        }
    }
}
