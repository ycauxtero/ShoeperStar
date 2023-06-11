using AutoMapper;
using ShoeperStar.Models.ViewModels;
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

            CreateMap<Shoe, ShoeDetailsVM>()
                .ForMember(s => s.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(s => s.ImageURL, opt => opt.MapFrom(x => x.ImageURL))
                .ForMember(s => s.Price, opt => opt.MapFrom(x => x.Price))
                .ForMember(s => s.Name, opt => opt.MapFrom(x => x.Name))
                .ForMember(s => s.Variants, opt => opt.MapFrom(x => x.Variants))
                .ForMember(s => s.Brand, opt => opt.MapFrom(x => x.Brand))
                .ForMember(s => s.Gender, opt => opt.MapFrom(x => x.Gender))
                .ForMember(s => s.Catergory, opt => opt.MapFrom(x => x.Catergory))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<Shoe, ShoeForCreationVM>();


            CreateMap<Variant, VariantVM>();
            CreateMap<Variant, VariantForCreationVM>()
                .ForMember(vm => vm.ShoeModel, opt => opt.MapFrom(v => v.Shoe.Name));

            CreateMap<Variant, ShoeVariantVM>()
                .ForMember(vm => vm.Shoe, opt => opt.MapFrom(v => v.Shoe));

            CreateMap<Size, SizeVM>();
            CreateMap<Size, ShoeSizeVM>()
                .ForMember(c => c.Variant, opt => opt.MapFrom(x => x.Variant));

            CreateMap<Brand, BrandVM>();
            CreateMap<Category, CategoryVM>();
            CreateMap<Gender, GenderVM>();



            //EntityForCreationVM To Entity
            CreateMap<ShoeForCreationVM, Shoe>()
                .ForMember(s => s.Price, opt => opt.MapFrom(x => Convert.ToDouble(x.Price)));

        }
    }
}
