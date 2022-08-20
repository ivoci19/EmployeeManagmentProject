using EmployeeServices.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SharedModels.ViewModels;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmployeeProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;

        private readonly IUserServices _userServices;

        public LoginController(IConfiguration config, IUserServices userServices)
        {
            _config = config;
            _userServices = userServices;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] UserLogin userLogin)
        {
            var user = Authenticate(userLogin);
            if (user != null)
            {
                var token = Generate(user);
                return Ok(token);
            }
            return NotFound("User not found! Retry again");
        }

        private LoginTokens Generate(UserViewModel user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
           {
                new Claim(ClaimTypes.NameIdentifier, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Role, user.RoleName),
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Audience"],
              claims,
              expires: DateTime.Now.AddMinutes(15),
              signingCredentials: credentials);

            var tokenHandler = new JwtSecurityTokenHandler();

            return new LoginTokens
            {
                Token = tokenHandler.WriteToken(token),
                ExpirationDate = DateTime.Now.AddMinutes(15)
            };
        }

        private UserViewModel Authenticate(UserLogin userLogin)
        {
            var user = _userServices.GetUserByUsernameAndPassword(userLogin.UserName, userLogin.Password);
            if (user != null)
            {
                return user;
            }
            return null;
        }


    }
}
