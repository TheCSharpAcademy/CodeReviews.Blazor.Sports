namespace SportsStatistics.ArchitectureTests.Layers;

public class ApplicationLayerTests : BaseTest
{
    [Fact]
    public void Should_NotHaveDependencyOn_InfrastructureLayer()
    {
        TestResult result = Types.InAssembly(ApplicationAssembly)
                                 .Should()
                                 .NotHaveDependencyOn(InfrastructureAssembly.GetName().Name)
                                 .GetResult();

        result.IsSuccessful.ShouldBeTrue();
    }

    [Fact]
    public void Should_NotHaveDependencyOn_PresentationLayer()
    {
        TestResult result = Types.InAssembly(ApplicationAssembly)
                                 .Should()
                                 .NotHaveDependencyOn(PresentationAssembly.GetName().Name)
                                 .GetResult();

        result.IsSuccessful.ShouldBeTrue();
    }
}
