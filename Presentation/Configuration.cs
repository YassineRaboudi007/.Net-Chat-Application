using ChatApplication.Presentation.HandleErrorHelper;

namespace ChatApplication.Presentation
{
    public static class Configuration
    {
        public static void AddPresentation(this IServiceCollection services)
        {
            services.AddScoped<IErrorHandlerController,ErrorHandlerController>();
        }
    }
}
