namespace VAArtGalleryWebAPI.WebApi.Models
{
    public class UpdateArtWorkRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public int CreationYear { get; set; }
        public decimal AskPrice { get; set; }
    }

}
