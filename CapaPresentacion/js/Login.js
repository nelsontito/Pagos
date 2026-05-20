$('#btnIngresar').on('click', function () {

    $('#btnIngresar').prop('disabled', true);

    let usuario = $("#txtUsuario").val().trim();
    let clave = $("#txtPassword").val().trim();

    //VALIDACIONES DE USUARIO
    if (usuario === "" || clave === "") {
        swal(
            "Mensaje:",
            "Debe de Ingresar Usuario y Contraseña: ",
            "info"
        );

        $('#btnIngresar').prop('disabled', false);
        return;
    }


    // Swal.fire({ title: "Exelente", text: "Ya puede iniciar sesion.", icon: "success" });
    loginSistema(usuario, clave);
})

function loginSistema(usuario, clave) {

    $.LoadingOverlay("show");

    $.ajax({
        type: "POST",
        url: "Login.aspx/Logeo",
        data: JSON.stringify({ Correo: usuario, Clave: clave }),
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        success: function (response) {
            $.LoadingOverlay("hide");
            if (response.d.Estado) {

                const dato = response.d.Data;

                sessionStorage.clear();
                sessionStorage.setItem('userEmi', JSON.stringify(dato));

                $("#txtUsuario, #txtPassword").val("");
                window.location.href = 'Inicio.aspx';

            } else {
                Swal.fire({ title: "Mensaje", text: response.d.Mensaje, icon: "warning" });
                $("#txtUsuario").val("");
                $("#txtPassword").val("");
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            $.LoadingOverlay("hide");
            console.log(xhr.status + " \n" + xhr.responseText, "\n" + thrownError);
        },
        complete: function () {
            // Rehabilitar el botón después de que la llamada AJAX se complete (éxito o error)
            $('#btnIngresar').prop('disabled', false);
        }
    });
}

// fin