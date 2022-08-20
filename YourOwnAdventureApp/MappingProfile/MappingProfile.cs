using AutoMapper;
using YourOwnAdventureApp.Models.Models;

namespace YourOwnAdventureApp.MappingProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AdventureDbModel, AdventureResponseModel>().ReverseMap();
            CreateMap<AdventureDto, AdventureDbModel>().ReverseMap();
            CreateMap<AdventureUserDbModel, AdventureUserDto>().ReverseMap();
        }
    }
}
