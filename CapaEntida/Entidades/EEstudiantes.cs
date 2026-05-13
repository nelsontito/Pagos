using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntida.Entidades
{
    public class EEstudiantes
    {
        public int IdEstudiante { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string NroCi { get; set; }
        public string Celular { get; set; }
        public string Codigo { get; set; }
        public string CodQrUrl { get; set; }
        public string ImagenEstUrl { get; set; }
        public string FechaRegistro { get; set; }
        public bool Estado { get; set; }
    }
}
