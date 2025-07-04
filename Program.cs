using Microsoft.EntityFrameworkCore;
using EncodeLabsTest.Models;

namespace EncodeLabsTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();
            builder.Services.AddDbContext<ProductosContext>(opt => opt.UseInMemoryDatabase("TodoList"));

            WebApplication? app = builder.Build();

            app.UseHttpsRedirection();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
