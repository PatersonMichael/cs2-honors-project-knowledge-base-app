using FluentValidation;
using KB.Web.API.DtoModels;

namespace KB.Web.API.Validators
{
    public class ExcerptCardValidator : AbstractValidator<ExcerptCard>
    {
        public ExcerptCardValidator()
        {
            RuleFor(x => x.Title).NotEmpty().MaximumLength(200);
            RuleFor(x => x.Excerpt).MaximumLength(1600);
            RuleFor(x => x.UserProfileId).NotEmpty();
        }
    }
}
