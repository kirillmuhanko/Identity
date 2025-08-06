using System.Linq.Expressions;
using Company.Identity.Application.Auth.Interfaces.Services;
using Company.Identity.Application.Event.Interfaces.Dispatchers;
using Company.Identity.Application.User.Commands;
using Company.Identity.Application.User.Events;
using Company.Identity.Application.User.Handlers;
using Company.Identity.Domain.User.Entities;
using Company.Identity.Domain.User.Interfaces.Repositories;
using Company.Identity.Domain.User.Interfaces.Specifications;
using Company.Identity.Domain.User.Interfaces.Validators;
using Company.Identity.Shared.ResultPattern.Results;
using Moq;
using Xunit;

namespace Company.Identity.Application.Tests.Unit.User.Handlers;

public class CreateUserHandlerTests
{
    private readonly Mock<IAuthService> _authServiceMock = new(MockBehavior.Strict);
    private readonly Mock<IEmailValidator> _emailValidatorMock = new(MockBehavior.Strict);
    private readonly Mock<IEventDispatcher> _eventDispatcherMock = new(MockBehavior.Strict);
    private readonly CreateUserHandler _handler;
    private readonly Mock<IPasswordValidator> _passwordValidatorMock = new(MockBehavior.Strict);
    private readonly Mock<IUserRepository> _userRepositoryMock = new(MockBehavior.Strict);
    private readonly Mock<IUserSpecification> _userSpecificationMock = new(MockBehavior.Strict);

    public CreateUserHandlerTests()
    {
        _handler = new CreateUserHandler(
            _authServiceMock.Object,
            _emailValidatorMock.Object,
            _passwordValidatorMock.Object,
            _userSpecificationMock.Object,
            _userRepositoryMock.Object,
            _eventDispatcherMock.Object
        );
    }

    [Fact]
    public async Task GivenValidUser_WhenCreatingUser_ThenUserIsCreatedSuccessfully()
    {
        // Given
        var command = new CreateUserCommand("johndoe", "john@example.com", "Str0ngP@ss!");

        Expression<Func<UserEntity, bool>> userSpec = u =>
            u.UserName == command.UserName && u.Email == command.Email;

        _passwordValidatorMock
            .Setup(p => p.IsPasswordStrong(command.Password))
            .Returns(true);

        _emailValidatorMock
            .Setup(e => e.IsFormatValid(command.Email))
            .Returns(true);

        _userSpecificationMock
            .Setup(s => s.HasUserNameAndEmail(command.UserName, command.Email))
            .Returns(userSpec);

        _userRepositoryMock
            .Setup(r => r.AnyAsync(userSpec))
            .ReturnsAsync(OperationResult<bool>.Ok(false));

        _authServiceMock
            .Setup(a => a.HashPassword(command.Password))
            .Returns("hashed_pass");

        var createdUser = new UserEntity(command.UserName, command.Email, "hashed_pass");

        _userRepositoryMock
            .Setup(r => r.AddAsync(It.IsAny<UserEntity>()))
            .ReturnsAsync(OperationResult<UserEntity>.Ok(createdUser));

        _authServiceMock
            .Setup(a => a.GenerateJwtToken(It.IsAny<UserEntity>()))
            .Returns("jwt_token");

        _eventDispatcherMock
            .Setup(d => d.DispatchAsync(It.IsAny<UserCreatedEvent>()))
            .Returns(Task.CompletedTask);

        // When
        var result = await _handler.HandleAsync(command);

        // Then
        Assert.True(result.IsSuccess);
        Assert.Equal("johndoe", result.Value.UserName);
        Assert.Equal("john@example.com", result.Value.Email);
        Assert.Equal("jwt_token", result.Value.Token);

        _eventDispatcherMock.Verify(d => d.DispatchAsync(It.IsAny<UserCreatedEvent>()), Times.Once);
    }
}