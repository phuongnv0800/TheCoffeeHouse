using Microsoft.AspNetCore.Identity;
using TCH.BackendApi.Entities;
using TCH.BackendApi.Models.Enum;

namespace TCH.BackendApi.EF
{
    public static class DbInitializer
    {
        public static void Initialize(APIContext context)
        {
            context.Database.EnsureCreated();
            if (context.Categories.Any())
            {
                return;   // DB has been seeded
            }
            var categories = new Category[]{
                new Category() {
                    ID= "f7c75354-48e8-49b5-9ea4-dca7b81888df",
                    Name = "Cà Phê Việt Nam",
                    UpdateDate = DateTime.Now,
                    CreateDate = DateTime.Now,
                    Description = "Mặc định",
                },
                new Category() {
                    ID="3287ce53-a833-4d4c-b821-a2d5b5cbced5",
                    Name = "Cà Phê Máy",
                    UpdateDate = DateTime.Now,
                    CreateDate = DateTime.Now,
                    Description = "Mặc định",
                },
                new Category() {
                    ID= "0e91fcb8-fa59-4a86-9af9-e608d3175caa",
                    Name = "Cold Brew",
                    UpdateDate = DateTime.Now,
                    CreateDate = DateTime.Now,
                    Description = "Mặc định",
                },
                new Category() {
                    ID= "4047a9ea-36a5-4b6b-a293-e3a914281736",
                    Name = "Trà trái cây",
                    UpdateDate = DateTime.Now,
                    CreateDate = DateTime.Now,
                    Description = "Mặc định",
                },
                new Category() {
                    ID= "6a53708b-87bc-4b17-8fea-2a7bb856ba9b",
                    Name = "Trà sữa Macchiato",
                    UpdateDate = DateTime.Now,
                    CreateDate = DateTime.Now,
                    Description = "Mặc định",
                },
                new Category() {
                    ID= "5922b0dc-51ef-48de-9d9f-48bfff9c2552",
                    Name = "Đá xay",
                    UpdateDate = DateTime.Now,
                    CreateDate = DateTime.Now,
                    Description = "Mặc định",
                },
                new Category() {
                    ID= "134aac36-958e-4a30-9887-85996c2a9771",
                    Name = "Matcha - Sô cô la",
                    UpdateDate = DateTime.Now,
                    CreateDate = DateTime.Now,
                    Description = "Mặc định",
                },
                new Category() {
                    ID= "44e2f99f-cd95-4b89-a3d3-553cf86ff173",
                    Name = "Bánh mặn",
                    UpdateDate = DateTime.Now,
                    CreateDate = DateTime.Now,
                    Description = "Mặc định",
                },
                new Category() {
                    ID= "a93ba116-4a2c-4d0a-aa79-a7b37cd48cb8",
                    Name = "Bánh ngọt",
                    UpdateDate = DateTime.Now,
                    CreateDate = DateTime.Now,
                    Description = "Mặc định",
                },
                new Category() {
                    ID= "3b9db312-c2e6-4d71-b7f3-406c6182eb5d",
                    Name = "Snack",
                    UpdateDate = DateTime.Now,
                    CreateDate = DateTime.Now,
                    Description = "Mặc định",
                },
                new Category() {
                    ID= "2d79d339-660c-468b-9d40-ef62cc9a5baa",
                    Name = "Cà phê tại nhà",
                    UpdateDate = DateTime.Now,
                    CreateDate = DateTime.Now,
                    Description = "Mặc định",
                },
                new Category() {
                    ID= "ea108c9e-ae7a-4e0f-a1d0-f7d14c079cff",
                    Name = "Trà tại nhà",
                    UpdateDate = DateTime.Now,
                    CreateDate = DateTime.Now,
                    Description = "Mặc định",
                },
                new Category() {
                    ID= "2e008313-4f56-40c0-89a9-01abe3d5e22f",
                    Name = "Bộ sưu tập quà tặng",
                    UpdateDate = DateTime.Now,
                    CreateDate = DateTime.Now,
                    Description = "Mặc định",
                },
                new Category() {
                    ID= "4f0a40b1-b135-4f50-ba4c-8568e7d5e089",
                    Name = "Combo",
                    UpdateDate = DateTime.Now,
                    CreateDate = DateTime.Now,
                    Description = "Mặc định",
                },
            };
            foreach (var item in categories)
            {
                context.Categories.Add(item);
            }
            context.SaveChanges();
            var products = new Product[]
            {
                new Product()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name =" Latte Táo Lê Quế Nóng",

                    ProductType  = ProductType.Drink,
                    CreateDate =DateTime.Now,
                    UpdateDate =DateTime.Now,
                    IsSale  = false,
                    PriceSale =  58000,
                    IsAvailable =true,

                    Formula ="",
                    Price = 58000,
                    Description ="",

                    LinkImage ="/images/latte-tao-le-que-nong.webp",
                    CategoryID ="3287ce53-a833-4d4c-b821-a2d5b5cbced5",
                },
                new Product()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name =" Latte Táo Lê Quế Đá",

                    ProductType  = ProductType.Drink,
                    CreateDate =DateTime.Now,
                    UpdateDate =DateTime.Now,
                    IsSale  = false,
                    PriceSale =  58000,
                    IsAvailable =true,

                    Formula ="",
                    Price = 58000,
                    Description ="",

                    LinkImage ="/images/latte-tao-le-que-da.webp",
                    CategoryID ="3287ce53-a833-4d4c-b821-a2d5b5cbced5",
                },
                new Product()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name =" Latte Táo Lê Quế Chai Fresh 500ml",

                    ProductType  = ProductType.Drink,
                    CreateDate =DateTime.Now,
                    UpdateDate =DateTime.Now,
                    IsSale  = false,
                    PriceSale =  107000,
                    IsAvailable =true,

                    Formula ="",
                    Price = 107000,
                    Description ="",

                    LinkImage ="/images/latte-tao-le-que-chai-fresh-500ml.webp",
                    CategoryID ="3287ce53-a833-4d4c-b821-a2d5b5cbced5",
                },
                new Product() {
                    ID = Guid.NewGuid().ToString(),
                    Name ="Mocha Nóng",

                    ProductType  = ProductType.Drink,
                    CreateDate =DateTime.Now,
                    UpdateDate =DateTime.Now,
                    IsSale  = false,
                    PriceSale =  49000,
                    IsAvailable =true,

                    Formula ="",
                    Price = 49000,
                    Description ="",

                    LinkImage ="/images/ mochanong.webp",
                    CategoryID ="3287ce53-a833-4d4c-b821-a2d5b5cbced5",
                },
                new Product()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name ="Mocha Đá",

                    ProductType  = ProductType.Drink,
                    CreateDate =DateTime.Now,
                    UpdateDate =DateTime.Now,
                    IsSale  = false,
                    PriceSale =  49000,
                    IsAvailable =true,

                    Formula ="",
                    Price = 49000,
                    Description ="",

