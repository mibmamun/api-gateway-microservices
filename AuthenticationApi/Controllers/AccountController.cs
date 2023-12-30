using JwtAuthManager;
using JwtAuthManager.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly JwtTokenHandler _jwtTokenHandler;
        public AccountController(JwtTokenHandler jwtTokenHandler)
        {
            _jwtTokenHandler = jwtTokenHandler;
        }

        [HttpPost]
        public ActionResult<AuthResponse?> Authenticate([FromBody] AuthRequest request)
        {
            var response = _jwtTokenHandler.GenerateJwtToken(request);
            if (response == null)
            {
                return Unauthorized();
            }
            return response;
        }

    }
}
