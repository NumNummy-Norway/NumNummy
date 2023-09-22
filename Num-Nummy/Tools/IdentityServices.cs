using Microsoft.AspNetCore.Authentication.JwtBearer;
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

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(opts => {

                   //what to validate
                   opts.TokenValidationParameters = new TokenValidationParameters
                   {
                       //accept just the token which is signed
                       ValidateIssuerSigningKey = true,
                       //the same signing key to decrypt the token 
                       IssuerSigningKey = key,
                       ValidateIssuer = false,
                       ValidateAudience = false

                   };

               });
            services.AddAuthorization();
            services.AddScoped<TokenServices>(); 

           
            
            return services;
        }
    }
}
