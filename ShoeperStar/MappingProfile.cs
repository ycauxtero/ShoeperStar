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

            CreateMap<Size, SizeForCreationVM>()
                .ForMember(vm => vm.Color, opt => opt.MapFrom(s => s.Variant.Color))
                .ForMember(vm => vm.ColorHex, opt => opt.MapFrom(s => s.Variant.ColorHex))
                .ForMember(vm => vm.VariantId, opt => opt.MapFrom(s => s.VariantId))
                .ForMember(vm => vm.ShoeId, opt => opt.MapFrom(s => s.Variant.ShoeId))
                .ForMember(vm => vm.ShoeModel, opt => opt.MapFrom(s => s.Variant.Shoe.Name));


            CreateMap<Brand, BrandVM>();
            CreateMap<Category, CategoryVM>();
            CreateMap<Gender, GenderVM>();

            CreateMap<CartItem, CartItemVM>()
                .ForMember(c => c.Size, opt => opt.MapFrom(x => x.Size))
                .ForMember(c => c.ColorHex, opt => opt.MapFrom(x => x.Size.Variant.ColorHex))
                .ForMember(c => c.Model, opt => opt.MapFrom(x => x.Size.Variant.Shoe.Name))
                .ForMember(c => c.Price, opt => opt.MapFrom(x => x.Size.Variant.Shoe.Price));

            CreateMap<Order, OrderVM>();

            // Note: so as long as the appropriate mapping for the child object properties
            // are in place, we need not explicitly use a ForMember and ForPath (for nested child properties) during mapping
            CreateMap<OrderItem, OrderItemVM>();




            //EntityForCreationVM To Entity
            CreateMap<ShoeForCreationVM, Shoe>()
                .ForMember(s => s.Price, opt => opt.MapFrom(x => Convert.ToDouble(x.Price)));

            CreateMap<VariantForCreationVM, Variant>();

            CreateMap<SizeForCreationVM, Size>()
                .ForMember(s => s.Quantity, opt => opt.MapFrom(x => Convert.ToInt32(x.Quantity)))
                .ForMember(s => s.Value, opt => opt.MapFrom(x => x.Value))
                .ForMember(s => s.SKU, opt => opt.MapFrom(x => x.SKU));


            //EntityMother To EntityChildForCreationVM
            CreateMap<Variant, SizeForCreationVM>()
                .ForMember(s => s.Color, opt => opt.MapFrom(v => v.Color))
                .ForMember(s => s.ColorHex, opt => opt.MapFrom(v => v.ColorHex))
                .ForMember(s => s.ShoeModel, opt => opt.MapFrom(v => v.Shoe.Name))
                .ForMember(s => s.ShoeId, opt => opt.MapFrom(v => v.ShoeId))
                .ForMember(s => s.VariantId, opt => opt.MapFrom(v => v.Id))
                .ForAllOtherMembers(opt => opt.Ignore());

        }
    }
}
