using BudgetBeavers.Application.Dtos.HomeUserDtos;
using BudgetBeavers.Application.Services;
using BudgetBeavers.Core.Interfaces;
using FluentAssertions;
using Moq;
using Xunit;
using AutoFixture;
using BudgetBeavers.Core.Entities;

namespace BudgetBeavers.Application.Tests.Services;

public class HomeUserServiceTests : TestBase
{
    private readonly Mock<IHomeUserRepository> _homeUserRepositoryMock = new();
    private readonly HomeUserService _homeUserService;

    public HomeUserServiceTests()
    {
        _homeUserService = new HomeUserService(_homeUserRepositoryMock.Object);
    }

    #region AddAsync

    [Fact]
    public async Task AddAsync_CreateHomeUserDtoIsNull_ThrowsArgumentNullExceptionAsync()
    {
        Func<Task> act = async () => await _homeUserService.AddAsync(null!);
        
        await act.Should().ThrowAsync<ArgumentNullException>()
            .WithParameterName("createHomeUserDto");
    }

    [Fact]
    public async Task AddAsync_HomeIdIsNull_ThrowsArgumentExceptionAsync()
    {
        var createHomeUserDto = Fixture.Build<CreateHomeUserDto>().With(hu => hu.HomeId, Guid.Empty).Create();
        
        Func<Task> act = async () => await _homeUserService.AddAsync(createHomeUserDto);
        
        await act.Should().ThrowAsync<ArgumentException>()
            .WithParameterName(nameof(createHomeUserDto.HomeId))
            .Where(e => e.Message.Contains("cannot be empty."));
    }
    
    [Fact]
    public async Task AddAsync_UserIdIsNull_ThrowsArgumentExceptionAsync()
    {
        var createHomeUserDto = Fixture.Build<CreateHomeUserDto>().With(hu => hu.UserId, Guid.Empty).Create();
        
        Func<Task> act = async () => await _homeUserService.AddAsync(createHomeUserDto);
        
        await act.Should().ThrowAsync<ArgumentException>()
            .WithParameterName(nameof(createHomeUserDto.UserId))
            .Where(e => e.Message.Contains("cannot be empty."));
    }
    
    [Fact]
    public async Task AddAsync_RoleIdIsNull_ThrowsArgumentExceptionAsync()
    {
        var createHomeUserDto = Fixture.Build<CreateHomeUserDto>().With(hu => hu.RoleId, Guid.Empty).Create();
        
        Func<Task> act = async () => await _homeUserService.AddAsync(createHomeUserDto);
        
        await act.Should().ThrowAsync<ArgumentException>()
            .WithParameterName(nameof(createHomeUserDto.RoleId))
            .Where(e => e.Message.Contains("cannot be empty."));
    }
    
    [Fact]
    public async Task AddAsync_CreateHomeUserDtoIsValid_SavesHomeUserAndReturnsDtoAsync()
    {
        // Arrange
        var createHomeUserDto = Fixture.Create<CreateHomeUserDto>();
        var homeUser = createHomeUserDto.ToEntity();
        var expectedHomeUserDto = homeUser.ToDto();

        _homeUserRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<HomeUser>()))
            .ReturnsAsync(homeUser);

        // Act
        var result = await _homeUserService.AddAsync(createHomeUserDto);

        // Assert
        result.Should().BeEquivalentTo(expectedHomeUserDto);
        _homeUserRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<HomeUser>()), Times.Once);
    }

    #endregion
    
}