using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Assioma.Ecomm.Application.Service;
using Assioma.Ecomm.Data;
using Assioma.Ecomm.Domain;
using Assioma.Ecomm.Domain.InfrastructureContracts;
using Assioma.Ecomm.Data.InMemoryTest;

namespace Assioma.Ecomm.Presentation.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            // Add Automapper
            services.AddAutoMapper(typeof(Assioma.Ecomm.Presentation.Automapper.AutomapperViewModel));

            // Register components
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IRepository<Product>, Repository<Product>>();

            // For make the project to run with in-memory database
            services.AddScoped<DbContext, EcommInMemoryContext>();
            services.AddDbContext<EcommInMemoryContext>(options => options.UseInMemoryDatabase("EcommInMemoryDB"));

            // Real database
            //services.AddScoped<DbContext, EcommContext>();
            //services.AddDbContext<EcommContext>(options => options.UseSqlServer(Configuration.GetConnectionString("EcommDBConnection")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Product}/{action=Index}/{id?}");
            });
        }
    }
}
