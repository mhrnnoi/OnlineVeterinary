using ErrorOr;
using MapsterMapper;
using Moq;
using OnlineVeterinary.Application.Common.Interfaces.Persistence;
using OnlineVeterinary.Application.Common.Interfaces.Services;
using OnlineVeterinary.Application.Features.Auth.Commands.Register;
using OnlineVeterinary.Application.Features.Auth.Common;
using OnlineVeterinary.Domain.Users.Entities;

namespace OnlineVeterinary.Application.UnitTests.Auth.Commands;

public class RegisterCommandHandlerTests
{


    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IJwtGenerator> _JwtGeneratorMock;
    private readonly Guid _id;
    private readonly RegisterCommand _command;
    private readonly RegisterCommandHandler _handler;

    public RegisterCommandHandlerTests()
    {
        _userRepositoryMock = new();
        _unitOfWorkMock = new();
        _mapperMock = new();
        _JwtGeneratorMock = new();

        _id = Guid.NewGuid();
        _command = new RegisterCommand("", "", "", "", 0);
        _handler = new RegisterCommandHandler(_userRepositoryMock.Object,
                                              _mapperMock.Object,
                                              _JwtGeneratorMock.Object,
                                              _unitOfWorkMock.Object);
    }


    [Fact]
    public async Task Handle_Should_ReturnFailure_WhenEmailIsAlreadyExist()
    {
        //Arrange

        _userRepositoryMock.Setup(x => x.GetByEmailAsync(_command.Email))
                            .ReturnsAsync(new User());
        //Act
        var result = await _handler.Handle(_command, default);
        //Assert
        Assert.True(result.IsError);
        Assert.Equal(Error.Failure(description: "this email is already exist "), result.FirstError);



    }

    [Fact]
    public async Task Handle_Should_ReturnAuthResult_WhenInputIsValid()
    {
        //Arrange

        _userRepositoryMock.Setup(x => x.GetByEmailAsync(_command.Email))
                             .ReturnsAsync((User?)null);

        User user = CreateUser(_command);

        var token = "token";

        _JwtGeneratorMock.Setup(x => x.GenerateToken(user))
            .Returns(token);
        _mapperMock.Setup(x => x.Map<User>(_command))
            .Returns(user);

        AuthResult authResult = CreateAuthResult(user, token);

        _mapperMock.Setup(x => x.Map<AuthResult>(user))
            .Returns(authResult);


        //Act
        var result = await _handler.Handle(_command, default);
        //Assert
        Assert.False(result.IsError);
        Assert.Equal(authResult, result.Value);
        _userRepositoryMock.Verify(x => x.Add(user), Times.Once);
        _unitOfWorkMock.Verify(x => x.SaveChangesAsync(), Times.Once);



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

    private User CreateUser(RegisterCommand _command)
    {
        return new User()
        {
            Email = _command.Email,
            FirstName = _command.FirstName,
            LastName = _command.LastName,
            Password = _command.Password,
            Role = _command.Role.ToString()
        };
    }
}
