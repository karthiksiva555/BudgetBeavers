using AutoFixture;
using AutoFixture.AutoMoq;

namespace BudgetBeavers.Application.Tests;

public abstract class TestBase
{
    protected readonly IFixture Fixture;
    
    protected TestBase()
    {
        Fixture = new Fixture();
        Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        Fixture.Customize(new AutoMoqCustomization());
    }
}