var builder = WebApplication.CreateBuilder(args);

// Servislerin eklenmesi
builder.Services.AddControllers();  // API Controller'ları için
builder.Services.AddEndpointsApiExplorer(); // API'ler için
builder.Services.AddSwaggerGen(); // Swagger dokümantasyonu için

// Veritabanı bağlantısı eklenmesi
builder.Services.AddDbContext<KTUBYSContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("KTUBYSContext")));

var app = builder.Build();

// Middleware eklemeleri
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
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

app.MapControllers(); // API controller'larını yönlendirme

app.Run();

