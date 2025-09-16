using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CleanApiSample.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public ActionResult<LoginResponse> Login([FromBody] LoginRequest request)
        {
            var configuredUser = _configuration["Jwt:UserName"]
                ?? throw new InvalidOperationException("Jwt:UserName configuration is missing.");
            var configuredPassword = _configuration["Jwt:Password"]
                ?? throw new InvalidOperationException("Jwt:Password configuration is missing.");

            if (!string.Equals(request.UserName, configuredUser, StringComparison.Ordinal) ||
                !string.Equals(request.Password, configuredPassword, StringComparison.Ordinal))
            {
                return Unauthorized();
            }

            var token = GenerateToken(request.UserName);
            return Ok(new LoginResponse(token));
        }

        private string GenerateToken(string userName)
        {
            var key = _configuration["Jwt:Key"]
                ?? throw new InvalidOperationException("Jwt:Key configuration is missing.");
            var issuer = _configuration["Jwt:Issuer"]
                ?? throw new InvalidOperationException("Jwt:Issuer configuration is missing.");
            var audience = _configuration["Jwt:Audience"]
                ?? throw new InvalidOperationException("Jwt:Audience configuration is missing.");

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, userName) }),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public record LoginRequest(string UserName, string Password);

        public record LoginResponse(string Token);
    }
}
