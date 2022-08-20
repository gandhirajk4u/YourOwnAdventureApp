using YourOwnAdventureApp.Models.Models;

namespace YourOwnAdventureApp.Service.Interfaces
{
    public interface IAdventureUserService
    {
        /// <summary>
        /// Create New User Adventure
        /// </summary>
        /// <param name="adventureUserDtos"></param>
        /// <returns></returns>
        Task CreateNewUserAdventure(List<AdventureUserDto>? adventureUserDtos);

        /// <summary>
        /// Get Users Adventure
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>

        Task<List<UserAdventureResponseModel>> GetUsersAdventure(Guid userId);
    }
}
