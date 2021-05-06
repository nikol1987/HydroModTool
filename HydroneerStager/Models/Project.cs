using System;
using System.Collections.Generic;
using System.Linq;

namespace HydroneerStager.Models
{
    public sealed class Project
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Path { get; set; }

        public string OutputPath { get; set; }

        public IList<ProjectItem> Items { get; set; } = new List<ProjectItem>();

        internal void AddItems(List<ProjectItem> items)
        {
            var newItems = Items
                .ToList();

            newItems.AddRange(items);

            Items = newItems;
        }

        internal void RemoveItems(IReadOnlyCollection<Guid> guids)
        {
            Items = Items.Where(e => !guids.Contains(e.Id)).ToList();
        }
    }
}
