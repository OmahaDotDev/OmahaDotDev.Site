﻿using Microsoft.AspNetCore.Mvc.Testing;
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
    public async Task  Test1()
    {
        using var arrange = new Arrange(_integrationTestFixture.ScopeFactory);

        var client = _integrationTestFixture.AppFactory.CreateClient(new WebApplicationFactoryClientOptions()
        {
            AllowAutoRedirect = false
        });
        var result = await client.GetAsync("/helloworld" );

    }
}