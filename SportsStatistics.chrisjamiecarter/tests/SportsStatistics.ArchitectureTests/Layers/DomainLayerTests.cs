namespace SportsStatistics.ArchitectureTests.Layers;

public class DomainLayerTests : BaseTest
{
    [Fact]
    public void Should_NotHaveDependencyOn_ApplicationLayer()
    {
        TestResult result = Types.InAssembly(DomainAssembly)
                                 .Should()
                                 .NotHaveDependencyOn(ApplicationAssembly.GetName().Name)
                                 .GetResult();

        result.IsSuccessful.ShouldBeTrue();
    }

    [Fact]
    public void Should_NotHaveDependencyOn_InfrastructureLayer()
    {
        TestResult result = Types.InAssembly(DomainAssembly)
                                 .Should()
                                 .NotHaveDependencyOn(InfrastructureAssembly.GetName().Name)
                                 .GetResult();

        result.IsSuccessful.ShouldBeTrue();
    }

    [Fact]
    public void Should_NotHaveDependencyOn_PresentationLayer()
    {
        TestResult result = Types.InAssembly(DomainAssembly)
                                 .Should()
                                 .NotHaveDependencyOn(PresentationAssembly.GetName().Name)
                                 .GetResult();

        result.IsSuccessful.ShouldBeTrue();
    }
}
