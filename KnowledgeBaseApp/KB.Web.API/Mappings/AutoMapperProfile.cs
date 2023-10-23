using AutoMapper;
using KB.Domain.Models;
using KB.Web.API.DtoModels;
using Author = KB.Domain.Models.Author;
using AuthorDto = KB.Web.API.DtoModels.Author;

namespace KB.Web.API.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // domain object to Dto
            CreateMap<UserProfile, UserProfileDto>();
            CreateMap<Author, AuthorDto>()
                .ForMember(dest => dest.UserProfileId, opt => opt.MapFrom(src => src.UserProfileId));

            // Dto to domain object
            CreateMap<UserProfileDto, UserProfile>();
            CreateMap<AuthorDto, Author>()
                .ForMember(dest => dest.UserProfile, opt => opt.Ignore());
        }
    }
}
