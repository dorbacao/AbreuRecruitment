using FluentValidation;
using MediatR;
using VAArtGalleryWebAPI.Application.Commands.Validators;
using VAArtGalleryWebAPI.Application.Commands;
using VAArtGalleryWebAPI.Domain.Interfaces;
using VAArtGalleryWebAPI.WebApi.Middleware;

namespace VAArtGalleryWebAPI.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            string repositoryPath = configuration["RepositoryPath"] ?? throw new Exception("Undefined repository path");

            services.AddScoped<IArtGalleryRepository>(serviceProvider => new ArtGalleryRepository(repositoryPath));
            services.AddScoped<IArtWorkRepository>(serviceProvider => new ArtWorkRepository(repositoryPath));

            return services;
        }

        public static IServiceCollection ConfigureMediatR(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
            services.AddScoped<IValidator<CreateArtGalleryCommand>, CreateArtGalleryValidator>();
            services.AddScoped<IValidator<UpdateArtGalleryCommand>, UpdateArtGalleryValidator>();
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(FluentValidationPipelineBehavior<,>));
            return services;
        }
    }
}