                    LinkImage ="/images/mocha-da.webp",
                    CategoryID ="3287ce53-a833-4d4c-b821-a2d5b5cbced5",
                },
                new Product()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name =" Espresso Nóng",

                    ProductType  = ProductType.Drink,
                    CreateDate =DateTime.Now,
                    UpdateDate =DateTime.Now,
                    IsSale  = false,
                    PriceSale =  40000,
                    IsAvailable =true,

                    Formula ="",
                    Price = 40000,
                    Description ="",

                    LinkImage ="/images/espressonong.webp",
                    CategoryID ="3287ce53-a833-4d4c-b821-a2d5b5cbced5",
                },
                new Product()
                                {
                                    ID = Guid.NewGuid().ToString(),
                                    Name =" Espresso Đá",

                                    ProductType  = ProductType.Drink,
                                    CreateDate =DateTime.Now,
                                    UpdateDate =DateTime.Now,
                                    IsSale  = false,
                                    PriceSale =  45000,
                                    IsAvailable =true,

                                    Formula ="",
                                    Price = 45000,
                                    Description ="",

                                    LinkImage ="/images/cfdenda-espressoda.webp",
                                    CategoryID ="3287ce53-a833-4d4c-b821-a2d5b5cbced5",
                                },
                new Product()
                                {
                                    ID = Guid.NewGuid().ToString(),
                                    Name =" Cappuccino Nóng",

                                    ProductType  = ProductType.Drink,
                                    CreateDate =DateTime.Now,
                                    UpdateDate =DateTime.Now,
                                    IsSale  = false,
                                    PriceSale =  49000,
                                    IsAvailable =true,

                                    Formula ="",
                                    Price = 49000,
                                    Description ="",

                                    LinkImage ="/images/cappuccino.webp",
                                    CategoryID ="3287ce53-a833-4d4c-b821-a2d5b5cbced5",
                                },
                new Product()
                                {
                                    ID = Guid.NewGuid().ToString(),
                                    Name =" Cappuccino Đá",

                                    ProductType  = ProductType.Drink,
                                    CreateDate =DateTime.Now,
                                    UpdateDate =DateTime.Now,
                                    IsSale  = false,
                                    PriceSale =  49000,
                                    IsAvailable =true,

                                    Formula ="",
                                    Price = 49000,
                                    Description ="",

                                    LinkImage ="/images/mocha-da.webp",
                                    CategoryID ="3287ce53-a833-4d4c-b821-a2d5b5cbced5",
                                },
                new Product()
                                {
                                    ID = Guid.NewGuid().ToString(),
                                    Name =" Americano Nóng",

                                    ProductType  = ProductType.Drink,
                                    CreateDate =DateTime.Now,
                                    UpdateDate =DateTime.Now,
                                    IsSale  = false,
                                    PriceSale =  40000,
                                    IsAvailable =true,

                                    Formula ="",
                                    Price = 40000,
                                    Description ="",

                                    LinkImage ="/images/americano-nong.webp",
                                    CategoryID ="3287ce53-a833-4d4c-b821-a2d5b5cbced5",
                                },
                new Product()
                                {
                                    ID = Guid.NewGuid().ToString(),
                                    Name =" Latte Đá",

                                    ProductType  = ProductType.Drink,
                                    CreateDate =DateTime.Now,
                                    UpdateDate =DateTime.Now,
                                    IsSale  = false,
                                    PriceSale =  49000,
                                    IsAvailable =true,

                                    Formula ="",
                                    Price = 49000,
                                    Description ="",

                                    LinkImage ="/images/latte-da.webp",
                                    CategoryID ="3287ce53-a833-4d4c-b821-a2d5b5cbced5",
                                },
                new Product()
                                {
                                    ID = Guid.NewGuid().ToString(),
                                    Name =" Caramel Macchiato Nóng",

                                    ProductType  = ProductType.Drink,
                                    CreateDate =DateTime.Now,
                                    UpdateDate =DateTime.Now,
                                    IsSale  = false,
                                    PriceSale =  49000,
                                    IsAvailable =true,

                                    Formula ="",
                                    Price = 49000,
                                    Description ="",

                                    LinkImage ="/images/caramel-macchiato-nong.webp",
                                    CategoryID ="3287ce53-a833-4d4c-b821-a2d5b5cbced5",
                                },
                new Product()
                                {
                                    ID = Guid.NewGuid().ToString(),
                                    Name =" Caramel Macchiato Đá",

                                    ProductType  = ProductType.Drink,
                                    CreateDate =DateTime.Now,
                                    UpdateDate =DateTime.Now,
                                    IsSale  = false,
                                    PriceSale =  49000,
                                    IsAvailable =true,

                                    Formula ="",
                                    Price = 49000,
                                    Description ="",

                                    LinkImage ="/images/caramel-macchiato-da.webp",
                                    CategoryID ="3287ce53-a833-4d4c-b821-a2d5b5cbced5",
                                },
                new Product()
                                {
                                    ID = Guid.NewGuid().ToString(),
                                    Name =" Latte Nóng",

                                    ProductType  = ProductType.Drink,
                                    CreateDate =DateTime.Now,
                                    UpdateDate =DateTime.Now,
                                    IsSale  = false,
                                    PriceSale =  49000,
                                    IsAvailable =true,

                                    Formula ="",
                                    Price = 49000,
                                    Description ="",

                                    LinkImage ="/images/latte.webp",
                                    CategoryID ="3287ce53-a833-4d4c-b821-a2d5b5cbced5",
                                },
                new Product()
                                {
                                    ID = Guid.NewGuid().ToString(),
                                    Name =" Americano Đá",

                                    ProductType  = ProductType.Drink,
                                    CreateDate =DateTime.Now,
                                    UpdateDate =DateTime.Now,
                                    IsSale  = false,
                                    PriceSale =  40000,
                                    IsAvailable =true,

                                    Formula ="",
                                    Price = 40000,
                                    Description ="",

                                    LinkImage ="/images/latte.webp",
                                    CategoryID ="3287ce53-a833-4d4c-b821-a2d5b5cbced5",
                                },
                new Product()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name =" Bạc Sỉu Đá",

                    ProductType  = ProductType.Drink,
                    CreateDate =DateTime.Now,
                    UpdateDate =DateTime.Now,
                    IsSale  = false,
                    PriceSale =  29000,
                    IsAvailable =true,

                    Formula ="",
                    Price = 29000,
                    Description ="",

                    LinkImage ="/images/bac-siu-da.webp ",
                    CategoryID ="f7c75354-48e8-49b5-9ea4-dca7b81888df",
                },
                new Product()
                                {
                                    ID = Guid.NewGuid().ToString(),
                                    Name =" Bạc Sỉu Nóng",

                                    ProductType  = ProductType.Drink,
                                    CreateDate =DateTime.Now,
                                    UpdateDate =DateTime.Now,
                                    IsSale  = false,
                                    PriceSale =  35000,
                                    IsAvailable =true,

                                    Formula ="",
                                    Price = 35000,
                                    Description ="",

                                    LinkImage ="/images/bac-siu-nong.webp ",
                                    CategoryID ="f7c75354-48e8-49b5-9ea4-dca7b81888df",
                                },
                new Product()
                                {
                                    ID = Guid.NewGuid().ToString(),
                                    Name =" Cà Phê Đen Đá",

                                    ProductType  = ProductType.Drink,
                                    CreateDate =DateTime.Now,
                                    UpdateDate =DateTime.Now,
                                    IsSale  = false,
                                    PriceSale =  29000,
                                    IsAvailable =true,

                                    Formula ="",
                                    Price = 29000,
                                    Description ="",

                                    LinkImage ="/images/cfdenda-espressoda.webp ",
                                    CategoryID ="f7c75354-48e8-49b5-9ea4-dca7b81888df",
                                },
                new Product()
                                {
                                    ID = Guid.NewGuid().ToString(),
                                    Name =" Cà Phê Đen Nóng",

                                    ProductType  = ProductType.Drink,
                                    CreateDate =DateTime.Now,
                                    UpdateDate =DateTime.Now,
                                    IsSale  = false,
                                    PriceSale =  35000,
                                    IsAvailable =true,

                                    Formula ="",
                                    Price = 35000,
                                    Description ="",

                                    LinkImage ="/images/ca-phe-den-nong.webp",
                                    CategoryID ="f7c75354-48e8-49b5-9ea4-dca7b81888df",
                                },
                new Product()
                                {
                                    ID = Guid.NewGuid().ToString(),
                                    Name =" Cà Phê Sữa Đá",

                                    ProductType  = ProductType.Drink,
                                    CreateDate =DateTime.Now,
                                    UpdateDate =DateTime.Now,
                                    IsSale  = false,
                                    PriceSale =  29000,
                                    IsAvailable =true,

                                    Formula ="",
                                    Price = 29000,
                                    Description ="",

                                    LinkImage ="/images/ca-phe-sua-da.webp",
                                    CategoryID ="f7c75354-48e8-49b5-9ea4-dca7b81888df",
                                },
                new Product()
                                {
                                    ID = Guid.NewGuid().ToString(),
                                    Name =" Cà Phê Sữa Đá Chai Fresh 250ml",

                                    ProductType  = ProductType.Drink,
                                    CreateDate =DateTime.Now,
                                    UpdateDate =DateTime.Now,
                                    IsSale  = false,
                                    PriceSale =  78000,
                                    IsAvailable =true,

                                    Formula ="",
                                    Price = 78000,
                                    Description ="",

                                    LinkImage ="/images/bottlecfsd.webp",
                                    CategoryID ="f7c75354-48e8-49b5-9ea4-dca7b81888df",
                                },
                new Product()
                                {
                                    ID = Guid.NewGuid().ToString(),
                                    Name =" Cà Phê Sữa Nóng",

                                    ProductType  = ProductType.Drink,
                                    CreateDate =DateTime.Now,
                                    UpdateDate =DateTime.Now,
                                    IsSale  = false,
                                    PriceSale =  78000,
                                    IsAvailable =true,

                                    Formula ="",
                                    Price = 78000,
                                    Description ="",

                                    LinkImage ="/images/ca-phe-sua-nong.webp",
                                    CategoryID ="f7c75354-48e8-49b5-9ea4-dca7b81888df",
                                },
                new Product()
                                {
                                    ID = Guid.NewGuid().ToString(),
                                    Name =" Cold Brew Sữa Tươi",

                                    ProductType  = ProductType.Drink,
                                    CreateDate =DateTime.Now,
                                    UpdateDate =DateTime.Now,
                                    IsSale  = false,
                                    PriceSale =  45000,
                                    IsAvailable =true,

                                    Formula ="",
                                    Price = 45000,
                                    Description ="",

                                    LinkImage ="/images/cold-brew-sua-tuoi.webp",
                                    CategoryID ="0e91fcb8-fa59-4a86-9af9-e608d3175caa ",
                                },
                new Product()
                                {
                                    ID = Guid.NewGuid().ToString(),
                                    Name =" Cold Brew Truyền Thống",

                                    ProductType  = ProductType.Drink,
                                    CreateDate =DateTime.Now,
                                    UpdateDate =DateTime.Now,
                                    IsSale  = false,
                                    PriceSale =  45000,
                                    IsAvailable =true,

                                    Formula ="",
                                    Price = 45000,
                                    Description ="",

                                    LinkImage ="/images/classic-cold-brew.webp",
                                    CategoryID ="0e91fcb8-fa59-4a86-9af9-e608d3175caa ",
                                },
                new Product()
                                {
                                    ID = Guid.NewGuid().ToString(),
                                    Name =" Trà Dưa Đào Sung Túc",

                                    ProductType  = ProductType.Drink,
                                    CreateDate =DateTime.Now,
                                    UpdateDate =DateTime.Now,
                                    IsSale  = false,
                                    PriceSale =  58000,
                                    IsAvailable =true,

                                    Formula ="",
                                    Price = 58000,
                                    Description ="",

                                    LinkImage ="/images/tra-dao-dua-luoi.webp",
                                    CategoryID ="4047a9ea-36a5-4b6b-a293-e3a914281736",
                                },
                new Product()
                                {
                                    ID = Guid.NewGuid().ToString(),
                                    Name =" Trà Sen Nhãn Sum Vầy",

                                    ProductType  = ProductType.Drink,
                                    CreateDate =DateTime.Now,
                                    UpdateDate =DateTime.Now,
                                    IsSale  = false,
                                    PriceSale =  58000,
                                    IsAvailable =true,

                                    Formula ="",
                                    Price = 58000,
                                    Description ="",

                                    LinkImage ="/images/tra-sen-nhan.webp",
                                    CategoryID ="4047a9ea-36a5-4b6b-a293-e3a914281736",
                                },
                new Product()
                                {
                                    ID = Guid.NewGuid().ToString(),
                                    Name =" Trà Long Nhãn Hạt Chia",

                                    ProductType  = ProductType.Drink,
                                    CreateDate =DateTime.Now,
                                    UpdateDate =DateTime.Now,
                                    IsSale  = false,
                                    PriceSale =  45000,
                                    IsAvailable =true,

                                    Formula ="",
                                    Price = 45000,
                                    Description ="",

                                    LinkImage ="/images/tra-nhan-da.webp ",
                                    CategoryID ="4047a9ea-36a5-4b6b-a293-e3a914281736",
                                },
                new Product()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name =" Trà Long Nhãn Hạt Chia Nóng",

                    ProductType  = ProductType.Drink,
                    CreateDate =DateTime.Now,
                    UpdateDate =DateTime.Now,
                    IsSale  = false,
                    PriceSale =  51000,
                    IsAvailable =true,

                    Formula ="",
                    Price = 51000,
                    Description ="",

                    LinkImage ="/images/nhan-hat-chia--nong.webp",
                    CategoryID ="4047a9ea-36a5-4b6b-a293-e3a914281736",
                },
                new Product()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name =" Trà Hạt Sen Đá",

                    ProductType  = ProductType.Drink,
                    CreateDate =DateTime.Now,
                    UpdateDate =DateTime.Now,
                    IsSale  = false,
                    PriceSale =  45000,
                    IsAvailable =true,

                    Formula ="",
                    Price = 45000,
                    Description ="",

                    LinkImage ="/images/tra-sen.webp",
                    CategoryID ="4047a9ea-36a5-4b6b-a293-e3a914281736",
                },
                new Product()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name =" Trà Hạt Sen Nóng",

                    ProductType  = ProductType.Drink,
                    CreateDate =DateTime.Now,
                    UpdateDate =DateTime.Now,
                    IsSale  = false,
                    PriceSale =  51000,
                    IsAvailable =true,

                    Formula ="",
                    Price = 51000,
                    Description ="",

                    LinkImage ="/images/tra-sen-nong.webp",
                    CategoryID ="4047a9ea-36a5-4b6b-a293-e3a914281736",
                },
                new Product()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name =" Trà Đào Cam Sả Đá",

                    ProductType  = ProductType.Drink,
                    CreateDate =DateTime.Now,
                    UpdateDate =DateTime.Now,
                    IsSale  = false,
                    PriceSale =  45000,
                    IsAvailable =true,

                    Formula ="",
                    Price = 45000,
                    Description ="",

                    LinkImage ="/images/ tra-dao-cam-xa.webp",
                    CategoryID ="4047a9ea-36a5-4b6b-a293-e3a914281736",
                },
                new Product()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name =" Trà Đào Cam Sả Nóng",

                    ProductType  = ProductType.Drink,
                    CreateDate =DateTime.Now,
                    UpdateDate =DateTime.Now,
                    IsSale  = false,
                    PriceSale =  51000,
                    IsAvailable =true,

                    Formula ="",
                    Price = 52000,
                    Description ="",

                    LinkImage ="/images/tdcs-nong.webp",
                    CategoryID ="4047a9ea-36a5-4b6b-a293-e3a914281736",
                },
                new Product()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name =" Trà Đào Cam Sả Chai Fresh 500ml",

                    ProductType  = ProductType.Drink,
                    CreateDate =DateTime.Now,
                    UpdateDate =DateTime.Now,
                    IsSale  = false,
                    PriceSale =  107000,
                    IsAvailable =true,

                    Formula ="",
                    Price = 107000,
                    Description ="",

                    LinkImage ="/images/bottle_tradao.webp",
                    CategoryID ="4047a9ea-36a5-4b6b-a293-e3a914281736",
                },
                new Product()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name =" Caramel Macchiato Đá",

                    ProductType  = ProductType.Drink,
                    CreateDate =DateTime.Now,
                    UpdateDate =DateTime.Now,
                    IsSale  = false,
                    PriceSale =  49000,
                    IsAvailable =true,

                    Formula ="",
                    Price = 49000,
                    Description ="",

                    LinkImage ="/images/caramel-macchiato-da.webp",
                    CategoryID ="6a53708b-87bc-4b17-8fea-2a7bb856ba9b",
                },
                new Product()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name =" Hồng Trà Latte Macchiato",

                    ProductType  = ProductType.Drink,
                    CreateDate =DateTime.Now,
                    UpdateDate =DateTime.Now,
                    IsSale  = false,
                    PriceSale =  54000,
                    IsAvailable =true,

                    Formula ="",
                    Price = 54000,
                    Description ="",

                    LinkImage ="/images/hong-tra-latte.webp",
                    CategoryID ="6a53708b-87bc-4b17-8fea-2a7bb856ba9b",
                },
                new Product()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name =" Hồng Trà Sữa Nóng",

                    ProductType  = ProductType.Drink,
                    CreateDate =DateTime.Now,
                    UpdateDate =DateTime.Now,
                    IsSale  = false,
                    PriceSale =  54000,
                    IsAvailable =true,

                    Formula ="",
                    Price = 54000,
                    Description ="",

                    LinkImage ="/images/hong-tra-sua-nong.webp",
                    CategoryID ="6a53708b-87bc-4b17-8fea-2a7bb856ba9b",
                },
                new Product()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name =" Hồng Trà Sữa Trân Châu",

                    ProductType  = ProductType.Drink,
                    CreateDate =DateTime.Now,
                    UpdateDate =DateTime.Now,
                    IsSale  = false,
                    PriceSale =  54000,
                    IsAvailable =true,

                    Formula ="",
                    Price = 54000,
                    Description ="",

                    LinkImage ="/images/hong-tra-sua-tran-chau.webp",
                    CategoryID ="6a53708b-87bc-4b17-8fea-2a7bb856ba9b",
                },
                new Product()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name =" Latte Táo Lê Quế Đá",

                    ProductType  = ProductType.Drink,
                    CreateDate =DateTime.Now,
                    UpdateDate =DateTime.Now,
                    IsSale  = false,
                    PriceSale =  58000,
                    IsAvailable =true,

                    Formula ="",
                    Price = 58000,
                    Description ="",

                    LinkImage ="/images/latte-tao-le-que-da.webp",
                    CategoryID ="6a53708b-87bc-4b17-8fea-2a7bb856ba9b",
                },
                new Product()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name =" Trà Đen Macchiato",

                    ProductType  = ProductType.Drink,
                    CreateDate =DateTime.Now,
                    UpdateDate =DateTime.Now,
                    IsSale  = false,
                    PriceSale =  49000,
                    IsAvailable =true,

                    Formula ="",
                    Price = 49000,
                    Description ="",

                    LinkImage ="/images/tra-den-matchiato.webp",
                    CategoryID ="6a53708b-87bc-4b17-8fea-2a7bb856ba9b",
                },
                new Product()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name =" Trà Sữa Mắc Ca Trân Châu Trắng",

                    ProductType  = ProductType.Drink,
                    CreateDate =DateTime.Now,
                    UpdateDate =DateTime.Now,
                    IsSale  = false,
                    PriceSale =  49000,
                    IsAvailable =true,

                    Formula ="",
                    Price = 49000,
                    Description ="",

                    LinkImage ="/images/tra-sua-mac-ca.webp",
                    CategoryID ="6a53708b-87bc-4b17-8fea-2a7bb856ba9b",
                },
                new Product()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name =" Trà sữa Masala Chai Nóng",

                    ProductType  = ProductType.Drink,
                    CreateDate =DateTime.Now,
                    UpdateDate =DateTime.Now,
                    IsSale  = false,
                    PriceSale =  58000,
                    IsAvailable =true,

                    Formula ="",
                    Price = 58000,
                    Description ="",

                    LinkImage ="/images/tra-sua-masala-chai-nong.webp",
                    CategoryID ="6a53708b-87bc-4b17-8fea-2a7bb856ba9b",
                },
                new Product()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name =" Trà sữa Masala Chai Trân Châu Chai Fresh 500ml",

                    ProductType  = ProductType.Drink,
                    CreateDate =DateTime.Now,
                    UpdateDate =DateTime.Now,
                    IsSale  = false,
                    PriceSale =  107000,
                    IsAvailable =true,

                    Formula ="",
                    Price = 107000,
                    Description ="",

                    LinkImage ="/images/tra-sua-masala-chai-tran-chau-chai-fresh-500ml.webp",
                    CategoryID ="6a53708b-87bc-4b17-8fea-2a7bb856ba9b",
                },
                new Product()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name =" Trà sữa Masala Chai Trân Châu Đá",

                    ProductType  = ProductType.Drink,
                    CreateDate =DateTime.Now,
                    UpdateDate =DateTime.Now,
                    IsSale  = false,
                    PriceSale =  58000,
                    IsAvailable =true,

                    Formula ="",
                    Price = 58000,
                    Description ="",

                    LinkImage ="/images/ masala-chai-tran-chau-lanh.webp",
                    CategoryID ="6a53708b-87bc-4b17-8fea-2a7bb856ba9b",
                },
                new Product()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name =" Trà Sữa Oolong Nướng Nóng",

                    ProductType  = ProductType.Drink,
                    CreateDate =DateTime.Now,
                    UpdateDate =DateTime.Now,
                    IsSale  = false,
                    PriceSale =  49000,
                    IsAvailable =true,

                    Formula ="",
                    Price = 49000,
                    Description ="",

                    LinkImage ="/images/oolong-nuong-nong.webp",
                    CategoryID ="6a53708b-87bc-4b17-8fea-2a7bb856ba9b",
                },
                new Product()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name = " Trà Sữa Oolong Nướng Trân Châu",

                                    ProductType = ProductType.Drink,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    IsSale = false,
                    PriceSale = 54000,
                    IsAvailable = true,

                    Formula = "",
                    Price = 54000,
                    Description = "",

                    LinkImage = "/images/olong-nuong-tran-chau.webp",
                    CategoryID = "6a53708b-87bc-4b17-8fea-2a7bb856ba9b",
                },
                new Product()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name = " Trà sữa Oolong Nướng Trân Châu Chai 500ml",

                                    ProductType = ProductType.Drink,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    IsSale = false,
                    PriceSale = 97000,
                    IsAvailable = true,

                    Formula = "",
                    Price = 97000,
                    Description = "",

                    LinkImage = "/images/bottle_oolong.webp",
                    CategoryID = "6a53708b-87bc-4b17-8fea-2a7bb856ba9b",
                },
                new Product()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name = " Cà Phê Đá Xay",

                                    ProductType = ProductType.Drink,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    IsSale = false,
                    PriceSale = 58000,
                    IsAvailable = true,

                    Formula = "",
                    Price = 58000,
                    Description = "",

                    LinkImage = "/images/ cf-da-xay.webp",
                    CategoryID = "5922b0dc-51ef-48de-9d9f-48bfff9c2552",
                },
                new Product()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name = " Chanh Sả Đá Xay",

                                    ProductType = ProductType.Drink,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    IsSale = false,
                    PriceSale = 49000,
                    IsAvailable = true,

                    Formula = "",
                    Price = 49000,
                    Description = "",

                    LinkImage = "/images/chanh-sa-da-xay.webp",
                    CategoryID = "5922b0dc-51ef-48de-9d9f-48bfff9c2552",
                },
                new Product()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name = " Chocolate Đá Xay",

                                    ProductType = ProductType.Drink,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    IsSale = false,
                    PriceSale = 58000,
                    IsAvailable = true,

                    Formula = "",
                    Price = 58000,
                    Description = "",

                    LinkImage = "/images/chocolate-ice-blended.webp",
                    CategoryID = "5922b0dc-51ef-48de-9d9f-48bfff9c2552",
                },
                new Product()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name = " Cookie Đá Xay",

                                    ProductType = ProductType.Drink,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    IsSale = false,
                    PriceSale = 58000,
                    IsAvailable = true,

                    Formula = "",
                    Price = 58000,
                    Description = "",

                    LinkImage = "/images/cookies_da_xay.webp",
                    CategoryID = "5922b0dc-51ef-48de-9d9f-48bfff9c2552",
                },
                new Product()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name = " Đào Việt Quất Đá Xay",

                                    ProductType = ProductType.Drink,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    IsSale = false,
                    PriceSale = 58000,
                    IsAvailable = true,

                    Formula = "",
                    Price = 58000,
                    Description = "",

                    LinkImage = "/images/daovietquat.webp",
                    CategoryID = "5922b0dc-51ef-48de-9d9f-48bfff9c2552",
                },
                new Product()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name = " Matcha Đá Xay",

                                    ProductType = ProductType.Drink,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    IsSale = false,
                    PriceSale = 58000,
                    IsAvailable = true,

                    Formula = "",
                    Price = 58000,
                    Description = "",

                    LinkImage = "/images/matchadaxa.webp",
                    CategoryID = "5922b0dc-51ef-48de-9d9f-48bfff9c2552",
                },
                new Product()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name = " Sinh Tố Việt Quất",

                                    ProductType = ProductType.Drink,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    IsSale = false,
                    PriceSale = 58000,
                    IsAvailable = true,

                    Formula = "",
                    Price = 58000,
                    Description = "",

                    LinkImage = "/images/sinh-to-viet-quat.webp",
                    CategoryID = "5922b0dc-51ef-48de-9d9f-48bfff9c2552",
                },
                new Product()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name = " Chocolate Đá",

                                    ProductType = ProductType.Drink,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    IsSale = false,
                    PriceSale = 58000,
                    IsAvailable = true,

                    Formula = "",
                    Price = 58000,
                    Description = "",

                    LinkImage = "/images/chocolate-da.webp",
                    CategoryID = "134aac36-958e-4a30-9887-85996c2a9771",
                },
                new Product()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name = " Chocolate Đá Xay",

                                    ProductType = ProductType.Drink,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    IsSale = false,
                    PriceSale = 58000,
                    IsAvailable = true,

                    Formula = "",
                    Price = 58000,
                    Description = "",

                    LinkImage = "/images/chocolate-ice-blended.webp",
                    CategoryID = "134aac36-958e-4a30-9887-85996c2a9771",
                },
                new Product()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name = " Chocolate Nóng",

                                    ProductType = ProductType.Drink,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    IsSale = false,
                    PriceSale = 58000,
                    IsAvailable = true,

                    Formula = "",
                    Price = 58000,
                    Description = "",

                    LinkImage = "/images/chocolatenong.webp",
                    CategoryID = "134aac36-958e-4a30-9887-85996c2a9771",
                },
                new Product()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name = " Matcha Latte Đá",

                                    ProductType = ProductType.Drink,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    IsSale = false,
                    PriceSale = 58000,
                    IsAvailable = true,

                    Formula = "",
                    Price = 58000,
                    Description = "",

                    LinkImage = "/images/matcha-latte-da.webp",
                    CategoryID = "134aac36-958e-4a30-9887-85996c2a9771",
                },
                new Product()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name = " Matcha Latte Nóng",

                                    ProductType = ProductType.Drink,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    IsSale = false,
                    PriceSale = 58000,
                    IsAvailable = true,

                    Formula = "",
                    Price = 58000,
                    Description = "",

                    LinkImage = "/images/matcha-latte.webp",
                    CategoryID = "134aac36-958e-4a30-9887-85996c2a9771",
                },
                new Product()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name = " Bánh Mì Que Pate",

                                    ProductType = ProductType.Drink,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    IsSale = false,
                    PriceSale = 12000,
                    IsAvailable = true,

                    Formula = "",
                    Price = 12000,
                    Description = "",

                    LinkImage = "/images/banhmique.webp",
                    CategoryID = "44e2f99f-cd95-4b89-a3d3-553cf86ff173",
                },
                new Product()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name = " Bánh Mì Que Pate Cay",

                                    ProductType = ProductType.Drink,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    IsSale = false,
                    PriceSale = 12000,
                    IsAvailable = true,

                    Formula = "",
                    Price = 12000,
                    Description = "",

                    LinkImage = "/images/banhmiquecay.webp",
                    CategoryID = "44e2f99f-cd95-4b89-a3d3-553cf86ff173",
                },
                new Product()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name = " Bánh Mì Thịt Nguội",

                                    ProductType = ProductType.Drink,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    IsSale = false,
                    PriceSale = 29000,
                    IsAvailable = true,

                    Formula = "",
                    Price = 29000,
                    Description = "",

                    LinkImage = "/images/banh-mi-vn-thit-nguoi.webp",
                    CategoryID = "44e2f99f-cd95-4b89-a3d3-553cf86ff173",
                },
                new Product()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name = " Chà Bông Phô Mai",

                                    ProductType = ProductType.Drink,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    IsSale = false,
                    PriceSale = 32000,
                    IsAvailable = true,

                    Formula = "",
                    Price = 32000,
                    Description = "",

                    LinkImage = "/images/cha-bong-pho-mai.webp",
                    CategoryID = "44e2f99f-cd95-4b89-a3d3-553cf86ff173",
                },
                new Product()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name = " Croissant Trứng Muối",

                                    ProductType = ProductType.Drink,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    IsSale = false,
                    PriceSale = 35000,
                    IsAvailable = true,

                    Formula = "",
                    Price = 35000,
                    Description = "",

                    LinkImage = "/images/croissant-trung-muoi.webp",
                    CategoryID = "44e2f99f-cd95-4b89-a3d3-553cf86ff173",
                },
                new Product()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name = "Mochi Kem Chocolate",

                                    ProductType = ProductType.Drink,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    IsSale = false,
                    PriceSale = 19000,
                    IsAvailable = true,

                    Formula = "",
                    Price = 19000,
                    Description = "",

                    LinkImage = "/images/mochi-kem-chocolate.webp",
                    CategoryID = "a93ba116-4a2c-4d0a-aa79-a7b37cd48cb8",
                },
                new Product()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name = "Mochi Kem Dừa Dứa",

                                    ProductType = ProductType.Drink,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    IsSale = false,
                    PriceSale = 19000,
                    IsAvailable = true,

                    Formula = "",
                    Price = 19000,
                    Description = "",

                    LinkImage = "/images/mochi-kem-dua-dua.webp",
                    CategoryID = "a93ba116-4a2c-4d0a-aa79-a7b37cd48cb8",
                },
                new Product()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name = "Mochi Kem Matcha",

                                    ProductType = ProductType.Drink,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    IsSale = false,
                    PriceSale = 19000,
                    IsAvailable = true,

                    Formula = "",
                    Price = 19000,
                    Description = "",

                    LinkImage = "/images/mochi-kem-matcha.webp",
                    CategoryID = "a93ba116-4a2c-4d0a-aa79-a7b37cd48cb8",
                },
                new Product()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name = "Mochi Kem Việt Quất",

                                    ProductType = ProductType.Drink,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    IsSale = false,
                    PriceSale = 19000,
                    IsAvailable = true,

                    Formula = "",
                    Price = 19000,
                    Description = "",

                    LinkImage = "/images/mochi-kem-viet-quat.webp",
                    CategoryID = "a93ba116-4a2c-4d0a-aa79-a7b37cd48cb8",
                },
                new Product()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name = "Mochi Kem Xoài",

                                    ProductType = ProductType.Drink,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    IsSale = false,
                    PriceSale = 19000,
                    IsAvailable = true,

                    Formula = "",
                    Price = 19000,
                    Description = "",

                    LinkImage = "/images/mochi-kem-xoai.webp",
                    CategoryID = "a93ba116-4a2c-4d0a-aa79-a7b37cd48cb8",
                },
                new Product()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name = "Mochi Kem Phúc Bồn Tử",

                                    ProductType = ProductType.Drink,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    IsSale = false,
                    PriceSale = 19000,
                    IsAvailable = true,

                    Formula = "",
                    Price = 19000,
                    Description = "",

                    LinkImage = "/images/mochi-kem-phuc-bon-tu.webp",
                    CategoryID = "a93ba116-4a2c-4d0a-aa79-a7b37cd48cb8",
                },
                new Product()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name = "Mousse Gấu Chocolate",

                                    ProductType = ProductType.Drink,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    IsSale = false,
                    PriceSale = 39000,
                    IsAvailable = true,

                    Formula = "",
                    Price = 39000,
                    Description = "",

                    LinkImage = "/images/mousse-gau-chocolate.webp",
                    CategoryID = "a93ba116-4a2c-4d0a-aa79-a7b37cd48cb8",
                },
                new Product()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name = "Mousse Passion Cheese",

                                    ProductType = ProductType.Drink,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    IsSale = false,
                    PriceSale = 29000,
                    IsAvailable = true,

                    Formula = "",
                    Price = 29000,
                    Description = "",

                    LinkImage = "/images/chanh-day.webp",
                    CategoryID = "a93ba116-4a2c-4d0a-aa79-a7b37cd48cb8",
                },
                new Product()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name = "Mousse Red Velvet",

                                    ProductType = ProductType.Drink,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    IsSale = false,
                    PriceSale = 29000,
                    IsAvailable = true,

                    Formula = "",
                    Price = 29000,
                    Description = "",

                    LinkImage = "/images/mousse-red-velvet.webp",
                    CategoryID = "a93ba116-4a2c-4d0a-aa79-a7b37cd48cb8",
                },
                new Product()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name = "Mousse Tiramisu",

                                    ProductType = ProductType.Drink,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    IsSale = false,
                    PriceSale = 32000,
                    IsAvailable = true,

                    Formula = "",
                    Price = 32000,
                    Description = "",

                    LinkImage = "/images/ mousse-tiramisu.webp",
                    CategoryID = "a93ba116-4a2c-4d0a-aa79-a7b37cd48cb8",
                },
                new Product()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name = "Mít Sấy",

                                    ProductType = ProductType.Drink,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    IsSale = false,
                    PriceSale = 20000,
                    IsAvailable = true,

                    Formula = "",
                    Price = 20000,
                    Description = "",

                    LinkImage = "/images/mit-say.webp",
                    CategoryID = "a93ba116-4a2c-4d0a-aa79-a7b37cd48cb8",
                },
                new Product()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name = "Cà Phê Rang Xay Original 1 Túi 1KG",

                                    ProductType = ProductType.Drink,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    IsSale = false,
                    PriceSale = 230000,
                    IsAvailable = true,

                    Formula = "",
                    Price = 230000,
                    Description = "",

                    LinkImage = "/images/ori-1-1kg.webp",
                    CategoryID = "2d79d339-660c-468b-9d40-ef62cc9a5baa",
                },
                new Product()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name = "Cà Phê Rang Xay Original 1 250gr",

                                    ProductType = ProductType.Drink,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    IsSale = false,
                    PriceSale = 49000,
                    IsAvailable = true,

                    Formula = "",
                    Price = 49000,
                    Description = "",

                    LinkImage = "/images/ca-phe-rang-xay-original.webp",
                    CategoryID = "2d79d339-660c-468b-9d40-ef62cc9a5baa",
                },
                new Product()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name = "Cà Phê Hòa Tan Đậm Vị Việt Túi 40x16G",

                                    ProductType = ProductType.Drink,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    IsSale = false,
                    PriceSale = 79000,
                    IsAvailable = true,

                    Formula = "",
                    Price = 79000,
                    Description = "",

                    LinkImage = "/images/ca-phe-dam-vi-viet_tui_new.webp",
                    CategoryID = "2d79d339-660c-468b-9d40-ef62cc9a5baa",
                },
                new Product()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name = "Cà Phê Sữa Đá Hòa Tan Hộp 10 gói ",

                                    ProductType = ProductType.Drink,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    IsSale = false,
                    PriceSale = 39000,
                    IsAvailable = true,

                    Formula = "",
                    Price = 39000,
                    Description = "",

                    LinkImage = "/images/cpsd-3in1.webp",
                    CategoryID = "2d79d339-660c-468b-9d40-ef62cc9a5baa",
                },
                new Product()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name = "Cà Phê Sữa Đá Hòa Tan Đậm Vị Hộp 18 gói x 16gr",

                                    ProductType = ProductType.Drink,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    IsSale = false,
                    PriceSale = 39000,
                    IsAvailable = true,

                    Formula = "",
                    Price = 39000,
                    Description = "",

                    LinkImage = "/images/ca-phe-sua-da-hoa-tan-dam-vi.webp",
                    CategoryID = "2d79d339-660c-468b-9d40-ef62cc9a5baa",
                },
                new Product()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name = "Cà Phê Sữa Đá Hòa Tan Túi 25 x 22gr",

                                    ProductType = ProductType.Drink,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    IsSale = false,
                    PriceSale = 79000,
                    IsAvailable = true,

                    Formula = "",
                    Price = 79000,
                    Description = "",

                    LinkImage = "/images/ca-phe-sua-da-hoa-tan-tui.webp",
                    CategoryID = "2d79d339-660c-468b-9d40-ef62cc9a5baa",
                },
                new Product()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name = "Cà Phê Rich Finish Gu Đậm Tinh Tế 350gr",

                                    ProductType = ProductType.Drink,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    IsSale = false,
                    PriceSale = 49000,
                    IsAvailable = true,

                    Formula = "",
                    Price = 49000,
                    Description = "",

                    LinkImage = "/images/rich-finish-nopromo.webp",
                    CategoryID = "2d79d339-660c-468b-9d40-ef62cc9a5baa",
                },
                new Product()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name = "Cà Phê Peak Flavor Hương Thơm Đỉnh Cao 350gr",

                                    ProductType = ProductType.Drink,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    IsSale = false,
                    PriceSale = 49000,
                    IsAvailable = true,

                    Formula = "",
                    Price = 49000,
                    Description = "",

                    LinkImage = "/images/peak-plavor-nopromo.webp",
                    CategoryID = "2d79d339-660c-468b-9d40-ef62cc9a5baa",
                },
                new Product()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name = "Cà Phê Arabica",

                                    ProductType = ProductType.Drink,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    IsSale = false,
                    PriceSale = 98000,
                    IsAvailable = true,

                    Formula = "",
                    Price = 98000,
                    Description = "",

                    LinkImage = "/images/arabica.webp",
                    CategoryID = "2d79d339-660c-468b-9d40-ef62cc9a5baa",
                },
                new Product()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name = "Cà phê sữa đá pack 6 lon",

                                    ProductType = ProductType.Drink,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    IsSale = false,
                    PriceSale = 69000,
                    IsAvailable = true,

                    Formula = "",
                    Price = 69000,
                    Description = "",

                    LinkImage = "/images/p6-lon-3in1.webp",
                    CategoryID = "2d79d339-660c-468b-9d40-ef62cc9a5baa",
                },
                new Product()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name = "Thùng 24 Lon Cà Phê Sữa Đá",

                                    ProductType = ProductType.Drink,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    IsSale = false,
                    PriceSale = 269000,
                    IsAvailable = true,

                    Formula = "",
                    Price = 269000,
                    Description = "",

                    LinkImage = "/images/24-lon-cpsd.webp",
                    CategoryID = "2d79d339-660c-468b-9d40-ef62cc9a5baa",
                },
                new Product()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name = "Combo 3 Hộp Cà Phê Sữa Đá Hòa Tan Đậm Vị Hộp 18 gói x 16gr",

                                    ProductType = ProductType.Drink,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    IsSale = false,
                    PriceSale = 109000,
                    IsAvailable = true,

                    Formula = "",
                    Price = 109000,
                    Description = "",

                    LinkImage = "/images/combo-3-hop-ca-phe-sua-da-hoa-tan-dam.webp",
                    CategoryID = "2d79d339-660c-468b-9d40-ef62cc9a5baa",
                },
                new Product()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name = "Combo 3 Hộp Cà Phê Sữa Đá Hòa Tan",

                                    ProductType = ProductType.Drink,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    IsSale = false,
                    PriceSale = 109000,
                    IsAvailable = true,

                    Formula = "",
                    Price = 109000,
                    Description = "",

                    LinkImage = "/images/combo-3cfsd-nopromo.webp",
                    CategoryID = "2d79d339-660c-468b-9d40-ef62cc9a5baa",
                },
                new Product()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name = "Combo 2 Café Rang Xay Original 1 250gr",

                                    ProductType = ProductType.Drink,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    IsSale = false,
                    PriceSale = 109000,
                    IsAvailable = true,

                    Formula = "",
                    Price = 109000,
                    Description = "",

                    LinkImage = "/images/2combo2-cforiginal.webp",
                    CategoryID = "2d79d339-660c-468b-9d40-ef62cc9a5baa",
                },
                new Product()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name = "Giftset Trà Tearoma",

                                    ProductType = ProductType.Drink,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    IsSale = false,
                    PriceSale = 169000,
                    IsAvailable = true,

                    Formula = "",
                    Price = 169000,
                    Description = "",

                    LinkImage = "/images/giftset-tra-tearoma.webp",
                    CategoryID = "ea108c9e-ae7a-4e0f-a1d0-f7d14c079cff",
                },
                new Product()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name = "Combo 3 hộp trà Lài túi lọc Tearoma",

                                    ProductType = ProductType.Drink,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    IsSale = false,
                    PriceSale = 69000,
                    IsAvailable = true,

                    Formula = "",
                    Price = 69000,
                    Description = "",

                    LinkImage = "/images/combo-3-hop-tra-lai-tui-loc-tearoma.webp",
                    CategoryID = "ea108c9e-ae7a-4e0f-a1d0-f7d14c079cff",
                },
                new Product()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name = "Combo 3 hộp trà Oolong túi lọc Tearoma",

                                    ProductType = ProductType.Drink,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    IsSale = false,
                    PriceSale = 69000,
                    IsAvailable = true,

                    Formula = "",
                    Price = 69000,
                    Description = "",

                    LinkImage = "/images/combo-3-hop-tra-oolong-tui-loc-tearoma1.webp",
                    CategoryID = "ea108c9e-ae7a-4e0f-a1d0-f7d14c079cff",
                },
                new Product()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name = "Combo 3 hộp trà Đào túi lọc Tearoma",

                                    ProductType = ProductType.Drink,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    IsSale = false,
                    PriceSale = 69000,
                    IsAvailable = true,

                    Formula = "",
                    Price = 69000,
                    Description = "",

                    LinkImage = "/images/combo-3-hop-tra-dao-tui-loc-tearoma.webp",
                    CategoryID = "ea108c9e-ae7a-4e0f-a1d0-f7d14c079cff",
                },
                new Product()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name = "Combo 3 hộp trà Sen túi lọc Tearoma",

                                    ProductType = ProductType.Drink,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    IsSale = false,
                    PriceSale = 69000,
                    IsAvailable = true,

                    Formula = "",
                    Price = 69000,
                    Description = "",

                    LinkImage = "/images/combo-3-hop-tra-sen-tui-loc-tearoma1.webp",
                    CategoryID = "ea108c9e-ae7a-4e0f-a1d0-f7d14c079cff",
                },
                new Product()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name = "Trà Đào Túi Lọc Tearoma 20 x 2gr",

                                    ProductType = ProductType.Drink,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    IsSale = false,
                    PriceSale = 28000,
                    IsAvailable = true,

                    Formula = "",
                    Price = 28000,
                    Description = "",

                    LinkImage = "/images/tra-dao-tui-loc-tearoma.webp",
                    CategoryID = "ea108c9e-ae7a-4e0f-a1d0-f7d14c079cff",
                },
                new Product()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name = "Trà Lài Túi Lọc Tearoma 20 x 2gr",

                                    ProductType = ProductType.Drink,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    IsSale = false,
                    PriceSale = 28000,
                    IsAvailable = true,

                    Formula = "",
                    Price = 28000,
                    Description = "",

                    LinkImage = "/images/tra-lai-tui-loc-tearoma.webp",
                    CategoryID = "ea108c9e-ae7a-4e0f-a1d0-f7d14c079cff",
                },
                new Product()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name = "Trà Olong Túi Lọc Tearoma 20 x 2gr",

                                    ProductType = ProductType.Drink,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    IsSale = false,
                    PriceSale = 28000,
                    IsAvailable = true,

                    Formula = "",
                    Price = 28000,
                    Description = "",

                    LinkImage = "/images/tra-oolong-tui-loc-tearoma.webp",
                    CategoryID = "ea108c9e-ae7a-4e0f-a1d0-f7d14c079cff",
                },
                new Product()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name = "Trà Sen Túi Lọc Tearoma 20 x 2gr",

                                    ProductType = ProductType.Drink,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    IsSale = false,
                    PriceSale = 28000,
                    IsAvailable = true,

                    Formula = "",
                    Price = 28000,
                    Description = "",

                    LinkImage = "/images/tra-sen-tui-loc-tearoma.webp",
                    CategoryID = "ea108c9e-ae7a-4e0f-a1d0-f7d14c079cff",
                },
                new Product()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name = "Trà Xanh Lá Tearoma 100gr",

                                    ProductType = ProductType.Drink,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    IsSale = false,
                    PriceSale = 74000,
                    IsAvailable = true,

                    Formula = "",
                    Price = 74000,
                    Description = "",

                    LinkImage = "/images/tra-xanh-la-tearoma.webp",
                    CategoryID = "ea108c9e-ae7a-4e0f-a1d0-f7d14c079cff",
                },
                new Product()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name = "Trà Sen Lá Tearoma 100gr",

                                    ProductType = ProductType.Drink,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    IsSale = false,
                    PriceSale = 74000,
                    IsAvailable = true,

                    Formula = "",
                    Price = 74000,
                    Description = "",

                    LinkImage = "/images/tra-sen-la-tearoma.webp",
                    CategoryID = "ea108c9e-ae7a-4e0f-a1d0-f7d14c079cff",
                },
                new Product()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name = "Trà Oolong Lá Tearoma 100gr",

                                    ProductType = ProductType.Drink,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    IsSale = false,
                    PriceSale = 74000,
                    IsAvailable = true,

                    Formula = "",
                    Price = 74000,
                    Description = "",

                    LinkImage = "/images/tra-oolong-la-tearoma.webp",
                    CategoryID = "ea108c9e-ae7a-4e0f-a1d0-f7d14c079cff",
                },
                new Product()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name = "Trà Lài Lá Tearoma 100gr",

                                    ProductType = ProductType.Drink,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    IsSale = false,
                    PriceSale = 74000,
                    IsAvailable = true,

                    Formula = "",
                    Price = 74000,
                    Description = "",

                    LinkImage = "/images/tra-lai-la-tearoma.webp",
                    CategoryID = "ea108c9e-ae7a-4e0f-a1d0-f7d14c079cff",
                },
                new Product()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name = "Bình Giữ Nhiệt Inox Trắng Đen 500ML",

                                    ProductType = ProductType.Drink,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    IsSale = false,
                    PriceSale = 250000,
                    IsAvailable = true,

                    Formula = "",
                    Price = 250000,
                    Description = "",

                    LinkImage = "/images/binh-giu-nhiet-inox-trang-den.webp",
                    CategoryID = "2e008313-4f56-40c0-89a9-01abe3d5e22f",
                },
                new Product()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name = "Bình Giữ Nhiệt Inox Xám Cam 473ML",

                                    ProductType = ProductType.Drink,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    IsSale = false,
                    PriceSale = 250000,
                    IsAvailable = true,

                    Formula = "",
                    Price = 250000,
                    Description = "",

                    LinkImage = "/images/bình giữ nhiệt inox xám cam 473ml.webp",
                    CategoryID = "2e008313-4f56-40c0-89a9-01abe3d5e22f",
                },
                new Product()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name = "Bộ Ống Hút Inox",

                                    ProductType = ProductType.Drink,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    IsSale = false,
                    PriceSale = 79000,
                    IsAvailable = true,

                    Formula = "",
                    Price = 79000,
                    Description = "",

                    LinkImage = "/images/bộ ống hút inox.webp",
                    CategoryID = "2e008313-4f56-40c0-89a9-01abe3d5e22f",
                },
                new Product()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name = "Cốc Sứ The Coffee House Gợn Sóng",

                                    ProductType = ProductType.Drink,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    IsSale = false,
                    PriceSale = 100000,
                    IsAvailable = true,

                    Formula = "",
                    Price = 100000,
                    Description = "",

                    LinkImage = "/images/cốc sứ the coffee house gợn sóng.webp",
                    CategoryID = "2e008313-4f56-40c0-89a9-01abe3d5e22f",
                },
                new Product()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name = "Cốc Sứ The Coffee House Sọc Ngang",

                                    ProductType = ProductType.Drink,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    IsSale = false,
                    PriceSale = 100000,
                    IsAvailable = true,

                    Formula = "",
                    Price = 100000,
                    Description = "",

                    LinkImage = "/images/cốc sứ the coffee house sọc ngang.webp",
                    CategoryID = "2e008313-4f56-40c0-89a9-01abe3d5e22f",
                },
                new Product()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name = "Túi Canvan Đà Nẵng",

                                    ProductType = ProductType.Drink,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    IsSale = false,
                    PriceSale = 79000,
                    IsAvailable = true,

                    Formula = "",
                    Price = 79000,
                    Description = "",

                    LinkImage = "/images/túi canvans đà nãng.webp",
                    CategoryID = "2e008313-4f56-40c0-89a9-01abe3d5e22f",
                },
                new Product()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name = "Túi Canvan Hà Nội",

                                    ProductType = ProductType.Drink,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    IsSale = false,
                    PriceSale = 79000,
                    IsAvailable = true,

                    Formula = "",
                    Price = 79000,
                    Description = "",

                    LinkImage = "/images/túi canvans hà nội.webp",
                    CategoryID = "2e008313-4f56-40c0-89a9-01abe3d5e22f",
                },
                new Product()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name = "Túi Canvan Trăng Nhà Sung Túc",

                                    ProductType = ProductType.Drink,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    IsSale = false,
                    PriceSale = 79000,
                    IsAvailable = true,

                    Formula = "",
                    Price = 79000,
                    Description = "",

                    LinkImage = "/images/túi canvans trăng nhà sung túc.webp",
                    CategoryID = "2e008313-4f56-40c0-89a9-01abe3d5e22f",
                }};

            foreach (var item in products)
            {
                context.Products.Add(item);
            }
            context.SaveChanges();
            var branches = new Branch[]
                {
                new Branch() {
                    ID= Guid.NewGuid().ToString(),
                    Name  = "HP Đỗ Nhuận",
                    City  ="Hải phòng",
                    Email  = "thecoffeehouse@coffee.com",
                    District  ="Ngô Quyền" ,
                    Adderss  = "Thửa đất số 172A, 3 Đỗ Nhuận, Đằng Giang, Ngô Quyền, Hải Phòng",
                    CreateDate  =DateTime.Now ,
                    UpdateDate  = DateTime.Now,
                },
                new Branch() {
                    ID= Guid.NewGuid().ToString(),
                    Name  = "HP Lạch Tray",
                    City  ="Hải phòng",
                    Email  = "thecoffeehouse@coffee.com",
                    District  ="Ngô Quyền" ,
                    Adderss  = "382-384 Lạch Tray, Q. Ngô Quyền, Hải Phòng",
                    CreateDate  =DateTime.Now ,
                    UpdateDate  = DateTime.Now,
                },
                new Branch() {
                    ID= Guid.NewGuid().ToString(),
                    Name  = "HP Cát Bi Plaza",
                    City  ="Hải phòng",
                    Email  = "thecoffeehouse@coffee.com",
                    District  ="Ngô Quyền" ,
                    Adderss  = "01 Lê Hồng Phong, Q. Ngô Quyền, Hải Phòng",
                    CreateDate  =DateTime.Now ,
                    UpdateDate  = DateTime.Now,
                },
                new Branch() {
                    ID= Guid.NewGuid().ToString(),
                    Name  = "HP Aeon Mall Lê Chân",
                    City  ="Hải phòng",
                    Email  = "thecoffeehouse@coffee.com",
                    District  ="Ngô Quyền" ,
                    Adderss  = "Tầng 01 Aeon Mall, Q.Lê Chân, Hải Phòng",
                    CreateDate  =DateTime.Now ,
                    UpdateDate  = DateTime.Now,
                },
                new Branch() {
                    ID= Guid.NewGuid().ToString(),
                    Name  = "HP Cầu Đất 2",
                    City  ="Hải phòng",
                    Email  = "thecoffeehouse@coffee.com",
                    District  ="Ngô Quyền" ,
                    Adderss  = "2 - 4 Cầu Đất, Q. Ngô Quyền, Hải Phòng",
                    CreateDate  =DateTime.Now ,
                    UpdateDate  = DateTime.Now,
                },
                new Branch() {
                    ID= Guid.NewGuid().ToString(),
                    Name  = "HP Trần Quang Khải",
                    City  ="Hải phòng",
                    Email  = "thecoffeehouse@coffee.com",
                    District  ="Q. Hồng Bàng" ,
                    Adderss  = "17 Trần Quang Khải, Q. Hồng Bàng, Hải Phòng",
                    CreateDate  =DateTime.Now ,
                    UpdateDate  = DateTime.Now,
                },
                new Branch() {
                    ID= Guid.NewGuid().ToString(),
                    Name  = "HP Trần Phú",
                    City  ="Hải phòng",
                    Email  = "thecoffeehouse@coffee.com",
                    District  ="Ngô Quyền" ,
                    Adderss  = "15 Trần Phú, Lương Khánh Thiện, Ngô Quyền, Hải Phòng",
                    CreateDate  =DateTime.Now ,
                    UpdateDate  = DateTime.Now,
                },
                new Branch() {
                    ID= Guid.NewGuid().ToString(),
                    Name  = "HP Điện Biên Phủ",
                    City  ="Hải phòng",
                    Email  = "thecoffeehouse@coffee.com",
                    District  ="Q. Hồng Bàng" ,
                    Adderss  = "86 Điện Biên Phủ, Hồng Bàng, Hải Phòng, Việt Nam",
                    CreateDate  =DateTime.Now ,
                    UpdateDate  = DateTime.Now,
                },
                };
            foreach (var item in branches)
            {
                context.Branches.Add(item);
            }
            context.SaveChanges();

            var sizes = new Size[]
            {
                new Size()
                {
                    ID= Guid.NewGuid().ToString(),
                    Name="S",
                    SubPrice = 0,
                },
                new Size()
                {
                    ID= Guid.NewGuid().ToString(),
                    Name="M",
                    SubPrice = 6000,
                },
                new Size()
                {
                    ID= Guid.NewGuid().ToString(),
                    Name="S",
                    SubPrice = 10000,
                },
            };
            foreach (var item in sizes)
            {
                context.Sizes.Add(item);
            }
            context.SaveChanges();

            var toppings = new Topping[]
            {
                new Topping()
                {
                    ID= Guid.NewGuid().ToString(),
                    Name="Sen ngâm",
                    SubPrice = 10000,
                },
                new Topping()
                {
                    ID= Guid.NewGuid().ToString(),
                    Name="Nhãn ngâm",
                    SubPrice = 10000,
                },
                new Topping()
                {
                    ID= Guid.NewGuid().ToString(),
                    Name="Trân châu trắng",
                    SubPrice = 10000,
                },
                new Topping()
                {
                    ID= Guid.NewGuid().ToString(),
                    Name="Trân châu trắng",
                    SubPrice = 10000,
                },
                new Topping()
                {
                    ID= Guid.NewGuid().ToString(),
                    Name="Trân châu đen",
                    SubPrice = 10000,
                },
                new Topping()
                {
                    ID= Guid.NewGuid().ToString(),
                    Name="Extra foam",
                    SubPrice = 10000,
                },
                new Topping()
                {
                    ID= Guid.NewGuid().ToString(),
                    Name="Espresso",
                    SubPrice = 10000,
                },
                new Topping()
                {
                    ID= Guid.NewGuid().ToString(),
                    Name="Sauce Caramel ",
                    SubPrice = 10000,
                },
            };
            foreach (var item in toppings)
            {
                context.Toppings.Add(item);
            }
            context.SaveChanges();

            var memberTypes = new MemberType[]
            {
                new MemberType()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name = "Thành viên mới",
                    MinPoint = 0,
                    MaxPoint = 99,
                    ConversationMoney= 100000,
                    ConversationPoint = 6,
                    ConversionForm = "",

                    Description = "Mặc định",
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                },
                new MemberType()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name = "Thành viên Đồng",
                    MinPoint = 100,
                    MaxPoint = 199,
                    ConversationMoney= 100000,
                    ConversationPoint = 6,
                    ConversionForm = "",

                    Description = "Mặc định",
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                },
                new MemberType()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name = "Thành viên Bạc",
                    MinPoint = 199,
                    MaxPoint = 499,
                    ConversationMoney= 100000,
                    ConversationPoint = 6,
                    ConversionForm = "",

                    Description = "Mặc định",
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                },
                new MemberType()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name = "Thành viên vàng",
                    MinPoint = 500,
                    MaxPoint = 2999,
                    ConversationMoney= 100000,
                    ConversationPoint = 6,
                    ConversionForm = "",

                    Description = "Mặc định",
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                },
                new MemberType()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name = "Thành viên kim cương",
                    MinPoint = 3000,
                    MaxPoint = 9999,
                    ConversationMoney= 100000,
                    ConversationPoint = 9,
                    ConversionForm = "",

                    Description = "Mặc định",
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                },

            };
            foreach (var item in memberTypes)
            {
                context.MemberTypes.Add(item);
            }
            context.SaveChanges();

            var materialTypes = new MaterialType[]
            {
                new MaterialType()
                {
                    ID= Guid.NewGuid().ToString(),
                    Name = "Đồ khô",
                    Description = "Mặc định",
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                },
                new MaterialType()
                {
                    ID= Guid.NewGuid().ToString(),
                    Name = "Đồ tươi",
                    Description = "Mặc định",
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                },
            };
            foreach (var item in materialTypes)
            {
                context.MaterialTypes.Add(item);
            }
            context.SaveChanges();

            var materials = new Material[]
            {
                new Material()
                {
                    ID= Guid.NewGuid().ToString(),
                    Description = "Mặc định",
                    CreateDate = DateTime.Now,
                    Name ="Cốc thuỷ tinh",
                    Quantity =9999,
                    UpdateDate = DateTime.Now,
                    Supplier = "Kho 1",

                    MaterialTypeID = materialTypes[0].ID,
                },
                new Material()
                {
                    ID= Guid.NewGuid().ToString(),
                    Description = "Mặc định",
                    CreateDate = DateTime.Now,
                    Name ="Thìa nhựa",
                    Quantity =9999,
                    UpdateDate = DateTime.Now,
                    Supplier = "Kho 1",

                    MaterialTypeID = materialTypes[0].ID,
                },
                new Material()
                {
                    ID= Guid.NewGuid().ToString(),
                    Description = "Mặc định",
                    CreateDate = DateTime.Now,
                    Name ="Ống hút",
                    Quantity =9999,
                    UpdateDate = DateTime.Now,
                    Supplier = "Kho 1",

                    MaterialTypeID = materialTypes[0].ID,
                },
                new Material()
                {
                    ID= Guid.NewGuid().ToString(),
                    Description = "Mặc định",
                    CreateDate = DateTime.Now,
                    Name ="Túi mang đi",
                    Quantity =9999,
                    UpdateDate = DateTime.Now,
                    Supplier = "Kho 1",

                    MaterialTypeID = materialTypes[0].ID,
                },
                new Material()
                {
                    ID= Guid.NewGuid().ToString(),
                    Description = "Mặc định",
                    CreateDate = DateTime.Now,
                    Name ="Cà Phê Rang Xay Original",
                    Quantity =1000,
                    UpdateDate = DateTime.Now,
                    Supplier = "Kho 1",

                    MaterialTypeID = materialTypes[0].ID,
                },
            };
            foreach (var item in materials)
            {
                context.Materials.Add(item);
            }
            context.SaveChanges();

            var roleAdmin = new AppRole()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Admin",

                Description = "Quản trị viên"
            };
            var roleManager = new AppRole()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Manger",

                Description = "Quản lý"
            };
            var roleBranch = new AppRole()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Branch",

                Description = "Quản lý chi nhánh"
            };
            var roleCustomer = new AppRole()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Staff",

                Description = "Nhân viên"
            };
            context.AppRoles.Add(roleAdmin);
            context.AppRoles.Add(roleManager);
            context.AppRoles.Add(roleCustomer);
            context.AppRoles.Add(roleBranch);
            context.SaveChanges();
            var hasher = new PasswordHasher<AppUser>();
            var admin = new AppUser()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "full",
                NormalizedUserName = "FULL",
                Email = "owlsng08@gmail.com",
                NormalizedEmail = "OWLSNG08@GMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "123123aA@"),
                SecurityStamp = Guid.NewGuid().ToString(),
                FirstName = "Nguyễn",
                LastName = "Phương",
                DateOfBirth = new DateTime(2000, 8, 17),
            };
            context.AppUsers.Add(admin);
            context.SaveChanges();
            foreach (var item in branches)
            {
                context.UserBranches.Add(new UserBranch()
                {
                    UserId = admin.Id,
                    BranchID = item.ID,
                });
            }
            context.SaveChanges();
            context.UserRoles.Add(new IdentityUserRole<string>()
            {
                UserId = admin.Id,
                RoleId = roleAdmin.Id,
            });
            context.SaveChanges();

        }
    }
}
