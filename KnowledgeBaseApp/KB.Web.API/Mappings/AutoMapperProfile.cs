using AutoMapper;
using KB.Domain.Models;
using KB.Web.API.DtoModels;
using Citation = KB.Domain.Models.Citation;
using CitationDto = KB.Web.API.DtoModels.Citation;
using ExcerptCard = KB.Domain.Models.ExcerptCard;
using Keyword = KB.Domain.Models.Keyword;
//using Author = KB.Domain.Models.Author;
//using AuthorDto = KB.Web.API.DtoModels.Author;
using SourceMaterial = KB.Domain.Models.SourceMaterial;
using SourceMaterialDto = KB.Web.API.DtoModels.SourceMaterial;
using KeywordDto = KB.Web.API.DtoModels.Keyword;
using ExcerptCardDto = KB.Web.API.DtoModels.ExcerptCard;
using Note = KB.Domain.Models.Note;
using NoteDto = KB.Web.API.DtoModels.Note;
using UserProfileDtoPass = KB.Web.API.DtoModels.UserProfile;
using UserProfileEntity = KB.Domain.Models.UserProfile;

namespace KB.Web.API.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserProfileEntity, UserProfileDtoPass>();
            CreateMap<UserProfileDtoPass, UserProfileEntity>();

            CreateMap<UserProfileEntity, UserProfileDto>();
            CreateMap<UserProfileDto, UserProfileEntity>();

            CreateMap<SourceMaterial, SourceMaterialDto>();
            CreateMap<SourceMaterialDto, SourceMaterial>();

            CreateMap<Citation, CitationDto>();
            CreateMap<CitationDto, Citation>();

            CreateMap<Keyword, KeywordDto>();
            CreateMap<KeywordDto, Keyword>();

            CreateMap<ExcerptCard, ExcerptCardDto>();
            CreateMap<ExcerptCardDto, ExcerptCard>();

            CreateMap<Note, NoteDto>();
            CreateMap<NoteDto, Note>();

            // mapping for userCredentials

            CreateMap<UserProfileEntity, UserLoginCredentials>();
            CreateMap<UserLoginCredentials, UserProfileEntity>();
        }
    }
}
