using CapaDatos;
using CapaEntida.Entidades;
using CapaEntida.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class NCarreras
    {
        #region "PATRON SINGLETON"
        private static NCarreras instancia = null;
        private NCarreras() { }
        public static NCarreras GetInstance()
        {
            if (instancia == null)
            {
                instancia = new NCarreras();
            }
            return instancia;
        }
        #endregion

        public Respuesta<List<ECarreras>> ObtenerCarrerasPorGrado(int idGradoAcademico)
        {
            return DCarreras.GetInstance().ObtenerCarrerasPorGrado(idGradoAcademico);
        }

        public Respuesta<int> GuardarOrEditCarrera(ECarreras oCarrera)
        {
            return DCarreras.GetInstance().GuardarOrEditCarrera(oCarrera);
        }
    }
}
