namespace YourOwnAdventureApp.Service.Interfaces
{
    public interface IAdventureUserService
    {
        /// <summary>
        /// Create New User Adventure
        /// </summary>
        /// <param name="adventureUserDtos"></param>
        /// <returns></returns>
        Task<List<AdventureUserDto>> CreateNewUserAdventure(List<AdventureUserDto>? adventureUserDtos);

        /// <summary>
        /// Update User Adventure
        /// </summary>
        /// <param name="adventureDtos"></param>
        /// <returns></returns>
        Task<List<AdventureUserDto>> UpdateUserAdventure(List<AdventureUserDto>? adventureDtos);

        /// <summary>
        /// Get Users Adventure
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>

        Task<List<UserAdventureResponseModel>> GetUsersAdventure(Guid userId);
    }
}
