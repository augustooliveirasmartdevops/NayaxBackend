using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Nayax.Dex.Application.Interfaces;

namespace Nayax.Dex.Api.Controllers
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthenticateController : ControllerBase
    {
        private readonly IAuthApplication _authApplication;
        private readonly ILogger _logger;

        public AuthenticateController(IAuthApplication authApplication, ILogger<AuthenticateController> logger)
        {
            _authApplication = authApplication;
            _logger = logger;
        }

        [HttpPost()]
        [Authorize(AuthenticationSchemes = "Basic")]
        public async Task<IActionResult> AuthenticateUserAsync()
        {
            try
            {
                return Ok(await Task.FromResult(new { Message = "Authorized access" }));
            }
            catch (Exception)
            {
                //_logger.LogError(ex, "Error authenticating user {UserName}", request.UserName);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Message = "An unexpected error occurred. Please try again."
                });
            }
        }
    }
}
