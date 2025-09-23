using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Nayax.Dex.Application.Interfaces;

namespace Nayax.Dex.Api.Controllers
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Basic")]
    public class DataExchangeController : ControllerBase
    {
        private readonly IDexApplication _dexApplication;
        private readonly ILogger<DataExchangeController> _logger;

        public DataExchangeController(IDexApplication dexApplication, ILogger<DataExchangeController> logger)
        {
            _dexApplication = dexApplication;
            _logger = logger;
        }

        [HttpPost("uploadDexFile")]
        public async Task<IActionResult> UploadDexFileAsync([FromForm] FormFileCollection files)
        {
            try
            {
                if (files == null || files.Count == 0)
                {
                    return BadRequest(new { Message = "No files were sent." });
                }

                await _dexApplication.UploadDexFileAsync();

                return Ok(new { Message = "Files uploaded successfully" });
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
