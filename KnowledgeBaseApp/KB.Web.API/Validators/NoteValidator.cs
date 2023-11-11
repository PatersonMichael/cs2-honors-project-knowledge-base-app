using FluentValidation;
using KB.Web.API.DtoModels;

namespace KB.Web.API.Validators
{
    public class NoteValidator : AbstractValidator<Note>
    {
        public NoteValidator()
        {
            RuleFor(x => x.Title).NotEmpty().MaximumLength(200);
            RuleFor(x => x.UserProfileId).NotEmpty();
        }
    }
}
