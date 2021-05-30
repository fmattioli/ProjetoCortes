using Cortes.Dominio.Entidades;
using Cortes.Repositorio.Interfaces.IAgendamentoRepositorio;
using Cortes.Services.Interfaces.AgendamentoServico;
using Cortes.Services.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cortes.Services.Servicos
{
    public class AgendamentoServico : IAgendamentoServico
    {
        private readonly IAgendamentoRepositorio agendamentoRepositorio;
        public AgendamentoServico(IAgendamentoRepositorio agendamentoRepositorio)
        {
            this.agendamentoRepositorio = agendamentoRepositorio;
        }

        public async Task<AgendamentoViewModel> CarregarDropDowns()
        {
            AgendamentoViewModel agendamento = new AgendamentoViewModel();
            await CarregarDropDownDiasSemana(agendamento);
            return agendamento;
        }

        private async Task CarregarDropDownDiasSemana(AgendamentoViewModel agendamento)
        {
            var grupos = await agendamentoRepositorio.DiasSemana(new DiasSemana());
            agendamento.Dias= new SelectList(grupos, "Codigo", "Dia", 0);
        }

    }
}
