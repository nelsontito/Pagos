using CapaEntida.DTOs;
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
    public partial class PagePagos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static Respuesta<List<ConsulMenDTO>> ConsultaMensualidad(string Codigo)
        {
            return NInscripciones.GetInstance().ConsultaMensualidad(Codigo);
        }

        [WebMethod]
        public static Respuesta<int> PagarMensualidad(int IdControl)
        {
            return NInscripciones.GetInstance().PagarMensualidad(IdControl);
        }
    }
}