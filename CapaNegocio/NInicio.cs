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
    public class NInicio
    {
        #region PATRON SINGLETON

        private static NInicio daoInicio = null;

        private NInicio() { }

        public static NInicio GetInstance()
        {
            if (daoInicio == null)
            {
                daoInicio = new NInicio();
            }

            return daoInicio;
        }

        #endregion

        public Respuesta<EInicio> ObtenerResumenDashboard()
        {
            return DInicio.GetInstance().ObtenerResumenDashboard();
        }
    }
}
