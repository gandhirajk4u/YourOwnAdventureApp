using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IO;
using YourOwnAdventureApp.Models.Models;
using YourOwnAdventureApp.Service.Interfaces;
using YourOwnAdventureApp.Service.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace YourOwnAdventureApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TakeAdventureController : ControllerBase
    {
        private readonly IAdventureUserService _adventureUserService;
        private readonly ILogger<TakeAdventureController> _logger;

        public TakeAdventureController(
            IAdventureUserService adventureUserService,
            ILogger<TakeAdventureController> logger)
        {
            _adventureUserService = adventureUserService;
            _logger = logger;
        }

        // GET api/<TakeAdventureController>/5
        [HttpGet("{userId}")]
        public async Task<IActionResult> Get([FromRoute] string userId)
        {
            _logger.LogInformation("Get User adventures request received, UserId: " + userId);
            try
            {
                var adventures = await _adventureUserService.GetUsersAdventure(new Guid(userId)).ConfigureAwait(false);
                if (adventures == null || adventures?.Count == 0)
                {
                    return NotFound();
                }
                _logger.LogInformation("Get adventures request completed");
                return Ok(adventures);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured on processing the Get User adventure request: " + ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // POST api/<TakeAdventureController>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] List<AdventureUserDto> adventureUserDto)
        {
            try
            {
                _logger.LogInformation("Create User adventure request received:" + JsonConvert.SerializeObject(adventureUserDto));
                var response = await _adventureUserService.CreateNewUserAdventure(adventureUserDto).ConfigureAwait(false);
                _logger.LogInformation("Create User adventure request completed");
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured on processing the Create User adventure request: " + ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // POST api/<TakeAdventureController>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] List<AdventureUserDto> adventureUserDto)
        {
            try
            {
                _logger.LogInformation("Update User adventure request received:" + JsonConvert.SerializeObject(adventureUserDto));
                var response = await _adventureUserService.CreateNewUserAdventure(adventureUserDto).ConfigureAwait(false);
                _logger.LogInformation("Update User adventure request completed");
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured on processing the Update User adventure request: " + ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

    }
}
