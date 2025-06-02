using MediatR;
using VAArtGalleryWebAPI.Domain.Entities;
using VAArtGalleryWebAPI.Domain.Interfaces;

namespace VAArtGalleryWebAPI.Application.Queries
{
    public class GetArtGalleryByIdQueryHandler(IArtGalleryRepository artGalleryRepository) : IRequestHandler<GetArtGalleryByIdQuery, ArtGallery>
    {
        public async Task<ArtGallery?> Handle(GetArtGalleryByIdQuery request, CancellationToken cancellationToken)
        {
            return await artGalleryRepository.GetArtGalleryByIdAsync(request.GalleryId, cancellationToken);
        }
    }
}
