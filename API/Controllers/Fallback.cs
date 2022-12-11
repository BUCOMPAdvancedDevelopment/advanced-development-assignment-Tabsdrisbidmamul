using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// Taken from: https://github.com/TryCatchLearn/Reactivities-v6/blob/main/API/Controllers/FallbackController.cs

namespace API.Controllers
{
    [AllowAnonymous]
    public class Fallback: ControllerBase
    { 
        /// <summary>
        /// Endpoint to return angular static files from the wwwroot
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
          return PhysicalFile(
            Path.Combine(
              Directory.GetCurrentDirectory(), "wwwroot", "index.html"),
             "text/HTML");
        }
    }
}