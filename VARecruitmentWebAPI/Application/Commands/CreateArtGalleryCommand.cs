using MediatR;
using VAArtGalleryWebAPI.Domain.Entities;

namespace VAArtGalleryWebAPI.Application.Commands
{
    public class CreateArtGalleryCommand(string name, string city, string manager, IList<CreateArtWork> artWorks) : IRequest<ArtGallery>
    {
        public string Name { get; set; } = name;
        public string City { get; set; } = city;
        public string Manager { get; set; } = manager;
        public List<CreateArtWork> ArtWorks { get; set; } = artWorks.ToList();

    }

}
