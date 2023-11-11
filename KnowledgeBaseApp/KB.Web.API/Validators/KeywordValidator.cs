using FluentValidation;
using KB.Web.API.DtoModels;

namespace KB.Web.API.Validators
{
    public class KeywordValidator : AbstractValidator<Keyword>
    {
        public KeywordValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.UserProfileId).NotEmpty();
        }
    }
}
