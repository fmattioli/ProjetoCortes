using Cortes.Repositorio.Interfaces.IUsuarioRepositorio;
using Cortes.Repositorio.Repositorios.UsuarioRepositorio;
using Cortes.Web.Models;
using Cortes.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Cortes.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                return View();
            }

            return View(model);
        }

        public IActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registro(RegistroViewModel model)
        {
            if (ModelState.IsValid)
            {
                return View();
            }
            return View(model);
        }

    }
}
