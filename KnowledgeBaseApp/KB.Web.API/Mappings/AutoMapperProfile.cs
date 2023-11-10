﻿using AutoMapper;
using KB.Domain.Models;
using KB.Web.API.DtoModels;
using Citation = KB.Domain.Models.Citation;
using CitationDto = KB.Web.API.DtoModels.Citation;
//using Author = KB.Domain.Models.Author;
//using AuthorDto = KB.Web.API.DtoModels.Author;
using SourceMaterial = KB.Domain.Models.SourceMaterial;
using SourceMaterialDto = KB.Web.API.DtoModels.SourceMaterial;

namespace KB.Web.API.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // domain object to Dto
            CreateMap<UserProfile, UserProfileDto>();
            //CreateMap<Author, AuthorDto>();
            CreateMap<SourceMaterial, SourceMaterialDto>();
            // Dto to domain object
            CreateMap<UserProfileDto, UserProfile>();
            //CreateMap<AuthorDto, Author>()
            //    .ForMember(dest => dest.UserProfile, opt => opt.Ignore())
            //    .ForMember(x => x.SourceMaterials, opt => opt.Ignore());
            CreateMap<SourceMaterialDto, SourceMaterial>();
            //.ForMember(x => x.Authors, opt => opt.Ignore());


            CreateMap<Citation, CitationDto>();
            CreateMap<CitationDto, Citation>();
        }
    }
}
