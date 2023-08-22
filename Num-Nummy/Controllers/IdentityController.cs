
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Num_Nummy.DTOs;
using Num_Nummy.Tools;
using NumClass.Domain;
using System.Security.Claims;

namespace Num_Nummy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly TokenServices _token;

        public IdentityController(UserManager<AppUser> userManager, TokenServices token)
        {
            _userManager = userManager;
            _token = token;

        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> logIn(LogInnDTO request)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(o => o.Email == request.Email);
            //  var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return Unauthorized();
            }
            var checkPassword = await _userManager.CheckPasswordAsync(user, request.Password);
            if (checkPassword)
            {

                return new UserDTO
                {
                    DisplayName = user.DisplayName,
                    UserName = user.UserName,
                    Tokens = _token.CreateToken(user),
                  

                };

            }


            return Unauthorized();


        }


        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> register(RegisterDTO request)
        {

            //method
            // var user = await _userManager.Users.AnyAsync(x=>x.UserName==request.Username);

            var user = await _userManager.FindByNameAsync(request.Username);
            var user2 = await _userManager.FindByEmailAsync(request.Email);


            {
                if (user != null)
                {
                    return BadRequest("username alrady exisit");
                }
                if (user2 != null)
                {
                    return BadRequest("Email alrady exisit");
                }
                var newUser = new AppUser
                {
                    Email = request.Email,
                    DisplayName = request.DisplayName,
                    Bio = "user",
                    UserName = request.Username
                };
                var adding = await _userManager.CreateAsync(newUser, request.Password);
                if (adding.Succeeded)
                {
                    return new UserDTO
                    {
                        UserName = newUser.DisplayName,
                        DisplayName = newUser.UserName,
                       
                        Tokens = _token.CreateToken(newUser)

                    };
                }
                return BadRequest("registering failed");
            }


        }


        [HttpGet("current")]
        
        public async Task<ActionResult<UserDTO>> currentUser()
        {


            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == User.FindFirstValue(ClaimTypes.Email));
            if (user != null)
            {
                return new UserDTO
                {
                    DisplayName = user.DisplayName,
                    UserName = user.UserName,
                    Tokens = _token.CreateToken(user),
                  

                };

            }
            else
            {
                return Unauthorized();
            }

        }








    }
}

