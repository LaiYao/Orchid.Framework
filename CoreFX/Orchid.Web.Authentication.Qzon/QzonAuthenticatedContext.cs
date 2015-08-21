using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orchid.Web.Authentication.Qzon
{
    public class QzonAuthenticatedContext: OAuthAuthenticatedContext
    {
        //   user:
        //     The JSON-serialized Qzon user info.
        //
        //   tokens:
        //     Qzon OAuth 2.0 access token, refresh token, etc.
        public QzonAuthenticatedContext(HttpContext context, OAuthAuthenticationOptions options, JObject user, TokenResponse tokens)
        { }

        public string Email { get; }
        public string FamilyName { get; set; }
        public string GivenName { get; set; }
        public string Id { get; }
        public string Name { get; }
        public string Profile { get; }
    }
}
