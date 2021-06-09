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

        public async Task<bool> ConfirmarAgendamento(Agendamento agendamento)
        {
            if (await ValidarAgendamento(agendamento))
            {
                //Configurar Cláusula WHERE
                var listWhere = new List<(bool isInt, string nome, string valor)>();
                listWhere.Add((true, nameof(agendamento.Codigo), agendamento.Codigo.ToString()));

                //Obter Id
                var dt = await generico.Select(await generico.MontarSelectGetId("DiasSemana", listWhere));
                agendamento.DiaSemana_Id = dt.Rows[0]["Id"].ToString();

                //Marcar corte
                List<string> listaDesconsiderar = new List<string>();
                listaDesconsiderar.Add("Codigo");
                await generico.RunSQLCommand(await generico.MontarInsert<Agendamento>(agendamento, listaDesconsiderar, true));
                return true;
            }
            return false;
        }

        public async Task<bool> LancarAgendamento(Agendamento agendamento)
        {
            try
            {
                //Configurar Cláusula WHERE
                var listWhere = new List<(bool isInt, string nome, string valor)>();
                listWhere.Add((true, nameof(agendamento.Codigo), agendamento.Codigo.ToString()));

                //Obter Id
                var dt = await generico.Select(await generico.MontarSelectGetId("DiasSemana", listWhere));
                agendamento.DiaSemana_Id = dt.Rows[0]["Id"].ToString();

                //Marcar corte
                List<string> listaDesconsiderar = new List<string>();
                listaDesconsiderar.Add("Codigo");
                await generico.RunSQLCommand(await generico.MontarInsert<Agendamento>(agendamento, listaDesconsiderar, true));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> ValidarAgendamento(Agendamento agendamento)
        {
            var listaWhere = new List<(bool isInt, string nome, string valor)>();
            var listaJoin = new List<(string, string, string)>();
            var listaSelect = new List<(bool isInt, string nome)>();

            listaWhere.Add((false, nameof(agendamento.Horario), agendamento.Horario));
            listaWhere.Add((true, nameof(agendamento.Codigo), agendamento.Codigo.ToString()));

            listaSelect.Add((false, nameof(agendamento.Horario)));
            listaSelect.Add((true, nameof(agendamento.Codigo)));
            listaJoin.Add(("DiasSemana", "Agendamentos", "DiaSemana_Id"));

            var dados = await generico.Select(await generico.MontarSelectWithJoin(nameof(agendamento), listaJoin, listaSelect, listaWhere));
            if (dados?.Rows?.Count >= 1)
                return false;
            return true;

        }

        public async Task<IList<DiasSemana>> DiasSemana(DiasSemana dias)
        {
            IList<DiasSemana> diasSemana = new List<DiasSemana>();
            var dados = await generico.Select(await generico.MontarSelectObjeto<DiasSemana>(dias, null, false));
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
            var dados = await generico.Select(await generico.MontarSelectObjeto<Estabelecimento>(new Estabelecimento(), null, false));
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

        public async Task<IList<Agendamento>> AgendamentosDiario()
        {
            try
            {
                IList<Agendamento> listaAgendamento = new List<Agendamento>();
                Agendamento agendamento = new Agendamento();
                var listaSelect = new List<(bool isInt, string nome)>();
                var listaWhere = new List<(bool isInt, string nome, string valor)>();
                var listaJoin = new List<(string, string, string)>();

                listaSelect.Add((false, nameof(agendamento.Horario)));
                listaSelect.Add((true, nameof(agendamento.Preco)));
                listaSelect.Add((true, nameof(agendamento.Nome)));
                listaSelect.Add((true, "Agendamentos.Id"));

                listaJoin.Add(("DiasSemana", "Agendamentos", "DiaSemana_Id"));
                listaWhere.Add((true, nameof(agendamento.Codigo), RetornarDiaDaSemanaCodigo(DateTime.Now.DayOfWeek)));
                listaWhere.Add((true, nameof(agendamento.Compareceu), "0"));

                var dados = await generico.Select(await generico.MontarSelectWithJoin(nameof(agendamento), listaJoin, listaSelect, listaWhere, true));
                listaAgendamento = (from DataRow dr in dados.Rows
                                    select new Agendamento()
                                    {
                                        Nome = dr["Nome"].ToString(),
                                        Horario = dr["Horario"].ToString(),
                                        Preco = decimal.Parse(dr["Preco"].ToString()),
                                        Id = dr["Id"].ToString()
                                    }).ToList();

                return listaAgendamento;
            }
            catch
            {
                throw;
            }
        }

        public string RetornarDiaDaSemanaCodigo(DayOfWeek dayOfWeek)
        {
            switch (dayOfWeek)
            {
                case DayOfWeek.Sunday:
                    return "7";
                case DayOfWeek.Monday:
                    return "1";
                case DayOfWeek.Tuesday:
                    return "2";
                case DayOfWeek.Wednesday:
                    return "3";
                case DayOfWeek.Thursday:
                    return "4";
                case DayOfWeek.Friday:
                    return "5";
                case DayOfWeek.Saturday:
                    return "6";
                default:
                    return "";
            }
        }

        public async Task<bool> CompareceuAgendamento(string Id, int compareceu)
        {
            try
            {
                var listaUpdate = new List<(bool isInt, string campo, string valor)>();
                var listaWhere = new List<(bool isInt, string campo, string valor)>();
                listaUpdate.Add((false, "Compareceu", compareceu.ToString()));
                listaWhere.Add((false, nameof(Id), Id));
                await generico.RunSQLCommand(await generico.Atualizar("Agendamentos", listaUpdate, listaWhere));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<(int CortesAbertos, int CortesFinalizado)> PreencherGraficos()
        {
            var dia = RetornarDiaDaSemanaCodigo(DateTime.Now.DayOfWeek);
            var listaSelect = new List<(bool isInt, string nome)>();
            var listaWhere = new List<(bool isInt, string nome, string valor)>();
            var listaJoin = new List<(string, string, string)>();

            listaSelect.Add((false, "Nome"));
            listaJoin.Add(("DiasSemana", "Agendamentos", "DiaSemana_Id"));
            listaWhere.Add((true, "Codigo", dia));
            listaWhere.Add((true, "CONVERT(VARCHAR,DataCorte,103)", DateTime.Now.ToString("dd/MM/yyyy")));
            listaWhere.Add((true, "Compareceu", "0"));

            var cortesAbertos = await generico.Select(await generico.MontarSelectWithJoin("Agendamento", listaJoin, listaSelect, listaWhere, true));

            dia = RetornarDiaDaSemanaCodigo(DateTime.Now.DayOfWeek);
            listaSelect = new List<(bool isInt, string nome)>();
            listaWhere = new List<(bool isInt, string nome, string valor)>();
            listaJoin = new List<(string, string, string)>();

            listaSelect.Add((false, "Nome"));
            listaJoin.Add(("DiasSemana", "Agendamentos", "DiaSemana_Id"));
            listaWhere.Add((true, "Codigo", dia));
            listaWhere.Add((true, "CONVERT(VARCHAR,DataCorte,103)", DateTime.Now.ToString("dd/MM/yyyy")));
            listaWhere.Add((true, "Compareceu", "1"));

            var cortesFinalizados = await generico.Select(await generico.MontarSelectWithJoin("Agendamento", listaJoin, listaSelect, listaWhere, true));

            var retorno = (cortesAbertos.Rows.Count, cortesFinalizados.Rows.Count);
            return retorno;
        }
    }
}
