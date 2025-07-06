using AutoMapper;
using Company.Identity.Application.Auth.DTOs;
using Company.Identity.Domain.User.Entities;

namespace Company.Identity.Application.Auth.MappingProfiles;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<UserEntity, CreateUserDto>();
    }
}