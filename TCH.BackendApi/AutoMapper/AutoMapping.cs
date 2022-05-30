using AutoMapper;
using TCH.Data.Entities;
using TCH.ViewModel.RequestModel;
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
        CreateMap<StockMaterial, StockVm>();

        CreateMap<Measure, MeasuresVm>();
        CreateMap<MeasuresVm, Measure>();

        CreateMap<ExchangeUnitRequest, UnitConversion>();


        CreateMap<RecipeRequest, RecipeDetail>();
        CreateMap<StockRequest, StockMaterial>();
        CreateMap<BranchRequest, Branch>();
        CreateMap<PromotionRequest, Promotion>();
    }
}
