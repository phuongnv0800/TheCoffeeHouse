using Microsoft.AspNetCore.Identity;
using TCH.BackendApi.Entities;

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
                    ID= Guid.NewGuid().ToString(),
                    Name = "Cà Phê Việt Nam",
                    UpdateDate = DateTime.Now,
                    CreateDate = DateTime.Now,
                    Description = "Mặc định",
                },
                new Category() {
                    ID= Guid.NewGuid().ToString(),
                    Name = "Cà Phê Máy",
                    UpdateDate = DateTime.Now,
                    CreateDate = DateTime.Now,
                    Description = "Mặc định",
                },
                new Category() {
                    ID= Guid.NewGuid().ToString(),
                    Name = "Cold Brew",
                    UpdateDate = DateTime.Now,
                    CreateDate = DateTime.Now,
                    Description = "Mặc định",
                },
                new Category() {
                    ID= Guid.NewGuid().ToString(),
                    Name = "Trà trái cây",
                    UpdateDate = DateTime.Now,
                    CreateDate = DateTime.Now,
                    Description = "Mặc định",
                },
                new Category() {
                    ID= Guid.NewGuid().ToString(),
                    Name = "Trà sữa Macchiato",
                    UpdateDate = DateTime.Now,
                    CreateDate = DateTime.Now,
                    Description = "Mặc định",
                },
                new Category() {
                    ID= Guid.NewGuid().ToString(),
                    Name = "Đá xay",
                    UpdateDate = DateTime.Now,
                    CreateDate = DateTime.Now,
                    Description = "Mặc định",
                },
                new Category() {
                    ID= Guid.NewGuid().ToString(),
                    Name = "Matcha - Sô cô la",
                    UpdateDate = DateTime.Now,
                    CreateDate = DateTime.Now,
                    Description = "Mặc định",
                },
                new Category() {
                    ID= Guid.NewGuid().ToString(),
                    Name = "Bánh mặn",
                    UpdateDate = DateTime.Now,
                    CreateDate = DateTime.Now,
                    Description = "Mặc định",
                },
                new Category() {
                    ID= Guid.NewGuid().ToString(),
                    Name = "Bánh ngọt",
                    UpdateDate = DateTime.Now,
                    CreateDate = DateTime.Now,
                    Description = "Mặc định",
                },
                new Category() {
                    ID= Guid.NewGuid().ToString(),
                    Name = "Snack",
                    UpdateDate = DateTime.Now,
                    CreateDate = DateTime.Now,
                    Description = "Mặc định",
                },
                new Category() {
                    ID= Guid.NewGuid().ToString(),
                    Name = "Cà phê tại nhà",
                    UpdateDate = DateTime.Now,
                    CreateDate = DateTime.Now,
                    Description = "Mặc định",
                },
                new Category() {
                    ID= Guid.NewGuid().ToString(),
                    Name = "Trà tại nhà",
                    UpdateDate = DateTime.Now,
                    CreateDate = DateTime.Now,
                    Description = "Mặc định",
                },
                new Category() {
                    ID= Guid.NewGuid().ToString(),
                    Name = "Bộ sưu tập quà tặng",
                    UpdateDate = DateTime.Now,
                    CreateDate = DateTime.Now,
                    Description = "Mặc định",
                },
                new Category() {
                    ID= Guid.NewGuid().ToString(),
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

            var branches = new Branch[]
            {
                new Branch() {
                    ID= Guid.NewGuid().ToString(),
                    Name  = "HP Đỗ Nhuận",
                    Area  = "Hải phòng",
                    City  ="Hải phòng",
                    Email  = "thecoffeehouse@coffee.com",
                    District  ="Ngô Quyền" ,
                    Adderss  = "Thửa đất số 172A, 3 Đỗ Nhuận, Đằng Giang, Ngô Quyền, Hải Phòng",
                    CreateDate  =DateTime.Now ,
                    UpdateDate  = DateTime.Now,
                    UserCreateID  = null,
                    UserUpdateID  = null,
                },
                new Branch() {
                    ID= Guid.NewGuid().ToString(),
                    Name  = "HP Lạch Tray",
                    Area  = "Hải phòng",
                    City  ="Hải phòng",
                    Email  = "thecoffeehouse@coffee.com",
                    District  ="Ngô Quyền" ,
                    Adderss  = "382-384 Lạch Tray, Q. Ngô Quyền, Hải Phòng",
                    CreateDate  =DateTime.Now ,
                    UpdateDate  = DateTime.Now,
                    UserCreateID  = null,
                    UserUpdateID  = null,
                },
                new Branch() {
                    ID= Guid.NewGuid().ToString(),
                    Name  = "HP Cát Bi Plaza",
                    Area  = "Hải phòng",
                    City  ="Hải phòng",
                    Email  = "thecoffeehouse@coffee.com",
                    District  ="Ngô Quyền" ,
                    Adderss  = "01 Lê Hồng Phong, Q. Ngô Quyền, Hải Phòng",
                    CreateDate  =DateTime.Now ,
                    UpdateDate  = DateTime.Now,
                    UserCreateID  = null,
                    UserUpdateID  = null,
                },
                new Branch() {
                    ID= Guid.NewGuid().ToString(),
                    Name  = "HP Aeon Mall Lê Chân",
                    Area  = "Hải phòng",
                    City  ="Hải phòng",
                    Email  = "thecoffeehouse@coffee.com",
                    District  ="Ngô Quyền" ,
                    Adderss  = "Tầng 01 Aeon Mall, Q.Lê Chân, Hải Phòng",
                    CreateDate  =DateTime.Now ,
                    UpdateDate  = DateTime.Now,
                    UserCreateID  = null,
                    UserUpdateID  = null,
                },
                new Branch() {
                    ID= Guid.NewGuid().ToString(),
                    Name  = "HP Cầu Đất 2",
                    Area  = "Hải phòng",
                    City  ="Hải phòng",
                    Email  = "thecoffeehouse@coffee.com",
                    District  ="Ngô Quyền" ,
                    Adderss  = "2 - 4 Cầu Đất, Q. Ngô Quyền, Hải Phòng",
                    CreateDate  =DateTime.Now ,
                    UpdateDate  = DateTime.Now,
                    UserCreateID  = null,
                    UserUpdateID  = null,
                },
                new Branch() {
                    ID= Guid.NewGuid().ToString(),
                    Name  = "HP Trần Quang Khải",
                    Area  = "Hải phòng",
                    City  ="Hải phòng",
                    Email  = "thecoffeehouse@coffee.com",
                    District  ="Q. Hồng Bàng" ,
                    Adderss  = "17 Trần Quang Khải, Q. Hồng Bàng, Hải Phòng",
                    CreateDate  =DateTime.Now ,
                    UpdateDate  = DateTime.Now,
                    UserCreateID  = null,
                    UserUpdateID  = null,
                },
                new Branch() {
                    ID= Guid.NewGuid().ToString(),
                    Name  = "HP Trần Phú",
                    Area  = "Hải phòng",
                    City  ="Hải phòng",
                    Email  = "thecoffeehouse@coffee.com",
                    District  ="Ngô Quyền" ,
                    Adderss  = "15 Trần Phú, Lương Khánh Thiện, Ngô Quyền, Hải Phòng",
                    CreateDate  =DateTime.Now ,
                    UpdateDate  = DateTime.Now,
                    UserCreateID  = null,
                    UserUpdateID  = null,
                },
                new Branch() {
                    ID= Guid.NewGuid().ToString(),
                    Name  = "HP Điện Biên Phủ",
                    Area  = "Hải phòng",
                    City  ="Hải phòng",
                    Email  = "thecoffeehouse@coffee.com",
                    District  ="Q. Hồng Bàng" ,
                    Adderss  = "86 Điện Biên Phủ, Hồng Bàng, Hải Phòng, Việt Nam",
                    CreateDate  =DateTime.Now ,
                    UpdateDate  = DateTime.Now,
                    UserCreateID  = null,
                    UserUpdateID  = null,
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
                    Unit = "Bean",
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
                    Unit = "Bean",
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
                    Unit = "Bean",
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
                    Unit = "Bean",
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
                    Unit = "Bean",
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
                    PriceOfUnit = 5000,
                    UpdateDate = DateTime.Now,
                    UserCreateID =null,
                    UserUpdateID =null,
                    Supplier = "Kho 1",
                    Unit = "Cốc",
                    MaterialTypeID = materialTypes[0].ID,
                },
                new Material()
                {
                    ID= Guid.NewGuid().ToString(),
                    Description = "Mặc định",
                    CreateDate = DateTime.Now,
                    Name ="Thìa nhựa",
                    Quantity =9999,
                    PriceOfUnit = 1000,
                    UpdateDate = DateTime.Now,
                    UserCreateID =null,
                    UserUpdateID =null,
                    Supplier = "Kho 1",
                    Unit = "Cai",
                    MaterialTypeID = materialTypes[0].ID,
                },
                new Material()
                {
                    ID= Guid.NewGuid().ToString(),
                    Description = "Mặc định",
                    CreateDate = DateTime.Now,
                    Name ="Ống hút",
                    Quantity =9999,
                    PriceOfUnit = 1000,
                    UpdateDate = DateTime.Now,
                    UserCreateID =null,
                    UserUpdateID =null,
                    Supplier = "Kho 1",
                    Unit = "Cái",
                    MaterialTypeID = materialTypes[0].ID,
                },
                new Material()
                {
                    ID= Guid.NewGuid().ToString(),
                    Description = "Mặc định",
                    CreateDate = DateTime.Now,
                    Name ="Túi mang đi",
                    Quantity =9999,
                    PriceOfUnit = 200,
                    UpdateDate = DateTime.Now,
                    UserCreateID =null,
                    UserUpdateID =null,
                    Supplier = "Kho 1",
                    Unit = "Túi",
                    MaterialTypeID = materialTypes[0].ID,
                },
                new Material()
                {
                    ID= Guid.NewGuid().ToString(),
                    Description = "Mặc định",
                    CreateDate = DateTime.Now,
                    Name ="Cà Phê Rang Xay Original",
                    Quantity =1000,
                    PriceOfUnit = 230000,
                    UpdateDate = DateTime.Now,
                    UserCreateID =null,
                    UserUpdateID =null,
                    Supplier = "Kho 1",
                    Unit = "Túi",
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
                NormalizedName = "ADMIN",
                Description = "Quản trị viên"
            };
            var roleManager = new AppRole()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Manger",
                NormalizedName = "MANAGER",
                Description = "Quản lý"
            };
            var roleBranch = new AppRole()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Branch",
                NormalizedName = "BRANCH",
                Description = "Quản lý chi nhánh"
            };
            var roleCustomer = new AppRole()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Staff",
                NormalizedName = "STAFF",
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
