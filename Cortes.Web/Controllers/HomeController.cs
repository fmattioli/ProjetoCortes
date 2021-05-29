using Cortes.Services.Interfaces;
using Cortes.Services.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
        public async Task<IActionResult> Login(LoginViewModel model, [FromServices] IUsuarioServico usuarioServices)
        {
            if (ModelState.IsValid)
            {
                var usuario = await usuarioServices.ObterUsuario(model);
                if(usuario != null)
                {
                    TempData["Token"] = usuario.Token;
                    TempData["Nome"] = usuario.Nome;
                    return RedirectToAction("Index", "Usuarios");
                }

                ModelState.AddModelError("", "E-mail ou senha inválidos");
                return View(model);
            }

            return View(model);
        }

        public IActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registro(RegistroViewModel model, [FromServices] IUsuarioServico usuarioServices)
        {
            if (ModelState.IsValid)
            {
                TempData["jsonUsuario"] = await usuarioServices.CriarUsuario(model);
                return RedirectToAction("Index", "Usuarios");
            }
            return View(model);
        }

    }
}
