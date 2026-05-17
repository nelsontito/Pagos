using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace CapaPresentacion.App_Start
{
    public class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // 1. Configuración de CORS (Permite peticiones desde la App de React Native)
            // El asterisco (*) significa que permites peticiones de cualquier origen, cabecera y método.
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);
            // Configuración y servicios de API web

            // Rutas de API web
            // 2. Habilitar rutas por Atributos (Ej: [Route("api/clientes")])
            config.MapHttpAttributeRoutes();

            // 3. Ruta por defecto de la API
            // Te sugiero agregar {action} para poder tener varios métodos en un mismo controlador
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                //routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // 3. Opcional: Forzar a que responda en JSON y no en XML
            //var formatter = config.Formatters.JsonFormatter;
            //formatter.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
            //config.Formatters.Remove(config.Formatters.XmlFormatter);

            //config.EnsureInitialized();
        }
    }
}