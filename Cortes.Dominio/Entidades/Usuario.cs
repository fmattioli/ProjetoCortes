using Cortes.Dominio.Util;
namespace Cortes.Dominio.Entidades
{
    public class Usuario : Comum
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Telefone { get; set; }
    }
}
