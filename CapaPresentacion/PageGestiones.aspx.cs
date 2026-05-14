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
    public partial class PageGestiones : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        public static Respuesta<List<EMeses>> ListaMeses()
        {
            return NGestiones.GetInstance().ListaMeses();
        }

        [WebMethod]
        public static Respuesta<List<EGestiones>> ListaGestiones()
        {
            return NGestiones.GetInstance().ListaGestiones();
        }

        [WebMethod]
        public static Respuesta<int> GuardarOrEditGestiones(EGestiones objeto)
        {
            return NGestiones.GetInstance().GuardarOrEditGestiones(objeto);
        }


    }
}