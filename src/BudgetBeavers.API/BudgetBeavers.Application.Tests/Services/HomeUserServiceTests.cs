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
    
    #region UpdateAsync
    
    [Fact]
    public async Task UpdateAsync_HomeUserIdIsInvalid_ThrowsArgumentExceptionAsync()
    {
        var updateHomeUserDto = Fixture.Create<UpdateHomeUserDto>();
        
        Func<Task> act = async () => await _homeUserService.UpdateAsync(Guid.Empty, updateHomeUserDto);

        await act.Should().ThrowAsync<ArgumentException>()
            .WithParameterName("id")
            .Where(e => e.Message.Contains("cannot be empty."));
    }

    [Fact]
    public async Task UpdateAsync_UpdateHomeUserDtoIsNull_ThrowsArgumentNullExceptionAsync()
    {
        Func<Task> act = async () => await _homeUserService.UpdateAsync(Guid.NewGuid(), null!);
        
        await act.Should().ThrowAsync<ArgumentNullException>()
            .WithParameterName("updateHomeUserDto");
    }

    [Fact]
    public async Task UpdateAsync_UserIdIsInvalid_ThrowsArgumentExceptionAsync()
    {
        var updateHomeUserDto = Fixture.Build<UpdateHomeUserDto>().With(hu => hu.UserId, Guid.Empty).Create();
        
        Func<Task> act = async () => await _homeUserService.UpdateAsync(Guid.NewGuid(), updateHomeUserDto);
        
        await act.Should().ThrowAsync<ArgumentException>()
            .WithParameterName(nameof(updateHomeUserDto.UserId))
            .Where(e => e.Message.Contains("cannot be empty."));
    }
    
    [Fact]
    public async Task UpdateAsync_RoleIdIsInvalid_ThrowsArgumentExceptionAsync()
    {
        var updateHomeUserDto = Fixture.Build<UpdateHomeUserDto>().With(hu => hu.RoleId, Guid.Empty).Create();
        
        Func<Task> act = async () => await _homeUserService.UpdateAsync(Guid.NewGuid(), updateHomeUserDto);
        
        await act.Should().ThrowAsync<ArgumentException>()
            .WithParameterName(nameof(updateHomeUserDto.RoleId))
            .Where(e => e.Message.Contains("cannot be empty."));
    }
    
    [Fact]
    public async Task UpdateAsync_HomeUserDoesNotExist_ThrowsKeyNotFoundExceptionAsync()
    {
        var updateHomeUserDto = Fixture.Create<UpdateHomeUserDto>();
        var homeUserId = Guid.NewGuid();

        _homeUserRepositoryMock.Setup(repo => repo.GetByIdAsync(homeUserId))
            .ReturnsAsync((HomeUser?)null);

        Func<Task> act = async () => await _homeUserService.UpdateAsync(homeUserId, updateHomeUserDto);

        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage($"No entity found with the provided id: {homeUserId}.");
    }
    
    [Fact]
    public async Task UpdateAsync_ValidHomeUserIdAndDto_UpdatesHomeUserAndReturnsDtoAsync()
    {
        // Arrange
        var homeUserId = Guid.NewGuid();
        var updateHomeUserDto = Fixture.Create<UpdateHomeUserDto>();
        var existingHomeUser = new HomeUser
        {
            Id = homeUserId,
            UserId = updateHomeUserDto.UserId,
            RoleId = updateHomeUserDto.RoleId
        };

        _homeUserRepositoryMock.Setup(repo => repo.GetByIdAsync(homeUserId))
            .ReturnsAsync(existingHomeUser);
        
        _homeUserRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<HomeUser>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _homeUserService.UpdateAsync(homeUserId, updateHomeUserDto);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(homeUserId);
        result.UserId.Should().Be(updateHomeUserDto.UserId);
        result.RoleId.Should().Be(updateHomeUserDto.RoleId);
        
        _homeUserRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<HomeUser>()), Times.Once);
    }
    
    #endregion
}