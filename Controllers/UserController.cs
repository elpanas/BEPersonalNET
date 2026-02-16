using BEPersonal.DTOs.In;
using BEPersonal.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BEPersonal.Controllers
{    
    [Route("api/user")]
    [ApiController]
    [Produces("application/json")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Login(UserDTOIn user)
        {
            if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
                return Unauthorized();

            bool loggedUser = await _userService.Login(user);

            if (!loggedUser)
                return Unauthorized();

            var token = GenerateJwtToken(user.Email);

            return Ok(new { Token = token });
        }

        private string GenerateJwtToken(string email)
        {
            // Carica la chiave segreta e il tempo di scadenza dalle variabili d'ambiente
            var key = Environment.GetEnvironmentVariable("JWT_SECRET");
            var expireHours = int.Parse(Environment.GetEnvironmentVariable("JWT_EXPIRE_HOURS")!);

            // Converte la chiave segreta in un array di byte
            var keyBytes = Encoding.UTF8.GetBytes(key!);

            // Crea i claims per il token (info sull'utente)
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // Crea le credenziali di firma usando la chiave segreta e l'algoritmo HMAC SHA256
            var creds = new SigningCredentials(
                    new SymmetricSecurityKey(keyBytes),
                    SecurityAlgorithms.HmacSha256
                    );

            // Crea il token JWT
            var token = new JwtSecurityToken(
                issuer: "myIssuer", // chi emette il token
                audience: "myAudience", // chi può usare il token
                claims: claims, // informazioni sull'utente
                expires: DateTime.UtcNow.AddHours(expireHours), // scadenza del token
                signingCredentials: creds // credenziali di firma
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
