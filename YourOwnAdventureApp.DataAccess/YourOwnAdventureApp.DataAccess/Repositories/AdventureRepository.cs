using Microsoft.EntityFrameworkCore;
using YourOwnAdventureApp.DataAccess.DbContexts;
using YourOwnAdventureApp.DataAccess.Interfaces;
using YourOwnAdventureApp.Models.Models;

namespace YourOwnAdventureApp.DataAccess.Repositories
{
    public class AdventureRepository : IAdventureRepository
    {
        private readonly AdventureDbContext _dbContext;
        public AdventureRepository(AdventureDbContext context)
        {
            _dbContext = context;           
        }

        /// <inheritdoc/>
        public async Task<List<AdventureDbModel>> CreateNewAdventure(List<AdventureDbModel> dbModels)
        {
            foreach (var adventure in dbModels)
            {
                adventure.AdventureId = Guid.NewGuid();
                adventure.CreatedDate = DateTime.Now;
                await _dbContext.tblAdventures.AddAsync(adventure).ConfigureAwait(false);
            }
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
            return dbModels;
        }

        /// <inheritdoc/>
        public async Task<List<AdventureDbModel>> UpdateAdventure(List<AdventureDbModel> dbModels)
        {
            foreach (var adventure in dbModels)
            {                
                adventure.UpdatedDate = DateTime.Now;
                _dbContext.tblAdventures.Update(adventure);
            }
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
            return dbModels;
        }

        /// <inheritdoc/>
        public async Task<List<AdventureDbModel>> GetAdventures()
        {
            var adventures = await (from adventure in _dbContext.tblAdventures orderby adventure.Path ascending select adventure).AsNoTracking().ToListAsync().ConfigureAwait(false);
            return adventures;
        }

        /// <inheritdoc/>
        public async Task<List<AdventureDbModel>> GetAdventuresByPath(string path)
        {
            var adventures = await (from adventure in _dbContext.tblAdventures.Where(x => x.Path.Contains(path)) orderby adventure.Path ascending select adventure).AsNoTracking().ToListAsync().ConfigureAwait(false);
            return adventures;
        }

        /// <inheritdoc/>
        public async Task<List<MyAdventureResponseModel>> GetAdventuresWithUserSelection(Guid userId)
        {    

            var adventures = await
                     (from adventure in _dbContext.tblAdventures
                     join useradventure in _dbContext.tblAdventureUser on adventure.AdventureId equals useradventure.AdventureId into groupjoin                          
                     from subadventure in groupjoin.DefaultIfEmpty()
                     where subadventure == null ? true : subadventure.UserId == userId
                     orderby adventure.Path
                     select new MyAdventureResponseModel
                          { 
                              AdventureId  = adventure.AdventureId,
                              Name = adventure.Name,
                              Path = adventure.Path,
                              IsUserSelected = subadventure == null ? false : true
                          }).ToListAsync().ConfigureAwait(false);
            return adventures;
        }
    }
}
