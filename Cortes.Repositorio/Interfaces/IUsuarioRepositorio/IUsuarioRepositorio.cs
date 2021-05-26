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
        Task<Usuario> Obter();
        Task<Usuario> Atualizar();
        Task<Usuario> Criar();
        Task<bool> Existe();
    }
}
