using CapaEntida.Entidades;
using CapaEntida.Responses;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CapaPresentacion
{
    public partial class PageCarreras : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        public static Respuesta<List<ECarreras>> ObtenerCarrerasPorGrado(int IdGradoAcademico)
        {
            return NCarreras.GetInstance().ObtenerCarrerasPorGrado(IdGradoAcademico);
        }

        [WebMethod]
        public static Respuesta<int> GuardarOrEditCarrera(ECarreras objeto)
        {
            return NCarreras.GetInstance().GuardarOrEditCarrera(objeto);
        }
    }
}