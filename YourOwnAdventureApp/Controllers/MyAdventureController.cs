using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YourOwnAdventureApp.Service.Interfaces;

namespace YourOwnAdventureApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyAdventureController : ControllerBase
    {
        private readonly IAdventureService _adventureUserService;
        private readonly ILogger<MyAdventureController> _logger;

        public MyAdventureController(
            IAdventureService adventureUserService,
            ILogger<MyAdventureController> logger)
        {
            _adventureUserService = adventureUserService;
            _logger = logger;
        }

        // GET api/<TakeAdventureController>/5
        [HttpGet("{userId}")]
        public async Task<IActionResult> Get([FromRoute] string userId)
        {
            _logger.LogInformation("Get adventures request received, UserId: " + userId);
            try
            {
                var adventures = await _adventureUserService.GetUserAdventureSelections(new Guid(userId)).ConfigureAwait(false);
                if (adventures == null || adventures?.Count == 0)
                {
                    return NotFound();
                }
                _logger.LogInformation("Get adventures request completed");
                return Ok(adventures);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured on processing the Get Adventure request: " + ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
