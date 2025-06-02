using MediatR;
using System.Xml.Linq;
using VAArtGalleryWebAPI.Domain.Entities;
using VAArtGalleryWebAPI.Domain.Interfaces;

namespace VAArtGalleryWebAPI.Application.Commands
{

    public class CreateArtGalleryCommandHandler : IRequestHandler<CreateArtGalleryCommand, Guid>
    {
        private readonly IArtGalleryRepository _artGalleryRepository;

        public CreateArtGalleryCommandHandler(IArtGalleryRepository artGalleryRepository)
        {
            _artGalleryRepository = artGalleryRepository;
        }

        public async Task<Guid> Handle(CreateArtGalleryCommand request, CancellationToken cancellationToken)
        {
            var newGallery = new ArtGallery(request.Name, request.City, request.Manager);
            
            request.ArtWorks.ForEach(art => newGallery.DisplayNewArtWork(art));

            await _artGalleryRepository.CreateAsync(newGallery, cancellationToken);

            return newGallery.Id;
        }
    }

}
