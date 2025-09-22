using AutoMapper;
using PRN232.Lab1.CoffeeStore.APIS.RequestModels;
using PRN232.Lab1.CoffeeStore.APIS.ResponseModels;
using PRN232.Lab1.CoffeeStore.Services.BusinessModels;

namespace PRN232.Lab1.CoffeeStore.APIS.Mapping
{
    public class ApiMappingProfile : Profile
    {
        public ApiMappingProfile()
        {
            // -------- Product --------
            CreateMap<CreateProductRequest, ProductModel>();
            CreateMap<UpdateProductRequest, ProductModel>();
            CreateMap<ProductModel, ProductResponse>();


            // CreateMenuRequest -> MenuModel
            CreateMap<CreateMenuRequest, MenuModel>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products));

            // UpdateMenuRequest -> MenuModel
            CreateMap<UpdateMenuRequest, MenuModel>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products));

            // CreateMenuProductRequest -> MenuProductModel
            CreateMap<CreateMenuProductRequest, MenuProductModel>();

            // MenuModel -> MenuResponse
            CreateMap<MenuModel, MenuResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products));

            // MenuProductModel -> MenuProductResponse
            CreateMap<MenuProductModel, MenuProductResponse>();
        }
    }
}
