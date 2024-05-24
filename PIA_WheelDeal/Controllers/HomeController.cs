using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PIA_WheelDeal.Models;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

using PIA_WheelDeal.Models.dbModels;
using Microsoft.AspNetCore.Authorization;
namespace PIA_WheelDeal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
		private readonly BaseDeGatosContext _context;

		public HomeController(ILogger<HomeController> logger, BaseDeGatosContext context)
        {
			_logger = logger;
			_context = context;

		}

		// GET: Vehiculoes
		public async Task<IActionResult> IndexAsync()
		{
			var baseDeGatosContext = _context.Vehiculos.Include(v => v.IdTipoNavigation);
			return View(await baseDeGatosContext.ToListAsync());
		}

        //Vista sin restricciones
		public IActionResult Acercade()
        {
            return View();
        }

        // Vista exclusiva de acceso para el ADMIN
        [Authorize (Roles = "Admin")]
        public IActionResult Gerencia()
        {
            return View();
        }
		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}
