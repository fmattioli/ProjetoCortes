using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cortes.Dominio.Entidades
{
    public class Estabelecimento
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Endereco { get; set; }
        public string HoraAbertura { get; set; }
        public string HoraFechamento { get; set; }
    }
}
