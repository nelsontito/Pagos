<%@ Page Title="" Language="C#" MasterPageFile="~/HomeMaster.Master" AutoEventWireup="true" CodeBehind="PageInscripciones.aspx.cs" Inherits="CapaPresentacion.PageInscripciones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="assets/plugins/datatables/jquery.dataTables.min.css" rel="stylesheet" type="text/css" />
<link href="assets/plugins/datatables/buttons.bootstrap4.min.css" rel="stylesheet" type="text/css" />
<link href="assets/plugins/datatables/fixedHeader.bootstrap.min.css" rel="stylesheet" type="text/css" />
<link href="assets/plugins/datatables/responsive.bootstrap.min.css" rel="stylesheet" type="text/css" />
<link href="assets/plugins/datatables/dataTables.bootstrap4.min.css" rel="stylesheet" type="text/css" />
<link href="assets/plugins/datatables/scroller.bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/pluginzero/select2/select2.min.css" rel="stylesheet" type="text/css" />

       <style>
        .input-reducido {
            width: 70px;
            text-align: center;
            font-weight: bold;
        }

        #tbData {
            border-radius: 8px;
            overflow: hidden;
        }

            #tbData tbody tr {
                transition: all .2s ease;
            }

                #tbData tbody tr:hover {
                    background: #f5f9ff;
                    transform: scale(1.01);
                }

            #tbData td {
                vertical-align: middle;
            }

        .docente-badge {
            background: #eaf4ff;
            padding: 6px 12px;
            border-radius: 20px;
            font-weight: 600;
            color: #0d6efd;
        }

        .semestre-badge {
            background: #fff3cd;
            padding: 5px 10px;
            border-radius: 15px;
            font-weight: bold;
        }

        .input-atraso {
            border: 2px solid #28a745;
            border-radius: 20px;
        }

            .input-atraso:focus {
                box-shadow: 0 0 6px rgba(40,167,69,.4);
            }

        .input-error {
            border: 2px solid #dc3545 !important; /* Rojo Bootstrap */
        }

            .input-error:focus {
                box-shadow: 0 0 6px rgba(220,53,69,.5) !important;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="titulo" runat="server">
    Panel de Inscripciones
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
   <div class="row">
        <div class="col-lg-12">
            <div class="card shadow-sm border-0" id="loadinzero">
                <div class="card-body">
                    <input id="txtIdEstudiante" value="0" type="hidden" />

                    <div class="row">
                        <div class="col-md-5 mb-3">
                            <div class="border rounded bg-light p-3 h-100">
                                <div class="d-flex align-items-center mb-3">
                                    <img id="imgEst" src="Imagenes/images.png" alt="Perfil" class="rounded-circle shadow-sm mr-3" style="width: 65px; height: 65px; object-fit: cover;">
                                    <div>
                                        <h5 id="lblNombres" class="mb-1 text-primary font-weight-bold" style="font-size: 1.1rem;">Esperando..</h5>
                                        <span id="lblDatos" class="badge badge-secondary p-1">Esperando..</span>
                                    </div>
                                </div>
                                <hr class="mt-0">
                                <div class="form-group mb-0">
                                    <label for="cboBuscarEstudiante" class="small font-weight-bold text-muted mb-1"><i class="fas fa-search mr-1"></i>Buscar Estudiante</label>
                                    <select class="form-control form-control-sm" id="cboBuscarEstudiante" style="width: 100%;">
                                        <option value=""></option>
                                    </select>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-4 mb-3">
                            <div class="p-2 h-100">
                                <div class="input-group input-group-sm mb-3">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text bg-white font-weight-bold" style="width: 85px;">Grado</span>
                                    </div>
                                    <select class="custom-select custom-select-sm" id="cboGrados"></select>
                                </div>

                                <div class="input-group input-group-sm mb-3">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text bg-white font-weight-bold" style="width: 85px;">Carrera</span>
                                    </div>
                                    <select class="custom-select custom-select-sm" id="cboCarreras"></select>
                                </div>

                                <div class="input-group input-group-sm mb-3">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text bg-white font-weight-bold" style="width: 85px;">Semestre</span>
                                    </div>
                                    <select class="custom-select custom-select-sm" id="cboSemestre"></select>
                                </div>
                                <div class="input-group input-group-sm mb-3">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text bg-white font-weight-bold" style="width: 85px;">Periodo</span>
                                    </div>
                                    <select class="custom-select custom-select-sm" id="cboPeriodo"></select>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-3 mb-3">
                            <div class="p-2 h-100 d-flex flex-column justify-content-between">                              

                                <div class="mt-auto">
                                    <button type="button" id="btnNuevoRegistro" class="btn btn-sm btn-block btn-outline-info mb-3 font-weight-bold shadow-sm">
                                        <i class="fas fa-plus mr-2"></i>Nuevo Registro
                                    </button>

                                    <button type="button" id="btnGuardarRegistro" class="btn btn-sm btn-block btn-success font-weight-bold shadow-sm" disabled>
                                        <i class="fas fa-save mr-2"></i>Guardar Registro
                                    </button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-lg-12">
            <div class="card shadow-sm border-0 mt-2">
                <div class="card-header bg-primary text-white py-2 px-4 border-0">
                    <h6 class="card-title m-0"><i class="fas fa-bookmark mr-2"></i>Lista Estudiantes Inscritos</h6>
                </div>
                <div class="card-body">
                    <div class="table-responsive mt-2">
                        <table id="tbData" class="table table-sm table-hover table-bordered" cellspacing="0" width="100%">
                            <thead class="thead-light">
                                <tr>
                                    <th>Id</th>
                                    <th>Estudiante</th>
                                    <th>Nro CI</th>
                                    <th>Grado Academico</th>
                                    <th>Carrera</th>
                                    <th>Semestre</th>
                                    <th class="text-center">Opciones</th>
                                </tr>
                            </thead>
                            <tbody></tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="footer" runat="server">
        <script src="assets/plugins/datatables/jquery.dataTables.min.js"></script>
<script src="assets/plugins/datatables/dataTables.bootstrap4.min.js"></script>

<script src="assets/plugins/datatables/dataTables.buttons.min.js"></script>
<script src="assets/plugins/datatables/buttons.bootstrap4.min.js"></script>

<script src="assets/plugins/datatables/jszip.min.js"></script>
<script src="assets/plugins/datatables/pdfmake.min.js"></script>
<script src="assets/plugins/datatables/vfs_fonts.js"></script>
<script src="assets/plugins/datatables/buttons.html5.min.js"></script>
<script src="assets/plugins/datatables/buttons.print.min.js"></script>
<script src="assets/plugins/datatables/dataTables.fixedHeader.min.js"></script>
<script src="assets/plugins/datatables/dataTables.keyTable.min.js"></script>
<script src="assets/plugins/datatables/dataTables.scroller.min.js"></script>


<script src="assets/plugins/datatables/dataTables.responsive.min.js"></script>
<script src="assets/plugins/datatables/responsive.bootstrap4.min.js"></script>
    <script src="assets/pluginzero/select2/select2.min.js"></script>
<script src="assets/pluginzero/select2/es.min.js"></script>

<script src="js/PageInscripciones.js?v=<%= DateTime.Now.ToString("yyyyMMddHHmmss") %>" type="text/javascript"></script>
</asp:Content>
