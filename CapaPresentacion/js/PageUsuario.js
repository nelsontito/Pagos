let tablaData;
let idEditar = 0;

$(document).ready(function () {
    cargarRoles();
    listarUsuarios();

});





function cargarRoles() {

    // Mostramos un texto de "Cargando..." mientras esperamos la respuesta
    $("#cboRol").html('<option value="">Cargando...</option>');

    $.ajax({
        url: "PageUsuarios.aspx/ListarRoles",
        type: "POST",
        data: "{}", // <-- Mejor compatibilidad con WebMethods sin parámetros
        contentType: 'application/json; charset=utf-8',
        dataType: "json",
        success: function (response) {
            if (response.d.Estado) {

                // 1. Empezamos con la opción por defecto
                let opcionesHTML = '<option value="">Seleccione Rol</option>';

                // 2. Concatenamos todas las opciones en la variable (en memoria)
                $.each(response.d.Data, function (i, row) {
                    opcionesHTML += `<option value="${row.IdRol}">${row.NombreRol}</option>`;
                });

                //$.each(response.d.Data, function (i, row) {
                //    if (row.Estado === true) {
                //        opcionesHTML += `<option value="${row.IdGestion}">${row.NombreGestion}</option>`;
                //    }
                //});

                // 3. Inyectamos todo al DOM en un solo movimiento
                $("#cboRol").html(opcionesHTML);

            } else {
                $("#cboRol").html('<option value="">Error al cargar</option>');
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(xhr.status + " \n" + xhr.responseText, "\n" + thrownError);
            $("#cboRol").html('<option value="">Error de conexión</option>');
        }
    });
}



function listarUsuarios() {
    if ($.fn.DataTable.isDataTable("#tbDatas")) {
        $("#tbDatas").DataTable().destroy();
        $('#tbDatas tbody').empty();
    }

    tablaData = $("#tbDatas").DataTable({
        responsive: true,
        ajax: {
            url: 'PageUsuarios.aspx/ListarUsuarios',
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: function (d) {
                return JSON.stringify(d);
            },
            dataSrc: function (json) {
                if (json.d.Estado) {
                    return json.d.Data;
                } else {
                    return [];
                }
            }
        },
        columns: [
            { data: "IdUsuario", visible: false, searchable: false },

            {
                data: "FotoUrl",
                className: "text-center align-middle",
                render: function (data) {
                    let foto = data && data !== "" ? data : "Imagenes/images.png";
                    return `<img src="${foto}" alt="Foto" class="rounded-circle" style="width:40px;height:40px;object-fit:cover;" onerror="this.src='Imagenes/images.png';">`;
                }
            },

            {
                data: null,
                className: "align-middle",
                render: function (data, type, row) {
                    return `${row.NombreUsuario} ${row.ApellidosUsuario}`;
                }
            },

            { data: "CiUsuario", className: "align-middle" },

            { data: "Correo", className: "align-middle" },

            {
                data: "Estado",
                className: "text-center align-middle",
                render: function (data) {
                    return data
                        ? '<span class="badge badge-primary">Activo</span>'
                        : '<span class="badge badge-danger">No Activo</span>';
                }
            },

            {
                defaultContent:
                    '<button class="btn btn-primary btn-editar btn-sm mr-2"><i class="fas fa-pencil-alt"></i></button>' +
                    '<button class="btn btn-info btn-detalle btn-sm"><i class="fas fa-address-book"></i></button>',
                orderable: false,
                searchable: false,
                width: "100px",
                className: "text-center align-middle"
            }
        ],
        order: [[0, "desc"]],
        language: {
            url: "https://cdn.datatables.net/plug-ins/1.11.5/i18n/es-ES.json"
        }
    });
}

$('#tbDatas tbody').on('click', '.btn-editar', function () {

    let data = tablaData.row($(this).closest('tr')).data();

    idEditar = data.IdUsuario;

    // llenar el modal
    $("#txtNombres").val(data.NombreUsuario);
    $("#txtApellidos").val(data.ApellidosUsuario);
    $("#txtCorreo").val(data.Correo);
    $("#txtNroci").val(data.CiUsuario);
    $("#cboRol").val(data.IdRol);
    $("#cboEstado").val(data.Estado ? 1 : 0);

    $('#imgDirectReg').attr('src', data.FotoUrl || "Imagenes/images.png");

    $("#cboEstado").prop("disabled", false);

    $("#myModalLabel").text("Editar Usuario");

    $("#mdData").modal("show");
});

$('#tbDatas tbody').on('click', '.btn-detalle', function () {

    let data = tablaData.row($(this).closest('tr')).data();

    alert("Usuario: " + data.NombreUsuario + "\nCorreo: " + data.Correo);
});







$("#btnRegistro").on("click", function () {

    idEditar = 0;

    $("#txtNombres").val("");
    $("#txtApellidos").val("");
    $("#txtCorreo").val("");
    $("#txtNroci").val("");
    $("#cboRol").val("");

    $("#cboEstado").val(1).prop("disabled", true);

    $('#imgDirectReg').attr('src', "Imagenes/images.png");
    $("#txtFotoUr").val("");
    $(".custom-file-label").text('Ningún archivo seleccionado');

    $("#myModalLabel").text("Nuevo Usuario");

    $("#mdData").modal("show");

})

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

function AlertaTimerTipo(titulo, mensaje, tipo, timer) {
    swal({
        title: titulo,
        text: mensaje,
        type: tipo,
        // Si le pasas un valor a timer lo usa; si no, usa 2000 por defecto
        timer: timer || 3000,
        showConfirmButton: false
    });
}

function habilitarBoton() {
    $('#btnGuardarCambios').prop('disabled', false);
}

$("#btnGuardarCambios").on("click", function () {
    // Bloqueo inmediato
    $('#btnGuardarCambios').prop('disabled', true);

    let idRol = $("#cboRol").val();

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

    if (idRol === "") {
        toastr.warning("", "Por favor, seleccione un Rol.")
        $("#cboRol").focus();
        habilitarBoton();
        return;
    }

    // 2. ARMAR EL OBJETO
    const objeto = {
        IdUsuario: idEditar,
        IdRol: parseInt(idRol),
        NombreUsuario: $("#txtNombres").val().trim(),
        ApellidosUsuario: $("#txtApellidos").val().trim(),
        Correo: $("#txtCorreo").val().trim(),
        CiUsuario: $("#txtNroci").val().trim(),
        Estado: ($("#cboEstado").val() === "1" ? true : false),
        FotoUrl: "" // Lo enviamos siempre vacío. Si hay foto nueva, el Base64 la reemplazará en C#.
    };

    const fileInput = document.getElementById('txtFotoUr');
    const file = fileInput.files[0];

    if (file) {
        const reader = new FileReader();
        reader.onload = function (e) {
            const base64String = e.target.result.split(',')[1];

            enviarAjaxUsuarios(objeto, base64String);
        };
        reader.readAsDataURL(file);
    } else {
        enviarAjaxUsuarios(objeto, "");
    }

});

function enviarAjaxUsuarios(objeto, base64String) {
    $("#mdData").find("div.modal-content").LoadingOverlay("show");

    $.ajax({
        type: "POST",
        url: "PageUsuarios.aspx/GuardarOrEditUsuarios",
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
                listarUsuarios();
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



//fin