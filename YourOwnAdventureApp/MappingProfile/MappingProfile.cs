namespace YourOwnAdventureApp.MappingProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AdventureDbModel, AdventureResponseModel>().ReverseMap();
            CreateMap<AdventureDto, AdventureDbModel>().ReverseMap();
            CreateMap<AdventureDbModel, AdventureDto>().ReverseMap();
            CreateMap<AdventureUserDbModel, AdventureUserDto>().ReverseMap();
            CreateMap<AdventureUserDto, AdventureUserDbModel>().ReverseMap();
        }
    }
}
