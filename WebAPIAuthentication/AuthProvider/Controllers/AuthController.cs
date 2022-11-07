using AuthProvider.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthProvider.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthController(
            IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public ActionResult Login([FromBody] UserApiModel model)
        {
            
            if (!string.IsNullOrWhiteSpace(model.Email)&& !string.IsNullOrWhiteSpace(model.Password) && !string.IsNullOrWhiteSpace(model.Name))
            {

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, model.Name),
                    new Claim(ClaimTypes.Email, model.Email),
                    new Claim(ClaimTypes.Role, model.RoleType.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                var token = GetToken(authClaims);

                return Ok(new JwtSecurityTokenHandler().WriteToken(token));
            }
            return BadRequest();
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(5),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }


    }
}