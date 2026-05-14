using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntida.Entidades
{
    public class ECarreras
    {
        public int IdCarrera { get; set; }

        public int IdGradoAcademico { get; set; }
        public string NombreCarrera { get; set; }
        public string Sigla { get; set; }
        public bool Estado { get; set; }
        public string NombreGrado { get; set; }

    }
}
