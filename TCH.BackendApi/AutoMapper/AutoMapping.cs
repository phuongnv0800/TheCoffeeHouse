using AutoMapper;
using TCH.BackendApi.Entities;
using TCH.ViewModel.SubModels;

namespace TCH.BackendApi.AutoMapper;

public class AutoMapping : Profile
{
    public AutoMapping()
    {
        // nguồn , đích ex: AppUser user = mapper.Map<AppUser>(model); model la nguồn
        CreateMap<CategoryVm, Category>();
        CreateMap<Category, CategoryVm>();
        CreateMap<ProductImage, ProductImageVm>();
        CreateMap<ProductImageVm, ProductImage>();
        CreateMap<Product, ProductVm>();
        CreateMap<ProductRequest, Product>();
        CreateMap<Size, SizeVm>();
        CreateMap<SizeVm, Size>();

        CreateMap<Topping, ToppingVm>();
        CreateMap<ToppingVm, Topping>();

        CreateMap<BranchRequest, Branch>();
    }
}
