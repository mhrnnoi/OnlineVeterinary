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
    private readonly LoginCommand _command;
    private readonly LoginCommandHandler _handler;

    public LoginCommandHandlerTests()
    {
        _userRepositoryMock = new();
        _mapperMock = new();
        _JwtGeneratorMock = new();

        _command = new LoginCommand("", "");
        _handler = new LoginCommandHandler(
           _userRepositoryMock.Object,
           _mapperMock.Object,
           _JwtGeneratorMock.Object
           );
    }

    [Fact]
    public async Task Handle_Should_ReturnFailure_WhenEmailInvalidAsync()
    {

        _userRepositoryMock.Setup(x => x.GetByEmailAsync(_command.Email))
                            .ReturnsAsync((User?)null);
        //Act
        var result = await _handler.Handle(_command, default);
        //Assert
        Assert.True(result.IsError);
        Assert.Equal(Error.Failure(description: "email or password incorrect"),
                                     result.FirstError);
    }

    [Fact]
    public async Task Handle_Should_ReturnFailure_WhenPasswordInvalidAsync()
    {

        var user = new User()
        {
            Email = _command.Email,
            Password = "other"
        };
        _userRepositoryMock.Setup(x => x.GetByEmailAsync(_command.Email))
                            .ReturnsAsync(user);
        //Act
        var result = await _handler.Handle(_command, default);
        //Assert
        Assert.True(result.IsError);
        Assert.Equal(Error.Failure(description: "email or password incorrect"),
                                     result.FirstError);
    }


    [Fact]
    public async Task Handle_Should_ReturnAuthResult_WhenInputIsValid()
    {
        //Arrange


        var user = new User()
        {
            Email = _command.Email,
            Password = _command.Password
        };
        _userRepositoryMock.Setup(x => x.GetByEmailAsync(_command.Email))
                             .ReturnsAsync(user);


        var token = "token";
        _JwtGeneratorMock.Setup(x => x.GenerateToken(user))
                            .Returns(token);

        AuthResult authResult = CreateAuthResult(user, token);

        _mapperMock.Setup(x => x.Map<AuthResult>(user))
                    .Returns(authResult);


        //Act
        var result = await _handler.Handle(_command, default);
        //Assert
        Assert.False(result.IsError);
        Assert.Equal(authResult, result.Value);
        _userRepositoryMock.Verify(x => x.GetByEmailAsync(_command.Email),
                                     Times.Once);



    }

    private static AuthResult CreateAuthResult(User user, string token)
    {
        return new AuthResult(Guid.Empty,
                              user.FirstName,
                              user.LastName,
                              user.Email,
                              user.Password,
                              user.Role,
                              token);
    }
}
