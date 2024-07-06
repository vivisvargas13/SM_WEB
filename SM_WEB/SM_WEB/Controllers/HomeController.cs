using Microsoft.AspNetCore.Mvc;
using SM_WEB.Entities;
using SM_WEB.Models;
using System.Diagnostics;

namespace SM_WEB.Controllers
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class HomeController(IUsuarioModel iUsuarioModel) : Controller
    {
        //abrir vistas y son llamados por un hipervinculo
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        //realizar las acciones de las vistas que abrimos
        [HttpPost]
        public IActionResult Index(Usuario usuario)
        {
            var respuesta = iUsuarioModel.IniciarSesion(usuario);

            if (respuesta.Codigo == 1)
                return RedirectToAction("Principal","Home");
                else
                ViewBag.msj =   respuesta.Mensaje;

            return View();
        }
        [HttpGet]
        public IActionResult Principal()
        {
            return View();
        }

    }
}
