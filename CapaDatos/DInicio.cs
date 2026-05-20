using CapaEntida.Entidades;
using CapaEntida.Responses;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class DInicio
    {
        #region PATRON SINGLETON

        private static DInicio daoInicio = null;

        private DInicio() { }

        public static DInicio GetInstance()
        {
            if (daoInicio == null)
            {
                daoInicio = new DInicio();
            }

            return daoInicio;
        }

        #endregion

        public Respuesta<EInicio> ObtenerResumenDashboard()
        {
            try
            {
                EInicio obj = new EInicio();

                using (SqlConnection con = ConexionBD.GetInstance().ConexionDB())
                {
                    con.Open();

                    // ESTUDIANTES
                    using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM ESTUDIANTES", con))
                    {
                        obj.TotalEstudiantes = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    // USUARIOS
                    using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM USUARIOS", con))
                    {
                        obj.TotalUsuarios = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    // CARRERAS
                    using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM CARRERAS", con))
                    {
                        obj.TotalCarreras = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    // MENSUALIDADES
                    using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM CONTROL_MENSUALIDADES", con))
                    {
                        obj.TotalMensualidades = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }

                return new Respuesta<EInicio>()
                {
                    Estado = true,
                    Data = obj,
                    Mensaje = "Dashboard obtenido correctamente"
                };
            }
            catch (Exception ex)
            {
                return new Respuesta<EInicio>()
                {
                    Estado = false,
                    Mensaje = ex.Message
                };
            }
        }
    }
}

