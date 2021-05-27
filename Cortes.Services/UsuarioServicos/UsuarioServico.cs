using Cortes.Dominio.Entidades;
using Cortes.Repositorio.Interfaces.IUsuarioRepositorio;
using Cortes.Services.Interfaces;
using Cortes.Services.ViewModels;
using System;
using System.Threading.Tasks;

namespace Cortes.Services.UsuarioServicos
{
    public class UsuarioServico : IUsuarioServico
    {
        private readonly IUsuarioRepositorio usuarioRepositorio;
        public UsuarioServico(IUsuarioRepositorio usuarioRepositorio)
        {
            this.usuarioRepositorio = usuarioRepositorio;
        }

        public async Task<Usuario> CriarUsuario(RegistroViewModel model)
        {
            Usuario usuario = new Usuario();
            usuario.Nome = model.Nome;
            usuario.Senha = model.Senha;
            usuario.Email = model.Email;
            usuario.Telefone = model.Telefone;
            return await usuarioRepositorio.Criar(usuario);

        }
    }
}
