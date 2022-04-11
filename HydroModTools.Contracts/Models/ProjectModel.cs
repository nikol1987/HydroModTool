using System;
using System.Collections.Generic;

namespace HydroModTools.Contracts.Models
{
    public sealed class ProjectModel
    {
        public ProjectModel(Guid id, string name, short modIndex, string path, string outputPath)
        {
            Id = id;
            Name = name;
            ModIndex = modIndex;
            Path = path;
            OutputPath = outputPath;
            Items = new List<ProjectItemModel>();
        }

        public ProjectModel(Guid id, string name, short modIndex, string path, string outputPath, IReadOnlyCollection<ProjectItemModel> items)
        {
            Id = id;
            Name = name;
            ModIndex = modIndex;
            Path = path;
            OutputPath = outputPath;
            Items = items;
        }

        public Guid Id { get; }

        public string Name { get; }

        public short ModIndex { get; }

        public string Path { get; }

        public string OutputPath { get; }

        public IReadOnlyCollection<ProjectItemModel> Items { get; }
    }

    public sealed class ProjectItemModel
    {
        public ProjectItemModel(Guid id, string name, string path)
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