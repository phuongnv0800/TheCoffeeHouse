using Microsoft.AspNetCore.Identity;
using TCH.BackendApi.Entities;

namespace TCH.BackendApi.EF
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();
            if (context.Categories.Any())
            {
                return;   // DB has been seeded
            }
            var categories = new Category[]{
                new Category() { 
                    ID= Guid.NewGuid().ToString(),
                    Name = "Coffee",
                    UpdateDate = DateTime.Now,
                    CreateDate = DateTime.Now,
                },
                new Category() {
                    ID= Guid.NewGuid().ToString(),
                    Name = "Trà sữa",
                    UpdateDate = DateTime.Now,
                    CreateDate = DateTime.Now,
                },
                new Category() {
                    ID= Guid.NewGuid().ToString(),
                    Name = "Trà trái cây",
                    UpdateDate = DateTime.Now,
                    CreateDate = DateTime.Now,
                },
                new Category() {
                    ID= Guid.NewGuid().ToString(),
                    Name = "Đồ đá xay",
                    UpdateDate = DateTime.Now,
                    CreateDate = DateTime.Now,
                },
                new Category() {
                    ID= Guid.NewGuid().ToString(),
                    Name = "Thưởng thức tại nhà",
                    UpdateDate = DateTime.Now,
                    CreateDate = DateTime.Now,
                },
                new Category() {
                    ID= Guid.NewGuid().ToString(),
                    Name = "Bánh - snack",
                    UpdateDate = DateTime.Now,
                    CreateDate = DateTime.Now,
                },
                new Category() {
                    ID= Guid.NewGuid().ToString(),
                    Name = "Combo",
                    UpdateDate = DateTime.Now,
                    CreateDate = DateTime.Now,
                },
            };
            
            foreach (var item in categories)
            {
                context.Categories.Add(item);
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
            
            foreach(var item in memberTypes)
            {
                context.MemberTypes.Add(item);
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
                Name = "MangerBranh",
                NormalizedName = "MANAGERBRANCH",
                Description = "Khách hàng"
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
            context.SaveChanges();
            var hasher = new PasswordHasher<AppUser>();
            var admin = new AppUser()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "owlsng08@gmail.com",
                NormalizedEmail = "OWLSNG08@GMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "123456"),
                SecurityStamp = Guid.NewGuid().ToString(),
                FirstName = "Nguyễn",
                LastName = "Phương",
                DateOfBirth = new DateTime(2000, 8, 17),
            };
            context.AppUsers.Add(admin);
            context.SaveChanges();

        }
    }
}
