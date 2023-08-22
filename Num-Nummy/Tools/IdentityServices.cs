
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Num_Nummy.Db;
using NumClass.Domain;
using System.Text;

namespace Num_Nummy.Tools
{
    public static class IdentityServices
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services,
                 IConfiguration config)
        {
            services.AddIdentityCore<AppUser>(opt =>
            {
                opt.Password.RequireNonAlphanumeric = false;

            })
            .AddEntityFrameworkStores<Context>();


            services.AddScoped<TokenServices>(); 

            services.AddAuthentication();
            
            return services;
        }
    }
}
