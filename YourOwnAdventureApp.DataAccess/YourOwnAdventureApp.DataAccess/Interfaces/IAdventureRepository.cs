namespace YourOwnAdventureApp.DataAccess.Interfaces
{
    public interface IAdventureRepository
    {
        /// <summary>
        /// Craete Adventure
        /// </summary>
        /// <param name="dbModel"></param>
        /// <returns>Task</returns>
        Task<List<AdventureDbModel>> CreateNewAdventure(List<AdventureDbModel> dbModel);

        /// <summary>
        /// Update Adventure
        /// </summary>
        /// <param name="dbModels"></param>
        /// <returns></returns>
        Task<List<AdventureDbModel>> UpdateAdventure(List<AdventureDbModel> dbModels);

        /// <summary>
        /// Get Adventures
        /// </summary>
        /// <returns>List AdventureDbModel</returns>

        Task<List<AdventureDbModel>> GetAdventures();

        /// <summary>
        /// Get Adventure By Path
        /// </summary>
        /// <param name="path"></param>
        /// <returns>List AdventureDbModel</returns>
        Task<List<AdventureDbModel>> GetAdventuresByPath(string path);

        /// <summary>
        /// Get Adventures With User Selection
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>List MyAdventureResponseModel</returns>

        Task<List<MyAdventureResponseModel>> GetAdventuresWithUserSelection(Guid userId);
    }
}
