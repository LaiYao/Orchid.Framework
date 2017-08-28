using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace OMP.Website.Areas.Admin
{
    [Route("[area]/[controller]/[action]")]
    [Area("Admin")]
    public abstract class AdminBaseController : Controller
    {
    }
}
