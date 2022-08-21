namespace YourOwnAdventureApp.UnitTests.ServiceTests
{
    public class AdventureServiceTests
    {
        private IAdventureService _adventureService;
        private IMapper _mapper;
        private Mock<IAdventureRepository> _madventureRepository;
        private List<AdventureDbModel> _givenAdventureDbModel;
        private List<AdventureResponseModel> _actualAdventureDbModel;
        private List<MyAdventureResponseModel> _givenmyAdventureResponseModel;
        private List<MyAdventureResponseModel> _actualmyAdventureResponseModel;
        [Fact]
        private void GetAdventureServiceTest()
        {
            GivenAdventureService();
            GivenAdventureDBModel();
            GivenGetAdventureDataRepository();
            WhenGetAdventuresIsCalled();
            ThenSuccessResponseIsReturned();
        }

        [Fact]
        private void GetAdventureServiceByPathTest()
        {
            GivenAdventureService();
            GivenAdventureDBModel();
            GivenGetAdventureByPathDataRepository();
            WhenGetAdventureByPathIsCalled();
            ThenSuccessResponseIsReturned();
        }

        [Fact]
        private void GetUserAdventureSelectionsTest()
        {
            GivenAdventureService();
            GivenMyAdventureResponseModel();
            GivenMyAdventureDataRepository();
            WhenMyAdventuresIsCalled();
            ThenMyAdventureSuccessResponseIsReturned();
        }

        private void GivenAdventureService()
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new YourOwnAdventureApp.MappingProfile.MappingProfile());
            });
            _mapper = mapperConfiguration.CreateMapper();
            _madventureRepository = new Mock<IAdventureRepository>();
            _adventureService = new AdventureService(_mapper, _madventureRepository.Object);
        }

        private void GivenAdventureDBModel()
        {
            _givenAdventureDbModel = new List<AdventureDbModel>()
            {
                new AdventureDbModel()
                {
                    AdventureId = new Guid("8d03e0ee-6800-4f9c-b098-1cf75f47099a"),
                    Name = "Skydiving",
                    Path = ",Skydiving"
                }
            };
        }

        private void GivenMyAdventureResponseModel()
        {
            _givenmyAdventureResponseModel = new List<MyAdventureResponseModel>()
            {
                new MyAdventureResponseModel()
                {
                    AdventureId = new Guid("8d03e0ee-6800-4f9c-b098-1cf75f47099a"),
                    Name = "Skydiving",
                    Path = ",Skydiving",
                    IsUserSelected = true
                }
            };
        }

        private void GivenGetAdventureDataRepository()
        {
            _madventureRepository.Setup(x => x.GetAdventures()).Returns(Task.FromResult(_givenAdventureDbModel));            
        }

        private void GivenGetAdventureByPathDataRepository()
        {
            _madventureRepository.Setup(x => x.GetAdventuresByPath(",Skydiving")).Returns(Task.FromResult(_givenAdventureDbModel));
        }

        private void GivenMyAdventureDataRepository()
        {
            _madventureRepository.Setup(x => x.GetAdventuresWithUserSelection(new Guid("660406cf-e4c1-4d76-9c6e-c6315603be65"))).Returns(Task.FromResult(_givenmyAdventureResponseModel));
        }

        private async void WhenGetAdventuresIsCalled()
        {
            _actualAdventureDbModel = await _adventureService.GetAdventures();
        }

        private async void WhenGetAdventureByPathIsCalled()
        {
            _actualAdventureDbModel = await _adventureService.GetAdventuresByPath(",Skydiving");
        }

        private async void WhenMyAdventuresIsCalled()
        {
            _actualmyAdventureResponseModel = await _adventureService.GetUserAdventureSelections(new Guid("660406cf-e4c1-4d76-9c6e-c6315603be65"));
        }

        private void ThenSuccessResponseIsReturned()
        {
            _madventureRepository.VerifyAll();
            Assert.Equal(expected: _givenAdventureDbModel[0].AdventureId, actual: _actualAdventureDbModel[0].AdventureId);
            Assert.Equal(expected: _givenAdventureDbModel[0].Name, actual: _actualAdventureDbModel[0].Name);
            Assert.Equal(expected: _givenAdventureDbModel[0].Path, actual: _actualAdventureDbModel[0].Path);
        }

        private void ThenMyAdventureSuccessResponseIsReturned()
        {
            _madventureRepository.VerifyAll();
            Assert.Equal(expected: _givenmyAdventureResponseModel[0].AdventureId, actual: _actualmyAdventureResponseModel[0].AdventureId);
            Assert.Equal(expected: _givenmyAdventureResponseModel[0].Name, actual: _actualmyAdventureResponseModel[0].Name);
            Assert.Equal(expected: _givenmyAdventureResponseModel[0].Path, actual: _actualmyAdventureResponseModel[0].Path);
            Assert.Equal(expected: _givenmyAdventureResponseModel[0].IsUserSelected, actual: _actualmyAdventureResponseModel[0].IsUserSelected);
        }

    }
}
