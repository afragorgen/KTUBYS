using KTUBYS.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();  // MVC için controller ve view desteği ekleniyor
builder.Services.AddControllers();  // API desteği ekleniyor

// Veritabanı bağlantısı için DbContext'i yapılandırıyoruz
builder.Services.AddDbContext<KTUBYSContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("KTUBYSContext") ?? throw new InvalidOperationException("Connection string 'KTUBYSContext' not found.")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// MVC ve API istekleri için gerekli middleware'leri ekliyoruz.
app.UseEndpoints(endpoints =>
{
    // MVC Endpoints (View rendering)
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    // API Endpoints
    endpoints.MapControllers();  // API controller'ları için
});

app.Run();


