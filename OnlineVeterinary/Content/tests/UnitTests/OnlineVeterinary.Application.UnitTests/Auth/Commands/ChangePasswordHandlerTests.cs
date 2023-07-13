using ErrorOr;
using MapsterMapper;
using Moq;
using OnlineVeterinary.Application.Common.Interfaces.Persistence;
using OnlineVeterinary.Application.Features.Auth.Commands.ChangePassword;
using OnlineVeterinary.Domain.Users.Entities;

namespace OnlineVeterinary.Application.UnitTests.Auth.Commands;

public class ChangePasswordHandlerTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Guid _id;
    private readonly ChangePasswordCommand _command;
    private readonly ChangePasswordCommandHandler _handler;

    public ChangePasswordHandlerTests()
    {
        _userRepositoryMock = new();
        _unitOfWorkMock = new();
        _mapperMock = new();

        _id = Guid.NewGuid();
        _command = new ChangePasswordCommand(_id.ToString(),
                                             "mehrn@email.com");
        _handler = new ChangePasswordCommandHandler(_userRepositoryMock.Object,
                                                    _mapperMock.Object,
                                                    _unitOfWorkMock.Object);
    }
    [Fact]
    public async Task Handle_Should_ReturnNotFound_WhenUserIsNotExist()
    {
        //Arrange
        _userRepositoryMock.Setup(x => x.GetByIdAsync(_id))
                            .ReturnsAsync((User?)null);
        //Act
        var result = await _handler.Handle(_command, default);
        //Assert
        Assert.True(result.IsError);
        Assert.Equal(Error.NotFound(description: "you have invalid Id or this user is not exist any more"),
                                     result.FirstError);

    }
    [Fact]
    public async Task Handle_Should_ReturnSuccesMassage_WhenInputIsCorrect()
    {
        //Arrange
        var user = new User();
        _userRepositoryMock.Setup(x => x.GetByIdAsync(_id))
                            .ReturnsAsync(user);
        //Act
        var result = await _handler.Handle(_command, default);
        //Assert
        Assert.False(result.IsError);
        Assert.Equal("Password changed successfully", result.Value);
        _userRepositoryMock.Verify(x => x.Update(user), Times.Once);
        _unitOfWorkMock.Verify(x => x.SaveChangesAsync(), Times.Once);

    }
}
