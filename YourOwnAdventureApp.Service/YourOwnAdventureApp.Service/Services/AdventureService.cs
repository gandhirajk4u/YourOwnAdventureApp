using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourOwnAdventureApp.DataAccess.Interfaces;
using YourOwnAdventureApp.DataAccess.Repositories;
using YourOwnAdventureApp.Models.Models;
using YourOwnAdventureApp.Service.Interfaces;

namespace YourOwnAdventureApp.Service.Services
{
    public class AdventureService : IAdventureService
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IAdventureRepository _repository;
        /// <summary>
        /// Adventure Service
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repository"></param>
        public AdventureService(ILogger<AdventureService> logger, IMapper mapper, IAdventureRepository repository)
        {
            _logger = logger;
            _mapper = mapper;
            _repository = repository;
        }

        /// <inheritdoc/>
        public async Task CreateNewAdventure(List<AdventureDto>? adventureDtos)
        {
            var adventures = _mapper.Map<List<AdventureDbModel>>(adventureDtos);
            await _repository.CreateNewAdventure(adventures).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<List<AdventureResponseModel>> GetAdventures()
        {          
            var adventures =  _mapper.Map<List<AdventureResponseModel>>(await _repository.GetAdventures().ConfigureAwait(false));
            return adventures;
        }

        /// <inheritdoc/>
        public async Task<List<AdventureResponseModel>> GetAdventuresByPath(string path)
        {
            var adventures = _mapper.Map<List<AdventureResponseModel>>(await _repository.GetAdventuresByPath(path).ConfigureAwait(false));
            return adventures;
        }

        /// <inheritdoc/>
        public async Task<List<MyAdventureResponseModel>> GetUserAdventureSelections(Guid userId)
        {
            var adventures = await _repository.GetAdventuresWithUserSelection(userId).ConfigureAwait(false);
            return adventures;
        }
    }
}
