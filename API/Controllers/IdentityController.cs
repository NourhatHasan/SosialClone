using API.DTOs;
using API.Services;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using sosialClone;
using System.Security.Claims;

namespace API.Controllers
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
        public async Task<ActionResult<userDTO>> logIn(logInDTo request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return Unauthorized();
            }
            var checkPassword = await _userManager.CheckPasswordAsync(user, request.Password);
            if (checkPassword)
            {
                return new userDTO
                {
                    DisplayName = user.DisplayName,
                    UserName = user.UserName,
                    Picture = null,
                    Tokens = _token.CreateToken(user)

                };

            }
            return Unauthorized();
        }


        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<userDTO>> register(RegisterDto request)
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
                    UserName = request.Username
                };
                var adding = await _userManager.CreateAsync(newUser, request.Password);
                if (adding.Succeeded)
                {
                    return new userDTO
                    {
                        UserName = newUser.DisplayName,
                        DisplayName = newUser.UserName,
                        Picture = null,
                        Tokens = _token.CreateToken(newUser)

                    };
                }
                return BadRequest("registering failed");
            }


        }


        [HttpGet("current")]
        public async Task<ActionResult<userDTO>> currentUser()
        {
            var user =await _userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));
            if (user!=null)
            {
                return new userDTO
                {
                    DisplayName = user.DisplayName,
                    UserName = user.UserName,
                    Tokens = _token.CreateToken(user),
                    Picture = null,
                    


                };

            } else
              {
                return Unauthorized();
              }  
           
        }





       
        

        }
    }

