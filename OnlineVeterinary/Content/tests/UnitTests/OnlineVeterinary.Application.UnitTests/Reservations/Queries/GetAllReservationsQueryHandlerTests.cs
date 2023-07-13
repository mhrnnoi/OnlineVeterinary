using ErrorOr;
using MapsterMapper;
using Moq;
using OnlineVeterinary.Application.Common.Interfaces.Persistence;
using OnlineVeterinary.Application.Features.Common;
using OnlineVeterinary.Application.Features.ReservedTimes.Queries.GetAll;
using OnlineVeterinary.Domain.Reservation.Entities;
using OnlineVeterinary.Domain.Users.Entities;

namespace OnlineVeterinary.Application.UnitTests.Reservations.Queries
{
    public class GetAllReservationsQueryHandlerTests
    {
        private readonly Mock<IReservationRepository> _reservationRepositoryMock;
        private readonly Mock<IUserRepository> _userRepositoryMock;

        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<ICacheService> _chacheServiceMock;

        public GetAllReservationsQueryHandlerTests()
        {
            _reservationRepositoryMock = new();
            _userRepositoryMock = new();
            _mapperMock = new();
            _chacheServiceMock = new();

        }
        [Fact]
        public async Task Handle_Should_ReturnNotFound_WhenUserIsNull()
        {
            //Arrange
            var userId = Guid.NewGuid();

            var command = new GetAllReservationsQuery(userId.ToString(), "doctor");
            var handler = new GetAllReservationsQueryHandler(
                _reservationRepositoryMock.Object,
                _mapperMock.Object,
                _userRepositoryMock.Object,
                _chacheServiceMock.Object);

   
          

          


            _userRepositoryMock.Setup(x => x.GetByIdAsync(userId))
                                .ReturnsAsync((User?)null);
            //Act
            var result = await handler.Handle(command, default);
            //Assert
            Assert.True(result.IsError);
            Assert.Equal(Error.NotFound(description: "you have invalid Id or this user is not exist any more"), result.FirstError);

        }
        [Fact]
        public async Task Handle_Should_ReturnReservationsDto_WhenInputValidOkChache()
        {
            //Arrange
            var userId = Guid.NewGuid();

            var command = new GetAllReservationsQuery(userId.ToString(), "doctor");
            var handler = new GetAllReservationsQueryHandler(
                _reservationRepositoryMock.Object,
                _mapperMock.Object,
                _userRepositoryMock.Object,
                _chacheServiceMock.Object);

   
          

          
            var user = new User() {Id = userId};

            _userRepositoryMock.Setup(x => x.GetByIdAsync(userId))
                                .ReturnsAsync(user);

            var key = $"{userId} reservations";
            var reservationDtos = new List<ReservationDTO>()
            {
                new ReservationDTO()
            };
            _chacheServiceMock.Setup(x => x.GetData<List<ReservationDTO>>(key))
                .Returns(reservationDtos);
            //Act
            var result = await handler.Handle(command, default);
            //Assert
            Assert.False(result.IsError);
            Assert.Equal(reservationDtos, result.Value);

        }
        [Fact]
        public async Task Handle_Should_ReturnReservationsDto_WhenInputValidNoChache()
        {
            //Arrange
            var userId = Guid.NewGuid();

            var command = new GetAllReservationsQuery(userId.ToString(), "doctor");
            var handler = new GetAllReservationsQueryHandler(
                _reservationRepositoryMock.Object,
                _mapperMock.Object,
                _userRepositoryMock.Object,
                _chacheServiceMock.Object);

   
          

          
            var user = new User() {Id = userId};

            _userRepositoryMock.Setup(x => x.GetByIdAsync(userId))
                                .ReturnsAsync(user);

            var key = $"{userId} reservations";
            var reservationDtos = new List<ReservationDTO>()
            {
            };
            var reservations = new List<Reservation>()
            {
            };
            reservations.Add(
                new Reservation() {DoctorId = userId}

            );
            _chacheServiceMock.Setup(x => x.GetData<List<ReservationDTO>>(key))
                .Returns(reservationDtos);

            reservationDtos.Add(
                new ReservationDTO()

            );
            _reservationRepositoryMock.Setup(x => x.GetAllAsync())
                .ReturnsAsync(reservations);

            _mapperMock.Setup(x => x.Map<List<ReservationDTO>>(It.IsAny<Reservation>()) )
                .Returns(reservationDtos);
            var expiryTime = DateTimeOffset.Now.AddSeconds(30);

            
            _chacheServiceMock.Setup(x => x.SetData<List<ReservationDTO>>(key, reservationDtos, expiryTime));
            
            //Act
            var result = await handler.Handle(command, default);
            //Assert
            Assert.False(result.IsError);
            Assert.Equal(reservationDtos, result.Value);

        }
    }
}