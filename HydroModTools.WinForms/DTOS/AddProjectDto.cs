namespace HydroModTools.WinForms.DTOs
{
    internal sealed class AddProjectDto
    {
        public AddProjectDto(string projectName, string modIndex, string cookedAssetsPath, string outputPath)
        {
            ProjectName = projectName;
            ModIndex = modIndex;
            CookedAssetsPath = cookedAssetsPath;
            OutputPath = outputPath;
        }

        public string ProjectName { get; }

        public string ModIndex { get; }

        public string CookedAssetsPath { get; }

        public string OutputPath { get; }
    }
}
