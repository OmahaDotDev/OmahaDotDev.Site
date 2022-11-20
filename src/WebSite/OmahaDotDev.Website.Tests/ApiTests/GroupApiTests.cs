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
        result.IsSuccessStatusCode.Should().BeTrue();
        using var scope = IntegrationTestFixture.ScopeFactory.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<SiteDbContext>();
        db.Groups.FirstOrDefault(g => g.Id == resultBody!.Id).Should().BeEquivalentTo(new GroupRecord("Test Group")
        {
            CreatedByUserId = user,
            UpdatedByUserId = user,
            CreatedDate = IntegrationTestFixture.TimeUtility.GetCurrentSystemTime(),
            UpdatedDate = IntegrationTestFixture.TimeUtility.GetCurrentSystemTime(),
            Id = resultBody!.Id
        });
    }
}