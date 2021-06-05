using Cortes.Dominio.Entidades;
using Cortes.Infra.Comum;
using Cortes.Repositorio.Interfaces.IAgendamentoRepositorio;
using Cortes.Repositorio.Interfaces.IEstabelecimentoRepositorio;
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
        private StringBuilder SQL = new StringBuilder();
        private Generico generico;
        private IAgendamentoRepositorio agendamentoRepositorio;
        public EstabelecimentoRepositorio(IConfiguration config, IAgendamentoRepositorio agendamentoRepositorio)
        {
            generico = new Generico(config);
            this.agendamentoRepositorio = agendamentoRepositorio;
        }

        public async Task<bool> ExisteConfiguracao(Estabelecimento estabelecimento)
        {
            var dt = await generico.Select(await generico.MontarSelectObjeto<Estabelecimento>(estabelecimento, null, false));
            return dt?.Rows.Count >= 1;
        }

        public async Task<bool> RealizarConfiguracao(Estabelecimento estabelecimento)
        {
            try
            {
                //Inserir dias
                DiasSemana diasSemana = new DiasSemana
                {
                    Codigo = 1,
                    Dia = "Segunda-Feira"
                };
                await generico.RunSQLCommand(await generico.MontarInsert<DiasSemana>(diasSemana, null, false));
                diasSemana = new DiasSemana
                {
                    Codigo = 2,
                    Dia = "Terça-Feira"
                };
                await generico.RunSQLCommand(await generico.MontarInsert<DiasSemana>(diasSemana, null, false));
                diasSemana = new DiasSemana
                {
                    Codigo = 3,
                    Dia = "Quarta-Feira"
                };
                await generico.RunSQLCommand(await generico.MontarInsert<DiasSemana>(diasSemana, null, false));
                diasSemana = new DiasSemana
                {
                    Codigo = 4,
                    Dia = "Quinta-Feira"
                };
                await generico.RunSQLCommand(await generico.MontarInsert<DiasSemana>(diasSemana, null, false));
                diasSemana = new DiasSemana
                {
                    Codigo = 5,
                    Dia = "Sexta-Feira"
                };
                await generico.RunSQLCommand(await generico.MontarInsert<DiasSemana>(diasSemana, null, false));
                diasSemana = new DiasSemana
                {
                    Codigo = 6,
                    Dia = "Sábado"
                };
                await generico.RunSQLCommand(await generico.MontarInsert<DiasSemana>(diasSemana, null, false));
                diasSemana = new DiasSemana
                {
                    Codigo = 7,
                    Dia = "Domingo"
                };
                await generico.RunSQLCommand(await generico.MontarInsert<DiasSemana>(diasSemana, null, false));
                estabelecimento = new Estabelecimento
                {
                    Nome = "CABELEIREIRO",
                    Endereco = "AVENIDA",
                    HoraAbertura = "08:30",
                    HoraFechamento = "23:00"
                };

                await generico.RunSQLCommand(await generico.MontarInsert<Estabelecimento>(estabelecimento, null, false));


                //Agendamentos dados fakes.
                for (int i = 1; i < 7; i++)
                {
                    foreach (var item in await Horarios())
                    {
                        var listWhere = new List<(bool isInt, string nome, string valor)>();
                        listWhere.Add((true, "Codigo", i.ToString()));
                        var dt = await generico.Select(await generico.MontarSelectGetId("DiasSemana", listWhere));
                        string DiaSemana_Id = dt.Rows[0]["Id"].ToString();

                        listWhere = new List<(bool isInt, string nome, string valor)>();
                        listWhere.Add((false, "Nome", "Felipe"));

                        dt = await generico.Select(await generico.MontarSelectGetId("Usuarios", listWhere));
                        string Usuario_Id = dt.Rows[0]["Id"].ToString();

                        Agendamento agendamento = new Agendamento
                        {
                            Codigo = int.Parse(agendamentoRepositorio.RetornarDiaDaSemanaCodigo(DateTime.Now.DayOfWeek)),
                            Endereco = "RUA FLOR DE MADEIRA" + new Random().Next(0, 100),
                            DiaSemana_Id = DiaSemana_Id,
                            Horario = item.Hora,
                            Nome = "CLIENTE " + new Random().Next(0, 100),
                            Preco = 35.00M,
                            Usuario_Id = Usuario_Id

                        };

                        //Caso não queira inserir uma coluna no insert
                        List<string> listaDesconsiderar = new List<string>();
                        listaDesconsiderar.Add("Codigo");

                        await generico.RunSQLCommand(await generico.MontarInsert<Agendamento>(agendamento, listaDesconsiderar, true));
                    }

                }
                return true;
            }
            catch
            {
                return false;
            }

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
    }
}
