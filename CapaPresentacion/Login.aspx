<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="CapaPresentacion.Login" %>

<!DOCTYPE html>

<html lang="es">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Login EMI - Registro de Pagos</title>

    <!-- Bootstrap -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet">
     <link href="assets/plugins/bootstrap-sweetalert/sweet-alert.css" rel="stylesheet" type="text/css" />
 <link href="assets/pluginzero/toastr/toastr.min.css" rel="stylesheet" />


    <style>
        body {
            min-height: 100vh;
            background: linear-gradient(135deg, #001f3f, #004b8d);
            font-family: 'Segoe UI', Arial, sans-serif;
        }

        .login-card {
            border: none;
            border-radius: 18px;
            overflow: hidden;
            box-shadow: 0 12px 35px rgba(0, 0, 0, 0.35);
        }

        .login-left {
            background: linear-gradient(160deg, #001f3f, #003f7d);
            color: white;
            padding: 45px 35px;
        }

        .login-left img {
            width: 95px;
            margin-bottom: 20px;
        }

        .login-left h3 {
            font-weight: 700;
        }

        .login-right {
            padding: 45px 40px;
            background: #ffffff;
        }

        .form-control {
            height: 48px;
            border-radius: 10px;
        }

        .form-control:focus {
            border-color: #003f7d;
            box-shadow: 0 0 0 0.15rem rgba(0, 63, 125, 0.25);
        }

        .btn-emi {
            background: #001f3f;
            color: white;
            height: 48px;
            border-radius: 10px;
            font-weight: 600;
        }

        .btn-emi:hover {
            background: #003f7d;
            color: white;
        }

        .footer-text {
            font-size: 12px;
            color: #777;
        }

        @media (max-width: 768px) {
            .login-left {
                text-align: center;
                padding: 30px 25px;
            }

            .login-right {
                padding: 35px 25px;
            }
        }
    </style>
</head>
<body>

<div class="container min-vh-100 d-flex align-items-center justify-content-center">
    <div class="row w-100 justify-content-center">
        <div class="col-12 col-md-10 col-lg-8 col-xl-7">

            <div class="card login-card">
                <div class="row g-0">

                    <!-- LADO INSTITUCIONAL -->
                    <div class="col-md-5 login-left d-flex flex-column justify-content-center align-items-center">
                        <img src="Imagenes/imagenemi.png" alt="Logo EMI">
                        <h3 class="text-center">EMI</h3>
                        <p class="text-center mb-0">
                            Escuela Militar de Ingeniería
                        </p>
                        <hr class="w-75 border-light">
                        <small class="text-center">
                            Sistema Institucional de Registro de Pagos
                        </small>
                    </div>

                    <!-- FORMULARIO -->
                    <div class="col-md-7 login-right">
                        <h4 class="fw-bold text-primary mb-1">Iniciar Sesión</h4>
                        <p class="text-muted mb-4">Acceso al módulo de registro de pagos</p>

                        <div class="mb-3">
                            <label class="form-label fw-semibold">Usuario</label>
                            <input type="text" id="txtUsuario" class="form-control" placeholder="Ingrese su usuario">
                        </div>

                        <div class="mb-3">
                            <label class="form-label fw-semibold">Contraseña</label>
                            <input type="password" id="txtPassword" class="form-control" placeholder="Ingrese su contraseña">
                        </div>

                        <div id="mensajeError" class="alert alert-danger py-2 d-none">
                            Debe ingresar usuario y contraseña.
                        </div>

                        <button id="btnIngresar" class="btn btn-emi w-100 mt-2">
                            Ingresar
                        </button>

                        <div class="text-center mt-4 footer-text">
                            © 2026 EMI - Registro de Pagos
                        </div>
                    </div>

                </div>
            </div>

        </div>
    </div>
</div>
        <!-- jQuery -->
    <script src="https://code.jquery.com/jquery-3.7.1.min.js"></script>
    <script src="assets/pluginzero/toastr/toastr.min.js"></script>
<script src="assets/plugins/bootstrap-sweetalert/sweet-alert.min.js"></script>
<script src="assets/plugins/loadingoverlay/loadingoverlay.js"></script>
    <script src="js/Login.js?v=<%= DateTime.Now.ToString("yyyyMMddHHmmss") %>" type="text/javascript"></script>
</body>
</html>
