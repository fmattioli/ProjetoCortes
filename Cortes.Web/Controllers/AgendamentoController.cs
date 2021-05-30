using Cortes.Repositorio.Interfaces.IAgendamentoRepositorio;
using Cortes.Services.Interfaces.AgendamentoServico;
using Cortes.Services.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cortes.Web.Controllers
{
    public class AgendamentoController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Agendamento(string Id, [FromServices] IAgendamentoServico agendamentoServico)
        {
            TempData["Id"] = Id;
            var model = await agendamentoServico.CarregarDropDowns();
            return View(model);
        }

        [HttpPost]
        public IActionResult Agendamento(AgendamentoViewModel model)
        {
            return View();
        }

        

    }
}
