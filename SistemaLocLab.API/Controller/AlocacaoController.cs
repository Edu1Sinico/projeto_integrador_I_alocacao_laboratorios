using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace SistemaLocLab.API.Controller
{
    [Route("[controller]")]
    public class AlocacaoController : Controller
    {
        private readonly ILogger<AlocacaoController> _logger;

        public AlocacaoController(ILogger<AlocacaoController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}