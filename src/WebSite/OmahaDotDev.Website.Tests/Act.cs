using Microsoft.AspNetCore.Mvc.Testing;
using OmahaDotDev.ResourceAccess.Database;

namespace OmahaDotDev.Website.Tests;

public class Act : IDisposable
{
    public HttpClient AppClient { get; internal set; }
    public Act(IntegrationTestFixture testFixture, string runAsUserId)
    {
        testFixture.RunAsUser(runAsUserId);
        AppClient = testFixture.AppFactory.CreateClient(new WebApplicationFactoryClientOptions()
        {
            AllowAutoRedirect = false
        });
    }

    public Act(IntegrationTestFixture testFixture)
    {

        AppClient = testFixture.AppFactory.CreateClient(new WebApplicationFactoryClientOptions()
        {
            AllowAutoRedirect = false
        });
    }

    public void Dispose()
    {
        AppClient.Dispose();
    }
}