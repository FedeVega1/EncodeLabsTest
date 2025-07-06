using Microsoft.EntityFrameworkCore;
using EncodeLabsTest.Models;
using EncodeLabsTest.Settings;

namespace EncodeLabsTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();
            builder.Services.AddDbContext<ProductosContext>(opt => opt.UseInMemoryDatabase("TodoList"));
            builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

            IConfigurationSection? appSettingsSection = builder.Configuration.GetSection("AppSettings");
            AppSettings? appSettings = appSettingsSection?.Get<AppSettings>();

            if (appSettings != null) 
                builder.WebHost.ConfigureKestrel(options =>
                {
                    options.ListenAnyIP(appSettings.Port, listenOptions => listenOptions.UseHttps());
                });

            WebApplication? app = builder.Build();

            app.UseHttpsRedirection();
            app.UseAuthorization();

            app.MapControllers();
            app.Run();
        }
    }
}
