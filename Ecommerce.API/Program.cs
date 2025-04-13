using Ecommerce.API.Extensions;
using Ecommerce.API.Helpers;
using Ecommerce.API.Middlewares;
using Ecommerce.Core.Models.Identity;
using Ecommerce.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Ecommerce.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services
            builder.AddDefaultConnectionStringService();
            builder.Services.AddJWTServices(builder.Configuration);
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddApplicationServices();
            builder.Services.AddAutoMapper(typeof(MappingProfiles));
            builder.Services.AddSwaggerExtension();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                await ApplicationIdentityDbContextSeed.SeedUsersAsync(userManager);
            }

            // Apply migrations
            await app.ApplyMigrationsAsync();

            // Set ExceptionMiddleware as the FIRST middleware
            app.UseMiddleware<ExceptionMiddleware>();

            // Enable Swagger in development
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Handle 404 and status code errors
            app.UseStatusCodePagesWithReExecute("/errors/{0}");

            app.UseStaticFiles();

            app.UseCors();

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
