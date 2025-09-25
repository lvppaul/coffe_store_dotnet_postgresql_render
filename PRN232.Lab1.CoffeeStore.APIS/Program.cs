using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.HttpOverrides;
using PRN232.Lab1.CoffeeStore.APIS.Filters;
using PRN232.Lab1.CoffeeStore.APIS.Mapping;
using PRN232.Lab1.CoffeeStore.APIS.Middleware;
using PRN232.Lab1.CoffeeStore.Repositories.Context;
using PRN232.Lab1.CoffeeStore.Repositories.Interfaces;
using PRN232.Lab1.CoffeeStore.Repositories.Repositories;
using PRN232.Lab1.CoffeeStore.Services.Interfaces;
using PRN232.Lab1.CoffeeStore.Services.Mapping;
using PRN232.Lab1.CoffeeStore.Services.Services;

namespace PRN232.Lab1.CoffeeStore.APIS
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ====== Services ======
            builder.Services.AddDbContext<CoffeeStoreContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IMenuRepository, MenuRepository>();
            builder.Services.AddScoped<IMenuService, MenuService>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();

            builder.Services.AddAutoMapper(cfg => { },
                typeof(ServiceMappingProfile).Assembly,
                typeof(ApiMappingProfile).Assembly);

            builder.Services.AddControllers(options =>
            {
                options.Filters.Add<ValidationFilter>();
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // ====== Middleware ======
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseHttpsRedirection();
            }
            else
            {
                // Render: tin cậy header từ proxy
                var forwardedHeaderOptions = new ForwardedHeadersOptions
                {
                    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
                };
                forwardedHeaderOptions.KnownNetworks.Clear();
                forwardedHeaderOptions.KnownProxies.Clear();
                app.UseForwardedHeaders(forwardedHeaderOptions);
            }

            app.UseErrorHandlingMiddleware();

            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
