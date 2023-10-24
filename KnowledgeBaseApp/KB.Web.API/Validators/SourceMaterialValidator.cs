using FluentValidation;
using KB.Web.API.DtoModels;

namespace KB.Web.API.Validators
{
    public class SourceMaterialValidator : AbstractValidator<SourceMaterial>
    {
        public SourceMaterialValidator()
        {
            RuleFor(s => s.Title).NotEmpty();
            RuleFor(s => s.PublishDate).NotEmpty();
            RuleFor(s => s.SourceMaterialType).NotEmpty();
            RuleFor(s => s.UserProfileId).NotEmpty();
        }
    }
}
