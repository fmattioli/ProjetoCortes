using Cortes.Repositorio.Interfaces.IUsuarioRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cortes.Dominio.Entidades;
using Cortes.Infra.Comum;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;

namespace Cortes.Repositorio.Repositorios.UsuarioRepositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private StringBuilder SQL = new StringBuilder();
        private Generico generico;
        public UsuarioRepositorio([FromServices] IConfiguration config)
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
            catch(Exception ex)
            {
                throw;
            }
        }

        public Task<bool> Existe()
        {
            throw new NotImplementedException();
        }

        public Task<Usuario> Obter(int id)
        {
            throw new NotImplementedException();
        }
    }
}
