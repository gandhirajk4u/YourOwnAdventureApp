namespace YourOwnAdventureApp.UnitTests.ServiceTests
{
    public class AdventureUserServiceTests
    {
        private IAdventureUserService _adventureUserService;
        private IMapper _mapper;
        private Mock<IAdventureUserRepository> _madventureUserRepository;        
        private List<UserAdventureResponseModel> _givenAdventureUserResponseModel;
        private List<UserAdventureResponseModel> _actualAdventureUserResponseModel;

        [Fact]
        private void GetUserAdventureServiceTest()
        {
            GivenUserAdventureService();
            GivenAdventureResponseModel();
            GivenGetAdventureByPathDataRepository();
            WhenGetUserAdventureIsCalled();
            ThenSuccessResponseIsReturned();
        }

        private void GivenUserAdventureService()
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new YourOwnAdventureApp.MappingProfile.MappingProfile());
            });
            _mapper = mapperConfiguration.CreateMapper();
            _madventureUserRepository = new Mock<IAdventureUserRepository>();
            _adventureUserService = new AdventureUserService(_mapper, _madventureUserRepository.Object);
        }

        private void GivenAdventureResponseModel()
        {
            _givenAdventureUserResponseModel = new List<UserAdventureResponseModel>()
            {
                new UserAdventureResponseModel()
                {
                    AdventureId = new Guid("8d03e0ee-6800-4f9c-b098-1cf75f47099a"),
                    UserId =  new Guid("835e2700-27e7-49c2-a09e-072f35a7fd19")
                }
            };
        }

        private void GivenGetAdventureByPathDataRepository()
        {
            _madventureUserRepository.Setup(x => x.GetUserAdventures(new Guid("835e2700-27e7-49c2-a09e-072f35a7fd19"))).Returns(Task.FromResult(_givenAdventureUserResponseModel));
        }

        private async void WhenGetUserAdventureIsCalled()
        {
            _actualAdventureUserResponseModel = await _adventureUserService.GetUsersAdventure(new Guid("835e2700-27e7-49c2-a09e-072f35a7fd19"));
        }

        private void ThenSuccessResponseIsReturned()
        {
            _madventureUserRepository.VerifyAll();
            Assert.Equal(expected: _givenAdventureUserResponseModel[0].AdventureId, actual: _actualAdventureUserResponseModel[0].AdventureId);
            Assert.Equal(expected: _givenAdventureUserResponseModel[0].UserId, actual: _actualAdventureUserResponseModel[0].UserId);           
        }
    }
}
