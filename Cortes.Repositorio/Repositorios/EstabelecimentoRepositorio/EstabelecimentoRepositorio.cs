using Cortes.Dominio.Entidades;
using Cortes.Infra.Comum;
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
        public EstabelecimentoRepositorio(IConfiguration config)
        {
            generico = new Generico(config);
        }

        public async Task<bool> ExisteConfiguracao(Estabelecimento estabelecimento)
        {
            var dt = await generico.Select(await generico.MontarSelect<Estabelecimento>(estabelecimento, null, false));
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
                await generico.RunSQLCommand(await generico.MontarInsert<DiasSemana>(diasSemana, false));
                diasSemana = new DiasSemana
                {
                    Codigo = 2,
                    Dia = "Terça-Feira"
                };
                await generico.RunSQLCommand(await generico.MontarInsert<DiasSemana>(diasSemana, false));
                diasSemana = new DiasSemana
                {
                    Codigo = 3,
                    Dia = "Quarta-Feira"
                };
                await generico.RunSQLCommand(await generico.MontarInsert<DiasSemana>(diasSemana, false));
                diasSemana = new DiasSemana
                {
                    Codigo = 4,
                    Dia = "Quinta-Feira"
                };
                await generico.RunSQLCommand(await generico.MontarInsert<DiasSemana>(diasSemana, false));
                diasSemana = new DiasSemana
                {
                    Codigo = 5,
                    Dia = "Sexta-Feira"
                };
                await generico.RunSQLCommand(await generico.MontarInsert<DiasSemana>(diasSemana, false));
                diasSemana = new DiasSemana
                {
                    Codigo = 6,
                    Dia = "Sábado"
                };
                await generico.RunSQLCommand(await generico.MontarInsert<DiasSemana>(diasSemana, false));
                diasSemana = new DiasSemana
                {
                    Codigo = 7,
                    Dia = "Domingo"
                };
                await generico.RunSQLCommand(await generico.MontarInsert<DiasSemana>(diasSemana, false));
                estabelecimento = new Estabelecimento
                {
                    Nome = "CABELEIREIRO",
                    Endereco = "AVENIDA"
                };

                await generico.RunSQLCommand(await generico.MontarInsert<Estabelecimento>(estabelecimento, false));
                return true;
            }
            catch
            {
                return false;
            }
            
        }
    }
}
