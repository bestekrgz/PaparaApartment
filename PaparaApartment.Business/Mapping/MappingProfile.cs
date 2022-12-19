using AutoMapper;
using PaparaApartment.Core.Entities.Concrete;
using PaparaApartment.Entities.Dtos.Block;
using PaparaApartment.Entities.Dtos.Claim;
using PaparaApartment.Entities.Dtos.Expense;
using PaparaApartment.Entities.Dtos.ExpenseType;
using PaparaApartment.Entities.Dtos.Message;
using PaparaApartment.Entities.Dtos.Payment;
using PaparaApartment.Entity.Dtos.User;
using PaparaApartment.Entity.Dtos.UserClaim;
using PaparaApartment.Entity.Dtos.UserDetail;
using PaparaApartment.Entities.Dtos.UserMessage;
using PaparaApartment.Entity.Concrete;
using PaparaApartment.Entity.Dtos.Apartment;
using PaparaApartment.Entity.Dtos.ApartmentExpense;
using PaparaApartment.Entity.Dtos.Block;
using PaparaApartment.Entity.Dtos.Car;



namespace PaparaApartment.Business.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ExpenseType, ExpenseTypeViewDto>();
            CreateMap<ExpenseTypeAddDto, ExpenseType>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.Trim()));

            CreateMap<ExpenseTypeDeleteDto, ExpenseType>();
            CreateMap<ExpenseTypeUpdateDto, ExpenseType>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.Trim()));

            CreateMap<UserAddWithDetailsDto, User>();
            CreateMap<UserAddWithDetailsDto, UserDetailAddDto>()
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.IdentityNo, opt => opt.MapFrom(src => src.IdentityNo));

            CreateMap<UserAddWithDetailsDto, Apartment>();

            CreateMap<UserClaimsViewDto, Claim>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ClaimName));
            CreateMap<User, UserUpdateDto>();
            CreateMap<User, UserViewDto>();
            CreateMap<UserViewDto, User>();
            CreateMap<UserUpdateDto, User>();

            CreateMap<UserDetailAddDto, UserDetail>();
            CreateMap<UserDetailUpdateDto, UserDetail>();
            CreateMap<UserMessageSendToOneDto, MessageAddDto>();
            CreateMap<UserMessageSendToAllDto, MessageAddDto>();
            CreateMap<UserMessageSendToOneDto, UserMessage>()
                .ForMember(dest => dest.ToUserId, opt => opt.MapFrom(src => src.RecipientId));

            CreateMap<UserMessageAddDto, UserMessage>();
            CreateMap<UserMessageSendToAllDto, UserMessage>();
            CreateMap<MessageAddDto, Message>();
            CreateMap<MessageAddForOneDto, MessageAddDto>();

            CreateMap<Block, BlockViewDto>();
            CreateMap<BlockAddDto, Block>();
            CreateMap<ApartmentAddDto, Apartment>();
            CreateMap<CarAddDto, Car>();
            CreateMap<UserAddWithDetailsDto, ApartmentUserUpdateDto>();
            CreateMap<UserClaimUpdateDto, UserClaim>();
            CreateMap<ClaimAddDto, Claim>();
            CreateMap<ExpenseAddForAllDto, Expense>();
            CreateMap<ExpenseUpdateDto, Expense>();
            CreateMap<ExpenseAddForOneDto, ExpenseAddForAllDto>();
            CreateMap<ApartmentExpenseAddDto, ApartmentExpense>();
            CreateMap<Payment, PaymentViewDto>();
            CreateMap<Message, MessageViewDto>();






        }
    }
}

