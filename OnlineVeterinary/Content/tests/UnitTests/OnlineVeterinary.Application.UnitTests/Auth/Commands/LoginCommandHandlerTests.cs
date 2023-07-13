using ErrorOr;
using MapsterMapper;
using Moq;
using OnlineVeterinary.Application.Common.Interfaces.Persistence;
using OnlineVeterinary.Application.Common.Interfaces.Services;
using OnlineVeterinary.Application.Features.Auth.Common;
using OnlineVeterinary.Application.Features.Auth.Queries.Login;
using OnlineVeterinary.Domain.Users.Entities;

namespace OnlineVeterinary.Application.UnitTests.Auth.Commands;

public class LoginCommandHandlerTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IJwtGenerator> _JwtGeneratorMock;


    public LoginCommandHandlerTests()
    {
        _userRepositoryMock = new();
        _mapperMock = new();
        _JwtGeneratorMock = new();
    }

    [Fact]
    public async Task Handle_Should_ReturnFailure_WhenEmailInvalidAsync()
    {
        var command = new LoginCommand("", "");
        var handler = new LoginCommandHandler(
            _userRepositoryMock.Object,
            _mapperMock.Object,
            _JwtGeneratorMock.Object
            );



        _userRepositoryMock.Setup(x => x.GetByEmailAsync(command.Email))
                            .ReturnsAsync((User?)null);
        //Act
        var result = await handler.Handle(command, default);
        //Assert
        Assert.True(result.IsError);
        Assert.Equal(Error.Failure(description: "email or password incorrect"), result.FirstError);
    }

    [Fact]
    public async Task Handle_Should_ReturnFailure_WhenPasswordInvalidAsync()
    {
        var command = new LoginCommand("", "");
        var handler = new LoginCommandHandler(
            _userRepositoryMock.Object,
            _mapperMock.Object,
            _JwtGeneratorMock.Object
            );


        var user = new User()
        {
            Email = command.Email,
            Password = "other"
        };
        _userRepositoryMock.Setup(x => x.GetByEmailAsync(command.Email))
                            .ReturnsAsync(user);
        //Act
        var result = await handler.Handle(command, default);
        //Assert
        Assert.True(result.IsError);
        Assert.Equal(Error.Failure(description: "email or password incorrect"), result.FirstError);
    }


    [Fact]
    public async Task Handle_Should_ReturnAuthResult_WhenInputIsValid()
    {
        //Arrange
        var command = new LoginCommand("", "");
        var handler = new LoginCommandHandler(
            _userRepositoryMock.Object,
            _mapperMock.Object,
            _JwtGeneratorMock.Object
            );



        var user = new User()
        {
            Email = command.Email,
            Password = command.Password
        };
        _userRepositoryMock.Setup(x => x.GetByEmailAsync(command.Email))
                             .ReturnsAsync(user);


        var token = "gtg";
        _JwtGeneratorMock.Setup(x => x.GenerateToken(user))
            .Returns(token);
        // _mapperMock.Setup(x => x.Map<User>(command))
        //     .Returns(user);
        var authResult = new AuthResult(Guid.Empty, user.FirstName, user.LastName, user.Email, user.Password, user.Role, token);

        _mapperMock.Setup(x => x.Map<AuthResult>(user))
            .Returns(authResult);


        //Act
        var result = await handler.Handle(command, default);
        //Assert
        Assert.False(result.IsError);
        Assert.Equal(authResult, result.Value);
        _userRepositoryMock.Verify(x => x.GetByEmailAsync(command.Email), Times.Once);



    }
}
