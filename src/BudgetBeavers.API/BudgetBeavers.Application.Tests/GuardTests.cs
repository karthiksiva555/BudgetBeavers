using BudgetBeavers.Application.Utilities;
using FluentAssertions;
using Xunit;

namespace BudgetBeavers.Application.Tests;

public class GuardTests
{
    [Fact]
    public void AgainstNull_ArgumentIsNull_ShouldThrowArgumentNullException()
    {
        // Arrange
        object? value = null;

        // Act
        var act = () => Guard.AgainstNull(value);

        // Assert
        act.Should()
            .Throw<ArgumentNullException>()
            .WithParameterName(nameof(value));
    }

    [Fact]
    public void AgainstNull_ArgumentIsNotNull_ShouldNotThrowException()
    {
        // Arrange
        var value = new object();

        // Act
        var act = () => Guard.AgainstNull(value);
        
        // Assert
        act.Should().NotThrow();
    }
    
    [Xunit.Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void AgainstNullOrWhiteSpace_InvalidValue_ThrowsArgumentException(string? value)
    {
        // Act
        var act = () => Guard.AgainstNullOrWhiteSpace(value);

        // Assert
        act.Should()
            .Throw<ArgumentException>()
            .Where(e => e.Message.Contains($"{nameof(value)} cannot be null or whitespace."))
            .WithParameterName(nameof(value));
    }

    [Fact]
    public void AgainstNullOrWhiteSpace_ValidValue_DoesNotThrow()
    {
        // Arrange
        const string validValue = "valid";
        
        // Act
        var act = () => Guard.AgainstNullOrWhiteSpace(validValue);

        // Assert
        act.Should().NotThrow();
    }
    
    [Fact]
    public void AgainstEmptyGuid_WhenGuidIsEmpty_ShouldThrowArgumentException()
    {
        // Arrange
        var emptyGuid = Guid.Empty;
        
        // Act
        var act = () => Guard.AgainstEmptyGuid(emptyGuid);

        // Assert
        act.Should()
            .Throw<ArgumentException>()
            .Where(e => e.Message.Contains($"{nameof(emptyGuid)} cannot be empty."))
            .WithParameterName(nameof(emptyGuid));
    }

    [Fact]
    public void AgainstEmptyGuid_WhenGuidIsNotEmpty_ShouldNotThrow()
    {
        // Arrange
        var validGuid = Guid.NewGuid();
        
        // Act
        var act = () => Guard.AgainstEmptyGuid(validGuid);

        // Assert
        act.Should().NotThrow();
    }
    
    [Fact]
    public void AgainstKeyNotFound_WhenObjectIsNull_ShouldThrowKeyNotFoundException()
    {
        // Arrange
        object? obj = null;
        var id = Guid.NewGuid();

        // Act
        var act = () => Guard.AgainstKeyNotFound(obj, id);

        act.Should()
            .Throw<KeyNotFoundException>()
            .WithMessage($"No entity found with the provided {nameof(id)}: {id}.");
    }

    [Fact]
    public void AgainstKeyNotFound_WhenObjectIsNotNull_ShouldNotThrow()
    {
        // Arrange
        var obj = new object();
        var id = Guid.NewGuid();

        // Act
        var act = () => Guard.AgainstKeyNotFound(obj, id);

        act.Should().NotThrow();
    }
}