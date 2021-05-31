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
    }
}
