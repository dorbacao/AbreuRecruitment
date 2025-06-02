using VAArtGalleryWebAPI.Application.Commands;

namespace VAArtGalleryWebAPI.WebApi.Models
{
    public class UpdateArtGalleryRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Manager { get; set; }
        public IList<UpdateArtWorkRequest> ArtWorks { get; set; } = new List<UpdateArtWorkRequest>();

        public UpdateArtGalleryCommand ToCommand()
        {
            var artWorks = this
                .ArtWorks
                .Select(artWork => new UpdateArtWork(artWork.Id, artWork.Name, artWork.Author, artWork.CreationYear, artWork.AskPrice)).ToList();
            var command = new UpdateArtGalleryCommand(this.Id, this.Name, this.City, this.Manager, artWorks);

            return command;

        }
    }
}
