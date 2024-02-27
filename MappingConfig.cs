using static Azure.Core.HttpHeader;
using AutoMapper;
using movieTickets.Models;
using movieTickets.Models.DTO;
using movieTickets.Models.DTO.AuthDTO;

namespace movieTickets
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Movie, MovieDTO>().ReverseMap();
            CreateMap<Movie, MovieDTOupdate>().ReverseMap();
            CreateMap<Experience, ExperienceDTO>().ReverseMap();
            CreateMap<Experience, ExperienceDTOupdate>().ReverseMap();
            CreateMap<LocalUser, UserDTO>().ReverseMap();
            //CreateMap<ApplicationUser, UserDTO>().ReverseMap();
            /* CreateMap<Movie, CouponCreateDTO>().ReverseMap();
             CreateMap<Coupon, CouponUpdateDTO>().ReverseMap();
             CreateMap<Coupon, CouponDTO>().ReverseMap();
             CreateMap<LocalUser, UserDTO>().ReverseMap();
             CreateMap<ApplicationUser, UserDTO>().ReverseMap();*/
        }
    }
}
