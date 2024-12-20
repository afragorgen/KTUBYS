using Microsoft.EntityFrameworkCore;
using KTUBYS;  // Context ve diğer gerekli namespace'ler
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Swagger servisini ekleyin
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Diğer servisleri eklemeye devam edin
        builder.Services.AddDbContext<KTUBYSContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("KTUBYSContext")));
        builder.Services.AddControllers();

        var app = builder.Build();

        // Swagger'ı etkinleştirin
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();  // Swagger'ı etkinleştirin
            app.UseSwaggerUI();  // Swagger UI'yi etkinleştirin
        }

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}

