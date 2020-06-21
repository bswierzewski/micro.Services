using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Database.Migrations
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services) {

            services.AddDbContext<DataContext>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) { }
    }
}
