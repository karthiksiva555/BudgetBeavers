using BudgetBeavers.Application.Services;
using BudgetBeavers.Core.Interfaces;
using FluentAssertions;
using Moq;
using Xunit;

namespace BudgetBeavers.Application.Tests.Services;

public class HomeUserServiceTests : TestBase
{
    private readonly Mock<IHomeUserRepository> _homeRepositoryMock = new();
    private readonly HomeUserService _homeService;

    public HomeUserServiceTests()
    {
        _homeService = new HomeUserService(_homeRepositoryMock.Object);
    }

    #region AddAsync

    [Fact]
    public async Task AddAsync_CreateHomeUserDtoIsNull_ThrowsArgumentNullExceptionAsync()
    {
        Func<Task> act = async () => await _homeService.AddAsync(null!);
        
        await act.Should().ThrowAsync<ArgumentNullException>()
            .WithParameterName("createUserDto");
    }

    #endregion

}