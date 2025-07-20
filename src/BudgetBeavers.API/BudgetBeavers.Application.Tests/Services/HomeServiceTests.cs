using AutoFixture;
using BudgetBeavers.Application.Dtos.HomeDtos;
using BudgetBeavers.Application.Services;
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

        _homeRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Core.Entities.Home>()))
            .ReturnsAsync(home);

        // Act
        var result = await _homeService.AddAsync(createHomeDto);

        // Assert
        result.Should().BeEquivalentTo(expectedHomeDto);
        _homeRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Core.Entities.Home>()), Times.Once);
    }
    
    #endregion
}