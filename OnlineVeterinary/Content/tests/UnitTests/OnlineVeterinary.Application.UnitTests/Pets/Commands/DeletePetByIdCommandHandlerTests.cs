using ErrorOr;
using MapsterMapper;
using Moq;
using OnlineVeterinary.Application.Common.Interfaces.Persistence;
using OnlineVeterinary.Application.Features.Pets.Commands.Delete;
using OnlineVeterinary.Domain.Pet.Entities;
using OnlineVeterinary.Domain.Users.Entities;

namespace OnlineVeterinary.Application.UnitTests.Pets.Commands
{
    public class DeletePetByIdCommandHandlerTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IPetRepository> _petRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<ICacheService> _chacheServiceMock;

        public DeletePetByIdCommandHandlerTests()
        {
            _userRepositoryMock = new();
            _petRepositoryMock = new();
            _mapperMock = new();
            _unitOfWorkMock = new();
            _chacheServiceMock = new();
        }
        [Fact]
        public async Task Handle_Should_ReturnNotFound_WhenUserIsNull()
        {
            //Arrange
            var careGiverId = Guid.NewGuid();
            var petId = Guid.NewGuid();

            var command = new DeletePetByIdCommand(petId, careGiverId.ToString());
            var handler = new DeletePetByIdCommandHandler(
                _petRepositoryMock.Object,
                _mapperMock.Object,
                _unitOfWorkMock.Object,
                _userRepositoryMock.Object,
                _chacheServiceMock.Object);

            _userRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                                .ReturnsAsync((User?)null);
            //Act
            var result = await handler.Handle(command, default);
            //Assert
            Assert.True(result.IsError);
            Assert.Equal(Error.NotFound(description: "you have invalid Id or this user is not exist any more"), result.FirstError);

        }

        [Fact]
        public async Task Handle_Should_ReturnNotFound_WhenPetIsNull()
        {
            //Arrange
            var careGiverId = Guid.NewGuid();
            var petId = Guid.NewGuid();

            var command = new DeletePetByIdCommand(petId, careGiverId.ToString());
            var handler = new DeletePetByIdCommandHandler(
                _petRepositoryMock.Object,
                _mapperMock.Object,
                _unitOfWorkMock.Object,
                _userRepositoryMock.Object,
                _chacheServiceMock.Object);


            var user = new User();
            _userRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                                .ReturnsAsync(user);

            _petRepositoryMock.Setup(x => x.GetByIdAsync(petId))
                .ReturnsAsync((Pet?)null);
            //Act
            var result = await handler.Handle(command, default);
            //Assert
            Assert.True(result.IsError);
            Assert.Equal(Error.NotFound(description: "you dont have any pet with this id"), result.FirstError);

        }

        [Fact]
        public async Task Handle_Should_ReturnNotFound_WhenPetIsNotYours()
        {
            //Arrange
            var careGiverId = Guid.NewGuid();
            var petId = Guid.NewGuid();

            var command = new DeletePetByIdCommand(petId, careGiverId.ToString());
            var handler = new DeletePetByIdCommandHandler(
                _petRepositoryMock.Object,
                _mapperMock.Object,
                _unitOfWorkMock.Object,
                _userRepositoryMock.Object,
                _chacheServiceMock.Object);

            var user = new User();
            _userRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                                .ReturnsAsync(user);

            var pet = new Pet() { CareGiverId = Guid.NewGuid() };
            _petRepositoryMock.Setup(x => x.GetByIdAsync(petId))
                .ReturnsAsync(pet);
            //Act
            var result = await handler.Handle(command, default);
            //Assert
            Assert.True(result.IsError);
            Assert.Equal(Error.NotFound(description: "you dont have any pet with this id"), result.FirstError);

        }

         [Fact]
        public async Task Handle_Should_ReturnSuccesMessage_WhenInputIsValid()
        {
            //Arrange
            var careGiverId = Guid.NewGuid();
            var petId = Guid.NewGuid();

            var command = new DeletePetByIdCommand(petId, careGiverId.ToString());
            var handler = new DeletePetByIdCommandHandler(
                _petRepositoryMock.Object,
                _mapperMock.Object,
                _unitOfWorkMock.Object,
                _userRepositoryMock.Object,
                _chacheServiceMock.Object);

            var user = new User();
            _userRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                                .ReturnsAsync(user);

            var pet = new Pet() { CareGiverId = careGiverId };
            _petRepositoryMock.Setup(x => x.GetByIdAsync(petId))
                .ReturnsAsync(pet);

            _petRepositoryMock.Setup(x => x.Remove(pet));

            _unitOfWorkMock.Setup(x => x.SaveChangesAsync())
                .Returns(Task.CompletedTask);
            
            var key = $"{careGiverId} pets";
            _chacheServiceMock.Setup(x => x.RemoveData(key));

                
                
            //Act
            var result = await handler.Handle(command, default);
            //Assert
            Assert.False(result.IsError);
            Assert.Equal("Deleted successfully", result.Value);
            _unitOfWorkMock.Verify(x=> x.SaveChangesAsync(),
            Times.Once);
            

        }
    }
}