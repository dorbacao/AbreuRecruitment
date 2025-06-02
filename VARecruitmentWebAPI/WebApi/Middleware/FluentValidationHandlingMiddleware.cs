using FluentValidation;

namespace VAArtGalleryWebAPI.WebApi.Middleware
{
    public class FluentValidationHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<FluentValidationHandlingMiddleware> _logger;

        public FluentValidationHandlingMiddleware(RequestDelegate next, ILogger<FluentValidationHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {
                _logger.LogError(ex, "Erro inesperado no servidor");
                await HandleExceptionAsync(context, ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado no servidor");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, ValidationException exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status400BadRequest;

            var messages = string.Join(Environment.NewLine, exception.Errors.Select(a => a.ErrorMessage).ToList());

            var response = new { message = messages };

            return context.Response.WriteAsJsonAsync(response);
        }
        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status400BadRequest;

            var response = new { message = exception.Message };

            return context.Response.WriteAsJsonAsync(response);
        }
    }

}
