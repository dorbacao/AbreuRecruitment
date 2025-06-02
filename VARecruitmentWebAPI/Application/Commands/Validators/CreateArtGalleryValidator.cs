using FluentValidation;

namespace VAArtGalleryWebAPI.Application.Commands.Validators
{
    public class CreateArtGalleryValidator : AbstractValidator<CreateArtGalleryCommand>
    {
        public CreateArtGalleryValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Nome é obrigatório.");
        }
    }

}
