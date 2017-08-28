using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Orchid.Core.Utilities;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OMP.Website.Areas.Admin.Controllers
{
    public class LoadBalanceController : Controller
    {
        #region | Fields |

        IStringLocalizer<LoadBalanceController> _localizer;

        #endregion

        #region | Ctor |

        public LoadBalanceController(IStringLocalizer<LoadBalanceController> localizer)
        {
            Check.NotNull(localizer, nameof(localizer));

            _localizer = localizer;
        }

        #endregion

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
    }
}
