let tablaData;
let idEditar = 0;

$(document).ready(function () {

    cargarGradosAcadeTable();
    cargarSemestre();
    cargaGestiones();
    cargarBuscadorEstudiantes();
});
function cargarGradosAcadeTable() {

    // Mostramos un texto de "Cargando..." mientras esperamos la respuesta
    $("#cboGrados").html('<option value="">Cargando grados...</option>');

    $.ajax({
        url: "PageGrados.aspx/ListaGradosAcademicos",
        type: "POST",
        data: "{}", // <-- Mejor compatibilidad con WebMethods sin parámetros
        contentType: 'application/json; charset=utf-8',
        dataType: "json",
        success: function (response) {
            if (response.d.Estado) {

                // 1. Empezamos con la opción por defecto
                let opcionesHTML = '<option value="">-- Seleccione un Grado --</option>';

                // 2. Concatenamos todas las opciones en la variable (en memoria)
                $.each(response.d.Data, function (i, row) {
                    opcionesHTML += `<option value="${row.IdGradoAcademico}">${row.GradoAcademico}</option>`;
                });

                //$.each(response.d.Data, function (i, row) {
                //    if (row.Estado === true) {
                //        opcionesHTML += `<option value="${row.IdGradoAcademico}">${row.Nombre}</option>`;
                //    }
                //});

                // 3. Inyectamos todo al DOM en un solo movimiento
                $("#cboGrados").html(opcionesHTML);

            } else {
                $("#cboGrados").html('<option value="">Error al cargar</option>');
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(xhr.status + " \n" + xhr.responseText, "\n" + thrownError);
            $("#cboGrados").html('<option value="">Error de conexión</option>');
        }
    });
}

$("#cboGrados").on("change", function () {
    const idGrados = $(this).val();

    if (idGrados) {
        cargarCarreras(idGrados);
    }
});

function cargarCarreras(idGradoAcademico) {

    // Mostramos un texto de "Cargando..." mientras esperamos la respuesta
    $("#cboCarreras").html('<option value="">Cargando grados...</option>');

    var request = {
        IdGradoAcademico: parseInt(idGradoAcademico)
    }
    $.ajax({
        url: "PageCarreras.aspx/ObtenerCarrerasPorGrado",
        type: "POST",
        data: JSON.stringify(request), // <-- Mejor compatibilidad con WebMethods con parámetros
        contentType: 'application/json; charset=utf-8',
        dataType: "json",
        success: function (response) {
            if (response.d.Estado) {

                // 1. Empezamos con la opción por defecto
                let opcionesHTML = '<option value="">-- Seleccione un Carrera --</option>';

                // 2. Concatenamos todas las opciones en la variable (en memoria)
                $.each(response.d.Data, function (i, row) {
                    opcionesHTML += `<option value="${row.IdCarrera}">${row.NombreCarrera}</option>`;
                });

                //$.each(response.d.Data, function (i, row) {
                //    if (row.Estado === true) {
                //        opcionesHTML += `<option value="${row.IdGradoAcademico}">${row.Nombre}</option>`;
                //    }
                //});

                // 3. Inyectamos todo al DOM en un solo movimiento
                $("#cboCarreras").html(opcionesHTML);

            } else {
                $("#cboCarreras").html('<option value="">Error al cargar</option>');
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(xhr.status + " \n" + xhr.responseText, "\n" + thrownError);
            $("#cboCarreras").html('<option value="">Error de conexión</option>');
        }
    });
}

function cargarSemestre() {

    // Mostramos un texto de "Cargando..." mientras esperamos la respuesta
    $("#cboSemestre").html('<option value="">Cargando Semestres...</option>');

    $.ajax({
        url: "PageInscripciones.aspx/ListaSemestres",
        type: "POST",
        data: "{}", // <-- Mejor compatibilidad con WebMethods sin parámetros
        contentType: 'application/json; charset=utf-8',
        dataType: "json",
        success: function (response) {
            if (response.d.Estado) {

                // 1. Empezamos con la opción por defecto
                let opcionesHTML = '<option value="">-- Seleccione un Grado --</option>';

                // 2. Concatenamos todas las opciones en la variable (en memoria)
                $.each(response.d.Data, function (i, row) {
                    opcionesHTML += `<option value="${row.IdSemestre}">${row.NombreSemestre}</option>`;
                });

                //$.each(response.d.Data, function (i, row) {
                //    if (row.Estado === true) {
                //        opcionesHTML += `<option value="${row.IdGradoAcademico}">${row.Nombre}</option>`;
                //    }
                //});

                // 3. Inyectamos todo al DOM en un solo movimiento
                $("#cboSemestre").html(opcionesHTML);

            } else {
                $("#cboSemestre").html('<option value="">Error al cargar</option>');
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(xhr.status + " \n" + xhr.responseText, "\n" + thrownError);
            $("#cboSemestre").html('<option value="">Error de conexión</option>');
        }
    });
}

function cargaGestiones() {

    // Mostramos un texto de "Cargando..." mientras esperamos la respuesta
    $("#cboPeriodo").html('<option value="">Cargando Gestion...</option>');

    $.ajax({
        url: "PageGestiones.aspx/ListaGestiones",
        type: "POST",
        data: "{}", // <-- Mejor compatibilidad con WebMethods sin parámetros
        contentType: 'application/json; charset=utf-8',
        dataType: "json",
        success: function (response) {
            if (response.d.Estado) {

                // 1. Empezamos con la opción por defecto
                let opcionesHTML = '<option value="">-- Seleccione un Periodo --</option>';

                // 2. Concatenamos todas las opciones en la variable (en memoria)
                // $.each(response.d.Data, function (i, row) {
                //     opcionesHTML += `<option value="${row.IdGestion}">${row.NombreGestion}</option>`;
                // });

                $.each(response.d.Data, function (i, row) {
                    if (row.Estado === true) {
                        opcionesHTML += `<option value="${row.IdGestion}">${row.NombreGestion}</option>`;
                    }
                });

                // 3. Inyectamos todo al DOM en un solo movimiento
                $("#cboPeriodo").html(opcionesHTML);

            } else {
                $("#cboPeriodo").html('<option value="">Error al cargar</option>');
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(xhr.status + " \n" + xhr.responseText, "\n" + thrownError);
            $("#cboPeriodo").html('<option value="">Error de conexión</option>');
        }
    });
}



// inicio configuracion docentes
function cargarBuscadorEstudiantes() {
    $("#cboBuscarEstudiante").select2({
        ajax: {
            url: "PageInscripciones.aspx/FiltroEstudiantes",
            dataType: 'json',
            type: "POST",
            contentType: "application/json; charset=utf-8",
            delay: 250,
            data: function (params) {
                return JSON.stringify({ busqueda: params.term });
            },
            processResults: function (data) {
                return {
                    results: data.d.Data.map((item) => ({
                        id: item.IdEstudiante,
                        text: item.Nombres + ' ' + item.Apellidos,
                        nroCi: item.NroCi,
                        celular: item.Celular,
                        Codigo: item.Codigo,
                        imagen: item.ImagenEstUrl,
                        dataCompleta: item
                    }))
                };
            },
            error: function (xhr, ajaxOptions, thrownError) {
                console.log(xhr.status + " \n" + xhr.responseText, "\n" + thrownError);
            }
        },
        language: "es",
        placeholder: 'Buscar por Nombre o CI...',
        minimumInputLength: 3,
        templateResult: formatoResultadosDocente
    });
}

function formatoResultadosDocente(data) {
    if (data.loading) return data.text;

    var imagenMostrar = data.imagen ? data.imagen : 'Imagenes/images.png';

    var contenedor = $(
        `<div class="d-flex align-items-center">
            <img src="${imagenMostrar}" style="height:40px; width:40px; margin-right:10px; border-radius:50%; object-fit:cover;"/>
            <div>
                <div style="font-weight: bold;">${data.text}</div>
                <div style="font-size: 0.85em; color: #666;">CI: ${data.nroCi} | Cel: ${data.celular}</div>
            </div>
         </div>`
    );

    return contenedor;
}

$("#cboBuscarEstudiante").on("select2:select", function (e) {
    const data = e.params.data;
    $("#txtIdEstudiante").val(data.id);
    $("#lblNombres").text("Est: " + data.text);
    $("#lblDatos").text("Nro CI: " + data.nroCi);
    $("#imgEst").attr("src", data.imagen || "Imagenes/images.png");
    $("#cboBuscarEstudiante").val(null).trigger("change");
});


// 2. EVENTO: NUEVO REGISTRO
$("#btnNuevoRegistro").on("click", function () {
    limpiarFormulario();
    $('#btnGuardarRegistro').prop('disabled', false); // Habilitamos para registrar

});


function limpiarFormulario() {
    idEditar = 0;

    $("#cboGrados").val("");
    $("#cboCarreras").val("");
    $("#cboSemestre").val("");
    $("#cboPeriodo").val("");

    $("#txtEstudiante").val("0");

    // Restablecer visuales del docente
    $("#lblNombres").text("Esperando..");
    $("#lblDatos").text("Esperando..");
    $("#imgEst").attr("src", "Imagenes/images.png");

    // Bloquear el botón de guardar y regresar su texto original
    $('#btnGuardarRegistro').prop('disabled', true);
    $('#btnGuardarRegistro').html('<i class="fas fa-save mr-2"></i>Guardar Registro').removeClass('btn-warning').addClass('btn-success');
}

function habilitarBoton() {
    $('#btnGuardarRegistro').prop('disabled', false);
}

$("#btnGuardarRegistro").on("click", function () {

    $('#btnGuardarRegistro').prop('disabled', true);

    let idGestion = $("#cboPeriodo").val();
    let idCarrera = $("#cboCarreras").val();
    let idSemestre = $("#cboSemestre").val();

    let idEstudiante = $("#txtIdEstudiante").val().trim();

    if (idEstudiante === "0" || idEstudiante === "") {
        MostrarToastZer("Por favor, Seleccione un Estudiante", "Atención", "warning");
        habilitarBoton();
        return;
    }

    if (idGestion === "") {
        MostrarToastZer("Por favor, seleccione una Gestion.", "Atención", "warning");
        $("#cboPeriodo").focus();
        habilitarBoton();
        return;
    }

    if (idCarrera === "") {
        MostrarToastZer("Por favor, seleccione una Carrera.", "Atención", "warning");
        $("#cboCarreras").focus();
        habilitarBoton();
        return;
    }

    if (idSemestre === "") {
        MostrarToastZer("Por favor, seleccione un Semestre.", "Atención", "warning");
        $("#cboSemestre").focus();
        habilitarBoton();
        return;
    }

    const objeto = {
        //IdAsignacion: idEditar,
        IdEstudiante: parseInt(idEstudiante),
        IdCarrera: parseInt(idCarrera),
        IdSemestre: parseInt(idSemestre),
        IdGestion: parseInt(idGestion)
    }

    $("#loadinzero").LoadingOverlay("show");

    $.ajax({
        type: "POST",
        url: "PageInscripciones.aspx/Registrar",
        data: JSON.stringify({ objeto: objeto }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            $("#loadinzero").LoadingOverlay("hide");

            AlertaTimerTipo(
                response.d.Estado ? '¡Excelente!' : 'Atención',
                response.d.Mensaje,
                response.d.Valor
            );

            if (response.d.Estado) {

                // if (tablaData) {
                //     tablaData.ajax.reload(null, false);
                // }

                // LIMPIAR TODO EL FORMULARIO Y BLOQUEAR BOTÓN
                limpiarFormulario();
            }

        },
        error: function (xhr) {
            console.log(xhr.responseText);
            $("#loadinzero").LoadingOverlay("hide");
            MostrarToastZer("No se pudo conectar con el servidor.", "Atención", "error");
        },
        //complete: function () {
        //    habilitarBoton();
        //}
    });

})
