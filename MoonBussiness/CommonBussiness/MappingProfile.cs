using AutoMapper;
using MoonModels;
using MoonModels.DTO.RequestDTO;
using MoonModels.DTO.ResponseDTO;


namespace MoonBussiness.CommonBussiness
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateAccountRequest, Account>()
                 .ForMember(dest => dest.Id, opt => opt.Ignore())
                 .ForMember(dest => dest.Status, opt => opt.MapFrom(src => true))
                 .ForMember(dest => dest.Type, opt => opt.MapFrom(src => AccountType.User));

            CreateMap<Account, AccountResponse>();

            CreateMap<LoginRequest, Account>();
            CreateMap<Account, LoginResponse>();

            CreateMap<TableRequest, Table>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => true));

            CreateMap<Booking, BookingResponse>();

            CreateMap<CreateFoodRequest, Food>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => true));

            CreateMap<ComboRequest, Combo>()
             .ForMember(dest => dest.Id, opt => opt.Ignore())
             .ForMember(dest => dest.Status, opt => opt.MapFrom(src => true));

            CreateMap<OderRequest, Order>()
             .ForMember(dest => dest.Id, opt =>opt.Ignore())
             .ForMember(dest => dest.Status, opt => opt.MapFrom(src => true));

            CreateMap<Order, OderResponse>();

            CreateMap<OderItemRequest,OrderItem>()
             .ForMember(dest => dest.Id, opt => opt.Ignore())
             .ForMember(dest => dest.Status, opt => opt.MapFrom(src => true));

            CreateMap<OrderItem, OrderItemResponse>();
        }
    }
}
