using AutoMapper;
using Microsoft.Extensions.Logging;
using YourOwnAdventureApp.DataAccess.Interfaces;
using YourOwnAdventureApp.Models.Models;
using YourOwnAdventureApp.Service.Interfaces;

namespace YourOwnAdventureApp.Service.Services
{
    public class AdventureUserService : IAdventureUserService
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IAdventureUserRepository _userRepository;
        /// <summary>
        /// Adventure User Service
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="userRepository"></param>
        /// <param name="repository"></param>
        public AdventureUserService(ILogger<AdventureUserService> logger, IMapper mapper, IAdventureUserRepository userRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _userRepository = userRepository;            
        }

        /// <inheritdoc/>
        public async Task CreateNewUserAdventure(List<AdventureUserDto>? adventureUserDtos)
        {
            var adventureUsers = _mapper.Map<List<AdventureUserDbModel>>(adventureUserDtos);
            await _userRepository.CreateNewUserAdventure(adventureUsers).ConfigureAwait(false);
        }

        public async Task<List<UserAdventureResponseModel>> GetUsersAdventure(Guid userId)
        {            
            return await _userRepository.GetUserAdventures(userId).ConfigureAwait(false);
        }
    }
}
