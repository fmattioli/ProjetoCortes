using Cortes.Dominio.Entidades;
using Cortes.Infra.Comum;
using Cortes.Repositorio.Interfaces.IAgendamentoRepositorio;
using Cortes.Repositorio.Interfaces.IEstabelecimentoRepositorio;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cortes.Repositorio.Repositorios.EstabelecimentoRepositorio
{
    public class EstabelecimentoRepositorio : IEstabelecimentoRepositorio
    {
        private Generico generico = new Generico();
        private IAgendamentoRepositorio agendamentoRepositorio;
        private DbSession _db;
        public StringBuilder SQL { get; set; } = new StringBuilder();

        public EstabelecimentoRepositorio(IConfiguration config, IAgendamentoRepositorio agendamentoRepositorio, DbSession dbSession)
        {
            this.agendamentoRepositorio = agendamentoRepositorio;
            _db = dbSession;
        }

        public async Task<bool> ExisteConfiguracao()
        {
            try
            {
                using (var conn = _db.Connection)
                {
                    Estabelecimento estabelecimento = new Estabelecimento();
                    SQL.AppendLine("SELECT TOP 1 * FROM Estabelecimento");
                    estabelecimento = await conn.QueryFirstOrDefaultAsync<Estabelecimento>(SQL.ToString());
                    return (estabelecimento is not null);
                }
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task<bool> RealizarConfiguracao(Estabelecimento estabelecimento)
        {
            try
            {
                using (var conn = _db.Connection)
                {
                    await conn.ExecuteAsync("delete from Agendamentos");
                    await conn.ExecuteAsync("delete from DiasSemana");
                    await conn.ExecuteAsync("delete from Estabelecimento");
                    
                    //Inserir dias
                    DiasSemana diasSemana = new DiasSemana
                    {
                        Codigo = 1,
                        Dia = "Segunda-Feira"
                    };
                    await conn.ExecuteAsync(await generico.MontarInsert<DiasSemana>(diasSemana, null, false));
                    diasSemana = new DiasSemana
                    {
                        Codigo = 2,
                        Dia = "Terça-Feira"
                    };
                    await conn.ExecuteAsync(await generico.MontarInsert<DiasSemana>(diasSemana, null, false));
                    diasSemana = new DiasSemana
                    {
                        Codigo = 3,
                        Dia = "Quarta-Feira"
                    };
                    await conn.ExecuteAsync(await generico.MontarInsert<DiasSemana>(diasSemana, null, false));
                    diasSemana = new DiasSemana
                    {
                        Codigo = 4,
                        Dia = "Quinta-Feira"
                    };
                    await conn.ExecuteAsync(await generico.MontarInsert<DiasSemana>(diasSemana, null, false));
                    diasSemana = new DiasSemana
                    {
                        Codigo = 5,
                        Dia = "Sexta-Feira"
                    };
                    await conn.ExecuteAsync(await generico.MontarInsert<DiasSemana>(diasSemana, null, false));
                    diasSemana = new DiasSemana
                    {
                        Codigo = 6,
                        Dia = "Sábado"
                    };
                    await conn.ExecuteAsync(await generico.MontarInsert<DiasSemana>(diasSemana, null, false));
                    diasSemana = new DiasSemana
                    {
                        Codigo = 7,
                        Dia = "Domingo"
                    };
                    await conn.ExecuteAsync(await generico.MontarInsert<DiasSemana>(diasSemana, null, false));
                    estabelecimento = new Estabelecimento
                    {
                        Nome = "CABELEIREIRO",
                        Endereco = "AVENIDA",
                        HoraAbertura = "08:30",
                        HoraFechamento = "23:00"
                    };

                    await conn.ExecuteAsync(await generico.MontarInsert<Estabelecimento>(estabelecimento, null, false));


                    //Agendamentos dados fakes.
                    for (int codigo = 1; codigo < 7; codigo++)
                    {
                        foreach (var item in await Horarios())
                        {
                            SQL.Clear();
                            SQL.Append($"SELECT ID FROM DiasSemana WHERE Codigo = @Codigo");
                            string DiaSemana_Id = await conn.QueryFirstOrDefaultAsync<string>(SQL.ToString(), new {codigo });

                            SQL.Clear();
                            SQL.Append("SELECT TOP 1 Id FROM Usuarios");

                            Usuario usuario = await conn.QueryFirstOrDefaultAsync<Usuario>("SELECT TOP 1 * FROM Usuarios");
                            
                            Agendamento agendamento = new Agendamento
                            {
                                Endereco = "RUA FLOR DE MADEIRA" + new Random().Next(0, 100),
                                DiaSemana_Id = DiaSemana_Id,
                                Horario = item.Hora,
                                Nome = "CLIENTE " + new Random().Next(0, 100),
                                Preco = 35.00M,
                                Usuario_Id = usuario.Id.ToString(),
                                DataCorte = DateTime.Now,
                                Compareceu = 0

                            };

                            await conn.ExecuteAsync(await generico.MontarInsert<Agendamento>(agendamento));
                        }

                    }
                    return true;
                }
            }
            catch
            {
                return false;
            }

        }

        public async Task<IList<Horario>> Horarios()
        {
            IList<Horario> horariosDisponiveis = new List<Horario>();
            using (var conn = _db.Connection)
            {
                Estabelecimento estabelecimento = await conn.QueryFirstOrDefaultAsync<Estabelecimento>(await generico.MontarSelectObjeto<Estabelecimento>(new Estabelecimento()));
                if (estabelecimento is not null)
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
    }
}
