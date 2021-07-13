using Cortes.Dominio.Entidades;
using Cortes.Infra.Comum;
using Cortes.Repositorio.Interfaces.IUsuarioRepositorio;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cortes.Repositorio.Repositorios.UsuarioRepositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        Generico generico = new Generico();
        private DbSession _db;
        public UsuarioRepositorio(DbSession dbSession)
        {
            _db = dbSession;
        }

        public async Task<Usuario> Atualizar(Usuario usuario)
        {
            throw new NotImplementedException();
        }

        public async Task<Usuario> Criar(Usuario usuario)
        {
            try
            {
                using (var conn = _db.Connection)
                {
                    await conn.QueryFirstOrDefaultAsync<Usuario>(await generico.MontarInsert<Usuario>(usuario));
                }
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
            using (var conn = _db.Connection)
            {
                usuario = await conn.QueryFirstOrDefaultAsync<Usuario>("SELECT TOP 1 * FROM Usuarios");
                return usuario;
            }
        }
    }
}
