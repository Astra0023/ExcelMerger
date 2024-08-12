using AutoMapper;
using backend.Models;

namespace backend.AutoMapperProfiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserResponse>()
                .ForMember(x => x.User_Id, opt => opt.MapFrom(x => x.User_Id))
                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Name))
                .ForMember(x => x.Email, opt => opt.MapFrom(x => x.Email))
                .ForMember(x => x.Status, opt => opt.MapFrom(x => x.Status));
                
        }
    }
}
