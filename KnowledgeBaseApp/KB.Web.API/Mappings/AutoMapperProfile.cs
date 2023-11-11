using AutoMapper;
using KB.Domain.Models;
using KB.Web.API.DtoModels;
using Citation = KB.Domain.Models.Citation;
using CitationDto = KB.Web.API.DtoModels.Citation;
using Keyword = KB.Domain.Models.Keyword;
//using Author = KB.Domain.Models.Author;
//using AuthorDto = KB.Web.API.DtoModels.Author;
using SourceMaterial = KB.Domain.Models.SourceMaterial;
using SourceMaterialDto = KB.Web.API.DtoModels.SourceMaterial;
using KeywordDto = KB.Web.API.DtoModels.Keyword;

namespace KB.Web.API.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserProfile, UserProfileDto>();
            CreateMap<UserProfileDto, UserProfile>();

            CreateMap<SourceMaterial, SourceMaterialDto>();
            CreateMap<SourceMaterialDto, SourceMaterial>();

            //CreateMap<Author, AuthorDto>();
            //CreateMap<AuthorDto, Author>()
            //    .ForMember(dest => dest.UserProfile, opt => opt.Ignore())
            //    .ForMember(x => x.SourceMaterials, opt => opt.Ignore());
            //.ForMember(x => x.Authors, opt => opt.Ignore());


            CreateMap<Citation, CitationDto>();
            CreateMap<CitationDto, Citation>();

            CreateMap<Keyword, KeywordDto>();
            CreateMap<KeywordDto, Keyword>();
        }
    }
}
