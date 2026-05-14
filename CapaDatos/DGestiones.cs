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
    public class DGestiones
    {
        #region "PATRON SINGLETON"
        private static DGestiones conexion = null;

        private DGestiones() { }

        public static DGestiones GetInstance()
        {
            if (conexion == null)
            {
                conexion = new DGestiones();
            }
            return conexion;
        }
        #endregion

        public Respuesta<List<ESemestres>> ListaSemestres()
        {
            try
            {
                List<ESemestres> rptLista = new List<ESemestres>();
                using (SqlConnection con = ConexionBD.GetInstance().ConexionDB())
                {
                    using (SqlCommand comando = new SqlCommand("usp_ListaSemestres", con))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        using (SqlDataReader dr = comando.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                rptLista.Add(new ESemestres()
                                {
                                    IdSemestre = Convert.ToInt32(dr["IdSemestre"]),
                                    NombreSemestre = dr["NombreSemestre"].ToString(),
                                });
                            }
                        }
                    }
                }
                return new Respuesta<List<ESemestres>>()
                {
                    Estado = true,
                    Data = rptLista,
                    Mensaje = "Lista obtenida correctamente"
                };
            }
            catch (Exception ex)
            {
                return new Respuesta<List<ESemestres>>()
                {
                    Estado = false,
                    Data = null,
                    Mensaje = $"Error al obtener la lista de grados académicos: {ex.Message}"
                };
            }
        }


        public Respuesta<List<EGestiones>> ListaGestiones()
        {
            try
            {
                List<EGestiones> rptLista = new List<EGestiones>();
                using (SqlConnection con = ConexionBD.GetInstance().ConexionDB())
                {
                    using (SqlCommand comando = new SqlCommand("usp_ListaGestiones", con))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        using (SqlDataReader dr = comando.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                rptLista.Add(new EGestiones()
                                {
                                    IdGestion = Convert.ToInt32(dr["IdGestion"]),
                                    NombreGestion = dr["NombreGestion"].ToString(),
                                    IdMesInicio = Convert.ToInt32(dr["IdMesInicio"]),
                                    IdMesFin = Convert.ToInt32(dr["IdMesFin"]),
                                    MesIni = dr["MesIni"].ToString(),
                                    MesFin = dr["MesFin"].ToString(),
                                    Estado = Convert.ToBoolean(dr["Estado"])
                                });
                            }
                        }
                    }
                }
                return new Respuesta<List<EGestiones>>()
                {
                    Estado = true,
                    Data = rptLista,
                    Mensaje = "Lista obtenida correctamente"
                };
            }
            catch (Exception ex)
            {
                return new Respuesta<List<EGestiones>>()
                {
                    Estado = false,
                    Data = null,
                    Mensaje = $"Error al obtener la lista de grados académicos: {ex.Message}"
                };
            }
        }


        public Respuesta<int> GuardarOrEditGestiones(EGestiones oModel)
        {
            Respuesta<int> response = new Respuesta<int>();
            int resultadoCodigo = 0;
            try
            {
                using (SqlConnection con = ConexionBD.GetInstance().ConexionDB())
                {
                    using (SqlCommand cmd = new SqlCommand("usp_GuardarOrEditGestiones", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdGestion", oModel.IdGestion);
                        cmd.Parameters.AddWithValue("@NombreGestion", oModel.NombreGestion);
                        cmd.Parameters.AddWithValue("@IdMesInicio", oModel.IdMesInicio);
                        cmd.Parameters.AddWithValue("@IdMesFin", oModel.IdMesFin);
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
                        response.Mensaje = "Ocurrio un problema en la gestion ya existe.";
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

                    case 4: // Validacion
                        response.Estado = true;
                        response.Valor = "success";
                        response.Mensaje = "El mes Inicio no puede ser mayor que el mes de Fin .";
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

        public Respuesta<List<EMeses>> ListaMeses()
        {
            try
            {
                List<EMeses> rptLista = new List<EMeses>();
                using (SqlConnection con = ConexionBD.GetInstance().ConexionDB())
                {
                    using (SqlCommand comando = new SqlCommand("usp_ListaMeses", con))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        using (SqlDataReader dr = comando.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                rptLista.Add(new EMeses()
                                {
                                    IdMes = Convert.ToInt32(dr["IdMes"]),
                                    NombreMes = dr["NombreMes"].ToString(),                                  
                                });
                            }
                        }
                    }
                }
                return new Respuesta<List<EMeses>>()
                {
                    Estado = true,
                    Data = rptLista,
                    Mensaje = "Lista obtenida correctamente"
                };
            }
            catch (Exception ex)
            {
                return new Respuesta<List<EMeses>>()
                {
                    Estado = false,
                    Data = null,
                    Mensaje = $"Error al obtener la lista de grados académicos: {ex.Message}"
                };
            }
        }

    }
}
