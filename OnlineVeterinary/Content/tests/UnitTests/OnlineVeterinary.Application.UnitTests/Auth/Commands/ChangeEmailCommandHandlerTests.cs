using ErrorOr;
using MapsterMapper;
using Moq;
using OnlineVeterinary.Application.Common.Interfaces.Persistence;
using OnlineVeterinary.Application.Features.Auth.Commands.ChangeEmail;
using OnlineVeterinary.Domain.Users.Entities;

namespace OnlineVeterinary.Application.UnitTests.Auth.Commands;

public class ChangeEmailCommandHandlerTests
{

    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Guid _id;
    private readonly ChangeEmailCommand _command;
    private readonly ChangeEmailCommandHandler _handler;

    public ChangeEmailCommandHandlerTests()
    {
        _userRepositoryMock = new();
        _unitOfWorkMock = new();
        _mapperMock = new();

        _id = Guid.NewGuid();
        _command = new ChangeEmailCommand("mehran@email.com",
                                             _id.ToString());
        _handler = new ChangeEmailCommandHandler(_userRepositoryMock.Object,
                                                    _unitOfWorkMock.Object,
                                                    _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_Should_ReturnNotFound_WhenUserIdIsNotExistInRepo()
    {
        //Arrange

        _userRepositoryMock.Setup(x => x.GetByIdAsync(_id))
                            .ReturnsAsync((User?)null);
        //Act
        var result = await _handler.Handle(_command, default);
        //Assert
        Assert.True(result.IsError);
        Assert.Equal(
            Error.NotFound(
            description: "you have invalid Id or this user is not exist any more"),
            result.FirstError);



    }



    [Fact]
    public async Task Handle_Should_ReturnFailure_WhenEmailIsAlreadyExist()
    {
        //Arrange


        _userRepositoryMock.Setup(x => x.GetByIdAsync(_id))
                            .ReturnsAsync(new User());

        _userRepositoryMock.Setup(x => x.GetByEmailAsync(_command.NewEmail))
                            .ReturnsAsync(new User());
        //Act
        var result = await _handler.Handle(_command, default);
        //Assert
        Assert.True(result.IsError);
        Assert.Equal(Error.Failure(description: "this email is already exist "),
                                     result.FirstError);



    }

    [Fact]
    public async Task Handle_Should_ReturnSuccesMassage_WhenInputIsCorrect()
    {
        //Arrange
        var user = new User();
        _userRepositoryMock.Setup(x => x.GetByIdAsync(_id))
                            .ReturnsAsync(user);

        _userRepositoryMock.Setup(x => x.GetByEmailAsync(_command.NewEmail))
                            .ReturnsAsync((User?)null);
        //Act
        var result = await _handler.Handle(_command, default);
        //Assert
        Assert.False(result.IsError);
        Assert.Equal("email changed successfully", result.Value);
        _userRepositoryMock.Verify(x => x.Update(user), Times.Once);
        _unitOfWorkMock.Verify(x => x.SaveChangesAsync(), Times.Once);



    }
}
