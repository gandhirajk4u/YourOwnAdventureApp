namespace YourOwnAdventureApp.Service.Services
{
    public class AdventureService : IAdventureService
    {
        private readonly IMapper _mapper;
        private readonly IAdventureRepository _repository;
        /// <summary>
        /// Adventure Service
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repository"></param>
        public AdventureService(IMapper mapper, IAdventureRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        /// <inheritdoc/>
        public async Task<List<AdventureDto>> CreateNewAdventure(List<AdventureDto>? adventureDtos)
        {
            var adventures = _mapper.Map<List<AdventureDbModel>>(adventureDtos);
            var response = await _repository.CreateNewAdventure(adventures).ConfigureAwait(false);
            return _mapper.Map<List<AdventureDto>>(response);
        }

        /// <inheritdoc/>
        public async Task<List<AdventureDto>> UpdateAdventure(List<AdventureDto>? adventureDtos)
        {
            var adventures = _mapper.Map<List<AdventureDbModel>>(adventureDtos);
            var response = await _repository.UpdateAdventure(adventures).ConfigureAwait(false);
            return _mapper.Map<List<AdventureDto>>(response);
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
