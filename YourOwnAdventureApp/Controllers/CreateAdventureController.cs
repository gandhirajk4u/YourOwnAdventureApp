using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using YourOwnAdventureApp.Models.Models;
using YourOwnAdventureApp.Service.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace YourOwnAdventureApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreateAdventureController : ControllerBase
    {
        private readonly IAdventureService _adventureService;
        private readonly ILogger<CreateAdventureController> _logger;

        public CreateAdventureController(
            IAdventureService adventureService,
            ILogger<CreateAdventureController> logger)
        {
            _adventureService = adventureService;
            _logger = logger;
        }

        // GET api/<CreateAdventureController>/5
        [HttpGet("{path}")]
        public async Task<IActionResult> GetAdventureByPath([FromRoute] string path)
        {
            _logger.LogInformation("Get adventures request received");
            var adventures = await _adventureService.GetAdventuresByPath(path).ConfigureAwait(false);
            if (adventures == null)
            {
                return NotFound();
            }
            _logger.LogInformation("Get adventures request completed");
            return Ok(adventures);
        }

        // GET api/<CreateAdventureController>/5
        [HttpGet()]
        public async Task<IActionResult> GetAdventures()
        {
            _logger.LogInformation("Get adventures request received");
            var adventures = await _adventureService.GetAdventures().ConfigureAwait(false);
            if (adventures == null)
            {
                return NotFound();
            }
            _logger.LogInformation("Get adventures request completed");
            return Ok(adventures);
        }

        // POST api/<CreateAdventureController>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] List<AdventureDto> adventureDto)
        {
            _logger.LogInformation("Create adventure request received:" + JsonConvert.SerializeObject(adventureDto));
            await _adventureService.CreateNewAdventure(adventureDto).ConfigureAwait(false);
            _logger.LogInformation("Create adventure request completed");
            return Ok();
        }

        // PUT api/<CreateAdventureController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }
    }
}
