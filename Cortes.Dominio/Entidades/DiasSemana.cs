using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cortes.Dominio.Entidades
{
    public class DiasSemana
    {
        public string SegundaFeira { get; set; }
        public string TercaFeira { get; set; }
        public string QuartaFeira { get; set; }
        public string QuintaFeira { get; set; }
        public string SextaFeira { get; set; }
        public string Sabado { get; set; }
        public string Domingo { get; set; }

        public IList<DiasSemana> Dias = new List<DiasSemana>();
    }
}
