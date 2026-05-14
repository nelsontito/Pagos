let tablaData;
let idEditar = 0;

$(document).ready(function () {
    ListarEstudiantes();
});


function ListarEstudiantes() {
    //if ($.fn.DataTable.isDataTable("#tbData")) {
    //    $("#tbData").DataTable().destroy();
    //    $('#tbData tbody').empty();
    //}

    tablaData = $("#tbDatas").DataTable({
        // Muestra el mensaje "Procesando..." nativo de DataTables
        "processing": true,
        "serverSide": true,  // ACTIVA EL MODO PAGINACIÓN EN EL SERVIDOR
        "responsive": true,
        "ajax": {
            "url": 'PageEstudiantes.aspx/ListaEstudiantesPaginado',
            "type": "POST",
            "contentType": "application/json; charset=utf-8",
            "dataType": "json",
            "data": function (d) {

                // GUARDAMOS EL DRAW EN UNA VARIABLE GLOBAL TEMPORAL (o en el mismo 'd')
                // para poder leerlo después en el dataFilter
                window.currentDraw = d.draw;

                // 'd' es el objeto gigante que DataTables intenta enviar por defecto.
                // Aquí lo transformamos para que encaje EXACTAMENTE con los parámetros de tu WebMethod en C#
                var parametros = {
                    Omitir: d.start,               // Cuántos registros saltar (Página actual)
                    TamanoPagina: d.length,        // Cuántos registros mostrar (Ej: 10, 25, 50)
                    Buscar: d.search.value || ""   // Lo que el usuario escribió en la caja de búsqueda
                };
                return JSON.stringify(parametros);
            },
            "dataFilter": function (data) {
                // Aquí interceptamos la respuesta cruda de tu WebMethod antes de que DataTables la lea
                var json = JSON.parse(data);

                // Extraemos los totales de la primera fila (si hay datos)
                var totalRecords = 0;
                var totalFiltered = 0;

                if (json.d.Estado && json.d.Data.length > 0) {
                    totalRecords = json.d.Data[0].TotalRegistros;
                    totalFiltered = json.d.Data[0].TotalFiltrados;
                }

                // Transformamos tu objeto "Respuesta" al formato que DataTables exige
                var respuestaDataTables = {
                    // No es estrictamente necesario en WebForms, pero es buena práctica
                    //draw: 0,
                    draw: window.currentDraw, // <-- ¡AHORA SÍ LE DEVOLVEMOS SU DRAW ORIGINAL!
                    recordsTotal: totalRecords,       // Total real en BD
                    recordsFiltered: totalFiltered,   // Total después de aplicar el buscador
                    data: json.d.Estado ? json.d.Data : [] // La lista de estudiantes real
                };

                return JSON.stringify(respuestaDataTables);
            }
        },
        "columns": [
            { "data": "IdEstudiante", "visible": false, "searchable": false },
            {
                "data": "ImagenEstUrl",
                className: "align-middle",
                render: function (data) {
                    let foto = data && data !== "" ? data : "Imagenes/images.png";
                    return `<img src="${foto}" alt="Foto" style="width:15px;height:15px;object-fit:cover;">`;
                }
            },

            {
                "data": null,
                className: "align-middle",
                render: function (data, type, row) {
                    return `${row.Nombres} ${row.Apellidos}`;
                }
            },

            { "data": "NroCi", className: "align-middle" },

            { "data": "Codigo", className: "align-middle" },
            { "data": "Celular", className: "align-middle" },

          
            // 4. COLUMNA OPCIONES
            {
                "defaultContent": `
                    <button class="btn btn-primary btn-editar btn-sm mr-2" title="Editar">
                        <i class="fas fa-pencil-alt"></i>
                    </button>
                    <button class="btn btn-info btn-detalle btn-sm" title="Ver Detalles">
                        <i class="fas fa-address-book"></i>
                    </button>`,
                "orderable": false,
                "searchable": false,
                className: "text-center align-middle"
            }
        ],
        // IMPORTANTE: En modo Server-Side con tu SP actual, no estamos manejando ordenamiento dinámico por columnas,
        // así que desactivamos el ordenamiento inicial para que no choque con el ORDER BY DESC de tu SP.
        "order": [],
        "language": {
            "url": "https://cdn.datatables.net/plug-ins/1.11.5/i18n/es-ES.json"
        }
    });
}

$('#tbDatas tbody').on('click', '.btn-editar', function () {

    let fila = $(this).closest('tr');
    if (fila.hasClass('child')) {
        fila = fila.prev();
    }

    let data = tablaData.row(fila).data();

    idEditar = data.IdEstudiante;

    // llenar el modal
    $("#txtNombres").val(data.Nombres);
    $("#txtApellidos").val(data.Apellidos);
    $("#txtNroCI").val(data.NroCi);
    $("#txtCodigo").val(data.Codigo);
    $("#txtTelefono").val(data.Celular);

    $("#cboEstado").val(data.Estado ? 1 : 0);

    $('#imgDirectReg').attr('src', data.ImagenEstUrl || "Imagenes/images.png");

    $("#cboEstado").prop("disabled", false);

    $("#myModalLabel").text("Editar Estudiante");

    $("#mdData").modal("show");
});

