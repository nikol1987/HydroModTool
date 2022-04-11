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

            RuleFor(e => e.ModIndex)
                .Custom((value, context) =>
                {
                    if (!short.TryParse(value, out short parsedValue))
                    {
                        context.AddFailure($"Mod Index contains '{value}' which is not a valid number");

                        return;
                    }

                    if (parsedValue <= 0)
                    {
                        context.AddFailure($"Mod Index should be greater than 0");
                    }
                });
        }
    }
}
