using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Cortes.Services.ViewModels
{
    public class AgendamentoViewModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Nome { get; set; }
        public string Endereco { get; set; }
        public SelectList Dias { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Escolha um dia válido")]
        public int DiaSelecionado { get; set; }
        public SelectList Horarios { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Escolha um horário válido")]
        public int HorarioSelecionado { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Cabeleireiro")]
        public string Usuario_Id { get; set; }
        [DisplayFormat(DataFormatString = "{0:C0}")]
        public decimal Preco { get; set; }

    }
}
