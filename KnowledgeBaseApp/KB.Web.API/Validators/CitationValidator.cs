using FluentValidation;
using KB.Web.API.DtoModels;

namespace KB.Web.API.Validators
{
    public class CitationValidator : AbstractValidator<Citation>
    {
        public CitationValidator()
        {
            RuleFor(c => c.Format).NotEmpty();
            RuleFor(c => c.UserProfileId).NotEmpty();
        }
    }
}
