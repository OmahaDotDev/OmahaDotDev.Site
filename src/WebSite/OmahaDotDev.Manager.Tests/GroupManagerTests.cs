using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using OmahaDotDev.Manager.PublicContract;
using OmahaDotDev.WebSite.Data;
using System.Net;
using System.Text;

namespace OmahaDotDev.Manager.Tests
{
    //https://stackoverflow.com/questions/64856573/net-core-integration-tests-resolve-scoped-services
    //https://www.azureblue.io/asp-net-core-integration-tests-with-test-containers-and-postgres/

    public class GroupManagerTests : IAsyncLifetime, IDisposable, IClassFixture<IntegrationTestFixture>
    {
        private readonly WebSiteApplicationFactory _webSiteApplicationFactory;
        private readonly IServiceScope _scope;
        private readonly IdentityDbContext _appDb;
        private readonly IntegrationTestFixture _integrationTestFixture;
        public void Dispose()
        {
            _scope.Dispose();
            _appDb.Dispose();
            // _webSiteApplicationFactory.Dispose();
        }

        public Task DisposeAsync()
        {
            return Task.CompletedTask;
        }


        public GroupManagerTests(IntegrationTestFixture testFixture)
        {
            _webSiteApplicationFactory = testFixture.WebSiteApplicationFactory;
            _scope = _webSiteApplicationFactory.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
            _appDb = _scope.ServiceProvider.GetRequiredService<IdentityDbContext>();
            _integrationTestFixture = testFixture;

            //ensure db exists
            //reset db
            //create test data
        }

        [Fact]
        public async Task GET_Root_Responds_OK()
        {

            using var client = _webSiteApplicationFactory.CreateClient();
            using var response = await client.GetAsync("/");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("Hello Worldd!", await response.Content.ReadAsStringAsync());
        }

        [Fact]
        public async Task gg()
        {
            var userId = await _integrationTestFixture.CreateTestUser("test");
            _webSiteApplicationFactory.CurrentAmbientContext = new Model.Common.AmbientContext() { UserId = userId, IsLoggedIn = true };
            using var client = _webSiteApplicationFactory.CreateClient();

            var body = new ApiCreateGroupRequest("trest", new List<string>() { "test" });

            using var response = await client.PostAsync("/Groups", new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8,
            "application/json"));

            //application.Services.GetRequiredService();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);


            //Assert.Equal("Hello Worldd!", await response.Content.ReadAsStringAsync());
        }

        public async Task InitializeAsync()
        {
            await _integrationTestFixture.ResetDb();
        }
    }
}
