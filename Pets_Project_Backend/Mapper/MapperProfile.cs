using AutoMapper;
using Pets_Project_Backend.Data.Models.UserModels;
using Pets_Project_Backend.Data.Models.UserModels.UserDtos;

namespace Pets_Project_Backend.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile() 
        {
            //CreateMap<User, UserRegistration_Dto>().ReverseMap();
          
        }
    }
}
