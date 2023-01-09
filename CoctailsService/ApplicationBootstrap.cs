using Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace CoctailsService
{
    public static class ApplicationBootstrap
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {

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
