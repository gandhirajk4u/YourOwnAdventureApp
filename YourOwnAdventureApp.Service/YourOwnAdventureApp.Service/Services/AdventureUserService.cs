namespace YourOwnAdventureApp.Service.Services
{
    public class AdventureUserService : IAdventureUserService
    {        
        private readonly IMapper _mapper;
        private readonly IAdventureUserRepository _userRepository;
        /// <summary>
        /// Adventure User Service
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="userRepository"></param>
        /// <param name="repository"></param>
        public AdventureUserService(IMapper mapper, IAdventureUserRepository userRepository)
        {          
            _mapper = mapper;
            _userRepository = userRepository;            
        }

        /// <inheritdoc/>
        public async Task<List<AdventureUserDto>> CreateNewUserAdventure(List<AdventureUserDto>? adventureUserDtos)
        {
            var adventureUsers = _mapper.Map<List<AdventureUserDbModel>>(adventureUserDtos);
            var response = await _userRepository.CreateNewUserAdventure(adventureUsers).ConfigureAwait(false);
            return _mapper.Map<List<AdventureUserDto>>(response);
        }

        /// <inheritdoc/>
        public async Task<List<AdventureUserDto>> UpdateUserAdventure(List<AdventureUserDto>? adventureDtos)
        {
            var adventures = _mapper.Map<List<AdventureUserDbModel>>(adventureDtos);
            var response = await _userRepository.UpdateUserAdventure(adventures).ConfigureAwait(false);
            return _mapper.Map<List<AdventureUserDto>>(response);
        }

        public async Task<List<UserAdventureResponseModel>> GetUsersAdventure(Guid userId)
        {            
            return await _userRepository.GetUserAdventures(userId).ConfigureAwait(false);
        }
    }
}
