namespace VAArtGalleryWebAPI.WebApi.Models
{
    public class CreateArtWorkRequest
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public int CreationYear { get; set; }
        public decimal AskPrice { get; set; }

    }
}
