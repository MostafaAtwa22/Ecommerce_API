namespace Ecommerce.API.Extensions
{
    public static class ConnectionStringsExtension
    {
        public static void AddDefaultConnectionStringService(this WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("No Connection String");

            builder.Services.AddDbContext<ApplicationDbContext>(option =>
            {
                option.UseSqlServer(connectionString);
            });
        }
    }
}
