using Microsoft.AspNetCore.Mvc;
using Site_SmartComfort.Models;
using System.Diagnostics;

namespace Site_SmartComfort.Controllers
{
    public class FavoritosController : Controller
    {
        public IActionResult Favoritos()
        {//retornando o repositorio com metodo todosClientes
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
