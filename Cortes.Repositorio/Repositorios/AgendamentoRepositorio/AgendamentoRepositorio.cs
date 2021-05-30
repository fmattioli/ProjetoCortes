using Cortes.Dominio.Entidades;
using Cortes.Infra.Comum;
using Cortes.Repositorio.Interfaces.IAgendamentoRepositorio;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cortes.Repositorio.Repositorios.AgendamentoRepositorio
{
    public class AgendamentoRepositorio : IAgendamentoRepositorio
    {
        private StringBuilder SQL = new StringBuilder();
        private Generico generico;
        public AgendamentoRepositorio(IConfiguration config)
        {
            generico = new Generico(config);
        }

        public async Task<IList<DiasSemana>> DiasSemana(DiasSemana dias)
        {
            IList<DiasSemana> diasSemana = new List<DiasSemana>();
            var dados = await generico.Select(await generico.MontarSelect<DiasSemana>(dias, null, false));
            diasSemana = (from DataRow dr in dados.Rows
                          select new DiasSemana()
                          {
                              Dia = dr["Dia"].ToString(),
                              Codigo = int.Parse(dr["Codigo"].ToString())
                          }).ToList();

            return diasSemana;
        }
    }
}
