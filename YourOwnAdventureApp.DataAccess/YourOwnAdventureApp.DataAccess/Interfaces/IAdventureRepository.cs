﻿using System;
using YourOwnAdventureApp.Models.Models;

namespace YourOwnAdventureApp.DataAccess.Interfaces
{
    public interface IAdventureRepository
    {
        /// <summary>
        /// Craete Adventure
        /// </summary>
        /// <param name="dbModel"></param>
        /// <returns>Task</returns>
        Task CreateNewAdventure(List<AdventureDbModel> dbModel);

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