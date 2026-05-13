using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntida.Entidades
{
    public class EUsuarios
    {
        public int IdUsuario { get; set; }

        public int IdRol { get; set; }
        public string NombreUsuario { get; set; }
        public string ApellidosUsuario { get; set; }
        public string CiUsuario { get; set; }
        public string Correo { get; set; }
        public string Contrasena { get; set; }
        public string FotoUrl { get; set; }
        public bool Estado { get; set; }
        public string NombreRol { get; set; }
    }
}

