let tablaData;
let idEditar = 0;

$(document).ready(function () {

    listaGrados();

});


function listaGrados() {
    if ($.fn.DataTable.isDataTable("#tbDatas")) {
        $("#tbDatas").DataTable().destroy();
        $('#tbDatas tbody').empty();
    }

    tablaData = $("#tbDatas").DataTable({
        responsive: true,
        "ajax": {
            "url": 'PageGrados.aspx/ListaGradosAcademicos',
            "type": "POST",
            "contentType": "application/json; charset=utf-8",
            "dataType": "json",
            "data": function (d) {
                return JSON.stringify(d);
            },
            "dataSrc": function (json) {
                if (json.d.Estado) {
                    return json.d.Data;
                } else {
                    return [];
                }
            }
        },
        "columns": [
            { "data": "IdGradoAcademico", "visible": false, "searchable": false },
            { "data": "GradoAcademico", "className": "align-middle" },
            {
                "data": "Estado", "className": "text-center align-middle", render: function (data) {
                    if (data === true)
                        return '<span class="badge badge-primary">Activo</span>';
                    else
                        return '<span class="badge badge-danger">No Activo</span>';
                }
            },
            { "data": "NroCarreras", "className": "align-middle" },
            {
                "defaultContent": '<button class="btn btn-primary btn-editar btn-sm mr-2"><i class="fas fa-pencil-alt"></i></button>' +
                    '<button class="btn btn-success btn-detalle btn-sm"><i class="fas fa-address-book"></i></button>',
                "orderable": false,
                "searchable": false,
                "width": "100px",
                "className": "text-center align-middle"
            }
        ],
        "order": [[0, "desc"]],
        "language": {
            "url": "https://cdn.datatables.net/plug-ins/1.11.5/i18n/es-ES.json"
        }
    });
}

$('#tbDatas tbody').on('click', '.btn-editar', function () {

    let fila = $(this).closest('tr');

    // Soporte responsive
    if (fila.hasClass('child')) {
        fila = fila.prev();
    }

    let data = tablaData.row(fila).data();

    idEditar = data.IdGradoAcademico;

    // Llenar modal
    $("#txtGrado").val(data.GradoAcademico);

    $("#cboEstado").val(data.Estado ? 1 : 0);

    $("#cboEstado").prop("disabled", false);

    $("#myModalLabel").text("Editar Grado");

    $("#mdData").modal("show");
});


$('#tbDatas tbody').on('click', '.btn-detalle', function () {

    let fila = $(this).closest('tr');

    if (fila.hasClass('child')) {
        fila = fila.prev();
    }

    let data = tablaData.row(fila).data();

    swal(
        "Detalle",
        "Grado: " + data.GradoAcademico + "\nCarreras: " + data.NroCarreras,
        "info"
    );
});


$("#btnNuevo").on("click", function () {

    idEditar = 0;

    $("#txtGrado").val("");

    $("#cboEstado").val(1);

    $("#cboEstado").prop("disabled", true);

    $("#myModalLabel").text("Nuevo Grado");

    $("#mdData").modal("show");
});


function habilitarBoton() {

    $('#btnGuardarCambios').prop('disabled', false);

}



$("#btnGuardarCambios").on("click", function () {

    // Bloquear botón
    $('#btnGuardarCambios').prop('disabled', true);

    // Validación
    if ($("#txtGrado").val().trim() === "") {

        toastr.warning("Debe completar el campo Grado.");

        $("#txtGrado").focus();

        habilitarBoton();

        return;
    }

    // Objeto
    const objeto = {

        IdGradoAcademico: idEditar,

        GradoAcademico: $("#txtGrado").val().trim(),

        Estado: ($("#cboEstado").val() === "1")
    };

    enviarAjaxGrados(objeto);

});

function enviarAjaxGrados(objeto) {

    $("#mdData")
        .find("div.modal-content")
        .LoadingOverlay("show");

    $.ajax({

        type: "POST",

        url: "PageGrados.aspx/GuardarOrEditGradoAcademicos",

        data: JSON.stringify({
            objeto: objeto
        }),

        contentType: "application/json; charset=utf-8",

        dataType: "json",

        success: function (response) {

            $("#mdData")
                .find("div.modal-content")
                .LoadingOverlay("hide");

            AlertaTimerTipo(
                response.d.Estado ? '¡Excelente!' : 'Atención',
                response.d.Mensaje,
                response.d.Valor
            );

            if (response.d.Estado) {

                $("#mdData").modal("hide");

                idEditar = 0;

                // Recargar tabla
                tablaData.ajax.reload(null, false);
                //location.reload();
            }
        },
        //error: function (xhr) {
        //    console.log("STATUS:", xhr.status);
        //    console.log("ERROR REAL:", xhr.responseText);

        //    $("#mdData").find("div.modal-content").LoadingOverlay("hide");

        //    toastr.error("Error del servidor. Revisa la consola.");
        //},

        error: function (xhr) {

            console.log(xhr.responseText);

            $("#mdData")
                .find("div.modal-content")
                .LoadingOverlay("hide");

            toastr.error("No se pudo conectar con el servidor.");
        },

        complete: function () {

            habilitarBoton();

        }
    });
}