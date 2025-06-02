using MediatR;
using VAArtGalleryWebAPI.Domain.Entities;
using VAArtGalleryWebAPI.Domain.Interfaces;

namespace VAArtGalleryWebAPI.Application.Commands
{
    public class UpdateArtGalleryCommandHandler : IRequestHandler<UpdateArtGalleryCommand, ArtGallery?>
    {
        private readonly IArtGalleryRepository _artGalleryRepository;

        public UpdateArtGalleryCommandHandler(IArtGalleryRepository artGalleryRepository)
        {
            _artGalleryRepository = artGalleryRepository;
        }

        public async Task<ArtGallery?> Handle(UpdateArtGalleryCommand request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            
            var artGallery = await _artGalleryRepository.GetArtGalleryByIdAsync(request.Id);

            if(artGallery == null)
            {
                return null;
            }

            artGallery.Name = request.Name;
            artGallery.City = request.City;
            artGallery.Manager = request.Manager;
            artGallery.ArtWorksOnDisplay?.Clear();

            request.ArtWorks.ForEach(art =>
            {
                var candidateWork = artGallery.ArtWorksOnDisplay?.Find(a => a.Id == art.Id);
                if(candidateWork != null)
                {
                    artGallery.SellArtWork(candidateWork);
                }

                var artWork = new ArtWork(art.Name, art.Author, art.CreationYear, art.AskPrice);
                artGallery.DisplayNewArtWork(artWork);
            });

            cancellationToken.ThrowIfCancellationRequested();

            await _artGalleryRepository.UpdateArtGalleryAsync(artGallery, cancellationToken);

            return artGallery;
        }
    }

}
