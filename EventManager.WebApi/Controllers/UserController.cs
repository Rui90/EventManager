using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using EventManager.Domain.Identity;
using EventManager.WebApi.ViewModels;

namespace EventManager.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController: ControllerBase
    {
        private readonly IConfiguration _config;

        private readonly UserManager<User> _userManager;

        private readonly SignInManager<User> _signInManager;

        private readonly IMapper _mapper;

        
        public UserController(
            IConfiguration configuration,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IMapper mapper
        ) {
            this._config = configuration;
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetUser() {
            return Ok(new User());
        }
        
        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserViewModel userViewModel) {
            try {
                var user = _mapper.Map<User>(userViewModel);
                var result = await _userManager.CreateAsync(user, userViewModel.Password);
                
                if (result.Succeeded) {
                    return Created("", _mapper.Map<UserViewModel>(user));
                }
                return BadRequest(result.Errors);
            } catch(Exception e) {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database failed {e.Message}");
            }
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginViewModel userViewModel) {
            try {
                var user = await _userManager.FindByNameAsync(userViewModel.UserName);
                var result = await _signInManager.CheckPasswordSignInAsync(user, userViewModel.Password, false);

                
                if (result.Succeeded) {
                    var appUser = await _userManager.Users.FirstOrDefaultAsync(u => u.NormalizedUserName == userViewModel.UserName.ToUpper());
                    var userToReturn = _mapper.Map<UserViewModel>(appUser);
                    return Ok( new {
                        token = GenerateJwtToken(appUser).Result,
                        user = userToReturn
                    });
                }
                return Unauthorized();
            } catch(Exception e) {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database failed {e.Message}");
            }
        }

        private async Task<string> GenerateJwtToken(User user) {
            var claims = new List<Claim> {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles) {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}