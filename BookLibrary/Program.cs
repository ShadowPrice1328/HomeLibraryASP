using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using BookLibrary.Data;
using BookLibrary.Interfaces;
using BookLibrary.Repositories;

namespace BookLibrary
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddRazorPages();

            builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("ConnectionString"));

            builder.Services.AddDbContext<AppDbContext>((serviceProvider, options) =>
            {
                var dbSettings = serviceProvider.GetRequiredService<IOptions<DatabaseSettings>>().Value;
                if (options.UseSqlServer($"Server={dbSettings.Server};Database={dbSettings.Database};Trusted_Connection={dbSettings.Trusted_Connection};Encrypt={dbSettings.Encrypt};") == null)
                    throw new InvalidOperationException("Connection string was not found.");                
            });

            builder.Services.AddScoped<IBookRepository, BookRepository>();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}
