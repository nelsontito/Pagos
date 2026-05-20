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

function MostrarToastZer(mensaje, titulo, tipo) {

    toastr.options = {
        "closeButton": true,          // Muestra una "X" para cerrar manualmente
        "progressBar": true,          // Muestra una barra de tiempo agotándose
        "positionClass": "toast-top-right", // Posición en pantalla
        "preventDuplicates": false,   // Evita que el mismo mensaje se repita varias veces seguidas
        "timeOut": "3000",            // Tiempo que dura en pantalla (3 segundos)
        "extendedTimeOut": "1000"     // Tiempo extra si el usuario pasa el mouse por encima
    };

    // Si no se envía un tipo, por defecto será 'info'
    let tipoToast = tipo || "info";

    // Ejecutamos la función dinámica de toastr
    toastr[tipoToast](mensaje, titulo || "");
}

$(document).ready(function () {

    const usuarioLog = sessionStorage.getItem('userEmi');

    if (!usuarioLog) {
        window.location.replace('Login.aspx');
        return;
    }

    try {
        const usua = JSON.parse(usuarioLog);
        // Nombre en la parte superior derecha
        $("#nombreusuariome").text(usua.NombreUsuario + " " + usua.ApellidosUsuario);

        // Rol en la parte superior derecha
        $("#rolusuariome").text(usua.NombreRol);

        // Nombre en el panel lateral
        $("#rolnomme").text(usua.NombreUsuario + " " + usua.ApellidosUsuario);

        // Foto arriba
        if (usua.FotoUrl && usua.FotoUrl !== "") {
            $("#imgUsuarioMe").attr("src", usua.FotoUrl);
            $("#imageUserMe").attr("src", usua.FotoUrl);
        } else {
            $("#imgUsuarioMe").attr("src", "assets/images/users/avatar-1.jpg");
            $("#imageUserMe").attr("src", "assets/images/users/avatar-1.jpg");
        }

    } catch (error) {
        console.error("Error leyendo sesión", error);
        sessionStorage.clear();
        window.location.replace('Login.aspx');
    }

    $("#close").click(function () {

        sessionStorage.clear();

        window.location.href = "Login.aspx";

    });
    $("#btnPerfil").click(function () {

        const usuarioLog = sessionStorage.getItem('userEmi');

        if (!usuarioLog) return;

        const usua = JSON.parse(usuarioLog);

        swal({
            title: "Perfil del Usuario",
            text:
                "Nombre: " + usua.NombreUsuario + " " + usua.ApellidosUsuario + "\n\n" +
                "CI: " + usua.CiUsuario + "\n\n" +
                "Correo: " + usua.Correo + "\n\n" +
                "Rol: " + usua.NombreRol,
            icon: usua.FotoUrl
        });

    });

});