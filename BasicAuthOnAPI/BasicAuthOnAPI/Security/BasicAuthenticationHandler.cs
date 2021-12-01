using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace BasicAuthOnAPI.Security
{
    public class BasicAuthenticationHandler : Microsoft.AspNetCore.Authentication.AuthenticationHandler<BasicAuthenticationOption>
    {
        public BasicAuthenticationHandler(IOptionsMonitor<BasicAuthenticationOption> options, ILoggerFactory logger, UrlEncoder urlEncoder, ISystemClock systemClock):base(options, logger,urlEncoder,systemClock)
        {

        }
        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            //1. Gelen request'in header'ının içinde "Authorization" var mı?
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return Task.FromResult(AuthenticateResult.NoResult());

            }

            //2. Gelen request'in header'ının içinde "Authorization" değeri standart formata dönüştürülüyor mu?
            if (!AuthenticationHeaderValue.TryParse(Request.Headers["Authorization"], out AuthenticationHeaderValue headerValue))
            {
                return Task.FromResult(AuthenticateResult.NoResult());
            }

            //3. headerValue değeri sizin aradığınız şey mi?
            if (!headerValue.Scheme.Equals("Basic", StringComparison.OrdinalIgnoreCase))
            {
                return Task.FromResult(AuthenticateResult.NoResult());
            }

            //Authorization: "Basic HKJq110mmabU==" 
            //Authorization: "Basic turkay:123" 

            byte[] incomingData = Convert.FromBase64String(headerValue.Parameter);
            string userCredential = Encoding.UTF8.GetString(incomingData);
            string userName = userCredential.Split(':')[0];
            string password = userCredential.Split(':')[1];

            if (!(userName=="turkay" && password == "123"))
            {
                return Task.FromResult(AuthenticateResult.Fail(new Exception("Hatalı giriş")));
            }

            Claim[] claims = new Claim[] { new Claim(ClaimTypes.Name, "Android1") };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, Scheme.Name);
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            AuthenticationTicket ticket = new AuthenticationTicket(claimsPrincipal, Scheme.Name);
            return Task.FromResult(AuthenticateResult.Success(ticket));



        }
    }
}
