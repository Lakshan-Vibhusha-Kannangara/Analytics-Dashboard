using API.DTOs;
using API.Models;
using AutoMapper;


public class MappingProfile : Profile
{
    public MappingProfile()
    {
 


        CreateMap<User, UserDTO>()
            .ForMember(dest => dest.userId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.Name))
               .ForMember(dest => dest.email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.avatar, opt => opt.MapFrom(src => src.Avatar))
                .ForMember(dest => dest.password, opt => opt.MapFrom(src => src.Password))
                 .ForMember(dest => dest.token, opt => opt.MapFrom(src => src.Token))
                  .ForMember(dest => dest.createdDate, opt => opt.MapFrom(src => src.CreatedDate));


        CreateMap<UserDTO, User>()

            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.name))
            .ForMember(dest => dest.Avatar, opt => opt.MapFrom(src => src.avatar))
              .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.email))
   .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.createdDate))
      .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.password));


    }

}
