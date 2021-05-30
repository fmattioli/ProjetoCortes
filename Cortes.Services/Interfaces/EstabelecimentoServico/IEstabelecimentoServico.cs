using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cortes.Services.Interfaces.EstabelecimentoServico
{
    public interface IEstabelecimentoServico
    {
        Task<bool> ExisteConfiguracao();
        Task<bool> RealizarConfiguracao();
    }
}
