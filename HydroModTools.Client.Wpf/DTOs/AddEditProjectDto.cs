namespace HydroModTools.Client.Wpf.DTOs
{
    public class AddEditProjectDto
    {
        public AddEditProjectDto(
            string name,
            string modIndex,
            string cookedAssetsDir,
            string distDir)
        {
            Name = name;
            ModIndex = modIndex;
            CookedAssetsDir = cookedAssetsDir;
            DistDir = distDir;
        }
        
        public string Name { get; }
        
        public string ModIndex { get; }
        
        public string CookedAssetsDir { get; }
        
        public string DistDir { get; }
    }
}