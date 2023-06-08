﻿using AutoMapper;
using ShoeperStar.Models.ViewModels.Shoe;
using ShoeperStar.Models;

namespace ShoeperStar
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            //Entity to EntityVM
            CreateMap<Shoe, ShoeVM>()
                .ForMember(s => s.BrandName, opt => opt.MapFrom(x => x.Brand.Name))
                .ForMember(s => s.CategoryName, opt => opt.MapFrom(x => x.Catergory.Name))
                .ForMember(s => s.GenderName, opt => opt.MapFrom(x => x.Gender.Name))
                .ForMember(s => s.Variants, opt => opt.MapFrom(x => x.Variants));

        }
    }
}
