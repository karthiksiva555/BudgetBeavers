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
        const string parameterName = "testParameter";

        // Act
        Action act = () => Guard.AgainstNull(value, parameterName);

        // Assert
        act.Should()
            .Throw<ArgumentNullException>()
            .WithParameterName(parameterName);
    }

    [Fact]
    public void AgainstNull_ArgumentIsNotNull_ShouldNotThrowException()
    {
        // Arrange
        var value = new object();
        const string parameterName = "testParameter";

        // Act
        var act = () => Guard.AgainstNull(value, parameterName);
        
        // Assert
        act.Should().NotThrow();
    }
    
    [Xunit.Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void AgainstNullOrWhiteSpace_InvalidValue_ThrowsArgumentException(string? value)
    {
        // Arrange
        const string paramName = "testParam";
        
        // Act
        var act = () => Guard.AgainstNullOrWhiteSpace(value, paramName);

        // Assert
        act.Should()
            .Throw<ArgumentException>()
            .Where(e => e.Message.Contains($"{paramName} cannot be null or whitespace."))
            .WithParameterName(paramName);
    }

    [Fact]
    public void AgainstNullOrWhiteSpace_ValidValue_DoesNotThrow()
    {
        // Arrange
        const string validValue = "valid";
        const string paramName = "testParam";
        
        // Act
        var act = () => Guard.AgainstNullOrWhiteSpace(validValue, paramName);

        // Assert
        act.Should().NotThrow();
    }
    
    [Fact]
    public void AgainstEmptyGuid_WhenGuidIsEmpty_ShouldThrowArgumentException()
    {
        // Arrange
        var emptyGuid = Guid.Empty;
        const string paramName = "testId";
        
        // Act
        var act = () => Guard.AgainstEmptyGuid(emptyGuid, paramName);

        // Assert
        act.Should()
            .Throw<ArgumentException>()
            .Where(e => e.Message.Contains($"{paramName} cannot be empty."))
            .WithParameterName(paramName);
    }

    [Fact]
    public void AgainstEmptyGuid_WhenGuidIsNotEmpty_ShouldNotThrow()
    {
        // Arrange
        var validGuid = Guid.NewGuid();
        const string paramName = "testId";
        
        // Act
        var act = () => Guard.AgainstEmptyGuid(validGuid, paramName);

        // Assert
        act.Should().NotThrow();
    }
    
    [Fact]
    public void AgainstKeyNotFound_WhenObjectIsNull_ShouldThrowKeyNotFoundException()
    {
        // Arrange
        object? obj = null;
        var id = Guid.NewGuid();
        const string keyName = "TestKey";

        // Act
        var act = () => Guard.AgainstKeyNotFound(obj, id, keyName);

        act.Should()
            .Throw<KeyNotFoundException>()
            .WithMessage($"No entity found with the provided {keyName}: {id}.");
    }

    [Fact]
    public void AgainstKeyNotFound_WhenObjectIsNotNull_ShouldNotThrow()
    {
        // Arrange
        var obj = new object();
        var id = Guid.NewGuid();
        const string keyName = "TestKey";

        // Act
        var act = () => Guard.AgainstKeyNotFound(obj, id, keyName);

        act.Should().NotThrow();
    }
}