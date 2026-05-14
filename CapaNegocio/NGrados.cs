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
    public class NGrados
    {
        #region "PATRON SINGLETON"
        private static NGrados conexion = null;

        private NGrados() { }

        public static NGrados GetInstance()
        {
            if (conexion == null)
            {
                conexion = new NGrados();
            }
            return conexion;
        }
        #endregion

        public Respuesta<List<EGrados>> ListaGradosAcademicos()
        {
            return DGrados.GetInstance().ListaGradosAcademicos();
        }


        public Respuesta<int> usp_GuardarOrEditGradoAcademicos(EGrados objeto)
        {
            return DGrados.GetInstance().usp_GuardarOrEditGradoAcademicos(objeto);
        }
    }
}
