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
            _logger.LogInformation("Get User adventures request received, UserId: " + userId);
            var adventures = await _adventureUserService.GetUserAdventureSelections(new Guid(userId)).ConfigureAwait(false);
            if (adventures == null)
            {
                return NotFound();
            }
            _logger.LogInformation("Get adventures request completed");
            return Ok(adventures);
        }
    }
}
