using ErrorOr;
using MapsterMapper;
using Moq;
using OnlineVeterinary.Application.Common.Interfaces.Persistence;
using OnlineVeterinary.Application.Features.CareGivers.Queries.GetPets;
using OnlineVeterinary.Application.Features.DTOs;
using OnlineVeterinary.Domain.Pet.Entities;
using OnlineVeterinary.Domain.Users.Entities;

namespace OnlineVeterinary.Application.UnitTests.CareGivers.Queries;



public class GetMyPetsQueryHandlerTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IPetRepository> _petRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<ICacheService> _chacheServiceMock;
    private readonly Guid _id;
    private readonly GetMyPetsQuery _query;
    private readonly GetMyPetsQueryHandler _handler;

    public GetMyPetsQueryHandlerTests()
    {
        _userRepositoryMock = new();
        _petRepositoryMock = new();
        _mapperMock = new();
        _chacheServiceMock = new();

        _id = Guid.NewGuid();
        _query = new GetMyPetsQuery(_id.ToString());
        _handler = new GetMyPetsQueryHandler(
            _petRepositoryMock.Object,
            _mapperMock.Object,
            _userRepositoryMock.Object,
            _chacheServiceMock.Object);
    }
    [Fact]
    public async Task Handle_Should_ReturnNotFound_WhenUserIsNotExist()
    {
        //Arrange

        _userRepositoryMock.Setup(x => x.GetByIdAsync(_id))
                            .ReturnsAsync((User?)null);
        //Act
        var result = await _handler.Handle(_query, default);
        //Assert
        Assert.True(result.IsError);
        Assert.Equal(
            Error.NotFound(
            description: "you have invalid Id or this user is not exist any more"),
            result.FirstError);
    }

    [Fact]
    public async Task Handle_Should_ReturnPetsDTOListFromChache_WhenInputValidCacheAsync()
    {
        //Arrange

        var user = new User();
        var userPetsDTO = new List<PetDTO>();

        AddFakePet(userPetsDTO);

        _userRepositoryMock.Setup(x => x.GetByIdAsync(_id))
                            .ReturnsAsync(user);

        var key = $"{_id} pets";

        _chacheServiceMock.Setup(x => x.GetData<List<PetDTO>>(key))
                            .Returns(userPetsDTO);
        //Act
        var result = await _handler.Handle(_query, default);
        //Assert
        Assert.False(result.IsError);
        Assert.Equal(userPetsDTO, result.Value);
        _chacheServiceMock.Verify(x => x.GetData<List<PetDTO>>(key)
                                    , Times.Once);
    }


    [Fact]
    public async Task Handle_Should_ReturnPetsDTOListFromDataBase_WhenInputValidNoChacheAsync()
    {
        //Arrange
        var user = new User();
        var userPetsDTO = new List<PetDTO>();


        _userRepositoryMock.Setup(x => x.GetByIdAsync(_id))
                            .ReturnsAsync(user);

        var key = $"{_id} pets";
        _chacheServiceMock.Setup(x => x.GetData<List<PetDTO>>(key))
                            .Returns(userPetsDTO);


        var userPets = new List<Pet>();
        userPets.Add(new Pet() { CareGiverId = _id });

        _petRepositoryMock.Setup(x => x.GetAllAsync())
                            .ReturnsAsync(userPets);

        AddFakePet(userPetsDTO);

        _mapperMock.Setup(x => x.Map<List<PetDTO>>(userPets))
                    .Returns(userPetsDTO);

        var expiry = DateTimeOffset.Now.AddSeconds(30);

        _chacheServiceMock.Setup(x => x.SetData(key,
                                                userPetsDTO,
                                                expiry))
                            .Returns(true);

        //Act
        var result = await _handler.Handle(_query, default);
        //Assert
        Assert.False(result.IsError);
        Assert.Equal(userPetsDTO, result.Value);

    }


    private void AddFakePet(List<PetDTO> userPetsDTO)
    {
        userPetsDTO.Add(new PetDTO(Guid.Empty,
                                   _id,
                                   "",
                                   DateTime.Now,
                                   "",
                                   ""));
    }
}
