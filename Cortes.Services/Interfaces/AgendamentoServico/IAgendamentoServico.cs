using Cortes.Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cortes.Services.Interfaces.AgendamentoServico
{
    public interface IAgendamentoServico
    {
        public Task<AgendamentoViewModel> CarregarDropDowns();
        public Task<bool> ConfirmarAgendamento(AgendamentoViewModel agendamentoModel);
        public Task<bool> RealizarLancamento(AgendamentoViewModel agendamentoModel);
        public Task<bool> ValidarAgendamento(AgendamentoViewModel model);
        public Task<bool> CompareceuAgendamento(string Id, int compareceu);
        public Task<IList<AgendamentoViewModel>> AgendamentosDiario();
        public Task<GraficoCortesViewModel> GraficosCortesDiarios();

    }
}
