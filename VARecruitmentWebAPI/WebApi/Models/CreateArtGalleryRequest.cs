namespace VAArtGalleryWebAPI.WebApi.Models
{
    public class CreateArtGalleryRequest(string name, string city, string manager)
    {
        public string Name { get; set; } = name;
        public string City { get; set; } = city;
        public string Manager { get; set; } = manager;
    }
}
