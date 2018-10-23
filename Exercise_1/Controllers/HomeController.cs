﻿using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Exercise_1.Identity;
using Microsoft.AspNetCore.Mvc;
using Exercise_1.Models;
using Exercise_1.Models.SchoolViewModel;
using Microsoft.EntityFrameworkCore;

namespace Exercise_1.Controllers
{
    public class HomeController : Controller
    {
        private readonly EFCContext _context;

        public HomeController(EFCContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
       

        //public IActionResult About()
        //{
        //    ViewData["Message"] = "Your application description page.";

        //    return View();
        //}
        public async Task<ActionResult> About()
        {
            List<EnrollmentDateGroup> groups = new List<EnrollmentDateGroup>();
            var conn = _context.Database.GetDbConnection();
            try
            {
                await conn.OpenAsync();
                using (var command = conn.CreateCommand())
                {
                    string query = "SELECT EnrollmentDate, COUNT(*) AS StudentCount "
                                   + "FROM Person "
                                   + "WHERE Discriminator = 'Student' "
                                   + "GROUP BY EnrollmentDate";
                    command.CommandText = query;
                    DbDataReader reader = await command.ExecuteReaderAsync();

                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            var row = new EnrollmentDateGroup { EnrollmentDate = reader.GetDateTime(0), StudentCount = reader.GetInt32(1) };
                            groups.Add(row);
                        }
                    }
                    reader.Dispose();
                }
            }
            finally
            {
                conn.Close();
            }
            return View(groups);
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
