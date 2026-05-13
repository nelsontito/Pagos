using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class ConexionBD
    {
        #region "PATRON SINGLETON"
        private static ConexionBD conexion = null;

        private ConexionBD() { }

        public static ConexionBD GetInstance()
        {
            if (conexion == null)
            {
                conexion = new ConexionBD();
            }
            return conexion;
        }
        #endregion

        public SqlConnection ConexionDB()
        {
            SqlConnection conexion = new SqlConnection
            {
                //ConnectionString = @"Data Source=SQL1001.site4now.net;Initial Catalog=db_abec4f_exportacion;User Id=db_abec4f_exportacion_admin;Password=xxxxx"
                ConnectionString = "Data Source=NELSON1;Initial Catalog=Mensualidades;Integrated Security=True"
            };

            return conexion;
        }
    }
}
