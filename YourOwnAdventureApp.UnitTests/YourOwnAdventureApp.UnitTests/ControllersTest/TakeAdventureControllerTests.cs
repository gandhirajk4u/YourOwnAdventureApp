namespace YourOwnAdventureApp.UnitTests.ControllersTest
{
    public class TakeAdventureControllerTests
    {
        private TakeAdventureController _adventureController;
        private Task<IActionResult> _result;
        private Mock<IAdventureUserService> _adventureUserService;
        private AdventureUserDto _adventureUserDto;

        /// <summary>
        /// Test Case for Create User Adventure
        /// </summary>
        [Fact]
        public void CreateUserAdventure()
        {
            GivenTakeAdventureController();
            GivenAdventureUserCreatedFromRepository(new List<AdventureUserDto>() { new AdventureUserDto() {UserId = new Guid ("ee11e25a-76da-4bae-8b59-30fab4c987dd"),
                AdventureId = new Guid("8dbd24f7-ffcd-418b-95ef-ef0c6f23bd0d")} });
            WhenTakeAdventureIsCalled(new List<AdventureUserDto>() { new AdventureUserDto() {UserId = new Guid ("ee11e25a-76da-4bae-8b59-30fab4c987dd"),
                AdventureId = new Guid("8dbd24f7-ffcd-418b-95ef-ef0c6f23bd0d")} });
            ThenSuccessResponseIsReturned();

        }

        /// <summary>
        /// Test Case for Upadate User Adventure
        /// </summary>
        [Fact]
        public void UpdateUserAdventure()
        {
            GivenTakeAdventureController();
            GivenAdventureUserUpdateFromRepository(new List<AdventureUserDto>() { new AdventureUserDto() {UserId = new Guid ("ee11e25a-76da-4bae-8b59-30fab4c987dd"),
                AdventureId = new Guid("8dbd24f7-ffcd-418b-95ef-ef0c6f23bd0d"), } });
            WhenTakeUpdateAdventureIsCalled(new List<AdventureUserDto>() { new AdventureUserDto() {UserId = new Guid ("ee11e25a-76da-4bae-8b59-30fab4c987dd"),
                AdventureId = new Guid("8dbd24f7-ffcd-418b-95ef-ef0c6f23bd0d")} });
            ThenSuccessResponseIsReturned();

        }

        /// <summary>
        /// Test Case for Get Adventure By Path
        /// </summary>
        [Fact]
        public void GetRequestReturnsUserAdvanture()
        {
            GivenTakeAdventureController();
            GivenAdventureUserDbModelObject();
            GivenAdventureUserFromRepository();
            WhenGetUserAdventuresIsCalled();
            ThenSuccessResponseIsReturned();
        }

        /// <summary>
        /// Test Case for Get Adventure for User Not Found
        /// </summary>
        [Fact]
        public void GetRequestReturnsNullUserAdvanture()
        {
            GivenTakeAdventureController();
            GivenAdventureUserDbModelObject();
            GivenAdventureUserNullFromRepository();
            WhenGetUserAdventuresIsCalled();
            ThenNullResponseIsReturned();
        }

        /// <summary>
        /// Test Case for Get Adventure  for User with Invalid Input
        /// </summary>
        [Fact]
        public void GetRequestReturnsUserAdvantureWithInvalidInput()
        {
            GivenTakeAdventureController();
            GivenAdventureUserDbModelObject();
            WhenGetUserAdventuresIsCalledWithInvalidInput();
            ThenNullResponseIsReturned();
        }

        private void GivenTakeAdventureController()
        {
            var iloggerMock = new Mock<ILogger<TakeAdventureController>>();
            _adventureUserService = new Mock<IAdventureUserService>();
            _adventureController = new TakeAdventureController(_adventureUserService.Object, iloggerMock.Object);
        }

        private void GivenAdventureUserCreatedFromRepository(List<AdventureUserDto> adventureDto)
        {
            _adventureUserService.Setup(x => x.CreateNewUserAdventure(adventureDto)).Returns(Task.FromResult(adventureDto));
        }       

        private void GivenAdventureUserUpdateFromRepository(List<AdventureUserDto> adventureDto)
        {
            _adventureUserService.Setup(x => x.UpdateUserAdventure(adventureDto)).Returns(Task.FromResult(adventureDto));
        }

        private void GivenAdventureUserDbModelObject()
        {
            _adventureUserDto = new AdventureUserDto()
            {
                UserId = new Guid("ee11e25a-76da-4bae-8b59-30fab4c987dd"),
                AdventureId = new Guid("67298803-82a8-4a8a-b179-80134ff6c5de")
                
            };
        }

        private void GivenAdventureUserFromRepository()
        {
            _adventureUserService.Setup(x => x.GetUsersAdventure(new Guid("1b5db65f-7fb3-4363-8796-ab60507dd90e"))).Returns(Task.FromResult(new List<UserAdventureResponseModel>() { new UserAdventureResponseModel { AdventureId = new Guid("8dbd24f7-ffcd-418b-95ef-ef0c6f23bd0d"), UserId = new Guid("1b5db65f-7fb3-4363-8796-ab60507dd90e"), Name = "Skydiving", Path = ",Skydiving" } }));
        }

        private void GivenAdventureUserNullFromRepository()
        {
            _adventureUserService.Setup(x => x.GetUsersAdventure(new Guid("1b5db65f-7fb3-4363-8796-ab60507dd90e"))).Returns(Task.FromResult(new List<UserAdventureResponseModel>()));
        }

        private void WhenTakeUpdateAdventureIsCalled(List<AdventureUserDto> adventureDto)
        {
            _result = _adventureController.Update(adventureDto);
        }

        private void WhenTakeAdventureIsCalled(List<AdventureUserDto> adventureDto)
        {
            _result = _adventureController.Create(adventureDto);
        }

        private void WhenGetUserAdventuresIsCalled()
        {
            _result = _adventureController.Get("1b5db65f-7fb3-4363-8796-ab60507dd90e");
        }

        private void WhenGetUserAdventuresIsCalledWithInvalidInput()
        {
            _result = _adventureController.Get("Test");
        }

        private async void ThenSuccessResponseIsReturned()
        {
            var result = await _result.ConfigureAwait(false) as OkObjectResult;
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(StatusCodes.Status200OK);
        }

        private async void ThenNullResponseIsReturned()
        {
            var result = await _result.ConfigureAwait(false) as OkObjectResult;
            result.Should().BeNull();            
        }
    }
}
