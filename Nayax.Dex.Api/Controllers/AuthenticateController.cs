using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Nayax.Dex.Api.Controllers
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Basic")]
    public class AuthenticateController : ControllerBase
    {
        [HttpPost]
        public IActionResult Get() => Ok(new { Message = "Authorized access" });
    }
}
