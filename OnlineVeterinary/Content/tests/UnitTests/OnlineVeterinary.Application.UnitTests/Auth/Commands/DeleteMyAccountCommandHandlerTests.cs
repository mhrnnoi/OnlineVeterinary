using ErrorOr;
using MapsterMapper;
using Moq;
using OnlineVeterinary.Application.Common.Interfaces.Persistence;
using OnlineVeterinary.Application.Features.Auth.Commands.Delete;
using OnlineVeterinary.Domain.Users.Entities;

namespace OnlineVeterinary.Application.UnitTests.Auth.Commands;

public class DeleteMyAccountCommandHandlerTests
{

    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Guid _id;
    private readonly DeleteMyAccountCommand _command;
    private readonly DeleteMyAccountCommandHandler _handler;

    public DeleteMyAccountCommandHandlerTests()
    {
        _userRepositoryMock = new();
        _unitOfWorkMock = new();
        _mapperMock = new();

        _id = Guid.NewGuid();
        _command = new DeleteMyAccountCommand(_id.ToString());
        _handler = new DeleteMyAccountCommandHandler(
            _mapperMock.Object,
            _unitOfWorkMock.Object,
            _userRepositoryMock.Object);
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
        var result = await _handler.Handle(_command,
                                             default);

        //Assert
        Assert.False(result.IsError);
        Assert.Equal("Deleted successfully", result.Value);
        _userRepositoryMock.Verify(x => x.Remove(user),
                                         Times.Once);
        _unitOfWorkMock.Verify(x => x.SaveChangesAsync(),
                                     Times.Once);



    }
}
