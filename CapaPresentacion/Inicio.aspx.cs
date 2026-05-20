using CapaEntida.Entidades;
using CapaEntida.Responses;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CapaPresentacion
{
    public partial class Inicio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [System.Web.Services.WebMethod]
        public static Respuesta<EInicio> ObtenerResumenDashboard()
        {
            return NInicio.GetInstance().ObtenerResumenDashboard();
        }
    }
}