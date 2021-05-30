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

        public Task<bool> ConfirmarAgendamento(Agendamento agendamento)
        {
            throw new NotImplementedException();
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

        public async Task<IList<Horario>> Horarios()
        {
            IList<Horario> horariosDisponiveis = new List<Horario>();
            var dados = await generico.Select(await generico.MontarSelect<Estabelecimento>(new Estabelecimento(), null, false));
            if (dados?.Rows.Count >= 1)
            {
                string horarioAbertura = dados.Rows[0]["HoraAbertura"].ToString();
                string horarioFechamento = dados.Rows[0]["HoraFechamento"].ToString();
                int codigo = 1;
                horariosDisponiveis.Add(new Horario
                {
                    Codigo = codigo,
                    Hora = horarioAbertura
                });

                int Abertura = int.Parse(horarioAbertura.Replace(":", ""));
                int Fechamento = int.Parse(horarioFechamento.Replace(":", ""));

                while (Abertura < Fechamento)
                {
                    codigo++;
                    var result = Convert.ToDateTime(horariosDisponiveis[horariosDisponiveis.Count - 1].Hora).AddMinutes(30).ToString("HH:mm");
                    horariosDisponiveis.Add(new Horario
                    {
                        Codigo = codigo,
                        Hora = result
                    });

                    Abertura = int.Parse(result.Replace(":", ""));
                }

                horariosDisponiveis.Add(new Horario
                {
                    Codigo = codigo++,
                    Hora = horarioFechamento.ToString()
                });
            }
            return horariosDisponiveis;
        }
    }
}
