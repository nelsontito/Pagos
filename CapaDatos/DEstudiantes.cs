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
    public class DEstudiantes
    {
        #region "PATRON SINGLETON"
        private static DEstudiantes conexion = null;

        private DEstudiantes() { }

        public static DEstudiantes GetInstance()
        {
            if (conexion == null)
            {
                conexion = new DEstudiantes();
            }
            return conexion;
        }
        #endregion


        public Respuesta<List<EEstudiantesDTO>> ListaEstudiantesPaginado(int Omitir, int TamanoPagina, string Buscar)
        {
            try
            {
                List<EEstudiantesDTO> rptLista = new List<EEstudiantesDTO>();

                using (SqlConnection con = ConexionBD.GetInstance().ConexionDB())
                {
                    using (SqlCommand cmd = new SqlCommand("usp_ListarEstudiantesPaginado", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Omitir", Omitir);
                        cmd.Parameters.AddWithValue("@TamanoPagina", TamanoPagina);
                        // Si Buscar es null, mandamos cadena vacía para evitar errores en SQL
                        cmd.Parameters.AddWithValue("@Buscar", Buscar ?? "");
                        con.Open();

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                rptLista.Add(new EEstudiantesDTO
                                {
                                    IdEstudiante = Convert.ToInt32(dr["IdEstudiante"]),
                                    Nombres = dr["Nombres"].ToString(),
                                    Apellidos = dr["Apellidos"].ToString(),
                                    NroCi = dr["NroCi"].ToString(),

                                    Celular = dr["Celular"].ToString(),
                                    Codigo = dr["Codigo"].ToString(),
                                    CodQrUrl = dr["CodQrUrl"].ToString(),
                                    ImagenEstUrl = dr["ImagenEstUrl"].ToString(),
                                    Estado = Convert.ToBoolean(dr["Estado"]),
                                    // Mapeo de los campos de paginación
                                    TotalRegistros = Convert.ToInt32(dr["TotalRegistros"]),
                                    TotalFiltrados = Convert.ToInt32(dr["TotalFiltrados"])
                                });
                            }
                        }
                    }
                }
                return new Respuesta<List<EEstudiantesDTO>>()
                {
                    Estado = true,
                    Data = rptLista,
                    Mensaje = "Lista obtenidos correctamente"
                };
            }
            catch (Exception ex)
            {
                // Maneja cualquier error inesperado
                return new Respuesta<List<EEstudiantesDTO>>()
                {
                    Estado = false,
                    Mensaje = "Ocurrió un error: " + ex.Message,
                    Data = null
                };
            }
        }

        public Respuesta<List<EEstudiantes>> FiltroEstudiantes(string Busqueda)
        {
            try
            {
                List<EEstudiantes> rptLista = new List<EEstudiantes>();

                using (SqlConnection con = ConexionBD.GetInstance().ConexionDB())
                {
                    using (SqlCommand comando = new SqlCommand("usp_FiltroEstudiantes", con))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Parameters.AddWithValue("@Busqueda", Busqueda);
                        con.Open();

                        using (SqlDataReader dr = comando.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                rptLista.Add(new EEstudiantes
                                {
                                    IdEstudiante = Convert.ToInt32(dr["IdEstudiante"]),
                                    Nombres = dr["Nombres"].ToString(),
                                    Apellidos = dr["Apellidos"].ToString(),
                                    NroCi = dr["NroCi"].ToString(),
                                    Celular = dr["Celular"].ToString(),
                                    Codigo = dr["Codigo"].ToString(),
                                    ImagenEstUrl = dr["ImagenEstUrl"].ToString()
                                });
                            }
                        }
                    }
                }
                return new Respuesta<List<EEstudiantes>>()
                {
                    Estado = true,
                    Data = rptLista,
                    Mensaje = "Lista obtenidos correctamente"
                };
            }
            catch (Exception ex)
            {
                // Maneja cualquier error inesperado
                return new Respuesta<List<EEstudiantes>>()
                {
                    Estado = false,
                    Mensaje = "Ocurrió un error: " + ex.Message,
                    Data = null
                };
            }
        }

        public Respuesta<int> GuardarOrEditEstudiantes(EEstudiantes oModel)
        {
            Respuesta<int> response = new Respuesta<int>();
            int resultadoCodigo = 0;
            try
            {
                using (SqlConnection con = ConexionBD.GetInstance().ConexionDB())
                {
                    using (SqlCommand cmd = new SqlCommand("usp_GuardarOrEditEstudiantes", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdEstudiante", oModel.IdEstudiante);
                        cmd.Parameters.AddWithValue("@Nombres", oModel.Nombres);
                        cmd.Parameters.AddWithValue("@Apellidos", oModel.Apellidos);
                        cmd.Parameters.AddWithValue("@NroCi", oModel.NroCi);
                        cmd.Parameters.AddWithValue("@Celular", oModel.Celular);
                        cmd.Parameters.AddWithValue("@Codigo", oModel.Codigo);
                        cmd.Parameters.AddWithValue("@CodQrUrl", string.IsNullOrEmpty(oModel.CodQrUrl) ? "" : oModel.CodQrUrl);
                        cmd.Parameters.AddWithValue("@ImagenEstUrl", string.IsNullOrEmpty(oModel.ImagenEstUrl) ? "" : oModel.ImagenEstUrl);
                        cmd.Parameters.AddWithValue("@Estado", oModel.Estado);
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
                    case 1: // Duplicado
                        response.Estado = false;
                        response.Valor = "warning";
                        response.Mensaje = "Ocurrio un problema el Codigo o Ci ya existe.";
                        break;

                    case 2: // Registro Nuevo
                        response.Estado = true;
                        response.Valor = "success";
                        response.Mensaje = "Registrado correctamente.";
                        break;

                    case 3: // Actualización
                        response.Estado = true;
                        response.Valor = "success";
                        response.Mensaje = "Actualizado correctamente.";
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
