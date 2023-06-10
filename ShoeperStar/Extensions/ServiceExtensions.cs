using Microsoft.EntityFrameworkCore;
using ShoeperStar.Data.Contracts;
using ShoeperStar.Data.Repository;
using ShoeperStar.Data.Services;

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

        public static void ConfigureHashIdService(this IServiceCollection services, IConfiguration Configuration)
        {
            var salt = Configuration.GetValue<string>("HashId:Salt");
            services.AddSingleton(typeof(HashIdService), new HashIdService(salt));
        }
    }
}
