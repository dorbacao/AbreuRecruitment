using MediatR;
using VAArtGalleryWebAPI.Domain.Entities;
using VAArtGalleryWebAPI.Domain.Interfaces;

namespace VAArtGalleryWebAPI.Application.Commands
{
    public class DeleteArtGalleryCommandHandler : IRequestHandler<DeleteArtGalleryCommand, bool>
    {
        private readonly IArtGalleryRepository _artGalleryRepository;

        public DeleteArtGalleryCommandHandler(IArtGalleryRepository artGalleryRepository)
        {
            _artGalleryRepository = artGalleryRepository;
        }

        public async Task<bool> Handle(DeleteArtGalleryCommand request, CancellationToken cancellationToken)
        {
            var result = await _artGalleryRepository.DeleteArtGalleryAsync(request.ArtGalleryId, cancellationToken);

            return result;
        }
    }

}
