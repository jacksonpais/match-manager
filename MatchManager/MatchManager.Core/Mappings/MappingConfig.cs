using AutoMapper;
using MatchManager.Domain.Entities.Account;
using MatchManager.DTO.Account;

namespace MatchManager.Core.Mappings
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<RegisterRequestDTO, AppUserMaster>().ReverseMap();
            CreateMap<UserDTO, AppUserMaster>().ReverseMap();
        }
    }
}
