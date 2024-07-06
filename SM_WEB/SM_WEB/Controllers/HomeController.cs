using Microsoft.AspNetCore.Mvc;
using SM_WEB.Models;
using System.Diagnostics;

namespace SM_WEB.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            IUsuarioModel model = new IUsuarioModel();
            model.IniciarSesion();

            return View(model);
        }

        public IActionResult Privacy()
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
