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
        public async Task<IActionResult> UploadDexFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest(new { Message = "No file was sent." });
            }

            try
            {
                using var reader = new StreamReader(file.OpenReadStream());
                var dexText = await reader.ReadToEndAsync();
                await _dexApplication.UploadDexFileAsync(dexText);

                return Ok(new { Message = "File processed successfully" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing DEX file");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { ex.Message });
            }
        }
    }
}
