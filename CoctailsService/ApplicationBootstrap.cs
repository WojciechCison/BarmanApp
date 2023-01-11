using Application.Repositories;
using Application.Services;
using Application.Services.Interfaces;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace CoctailsService
{
    public static class ApplicationBootstrap
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddSingleton<ITokenService, TokenService>();

            //user
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();

            //igridient
            services.AddScoped<IIngridientRepository, IngridientRepository>();
            services.AddScoped<IIngridientService, IngridientService>();

            //coctail
            services.AddScoped<ICoctailRepository, CoctailRepository>();
            services.AddScoped<ICoctailService, CoctailService>();

            return services;
        }

        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = new SqlConnectionStringBuilder(configuration.GetConnectionString("DevelopmentConnectionString"));

            services.AddDbContext<CoctailsContext>(options => options.UseSqlServer(connectionString.ConnectionString));

            return services;
        }
    }
}
