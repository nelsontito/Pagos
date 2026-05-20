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
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.AppendHeader("Cache-Control", "no-store");
        }

        [WebMethod]
        public static Respuesta<EUsuarios> Logeo(string Correo, string Clave)
        {
            try
            {
                var resp = NUsuarios.GetInstance().LoginUsuario(Correo);
                if (!resp.Estado || resp.Data == null)
                {
                    return new Respuesta<EUsuarios>
                    {
                        Estado = false,
                        Mensaje = resp.Mensaje
                    };
                }

                var objUsua = resp.Data;

                // verificamos si está activo
                if (!objUsua.Estado)
                {
                    return new Respuesta<EUsuarios>
                    {
                        Estado = false,
                        Mensaje = "Su cuenta se encuentra inactiva. contáctese con Dep. de Sistemas."
                    };
                }

                // verificamos la contraseña (BCrypt)
                bool passCorrecta = Utilidades.GetInstance().Verify(Clave, objUsua.Contrasena);

                if (!passCorrecta)
                {
                    return new Respuesta<EUsuarios>
                    {
                        Estado = false,
                        Mensaje = "Usuario o Contraseña incorrectos."
                    };
                }

                objUsua.Contrasena = "";

                return new Respuesta<EUsuarios>
                {
                    Estado = true,
                    Data = objUsua,
                    Mensaje = "Bienvenido al sistema"
                };
            }
            catch (Exception ex)
            {
                return new Respuesta<EUsuarios>
                {
                    Estado = false,
                    Mensaje = "Ocurrió un error: " + ex.Message
                };
            }
        }
    }
}