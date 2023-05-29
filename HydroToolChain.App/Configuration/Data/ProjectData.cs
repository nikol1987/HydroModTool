using System.ComponentModel.DataAnnotations;

namespace HydroToolChain.App.Configuration.Data;

public class ProjectData
{
    internal ProjectData(Guid id)
    {
        Id = id;
        Items = new List<ProjectItemData>();
    }
    
    public ProjectData()
        : this(Guid.NewGuid())
    {}
    
    public Guid Id { get; set; } 
    
    public List<ProjectItemData> Items { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Name is Required")]
    [MinLength(3, ErrorMessage = "Must be at least 3 characters")]
    public string Name { get; set; } = "";

    [Required(AllowEmptyStrings = false, ErrorMessage = "Index is Required")]
    public int ModIndex { get; set; } = 500;

    [Required(AllowEmptyStrings = false, ErrorMessage = "Asset Path is Required")]
    [CustomValidation(typeof(ProjectData), nameof(ValidatePath))]

    public string CookedAssetsPath { get; set; } = "";
    
    [Required(AllowEmptyStrings = false, ErrorMessage = "Dist Path is Required")]
    [CustomValidation(typeof(ProjectData), nameof(ValidatePath))]

    public string OutputPath { get; set; } = "";
    
    public static ValidationResult? ValidatePath(string path, ValidationContext vctx)
    {
        try
        {
            if (!Directory.Exists(path))
            {
                return new ValidationResult("Path doesn't exist");
            }
        }
        catch (Exception)
        {
            return new ValidationResult("Invalid path or can´t write to path");
        }
        
        return ValidationResult.Success;
    }
}