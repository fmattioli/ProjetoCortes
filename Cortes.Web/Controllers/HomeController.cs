using Cortes.Services.Interfaces;
using Cortes.Services.ViewModels;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> Registro(RegistroViewModel model, [FromServices] IUsuarioServico usuarioServices)
        {
            if (ModelState.IsValid)
            {
                var resultado = await usuarioServices.CriarUsuario(model);
                return View();
            }
            return View(model);
        }

    }
}
