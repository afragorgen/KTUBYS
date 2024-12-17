using KTUBYS.Data;  // Veritabanı bağlamı için
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Veritabanı bağlantısını yapılandırıyoruz
builder.Services.AddDbContext<KTUBYSContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("KTUBYSContext")));

// MVC ve Controller'lar için servisleri ekliyoruz
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Middleware yapılandırmaları
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Varsayılan rotayı ayarlıyoruz (HomeController ve Index action)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

