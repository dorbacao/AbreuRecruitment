using System.Text.Json;
using VAArtGalleryWebAPI.Domain.Interfaces;
using VAArtGalleryWebAPI.Domain.Entities;

namespace VAArtGalleryWebAPI.Infrastructure
{
    public class ArtGalleryRepository(string filePath) : IArtGalleryRepository
    {
        private readonly string _filePath = filePath;

        public async Task<List<ArtGallery>> GetAllArtGalleriesAsync(CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using StreamReader sr = new(_filePath);
            string galleriesJson = await sr.ReadToEndAsync();
            return JsonSerializer.Deserialize<List<ArtGallery>>(galleriesJson) ?? [];
        }

        public async Task<ArtGallery?> GetArtGalleryByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var galleries = await GetAllArtGalleriesAsync(cancellationToken);
            return galleries.Find(g => g.Id == id);
        }

        public async Task<ArtGallery> CreateArtGalleryAsync(ArtGallery artGallery, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var galleries = await GetAllArtGalleriesAsync(cancellationToken);

            artGallery.Id = Guid.NewGuid();
            galleries.Add(artGallery);

            await UpdateGalleryAsync(galleries);

            return artGallery;

        }

        public async Task<bool> DeleteArtGalleryAsync(Guid artGalleryId, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var galleries = await GetAllArtGalleriesAsync(cancellationToken);

            var count = galleries.RemoveAll(a => a.Id == artGalleryId);

            cancellationToken.ThrowIfCancellationRequested();

            if(count == 0)
            {
                return false;
            }
            else
            {
                await UpdateGalleryAsync(galleries);
            }

            return true;
        }

        private async Task UpdateGalleryAsync(IList<ArtGallery> allArtGallery)
        {
            using TextWriter tw = new StreamWriter(_filePath, false);
            await tw.WriteAsync(JsonSerializer.Serialize(allArtGallery));
        }
    }
}
