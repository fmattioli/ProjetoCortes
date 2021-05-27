using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cortes.Services.ViewModels
{
    public class RegistroViewModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigátorio")]
        [StringLength(40, ErrorMessage = "Use menos caracteres")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigátorio")]
        [StringLength(40, ErrorMessage = "Use menos caracteres")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirme sua senha")]
        [Compare("Senha", ErrorMessage = "As senhas não são iguais")]
        public string SenhaConfirmada { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Telefone { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Endereco { get; set; }
    }
}
