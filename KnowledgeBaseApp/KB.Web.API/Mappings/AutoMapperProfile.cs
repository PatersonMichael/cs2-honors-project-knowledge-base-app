using AutoMapper;
using KB.Domain.Models;
using KB.Web.API.DtoModels;

namespace KB.Web.API.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // domain object to Dto
            CreateMap<UserProfile, UserProfileDto>();

            // Dto to domain object
            CreateMap<UserProfileDto, UserProfile>();
        }
    }
}
