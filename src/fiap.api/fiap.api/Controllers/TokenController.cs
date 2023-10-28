using Microsoft.AspNetCore.Mvc;

using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace fiap.api.Controllers
{
    [Route("/token")]
    [ApiController]
    public class TokenController : Controller
    {
        [HttpPost]
        public IActionResult Create(TokenInfo model)
        {
            if (IsValidUserAndPassword(model))
            {
                var token = GenerateToken(model.UserName);

                //user => Guardar cria um refreshtoken

                return new OkObjectResult(new { token = token,  });
            }

            return new BadRequestResult();
        }

        private string GenerateToken(string userName)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, userName));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddMinutes(5)).ToUnixTimeSeconds().ToString()));

            var symmetricSecurityKey = new SymmetricSecurityKey(Security.GetKey());
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtHeader = new JwtHeader(signingCredentials);
            var jwtPayload = new JwtPayload(claims);

            var token = new JwtSecurityToken(jwtHeader, jwtPayload);

            var handler = new JwtSecurityTokenHandler();
            var tokenResult =handler.WriteToken(token);

            return tokenResult;
        }

        private bool IsValidUserAndPassword(TokenInfo model)
        {
            //fake login
            return model.UserName == model.Password;
        }
    }
    public class TokenInfo
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
