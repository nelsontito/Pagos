using CapaEntida.DTOs;
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
    public partial class PageEstudiantes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        public static Respuesta<List<EEstudiantesDTO>> ListaEstudiantesPaginado(int Omitir, int TamanoPagina, string Buscar)
        {
            return NEstudiantes.GetInstance().ListaEstudiantesPaginado(Omitir, TamanoPagina, Buscar);
        }

        [WebMethod]
        public static Respuesta<int> GuardarOrEditEstudiantes(EEstudiantes objeto, string base64Image)
        {
            try
            {
                var utilidades = Utilidades.GetInstance();

                // 1. Manejo de la foto
                if (!string.IsNullOrEmpty(base64Image))
                {
                    byte[] imageBytes = Convert.FromBase64String(base64Image);
                    using (var stream = new MemoryStream(imageBytes))
                    {
                        string folder = "/ImgEstudiantes/";
                        objeto.ImagenEstUrl = utilidades.UploadPhoto(stream, folder);
                    }
                }
                else
                {
                    objeto.ImagenEstUrl = "";
                }

                // 2. genero el codigo y codqr solo cuando es registro una sola vez
                if (objeto.IdEstudiante == 0)
                {
                    string codigoEncryp = Servicios.Encrypt(objeto.Codigo);
                    // metodo de generacion de codigo qr
                    objeto.CodQrUrl = Utilidades.GetInstance().GenerarQr(codigoEncryp);
                }
                else
                {
                    objeto.CodQrUrl = "";
                }

                return NEstudiantes.GetInstance().GuardarOrEditEstudiantes(objeto);
            }
            catch (Exception ex)
            {
                return new Respuesta<int> { Estado = false, Valor = "error", Mensaje = "Error en el servidor: " + ex.Message };
            }
        }
    }
}