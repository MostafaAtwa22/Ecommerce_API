namespace Ecommerce.API.Extensions
{
    public static class ApplicationRepositoryExtension
    {
        public static void AddRepositoryServices(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
        }
    }
}
