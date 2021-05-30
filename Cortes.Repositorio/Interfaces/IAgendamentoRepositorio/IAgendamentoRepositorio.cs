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
        Task<bool> ConfirmarAgendamento(Agendamento agendamento);
    }
}
