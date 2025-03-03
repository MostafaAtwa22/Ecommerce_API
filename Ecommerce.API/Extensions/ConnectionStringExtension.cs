namespace Ecommerce.API.Extensions
{
    public static class ConnectionStringExtension
    {
        public static void AddConnectionStringService(this WebApplicationBuilder builder)
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
