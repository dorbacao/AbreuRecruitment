using FluentValidation;

namespace VAArtGalleryWebAPI.Application.Commands.Validators
{
    public class UpdateArtGalleryValidator : AbstractValidator<UpdateArtGalleryCommand>
    {
        public UpdateArtGalleryValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty().WithMessage("Identificador não encontrado");
            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("Nome é obrigatório.");
            RuleFor(x => x.Manager).NotNull().NotEmpty().WithMessage("Gerente é obrigatório.");
            RuleFor(x => x.City).NotNull().NotEmpty().WithMessage("Cidade é obrigatória.");
            RuleFor(x => x.ArtWorks.Count).GreaterThan(0).WithMessage("Ao menos uma arte é obrigatória.");

            RuleForEach(x => x.ArtWorks).ChildRules(art =>
            {
                art.RuleFor(a => a.Author).NotNull().NotEmpty().WithMessage("Autor da obra é obrigatório");
                art.RuleFor(a => a.Name).NotNull().NotEmpty().WithMessage("Nome da obra é obrigatório");
                art.RuleFor(a => a.AskPrice).NotNull().NotEmpty().GreaterThan(0).WithMessage("Preço da obra é obrigatório e deve ser superior a 0.00");
                art.RuleFor(a => a.CreationYear).LessThanOrEqualTo(DateTime.Now.Year).WithMessage("O ano de criação da obra não pode ser superior ao ano atual");
            });

        }
    }

}
