

using AutoMapper;
using DatingApp.API.Data.Entity;
using DatingApp.API.DTO;
using DatingApp.API.Extensions;

namespace DatingApp.API.Profiles
{
    public class UserMapperProfile: Profile
    {
        public UserMapperProfile(){
            CreateMap<User,MemberDTO>()
            .ForMember(dest =>dest.Age,
                        option => option.MapFrom(src=>src.DateOfBirth.GetAge()));
            CreateMap<UserRegisterDTO,User>();
        }
        
    }
}