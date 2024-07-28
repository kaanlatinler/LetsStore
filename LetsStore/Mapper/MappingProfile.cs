using AutoMapper;
using LetsStore.DTO;
using LetsStore.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace LetsStore.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserDTO, User>().ReverseMap();
        }
    }
}
