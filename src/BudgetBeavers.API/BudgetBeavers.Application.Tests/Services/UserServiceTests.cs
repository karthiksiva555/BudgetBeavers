using AutoFixture;
using BudgetBeavers.Application.Dtos.UserDtos;
using BudgetBeavers.Application.Interfaces;
using BudgetBeavers.Application.Services;
using BudgetBeavers.Core.Interfaces;
using FluentAssertions;
using Moq;
using Xunit;

namespace BudgetBeavers.Application.Tests.Services;

public class UserServiceTests : TestBase
{
    private readonly Mock<IUserRepository> _userRepository = new();
    private readonly IUserService _userService;

    public UserServiceTests()
    {
        _userService = new UserService(_userRepository.Object);
    }
    
    #region AddAsync
    
    [Fact]
    public async Task AddAsync_CreateUserDtoIsNull_ThrowsArgumentNullExceptionAsync()
    {
        Func<Task> act = async () => await _userService.AddAsync(null!);
        
        await act.Should().ThrowAsync<ArgumentNullException>()
            .WithParameterName("createUserDto");
    }
    
    [Xunit.Theory]
    [InlineData(null)]
    [InlineData("")]
    public async Task AddAsync_UserFirstNameIsInvalid_ThrowsArgumentExceptionAsync(string? firstName)
    {
        // Arrange
        var createUserDto = Fixture.Build<CreateUserDto>().With(u => u.FirstName, firstName).Create();
        
        // Act
        Func<Task> act = async () => await _userService.AddAsync(createUserDto);
        
        // Assert
        await act.Should().ThrowAsync<ArgumentException>()
            .WithParameterName(nameof(createUserDto.FirstName))
            .Where(e => e.Message.Contains("cannot be null or whitespace."));
    }
    
    [Xunit.Theory]
    [InlineData(null)]
    [InlineData("")]
    public async Task AddAsync_UserLastNameIsInvalid_ThrowsArgumentExceptionAsync(string? lastName)
    {
        // Arrange
        var createUserDto = Fixture.Build<CreateUserDto>().With(u => u.LastName, lastName).Create();
        
        // Act
        Func<Task> act = async () => await _userService.AddAsync(createUserDto);
        
        // Assert
        await act.Should().ThrowAsync<ArgumentException>()
            .WithParameterName(nameof(createUserDto.LastName))
            .Where(e => e.Message.Contains("cannot be null or whitespace."));
    }
    
    [Xunit.Theory]
    [InlineData(null)]
    [InlineData("")]
    public async Task AddAsync_PasswordIsInvalid_ThrowsArgumentExceptionAsync(string? password)
    {
        // Arrange
        var createUserDto = Fixture.Build<CreateUserDto>().With(u => u.Password, password).Create();
        
        // Act
        Func<Task> act = async () => await _userService.AddAsync(createUserDto);
        
        // Assert
        await act.Should().ThrowAsync<ArgumentException>()
            .WithParameterName(nameof(createUserDto.Password))
            .Where(e => e.Message.Contains("cannot be null or whitespace."));
    }
    
    [Xunit.Theory]
    [InlineData(null)]
    [InlineData("")]
    public async Task AddAsync_EmailIsInvalid_ThrowsArgumentExceptionAsync(string? email)
    {
        // Arrange
        var createUserDto = Fixture.Build<CreateUserDto>().With(u => u.Email, email).Create();
        
        // Act
        Func<Task> act = async () => await _userService.AddAsync(createUserDto);
        
        // Assert
        await act.Should().ThrowAsync<ArgumentException>()
            .WithParameterName(nameof(createUserDto.Email))
            .Where(e => e.Message.Contains("cannot be null or whitespace."));
    }
    
    #endregion
}