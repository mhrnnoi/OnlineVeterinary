using ErrorOr;
using MapsterMapper;
using Moq;
using OnlineVeterinary.Application.Common.Interfaces.Persistence;
using OnlineVeterinary.Application.Features.Common;
using OnlineVeterinary.Application.Features.ReservedTimes.Queries.GetAll;
using OnlineVeterinary.Domain.Reservation.Entities;
using OnlineVeterinary.Domain.Users.Entities;

namespace OnlineVeterinary.Application.UnitTests.Reservations.Queries;

public class GetAllReservationsQueryHandlerTests
{
    private readonly Mock<IReservationRepository> _reservationRepositoryMock;
    private readonly Mock<IUserRepository> _userRepositoryMock;

    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<ICacheService> _chacheServiceMock;
    private readonly Guid _userId;
    private readonly GetAllReservationsQuery _command;
    private readonly GetAllReservationsQueryHandler _handler;

    public GetAllReservationsQueryHandlerTests()
    {
        _reservationRepositoryMock = new();
        _userRepositoryMock = new();
        _mapperMock = new();
        _chacheServiceMock = new();

        _userId = Guid.NewGuid();

        _command = new GetAllReservationsQuery(_userId.ToString(),
                                                 "doctor");
        _handler = new GetAllReservationsQueryHandler(_reservationRepositoryMock.Object,
                                                      _mapperMock.Object,
                                                      _userRepositoryMock.Object,
                                                      _chacheServiceMock.Object);

    }
    [Fact]
    public async Task Handle_Should_ReturnNotFound_WhenUserIsNotExist()
    {
        //Arrange

        _userRepositoryMock.Setup(x => x.GetByIdAsync(_userId))
                            .ReturnsAsync((User?)null);
        //Act
        var result = await _handler.Handle(_command, default);
        //Assert
        Assert.True(result.IsError);
        Assert.Equal(Error.NotFound(description: "you have invalid Id or this user is not exist any more"),
                                     result.FirstError);

    }
    [Fact]
    public async Task Handle_Should_ReturnReservationsDto_WhenInputValidOkChache()
    {
        //Arrange

        var user = new User() { Id = _userId };

        _userRepositoryMock.Setup(x => x.GetByIdAsync(_userId))
                            .ReturnsAsync(user);

        var key = $"{_userId} reservations";
        var reservationDtos = new List<ReservationDTO>() { new ReservationDTO() };

        _chacheServiceMock.Setup(x => x.GetData<List<ReservationDTO>>(key))
                            .Returns(reservationDtos);
        //Act
        var result = await _handler.Handle(_command, default);
        //Assert
        Assert.False(result.IsError);
        Assert.Equal(reservationDtos, result.Value);

    }
    [Fact]
    public async Task Handle_Should_ReturnReservationsDto_WhenInputValidNoChache()
    {
        //Arrange
        var user = new User() { Id = _userId };

        _userRepositoryMock.Setup(x => x.GetByIdAsync(_userId))
                            .ReturnsAsync(user);

        var key = $"{_userId} reservations";
        var reservationDtos = new List<ReservationDTO>() { new ReservationDTO() };

        var reservations = new List<Reservation>() { new Reservation() { DoctorId = _userId } };

        _chacheServiceMock.Setup(x => x.GetData<List<ReservationDTO>>(key))
                            .Returns(reservationDtos);


        _reservationRepositoryMock.Setup(x => x.GetAllAsync())
                                    .ReturnsAsync(reservations);

        _mapperMock.Setup(x => x.Map<List<ReservationDTO>>(It.IsAny<Reservation>()))
                    .Returns(reservationDtos);

        var expiryTime = DateTimeOffset.Now.AddSeconds(30);

        _chacheServiceMock.Setup(x => x.SetData<List<ReservationDTO>>(key,
                                                                      reservationDtos,
                                                                      expiryTime));

        //Act
        var result = await _handler.Handle(_command, default);
        //Assert
        Assert.False(result.IsError);
        Assert.Equal(reservationDtos, result.Value);

    }
}
