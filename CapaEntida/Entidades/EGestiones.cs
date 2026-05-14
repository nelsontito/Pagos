using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntida.Entidades
{
    public class EGestiones
    {
        public int IdGestion { get; set; }
        public string NombreGestion { get; set; }
        public int IdMesInicio { get; set; }
        public int IdMesFin { get; set; }
        public bool Estado { get; set; }        
        public string MesIni { get; set; }
        public string MesFin { get; set; }

    }
}
