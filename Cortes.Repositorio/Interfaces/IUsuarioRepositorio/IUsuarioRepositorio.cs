using Cortes.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cortes.Repositorio.Interfaces.IUsuarioRepositorio
{
    public interface IUsuarioRepositorio
    {
        Task<Usuario> Obter(Usuario usuario, IList<string> hasWhere);
        Task<Usuario> Atualizar(Usuario usuario);
        Task<Usuario> Criar(Usuario usuario);
        Task<bool> Existe();
    }
}
