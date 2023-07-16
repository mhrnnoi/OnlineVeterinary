using MapsterMapper;
using Moq;
using OnlineVeterinary.Application.Common.Interfaces.Persistence;
using OnlineVeterinary.Application.Features.Doctors.Queries.GetAll;
using OnlineVeterinary.Application.Features.Features.Doctors.Common;
using OnlineVeterinary.Domain.Users.Entities;

namespace OnlineVeterinary.Application.UnitTests.Doctors.Queries;

public class GetAllDoctorsQueryHandlerTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<ICacheService> _chacheServiceMock;
    private readonly GetAllDoctorsQuery _query;
    private readonly GetAllDoctorsQueryHandler _handler;
    private const string Key = "doctors";


    public GetAllDoctorsQueryHandlerTests()
    {
        _userRepositoryMock = new();
        _mapperMock = new();
        _chacheServiceMock = new();

        _query = new GetAllDoctorsQuery();
        _handler = new GetAllDoctorsQueryHandler(
           _userRepositoryMock.Object,
           _mapperMock.Object,
           _chacheServiceMock.Object);
    }
    [Fact]
    public async Task Handle_Should_ReturnDoctorsDTOListFromChache_WhenInputValidCacheAsync()
    {
        //Arrange


        var doctorsDTO = new List<DoctorDTO>();

        doctorsDTO.Add(It.IsAny<DoctorDTO>());

        _chacheServiceMock.Setup(x => x.GetData<List<DoctorDTO>>(Key))
                            .Returns(doctorsDTO);

        //Act
        var result = await _handler.Handle(_query, default);
        //Assert
        Assert.False(result.IsError);
        Assert.Equal(doctorsDTO, result.Value);
        _chacheServiceMock.Verify(x => x.GetData<List<DoctorDTO>>(Key)
                                , Times.Once);
    }

    [Fact]
    public async Task Handle_Should_ReturnDoctorsDTOListFromDb_WhenInputValidNoCacheAsync()
    {
        //Arrange


        var users = new List<User>();
        _userRepositoryMock.Setup(x => x.GetAllAsync())
                                .ReturnsAsync(users);
        var doctorsDTO = new List<DoctorDTO>();

        _mapperMock.Setup(x => x.Map<List<DoctorDTO>>(users))
                    .Returns(doctorsDTO);

        var expiry = DateTimeOffset.Now.AddSeconds(30);


        _chacheServiceMock.Setup(x => x.SetData<List<DoctorDTO>>(
                                                            Key,
                                                            doctorsDTO,
                                                            expiry))
                                                        .Returns(true);

        //Act
        var result = await _handler.Handle(_query, default);
        //Assert
        Assert.False(result.IsError);
        Assert.Equal(doctorsDTO, result.Value);
        _userRepositoryMock.Verify(x => x.GetAllAsync(),
                                       Times.Once);
    }

}
