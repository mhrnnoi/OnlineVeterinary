using ErrorOr;
using MapsterMapper;
using Moq;
using OnlineVeterinary.Application.Common.Interfaces.Persistence;
using OnlineVeterinary.Application.Features.Doctors.Queries.GetById;
using OnlineVeterinary.Application.Features.Features.Doctors.Common;
using OnlineVeterinary.Domain.Users.Entities;

namespace OnlineVeterinary.Application.UnitTests.Doctors.Queries;

public class GetDoctorByIdQueryHandlerTests
{

    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Guid _id;
    private readonly GetDoctorByIdQuery _query;
    private readonly GetDoctorByIdQueryHandler _handler;

    public GetDoctorByIdQueryHandlerTests()
    {
        _userRepositoryMock = new();
        _mapperMock = new();

        _id = Guid.NewGuid();
        _query = new GetDoctorByIdQuery(_id);
        _handler = new GetDoctorByIdQueryHandler(
           _userRepositoryMock.Object,
           _mapperMock.Object);
    }
    [Fact]
    public async Task Handle_Should_ReturnNotFound_WhenUserIsNotExist()
    {

        //Arrange
        _userRepositoryMock.Setup(x => x.GetByIdAsync(_id))
                            .ReturnsAsync((User?)null);

        //act
        var result = await _handler.Handle(_query, default);
        //Assert
        Assert.True(result.IsError);
        Assert.Equal(Error.NotFound(description: "doctor with this id is not exist"),
                                     result.FirstError);

        _userRepositoryMock.Verify(x => x.GetByIdAsync(_id),
                                    Times.Once);

    }

    [Fact]
    public async Task Handle_Should_ReturnNotFound_WhenUserIsNotDoctorAsync()
    {
        //Arrange


        var user = new User() { Role = "1" };
        _userRepositoryMock.Setup(x => x.GetByIdAsync(_id))
                            .ReturnsAsync(user);

        //Act

        var result = await _handler.Handle(_query, default);
        //Assert
        Assert.True(result.IsError);
        Assert.Equal(Error.NotFound(description: "doctor with this id is not exist"),
                                     result.FirstError);
        _userRepositoryMock.Verify(x => x.GetByIdAsync(_id),
                                     Times.Once);

    }

    [Fact]
    public async Task Handle_Should_ReturnDoctorDto_WhenDoctorWithIdFoundedAsync()
    {
        //Arrange

        var user = new User() { Role = "doctor" };
        _userRepositoryMock.Setup(x => x.GetByIdAsync(_id))
                            .ReturnsAsync(user);


        var doctorDto = It.IsAny<DoctorDTO>();
        _mapperMock.Setup(x => x.Map<DoctorDTO>(user))
            .Returns(doctorDto);
        //Act
        var result = await _handler.Handle(_query, default);
        //Assert
        Assert.False(result.IsError);
        Assert.Equal(doctorDto, result.Value);
        _userRepositoryMock.Verify(x => x.GetByIdAsync(_id),
                                     Times.Once);

    }
}
