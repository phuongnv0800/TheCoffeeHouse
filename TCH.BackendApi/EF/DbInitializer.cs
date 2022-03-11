using Microsoft.AspNetCore.Identity;
using TCH.BackendApi.EF;

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
            //var categories = new Category[]{
            //    new Category() { Name = "Smartphone"},
            //    new Category(){ Name = "Laptop"}
            //};
            //foreach (var item in categories)
            //{
            //    context.Categories.Add(item);
            //}
            //context.SaveChanges();

            //var products = new Product[]{
            //    new Product(){
            //        Name = "Tempsoft",
            //        Price = 601,
            //        Quantity = 52,
            //        Description = "Sản phẩm của bạn có cốt truyện đặc biệt đặc biệt đối với bạn không? Rất có thể nó sẽ đặc biệt đặc biệt và được khán giả yêu mến. Sử dụng câu chuyện đó trong mô tả sản phẩm của bạn để thêm nhiều tính cách cho mặt hàng của bạn, thu hút khán giả và thu phục trái tim và khối óc.",
            //        CategoryId = 2
            //    },new Product() {
            //        Name = "Alphazap",
            //        Price = 980,
            //        Quantity = 2,
            //        Description = "Sản phẩm của bạn có cốt truyện đặc biệt đặc biệt đối với bạn không? Rất có thể nó sẽ đặc biệt đặc biệt và được khán giả yêu mến. Sử dụng câu chuyện đó trong mô tả sản phẩm của bạn để thêm nhiều tính cách cho mặt hàng của bạn, thu hút khán giả và thu phục trái tim và khối óc.",
            //        CategoryId = 1
            //    },new Product() {
            //        Name = "Cardguard",
            //        Price = 930,
            //        Quantity = 41,
            //        Description = "Sản phẩm của bạn có cốt truyện đặc biệt đặc biệt đối với bạn không? Rất có thể nó sẽ đặc biệt đặc biệt và được khán giả yêu mến. Sử dụng câu chuyện đó trong mô tả sản phẩm của bạn để thêm nhiều tính cách cho mặt hàng của bạn, thu hút khán giả và thu phục trái tim và khối óc.",
            //        CategoryId = 1
            //    },new Product() {
            //        Name = "Otcom",
            //        Price = 852,
            //        Quantity = 6,
            //        Description = "Sản phẩm của bạn có cốt truyện đặc biệt đặc biệt đối với bạn không? Rất có thể nó sẽ đặc biệt đặc biệt và được khán giả yêu mến. Sử dụng câu chuyện đó trong mô tả sản phẩm của bạn để thêm nhiều tính cách cho mặt hàng của bạn, thu hút khán giả và thu phục trái tim và khối óc.",
            //        CategoryId = 1
            //    },new Product() {
            //        Name = "Konklux",
            //        Price = 982,
            //        Quantity = 12,
            //        Description = "Sản phẩm của bạn có cốt truyện đặc biệt đặc biệt đối với bạn không? Rất có thể nó sẽ đặc biệt đặc biệt và được khán giả yêu mến. Sử dụng câu chuyện đó trong mô tả sản phẩm của bạn để thêm nhiều tính cách cho mặt hàng của bạn, thu hút khán giả và thu phục trái tim và khối óc.",
            //        CategoryId = 1,
            //    },new Product() {
            //        Name = "Transcof",
            //        Price = 840,
            //        Quantity = 12,
            //        Description = "Sản phẩm của bạn có cốt truyện đặc biệt đặc biệt đối với bạn không? Rất có thể nó sẽ đặc biệt đặc biệt và được khán giả yêu mến. Sử dụng câu chuyện đó trong mô tả sản phẩm của bạn để thêm nhiều tính cách cho mặt hàng của bạn, thu hút khán giả và thu phục trái tim và khối óc.",
            //        CategoryId = 2,
            //    },new Product() {
            //        Name = "Overhold",
            //        Price = 976,
            //        Quantity = 33,
            //        Description = "Sản phẩm của bạn có cốt truyện đặc biệt đặc biệt đối với bạn không? Rất có thể nó sẽ đặc biệt đặc biệt và được khán giả yêu mến. Sử dụng câu chuyện đó trong mô tả sản phẩm của bạn để thêm nhiều tính cách cho mặt hàng của bạn, thu hút khán giả và thu phục trái tim và khối óc.",
            //        CategoryId = 2,
            //    },new Product() {
            //        Name = "Zontrax",
            //        Price = 558,
            //        Quantity = 93,
            //        Description = "Sản phẩm của bạn có cốt truyện đặc biệt đặc biệt đối với bạn không? Rất có thể nó sẽ đặc biệt đặc biệt và được khán giả yêu mến. Sử dụng câu chuyện đó trong mô tả sản phẩm của bạn để thêm nhiều tính cách cho mặt hàng của bạn, thu hút khán giả và thu phục trái tim và khối óc.",
            //        CategoryId = 1,
            //    },new Product() {
            //        Name = "Rank",
            //        Price = 599,
            //        Quantity = 85,
            //        Description = "Sản phẩm của bạn có cốt truyện đặc biệt đặc biệt đối với bạn không? Rất có thể nó sẽ đặc biệt đặc biệt và được khán giả yêu mến. Sử dụng câu chuyện đó trong mô tả sản phẩm của bạn để thêm nhiều tính cách cho mặt hàng của bạn, thu hút khán giả và thu phục trái tim và khối óc.",
            //        CategoryId = 2,
            //    },new Product() {
            //        Name = "Trippledex",
            //        Price = 634,
            //        Quantity = 79,
            //        Description = "Sản phẩm của bạn có cốt truyện đặc biệt đặc biệt đối với bạn không? Rất có thể nó sẽ đặc biệt đặc biệt và được khán giả yêu mến. Sử dụng câu chuyện đó trong mô tả sản phẩm của bạn để thêm nhiều tính cách cho mặt hàng của bạn, thu hút khán giả và thu phục trái tim và khối óc.",
            //        CategoryId = 1,
            //    },new Product() {
            //        Name = "Zamit",
            //        Price = 652,
            //        Quantity = 21,
            //        Description = "Sản phẩm của bạn có cốt truyện đặc biệt đặc biệt đối với bạn không? Rất có thể nó sẽ đặc biệt đặc biệt và được khán giả yêu mến. Sử dụng câu chuyện đó trong mô tả sản phẩm của bạn để thêm nhiều tính cách cho mặt hàng của bạn, thu hút khán giả và thu phục trái tim và khối óc.",
            //        CategoryId = 1,
            //    }
            //};
            //foreach (var item in products)
            //{
            //    context.Products.Add(item);
            //}
            //context.SaveChanges();

            //var roleAdmin = new AppRole(){
            //    Id = Guid.NewGuid(),
            //    Name = "admin",
            //    NormalizedName = "ADMIN",
            //    Description = "Quản trị viên"
            //};
            //var roleCustomer = new AppRole(){
            //    Id = Guid.NewGuid(),
            //    Name = "customer",
            //    NormalizedName = "CUSTOMER",
            //    Description = "Khách hàng"
            //};
            //context.AppRoles.Add(roleAdmin);
            //context.AppRoles.Add(roleCustomer);
            //context.SaveChanges();
            //var hasher = new PasswordHasher<AppUser>();
            //var admin = new AppUser(){
            //        Id = Guid.NewGuid(),
            //        UserName = "admin",
            //        NormalizedUserName = "ADMIN",
            //        Email = "owlsng08@gmail.com",
            //        NormalizedEmail = "OWLSNG08@GMAIL.COM",
            //        EmailConfirmed = true,
            //        PasswordHash = hasher.HashPassword(null, "Admin@123"),
            //        SecurityStamp = Guid.NewGuid().ToString(),
            //        FirstName = "Nguyễn",
            //        LastName = "Phương",
            //        Dob = new DateTime(2000, 8, 17),
            //};
            //context.AppUsers.Add(admin);
            //context.SaveChanges();
        }
    }
}
