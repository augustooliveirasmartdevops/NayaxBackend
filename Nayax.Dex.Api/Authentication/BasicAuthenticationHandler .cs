using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Nayax.Dex.Application.Interfaces;

namespace Nayax.Dex.Api.Authentication
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IAuthApplication _authApplication;

        public BasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            IAuthApplication authApplication)
            : base(options, logger, encoder)
        {
            _authApplication = authApplication;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.TryGetValue("Authorization", out StringValues value))
            {
                return AuthenticateResult.Fail("Missing Authorization header");
            }

            if (string.IsNullOrWhiteSpace(value.ToString()))
            {
                return AuthenticateResult.Fail("Empty Authorization header");
            }

            try
            {
                var authHeader = AuthenticationHeaderValue.Parse(value.ToString());
                var credentialBytes = Convert.FromBase64String(authHeader.Parameter ?? string.Empty);
                var credentials = Encoding.UTF8.GetString(credentialBytes).Split(':', 2);
                if (credentials.Length != 2)
                {
                    return AuthenticateResult.Fail("Invalid credentials format");
                }

                var userName = credentials[0];
                var password = credentials[1];

                var isValid = await _authApplication.ValidateCredentialsAsync(userName, password);
                if (!isValid)
                {
                    return AuthenticateResult.Fail("Invalid username or password");
                }

                var claims = new[] {
                    new Claim(ClaimTypes.NameIdentifier, userName),
                    new Claim(ClaimTypes.Name, userName)
                };

                var identity = new ClaimsIdentity(claims, Scheme.Name);
                var principal = new ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(principal, Scheme.Name);

                return AuthenticateResult.Success(ticket);
            }
            catch
            {
                return AuthenticateResult.Fail("Invalid Authorization header");
            }
        }
    }
}
