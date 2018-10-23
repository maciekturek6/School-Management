using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Exercise_1.Controllers
{
    [Authorize]
    public class CalendarsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}