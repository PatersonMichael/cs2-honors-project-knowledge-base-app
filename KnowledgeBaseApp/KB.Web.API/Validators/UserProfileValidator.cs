using System.Text.RegularExpressions;
using FluentValidation;
using KB.Web.API.DtoModels;

namespace KB.Web.API.Validators
{
    public class UserProfileValidator : AbstractValidator<UserProfile>

    {
        /*
         * At least:
         * One Uppercase Letter, One lowercase letter, one number, one symbol.
         * Min: 19 characters
         * Max: 256 characters
         */
        private readonly Regex _strongPasswordRegex = new Regex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{9,256}$");

        public UserProfileValidator()
        {
            // up = userProfileDto
            RuleFor(up => up.FirstName).NotEmpty().MaximumLength(100);
            RuleFor(up => up.LastName).NotEmpty().MaximumLength(100);
            RuleFor(up => up.Email).EmailAddress();
            RuleFor(up => up.BirthDate).NotEmpty();
            RuleFor(up => up.Nametag).NotEmpty().MaximumLength(60);
            RuleFor(up => up.Password).NotEmpty()
                .Matches(_strongPasswordRegex)
                .MinimumLength(19)
                .MaximumLength(256);
        }
    }
}
