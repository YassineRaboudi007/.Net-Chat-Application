using ChatApplication.Application.Behaviors;
using FluentValidation;
using MediatR;

namespace ChatApplication.Application
{
    public static class Configuration
    {
        public static void AddApplication(this IServiceCollection services)
        {
            AddFluentValidation(services);
            AddMediator(services);
            AddConnectedUserRegister(services);
        }

        private static void AddMediator(IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipeLineBeahvior<,>));
        }

        public static void AddFluentValidation(IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(typeof(Program).Assembly, includeInternalTypes: true);
        }

        public static void AddConnectedUserRegister(IServiceCollection services)
        {
            services.AddSingleton<IList<Guid>>(opts => new List<Guid>());
        }

    }
}
