using Microsoft.AspNetCore.Identity;
using NumClass.Domain;

namespace Num_Nummy.Db
{
    public class Seed
    {
        // with usermanager the api manages users in stores
        public static async Task SeedData(Context context, UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var users = new List<AppUser>
                {
                    new AppUser
                    {
                        DisplayName = "Admin",
                        UserName = "Admin",
                        Email = "Admin@test.com",
                        Bio="ADMIN"
                        
                    },
                    new AppUser
                    {
                        DisplayName = "User",
                        UserName = "User",
                        Email = "User@test.com",
                        Bio="USER"
                    },

                };

                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "Pa$$w0rd");
                }
                await context.SaveChangesAsync();
            }
          
        }
    }
}