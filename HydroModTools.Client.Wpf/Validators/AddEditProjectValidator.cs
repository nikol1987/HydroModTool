using FluentValidation;
using HydroModTools.Client.Wpf.DTOs;

namespace HydroModTools.Client.Wpf.Validators
{
    internal class AddEditProjectValidator  : AbstractValidator<AddEditProjectDto>
    {
        public AddEditProjectValidator()
        {
            RuleFor(e => e.Name)
                .NotEmpty()
                .WithMessage("Project name cant be empty!");

            RuleFor(e => e.CookedAssetsDir)
                .NotEmpty()
                .WithMessage("Select Mining Folder inside the cooked assets path!");

            RuleFor(e => e.DistDir)
                .NotEmpty()
                .WithMessage("Select the root folder where you want to place the staging folder");

            RuleFor(e => e.ModIndex)
                .Custom((value, context) =>
                {
                    if (!short.TryParse(value, out var parsedValue))
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