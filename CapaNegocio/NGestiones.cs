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
    public class NGestiones
    {
        #region "PATRON SINGLETON"
        private static NGestiones conexion = null;

        private NGestiones() { }

        public static NGestiones GetInstance()
        {
            if (conexion == null)
            {
                conexion = new NGestiones();
            }
            return conexion;
        }
        #endregion

        public Respuesta<List<ESemestres>> ListaSemestres()
        {
            return DGestiones.GetInstance().ListaSemestres();
        }

        public Respuesta<List<EGestiones>> ListaGestiones()
        {
            return DGestiones.GetInstance().ListaGestiones();
        }


        public Respuesta<int> GuardarOrEditGestiones(EGestiones objeto)
        {
            return DGestiones.GetInstance().GuardarOrEditGestiones(objeto);
        }

        public Respuesta<List<EMeses>> ListaMeses()
        {
            return DGestiones.GetInstance().ListaMeses();
        }
    }
}
