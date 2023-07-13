using ErrorOr;
using MapsterMapper;
using Moq;
using OnlineVeterinary.Application.Common.Interfaces.Persistence;
using OnlineVeterinary.Application.Features.Doctors.Queries.GetById;
using OnlineVeterinary.Application.Features.Features.Doctors.Common;
using OnlineVeterinary.Domain.Users.Entities;

namespace OnlineVeterinary.Application.UnitTests.Doctors.Queries
{
    public class GetDoctorByIdQueryHandlerTests
    {
        
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;

        public GetDoctorByIdQueryHandlerTests()
        {
            _userRepositoryMock = new();
            _mapperMock = new();
        }
        [Fact]
        public async Task Handle_Should_ReturnNotFound_WhenUserIsNullAsync()
        {
            var id = Guid.NewGuid();
            var query = new GetDoctorByIdQuery(id);
            var handler = new GetDoctorByIdQueryHandler(
                _userRepositoryMock.Object,
                _mapperMock.Object);

            _userRepositoryMock.Setup(x => x.GetByIdAsync(id))
                                .ReturnsAsync((User?)null);
            
            var result = await handler.Handle(query, default);
            //Assert
            Assert.True(result.IsError);
            Assert.Equal(Error.NotFound(description: "doctor with this id is not exist"), result.FirstError);
            _userRepositoryMock.Verify(x=> x.GetByIdAsync(id)
            , Times.Once);
                
        }

         [Fact]
        public async Task Handle_Should_ReturnNotFound_WhenUserIsNotDoctorAsync()
        {
            var id = Guid.NewGuid();
            var query = new GetDoctorByIdQuery(id);
            var handler = new GetDoctorByIdQueryHandler(
                _userRepositoryMock.Object,
                _mapperMock.Object);
            
            var user = new User() {Role = "1"};
            _userRepositoryMock.Setup(x => x.GetByIdAsync(id))
                                .ReturnsAsync(user);
            
            var result = await handler.Handle(query, default);
            //Assert
            Assert.True(result.IsError);
            Assert.Equal(Error.NotFound(description: "doctor with this id is not exist"), result.FirstError);
            _userRepositoryMock.Verify(x=> x.GetByIdAsync(id)
            , Times.Once);
                
        }

             [Fact]
        public async Task Handle_Should_ReturnDoctorDto_WhenDoctorWithIdFoundedAsync()
        {
            var id = Guid.NewGuid();
            var query = new GetDoctorByIdQuery(id);
            var handler = new GetDoctorByIdQueryHandler(
                _userRepositoryMock.Object,
                _mapperMock.Object);
            
            var user = new User() {Role = "doctor"};
            _userRepositoryMock.Setup(x => x.GetByIdAsync(id))
                                .ReturnsAsync(user);


            var doctorDto = It.IsAny<DoctorDTO>();
            _mapperMock.Setup(x => x.Map<DoctorDTO>(user))
                .Returns(doctorDto);
            
            var result = await handler.Handle(query, default);
            //Assert
            Assert.False(result.IsError);
            Assert.Equal(doctorDto, result.Value);
            _userRepositoryMock.Verify(x=> x.GetByIdAsync(id)
            , Times.Once);
                
        }
    }
}