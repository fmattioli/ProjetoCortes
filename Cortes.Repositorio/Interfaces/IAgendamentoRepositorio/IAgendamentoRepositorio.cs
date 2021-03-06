using Cortes.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cortes.Repositorio.Interfaces.IAgendamentoRepositorio
{
    public interface IAgendamentoRepositorio
    {
        Task<IList<DiasSemana>> DiasSemana(DiasSemana dias);
        Task<IList<Horario>> Horarios();
        Task<IList<Agendamento>> AgendamentosDiario();
        Task<bool> ConfirmarAgendamento(Agendamento agendamento);
        Task<bool> LancarAgendamento(Agendamento agendamento);
        Task<bool> CompareceuAgendamento(string Id, int compareceu);
        Task<bool> ValidarAgendamento(Agendamento agendamento);
        string RetornarDiaDaSemanaCodigo();
        Task<GraficoCortes> PreencherGraficos();
        
    }
}
