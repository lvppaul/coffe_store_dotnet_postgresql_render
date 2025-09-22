using AutoMapper;
using PRN232.Lab1.CoffeeStore.Repositories.Models;
using PRN232.Lab1.CoffeeStore.Services.BusinessModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232.Lab1.CoffeeStore.Services.Mapping
{
    public class ServiceMappingProfile : Profile
    {
        public ServiceMappingProfile()
        {
            // -------- Product --------
            CreateMap<Product, ProductModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));

            CreateMap<ProductModel, Product>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Category, opt => opt.Ignore()); // ❗ không map Category object

            // Menu
            CreateMap<Menu, MenuModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.MenuId))
                .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.ProductInMenus));

            CreateMap<MenuModel, Menu>()
                .ForMember(dest => dest.MenuId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ProductInMenus, opt => opt.MapFrom(src => src.Products));

            // ProductInMenu
            CreateMap<ProductInMenu, MenuProductModel>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Product.Price));

            CreateMap<MenuProductModel, ProductInMenu>()
                .ForMember(dest => dest.Product, opt => opt.Ignore()); 

            // -------- Category -------- //
            CreateMap<Repositories.Models.Category, CategoryModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.CategoryId))
                .ReverseMap();
        }
    }
}
