using System;

namespace HydroneerStager.Models
{
    public class ProjectItem
    {
        public ProjectItem() { }

        public ProjectItem(Guid id,
                           string name,
                           string path)
        {
            Id = id;
            Name = name;
            Path = path;
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Path { get; set; }
    }
}