using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Exercise_1.Controllers
{
    [Authorize]
    public class SecretController : Controller
    {
        public IActionResult Index()
        {
            return Content("Secret Information");
        }

        public IActionResult Secret()
        {
            return Content("Secret Information");
        }

        public IActionResult Public()
        {
            return Content("Public Information");
        }
    }
}