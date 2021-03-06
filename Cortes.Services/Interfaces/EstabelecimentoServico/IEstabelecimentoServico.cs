using Cortes.Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cortes.Services.Interfaces.EstabelecimentoServico
{
    public interface IEstabelecimentoServico
    {
        Task ExisteConfiguracao();
        Task<bool> RealizarConfiguracao(EstabelecimentoViewModel estabelecimento);
    }
}
