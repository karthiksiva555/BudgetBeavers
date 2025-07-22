using AutoFixture;
using BudgetBeavers.Application.Dtos.HomeDtos;
using BudgetBeavers.Application.Services;
using BudgetBeavers.Core.Entities;
using BudgetBeavers.Core.Interfaces;
using FluentAssertions;
using Moq;
using Xunit;

namespace BudgetBeavers.Application.Tests.Services;

public class HomeServiceTests : TestBase
{
    private readonly Mock<IHomeRepository> _homeRepositoryMock = new();
    private readonly HomeService _homeService;

    public HomeServiceTests()
    {
        _homeService = new HomeService(_homeRepositoryMock.Object);
    }

    #region AddAsync

    [Fact]
    public async Task AddAsync_CreateHomeDtoIsNull_ThrowsArgumentNullExceptionAsync()
    {
        Func<Task> act = async () => await _homeService.AddAsync(null!);
        
        await act.Should().ThrowAsync<ArgumentNullException>()
            .WithParameterName("createHomeDto");
    }
    
    [Xunit.Theory]
    [InlineData(null)]
    [InlineData("")]
    public async Task AddAsync_NameIsInvalid_ThrowsArgumentExceptionAsync(string? name)
    {
        var createHomeDto = Fixture.Build<CreateHomeDto>()
            .With(h => h.Name, name)
            .Create();
        
        Func<Task> act = async () => await _homeService.AddAsync(createHomeDto);
        
        await act.Should().ThrowAsync<ArgumentException>()
            .WithParameterName(nameof(createHomeDto.Name))
            .Where(e => e.Message.Contains("cannot be null or whitespace."));
    }

