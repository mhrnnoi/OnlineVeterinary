using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using MapsterMapper;
using Moq;
using OnlineVeterinary.Application.Common.Interfaces.Persistence;
using OnlineVeterinary.Application.Features.Pets.Commands.Add;
using OnlineVeterinary.Domain.Pet.Entities;
using OnlineVeterinary.Domain.Users.Entities;
using Xunit;

namespace OnlineVeterinary.Application.UnitTests.Pets.Commands
{
    public class AddPetCommandHandlerTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IPetRepository> _petRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<ICacheService> _chacheServiceMock;

        public AddPetCommandHandlerTests()
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
            var id = Guid.NewGuid();
            var command = new AddPetCommand("", DateTime.Now, 0, id, "");
            var handler = new AddPetCommandHandler(
                _petRepositoryMock.Object,
                _mapperMock.Object,
                _unitOfWorkMock.Object,
                _userRepositoryMock.Object,
                _chacheServiceMock.Object);

            _userRepositoryMock.Setup(x => x.GetByIdAsync(id))
                                .ReturnsAsync((User?)null);
            //Act
            var result = await handler.Handle(command, default);
            //Assert
            Assert.True(result.IsError);
            Assert.Equal(Error.NotFound(description: "you have invalid Id or this user is not exist any more"), result.FirstError);

        }

        [Fact]
        public async Task Handle_Should_AddPet_WhenUserIsFound()
        {
            //Arrange
            var id = Guid.NewGuid();
            var command = new AddPetCommand("", DateTime.Now, 0, id, "");
            var handler = new AddPetCommandHandler(
                _petRepositoryMock.Object,
                _mapperMock.Object,
                _unitOfWorkMock.Object,
                _userRepositoryMock.Object,
                _chacheServiceMock.Object);

            var user = new User();
            _userRepositoryMock.Setup(x => x.GetByIdAsync(id))
                                .ReturnsAsync(user);
            
            var pet = It.IsAny<Pet>();
            _mapperMock.Setup(x => x.Map<Pet>(command))
                .Returns(pet);
            _unitOfWorkMock.Setup(x => x.SaveChangesAsync())
                .Returns(Task.CompletedTask);

            _chacheServiceMock.Setup(x => x.RemoveData($"{id} pets"));
            //Act
            var result = await handler.Handle(command, default);
            //Assert
            Assert.False(result.IsError);
            Assert.Equal("pet Added successfully", result.Value);
            _unitOfWorkMock.Verify(x=> x.SaveChangesAsync(), 
            Times.Once);
            _petRepositoryMock.Verify(x=> x.Add(pet), 
            Times.Once);

        }
    }
}