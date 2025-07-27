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
    private readonly Mock<IPasswordService> _passwordService = new();
    private readonly IUserService _userService;

    public UserServiceTests()
    {
        _userService = new UserService(_userRepository.Object, _passwordService.Object);
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
    
    [Fact]
    public async Task AddAsync_CreateUserDtoIsValid_ReturnsUserDtoAsync()
    {
        // Arrange
        var createUserDto = Fixture.Create<CreateUserDto>();
        var user = createUserDto.ToEntity();
        user.Id = Guid.NewGuid();
        user.PasswordHash = "hashedPassword";
        
        _userRepository.Setup(repo => repo.AddAsync(It.IsAny<Core.Entities.User>()))
            .ReturnsAsync(user);
        
        _passwordService.Setup(ps => ps.HashPassword(createUserDto.Password))
            .Returns("hashedPassword");
        
        // Act
        var result = await _userService.AddAsync(createUserDto);
        
        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(user.Id);
        result.FirstName.Should().Be(createUserDto.FirstName);
        result.LastName.Should().Be(createUserDto.LastName);
        result.Email.Should().Be(createUserDto.Email);
        result.PhoneNumber.Should().Be(createUserDto.PhoneNumber);
        _passwordService.Verify(p => p.HashPassword(createUserDto.Password), Times.Once);
    }
    
    #endregion

    #region UpdateAsync

    [Fact]
    public async Task UpdateAsync_UpdateUserDtoIsNull_ThrowsArgumentNullExceptionAsync()
    {
        Func<Task> act = async () => await _userService.UpdateAsync(Guid.NewGuid(), null!);
        
        await act.Should().ThrowAsync<ArgumentNullException>()
            .WithParameterName("updateUserDto");
    }
    
    [Fact]
    public async Task UpdateAsync_UserIdIsEmpty_ThrowsArgumentExceptionAsync()
    {
        var updateUserDto = Fixture.Create<UpdateUserDto>();
        
        Func<Task> act = async () => await _userService.UpdateAsync(Guid.Empty, updateUserDto);
        
        await act.Should().ThrowAsync<ArgumentException>()
            .WithParameterName("id")
            .Where(e => e.Message.Contains("cannot be empty."));
    }

    [Xunit.Theory]
    [InlineData(null)]
    [InlineData("")]
    public async Task UpdateAsync_FirstNameIsInvalid_ThrowsArgumentExceptionAsync(string? firstName)
    {
        // Arrange
        var updateUserDto = Fixture.Build<UpdateUserDto>().With(u => u.FirstName, firstName).Create();
        
        // Act
        Func<Task> act = async () => await _userService.UpdateAsync(Guid.NewGuid(), updateUserDto);
        
        // Assert
        await act.Should().ThrowAsync<ArgumentException>()
            .WithParameterName(nameof(updateUserDto.FirstName))
            .Where(e => e.Message.Contains("cannot be null or whitespace."));
    }
    
    [Xunit.Theory]
    [InlineData(null)]
    [InlineData("")]
    public async Task UpdateAsync_LastNameIsInvalid_ThrowsArgumentExceptionAsync(string? lastName)
    {
        // Arrange
        var updateUserDto = Fixture.Build<UpdateUserDto>().With(u => u.LastName, lastName).Create();
        
        // Act
        Func<Task> act = async () => await _userService.UpdateAsync(Guid.NewGuid(), updateUserDto);
        
        // Assert
        await act.Should().ThrowAsync<ArgumentException>()
            .WithParameterName(nameof(updateUserDto.LastName))
            .Where(e => e.Message.Contains("cannot be null or whitespace."));
    }
    
    [Fact]
    public async Task UpdateAsync_UserDoesNotExist_ThrowsKeyNotFoundExceptionAsync()
    {
        var updateUserDto = Fixture.Create<UpdateUserDto>();
        var userId = Guid.NewGuid();
        
        _userRepository.Setup(repo => repo.GetByIdAsync(userId))
            .ReturnsAsync((Core.Entities.User?)null);
        
        Func<Task> act = async () => await _userService.UpdateAsync(userId, updateUserDto);
        
        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage($"No entity found with the provided id: {userId}.");
    }

    [Fact]
    public async Task UpdateAsync_UserExists_ReturnsUpdatedUserDtoAsync()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var updateUserDto = Fixture.Create<UpdateUserDto>();

        var existingUser = new Core.Entities.User
        {
            Id = userId,
            FirstName = "OldFirstName",
            LastName = "OldLastName",
            Email = "testemail@test.com",
            PhoneNumber = "1234567890",
            PasswordHash = "oldPasswordHash"
        };

        _userRepository.Setup(u => u.GetByIdAsync(userId))
            .ReturnsAsync(existingUser);
        
        // Act
        var result = await _userService.UpdateAsync(userId, updateUserDto);
        
        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(userId);
        result.FirstName.Should().Be(updateUserDto.FirstName);
        result.LastName.Should().Be(updateUserDto.LastName);
        result.Email.Should().Be(existingUser.Email);
        result.PhoneNumber.Should().Be(updateUserDto.PhoneNumber);
        _userRepository.Verify(u => u.UpdateAsync(It.IsAny<Core.Entities.User>()), Times.Once);
    }

    #endregion
}