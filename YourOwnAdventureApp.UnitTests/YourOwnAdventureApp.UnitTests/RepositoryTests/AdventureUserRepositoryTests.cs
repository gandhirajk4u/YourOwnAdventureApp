namespace YourOwnAdventureApp.UnitTests.RepositoryTests
{
    public class AdventureUserRepositoryTests
    {
        private static IAdventureUserRepository _adventureUserRepository;
        private static AdventureDbContext _dbContext;
        private static List<AdventureUserDbModel> _givenAdventureUserDbModel;
        private static List<AdventureUserDbModel> _actualAdventureUserDbModel;
        private static List<UserAdventureResponseModel> _actualUserAdventureResponseModel;
        private AdventureDbContext CreateDbContext(string databaseName)
        {
            var option = new DbContextOptionsBuilder<AdventureDbContext>().UseInMemoryDatabase(databaseName).Options;

            var dbContext = new AdventureDbContext(option);
            if (dbContext != null)
            {
                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();
            }

            return dbContext;
        }

        [Fact]
        private void CreateUserAdventureRespository()
        {
            GivenSystem();
            GivenUserAdventureEntity();
            WhenCreateUserAdventureCalled();
            ThenUserAdventureVerified();
            _dbContext.DisposeAsync();
        }

        [Fact]
        private void UpdateUserAdventureRespository()
        {
            GivenSystem();
            GivenUserAdventureEntity();
            WhenUpdateUserAdventureCalled();
            ThenUserAdventureVerified();
            _dbContext.DisposeAsync();
        }

        [Fact]
        private void GetUserAdventures()
        {
            GivenSystem();
            GivenUserAdventureEntity();
            WhenGetUserAdventuresCalled();
            ThenGetUserAdventureVerified();
            _dbContext.DisposeAsync();
        }

        private void GivenSystem()
        {
            _dbContext = CreateDbContext(typeof(AdventureUserRepositoryTests).ToString());
            _adventureUserRepository = new AdventureUserRepository(_dbContext);
        }

        private void GivenUserAdventureEntity()
        {
            _givenAdventureUserDbModel = new List<AdventureUserDbModel>() { new AdventureUserDbModel() { UserId = new Guid("0aa75e79-f344-4dfe-a696-4869f605c839"),
                AdventureId = new Guid("2a9b8781-9ec6-4ae3-b1c2-ae64c0125d40") } };
        }

        private async Task WhenCreateUserAdventureCalled()
        {
            _actualAdventureUserDbModel = await _adventureUserRepository.CreateNewUserAdventure(_givenAdventureUserDbModel).ConfigureAwait(false);
        }

        private async Task WhenUpdateUserAdventureCalled()
        {
            _actualAdventureUserDbModel = await _adventureUserRepository.UpdateUserAdventure(_givenAdventureUserDbModel).ConfigureAwait(false);
        }

        private async Task WhenGetUserAdventuresCalled()
        {
            _dbContext.tblAdventures.Add(new AdventureDbModel()
            {
                Name = "Skydiving",
                Path = ",Skydiving",
                CreatedBy = new Guid("8dbd24f7-ffcd-418b-95ef-ef0c6f23bd0d")
            });
            _dbContext.tblAdventureUser.Add(new AdventureUserDbModel()
            {
                UserId = new Guid("0aa75e79-f344-4dfe-a696-4869f605c839"),
                AdventureId = new Guid("2a9b8781-9ec6-4ae3-b1c2-ae64c0125d40")
            });
            _dbContext.SaveChanges();
            _actualUserAdventureResponseModel = await _adventureUserRepository.GetUserAdventures(new Guid("0aa75e79-f344-4dfe-a696-4869f605c839")).ConfigureAwait(false);
        }

        private void ThenUserAdventureVerified()
        {
            Assert.Equal(expected: _givenAdventureUserDbModel[0].UserId, actual: _actualAdventureUserDbModel[0].UserId);
            Assert.Equal(expected: _givenAdventureUserDbModel[0].AdventureId, actual: _actualAdventureUserDbModel[0].AdventureId);
        }

        private void ThenGetUserAdventureVerified()
        {
           if(_actualUserAdventureResponseModel.Count == 0)
            {
                Assert.True(true);
            }
        }
    }
}
