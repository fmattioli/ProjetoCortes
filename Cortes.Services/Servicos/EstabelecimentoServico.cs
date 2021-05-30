using Cortes.Dominio.Entidades;
using Cortes.Repositorio.Interfaces.IEstabelecimentoRepositorio;
using Cortes.Services.Interfaces.EstabelecimentoServico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cortes.Services.Servicos
{
    public class EstabelecimentoServico : IEstabelecimentoServico
    {
        private readonly IEstabelecimentoRepositorio estabelecimentoRepositorio;
        public EstabelecimentoServico(IEstabelecimentoRepositorio estabelecimentoRepositorio)
        {
            this.estabelecimentoRepositorio = estabelecimentoRepositorio;
        }
        public async Task<bool> ExisteConfiguracao()
        {
            return await estabelecimentoRepositorio.ExisteConfiguracao(new Estabelecimento());
        }

        public async Task<bool> RealizarConfiguracao()
        {
            return await estabelecimentoRepositorio.RealizarConfiguracao(new Estabelecimento());
        }
    }
}
