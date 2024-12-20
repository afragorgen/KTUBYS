using Microsoft.EntityFrameworkCore;
using KTUBYS;  // Context ve diğer gerekli namespace'ler

var builder = WebApplication.CreateBuilder(args);

// 1. Servislerin eklenmesi
builder.Services.AddControllers(); // API Controller'larını kullanabilmek için
builder.Services.AddEndpointsApiExplorer(); // API'ler için
builder.Services.AddSwaggerGen(); // Swagger dokümantasyonu için

// 2. Veritabanı bağlantısı (SQL Server kullanıyorsanız)
builder.Services.AddDbContext<KTUBYSContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("KTUBYSContext")));

// 3. Uygulama ayarları
var app = builder.Build();

// 4. Middleware (Ara katmanlar) eklemeleri
if (app.Environment.IsDevelopment())
{
    // Geliştirme ortamında Swagger dokümantasyonu ve UI
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// 5. API Controller'larını yönlendirme
app.MapControllers(); // API controller'larını haritalandırma

app.Run();


