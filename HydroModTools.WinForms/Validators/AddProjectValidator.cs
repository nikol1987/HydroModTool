using FluentValidation;
using HydroModTools.WinForms.DTOs;

namespace HydroModTools.WinForms.Validators
{
    internal sealed class AddProjectValidator : AbstractValidator<AddProjectDto>
    {
        public AddProjectValidator()
        {
            RuleFor(e => e.ProjectName)
                .NotEmpty()
                .WithMessage("Project name cant be empty!");

            RuleFor(e => e.CookedAssetsPath)
                .NotEmpty()
                .WithMessage("Select Mining Folder inside the cooked assets path!");

            RuleFor(e => e.OutputPath)
                .NotEmpty()
                .WithMessage("Select the root folder where you want to place the staging folder");
        }
    }
}
