using BudgetBeavers.Application.Dtos.HomeUserDtos;
using BudgetBeavers.Application.Services;
using BudgetBeavers.Core.Interfaces;
using FluentAssertions;
using Moq;
using Xunit;
using AutoFixture;

namespace BudgetBeavers.Application.Tests.Services;

public class HomeUserServiceTests : TestBase
{
    private readonly Mock<IHomeUserRepository> _homeRepositoryMock = new();
    private readonly HomeUserService _homeUserService;

    public HomeUserServiceTests()
    {
        _homeUserService = new HomeUserService(_homeRepositoryMock.Object);
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

    #endregion

}