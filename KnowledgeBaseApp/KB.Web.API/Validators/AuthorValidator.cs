using FluentValidation;
using KB.Web.API.DtoModels;

namespace KB.Web.API.Validators
{
    public class AuthorValidator : AbstractValidator<Author>
    {
        public AuthorValidator()
        {
            RuleFor(a => a.FirstName).NotEmpty().MaximumLength(200);
            RuleFor(a => a.LastName).NotEmpty().MaximumLength(200);
            RuleFor(a => a.UserProfileId).NotEmpty();
        }
    }
}
