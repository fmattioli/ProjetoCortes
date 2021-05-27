using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cortes.Services.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
    }
}
