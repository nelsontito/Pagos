using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntida.DTOs
{
    public class ConsulMenDTO
    {
        public int IdControl { get; set; }
        public int IdMes { get; set; }
        public string NombreMes { get; set; }
        public bool EstaPagado { get; set; }
        public string FechaPago { get; set; }
        public string NombreGestion { get; set; }
        public string NombreCarrera { get; set; }
    
    }
}
