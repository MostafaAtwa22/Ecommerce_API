using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;

namespace Ecommerce.API.Extensions
{
    public static class SwaggerServicesExtension
    {
        public static void AddSwaggerExtension(this IServiceCollection services)
        {
            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "API Ecommerce",
                    Description = "How to make new things (payment, Shopping carts, ...)",
                    TermsOfService = new Uri("https://www.google.com"),
                    Contact = new OpenApiContact
                    {
                        Name = "Ecommerce",
                        Email = "atwamostafa5@gmail.com",
                        Url = new Uri("https://www.google.com")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Ecommerce",
                        Url = new Uri("https://www.google.com")
                    }
                });

                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your token in the input field.\r\nExample: \"Bearer abcdef12345\""
                });

                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
            });
        }
    }
}
