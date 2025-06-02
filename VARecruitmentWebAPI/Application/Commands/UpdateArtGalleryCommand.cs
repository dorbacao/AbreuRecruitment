using MediatR;
using VAArtGalleryWebAPI.Domain.Entities;

namespace VAArtGalleryWebAPI.Application.Commands
{
    public class UpdateArtGalleryCommand(Guid id, string name, string city, string manager, IList<UpdateArtWork> artWorks) : IRequest<ArtGallery>
    {
        public Guid Id { get; set; } = id;
        public string Name { get; set; } = name;
        public string City { get; set; } = city;
        public string Manager { get; set; } = manager;
        public List<UpdateArtWork> ArtWorks { get; set; } = artWorks.ToList();

    }

}
