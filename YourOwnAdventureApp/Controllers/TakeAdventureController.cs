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

        /// <summary>
        /// The endpoint get the user selected adventures tree
        /// </summary>       
        /// <param name="userId">The userId to get user selection. Example: "C0A0BF64-117C-4C46-A275-D5033C03348E"</param>
        /// <returns>OK</returns>
        /// <response code = "200">
        /// Sample Response:
        ///     
        ///     [
        ///         {
        ///             "userId": "c0a0bf64-117c-4c46-a275-d5033c03348e",
        ///             "adventureId": "85cf894d-1fd1-4d0a-be82-1dadf0b8fa17",
        ///             "name": "ScubaDiving",
        ///             "path": ",ScubaDiving"
        ///         },
        ///         {
        ///             "userId": "c0a0bf64-117c-4c46-a275-d5033c03348e",
        ///             "adventureId": "01ea8fb0-1c10-4b29-84db-269a0938e3ea",
        ///             "name": "KnowSwimming",
        ///             "path": ",ScubaDiving,KnowSwimming"
        ///         },
        ///         {
        ///             "userId": "c0a0bf64-117c-4c46-a275-d5033c03348e",
        ///             "adventureId": "2dfba092-2fb1-49e4-89ae-0615baa9594e",
        ///             "name": "Yes",
        ///             "path": ",ScubaDiving,KnowSwimming,Yes"
        ///         },
        ///         {
        ///             "userId": "c0a0bf64-117c-4c46-a275-d5033c03348e",
        ///             "adventureId": "d2b2101c-f42e-4c19-97ff-c067f33f590e",
        ///             "name": "AllowdToDive",
        ///             "path": ",ScubaDiving,KnowSwimming,Yes,GetReadyToDive"
        ///         }
        ///     ]
        /// </response>
        /// <response code="500">There was an unspecified internal error while processing the request</response>
        /// <response code="404">No adventure data found to return</response>
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

        /// <summary>
        /// The endpoint creates user tree selection
        /// </summary>        
        /// <returns>OK</returns>
        /// <remarks>
        /// Sample Request: 
        ///     
        ///     Post /TakeAdventure
        ///     [
        ///          {
        ///             "adventureid": "26516A58-D3B8-43D9-AD70-A1032DC8958D",
        ///             "userid": "c0a0bf64-117c-4c46-a275-d5033c03348e"        
        ///          },
        ///          {
        ///             "adventureid": "BE9B3CCE-6BAB-4622-BF84-DC7F3E70F1C4",
        ///             "userid": "c0a0bf64-117c-4c46-a275-d5033c03348e"        
        ///          },
        ///          {
        ///             "adventureid": "31508406-666E-4DE7-A6ED-47EAD9B72A45",
        ///             "userid": "c0a0bf64-117c-4c46-a275-d5033c03348e"
        ///          }
        ///     ]
        /// </remarks>   
        /// <response code = "200">
        /// Sample Response:
        /// 
        ///     [
        ///         {
        ///              "adventureId": "26516a58-d3b8-43d9-ad70-a1032dc8958d",
        ///              "userId": "c0a0bf64-117c-4c46-a275-d5033c03348e",
        ///              "createdDate": "2022-08-22T15:04:01.2175201+05:30",
        ///              "updatedDate": null
        ///         },
        ///         {
        ///              "adventureId": "be9b3cce-6bab-4622-bf84-dc7f3e70f1c4",
        ///              "userId": "c0a0bf64-117c-4c46-a275-d5033c03348e",
        ///              "createdDate": "2022-08-22T15:04:01.3786135+05:30",
        ///              "updatedDate": null
        ///         },
        ///         {
        ///              "adventureId": "31508406-666e-4de7-a6ed-47ead9b72a45",
        ///              "userId": "c0a0bf64-117c-4c46-a275-d5033c03348e",
        ///              "createdDate": "2022-08-22T15:04:01.3790404+05:30",
        ///              "updatedDate": null
        ///         }
        ///     ]             
        /// </response>
        /// <response code="500">There was an unspecified internal error while processing the request</response>
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

        /// <summary>
        /// The endpoint updates user tree selection
        /// </summary>        
        /// <returns>OK</returns>
        /// <remarks>
        /// Sample Request: 
        ///     
        ///     PUT /TakeAdventure
        ///     [
        ///          {
        ///             "adventureid": "26516A58-D3B8-43D9-AD70-A1032DC8958D",
        ///             "userid": "c0a0bf64-117c-4c46-a275-d5033c03348e"        
        ///          },
        ///          {
        ///             "adventureid": "BE9B3CCE-6BAB-4622-BF84-DC7F3E70F1C4",
        ///             "userid": "c0a0bf64-117c-4c46-a275-d5033c03348e"        
        ///          },
        ///          {
        ///             "adventureid": "31508406-666E-4DE7-A6ED-47EAD9B72A45",
        ///             "userid": "c0a0bf64-117c-4c46-a275-d5033c03348e"
        ///          }
        ///     ]
        /// </remarks>   
        /// <response code = "200">
        /// Sample Response:
        /// 
        ///     [
        ///         {
        ///              "adventureId": "26516a58-d3b8-43d9-ad70-a1032dc8958d",
        ///              "userId": "c0a0bf64-117c-4c46-a275-d5033c03348e",
        ///              "createdDate": "2022-08-22T15:04:01.2175201+05:30",
        ///              "updatedDate": null
        ///         },
        ///         {
        ///              "adventureId": "be9b3cce-6bab-4622-bf84-dc7f3e70f1c4",
        ///              "userId": "c0a0bf64-117c-4c46-a275-d5033c03348e",
        ///              "createdDate": "2022-08-22T15:04:01.3786135+05:30",
        ///              "updatedDate": null
        ///         },
        ///         {
        ///              "adventureId": "31508406-666e-4de7-a6ed-47ead9b72a45",
        ///              "userId": "c0a0bf64-117c-4c46-a275-d5033c03348e",
        ///              "createdDate": "2022-08-22T15:04:01.3790404+05:30",
        ///              "updatedDate": null
        ///         }
        ///     ]             
        /// </response>
        /// <response code="500">There was an unspecified internal error while processing the request</response>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] List<AdventureUserDto> adventureUserDto)
        {
            try
            {
                _logger.LogInformation("Update User adventure request received:" + JsonConvert.SerializeObject(adventureUserDto));
                var response = await _adventureUserService.UpdateUserAdventure(adventureUserDto).ConfigureAwait(false);
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
