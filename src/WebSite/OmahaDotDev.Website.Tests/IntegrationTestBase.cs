using Xunit;

namespace OmahaDotDev.Website.Tests;

public class IntegrationTestBase : IClassFixture<IntegrationTestFixture>, IAsyncLifetime
{
    protected readonly IntegrationTestFixture IntegrationTestFixture;

    public IntegrationTestBase(IntegrationTestFixture integrationTestFixture)
    {
        IntegrationTestFixture = integrationTestFixture;
    }

    #region IAsyncLifetime
    public async Task InitializeAsync()
    {
        await IntegrationTestFixture.ResetSystemState();
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
        //throw new NotImplementedException();
    }
    #endregion
}