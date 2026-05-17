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
    public partial class PageInscripciones : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static Respuesta<List<ESemestres>> ListaSemestres()
        {
            return NGestiones.GetInstance().ListaSemestres();
        }

        [WebMethod]
        public static Respuesta<List<EEstudiantes>> FiltroEstudiantes(string busqueda)
        {
            return NEstudiantes.GetInstance().FiltroEstudiantes(busqueda);
        }

        [WebMethod]
        public static Respuesta<int> Registrar(EInscripciones objeto)
        {
            return NInscripciones.GetInstance().Registrar(objeto);
        }

    }
}