using ChatApplication.Domain.Abstractions.Auth;
using ChatApplication.Domain.Abstractions.Repositories;
using ChatApplication.Domain.Abstractions.UnitOfWork;
using ChatApplication.Infrastructure.Jwt;
using ChatApplication.Infrastructure.Repositories;
using ChatApplication.Infrastructure.UntiOfWork;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ChatApplication.Infrastructure
{
    public static class Configuration
    {
        public static void AddInfra(this IServiceCollection services,IConfiguration configuration)
        {
            AddRepos(services);
            AddDbContext(services,configuration);
            AddJwtAuth(services);
            AddSignalR(services);
        }

        private static void AddRepos(IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepostiroy>();
            services.AddTransient<IRoomRepository, RoomRepository>();
            services.AddTransient<IMessageRepository, MessageRepository>();
            services.AddTransient<IJwtProvider, JwtProvider>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

        }

        private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                sqlServerOptionsAction : sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure();
                }
                ));
        }
        private static void AddSignalR(IServiceCollection services)
        {
            services.AddSignalR();
        }

        public static void AddJwtAuth(this IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(o => o.TokenValidationParameters = new()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Secret Key")),
                });
        }

    }
}
