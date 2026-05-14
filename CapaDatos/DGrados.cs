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
    public class DGrados
    {
        #region "PATRON SINGLETON"
        private static DGrados conexion = null;

        private DGrados() { }

        public static DGrados GetInstance()
        {
            if (conexion == null)
            {
                conexion = new DGrados();
            }
            return conexion;
        }
        #endregion

        public Respuesta<List<EGrados>> usp_ObtenerGradoAcadConteo()
        {
            Respuesta<List<EGrados>> response = new Respuesta<List<EGrados>>();

            List<EGrados> lista = new List<EGrados>();

            try
            {
                using (SqlConnection con = ConexionBD.GetInstance().ConexionDB())
                {
                    SqlCommand cmd = new SqlCommand("usp_ObtenerGradoAcadConteo", con);

                    cmd.CommandType = CommandType.StoredProcedure;

                    con.Open();

                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        lista.Add(new EGrados()
                        {
                            IdGradoAcademico = Convert.ToInt32(dr["IdGradoAcademico"]),
                            GradoAcademico = dr["GradoAcademico"].ToString(),
                            Estado = Convert.ToBoolean(dr["Estado"]),
                            //NroCarreras = Convert.ToInt32(dr["NroCarreras"])
                        });
                    }
                }

                response.Estado = true;
                response.Data = lista;
            }
            catch (Exception ex)
            {
                response.Estado = false;
                response.Mensaje = ex.Message;
            }

            return response;
        }



        public Respuesta<List<EGrados>> ListaGradosAcademicos()
        {
            try
            {
                List<EGrados> rptLista = new List<EGrados>();
                using (SqlConnection con = ConexionBD.GetInstance().ConexionDB())
                {
                    using (SqlCommand comando = new SqlCommand("usp_ObtenerGradoAcadConteo", con))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        using (SqlDataReader dr = comando.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                rptLista.Add(new EGrados()
                                {
                                    IdGradoAcademico = Convert.ToInt32(dr["IdGradoAcademico"]),
                                    GradoAcademico = dr["GradoAcademico"].ToString(),
                                    Estado = Convert.ToBoolean(dr["Estado"]),
                                    NroCarreras = Convert.ToInt32(dr["NroCarreras"])
                                });
                            }
                        }
                    }
                }
                return new Respuesta<List<EGrados>>()
                {
                    Estado = true,
                    Data = rptLista,
                    Mensaje = "Lista obtenida correctamente"
                };
            }
            catch (Exception ex)
            {
                return new Respuesta<List<EGrados>>()
                {
                    Estado = false,
                    Data = null,
                    Mensaje = $"Error al obtener la lista de grados académicos: {ex.Message}"
                };
            }
        }


        public Respuesta<int> usp_GuardarOrEditGradoAcademicos(EGrados oModel)
        {
            Respuesta<int> response = new Respuesta<int>();
            int resultadoCodigo = 0;
            try
            {
                using (SqlConnection con = ConexionBD.GetInstance().ConexionDB())
                {
                    using (SqlCommand cmd = new SqlCommand("usp_GuardarOrEditGradoAcademicos", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdGradoAcademico", oModel.IdGradoAcademico);
                        cmd.Parameters.AddWithValue("@GradoAcademico", oModel.GradoAcademico);
                        //cmd.Parameters.AddWithValue("@FechaRegistro", oModel.FechaRegistro);
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
                        response.Mensaje = "Ocurrio un problema en el nivel academico ya existe.";
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