    [Fact]
    public async Task AddAsync_CreateHomeDtoIsValid_SavesHomeAndReturnsDtoAsync()
    {
        // Arrange
        var createHomeDto = Fixture.Build<CreateHomeDto>().With(h => h.Name, "Test Home").Create();
        var home = createHomeDto.ToEntity();
        var expectedHomeDto = home.ToDto();

        _homeRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Home>()))
            .ReturnsAsync(home);

        // Act
        var result = await _homeService.AddAsync(createHomeDto);

        // Assert
        result.Should().BeEquivalentTo(expectedHomeDto);
        _homeRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Home>()), Times.Once);
    }
    
    #endregion
    
    #region UpdateAsync
    
    [Fact]
    public async Task UpdateAsync_UpdateHomeDtoIsNull_ThrowsArgumentNullExceptionAsync()
    {
        Func<Task> act = async () => await _homeService.UpdateAsync(Guid.NewGuid(), null!);
        
        await act.Should().ThrowAsync<ArgumentNullException>()
            .WithParameterName("updateHomeDto");
    }
    
    [Fact]
    public async Task UpdateAsync_NameIsInvalid_ThrowsArgumentExceptionAsync()
    {
        var updateHomeDto = Fixture.Build<UpdateHomeDto>()
            .With(h => h.Name, (string?)null)
            .Create();
        
        Func<Task> act = async () => await _homeService.UpdateAsync(Guid.NewGuid(), updateHomeDto);
        
        await act.Should().ThrowAsync<ArgumentException>()
            .WithParameterName(nameof(updateHomeDto.Name))
            .Where(e => e.Message.Contains("cannot be null or whitespace."));
    }
    
    [Fact]
    public async Task UpdateAsync_HomeIdIsInvalid_ThrowsArgumentExceptionAsync()
    {
        var updateHomeDto = Fixture.Build<UpdateHomeDto>()
            .With(h => h.Name, "Updated Home")
            .Create();
        
        Func<Task> act = async () => await _homeService.UpdateAsync(Guid.Empty, updateHomeDto);
        
        await act.Should().ThrowAsync<ArgumentException>()
            .WithParameterName("id")
            .Where(e => e.Message.Contains("cannot be empty."));
    }
    
    [Fact]
    public async Task UpdateAsync_HomeDoesNotExist_ThrowsKeyNotFoundExceptionAsync()
    {
        var updateHomeDto = Fixture.Build<UpdateHomeDto>().With(h => h.Name, "Updated Home").Create();
        var homeId = Guid.NewGuid();

        _homeRepositoryMock.Setup(repo => repo.GetByIdAsync(homeId))
            .ReturnsAsync((Home?)null);

        Func<Task> act = async () => await _homeService.UpdateAsync(homeId, updateHomeDto);
        
        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage($"No entity found with the provided id: {homeId}.");
    }
    
    [Fact]
    public async Task UpdateAsync_ValidHomeIdAndDto_UpdatesHomeAndReturnsDtoAsync()
    {
        // Arrange
        var createdAt = DateTime.UtcNow.AddDays(-10);
        var homeId = Guid.NewGuid();
        var updateHomeDto = Fixture.Build<UpdateHomeDto>()
            .With(h => h.Name, "Updated Home")
            .Create();
        
        var existingHome = new Home { Id = homeId, Name = "Old Home", CreatedAt = createdAt };
        var updatedHome = new Home { Id = homeId, Name = updateHomeDto.Name, CreatedAt = createdAt };
        var expectedHomeDto = updatedHome.ToDto();

        _homeRepositoryMock.Setup(repo => repo.GetByIdAsync(homeId))
            .ReturnsAsync(existingHome);
        _homeRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Home>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _homeService.UpdateAsync(homeId, updateHomeDto);

        // Assert
        result.Should().BeEquivalentTo(expectedHomeDto);
        _homeRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Home>()), Times.Once);
    }
    
    #endregion
    
    #region DeleteAsync
    
    [Fact]
    public async Task DeleteAsync_HomeIdIsEmpty_ThrowsArgumentExceptionAsync()
    {
        // Act  
        var act = async () => await _homeService.DeleteAsync(Guid.Empty);
        
        // Assert
        await act.Should().ThrowAsync<ArgumentException>()
            .WithParameterName("id")
            .Where(e => e.Message.Contains("cannot be empty."));
    }
    
    [Fact]
    public async Task DeleteAsync_HomeDoesNotExist_ThrowsKeyNotFoundExceptionAsync()
    {
        // Arrange
        var homeId = Guid.NewGuid();

        _homeRepositoryMock.Setup(repo => repo.GetByIdAsync(homeId))
            .ReturnsAsync((Home?)null);

        // Act
        var act = async () => await _homeService.DeleteAsync(homeId);
        
        // Assert
        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage($"No entity found with the provided id: {homeId}.");
    }

    [Fact]
    public async Task DeleteAsync_ValidHomeIdAndDto_DeletesHomeAndReturnsDtoAsync()
    {
        // Arrange
        var homeId = Guid.NewGuid();
        var home = new Home { Id = homeId, Name = "Test Home", CreatedAt = DateTime.UtcNow.AddDays(-10) };
        _homeRepositoryMock.Setup(repo => repo.GetByIdAsync(homeId))
            .ReturnsAsync(home);
        _homeRepositoryMock.Setup(repo => repo.DeleteAsync(It.IsAny<Home>()))
            .Returns(Task.CompletedTask);
        
        // Act
        await _homeService.DeleteAsync(homeId);
        
        // Assert
        _homeRepositoryMock.Verify(repo => repo.DeleteAsync(It.Is<Home>(h => h.Id == homeId)), Times.Once);
    }
    
    #endregion
    
    #region GetByIdAsync
    
    [Fact]
    public async Task GetByIdAsync_HomeIdIsEmpty_ThrowsArgumentExceptionAsync()
    {
        // Act
        var act = async () => await _homeService.GetByIdAsync(Guid.Empty);
        
        // Assert
        await act.Should().ThrowAsync<ArgumentException>()
            .WithParameterName("id")
            .Where(e => e.Message.Contains("cannot be empty."));
    }
    
    [Fact]
    public async Task GetByIdAsync_HomeDoesNotExist_ThrowsKeyNotFoundExceptionAsync()
    {
        // Arrange
        var homeId = Guid.NewGuid();
        _homeRepositoryMock.Setup(repo => repo.GetByIdAsync(homeId))
            .ReturnsAsync((Home?)null);
        
        // Act
        var act = async () => await _homeService.GetByIdAsync(homeId);
        
        // Assert
        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage($"No entity found with the provided id: {homeId}.");
    }
    
    [Fact]
    public async Task GetByIdAsync_ValidHomeId_ReturnsHomeDtoAsync()
    {
        // Arrange
        var homeId = Guid.NewGuid();
        var home = new Home { Id = homeId, Name = "Test Home", CreatedAt = DateTime.UtcNow.AddDays(-10) };
        var expectedHomeDto = home.ToDto();
        
        _homeRepositoryMock.Setup(repo => repo.GetByIdAsync(homeId))
            .ReturnsAsync(home);
        
        // Act
        var result = await _homeService.GetByIdAsync(homeId);
        
        // Assert
        result.Should().BeEquivalentTo(expectedHomeDto);
    }
    
    #endregion
}