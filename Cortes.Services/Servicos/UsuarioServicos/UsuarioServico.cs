using Cortes.Dominio.Entidades;
using Cortes.Repositorio.Interfaces.IUsuarioRepositorio;
using Cortes.Services.Auth;
using Cortes.Services.Interfaces;
using Cortes.Services.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cortes.Services.UsuarioServicos
{
    public class UsuarioServico : IUsuarioServico
    {
        private readonly IUsuarioRepositorio usuarioRepositorio;
        private readonly IClientAuth auth;
        public UsuarioServico(IUsuarioRepositorio usuarioRepositorio, IClientAuth auth)
        {
            this.usuarioRepositorio = usuarioRepositorio;
            this.auth = auth;
        }

        public async Task<string> CriarUsuario(RegistroViewModel model)
        {
            Usuario usuario = new Usuario();
            usuario.Nome = model.Nome;
            usuario.Senha = Criptografia.Encrypt(model.Senha);
            //string teste = Criptografia.Decrypt(usuario.Senha);
            usuario.Endereco = model.Endereco;
            usuario.Email = model.Email;
            usuario.Telefone = model.Telefone;
            await usuarioRepositorio.Criar(usuario);
            string token = Token.GenerateToken(model.Nome, auth.Key);

            //Popular LoginViewModel
            LoginViewModel login = new LoginViewModel();
            login.Nome = model.Nome;
            login.Token = token;
            return JsonConvert.SerializeObject(login);

        }

        public async Task<LoginViewModel> ObterUsuario(LoginViewModel login)
        {
            Usuario usuario = new Usuario
            {
                Email = login.Email,
                Senha = Criptografia.Encrypt(login.Senha)
            };

            //Caso seja desejado o WHERE, basta implementar uma lista com as cláusulas desejada.
            IList<string> hasWhere = new List<string>();
            hasWhere.Add(nameof(login.Email));
            hasWhere.Add(nameof(login.Senha));
            usuario = await usuarioRepositorio.Obter(usuario, hasWhere);

            if (usuario != null)
            {
                login.Token = Token.GenerateToken(usuario.Nome, auth.Key);
                login.Nome = usuario.Nome;
                login.Id = usuario.Id;
                return login;
            }
            return null;
        }
    }
}
