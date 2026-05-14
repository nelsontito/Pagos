using CapaEntida.Entidades;
using CapaEntida.Responses;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CapaPresentacion
{
    public partial class PageGrados : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static Respuesta<List<EGrados>> ListaGradosAcademicos()
        {
            return NGrados.GetInstance().ListaGradosAcademicos();
        }

        [WebMethod]
        public static Respuesta<int> GuardarOrEditGradoAcademicos(EGrados objeto)
        {
            return NGrados.GetInstance().usp_GuardarOrEditGradoAcademicos(objeto);
        }
    }
}