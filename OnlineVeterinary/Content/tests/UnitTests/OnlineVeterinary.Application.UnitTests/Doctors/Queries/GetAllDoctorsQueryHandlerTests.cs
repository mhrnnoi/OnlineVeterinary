using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MapsterMapper;
using Moq;
using OnlineVeterinary.Application.Common.Interfaces.Persistence;
using OnlineVeterinary.Application.Features.Doctors.Queries.GetAll;
using OnlineVeterinary.Application.Features.Features.Doctors.Common;
using OnlineVeterinary.Domain.Users.Entities;
using Xunit;

namespace OnlineVeterinary.Application.UnitTests.Doctors.Queries
{
    public class GetAllDoctorsQueryHandlerTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<ICacheService> _chacheServiceMock;

        public GetAllDoctorsQueryHandlerTests()
        {
            _userRepositoryMock = new();
            _mapperMock = new();
            _chacheServiceMock = new();
        }
        [Fact]
        public async Task Handle_Should_ReturnDoctorsDTOListFromChache_WhenInputValidCacheAsync()
        {
            //Arrange
            var query = new GetAllDoctorsQuery();
            var handler = new GetAllDoctorsQueryHandler(
                _userRepositoryMock.Object,
                _mapperMock.Object,
                _chacheServiceMock.Object);

            var key = "doctors";
            var doctorsDTO = new List<DoctorDTO>();
            doctorsDTO.Add(It.IsAny<DoctorDTO>());
            _chacheServiceMock.Setup(x => x.GetData<List<DoctorDTO>>(key))
                .Returns(doctorsDTO);

            //Act
            var result = await handler.Handle(query, default);
            //Assert
            Assert.False(result.IsError);
            Assert.Equal(doctorsDTO, result.Value);
            _chacheServiceMock.Verify(x => x.GetData<List<DoctorDTO>>(key)
            , Times.Once);
        }

        [Fact]
        public async Task Handle_Should_ReturnDoctorsDTOListFromDb_WhenInputValidNoCacheAsync()
        {
            //Arrange
            var query = new GetAllDoctorsQuery();
            var handler = new GetAllDoctorsQueryHandler(
                _userRepositoryMock.Object,
                _mapperMock.Object,
                _chacheServiceMock.Object);

            var users = new List<User>();
            _userRepositoryMock.Setup(x => x.GetAllAsync())
                .ReturnsAsync(users);
            var doctorsDTO = new List<DoctorDTO>();

            _mapperMock.Setup(x => x.Map<List<DoctorDTO>>(users))
                .Returns(doctorsDTO);
            
            var expiry = DateTimeOffset.Now.AddSeconds(30);


            var key = "doctors";
            _chacheServiceMock.Setup(x => x.SetData<List<DoctorDTO>>(
                key,
                doctorsDTO,
                expiry))
                .Returns(true);

            //Act
            var result = await handler.Handle(query, default);
            //Assert
            Assert.False(result.IsError);
            Assert.Equal(doctorsDTO, result.Value);
            _userRepositoryMock.Verify(x => x.GetAllAsync()
            , Times.Once);
        }

    }
}