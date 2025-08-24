using TaskManagement.Application.DTOs;
using AutoMapper;
using TaskManagement.Domain.Entities;

namespace Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Team, TeamDto>().ReverseMap();
            CreateMap<TaskEntity, TaskDto>().ReverseMap();
        }
    }
}