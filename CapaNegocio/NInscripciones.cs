using CapaDatos;
using CapaEntida.DTOs;
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

        public Respuesta<int> PagarMensualidad(int IdControl)
        {
            return DInscripciones.GetInstance().PagarMensualidad(IdControl);
        }

        public Respuesta<List<ConsulMenDTO>> ConsultaMensualidad(string Codigo)
        {
            return DInscripciones.GetInstance().ConsultaMensualidad(Codigo);
        }

        public Respuesta<int> Registrar(EInscripciones oModel)
        {
            return DInscripciones.GetInstance().Registrar(oModel);
        }
    }
}
