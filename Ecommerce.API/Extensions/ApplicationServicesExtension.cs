using Ecommerce.API.Errors;
using Ecommerce.Core.Models.Identity;
using Ecommerce.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.API.Extensions
{
    public static class ApplicationServicesExtension
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddHttpContextAccessor();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.Configure<ApiBehaviorOptions>(option =>
            {
                option.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState
                        .Where(e => e.Value!.Errors.Count > 0)
                        .SelectMany(x => x.Value!.Errors)
                        .Select(x => x.ErrorMessage)
                        .ToArray();

                    var errorResponse = new ApiValidationErrorResponse
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(errorResponse);
                };
            });

            services.AddCors(opt =>
            {
                opt.AddPolicy("Policy", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200");
                });
            });

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
            })
           .AddEntityFrameworkStores<ApplicationDbContext>()
           .AddDefaultTokenProviders();
        }
    }
}
