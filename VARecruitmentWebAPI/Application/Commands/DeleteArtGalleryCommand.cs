using MediatR;

namespace VAArtGalleryWebAPI.Application.Commands
{
    public class DeleteArtGalleryCommand(Guid artGalleryId) : IRequest<bool>
    {
        public Guid ArtGalleryId { get; set; } = artGalleryId;
    }

}
