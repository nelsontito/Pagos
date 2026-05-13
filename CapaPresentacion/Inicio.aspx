<%@ Page Title="" Language="C#" MasterPageFile="~/HomeMaster.Master" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="CapaPresentacion.Inicio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="titulo" runat="server">
    BIENVENIDO...
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">

    <div class="row">
        <div class="col-sm-6 col-lg-3">
            <div class="card text-center">
                <div class="card-heading">
                    <h4 class="card-title text-muted font-weight-light mb-0">Estudiantes Registrados</h4>
                </div>
                <div class="card-body p-t-10">
                    <h2 class="m-t-0 m-b-15">
                        <i class="mdi mdi-school text-primary m-r-10"></i><b>0</b>
                    </h2>
                    <p class="text-muted m-b-0 m-t-20">Total de estudiantes activos</p>
                </div>
            </div>
        </div>

        <div class="col-sm-6 col-lg-3">
            <div class="card text-center">
                <div class="card-heading">
                    <h4 class="card-title text-muted font-weight-light mb-0">Usuarios Activos</h4>
                </div>
                <div class="card-body p-t-10">
                    <h2 class="m-t-0 m-b-15">
                        <i class="mdi mdi-account-multiple text-success m-r-10"></i><b>0</b>
                    </h2>
                    <p class="text-muted m-b-0 m-t-20">Personal con acceso al sistema</p>
                </div>
            </div>
        </div>

        <div class="col-sm-6 col-lg-3">
            <div class="card text-center">
                <div class="card-heading">
                    <h4 class="card-title text-muted font-weight-light mb-0">Carreras Disponibles</h4>
                </div>
                <div class="card-body p-t-10">
                    <h2 class="m-t-0 m-b-15">
                        <i class="mdi mdi-library-books text-info m-r-10"></i><b>0</b>
                    </h2>
                    <p class="text-muted m-b-0 m-t-20">Carreras registradas</p>
                </div>
            </div>
        </div>

        <div class="col-sm-6 col-lg-3">
            <div class="card text-center">
                <div class="card-heading">
                    <h4 class="card-title text-muted font-weight-light mb-0">Mensualidades</h4>
                </div>
                <div class="card-body p-t-10">
                    <h2 class="m-t-0 m-b-15">
                        <i class="mdi mdi-cash-multiple text-warning m-r-10"></i><b>0</b>
                    </h2>
                    <p class="text-muted m-b-0 m-t-20">Pagos pendientes o registrados</p>
                </div>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-lg-6">
            <div class="card">
                <div class="card-header bg-primary py-2 px-4">
                    <h3 class="card-title m-0">
                        <i class="mdi mdi-view-dashboard mr-2"></i>Resumen del Sistema
                    </h3>
                </div>
                <div class="card-body">
                    <p>
                        Sistema de control académico y mensualidades para la gestión de estudiantes,
                        carreras, usuarios y pagos registrados.
                    </p>
                </div>
            </div>
        </div>

        <div class="col-lg-6">
            <div class="card">
                <div class="card-header bg-primary py-2 px-4">
                    <h3 class="card-title m-0">
                        <i class="mdi mdi-link-variant mr-2"></i>Accesos Rápidos
                    </h3>
                </div>
<%--                <div class="card-body">
                    <a href="PageEstudiantes.aspx" class="btn btn-primary btn-sm mr-2">
                        <i class="mdi mdi-school mr-1"></i> Estudiantes
                    </a>

                    <a href="PageCarreras.aspx" class="btn btn-info btn-sm mr-2">
                        <i class="mdi mdi-library-books mr-1"></i> Carreras
                    </a>

                    <a href="PageUsuario.aspx" class="btn btn-success btn-sm">
                        <i class="mdi mdi-account-multiple mr-1"></i> Usuarios
                    </a>
                </div>--%>
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
