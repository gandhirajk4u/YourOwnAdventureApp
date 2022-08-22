namespace YourOwnAdventureApp.UnitTests.ControllersTest
{
    public  class CreateAdventureControllerTests
    {
        private CreateAdventureController _adventureController;
        private Task<IActionResult> _result;
        private Mock<IAdventureService> _adventureService;
        private AdventureDto _adventureDto;

        /// <summary>
        /// Test Case for Create Adventure
        /// </summary>
        [Fact]
        public void CreateAdventure()
        {
            GivenCreateAdventureController();
            GivenAdventureCreatedFromRepository(new List<AdventureDto>() { new AdventureDto() {Name = "Skydiving",
                Path = ",Skydiving",
                CreatedBy = new Guid("8dbd24f7-ffcd-418b-95ef-ef0c6f23bd0d") } });
            WhenCreateAdventureIsCalled(new List<AdventureDto>() { new AdventureDto() {Name = "Skydiving",
                Path = ",Skydiving",
                CreatedBy = new Guid("8dbd24f7-ffcd-418b-95ef-ef0c6f23bd0d") } });
            ThenSuccessResponseIsReturned();

        }

        /// <summary>
        /// Test Case for Update Adventure
        /// </summary>
        [Fact]
        public void UpdateAdventure()
        {
            GivenCreateAdventureController();
            GivenAdventureUpdatedFromRepository(new List<AdventureDto>() { new AdventureDto() { AdventureId = "ee11e25a-76da-4bae-8b59-30fab4c987dd" ,Name = "Skydiving",
                Path = ",Skydiving",
                UpdatedBy = new Guid("8dbd24f7-ffcd-418b-95ef-ef0c6f23bd0d") } });
            WhenUpdateAdventureIsCalled(new List<AdventureDto>() { new AdventureDto() {AdventureId = "ee11e25a-76da-4bae-8b59-30fab4c987dd", Name = "Skydiving",
                Path = ",Skydiving",
                UpdatedBy = new Guid("8dbd24f7-ffcd-418b-95ef-ef0c6f23bd0d") } });
            ThenSuccessResponseIsReturned();

        }

        /// <summary>
        /// Test Case for Get Adventure
        /// </summary>
        [Fact]
        public void GetRequestReturnsAdventure()
        {
            GivenCreateAdventureController();
            GivenAdventureDbModelObject();
            GivenAdventureDetailsFromRepository();
            WhenGetAdventuresIsCalled();
            ThenSuccessResponseIsReturned();
        }

        /// <summary>
        /// Test Case for Get Adventure Not Found
        /// </summary>
        [Fact]
        public void GetRequestReturnsAdventureNotFound()
        {
            GivenCreateAdventureController();
            GivenAdventureDbModelObject();
            GivenAdventureEmptyFromRepository();
            WhenGetAdventuresIsCalled();
            ThenNotFoudResponseIsReturned();
        }       
        

        /// <summary>
        /// Test Case for Get Adventure By Path
        /// </summary>
        [Fact]
        public void GetRequestReturnsAdventureByPath()
        {
            GivenCreateAdventureController();
            GivenAdventureDbModelObject();
            GivenAdventureByPathFromRepository();
            WhenGetAdventuresByPathIsCalled();
            ThenSuccessResponseIsReturned();

        }

        /// <summary>
        /// Test Case for Get Adventure By Path Not Found
        /// </summary>
        [Fact]
        public void GetRequestReturnsAdventureByPathNotFound()
        {
            GivenCreateAdventureController();
            GivenAdventureDbModelObject();
            GivenAdventureByPathNullFromRepository();
            WhenGetAdventuresByPathIsCalled();
            ThenNotFoudResponseIsReturned();
        }

        private void GivenCreateAdventureController()
        {
            var iloggerMock = new Mock<ILogger<CreateAdventureController>>();
            _adventureService = new Mock<IAdventureService>();
            _adventureController = new CreateAdventureController(_adventureService.Object, iloggerMock.Object);
        }       

        private void GivenAdventureCreatedFromRepository(List<AdventureDto> adventureDto)
        {
            _adventureService.Setup(x => x.CreateNewAdventure(adventureDto)).Returns(Task.FromResult(adventureDto));
        }

        private void GivenAdventureUpdatedFromRepository(List<AdventureDto> adventureDto)
        {
            _adventureService.Setup(x => x.UpdateAdventure(adventureDto)).Returns(Task.FromResult(adventureDto));
        }

        private void WhenCreateAdventureIsCalled(List<AdventureDto> adventureDto)
        {
            _result = _adventureController.Create(adventureDto);
        }

        private void WhenUpdateAdventureIsCalled(List<AdventureDto> adventureDto)
        {
            _result = _adventureController.Update(adventureDto);
        }

        private async void ThenSuccessResponseIsReturned()
        {
            var result = await _result.ConfigureAwait(false) as OkObjectResult;
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(StatusCodes.Status200OK);
        }

        private async void ThenNotFoudResponseIsReturned()
        {
            var result = await _result.ConfigureAwait(false) as OkObjectResult;
            result.Should().BeNull();        
        }

        private void GivenAdventureDbModelObject()
        {
            _adventureDto = new AdventureDto()
            {
                Name = "Skydiving",
                Path = ",Skydiving",
                CreatedBy = new Guid("8dbd24f7-ffcd-418b-95ef-ef0c6f23bd0d")
            };
        }

        private void GivenAdventureDetailsFromRepository()
        {
            _adventureService.Setup(x => x.GetAdventures()).Returns(Task.FromResult(new List<AdventureResponseModel>() { new AdventureResponseModel { AdventureId = new Guid("8dbd24f7-ffcd-418b-95ef-ef0c6f23bd0d") , Name = "Skydiving", Path = ",Skydiving" } }));
        }

        private void GivenAdventureEmptyFromRepository()
        {
            _adventureService.Setup(x => x.GetAdventures()).Returns(Task.FromResult(new List<AdventureResponseModel>()));
        }

        private void GivenAdventureByPathFromRepository()
        {
            _adventureService.Setup(x => x.GetAdventuresByPath(",Skydiving")).Returns(Task.FromResult(new List<AdventureResponseModel>() { new AdventureResponseModel { AdventureId = new Guid("8dbd24f7-ffcd-418b-95ef-ef0c6f23bd0d"), Name = "Skydiving", Path = ",Skydiving" } }));
        }

        private void GivenAdventureByPathNullFromRepository()
        {
            _adventureService.Setup(x => x.GetAdventuresByPath(",Skydiving")).Returns(Task.FromResult(new List<AdventureResponseModel>()));
        }

        private void WhenGetAdventuresIsCalled()
        {
            _result = _adventureController.GetAdventures();
        }        

        private void WhenGetAdventuresByPathIsCalled()
        {
            _result = _adventureController.GetAdventureByPath(",Skydiving");
        }

    }
}
