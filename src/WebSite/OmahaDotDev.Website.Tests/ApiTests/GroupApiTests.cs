using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using OmahaDotDev.Manager.PublicContract.Group;
using OmahaDotDev.ResourceAccess.Database;
using OmahaDotDev.ResourceAccess.Database.Model;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace OmahaDotDev.Website.Tests.ApiTests;

public class GroupApiTests : IntegrationTestBase
{
    public GroupApiTests(IntegrationTestFixture integrationTestFixture) : base(integrationTestFixture)
    {
    }

    [Fact]
    public async Task CreateGroup_ShouldCreateGroup()
    {
        //arrange
        using var arrange = new Arrange(IntegrationTestFixture.ScopeFactory);
        var user = await arrange.CreateTestSiteAdminAsync();

        //act
        using var act = new Act(IntegrationTestFixture, user);
        var requestBody = new ApiCreateGroupRequest("Test Group", new List<string>() { "one" });
        var result = await act.AppClient.PostAsJsonAsync("/groups", requestBody);
        var resultBody = await result.Content.ReadFromJsonAsync<ApiGroupResponse>();

        //assert
        using var assert = new Assert(IntegrationTestFixture.ScopeFactory);
        result.IsSuccessStatusCode.Should().BeTrue();
        var expected = new GroupRecord("Test Group")
        {
            CreatedByUserId = user,
            UpdatedByUserId = user,
            CreatedDate = IntegrationTestFixture.TimeUtility.GetCurrentSystemTime(),
            UpdatedDate = IntegrationTestFixture.TimeUtility.GetCurrentSystemTime(),
            Id = resultBody!.Id
        };

        assert.SiteDb.Groups.FirstOrDefault(g => g.Id == resultBody!.Id).Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task CreateGroup_ShouldFail_WhenNotLoggedIn()
    {
        //act
        using var act = new Act(IntegrationTestFixture);
        var requestBody = new ApiCreateGroupRequest("Test Group", new List<string>() { "one" });
        var result = await act.AppClient.PostAsJsonAsync("/groups", requestBody);


        //assert
        using var assert = new Assert(IntegrationTestFixture.ScopeFactory);
        result.Should().HaveStatusCode(HttpStatusCode.Forbidden);

    }
}