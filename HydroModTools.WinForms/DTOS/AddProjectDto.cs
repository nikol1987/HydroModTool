namespace HydroModTools.WinForms.DTOs
{
    internal sealed class AddProjectDto
    {
        public AddProjectDto(string projectName, string cookedAssetsPath, string outputPath)
        {
            ProjectName = projectName;
            CookedAssetsPath = cookedAssetsPath;
            OutputPath = outputPath;
        }

        public string ProjectName { get; }

        public string CookedAssetsPath { get; }

        public string OutputPath { get; }
    }
}
