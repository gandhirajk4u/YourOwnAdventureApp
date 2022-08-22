namespace YourOwnAdventureApp.UnitTests.RepositoryTests
{
    public class AdventureRespositoryTests
    {
        private static IAdventureRepository _adventureRepository;
        private static AdventureDbContext _dbContext;
        private static List<AdventureDbModel> _givenAdventureDbModel;
        private static List<AdventureDbModel> _actualAdventureDbModel;
        private static List<MyAdventureResponseModel> _actualMyAdventureResponseModel;

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
        private void CreateAdventureRespository()
        {
            GivenSystem();
            GivenAdventureEntity();
            WhenCreateAdventureCalled();
            ThenAdventureVerified();
            _dbContext.DisposeAsync();
        }

        [Fact]
        private void UpdateAdventureRespository()
        {
            GivenSystem();
            GivenAdventureEntity();
            WhenUpdateAdventureCalled();
            ThenAdventureVerified();
            _dbContext.DisposeAsync();
        }

        [Fact]
        private void GetAdventureRespository()
        {
            GivenSystem();
            GivenAdventureEntity();
            WhenGetAdventureCalled();
            ThenAdventureIsRetrived();
            _dbContext.DisposeAsync();
        }

        [Fact]
        private void GetAdventureRespositoryByPath()
        {
            GivenSystem();
            GivenAdventureEntity();
            WhenGetAdventureByPathCalled();
            ThenAdventureIsRetrived();
            _dbContext.DisposeAsync();
        }

        [Fact]
        private void GetAdventuresWithUserSelectionRepository()
        {
            GivenSystem();
            GivenAdventureEntity();
            WhenGetAdventureUserSelectionIsCalled();
            ThenAdventureUserSelectionIsRetrived();
            _dbContext.DisposeAsync();
        }

        private void GivenSystem()
        {
            _dbContext = CreateDbContext(typeof(AdventureRespositoryTests).ToString());
            _adventureRepository = new AdventureRepository(_dbContext);
        }

        private void GivenAdventureEntity()
        {
            _givenAdventureDbModel = new List<AdventureDbModel>() { new AdventureDbModel() { Name = "Skydiving",
                Path = ",Skydiving",
                CreatedBy = new Guid("8dbd24f7-ffcd-418b-95ef-ef0c6f23bd0d") } };
        }

        private async Task WhenCreateAdventureCalled()
        {
            _actualAdventureDbModel = await _adventureRepository.CreateNewAdventure(_givenAdventureDbModel).ConfigureAwait(false);
        }
        private async Task WhenUpdateAdventureCalled()
        {
            _actualAdventureDbModel = await _adventureRepository.UpdateAdventure(_givenAdventureDbModel).ConfigureAwait(false);
        }
        private async Task WhenGetAdventureCalled()
        {
            _dbContext.tblAdventures.Add(new AdventureDbModel()
            {
                Name = "Skydiving",
                Path = ",Skydiving",
                CreatedBy = new Guid("8dbd24f7-ffcd-418b-95ef-ef0c6f23bd0d")
            });
            _dbContext.SaveChanges();
            _actualAdventureDbModel = await _adventureRepository.GetAdventures().ConfigureAwait(false);
        }

        private async Task WhenGetAdventureByPathCalled()
        {
            _dbContext.tblAdventures.Add(new AdventureDbModel()
            {
                Name = "Skydiving",
                Path = ",Skydiving",
                CreatedBy = new Guid("8dbd24f7-ffcd-418b-95ef-ef0c6f23bd0d")
            });
            _dbContext.SaveChanges();
            _actualAdventureDbModel = await _adventureRepository.GetAdventuresByPath(",Skydiving").ConfigureAwait(false);
        }

        private async Task WhenGetAdventureUserSelectionIsCalled()
        {
            _dbContext.tblAdventures.Add(new AdventureDbModel()
            {
                Name = "Skydiving",
                Path = ",Skydiving",
                CreatedBy = new Guid("8dbd24f7-ffcd-418b-95ef-ef0c6f23bd0d")
            });
            _dbContext.tblAdventureUser.Add(new AdventureUserDbModel()
            {
                UserId = new Guid("55bab274-695a-461d-bd8b-94b76a89a8ed"),
                AdventureId = new Guid("cdacdfc1-4ac2-445a-9280-410cee86b419")
            });
            _dbContext.SaveChanges();
            _actualMyAdventureResponseModel = await _adventureRepository.GetAdventuresWithUserSelection(new Guid("55bab274-695a-461d-bd8b-94b76a89a8ed")).ConfigureAwait(false);
        }

        private void ThenAdventureVerified()
        {
            Assert.Equal(expected: _givenAdventureDbModel[0].Name, actual: _actualAdventureDbModel[0].Name);
        }

        private void ThenAdventureIsRetrived()
        {
            Assert.Equal(expected: _givenAdventureDbModel[0].Path, actual: _actualAdventureDbModel[0].Path);
        }

        private void ThenAdventureUserSelectionIsRetrived()
        {
            Assert.Equal(expected: _givenAdventureDbModel[0].Path, actual: _actualMyAdventureResponseModel[0].Path);
        }
    }
}
