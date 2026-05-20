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
    public class DUsuarios
    {
        #region "PATRON SINGLETON"
        private static DUsuarios conexion = null;

        private DUsuarios() { }

        public static DUsuarios GetInstance()
        {
            if (conexion == null)
            {
                conexion = new DUsuarios();
            }
            return conexion;
        }
        #endregion

        public Respuesta<List<ERoles>> ListarRoles()
        {
            try
            {
                List<ERoles> rptLista = new List<ERoles>();
                using (SqlConnection con = ConexionBD.GetInstance().ConexionDB())
                {
                    //sp_ListarRoles
                    using (SqlCommand comando = new SqlCommand("usp_ListaRoles", con))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        using (SqlDataReader dr = comando.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                rptLista.Add(new ERoles
                                {
                                    IdRol = Convert.ToInt32(dr["IdRol"]),
                                    NombreRol = dr["NombreRol"].ToString(),
                                    Estado = Convert.ToBoolean(dr["Estado"])
                                });
                            }
                        }
                    }
                }
                return new Respuesta<List<ERoles>>
                {
                    Estado = true,
                    Data = rptLista,
                    Mensaje = "Lista obtenida correctamente"
                };
            }
            catch (Exception ex)
            {
                return new Respuesta<List<ERoles>>
                {
                    Estado = false,
                    Data = null,
                    Mensaje = $"Error al obtener la lista: {ex.Message}"
                };
            }
        }

        public Respuesta<int> GuardarOrEditUsuarios(EUsuarios oModel)
        {
            Respuesta<int> response = new Respuesta<int>();
            int resultadoCodigo = 0;
            try
            {
                using (SqlConnection con = ConexionBD.GetInstance().ConexionDB())
                {
                    using (SqlCommand cmd = new SqlCommand("usp_GuardarOrEditUsuarios", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdUsuario", oModel.IdUsuario);
                        cmd.Parameters.AddWithValue("@IdRol", oModel.IdRol);
                        cmd.Parameters.AddWithValue("@NombreUsuario", oModel.NombreUsuario);
                        cmd.Parameters.AddWithValue("@ApellidosUsuario", oModel.ApellidosUsuario);
                        cmd.Parameters.AddWithValue("@CiUsuario", oModel.CiUsuario);
                        cmd.Parameters.AddWithValue("@Correo", oModel.Correo);
                        // Blindaje contra nulos en la Clave (Si es Update, puede que venga nula. La mandamos vacía para que el SP la ignore)
                        cmd.Parameters.AddWithValue("@Contrasena", string.IsNullOrEmpty(oModel.Contrasena) ? "" : oModel.Contrasena);
                        cmd.Parameters.AddWithValue("@FotoUrl", string.IsNullOrEmpty(oModel.FotoUrl) ? "" : oModel.FotoUrl);
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
                        response.Mensaje = "Ocurrio un problema el Correo o Ci ya existe.";
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

        public Respuesta<List<EUsuarios>> ListarUsuarios()
        {
            try
            {
                List<EUsuarios> rptLista = new List<EUsuarios>();

                using (SqlConnection con = ConexionBD.GetInstance().ConexionDB())
                {
                    using (SqlCommand comando = new SqlCommand("usp_ListaUsuarios", con))
                    {
                        comando.CommandType = CommandType.StoredProcedure;

                        con.Open();

                        using (SqlDataReader dr = comando.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                rptLista.Add(new EUsuarios
                                {
                                    IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                                    IdRol = Convert.ToInt32(dr["IdRol"]),
                                    NombreRol = dr["NombreRol"].ToString(),
                                    NombreUsuario = dr["NombreUsuario"].ToString(),
                                    ApellidosUsuario = dr["ApellidosUsuario"].ToString(),
                                    CiUsuario = dr["CiUsuario"].ToString(),
                                    Correo = dr["Correo"].ToString(),
                                    FotoUrl = dr["FotoUrl"].ToString(),
                                    Estado = Convert.ToBoolean(dr["Estado"])
                                });
                            }
                        }
                    }
                }
                return new Respuesta<List<EUsuarios>>()
                {
                    Estado = true,
                    Data = rptLista,
                    Mensaje = "Lista obtenidos correctamente"
                };
            }
            catch (Exception ex)
            {
                // Maneja cualquier error inesperado
                return new Respuesta<List<EUsuarios>>()
                {
                    Estado = false,
                    Mensaje = "Ocurrió un error: " + ex.Message,
                    Data = null
                };
            }
        }

        public Respuesta<EUsuarios> LoginUsuario(string Correo)
        {
            try
            {
                EUsuarios obj = null;

                using (SqlConnection con = ConexionBD.GetInstance().ConexionDB())
                {
                    using (SqlCommand comando = new SqlCommand("usp_LoginUsuario", con))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Parameters.AddWithValue("@Correo", Correo);

                        con.Open();
                        using (SqlDataReader dr = comando.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                obj = new EUsuarios
                                {
                                    IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                                    IdRol = Convert.ToInt32(dr["IdRol"]),
                                    NombreUsuario = dr["NombreUsuario"].ToString(),
                                    ApellidosUsuario = dr["ApellidosUsuario"].ToString(),
                                    CiUsuario = dr["CiUsuario"].ToString(),
                                    Correo = dr["Correo"].ToString(),
                                    Contrasena = dr["Contrasena"].ToString(),
                                    FotoUrl = dr["FotoUrl"].ToString(),
                                    Estado = Convert.ToBoolean(dr["Estado"]),
                                    NombreRol = dr["NombreRol"].ToString(),
                                };
                            }
                        }
                    }
                }

                return new Respuesta<EUsuarios>
                {
                    Estado = obj != null,
                    Data = obj,
                    Mensaje = obj != null ? "Bienvenido usuario" : "Usuario o Contraseña incorrectos."
                };
            }
            catch (Exception)
            {
                return new Respuesta<EUsuarios>
                {
                    Estado = false,
                    Mensaje = "Ocurrió un error en el servidor. Intente más tarde.",
                    Data = null
                };
            }
        }
    }

}

