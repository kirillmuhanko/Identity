using AutoMapper;
using Company.Identity.Api.User.Requests;
using Company.Identity.Api.User.Responses;
using Company.Identity.Application.User.Commands;
using Company.Identity.Application.User.DTOs;

namespace Company.Identity.Api.User.MappingProfiles;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<CreateUserRequest, CreateUserCommand>();
        CreateMap<CreateUserDto, CreateUserResponse>();
    }
}