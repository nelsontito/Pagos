using CapaEntida.Entidades;
using CapaEntida.Responses;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class DInscripciones
    {
        #region "PATRON SINGLETON"
        private static DInscripciones conexion = null;

        private DInscripciones() { }

        public static DInscripciones GetInstance()
        {
            if (conexion == null)
            {
                conexion = new DInscripciones();
            }
            return conexion;
        }
        #endregion



        public Respuesta<int> Registrar(EInscripciones oModel)
        {
            Respuesta<int> response = new Respuesta<int>();
            int resultadoCodigo = 0;
            try
            {
                using (SqlConnection con = ConexionBD.GetInstance().ConexionDB())
                {
                    using (SqlCommand cmd = new SqlCommand("usp_RegistrarInscripcion", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdEstudiante", oModel.IdEstudiante);
                        cmd.Parameters.AddWithValue("@IdCarrera", oModel.IdCarrera);
                        cmd.Parameters.AddWithValue("@IdSemestre", oModel.IdSemestre);
                        cmd.Parameters.AddWithValue("@IdGestion", oModel.IdGestion);

                        SqlParameter outputParam = new SqlParameter("@Resultado", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(outputParam);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        resultadoCodigo = Convert.ToInt32(outputParam.Value);
                    }
                }
                response.Data = resultadoCodigo;
                switch (resultadoCodigo)
                {
                    case 1: // Registro Nuevo
                        response.Estado = true;
                        response.Valor = "success";
                        response.Mensaje = "Inscripcion Correctamente.";
                        break;

                    case 2: // Duplicado
                        response.Estado = false;
                        response.Valor = "warning";
                        response.Mensaje = "Registro Existente para este estudiante.";
                        break;


                    case 0: // Error
                    default:
                        response.Estado = false;
                        response.Valor = "error";
                        response.Mensaje = "No se pudo completar la operación.";
                        break;
                }
            }
            catch (Exception ex)
            {
                response.Estado = false;
                response.Valor = "error";
                response.Mensaje = $"Error interno: {ex.Message}";
            }
            return response;
        }

    }


}
