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

        /// <summary>
        /// The endpoint get the adventure tree based on materialized path
        /// </summary>       
        /// <param name="path">The tree path value Example: ,Sky_diving</param>
        /// <returns>OK</returns>
        /// <response code = "200">
        /// Sample Response:
        ///     
        ///     [
        ///         {
        ///             "adventureId": "055998f6-af60-4292-9c3a-47e48cb891c2",
        ///             "name": "Sky_diving",
        ///             "path": ",Sky_diving"
        ///         },
        ///         {
        ///             "adventureId": "b1ccebc3-fdda-46ad-bde8-8c735b1f6d4d",
        ///              "name": "TrainedDiver",
        ///             "path": ",Sky_diving,TrainedDiver"
        ///         },
        ///         {
        ///             "adventureId": "801a5458-da51-45c7-ab89-b1810b38375c",
        ///             "name": "Yes",
        ///             "path": ",Sky_diving,TrainedDiver,Yes"
        ///         },
        ///         {
        ///             "adventureId": "890064ca-f31d-418e-ba7d-5064510b6589",
        ///             "name": "AllowdToDive",
        ///             "path": ",Sky_diving,TrainedDiver,Yes,AllowdToDive"
        ///         },
        ///         {
        ///             "adventureId": "49d807b1-de86-4adb-a94b-3b2b0d981005",
        ///             "name": "No",
        ///             "path": ",Sky_diving,TrainedDiver,No"
        ///         }         
        ///     ]
        /// </response>
        /// <response code="500">There was an unspecified internal error while processing the request</response>
        /// <response code="404">No adventure data found to return</response>
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

        /// <summary>
        /// The endpoint get the full adventure tree 
        /// </summary>        
        /// <returns>OK</returns>
        /// <response code = "200">
        /// Sample Response:
        ///     
        ///     [
        ///         {
        ///             "adventureId": "055998f6-af60-4292-9c3a-47e48cb891c2",
        ///             "name": "Sky_diving",
        ///             "path": ",Sky_diving"
        ///         },
        ///         {
        ///             "adventureId": "b1ccebc3-fdda-46ad-bde8-8c735b1f6d4d",
        ///              "name": "TrainedDiver",
        ///             "path": ",Sky_diving,TrainedDiver"
        ///         },
        ///         {
        ///             "adventureId": "801a5458-da51-45c7-ab89-b1810b38375c",
        ///             "name": "Yes",
        ///             "path": ",Sky_diving,TrainedDiver,Yes"
        ///         },
        ///         {
        ///             "adventureId": "890064ca-f31d-418e-ba7d-5064510b6589",
        ///             "name": "AllowdToDive",
        ///             "path": ",Sky_diving,TrainedDiver,Yes,AllowdToDive"
        ///         },
        ///         {
        ///             "adventureId": "49d807b1-de86-4adb-a94b-3b2b0d981005",
        ///             "name": "No",
        ///             "path": ",Sky_diving,TrainedDiver,No"
        ///         }         
        ///     ]
        /// </response>
        /// <response code="500">There was an unspecified internal error while processing the request</response>
        /// <response code="404">No adventure data found to return</response>
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

        /// <summary>
        /// The endpoint creates the adventure tree 
        /// </summary>        
        /// <returns>OK</returns>
        /// <remarks>
        /// Sample Request: 
        ///     
        ///     Post /CreateAdventure
        ///     [
        ///         {
        ///              "name": "ScubaDiving",
        ///              "path": ",ScubaDiving",
        ///              "createdby": "fd1d8766-7019-4b5a-813a-ee6a367ae33b"
        ///         },
        ///         {
        ///              "name": "KnowSwimming",
        ///              "path": ",ScubaDiving,KnowSwimming",
        ///              "createdby": "fd1d8766-7019-4b5a-813a-ee6a367ae33b"        
        ///         },
        ///         {
        ///              "name": "Yes",
        ///              "path": ",ScubaDiving,KnowSwimming,Yes",
        ///              "createdby": "fd1d8766-7019-4b5a-813a-ee6a367ae33b"
        ///         },
        ///         {
        ///              "name": "GetReadyToDive",
        ///              "path": ",ScubaDiving,KnowSwimming,Yes,GetReadyToDive",
        ///              "createdby": "fd1d8766-7019-4b5a-813a-ee6a367ae33b"
        ///         },
        ///         {
        ///              "name": "No",
        ///              "path": ",ScubaDiving,KnowSwimming,No",
        ///              "createdby": "fd1d8766-7019-4b5a-813a-ee6a367ae33b"
        ///         }
        ///     ]
        /// </remarks>   
        /// <response code = "200">
        /// Sample Response:
        /// 
        ///     [
        ///         {
        ///              "adventureId": "26516a58-d3b8-43d9-ad70-a1032dc8958d",
        ///              "name": "ScubaDiving",
        ///              "path": ",ScubaDiving",
        ///              "createdBy": "fd1d8766-7019-4b5a-813a-ee6a367ae33b",
        ///              "updatedBy": "00000000-0000-0000-0000-000000000000",
        ///              "createdDate": "2022-08-22T14:16:10.3140044+05:30",
        ///              "updatedDate": null
        ///         },
        ///         {
        ///              "adventureId": "be9b3cce-6bab-4622-bf84-dc7f3e70f1c4",
        ///              "name": "KnowSwimming",
        ///              "path": ",ScubaDiving,KnowSwimming",
        ///              "createdBy": "fd1d8766-7019-4b5a-813a-ee6a367ae33b",
        ///              "updatedBy": "00000000-0000-0000-0000-000000000000",
        ///              "createdDate": "2022-08-22T14:16:10.4877841+05:30",
        ///              "updatedDate": null
        ///          },
        ///          {
        ///              "adventureId": "099efb3d-421e-4c3b-872a-2141b9621eaa",
        ///              "name": "Yes",
        ///              "path": ",ScubaDiving,KnowSwimming,Yes",
        ///              "createdBy": "fd1d8766-7019-4b5a-813a-ee6a367ae33b",
        ///              "updatedBy": "00000000-0000-0000-0000-000000000000",
        ///              "createdDate": "2022-08-22T14:16:10.4882695+05:30",
        ///              "updatedDate": null
        ///          },
        ///          {
        ///              "adventureId": "45e0aa9a-7bf2-47b7-aa6d-740b48f96765",
        ///              "name": "AllowdToDive",
        ///              "path": ",ScubaDiving,KnowSwimming,Yes,GetReadyToDive",
        ///              "createdBy": "fd1d8766-7019-4b5a-813a-ee6a367ae33b",
        ///              "updatedBy": "00000000-0000-0000-0000-000000000000",
        ///              "createdDate": "2022-08-22T14:16:10.4883033+05:30",
        ///              "updatedDate": null
        ///          },
        ///          {
        ///              "adventureId": "31508406-666e-4de7-a6ed-47ead9b72a45",
        ///              "name": "No",
        ///              "path": ",ScubaDiving,KnowSwimming,No",
        ///              "createdBy": "fd1d8766-7019-4b5a-813a-ee6a367ae33b",
        ///              "updatedBy": "00000000-0000-0000-0000-000000000000",
        ///              "createdDate": "2022-08-22T14:16:10.4883503+05:30",
        ///              "updatedDate": null
        ///           }
        ///     ]             
        /// </response>
        /// <response code="500">There was an unspecified internal error while processing the request</response>        
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

        /// <summary>
        /// The endpoint updates the adventure tree 
        /// </summary>        
        /// <returns>OK</returns>
        /// <remarks>
        /// Sample Request: 
        ///     
        ///     Put /CreateAdventure
        ///     [    
        ///         {
        ///              "adventureId": "31508406-666e-4de7-a6ed-47ead9b72a45",
        ///              "name": "Not_Known",
        ///              "path": ",ScubaDiving,KnowSwimming,Not_Known",
        ///              "updatedby": "fd1d8766-7019-4b5a-813a-ee6a367ae33b"
        ///         }
        ///     ]
        /// </remarks>   
        /// <response code = "200">
        /// Sample Response:
        /// 
        ///     [               
        ///          {
        ///              "adventureId": "31508406-666e-4de7-a6ed-47ead9b72a45",
        ///              "name": "Not_Known",
        ///              "path": ",ScubaDiving,KnowSwimming,Not_Known",
        ///              "createdBy": "00000000-0000-0000-0000-000000000000",
        ///              "updatedBy": "fd1d8766-7019-4b5a-813a-ee6a367ae33b",
        ///              "createdDate": null,
        ///              "updatedDate": "2022-08-22T14:55:34.4831285+05:30"
        ///           }
        ///     ]             
        /// </response>
        /// <response code="500">There was an unspecified internal error while processing the request</response>
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
