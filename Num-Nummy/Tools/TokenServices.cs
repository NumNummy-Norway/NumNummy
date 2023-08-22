using Microsoft.IdentityModel.Tokens;
using NumClass.Domain;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Num_Nummy.Tools
{
    public class TokenServices
    {
        private readonly IConfiguration _conf;

        public TokenServices(IConfiguration conf)
        {
            _conf = conf;
        }

        //our token will be a string
        public string CreateToken(AppUser user)
        {
            //claims that the user claims about themeselves
            //we will put the claims in the token and extract
            //from it when the API Controllers recieves it 

            //we use this claims to authenticate så
            // we use it when we even
            //get a user from the database or
            //establish a new user
            var claims = new List<Claim>()
            {
                //name Claim will be what the user adds as a username
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim(ClaimTypes.Email,user.Email),
            };

            //creating the key to sign in 
            //the same key is used to encrypt and decrypt
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_conf["TokenKey"]));

            //the token will be signed using symetric securityKey with a signature Algorithme
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            //now we descripe our token
            var TokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(3),
                SigningCredentials = creds
            };

            //creating and returning the token 
            var TokenHandler = new JwtSecurityTokenHandler();
            var token = TokenHandler.CreateToken(TokenDescriptor);
            return TokenHandler.WriteToken(token);

        }
        //we did create a token which can be valid or not 
        // we need now to accept the valid token in IdentityServices(program.cs)
    }
}