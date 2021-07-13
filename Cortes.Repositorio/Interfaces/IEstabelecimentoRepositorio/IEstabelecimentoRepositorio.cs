using Cortes.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cortes.Repositorio.Interfaces.IEstabelecimentoRepositorio
{
    public interface IEstabelecimentoRepositorio
    {
        Task ExisteConfiguracao();
        Task<bool> RealizarConfiguracao(Estabelecimento estabelecimento);
        Task<IList<Horario>> Horarios();
    }
}
