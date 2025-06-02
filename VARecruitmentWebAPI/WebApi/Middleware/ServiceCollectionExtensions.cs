using FluentValidation;
using VAArtGalleryWebAPI.Application.Commands.Validators;
using VAArtGalleryWebAPI.Application.Commands;
using MediatR;

namespace VAArtGalleryWebAPI.WebApi.Middleware
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureMediatR(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
            serviceCollection.AddScoped<IValidator<CreateArtGalleryCommand>, CreateArtGalleryValidator>();
            serviceCollection.AddScoped(typeof(IPipelineBehavior<,>), typeof(FluentValidationPipelineBehavior<,>));


        }
    }

}
