using MediatR;
using System.Xml.Linq;
using VAArtGalleryWebAPI.Domain.Entities;
using VAArtGalleryWebAPI.Domain.Interfaces;

namespace VAArtGalleryWebAPI.Application.Commands
{

    public class CreateArtGalleryCommandHandler : IRequestHandler<CreateArtGalleryCommand, ArtGallery>
    {
        private readonly IArtGalleryRepository _artGalleryRepository;

        public CreateArtGalleryCommandHandler(IArtGalleryRepository artGalleryRepository)
        {
            _artGalleryRepository = artGalleryRepository;
        }

        public async Task<ArtGallery> Handle(CreateArtGalleryCommand request, CancellationToken cancellationToken)
        {
            var newGallery = new ArtGallery(request.Name, request.City, request.Manager);

            request.ArtWorks.ForEach(art =>
            {
                var artWork = new ArtWork(art.Name, art.Author, art.CreationYear, art.AskPrice);
                newGallery.DisplayNewArtWork(artWork);
            });

            await _artGalleryRepository.CreateArtGalleryAsync(newGallery, cancellationToken);

            return newGallery;
        }
    }

}
