using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BuberBreakfast.Controllers
{
    [Route("[controller]")]
    public class ErrorsController : ControllerBase
    { 
        [Route("/error")]
        public IActionResult Error()
        {
            return Problem();
        }
    }
}