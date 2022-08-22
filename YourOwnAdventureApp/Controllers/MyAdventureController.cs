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

        /// <summary>
        /// The endpoint get adventures tree with highlighted user choices - "isUserSelected": true 
        /// </summary>       
        /// <param name="userId">The userId to get user selection. Example: "C0A0BF64-117C-4C46-A275-D5033C03348E"</param>
        /// <returns>OK</returns>
        /// <response code = "200">
        /// Sample Response:
        ///     
        ///     [
        ///         {
        ///             "adventureId": "85cf894d-1fd1-4d0a-be82-1dadf0b8fa17",
        ///             "name": "ScubaDiving",
        ///             "path": ",ScubaDiving",
        ///             "isUserSelected": true
        ///         },
        ///         {
        ///             "adventureId": "01ea8fb0-1c10-4b29-84db-269a0938e3ea",
        ///             "name": "KnowSwimming",
        ///             "path": ",ScubaDiving,KnowSwimming",
        ///             "isUserSelected": true
        ///         },
        ///         {
        ///             "adventureId": "c3f597c1-3ccb-45b1-8da7-d28a20be00f1",
        ///             "name": "No",
        ///             "path": ",ScubaDiving,KnowSwimming,No",
        ///             "isUserSelected": false
        ///         },
        ///         {
        ///             "adventureId": "2dfba092-2fb1-49e4-89ae-0615baa9594e",
        ///             "name": "Yes",
        ///             "path": ",ScubaDiving,KnowSwimming,Yes",
        ///             "isUserSelected": true
        ///         },
        ///         {
        ///              "adventureId": "d2b2101c-f42e-4c19-97ff-c067f33f590e",
        ///              "name": "AllowdToDive",
        ///              "path": ",ScubaDiving,KnowSwimming,Yes,GetReadyToDive",
        ///              "isUserSelected": true
        ///         }
        ///     ]
        /// </response>
        /// <response code="500">There was an unspecified internal error while processing the request</response>
        /// <response code="404">No adventure data found to return</response>
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
