using System;

namespace HydroneerStager.Contracts.Models.WinformModels
{
    public sealed class ProjectItemModel
    {
        public ProjectItemModel(Guid id,
                                string name,
                                string path)
        {
            Id = id;
            Name = name;
            Path = path;
        }

        public Guid Id { get; }

        public string Name { get; }

        public string Path { get; }
    }
}