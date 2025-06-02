using VAArtGalleryWebAPI.Domain.Entities;

namespace VAArtGalleryWebAPI.Domain.Interfaces
{
    public interface IArtGalleryRepository
    {
        Task<List<ArtGallery>> GetAllArtGalleriesAsync(CancellationToken cancellationToken = default);
        Task<ArtGallery?> GetArtGalleryByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<ArtGallery> CreateArtGalleryAsync(ArtGallery artGallery, CancellationToken cancellationToken = default);
        Task<ArtGallery> UpdateArtGalleryAsync(ArtGallery artGallery, CancellationToken cancellationToken = default);
        Task<bool> DeleteArtGalleryAsync(Guid artGalleryId, CancellationToken cancellationToken = default);
    }
}