using ErrorOr;
using MapsterMapper;
using Moq;
using OnlineVeterinary.Application.Common.Interfaces.Persistence;
using OnlineVeterinary.Application.Common.Interfaces.Services;
using OnlineVeterinary.Application.Features.Auth.Commands.Register;
using OnlineVeterinary.Application.Features.Auth.Common;
using OnlineVeterinary.Domain.Users.Entities;

namespace OnlineVeterinary.Application.UnitTests.Auth.Commands
{
    public class RegisterCommandHandlerTests
    {


        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IJwtGenerator> _JwtGeneratorMock;


        public RegisterCommandHandlerTests()
        {
            _userRepositoryMock = new();
            _unitOfWorkMock = new();
            _mapperMock = new();
            _JwtGeneratorMock = new();
        }


        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenEmailIsAlreadyExist()
        {
            //Arrange
            var id = Guid.NewGuid();
            var command = new RegisterCommand("", "", "", "", 0);
            var handler = new RegisterCommandHandler(
                _userRepositoryMock.Object,
                _mapperMock.Object,
                _JwtGeneratorMock.Object,
                _unitOfWorkMock.Object);



            _userRepositoryMock.Setup(x => x.GetByEmailAsync(command.Email))
                                .ReturnsAsync(new User());
            //Act
            var result = await handler.Handle(command, default);
            //Assert
            Assert.True(result.IsError);
            Assert.Equal(Error.Failure(description: "this email is already exist "), result.FirstError);



        }

        [Fact]
        public async Task Handle_Should_ReturnAuthResult_WhenInputIsValid()
        {
            //Arrange
            var command = new RegisterCommand("", "", "", "", 0);
            var handler = new RegisterCommandHandler(
                _userRepositoryMock.Object,
                _mapperMock.Object,
                _JwtGeneratorMock.Object,
                _unitOfWorkMock.Object);




            _userRepositoryMock.Setup(x => x.GetByEmailAsync(command.Email))
                                 .ReturnsAsync((User?)null);

            var user = new User()
            {
                Email = command.Email,
                FirstName = command.FirstName,
                LastName = command.LastName,
                Password = command.Password,
                Role = command.Role.ToString()
            };
            var token = "gtg";
            _JwtGeneratorMock.Setup(x => x.GenerateToken(user))
                .Returns(token);
            _mapperMock.Setup(x => x.Map<User>(command))
                .Returns(user);
            var authResult = new AuthResult(Guid.Empty, user.FirstName, user.LastName, user.Email, user.Password, user.Role, token);

            _mapperMock.Setup(x => x.Map<AuthResult>(user))
                .Returns(authResult);


            //Act
            var result = await handler.Handle(command, default);
            //Assert
            Assert.False(result.IsError);
            Assert.Equal(authResult, result.Value);
            _userRepositoryMock.Verify(x => x.Add(user), Times.Once);
            _unitOfWorkMock.Verify(x => x.SaveChangesAsync(), Times.Once);



        }
    }
}