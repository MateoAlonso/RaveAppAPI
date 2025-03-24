using Microsoft.AspNetCore.Mvc;
using RaveAppAPI.Services.Helpers;
using RaveAppAPI.Services.RequestModel.Security;

namespace RaveAppAPI.Controllers
{
    public class SecurityController : ApiController
    {
        private readonly IConfiguration _config;
        public SecurityController(IConfiguration config)
        {
            _config = config;
        }
        [HttpPost("Login")]
        public IActionResult Login(LoginRequest request)
        {
            if (request.Usuario == _config["APIUser"] && request.Pass == _config["APIPass"])
            {
                string token = JwtHelper.GenerateToken(_config["SigningKey"], _config["ValidIssuer"]);
                return Ok(MapLoginResponse(token));
            }
            return Unauthorized();
        }
        private LoginResponse MapLoginResponse(string token)
        {
            return new LoginResponse(token);
        }
    }
}
