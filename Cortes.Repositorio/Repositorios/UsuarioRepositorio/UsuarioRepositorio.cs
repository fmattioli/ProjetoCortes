using Cortes.Dominio.Entidades;
using Cortes.Infra.Comum;
using Cortes.Repositorio.Interfaces.IUsuarioRepositorio;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cortes.Repositorio.Repositorios.UsuarioRepositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        
        private StringBuilder SQL = new StringBuilder();
        private Generico generico;
        public UsuarioRepositorio(IConfiguration config)
        {
            generico = new Generico(config);
        }

        public async Task<Usuario> Atualizar(Usuario usuario)
        {
            await generico.Select("");
            return new Usuario();
        }

        public async Task<Usuario> Criar(Usuario usuario)
        {
            try
            {
                await generico.RunSQLCommand(await generico.MontarInsert<Usuario>(usuario));
                return usuario;
            }
            catch
            {
                throw;
            }
        }

        public Task<bool> Existe()
        {
            throw new NotImplementedException();
        }

        public async Task<Usuario> Obter(Usuario usuario, IList<string> hasWhere)
        {
            var dados = await generico.Select(await generico.MontarSelectObjeto<Usuario>(usuario, hasWhere));
            if (dados.Rows.Count >= 1)
            {
                usuario = new Usuario()
                {
                    Nome = dados.Rows[0]["Nome"].ToString(),
                    Email = dados.Rows[0]["Email"].ToString(),
                    Senha = dados.Rows[0]["Senha"].ToString(),
                    Telefone = dados.Rows[0]["Telefone"].ToString(),
                    Endereco = dados.Rows[0]["Endereco"].ToString(),
                    Id = dados.Rows[0]["Id"].ToString()
                };

                return usuario;
            }
            return null;
        }
    }
}
