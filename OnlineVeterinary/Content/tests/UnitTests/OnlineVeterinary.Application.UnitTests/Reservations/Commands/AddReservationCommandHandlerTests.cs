using ErrorOr;
using MapsterMapper;
using Moq;
using OnlineVeterinary.Application.Common.Interfaces.Persistence;
using OnlineVeterinary.Application.Common.Interfaces.Services;
using OnlineVeterinary.Application.Features.Common;
using OnlineVeterinary.Application.Features.Reservations.Commands.Add;
using OnlineVeterinary.Domain.Pet.Entities;
using OnlineVeterinary.Domain.Reservation.Entities;
using OnlineVeterinary.Domain.Users.Entities;

namespace OnlineVeterinary.Application.UnitTests.Reservations.Commands
{
    public class AddReservationCommandHandlerTests
    {

        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IPetRepository> _petRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IReservationRepository> _reservationRepositoryMock;
        private readonly Mock<IDateTimeProvider> _dateTimeProviderMock;

        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<ICacheService> _chacheServiceMock;

        public AddReservationCommandHandlerTests()
        {
            _userRepositoryMock = new();
            _petRepositoryMock = new();
            _mapperMock = new();
            _unitOfWorkMock = new();
            _chacheServiceMock = new();
            _reservationRepositoryMock = new();
            _dateTimeProviderMock = new();
        }
        [Fact]
        public async Task Handle_Should_ReturnNotFound_WhenUserIsNull()
        {
            //Arrange
            var careGiverId = Guid.NewGuid();
            var petId = Guid.NewGuid();
            var doctorId = Guid.NewGuid();

            var command = new AddReservationCommand(petId, doctorId, careGiverId.ToString());
            var handler = new AddReservationCommandHandler(
                _reservationRepositoryMock.Object,
                _mapperMock.Object,
                _unitOfWorkMock.Object,
                 _petRepositoryMock.Object,
                 _dateTimeProviderMock.Object,
                _userRepositoryMock.Object,
                _chacheServiceMock.Object);

            var users = new List<User>()
            {
                new User() {Role = "doctor", Id = doctorId}

            };
            var pet = new Pet() { Id = petId };
            _userRepositoryMock.Setup(x => x.GetAllAsync())
                                .ReturnsAsync(users);

            _petRepositoryMock.Setup(x => x.GetByIdAsync(petId))
                .ReturnsAsync(pet);


            _userRepositoryMock.Setup(x => x.GetByIdAsync(careGiverId))
                                .ReturnsAsync((User?)null);
            //Act
            var result = await handler.Handle(command, default);
            //Assert
            Assert.True(result.IsError);
            Assert.Equal(Error.NotFound(description: "you have invalid Id or this user is not exist any more"), result.FirstError);

        }
        [Fact]
        public async Task Handle_Should_ReturnNotFound_WhenDoctorIsNull()
        {
            //Arrange
            var careGiverId = Guid.NewGuid();
            var petId = Guid.NewGuid();
            var doctorId = Guid.NewGuid();

            var command = new AddReservationCommand(petId, doctorId, careGiverId.ToString());
            var handler = new AddReservationCommandHandler(
                _reservationRepositoryMock.Object,
                _mapperMock.Object,
                _unitOfWorkMock.Object,
                 _petRepositoryMock.Object,
                 _dateTimeProviderMock.Object,
                _userRepositoryMock.Object,
                _chacheServiceMock.Object);
            var users = new List<User>()
            {

            };
            var pet = new Pet() { Id = petId };
            _userRepositoryMock.Setup(x => x.GetAllAsync())
                                .ReturnsAsync(users);

            _petRepositoryMock.Setup(x => x.GetByIdAsync(petId))
                .ReturnsAsync(pet);



            var user = new User() { Id = careGiverId };
            _userRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                                .ReturnsAsync(user);
            //Act
            var result = await handler.Handle(command, default);
            //Assert
            Assert.True(result.IsError);
            Assert.Equal(Error.NotFound(description: "the doctor with this id is not exist"), result.FirstError);

        }
        [Fact]
        public async Task Handle_Should_ReturnNotFound_WhenPetIsNull()
        {
            //Arrange
            var careGiverId = Guid.NewGuid();
            var petId = Guid.NewGuid();
            var doctorId = Guid.NewGuid();

            var command = new AddReservationCommand(petId, doctorId, careGiverId.ToString());
            var handler = new AddReservationCommandHandler(
                _reservationRepositoryMock.Object,
                _mapperMock.Object,
                _unitOfWorkMock.Object,
                 _petRepositoryMock.Object,
                 _dateTimeProviderMock.Object,
                _userRepositoryMock.Object,
                _chacheServiceMock.Object);
            var users = new List<User>()
            {
                new User() {Role = "doctor", Id = doctorId}

            };
            var pet = new Pet() { Id = petId };
            _userRepositoryMock.Setup(x => x.GetAllAsync())
                                .ReturnsAsync(users);

            _petRepositoryMock.Setup(x => x.GetByIdAsync(petId))
                .ReturnsAsync((Pet?)null);



            var user = new User() { Id = careGiverId };
            _userRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                                .ReturnsAsync(user);
            //Act
            var result = await handler.Handle(command, default);
            //Assert
            Assert.True(result.IsError);
            Assert.Equal(Error.NotFound(description: "the pet with this id is not exist"), result.FirstError);

        }
        [Fact]
        public async Task Handle_Should_ReturnNotFound_WhenPetIsNotYours()
        {
            //Arrange
            var careGiverId = Guid.NewGuid();
            var petId = Guid.NewGuid();
            var doctorId = Guid.NewGuid();

            var command = new AddReservationCommand(petId, doctorId, careGiverId.ToString());
            var handler = new AddReservationCommandHandler(
                _reservationRepositoryMock.Object,
                _mapperMock.Object,
                _unitOfWorkMock.Object,
                 _petRepositoryMock.Object,
                 _dateTimeProviderMock.Object,
                _userRepositoryMock.Object,
                _chacheServiceMock.Object);
            var users = new List<User>()
            {
                new User() {Role = "doctor", Id = doctorId}

            };
            var pet = new Pet()
            {
                Id = petId,
                CareGiverId = Guid.NewGuid()
            };
            _userRepositoryMock.Setup(x => x.GetAllAsync())
                                .ReturnsAsync(users);

            _petRepositoryMock.Setup(x => x.GetByIdAsync(petId))
                .ReturnsAsync(pet);



            var user = new User() { Id = careGiverId };
            _userRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                                .ReturnsAsync(user);
            //Act
            var result = await handler.Handle(command, default);
            //Assert
            Assert.True(result.IsError);
            Assert.Equal(Error.NotFound(description: "you dont have any pet with this id"), result.FirstError);

        }
        [Fact]
        public async Task Handle_Should_ReturnReservationDtoWith30MinutuesAfterLastReservedTimeorNow_WhenInputIsValidAnd30MinutuesAfterLastReserveOrNowIsInWorkingHour()
        {
            //Arrange
            var careGiverId = Guid.NewGuid();
            var petId = Guid.NewGuid();
            var doctorId = Guid.NewGuid();

            var command = new AddReservationCommand(petId, doctorId, careGiverId.ToString());
            var handler = new AddReservationCommandHandler(
                _reservationRepositoryMock.Object,
                _mapperMock.Object,
                _unitOfWorkMock.Object,
                 _petRepositoryMock.Object,
                 _dateTimeProviderMock.Object,
                _userRepositoryMock.Object,
                _chacheServiceMock.Object);
            var users = new List<User>()
            {
                new User() {Role = "doctor", Id = doctorId}

            };
            var pet = new Pet()
            {
                Id = petId,
                CareGiverId = careGiverId
            };
            _userRepositoryMock.Setup(x => x.GetAllAsync())
                                .ReturnsAsync(users);

            _petRepositoryMock.Setup(x => x.GetByIdAsync(petId))
                .ReturnsAsync(pet);



            var user = new User() { Id = careGiverId };
            _userRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                                .ReturnsAsync(user);

            var now = new DateTime(2023, 07, 1, 08, 0, 0);
            _dateTimeProviderMock.Setup(x => x.UtcNow)
                .Returns(now);

            var reservations = new List<Reservation>()
            {
                new Reservation() {DateOfReservation = now
                , DoctorId = doctorId}
            };
            _reservationRepositoryMock.Setup(x => x.GetAllAsync())
                .ReturnsAsync(reservations);

            var reservation = new Reservation()
            {
                DateOfReservation = now.AddMinutes(30)
                ,
                DoctorId = doctorId,
                PetId = petId,
                CareGiverId = careGiverId
            };
            _reservationRepositoryMock.Setup(x => x.Add(reservation))

            ;


            _unitOfWorkMock.Setup(x => x.SaveChangesAsync())
                .Returns(Task.CompletedTask);

            var reservationDto = new ReservationDTO()
            {
                DateOfReservation = reservation.DateOfReservation

            };
            _mapperMock.Setup(x => x.Map<ReservationDTO>(It.IsAny<Reservation>()))
                .Returns(reservationDto);

            //Act
            var result = await handler.Handle(command, default);
            //Assert
            Assert.False(result.IsError);
            Assert.Equal(reservationDto, result.Value);
            _unitOfWorkMock.Verify(x => x.SaveChangesAsync(),
            Times.Once);

        }
        [Fact]
        public async Task Handle_Should_ReturnReservationDtoTommarowFirstTime_WhenInputIsValidAnd30MinutuesAfterLastReserveOrNowIsNotInWorkingHour()
        {
            //Arrange
            var careGiverId = Guid.NewGuid();
            var petId = Guid.NewGuid();
            var doctorId = Guid.NewGuid();

            var command = new AddReservationCommand(petId, doctorId, careGiverId.ToString());
            var handler = new AddReservationCommandHandler(
                _reservationRepositoryMock.Object,
                _mapperMock.Object,
                _unitOfWorkMock.Object,
                 _petRepositoryMock.Object,
                 _dateTimeProviderMock.Object,
                _userRepositoryMock.Object,
                _chacheServiceMock.Object);
            var users = new List<User>()
            {
                new User() {Role = "doctor", Id = doctorId}

            };
            var pet = new Pet()
            {
                Id = petId,
                CareGiverId = careGiverId
            };
            _userRepositoryMock.Setup(x => x.GetAllAsync())
                                .ReturnsAsync(users);

            _petRepositoryMock.Setup(x => x.GetByIdAsync(petId))
                .ReturnsAsync(pet);



            var user = new User() { Id = careGiverId };
            _userRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                                .ReturnsAsync(user);

            var now = new DateTime(2023, 07, 1, 21, 0, 0);
            _dateTimeProviderMock.Setup(x => x.UtcNow)
                .Returns(now);

            var reservations = new List<Reservation>()
            {
                new Reservation() {DateOfReservation = now
                , DoctorId = doctorId}
            };
            _reservationRepositoryMock.Setup(x => x.GetAllAsync())
                .ReturnsAsync(reservations);

            var reservation = new Reservation()
            {
                DateOfReservation = new DateTime(
                                now.Year,
                                now.Month,
                                now.Day + 1,
                                07, 0, 0)
                ,
                DoctorId = doctorId,
                PetId = petId,
                CareGiverId = careGiverId
            };
            _reservationRepositoryMock.Setup(x => x.Add(It.IsAny<Reservation>()))

            ;


            _unitOfWorkMock.Setup(x => x.SaveChangesAsync())
                .Returns(Task.CompletedTask);

            var reservationDto = new ReservationDTO()
            {
                DateOfReservation = reservation.DateOfReservation

            };
            var key = $"{careGiverId} reservations";

            _chacheServiceMock.Setup(x => x.RemoveData(key));
                
            _mapperMock.Setup(x => x.Map<ReservationDTO>(It.IsAny<Reservation>()))
                .Returns(reservationDto);

            //Act
            var result = await handler.Handle(command, default);
            //Assert
            Assert.False(result.IsError);
            Assert.Equal(reservationDto, result.Value);
            _unitOfWorkMock.Verify(x => x.SaveChangesAsync(),
            Times.Once);

        }
    }
}