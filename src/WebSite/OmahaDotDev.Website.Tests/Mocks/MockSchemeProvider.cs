using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace OmahaDotDev.Website.Tests.Mocks;

public class MockSchemeProvider : AuthenticationSchemeProvider
{
    public MockSchemeProvider(IOptions<AuthenticationOptions> options) : base(options)
    {
    }
    protected MockSchemeProvider(
        IOptions<AuthenticationOptions> options,
        IDictionary<string, AuthenticationScheme> schemes
    )
        : base(options, schemes)
    {
    }

    public override Task<AuthenticationScheme?> GetSchemeAsync(string name)
    {
        AuthenticationScheme mockScheme = new(
            IdentityConstants.ApplicationScheme,
            IdentityConstants.ApplicationScheme,
            typeof(MockAuthenticationHandler)
        );
        return Task.FromResult(mockScheme)!;
    }
}

public class MockAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly MockClaims _claimSeed;

    public MockAuthenticationHandler(
        MockClaims claimSeed,
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock
    )
        : base(options, logger, encoder, clock)
    {
        _claimSeed = claimSeed;
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var claimsIdentity = new ClaimsIdentity(_claimSeed.GetSeeds(), IdentityConstants.ApplicationScheme);
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
        var ticket = new AuthenticationTicket(claimsPrincipal, IdentityConstants.ApplicationScheme);
        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}

public class MockClaims
{
    private readonly IEnumerable<Claim> _seed;

    public MockClaims(IEnumerable<Claim> seed)
    {
        _seed = seed;
    }

    public IEnumerable<Claim> GetSeeds() => _seed;
}
