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
    public class NInscripciones
    {
        #region "PATRON SINGLETON"
        private static NInscripciones conexion = null;

        private NInscripciones() { }

        public static NInscripciones GetInstance()
        {
            if (conexion == null)
            {
                conexion = new NInscripciones();
            }
            return conexion;
        }
        #endregion}

        public Respuesta<int> Registrar(EInscripciones oModel)
        {
            return DInscripciones.GetInstance().Registrar(oModel);
        }
    }
}
