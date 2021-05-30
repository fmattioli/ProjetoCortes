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
        Task<bool> ExisteConfiguracao(Estabelecimento estabelecimento);
        Task<bool> RealizarConfiguracao(Estabelecimento estabelecimento);
    }
}
