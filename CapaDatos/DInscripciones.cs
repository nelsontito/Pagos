using CapaEntida.DTOs;
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

        public Respuesta<int> PagarMensualidad(int IdControl)
        {
            Respuesta<int> response = new Respuesta<int>();
            int resultadoCodigo = 0;

            try
            {
                using (SqlConnection con = ConexionBD.GetInstance().ConexionDB())
                {
                    using (SqlCommand cmd = new SqlCommand("usp_RegistrarPagoMensualidadNew", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // 2. Parámetro
                        cmd.Parameters.AddWithValue("@IdControl", IdControl);

                        // 4. Parámetro de Salida
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

                // 5. Interpretación de la respuesta
                switch (resultadoCodigo)
                {
                    case 1:
                        response.Estado = true;
                        response.Valor = "success";
                        response.Mensaje = "Mensualidad pagada exitosamente.";
                        break;

                    case 2:
                        response.Estado = false;
                        response.Valor = "warning";
                        response.Mensaje = "El Estudiante no existe o ya fue eliminada.";
                        break;

                    case 3:
                        response.Estado = false;
                        response.Valor = "warning";
                        response.Mensaje = "Mensualidad ya Cancelada.";
                        break;
                    case 4:
                        response.Estado = false;
                        response.Valor = "warning";
                        response.Mensaje = "Debe cancelar primero las cuotas anteriores.";
                        break;
                    case 0:
                    default:
                        response.Estado = false;
                        response.Valor = "error";
                        response.Mensaje = "No se pudo completar la operación en la base de datos.";
                        break;
                }
            }
            catch (Exception ex)
            {
                //response.Data = 0;
                response.Estado = false;
                response.Valor = "error";
                response.Mensaje = "Error interno: " + ex.Message;
            }

            return response;
        }

        public Respuesta<List<ConsulMenDTO>> ConsultaMensualidad(string Codigo)
        {
            try
            {
                List<ConsulMenDTO> rptLista = new List<ConsulMenDTO>();

                using (SqlConnection con = ConexionBD.GetInstance().ConexionDB())
                {
                    using (SqlCommand comando = new SqlCommand("usp_ConsultarMensualidadesPorCodigo", con))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Parameters.AddWithValue("@Codigo", Codigo);
                        con.Open();

                        using (SqlDataReader dr = comando.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                rptLista.Add(new ConsulMenDTO
                                {
                                    IdControl = Convert.ToInt32(dr["IdControl"]),
                                    IdMes = Convert.ToInt32(dr["IdMes"]),
                                    NombreMes = dr["NombreMes"].ToString(),
                                    EstaPagado = Convert.ToBoolean(dr["EstaPagado"]),
                                    // ANTES: 
                                    // FechaPago = Convert.ToDateTime(dr["FechaPago"]).ToString("dd/MM/yyyy"),
                                    // AHORA:
                                    FechaPago = dr["FechaPago"] == DBNull.Value ? "" : Convert.ToDateTime(dr["FechaPago"]).ToString("dd/MM/yyyy"),
                                    NombreGestion = dr["NombreGestion"].ToString(),
                                    NombreCarrera = dr["NombreCarrera"].ToString()
                                });
                            }
                        }
                    }
                }
                return new Respuesta<List<ConsulMenDTO>>()
                {
                    Estado = true,
                    Data = rptLista,
                    Mensaje = "mensualidades obtenidas correctamente"
                };
            }
            catch (Exception ex)
            {
                // Maneja cualquier error inesperado
                return new Respuesta<List<ConsulMenDTO>>()
                {
                    Estado = false,
                    Mensaje = $"Error al obtener la lista de mensualidades: {ex.Message}",
                    Data = null
                };
            }
        }



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
