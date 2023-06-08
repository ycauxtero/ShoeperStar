using Microsoft.EntityFrameworkCore;
using ShoeperStar.Data.Contracts;
using ShoeperStar.Data.Repository;

namespace ShoeperStar.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureSql(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                                    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnectionString"))
                                    );
        }

        public static void ConfigureRepositoryManager(this IServiceCollection services) =>
                                                services.AddScoped<IRepositoryManager, RepositoryManager>();
    }
}
