using Ecommerce.Core.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Infrastructure
{
    public class ApplicationIdentityDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<ApplicationUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new ApplicationUser
                {
                    DisplayName = "Mostafa",
                    Email = "m@gmail.com",
                    UserName = "Mostafa22",
                    Address = new Address
                    {
                        FirstName = "Mostafa",
                        LastName = "Atwa",
                        Street = "Atwa Street",
                        City = "Cairo",
                        State = "NY",
                        Zipcode = "200"
                    }
                };

                await userManager.CreateAsync(user, "Mostafa@123");
            }
        }
    }
}
