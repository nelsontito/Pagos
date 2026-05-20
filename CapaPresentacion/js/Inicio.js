$(document).ready(function () {
    cargarDashboard();
});

function cargarDashboard() {
    $.ajax({
        type: "POST",
        url: "Inicio.aspx/ObtenerResumenDashboard",
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {

            if (response.d.Estado) {

                const data = response.d.Data;

                $("#lblTotalEstudiantes").text(data.TotalEstudiantes);
                $("#lblTotalUsuarios").text(data.TotalUsuarios);
                $("#lblTotalCarreras").text(data.TotalCarreras);
                $("#lblTotalMensualidades").text(data.TotalMensualidadesPendientes);
            }
        },
        error: function (xhr) {
            console.log(xhr.responseText);
            toastr.error("No se pudo cargar el dashboard.");
        }
    });
}