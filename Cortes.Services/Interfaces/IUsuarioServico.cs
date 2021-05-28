using Cortes.Dominio.Entidades;
using Cortes.Services.ViewModels;
using System.Threading.Tasks;

namespace Cortes.Services.Interfaces
{
    public interface IUsuarioServico
    {
        Task<string> CriarUsuario(RegistroViewModel usuario);
        Task<LoginViewModel> ObterUsuario(LoginViewModel login);
    }
}
