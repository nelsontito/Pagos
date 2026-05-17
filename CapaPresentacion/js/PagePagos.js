let tablaData;
let idEditar = 0;

$(document).ready(function () {

  
    cargarBuscadorEstudiantes();
});
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
        templateResult: formatoResultadosEstudiantes
    });
}

function formatoResultadosEstudiantes(data) {
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
    $("#txtCodigo").val(data.Codigo);
    $("#lblNombres").text("Est: " + data.text);
    $("#lblDatos").text("Nro CI: " + data.nroCi);
    $("#imgEst").attr("src", data.imagen || "Imagenes/images.png");
    $("#cboBuscarEstudiante").val(null).trigger("change");

    listaMensualidades(data.Codigo)
});

function listaMensualidades(codigo) {
    if ($.fn.DataTable.isDataTable("#tbData")) {
        $("#tbData").DataTable().destroy();
        $('#tbData tbody').empty();
    }

    const request = {
        Codigo: codigo
    };

    tablaData = $("#tbData").DataTable({
        responsive: true,
        "ajax": {
            "url": 'PagePagos.aspx/ConsultaMensualidad', // Nota: Asegúrate de que esta URL apunte a tu método de mensualidades
            "type": "POST",
            "contentType": "application/json; charset=utf-8",
            "dataType": "json",
            "data": function () {
                return JSON.stringify(request);
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
            { "data": "NombreCarrera", "className": "align-middle" },
            { "data": "NombreMes", "className": "align-middle" },
            {
                "data": "EstaPagado",
                "className": "text-center align-middle",
                render: function (data) {
                    if (data === true)
                        return '<span class="badge badge-primary">Cancelado</span>';
                    else
                        return '<span class="badge badge-danger">No Cancelado</span>';
                }
            },
            { "data": "FechaPago", "className": "align-middle" },
            {
                // CAMBIO AQUÍ: Cambiamos defaultContent por render
                "data": null,
                "orderable": false,
                "searchable": false,
                "className": "text-center align-middle",
                render: function (data, type, row) {
                    // Evaluamos el estado de la mensualidad en esta fila específica
                    if (row.EstaPagado === true) {
                        // Botón DESHABILITADO (gris y sin clase btn-editar para evitar clics)
                        // Le cambié el icono a un 'check' para mejor UX
                        return '<button class="btn btn-secondary btn-sm" disabled><i class="fas fa-check"></i></button>';
                    } else {
                        // Botón HABILITADO (tu diseño original)
                        return '<button class="btn btn-primary btn-pagar btn-sm"><i class="fas fa-pencil-alt"></i></button>';
                    }
                }
            }
        ],
        "order": [],
        "language": {
            "url": "https://cdn.datatables.net/plug-ins/1.11.5/i18n/es-ES.json"
        }
    });
}

$('#tbData tbody').on('click', '.btn-pagar', function () {
    let fila = $(this).closest('tr');
    if (fila.hasClass('child')) {
        fila = fila.prev();
    }

    let data = tablaData.row(fila).data();
    let codigo = $("#txtCodigo").val();

    $.ajax({
        url: "PagePagos.aspx/PagarMensualidad",
        type: "POST",
        data: JSON.stringify({ IdControl: parseInt(data.IdControl) }),
        contentType: 'application/json; charset=utf-8',
        dataType: "json",
        success: function (response) {
            AlertaTimerTipo(
                response.d.Estado ? '¡Excelente!' : 'Atención',
                response.d.Mensaje,
                response.d.Valor
            );

            if (response.d.Estado) {
                listaMensualidades(codigo);
            }
        },
        error: function (xhr) {
            AlertaTimerTipo('Error', 'Problema de comunicación con el servidor.', 'error');
        }
    });
});
