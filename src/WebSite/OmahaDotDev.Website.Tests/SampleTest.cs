using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using OmahaDotDev.Manager.PublicContract;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace OmahaDotDev.Website.Tests;

public class SampleTest : IntegrationTestBase
{
    public SampleTest(IntegrationTestFixture integrationTestFixture) : base(integrationTestFixture)
    {
    }

    [Fact]
    public async Task Test1()
    {
        using var arrange = new Arrange(IntegrationTestFixture.ScopeFactory);
        var user = await arrange.CreateTestSiteAdminAsync();

        var client = IntegrationTestFixture.AppFactory.CreateClient(new WebApplicationFactoryClientOptions()
        {
            AllowAutoRedirect = false
        });
        IntegrationTestFixture.RunAsUser(user);

        var result = await client.GetAsync("/helloworld");

        result.IsSuccessStatusCode.Should().BeTrue();
    }

    [Fact]
    public async Task Test2()
    {
        var client = IntegrationTestFixture.AppFactory.CreateClient(new WebApplicationFactoryClientOptions()
        {
            AllowAutoRedirect = false
        });

        var result = await client.GetAsync("/helloworld");

        result.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }
    [Fact]
    public async Task Test3()
    {
        using var arrange = new Arrange(IntegrationTestFixture.ScopeFactory);
        var user = await arrange.CreateTestSiteAdminAsync();


        var client = IntegrationTestFixture.AppFactory.CreateClient(new WebApplicationFactoryClientOptions()
        {
            AllowAutoRedirect = false
        });
        IntegrationTestFixture.RunAsUser(user);

        var requestBody = new ApiCreateGroupRequest("Test Group", new List<string>() { "one" });
        var result = await client.PostAsJsonAsync("/groups", requestBody);

        result.IsSuccessStatusCode.Should().BeTrue();
    }
}