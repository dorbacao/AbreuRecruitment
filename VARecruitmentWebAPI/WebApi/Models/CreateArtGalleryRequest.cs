using VAArtGalleryWebAPI.Application.Commands;

namespace VAArtGalleryWebAPI.WebApi.Models
{
    public class CreateArtGalleryRequest
    {
        public string Name { get; set; }
        public string City { get; set; }
        public string Manager { get; set; }
        public IList<CreateArtWorkRequest> ArtWorks { get; set; } = new List<CreateArtWorkRequest>();

        public CreateArtGalleryCommand ToCommand()
        {
            var artWorks = this
                .ArtWorks
                .Select(artWork => new CreateArtWork(artWork.Name, artWork.Author, artWork.CreationYear, artWork.AskPrice)).ToList();
            var command = new CreateArtGalleryCommand(this.Name, this.City, this.Manager, artWorks);

            return command;

        }
    }
}
