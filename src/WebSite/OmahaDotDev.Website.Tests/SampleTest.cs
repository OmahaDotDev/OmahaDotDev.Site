using Xunit;

namespace OmahaDotDev.Website.Tests;

public class SampleTest : IClassFixture<IntegrationTestFixture>
{
    private readonly IntegrationTestFixture _integrationTestFixture;

    public SampleTest(IntegrationTestFixture integrationTestFixture)
    {
        _integrationTestFixture = integrationTestFixture;
    }
    [Fact]
    public void Test1()
    {
        using var arrange = new Arrange(_integrationTestFixture.ScopeFactory);


    }
}