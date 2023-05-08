using AutoMapper;
using UserManagement.Domain.Models;
using UserManagement.Presentation.WebAPI.Models;

namespace UserManagement.Presentation.WebAPI.Mapper;

public class UserMapperProfile : Profile
{
    public UserMapperProfile()
    {
        CreateMap<PaymentMethodDto, PaymentMethod>().ReverseMap();
        CreateMap<UserDto, User>().ReverseMap();
    }
}