using Cortes.Dominio.Entidades;
using Cortes.Infra.Comum;
using Cortes.Repositorio.Interfaces.IAgendamentoRepositorio;
using Dapper;
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
        private Generico generico = new Generico();
        private DbSession _db;

        public AgendamentoRepositorio(DbSession dbSession)
        {
            _db = dbSession;
        }

        public async Task<bool> ConfirmarAgendamento(Agendamento agendamento)
        {
            try
            {
                if (await ValidarAgendamento(agendamento))
                {
                    //Configurar Cláusula WHERE

                    using (var conn = _db.Connection)
                    {

                        SQL.Clear();
                        SQL.Append($"SELECT ID FROM DiasSemana WHERE Id = @Id");
                        agendamento.DiaSemana_Id = await conn.QueryFirstOrDefaultAsync<string>(SQL.ToString(),new { agendamento.Id });

                        var result = await conn.ExecuteAsync(sql: await generico.MontarInsert<Agendamento>(agendamento, null, true), agendamento);
                    }

                    using (var conn = _db.Connection)
                    {
                        
                    }

                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> LancarAgendamento(Agendamento agendamento)
        {
            try
            {
                using (var conn = _db.Connection)
                {
                    SQL.Clear();
                    SQL.Append($"SELECT ID FROM DiasSemana WHERE Id = @Id");
                    agendamento.DiaSemana_Id = await conn.QueryFirstOrDefaultAsync<string>(SQL.ToString(), new { agendamento.Id });
                    var result = await conn.ExecuteAsync(sql: await generico.MontarInsert<Agendamento>(agendamento, null, true), agendamento);
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> ValidarAgendamento(Agendamento agendamento)
        {
            using (var conn = _db.Connection)
            {
                SQL.Clear();
                SQL.AppendLine("SELECT ISNULL(count(*),0) as Total			");
                SQL.AppendLine("FROM Agendamento 			");
                SQL.AppendLine("WHERE DiasSemana_Id = @Id	");
                SQL.AppendLine("AND Horario = @Horario		");

                int total = await conn.QueryFirstOrDefaultAsync<int>
                    (sql: SQL.ToString(), param: new { agendamento.Id, agendamento.Horario });

                if (total == 0)
                    return true;
            }
            return false;
        }

        public async Task<IList<DiasSemana>> DiasSemana(DiasSemana dias)
        {
            IList<DiasSemana> diasSemana = new List<DiasSemana>();
            using (var conn = _db.Connection)
            {
                SQL.Clear();
                SQL.AppendLine("SELECT Id, Dia FROM DiasSemana");
                diasSemana = (await conn.QueryAsync<DiasSemana>(SQL.ToString())).ToList();
                return diasSemana;
            }
        }

        public async Task<IList<Horario>> Horarios()
        {
            IList<Horario> horariosDisponiveis = new List<Horario>();
            using (var conn = _db.Connection)
            {
                Estabelecimento estabelecimento = await conn.QueryFirstOrDefaultAsync<Estabelecimento>(await generico.MontarSelectObjeto<Estabelecimento>(new Estabelecimento()));
                if(estabelecimento is not null)
                {
                    string horarioAbertura = estabelecimento.HoraAbertura;
                    string horarioFechamento = estabelecimento.HoraFechamento;
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

        public async Task<IList<Agendamento>> AgendamentosDiario()
        {
            try
            {
                IList<Agendamento> listaAgendamento = new List<Agendamento>();
                using (var conn = _db.Connection)
                {
                    string codigoDia = RetornarDiaDaSemanaCodigo();
                    SQL.Clear();
                    SQL.AppendLine("SELECT Agendamento.* FROM Agendamento ");
                    SQL.AppendLine("INNER JOIN DiasSemana ON DiasSemana.Id = Agendamento.DiasSemana_Id");
                    SQL.AppendLine("WHERE DiasSemana.Codigo = @codigo");
                    listaAgendamento = (await conn.QueryAsync<Agendamento>(SQL.ToString(), new { codigo = codigoDia })).ToList();
                    return listaAgendamento;
                }
            }
            catch
            {
                throw;
            }
        }

        public string RetornarDiaDaSemanaCodigo()
        {
            DayOfWeek dayOfWeek = DateTime.Now.DayOfWeek;
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

                using (var conn = _db.Connection)
                {
                    var result = await conn.ExecuteAsync(sql: await generico.Atualizar("Agendamentos", listaUpdate, listaWhere));
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public async Task<GraficoCortes> PreencherGraficos()
        {
            var codigo = RetornarDiaDaSemanaCodigo();
            using (var conn = _db.Connection)
            {
                SQL.Clear();
                SQL.AppendLine("SELECT TOP 1																");
                SQL.AppendLine("	(																		");
                SQL.AppendLine("	SELECT COUNT(*) AS CortesAbertos										");
                SQL.AppendLine("	FROM agendamentos														");
                SQL.AppendLine("	INNER JOIN DiasSemana ON DiasSemana.Id = Agendamentos.DiaSemana_Id		");
                SQL.AppendLine("	WHERE Codigo = @codigo														");
                SQL.AppendLine("	AND Compareceu = '0' 													");
                SQL.AppendLine("	) AS CortesAbertos,														");
                SQL.AppendLine("	(																		");
                SQL.AppendLine("	SELECT COUNT(*) AS CortesAbertos										");
                SQL.AppendLine("	FROM agendamentos														");
                SQL.AppendLine("	INNER JOIN DiasSemana ON DiasSemana.Id = Agendamentos.DiaSemana_Id		");
                SQL.AppendLine("	WHERE Codigo = @codigo														");
                SQL.AppendLine("	AND Compareceu = '1' 													");
                SQL.AppendLine("	) AS CortesFinalizados,													");
                SQL.AppendLine("	(																		");
                SQL.AppendLine("	SELECT COUNT(*) AS CortesAbertos										");
                SQL.AppendLine("	FROM agendamentos														");
                SQL.AppendLine("	INNER JOIN DiasSemana ON DiasSemana.Id = Agendamentos.DiaSemana_Id		");
                SQL.AppendLine("	WHERE Codigo = @codigo														");
                SQL.AppendLine("	AND Compareceu = '2' 													");
                SQL.AppendLine("	) AS CortesCancelados													");
                SQL.AppendLine("FROM Agendamentos															");

                GraficoCortes cortes = await conn.QueryFirstOrDefaultAsync<GraficoCortes>
                   (sql: SQL.ToString(), param: new { codigo });

                return cortes;
            }


        }
    }
}
