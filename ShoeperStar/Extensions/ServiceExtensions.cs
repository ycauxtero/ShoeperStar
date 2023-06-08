using Microsoft.EntityFrameworkCore;

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
    }
}
