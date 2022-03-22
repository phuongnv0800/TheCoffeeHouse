using AutoMapper;
using TCH.BackendApi.Entities;

namespace TCH.BackendApi.ViewModels
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            // nguồn , đích
            CreateMap<CategoryVm, Category>();
            CreateMap<Category, CategoryVm>();
            CreateMap<ProductImage, ProductImageVm>();
            CreateMap<ProductImageVm, ProductImage>();
            CreateMap<Product, ProductVm>();
            CreateMap<ProductRequest, Product>();
            CreateMap<Size, SizeVm>();
            CreateMap<SizeVm, Size>();
        }
    }
}
