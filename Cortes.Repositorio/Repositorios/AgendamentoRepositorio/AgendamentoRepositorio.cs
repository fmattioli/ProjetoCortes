using Cortes.Dominio.Entidades;
using Cortes.Infra.Comum;
using Cortes.Repositorio.Interfaces.IAgendamentoRepositorio;
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
        public async Task<IList<DiasSemana>> DiasSemana(DiasSemana dias)
        {
            IList<DiasSemana> diasSemana = new List<DiasSemana>();
            var dados = await generico.Select(await generico.MontarSelect<DiasSemana>(dias, null));
            diasSemana = (from DataRow dr in dados.Rows
                          select new DiasSemana()
                          {
                              SegundaFeira = dr["PK_Natureza"].ToString(),
                              TercaFeira = dr["Descricao"].ToString(),
                              QuartaFeira = dr["DiasRetorno"].ToString(),
                              QuintaFeira = dr["DiasRetorno"].ToString(),
                              SextaFeira = dr["DiasRetorno"].ToString(),
                              Sabado = dr["DiasRetorno"].ToString(),
                              Domingo = dr["DiasRetorno"].ToString()

                          }).ToList();

            return diasSemana;
        }
    }
}
