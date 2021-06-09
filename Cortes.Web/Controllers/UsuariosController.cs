using Cortes.Services.Interfaces.AgendamentoServico;
using Cortes.Services.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cortes.Web.Controllers
{
    public class UsuariosController : Controller
    {
        public async Task<IActionResult> Index(string Id, [FromServices] IAgendamentoServico agendamentoServico)
        {
            if (string.IsNullOrEmpty(Id))
                return RedirectToAction("Login", "Home");


            TempData["Id"] = Id;
            return View(await agendamentoServico.AgendamentosDiario());
        }

        public async Task<JsonResult> GraficosCortesDiario([FromServices] IAgendamentoServico agendamentoServico)
        {
            var resultado = await agendamentoServico.GraficosCortesDiarios();
            return Json(resultado);
        }

    }
}
