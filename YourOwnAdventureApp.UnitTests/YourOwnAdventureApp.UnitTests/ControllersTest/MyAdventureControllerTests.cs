namespace YourOwnAdventureApp.UnitTests.ControllersTest
{
    public class MyAdventureControllerTests
    {
        private MyAdventureController _myAdventureController;
        private Task<IActionResult> _result;
        private Mock<IAdventureService> _adventureService;

        /// <summary>
        /// Test Case for Get User Adventure Selections
        /// </summary>
        [Fact]
        public void GetUserAdventureSelections()
        {
            GivenMyAdventureController();
            GivenMyAdventureDetailsFromRepository();
            WhenGetAdventuresIsCalled();
            ThenSuccessResponseIsReturned();
        }

        /// <summary>
        /// Test Case for Get User Adventure Selections - Null
        /// </summary>
        [Fact]
        public void GetUserAdventureSelectionsNull()
        {
            GivenMyAdventureController();
            GivenMyAdventureDetailsNullFromRepository();
            WhenGetAdventuresIsCalled();
            ThenNullResponseIsReturned();
        }

        /// <summary>
        /// Test Case for Get User Adventure Selections - Invalid Input
        /// </summary>
        [Fact]
        public void GetUserAdventureSelectionsError()
        {
            GivenMyAdventureController();
            WhenGetAdventuresIsCalledWithErrorRequest();
            ThenNullResponseIsReturned();
        }

        private void GivenMyAdventureController()
        {
            var iloggerMock = new Mock<ILogger<MyAdventureController>>();
            _adventureService = new Mock<IAdventureService>();
            _myAdventureController = new MyAdventureController(_adventureService.Object, iloggerMock.Object);
        }

        private void GivenMyAdventureDetailsFromRepository()
        {
            _adventureService.Setup(x => x.GetUserAdventureSelections(new Guid("5e49881b-8e11-4b15-85a7-51d96624a6b0"))).Returns(Task.FromResult(new List<MyAdventureResponseModel>() { new MyAdventureResponseModel { AdventureId = new Guid("8dbd24f7-ffcd-418b-95ef-ef0c6f23bd0d"), Name = "Skydiving", Path = ",Skydiving", IsUserSelected = true } }));
        }

        private void GivenMyAdventureDetailsNullFromRepository()
        {
            _adventureService.Setup(x => x.GetUserAdventureSelections(new Guid("5e49881b-8e11-4b15-85a7-51d96624a6b0"))).Returns(Task.FromResult(new List<MyAdventureResponseModel>()));
        }

        private void WhenGetAdventuresIsCalled()
        {
            _result = _myAdventureController.Get("5e49881b-8e11-4b15-85a7-51d96624a6b0");
        }

        private void WhenGetAdventuresIsCalledWithErrorRequest()
        {
            _result = _myAdventureController.Get("Test");
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
