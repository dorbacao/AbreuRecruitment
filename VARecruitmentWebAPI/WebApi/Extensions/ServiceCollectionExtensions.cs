using FluentValidation;
using VAArtGalleryWebAPI.Application.Commands.Validators;
using VAArtGalleryWebAPI.Application.Commands;

namespace VAArtGalleryWebAPI.WebApi.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureMediatR(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
            serviceCollection.AddScoped<IValidator<CreateArtGalleryCommand>, CreateArtGalleryValidator>();
        }
    }
}
