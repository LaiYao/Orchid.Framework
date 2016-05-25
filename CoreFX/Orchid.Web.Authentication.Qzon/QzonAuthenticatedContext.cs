using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;

namespace Orchid.Web.Authentication.Qzon
{
    public class QzonAuthenticatedContext : OAuthCreatingTicketContext
    {
        //   user:
        //     The JSON-serialized Qzon user info.
        //
        //   tokens:
        //     Qzon OAuth 2.0 access token, refresh token, etc.
        public QzonAuthenticatedContext(HttpContext context, OAuthAuthenticationOptions options, JObject user, OAuthTokenResponse tokens) 
            : base(context, options, user, tokens)
        {
        }

        public string Email { get; }
        public string FamilyName { get; set; }
        public string GivenName { get; set; }
        public string Id { get; }
        public string Name { get; }
        public string Profile { get; }
    }
}
