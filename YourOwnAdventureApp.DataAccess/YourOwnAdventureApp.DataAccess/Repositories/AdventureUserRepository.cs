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
            string userId = string.Empty;
            foreach (var adventure in dbModels)
            {
                adventure.CreatedDate = DateTime.Now;
                userId = adventure.UserId.ToString();
                await _dbContext.tblAdventureUser.AddAsync(adventure).ConfigureAwait(false);
            }
            var objtoDelete = await (from deleteObj in _dbContext.tblAdventureUser where deleteObj.UserId == new Guid(userId) select deleteObj).AsNoTracking().ToListAsync().ConfigureAwait(false);
            _dbContext.tblAdventureUser.RemoveRange(objtoDelete);
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
