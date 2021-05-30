using Cortes.Services.Interfaces;
using Cortes.Services.Interfaces.EstabelecimentoServico;
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
        public async Task<IActionResult> Login(LoginViewModel model, [FromServices] IUsuarioServico usuarioServices, [FromServices] IEstabelecimentoServico estabelecimentoServico)
        {
            if (ModelState.IsValid)
            {
                var usuario = await usuarioServices.ObterUsuario(model);
                if(usuario != null)
                {
                    //Validar se existe configurações necessárias!
                    if (!await estabelecimentoServico.ExisteConfiguracao())
                    {
                        //Configurações iniciais
                        await estabelecimentoServico.RealizarConfiguracao();
                    }
                    TempData["Token"] = usuario.Token;
                    TempData["Nome"] = usuario.Nome;
                    TempData["Id"] = usuario.Id;
                    ViewBag.Id = usuario.Id;
                    return RedirectToAction("Index", "Usuarios", new { Id = usuario.Id});
                }

                ModelState.AddModelError("Email", "E-mail ou senha inválidos");
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
