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
            try
            {
                var adventures = await _adventureService.GetAdventuresByPath(path).ConfigureAwait(false);
                if (adventures == null || adventures?.Count == 0)
                {
                    return NotFound();
                }
                _logger.LogInformation("Get adventures request completed: " + JsonConvert.SerializeObject(adventures));
                return Ok(adventures);
            }
            catch(Exception ex)
            {
                _logger.LogError("Error occured on processing the Get Adventure By Path request: " + ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }           
        }

        // GET api/<CreateAdventureController>/5
        [HttpGet()]
        public async Task<IActionResult> GetAdventures()
        {
            _logger.LogInformation("Get adventures request received");
            try
            {
                var adventures = await _adventureService.GetAdventures().ConfigureAwait(false);
                if (adventures == null || adventures?.Count == 0)
                {
                    return NotFound();
                }
                _logger.LogInformation("Get adventures request completed: " + JsonConvert.SerializeObject(adventures));
                return Ok(adventures);

            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured on processing the Get Adventure request: " + ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // POST api/<CreateAdventureController>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] List<AdventureDto> adventureDto)
        {
            try
            {
                _logger.LogInformation("Create adventure request received:" + JsonConvert.SerializeObject(adventureDto));
                var response = await _adventureService.CreateNewAdventure(adventureDto).ConfigureAwait(false);
                _logger.LogInformation("Create adventure request completed" + JsonConvert.SerializeObject(response));
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured on processing the Get Adventure request: " + ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // PUT api/<CreateAdventureController>/5
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] List<AdventureDto> adventureDto)
        {
            try
            {
                _logger.LogInformation("Create adventure request received:" + JsonConvert.SerializeObject(adventureDto));
                var response = await _adventureService.UpdateAdventure(adventureDto).ConfigureAwait(false);
                _logger.LogInformation("Create adventure request completed" + JsonConvert.SerializeObject(response));
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured on processing the Get Adventure request: " + ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
