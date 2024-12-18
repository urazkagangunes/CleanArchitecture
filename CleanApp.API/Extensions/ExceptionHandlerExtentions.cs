using CleanApp.API.ExceptionHandler;

namespace CleanApp.API.Extensions
{
    public static class ExceptionHandlerExtentions
    {
        public static IServiceCollection AddExceptionHandlerExt(this IServiceCollection services)
        {
            services.AddScoped<CriticalExceptionHandler>();
            services.AddScoped<GlobalExceptionHandler>();

            return services;
        }
    }
}