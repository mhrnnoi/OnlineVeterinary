using ErrorOr;
using MapsterMapper;
using Moq;
using OnlineVeterinary.Application.Common.Interfaces.Persistence;
using OnlineVeterinary.Application.Features.Auth.Commands.ChangeEmail;
using OnlineVeterinary.Domain.Users.Entities;

namespace OnlineVeterinary.Application.UnitTests.Auth.Commands
{
    public class ChangeEmailCommandHandlerTests
    {
        
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IMapper> _mapperMock;

        public ChangeEmailCommandHandlerTests()
        {
            _userRepositoryMock = new();
            _unitOfWorkMock =  new();
            _mapperMock = new();
        }

        [Fact]
        public async Task Handle_Should_ReturnNotFound_WhenUserIsNull()
        {
            //Arrange
            var id = Guid.NewGuid();
            var command = new ChangeEmailCommand("mehrn@email.com",id.ToString());
            var handler = new ChangeEmailCommandHandler(_userRepositoryMock.Object,
                                                        _unitOfWorkMock.Object,
                                                        _mapperMock.Object);

            _userRepositoryMock.Setup(x=> x.GetByIdAsync(id))
                                .ReturnsAsync((User?)null);
            //Act
            var result = await handler.Handle(command, default);
            //Assert
            Assert.True(result.IsError);
            Assert.Equal(
                Error.NotFound(
                description : "you have invalid Id or this user is not exist any more"),
             result.FirstError);
            
            

        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenEmailIsAlreadyExist()
        {
            //Arrange
            var id = Guid.NewGuid();
            var command = new ChangeEmailCommand("mehrn@email.com",id.ToString());
            var handler = new ChangeEmailCommandHandler(_userRepositoryMock.Object,
                                                        _unitOfWorkMock.Object,
                                                        _mapperMock.Object);

            _userRepositoryMock.Setup(x=> x.GetByIdAsync(id))
                                .ReturnsAsync(new User());

            _userRepositoryMock.Setup(x=> x.GetByEmailAsync(command.NewEmail))
                                .ReturnsAsync(new User());
            //Act
            var result = await handler.Handle(command, default);
            //Assert
            Assert.True(result.IsError);
            Assert.Equal(Error.Failure(description : "this email is already exist "), result.FirstError);
            
            

        }

        [Fact]
        public async Task Handle_Should_ReturnSuccesMassage_WhenInputIsCorrect()
        {
            //Arrange
            var id = Guid.NewGuid();
            var command = new ChangeEmailCommand("mehrn@email.com",id.ToString());
            var handler = new ChangeEmailCommandHandler(_userRepositoryMock.Object,
                                                        _unitOfWorkMock.Object,
                                                        _mapperMock.Object);

            var user = new User();
            _userRepositoryMock.Setup(x=> x.GetByIdAsync(id))
                                .ReturnsAsync(user);

            _userRepositoryMock.Setup(x=> x.GetByEmailAsync(command.NewEmail))
                                .ReturnsAsync((User?)null);
            //Act
            var result = await handler.Handle(command, default);
            //Assert
            Assert.False(result.IsError);
            Assert.Equal("email changed successfully", result.Value);
            _userRepositoryMock.Verify(x => x.Update(user),Times.Once);
            _unitOfWorkMock.Verify(x => x.SaveChangesAsync(),Times.Once);
            
            

        }
    }
}