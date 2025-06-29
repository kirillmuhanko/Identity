using AutoMapper;
using Company.Identity.Application.User.DTOs;
using Company.Identity.Domain.User.Entities;

namespace Company.Identity.Application.User.MappingProfiles;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<UserEntity, CreateUserDto>();
    }
}