using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace TestAuth.Filters
{
    public class GllabAuthenticationSchemeOptions
        : AuthenticationSchemeOptions
    { }
    public class GllabAuthenticationHandler: AuthenticationHandler<GllabAuthenticationSchemeOptions>
    {
        public static string SchemaName => "Gllab";
        protected  override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            
            var remoteUser = Context.Request.Headers["Remote_User"].ToString();
            if (string.IsNullOrEmpty(remoteUser))
            {
                Logger.Log(LogLevel.Error, "Нет Remote_User");
                return AuthenticateResult.Fail("No principal.");
            }
            
            var claims = new[] {
                new Claim(ClaimTypes.Name, remoteUser) 
            };
            var claimsIdentity = new ClaimsIdentity(claims, SchemaName);

            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            var ticket = new AuthenticationTicket(claimsPrincipal, null, SchemaName);
            return AuthenticateResult.Success(ticket);
        }

        public GllabAuthenticationHandler(IOptionsMonitor<GllabAuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) :
            base(options, logger, encoder, clock)
        {
        }
        
    }
}