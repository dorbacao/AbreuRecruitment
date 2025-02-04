using VAArtGalleryWebAPI.Domain.Interfaces;

namespace VAArtGalleryWebAPI.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            string repositoryPath = configuration["RepositoryPath"] ?? throw new Exception("Undefined repository path");

            services.AddSingleton<IArtGalleryRepository>(new ArtGalleryRepository(repositoryPath));
            services.AddSingleton<IArtWorkRepository>(new ArtWorkRepository(repositoryPath));
            return services;
        }
    }
}
