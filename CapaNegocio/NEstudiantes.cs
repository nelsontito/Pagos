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
    public class NEstudiantes
    {
        #region "PATRON SINGLETON"
        private static NEstudiantes conexion = null;

        private NEstudiantes() { }

        public static NEstudiantes GetInstance()
        {
            if (conexion == null)
            {
                conexion = new NEstudiantes();
            }
            return conexion;
        }
        #endregion}

        public Respuesta<List<EEstudiantes>> FiltroEstudiantes(string Busqueda)
        {
            return DEstudiantes.GetInstance().FiltroEstudiantes(Busqueda);
        }

        public Respuesta<List<EEstudiantesDTO>> ListaEstudiantesPaginado(int Omitir, int TamanoPagina, string Buscar)
        {
            return DEstudiantes.GetInstance().ListaEstudiantesPaginado(Omitir, TamanoPagina, Buscar);
        }

        public Respuesta<int> GuardarOrEditEstudiantes(EEstudiantes oModel)
        {
            return DEstudiantes.GetInstance().GuardarOrEditEstudiantes(oModel);
        }

        public Respuesta<EstudianteResponseDTO> BuscarCodigoEstudiante(string Codigo)
        {
            return DEstudiantes.GetInstance().BuscarCodigoEstudiante(Codigo);
        }
    }
}
