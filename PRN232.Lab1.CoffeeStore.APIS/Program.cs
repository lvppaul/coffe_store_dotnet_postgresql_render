using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PRN232.Lab1.CoffeeStore.APIS.Filters;
using PRN232.Lab1.CoffeeStore.APIS.Mapping;
using PRN232.Lab1.CoffeeStore.APIS.Middleware;
using PRN232.Lab1.CoffeeStore.Repositories.Context;
using PRN232.Lab1.CoffeeStore.Repositories.Interfaces;
using PRN232.Lab1.CoffeeStore.Repositories.Repositories;
using PRN232.Lab1.CoffeeStore.Services.Interfaces;
using PRN232.Lab1.CoffeeStore.Services.Mapping;
using PRN232.Lab1.CoffeeStore.Services.Services;
using System;

namespace PRN232.Lab1.CoffeeStore.APIS
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<CoffeeStoreContext>(options =>
      options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IMenuRepository, MenuRepository>();
            builder.Services.AddScoped<IMenuService, MenuService>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            // AutoMapper via DI package (scans profiles in these assemblies)
            builder.Services.AddAutoMapper(cfg => { },
                typeof(ServiceMappingProfile).Assembly,
                typeof(ApiMappingProfile).Assembly);

            builder.Services.AddControllers(options =>
            {
                options.Filters.Add<ValidationFilter>(); // Bắt validation error
            });


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
          
        

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();

                // Chỉ bật redirect HTTPS khi chạy local (Development)
                app.UseHttpsRedirection();
            }
            else
            {
                // Với Render (Production), không ép HTTPS
                app.UseForwardedHeaders(new ForwardedHeadersOptions
                {
                    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
                });
            }


            app.UseErrorHandlingMiddleware();


            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
