using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cortes.Dominio.Entidades
{
    public class Agendamento
    {
		public string Id { get; set; }
		public string Nome { get; set; }
		public string Endereco { get; set; }
		public string DiaSemana_Id { get; set; }
		public string Usuario_Id { get; set; }
		public string Horario { get; set; }
		public decimal Preco { get; set; }
	}
}
