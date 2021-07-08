using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cortes.Dominio.Entidades
{

    public class GraficoCortes
    {
        public int CortesAbertos { get; set; }
        public int CortesFinalizados { get; set; }
        public int CortesCancelados { get; set; }
    }
}
