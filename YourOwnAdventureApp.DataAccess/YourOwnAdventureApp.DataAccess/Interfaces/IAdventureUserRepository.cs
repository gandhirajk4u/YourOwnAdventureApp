using YourOwnAdventureApp.Models.Models;

namespace YourOwnAdventureApp.DataAccess.Interfaces
{
    public interface IAdventureUserRepository
    {
        /// <summary>
        /// Craete New User Adventure
        /// </summary>
        /// <param name="dbModel"></param>
        /// <returns></returns>
        Task<List<AdventureUserDbModel>> CreateNewUserAdventure(List<AdventureUserDbModel> dbModels);

        /// <summary>
        /// Update Adventure
        /// </summary>
        /// <param name="dbModels"></param>
        /// <returns></returns>

        Task<List<AdventureUserDbModel>> UpdateUserAdventure(List<AdventureUserDbModel> dbModels);

        /// <summary>
        /// Get User Adventures
        /// </summary>
        /// <returns></returns>

        Task<List<UserAdventureResponseModel>> GetUserAdventures(Guid userId);

    }
}
