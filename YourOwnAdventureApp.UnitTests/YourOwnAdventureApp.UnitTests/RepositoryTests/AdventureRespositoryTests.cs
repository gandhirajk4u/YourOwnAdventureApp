using Microsoft.EntityFrameworkCore;
using YourOwnAdventureApp.DataAccess.DbContexts;
using System.Linq;
using YourOwnAdventureApp.DataAccess.Repositories;

namespace YourOwnAdventureApp.UnitTests.RepositoryTests
{
    public class AdventureRespositoryTests
    {
        private static IAdventureRepository _adventureRepository;
        private static AdventureDbContext _dbContext;
        private static List<AdventureDbModel> _givenAdventureDbModel;
        private static List<AdventureDbModel> _actualAdventureDbModel;
        private readonly IMapper _mapper;
        private static bool result = false;


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
            ThenAdventureCreated();
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

        private void ThenAdventureCreated()
        {
            Assert.Equal(expected: _givenAdventureDbModel[0].Name, actual: _actualAdventureDbModel[0].Name);
        }
        private void ThenAdventureIsRetrived()
        {
            Assert.Equal(expected: _givenAdventureDbModel[0].Path, actual: _actualAdventureDbModel[0].Path);
        }
    }
}
