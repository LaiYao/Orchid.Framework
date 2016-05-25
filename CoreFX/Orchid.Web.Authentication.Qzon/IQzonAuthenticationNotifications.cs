using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.OAuth;

namespace Orchid.Web.Authentication.Qzon
{
    public interface IQzonAuthenticationNotifications: IOAuthEvents
    {
    }
}
