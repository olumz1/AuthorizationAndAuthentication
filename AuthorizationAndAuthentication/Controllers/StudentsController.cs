﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthorizationAndAuthentication.Controllers
{
    [Authorize]
    public class StudentsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
