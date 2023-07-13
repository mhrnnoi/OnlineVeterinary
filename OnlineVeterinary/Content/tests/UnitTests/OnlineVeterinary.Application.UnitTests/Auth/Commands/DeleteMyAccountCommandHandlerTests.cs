using ErrorOr;
using MapsterMapper;
using Moq;
using OnlineVeterinary.Application.Common.Interfaces.Persistence;
using OnlineVeterinary.Application.Features.Auth.Commands.ChangeEmail;
using OnlineVeterinary.Application.Features.Auth.Commands.Delete;
using OnlineVeterinary.Domain.Users.Entities;

namespace OnlineVeterinary.Application.UnitTests.Auth.Commands
{
    public class DeleteMyAccountCommandHandlerTests
    {

        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IMapper> _mapperMock;

        public DeleteMyAccountCommandHandlerTests()
        {
            _userRepositoryMock = new();
            _unitOfWorkMock = new();
            _mapperMock = new();
        }

        [Fact]
        public async Task Handle_Should_ReturnNotFound_WhenUserIsNull()
        {
            //Arrange
            var id = Guid.NewGuid();
            var command = new DeleteMyAccountCommand(id.ToString());
            var handler = new DeleteMyAccountCommandHandler(
                _mapperMock.Object,
                _unitOfWorkMock.Object,
                _userRepositoryMock.Object);

            _userRepositoryMock.Setup(x => x.GetByIdAsync(id))
                                .ReturnsAsync((User?)null);
            //Act
            var result = await handler.Handle(command, default);
            //Assert
            Assert.True(result.IsError);
            Assert.Equal(Error.NotFound(description: "you have invalid Id or this user is not exist any more"), result.FirstError);



        }

        [Fact]
        public async Task Handle_Should_ReturnSuccesMassage_WhenInputIsCorrect()
        {
            //Arrange
            var id = Guid.NewGuid();
            var command = new DeleteMyAccountCommand(id.ToString());
            var handler = new DeleteMyAccountCommandHandler(
                _mapperMock.Object,
                _unitOfWorkMock.Object,
                _userRepositoryMock.Object);

            var user = new User();
            _userRepositoryMock.Setup(x => x.GetByIdAsync(id))
                                .ReturnsAsync(user);


            //Act
            var result = await handler.Handle(command, default);
            //Assert
            Assert.False(result.IsError);
            Assert.Equal("Deleted successfully", result.Value);
            _userRepositoryMock.Verify(x => x.Remove(user), Times.Once);
            _unitOfWorkMock.Verify(x => x.SaveChangesAsync(), Times.Once);



        }
    }
}