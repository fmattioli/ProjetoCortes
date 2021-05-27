using Cortes.Dominio.Entidades;
using Cortes.Services.ViewModels;
using System.Threading.Tasks;

namespace Cortes.Services.Interfaces
{
    public interface IUsuarioServico
    {
        Task<Usuario> CriarUsuario(RegistroViewModel usuario);
    }
}
