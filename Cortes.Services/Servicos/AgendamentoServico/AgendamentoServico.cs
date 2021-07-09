using Cortes.Dominio.Entidades;
using Cortes.Repositorio.Interfaces.IAgendamentoRepositorio;
using Cortes.Services.Interfaces.AgendamentoServico;
using Cortes.Services.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
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

        public async Task<IList<AgendamentoViewModel>> AgendamentosDiario()
        {
            var listaAgendamento = new List<AgendamentoViewModel>();
            
            foreach (var item in await agendamentoRepositorio.AgendamentosDiario())
            {
                listaAgendamento.Add(new AgendamentoViewModel()
                {
                    Id = item.Id.ToString(),
                    Nome = item.Nome,
                    HorarioSelecionado = item.Horario,
                    Preco = item.Preco
                });
            }
            return listaAgendamento;
        }

        public async Task<AgendamentoViewModel> CarregarDropDowns()
        {
            AgendamentoViewModel agendamento = new AgendamentoViewModel();
            await CarregarDropDownDiasSemana(agendamento);
            await CarregarDropDownHorarios(agendamento);
            return agendamento;
        }

        public async Task<bool> CompareceuAgendamento(string Id, int compareceu)
        {
            return await agendamentoRepositorio.CompareceuAgendamento(Id, compareceu);
        }

        public async Task<bool> ConfirmarAgendamento(AgendamentoViewModel model)
        {
            Agendamento agendamento = new Agendamento
            {
                Endereco = model.Endereco,
                Horario = model.HorarioSelecionado,
                Usuario_Id = model.Usuario_Id,
                Nome = model.Nome,
                Preco = model.Preco,
                DataCorte = DateTime.Now,
                Compareceu = 0
            };

            return await agendamentoRepositorio.ConfirmarAgendamento(agendamento);
        }

        public async Task<GraficoCortesViewModel> GraficosCortesDiarios()
        {
            var resultado = await agendamentoRepositorio.PreencherGraficos();
            return new GraficoCortesViewModel
            {
                CortesAbertos = resultado.CortesAbertos,
                CortesFinalizados = resultado.CortesFinalizados,
                CortesCancelados = resultado.CortesCancelados
            };
        }

        public async Task<bool> RealizarLancamento(AgendamentoViewModel agendamentoModel)
        {
            Agendamento agendamento = new Agendamento
            {
                Endereco = agendamentoModel.Endereco,
                Horario = agendamentoModel.HorarioSelecionado,
                Usuario_Id = agendamentoModel.Usuario_Id,
                Nome = agendamentoModel.Nome,
                Preco = agendamentoModel.Preco,
                DataCorte = DateTime.Now,
                Compareceu = 3
            };

            return await agendamentoRepositorio.LancarAgendamento(agendamento);
        }

        public async Task<bool> ValidarAgendamento(AgendamentoViewModel model)
        {
            Agendamento agendamento = new Agendamento
            {
                Endereco = model.Endereco,
                Horario = model.HorarioSelecionado,
                Usuario_Id = model.Usuario_Id,
                Nome = model.Nome,
                Preco = model.Preco
            };
            return await agendamentoRepositorio.ValidarAgendamento(agendamento);
        }

        private async Task CarregarDropDownDiasSemana(AgendamentoViewModel agendamento)
        {
            var dias = await agendamentoRepositorio.DiasSemana(new DiasSemana());
            agendamento.Dias= new SelectList(dias, "Codigo", "Dia", 0);
        }

        private async Task CarregarDropDownHorarios(AgendamentoViewModel agendamento)
        {
            var horarios = await agendamentoRepositorio.Horarios();
            agendamento.Horarios = new SelectList(horarios, "Codigo", "Hora", 0);
        }

    }
}
