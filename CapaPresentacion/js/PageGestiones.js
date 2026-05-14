let tablaData;
let idEditar = 0;

$(document).ready(function () {

    cargarTodoslosMeses();
    listaGestiones();

});

function listaGestiones() {
    //if ($.fn.DataTable.isDataTable("#tbDatas")) {
    //    $("#tbDatas").DataTable().destroy();
    //    $('#tbDatas tbody').empty();
    //}

    tablaData = $("#tbDatas").DataTable({
        responsive: true,
        "ajax": {
            "url": 'PageGestiones.aspx/ListaGestiones',
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
            { "data": "IdGestion", "visible": false, "searchable": false },
            { "data": "NombreGestion", "className": "align-middle" },
            { "data": "MesIni", "className": "align-middle" },
            { "data": "MesFin", "className": "align-middle" },
            {
                "data": "Estado", "className": "text-center align-middle", render: function (data) {
                    if (data === true)
                        return '<span class="badge badge-primary">Activo</span>';
                    else
                        return '<span class="badge badge-danger">No Activo</span>';
                }
            },
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

function cargarTodoslosMeses() {

    let combosMeses = $("#cboMesIni, #cboMesFin");

    // 2. Mostramos el mensaje de carga en todos a la vez
    combosMeses.html('<option value="">Cargando...</option>');

    $.ajax({
        url: "PageGestiones.aspx/ListaMeses",
        type: "POST",
        data: "{}",
        contentType: 'application/json; charset=utf-8',
        dataType: "json",
        success: function (response) {
            if (response.d.Estado) {

                let opcionesHTML = '<option value="">-- Seleccione un Mes --</option>';

                $.each(response.d.Data, function (i, row) {
                    opcionesHTML += `<option value="${row.IdMes}">${row.NombreMes}</option>`;
                });

                // 3. ¡LA MAGIA! Inyectamos el HTML en los 4 selects al mismo tiempo
                combosMeses.html(opcionesHTML);

            } else {
                combosMeses.html('<option value="">Error al cargar</option>');
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(xhr.status + " \n" + xhr.responseText, "\n" + thrownError);
            combosMeses.html('<option value="">Error de conexión</option>');
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

    idEditar = data.IdGestion;

    // Llenar modal
    $("#txtGestion").val(data.NombreGestion);
    $("#cboMesIni").val(data.IdMesInicio);
    $("#cboMesFin").val(data.IdMesFin);

    $("#cboEstado").val(data.Estado ? 1 : 0);

    $("#cboEstado").prop("disabled", false);

    $("#myModalLabel").text("Editar Gestion");

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
        "Inicio: " + data.MesIni + "\nFin: " + data.MesFin,
        "info"
    );
});


$("#btnNuevo").on("click", function () {

    idEditar = 0;

    $("#txtGestion").val("");
    $("#cboMesIni").val("");
    $("#cboMesFin").val("");

    $("#cboEstado").val(1);

    $("#cboEstado").prop("disabled", true);

    $("#myModalLabel").text("Nueva Gestion");

    $("#mdData").modal("show");
});

function habilitarBoton() {

    $('#btnGuardarCambios').prop('disabled', false);

}



$("#btnGuardarCambios").on("click", function () {

    // Bloquear botón
    $('#btnGuardarCambios').prop('disabled', true);

    // Validación
    if ($("#txtGestion").val().trim() === "") {

        toastr.warning("Debe completar el campo de Gestion.");

        $("#txtGestion").focus();

        habilitarBoton();

        return;
    }
    if ($("#cboMesIni").val() === "") {

        toastr.warning("Debe seleccionar un Mes de Inicio.");

        $("#cboMesIni").focus();

        habilitarBoton();

        return;
    }
    if ($("#cboMesFin").val() === "") {

        toastr.warning("Debe seleccionar un Mes Fin.");

        $("#cboMesFin").focus();

        habilitarBoton();

        return;
    }

    // Objeto
    const objeto = {

        IdGestion: idEditar,

        NombreGestion: $("#txtGestion").val().trim(),
        IdMesInicio: parseInt($("#cboMesIni").val()),
        IdMesFin: parseInt($("#cboMesFin").val()),
        Estado: ($("#cboEstado").val() === "1")
    };

    $("#mdData")
        .find("div.modal-content")
        .LoadingOverlay("show");

    $.ajax({

        type: "POST",
        url: "PageGestiones.aspx/GuardarOrEditGestiones",
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
                if (tablaData) {
                    tablaData.ajax.reload(null, false);
                }
                idEditar = 0;
            }
        },

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

});
