using BudgetBeavers.Application.Dtos.HomeUserDtos;
using BudgetBeavers.Application.Services;
using BudgetBeavers.Core.Interfaces;
using FluentAssertions;
using Moq;
using Xunit;
using AutoFixture;
using BudgetBeavers.Application.Dtos.UserDtos;
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
    public async Task UpdateAsync_UserIdAndRoleIdAreNull_ThrowsArgumentExceptionAsync()
    {
        var updateHomeUserDto = Fixture.Build<UpdateHomeUserDto>()
            .With(hu => hu.UserId, (Guid?)null)
            .With(hu => hu.RoleId, (Guid?)null)
            .Create();
        
        Func<Task> act = async () => await _homeUserService.UpdateAsync(Guid.NewGuid(), updateHomeUserDto);
        
        await act.Should().ThrowAsync<ArgumentException>()
            .WithParameterName(nameof(updateHomeUserDto))
            .Where(e => e.Message.Contains("At least one property (RoleId or UserId) must be provided for update."));
    }
    
    [Fact]
    public async Task UpdateAsync_ValidHomeUserIdAndDto_UpdatesHomeUserAndReturnsDtoAsync()
    {
        // Arrange
        var homeUserId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var roleId = Guid.NewGuid();
        var updateHomeUserDto = Fixture.Build<UpdateHomeUserDto>()
            .With(hu => hu.UserId, userId)
            .With(hu => hu.RoleId, roleId)
            .Create();
        var existingHomeUser = new HomeUser
        {
            Id = homeUserId,
            UserId = userId,
            RoleId = roleId
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
        result.UserId.Should().Be(userId);
        result.RoleId.Should().Be(roleId);
        
        _homeUserRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<HomeUser>()), Times.Once);
    }
    
    #endregion

    #region DeleteAsync

    [Fact]
    public async Task DeleteAsync_IdIsInvalid_ThrowsArgumentExceptionAsync()
    {
        Func<Task> action = async () => await _homeUserService.DeleteAsync(Guid.Empty);
        
        await action.Should().ThrowAsync<ArgumentException>()
            .WithParameterName("id")
            .Where(e => e.Message.Contains("cannot be empty."));
    }
    
    [Fact]
    public async Task DeleteAsync_HomeUserNotFound_ThrowsKeyNotFoundExceptionAsync()
    {
        var homeUserId = Guid.NewGuid();
       
        _homeUserRepositoryMock.Setup(repo => repo.GetByIdAsync(homeUserId))
            .ReturnsAsync((HomeUser?)null);
        
        Func<Task> action = async () => await _homeUserService.DeleteAsync(homeUserId);
        
        await action.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage($"No entity found with the provided id: {homeUserId}.");
    }

    [Fact]
    public async Task DeleteAsync_ValidId_DeletesHomeUserAsync()
    {
        var homeUserId = Guid.NewGuid();
        var existingHomeUser = new HomeUser { Id = homeUserId };
        _homeUserRepositoryMock.Setup(repo => repo.GetByIdAsync(homeUserId))
            .ReturnsAsync(existingHomeUser);
        _homeUserRepositoryMock.Setup(repo => repo.DeleteAsync(existingHomeUser))
            .Returns(Task.CompletedTask);
        
        await _homeUserService.DeleteAsync(homeUserId);
        
        _homeUserRepositoryMock.Verify(repo => repo.DeleteAsync(existingHomeUser), Times.Once);
    }

    #endregion
    
    #region GetByIdAsync
    
    [Fact]
    public async Task GetByIdAsync_IdIsInvalid_ThrowsArgumentExceptionAsync()
    {
        Func<Task> act = async () => await _homeUserService.GetByIdAsync(Guid.Empty);
        
        await act.Should().ThrowAsync<ArgumentException>()
            .WithParameterName("id")
            .Where(e => e.Message.Contains("cannot be empty."));
    }

    [Fact]
    public async Task GetByIdAsync_HomeUserNotFound_ThrowsKeyNotFoundExceptionAsync()
    {
        var homeUserId = Guid.NewGuid();
        _homeUserRepositoryMock.Setup(repo => repo.GetByIdAsync(homeUserId))
            .ReturnsAsync((HomeUser?)null);
        
        Func<Task> act = async () => await _homeUserService.GetByIdAsync(homeUserId);
        
        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage($"No entity found with the provided id: {homeUserId}.");
    }

    [Fact]
    public async Task GetByIdAsync_ValidId_ReturnsHomeUserDtoAsync()
    {
        var homeUserId = Guid.NewGuid();
        var existingHomeUser = new HomeUser
        {
            Id = homeUserId,
            UserId = Guid.NewGuid(),
            HomeId = Guid.NewGuid(),
            RoleId = Guid.NewGuid()
        };
        var expectedDto = existingHomeUser.ToDto();
        _homeUserRepositoryMock.Setup(repo => repo.GetByIdAsync(homeUserId))
            .ReturnsAsync(existingHomeUser);
        
        var result = await _homeUserService.GetByIdAsync(homeUserId);
        
        result.Should().BeEquivalentTo(expectedDto);
        _homeUserRepositoryMock.Verify(repo => repo.GetByIdAsync(homeUserId), Times.Once);
    }

    #endregion
    
    #region GetMembersByHomeIdAsync
    
    [Fact]
    public async Task GetMembersByHomeIdAsync_HomeIdIsInvalid_ThrowsArgumentExceptionAsync()
    {
        Func<Task> act = async () => await _homeUserService.GetMembersByHomeIdAsync(Guid.Empty);
        
        await act.Should().ThrowAsync<ArgumentException>()
            .WithParameterName("homeId")
            .Where(e => e.Message.Contains("cannot be empty."));
    }

    [Fact]
    public async Task GetMembersByHomeIdAsync_HomeDoesNotHaveMembers_ReturnsEmptyUserListAsync()
    {
        var homeId = Guid.NewGuid();
        _homeUserRepositoryMock.Setup(repo => repo.GetMembersByHomeIdAsync(homeId))
            .ReturnsAsync(new List<User?>());
        
        var result = await _homeUserService.GetMembersByHomeIdAsync(homeId);
        
        result.Should().BeEmpty();
        _homeUserRepositoryMock.Verify(repo => repo.GetMembersByHomeIdAsync(homeId), Times.Once);
    }
    
    [Fact]
    public async Task GetMembersByHomeIdAsync_HomeIdHasAssociatedMembers_ReturnsUserListAsync()
    {
        var homeId = Guid.NewGuid();
        var users = Fixture.Create<IEnumerable<User>>();
        var userList = users.ToList();
        var expectedUsers = userList.Select(u => u.ToDto());
        _homeUserRepositoryMock.Setup(repo => repo.GetMembersByHomeIdAsync(homeId))
            .ReturnsAsync(userList);
        
        var result = await _homeUserService.GetMembersByHomeIdAsync(homeId);
        
        result.Should().BeEquivalentTo(expectedUsers);
        _homeUserRepositoryMock.Verify(repo => repo.GetMembersByHomeIdAsync(homeId), Times.Once);
    }
    
    #endregion
}