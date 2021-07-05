using Cortes.Dominio.Entidades;
using Cortes.Repositorio.Interfaces.IEstabelecimentoRepositorio;
using Cortes.Services.Interfaces.EstabelecimentoServico;
using Cortes.Services.ViewModels;
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
            return await estabelecimentoRepositorio.ExisteConfiguracao();
        }

        public async Task<bool> RealizarConfiguracao(EstabelecimentoViewModel estabelecimento) 
        {
            return await estabelecimentoRepositorio.RealizarConfiguracao(null);
        }
    }
}
