using AutoMapper;
using Company.Identity.Api.Auth.Requests;
using Company.Identity.Api.Auth.Responses;
using Company.Identity.Application.Auth.Commands;
using Company.Identity.Application.Auth.DTOs;

namespace Company.Identity.Api.Auth.MappingProfiles;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<CreateUserRequest, CreateUserCommand>();
        CreateMap<CreateUserDto, CreateUserResponse>();
    }
}