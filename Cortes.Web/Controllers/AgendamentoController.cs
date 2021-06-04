using Cortes.Repositorio.Interfaces.IAgendamentoRepositorio;
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
    public class AgendamentoController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Agendamento(string Id, [FromServices] IAgendamentoServico agendamentoServico)
        {
            TempData["Id"] = Id;
            var model = await agendamentoServico.CarregarDropDowns();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> VerificarAgendamento(string Id, [FromServices] IAgendamentoServico agendamentoServico)
        {
            TempData["Id"] = Id;
            var model = await agendamentoServico.CarregarDropDowns();
            return View(model);
        }

        [Authorize]
        public async Task<JsonResult> RealizarAgendamento(string model, [FromServices] IAgendamentoServico agendamentoServico)
        {
            AgendamentoViewModel agendamentoModel  = JsonConvert.DeserializeObject<AgendamentoViewModel>(model);
            var retorno = await agendamentoServico.ConfirmarAgendamento(agendamentoModel);
            return Json(retorno);
        }

        [Authorize]
        public async Task<JsonResult> VerificarSeExisteAgendamento(string model, [FromServices] IAgendamentoServico agendamentoServico)
        {
            AgendamentoViewModel agendamentoModel = JsonConvert.DeserializeObject<AgendamentoViewModel>(model);
            var retorno = await agendamentoServico.ValidarAgendamento(agendamentoModel);
            return Json(retorno);
        }


    }
}