$('#tbDatas  tbody').on('click', '.btn-detalle', function () {

    let fila = $(this).closest('tr');
    if (fila.hasClass('child')) {
        fila = fila.prev();
    }

    let data = tablaData.row(fila).data();

    alert("Estudiante: " + data.Nombres + "\nCodigo: " + data.Codigo);
});

$("#btnNuevo").on("click", function () {

    idEditar = 0;

    $("#txtNombres").val("");
    $("#txtApellidos").val("");
    $("#txtNroCI").val("");
    $("#txtCodigo").val("");
    $("#txtTelefono").val("");

    $("#cboEstado").val(1).prop("disabled", true);

    $('#imgDirectReg').attr('src', "Imagenes/images.png");
    $("#txtFotoUr").val("");
    $(".custom-file-label").text('Ningún archivo seleccionado');

    $("#myModalLabel").text("Nuevo Usuario");
    $("#mdData").modal("show");
});

const TAMANO_MAXIMO = 2 * 1024 * 1024; // 2 MB en bytes

function esImagen(file) {
    return file && file.type.startsWith("image/");
}

function mostrarImagenSeleccionada(input) {
    let file = input.files[0];
    let reader = new FileReader();

    // Si NO se seleccionó archivo (ej: presionaron "Cancelar")
    if (!file) {
        resetearVistaFoto(input);
        return;
    }

    // Validación: si no es imagen, mostramos error
    if (!esImagen(file)) {
        //MostrarToastZer("El archivo seleccionado no es una imagen válida.", "Atención", "error");
        toastr.error("El archivo seleccionado no es una imagen válida.");
        resetearVistaFoto(input);
        return;
    }

    // 3. Validación: Tamaño máximo
    if (file.size > TAMANO_MAXIMO) {
        toastr.error("La imagen supera el tamaño máximo permitido de 2 MB.");
        resetearVistaFoto(input);
        return;
    }

    // Si todo es válido → mostrar vista previa
    reader.onload = (e) => $('#imgDirectReg').attr('src', e.target.result);
    reader.readAsDataURL(file);

    // Mostrar nombre del archivo
    $(input).next('.custom-file-label').text(file.name);
}
// Función auxiliar para limpiar (DRY - Don't Repeat Yourself)
function resetearVistaFoto(input) {
    $('#imgDirectReg').attr('src', "Imagenes/images.png");
    $(input).next('.custom-file-label').text('Ningún archivo seleccionado');
    input.value = ""; // Limpia el input file
}

$('#txtFotoUr').change(function () {
    mostrarImagenSeleccionada(this);
});



function habilitarBoton() {
    $('#btnGuardarCambios').prop('disabled', false);
}

$("#btnGuardarCambios").on("click", function () {
    // Bloqueo inmediato
    $('#btnGuardarCambios').prop('disabled', true);

    const inputs = $("#mdData input.model").serializeArray();
    const inputs_sin_valor = inputs.filter(item => item.value.trim() === "");

    if (inputs_sin_valor.length > 0) {
        const mensaje = `Debe completar el campo: "${inputs_sin_valor[0].name}"`;
        //MostrarToastZer(mensaje, "Atención", "warning");
        toastr.warning("", mensaje)
        $(`input[name="${inputs_sin_valor[0].name}"]`).focus();
        habilitarBoton();
        return;
    }

    // 2. ARMAR EL OBJETO
    const objeto = {
        IdEstudiante: idEditar,
        Nombres: $("#txtNombres").val().trim(),
        Apellidos: $("#txtApellidos").val().trim(),
        NroCi: $("#txtNroCI").val().trim(),
        Codigo: $("#txtCodigo").val().trim(),
        Celular: $("#txtTelefono").val().trim(),
        Estado: ($("#cboEstado").val() === "1"),
        ImagenEstUrl: ""
    };

    const fileInput = document.getElementById('txtFotoUr');
    const file = fileInput.files[0];

    if (file) {
        const reader = new FileReader();
        reader.onload = function (e) {
            const base64String = e.target.result.split(',')[1];

            enviarAjaxEstudiantes(objeto, base64String);
        };
        reader.readAsDataURL(file);
    } else {
        enviarAjaxEstudiantes(objeto, "");
    }

});

function enviarAjaxEstudiantes(objeto, base64String) {
    $("#mdData").find("div.modal-content").LoadingOverlay("show");

    $.ajax({
        type: "POST",
        url: "PageEstudiantes.aspx/GuardarOrEditEstudiantes",
        data: JSON.stringify({ objeto: objeto, base64Image: base64String }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            $("#mdData").find("div.modal-content").LoadingOverlay("hide");

            AlertaTimerTipo(
                response.d.Estado ? '¡Excelente!' : 'Atención', // Título dinámico
                response.d.Mensaje, // Texto del servidor
                response.d.Valor // Icono (success/error/warning)
            );

            if (response.d.Estado) {
                $("#mdData").modal("hide");
                idEditar = 0;
                tablaData.ajax.reload(null, false);
            }
        },
        error: function () {
            $("#mdData").find("div.modal-content").LoadingOverlay("hide");
            toastr.error("No se pudo conectar con el servidor.");
        },
        complete: function () {
            habilitarBoton();
        }
    });

}

// fin