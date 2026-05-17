using CapaEntida.DTOs;
using CapaEntida.Responses;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace CapaPresentacion.Api
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    // 2. Definimos la ruta base para este controlador
    [RoutePrefix("api/estudiantes")]
    public class EstudiantesController : ApiController
    {

        // (Opcional) Aquí mismo podrías poner un [HttpGet] para listar clientes si lo necesitas
        [HttpPost]
        [Route("consulta")]
        public IHttpActionResult ConsultaMensualidad(RequestDTO request)
        {
            if (request == null || string.IsNullOrEmpty(request.Codigo))
            {
                var respuestaError = new Respuesta<List<ConsulMenDTO>>
                {
                    Estado = false,
                    Mensaje = "No se obtuvo datos del codigo QR.",
                    Data = null
                };

                return Ok(respuestaError);
            }

            var respuesta = NInscripciones.GetInstance().ConsultaMensualidad(request.Codigo);
            return Ok(respuesta);
        }
    }
}