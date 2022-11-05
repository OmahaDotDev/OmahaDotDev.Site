using System.Net;

namespace OmahaDotDev.Manager.Tests
{
    public class GroupManagerTests
    {
        [Fact]
        public async Task GET_Root_Responds_OK()
        {
            await using var application = new TestApplication();

            using var client = application.CreateClient();
            using var response = await client.GetAsync("/");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("Hello Worldd!", await response.Content.ReadAsStringAsync());
        }
    }
}
