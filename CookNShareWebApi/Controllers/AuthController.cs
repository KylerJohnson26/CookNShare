using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;
using CookNShareWebApi.Dtos;
using CookNShareWebApi.Models;
using CookNShareWebApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System;

namespace CookNShareWebApi.Controllers
{
    [Route("api/[controller]")]
    public class AuthController: Controller
    {
        private readonly IAuthService _authService;
        private readonly IConfiguration _configuration;
        public AuthController(IAuthService authService, IConfiguration configuration)
        {
            _authService = authService;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserForRegisterDto userForRegisterDto)
        {
            if(!string.IsNullOrEmpty(userForRegisterDto.Username))
                userForRegisterDto.Username = userForRegisterDto.Username.ToLower();

            if(await _authService.UserExists(userForRegisterDto.Username))
                ModelState.AddModelError("Username", "Username already exists");

            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var userToCreate = new User
            {
                Username = userForRegisterDto.Username,
                EmailAddress = userForRegisterDto.EmailAddress,
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                DateOfBirth = userForRegisterDto.DateOfBirth,
                DateCreated = DateTime.Now,
                LastActive = DateTime.Now
            };

            var createdUser = await _authService.Register(userToCreate, userForRegisterDto.Password);
            return StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserForLoginDto userForLoginDto)
        {
            var user = await _authService.Login(userForLoginDto.Username.ToLower(), userForLoginDto.Password);

            if(user == null)
                return Unauthorized();
            
            //generate token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Token").Value);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Username)
                }),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new {tokenString});
        }


    }
}