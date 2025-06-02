using System.Text.Json;
using VAArtGalleryWebAPI.Domain.Interfaces;
using VAArtGalleryWebAPI.Domain.Entities;

namespace VAArtGalleryWebAPI.Infrastructure
{
    public class ArtWorkRepository(string filePath) : IArtWorkRepository
    {
        private readonly string _filePath = filePath;

        public async Task<ArtWork> CreateAsync(Guid artGalleryId, ArtWork artWork, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var galleries = await new ArtGalleryRepository(_filePath).GetAllArtGalleriesAsync(cancellationToken);

            var gallery = galleries.Find(g => g.Id == artGalleryId) ?? throw new ArgumentException("unknown art gallery", nameof(artGalleryId));
            artWork.Id = Guid.NewGuid();

            if (gallery.ArtWorksOnDisplay == null)
            {
                gallery.ArtWorksOnDisplay = [artWork];
            }
            else
            {
                gallery.ArtWorksOnDisplay.Add(artWork);
            }

            cancellationToken.ThrowIfCancellationRequested();

            await UpdateGalleries(galleries);

            return artWork;
        }

        public async Task<bool> DeleteAsync(Guid artWorkId, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var artGalleryRepository = new ArtGalleryRepository(_filePath);

            var galleries = await artGalleryRepository.GetAllArtGalleriesAsync(cancellationToken);

            var gallery = galleries.Find(a => a.ArtWorksOnDisplay?.Any(art => art.Id == artWorkId) == true);

            if (gallery == null)
            {
                return false;
            }

            var artWork = gallery.ArtWorksOnDisplay?.Find(art => art.Id == artWorkId) ?? throw new ArgumentException("unknown art work", nameof(artWorkId));

            gallery.SellArtWork(artWork);

            cancellationToken.ThrowIfCancellationRequested();

            await UpdateGalleries(galleries);

            return true;
        }

        public async Task<List<ArtWork>> GetArtWorksByGalleryIdAsync(Guid artGalleryId, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var galleries = await new ArtGalleryRepository(_filePath).GetAllArtGalleriesAsync(cancellationToken);

            var gallery = galleries.Find(g => g.Id == artGalleryId) ?? throw new ArgumentException("unknown art gallery", nameof(artGalleryId));

            return gallery.ArtWorksOnDisplay ?? [];
        }

        private async Task UpdateGalleries(List<ArtGallery> galleries)
        {
            /*
             O uso de Task.Run() neste método não parece necessário. 

             Essa abordagem poderia fazer sentido se não houvesse uma alternativa assíncrona disponível 
             para operações de I/O, mas este não é o caso. 

             A classe TextWriter já oferece suporte para operações assíncronas por meio do método WriteAsync(), 
             tornando desnecessário o uso de Task.Run(). Além disso, utilizar Task.Run() para operações de I/O 
             geralmente não é recomendado, pois pode levar a um uso ineficiente de threads.

             Uma abordagem melhor seria chamar diretamente WriteAsync() para garantir que a operação 
             seja verdadeiramente assíncrona e aproveite os benefícios do modelo de programação assíncrona.
             */
            using TextWriter tw = new StreamWriter(_filePath, false);
            await tw.WriteAsync(JsonSerializer.Serialize(galleries));
        }
    }
}