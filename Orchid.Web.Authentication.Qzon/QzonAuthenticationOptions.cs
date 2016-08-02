using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.OAuth;

namespace Orchid.Web.Authentication.Qzon
{
    public class QzonAuthenticationOptions
    {
        public QzonAuthenticationOptions()
        {
            
        }

        // access_type. Set to 'offline' to request a refresh token.
        public string AccessType { get; set; }
    }
}
