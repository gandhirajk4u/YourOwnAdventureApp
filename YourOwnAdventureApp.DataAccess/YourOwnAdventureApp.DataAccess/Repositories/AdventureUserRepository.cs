using Microsoft.EntityFrameworkCore;
using YourOwnAdventureApp.DataAccess.DbContexts;
using YourOwnAdventureApp.DataAccess.Interfaces;
using YourOwnAdventureApp.Models.Models;

namespace YourOwnAdventureApp.DataAccess.Repositories
{
    public  class AdventureUserRepository : IAdventureUserRepository
    {
        private readonly AdventureDbContext _dbContext;
        public AdventureUserRepository(AdventureDbContext context)
        {
            _dbContext = context;
        }

        /// <inheritdoc/>
        public async Task<List<AdventureUserDbModel>> CreateNewUserAdventure(List<AdventureUserDbModel> dbModels)
        {
            foreach (var adventure in dbModels)
            {
                adventure.AdventureUserId = Guid.NewGuid();
                adventure.CreatedDate = DateTime.Now;
                await _dbContext.tblAdventureUser.AddAsync(adventure).ConfigureAwait(false);
            }
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
            return dbModels;
        }

        /// <inheritdoc/>
        public async Task<List<AdventureUserDbModel>> UpdateUserAdventure(List<AdventureUserDbModel> dbModels)
        {
            foreach (var adventure in dbModels)
            {
                adventure.UpdatedDate = DateTime.Now;
                _dbContext.tblAdventureUser.Update(adventure);
            }
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
            return dbModels;
        }

        /// <inheritdoc/>
        public async Task<List<UserAdventureResponseModel>> GetUserAdventures(Guid userId)
        {
            var usersAdventure = await (from useradventure in _dbContext.tblAdventureUser
                                        join adventure in _dbContext.tblAdventures on useradventure.AdventureId equals adventure.AdventureId
                                        where useradventure.UserId == userId orderby adventure.Path ascending
                                        select new UserAdventureResponseModel { UserId = useradventure.UserId, AdventureId = adventure.AdventureId, Name = adventure.Name, Path = adventure.Path}).ToListAsync().ConfigureAwait(false);
            return usersAdventure;
        }

     }
}
