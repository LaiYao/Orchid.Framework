using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Framework.Internal;
using Orchid.Web.Authentication.Qzon;

namespace Microsoft.AspNet.Builder
{
    public static class QzonAppBuilderExtensions
    {
        public static IApplicationBuilder UseQzonAuthentication([NotNull] this IApplicationBuilder app, Action<QzonAuthenticationOptions> configureOptions = null, string optionsName = "")
        {
            // TODO
            return app;
        }
    }
}
