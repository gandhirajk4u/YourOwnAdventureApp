using YourOwnAdventureApp.Models.Models;

namespace YourOwnAdventureApp.Service.Interfaces
{
    public interface IAdventureService
    {
        /// <summary>
        /// Create New Adventure Service
        /// </summary>
        /// <param name="adventureDtos"></param>
        /// <returns></returns>
        Task CreateNewAdventure(List<AdventureDto>? adventureDtos);

        /// <summary>
        /// Get Adventures
        /// </summary>
        /// <returns></returns>
        Task<List<AdventureResponseModel>> GetAdventures();

        /// <summary>
        /// Get Adventures By Path
        /// </summary>
        /// <returns></returns>
        Task<List<AdventureResponseModel>> GetAdventuresByPath(string path);

        /// <summary>
        /// Get User Adventure Selections
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>

        Task<List<MyAdventureResponseModel>> GetUserAdventureSelections(Guid userId);
    }
}
