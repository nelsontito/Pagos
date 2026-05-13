<%@ Page Title="" Language="C#" MasterPageFile="~/HomeMaster.Master" AutoEventWireup="true" CodeBehind="PageEstudiantes.aspx.cs" Inherits="CapaPresentacion.PageEstudiantes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="assets/plugins/datatables/jquery.dataTables.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/plugins/datatables/buttons.bootstrap4.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/plugins/datatables/fixedHeader.bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/plugins/datatables/responsive.bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/plugins/datatables/dataTables.bootstrap4.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/plugins/datatables/scroller.bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/css/inpfile.css" rel="stylesheet" />
  <style>
    .est-perfil {
        width: 125px;
        height: 125px;
        object-fit: cover;
        object-position: center;
    }

    /* IMAGENES DE LA TABLA */
    #tbDatas img {
        width: 50px !important;
        height: 50px !important;
        border-radius: 50%;
        object-fit: cover;
        object-position: center;
    }

    /* CENTRAR FOTO EN LA TABLA */
    #tbDatas td:nth-child(2),
    #tbDatas th:nth-child(2) {
        text-align: center;
        vertical-align: middle;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="titulo" runat="server">
    Panel de Estudiantes de la EMI...

</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="body" runat="server">


<div class="row">
        <div class="col-lg-12">
            <div class="card">
                <div class="card-header bg-primary py-2 px-4 d-flex justify-content-between align-items-center">

                    <h3 class="card-title m-0 text-white">
                        <i class="fas fa-user-friends mr-2"></i>Estudiantes Emilianos
                    </h3>

                    <button type="button" id="btnNuevo" class="btn btn-light btn-sm">
                        <i class="fas fa-plus-circle mr-2"></i>Nuevo Registro
                    </button>

                </div>
                <div class="card-body">
                    <div class="col-lg-12">
                        <table id="tbDatas" class="table table-sm table-striped table-bordered" cellspacing="0" width="100%">
                            <thead>
                                <tr>
                                    <th>id</th>
                                    <th>Foto</th>
                                    <th>Nombre</th>                                    
                                    <th>Ci</th>
                                    <th>Codigo</th>
                                    <th>Telefono</th>
                                    <th>Acciones</th>
                                </tr>
                            </thead>
                            <tbody></tbody>
                        </table>

                    </div>
                </div>
            </div>
        </div>
    </div>


    <div class="modal fade" id="mdData" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title m-0" id="myModalLabel">Estudiantes</h4>
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-lg-8">

                            <div class="form-row">

                                <div class="form-group col-sm-4">
                                    <label for="txtNombre">Nombre</label>
                                    <input type="text" class="form-control input-sm model form-new" id="txtNombres" name="Nombres">
                                </div>
                                <div class="form-group col-sm-4">
                                    <label for="txtApellidos">Apellidos</label>
                                    <input type="text" class="form-control input-sm model form-new" id="txtApellidos" name="Apellidos">
                                </div>
                                <div class="form-group col-sm-4">
                                    <label for="txtNro CI">Nro CI</label>
                                    <input type="text" class="form-control input-sm model" id="txtNroCI" name="Nro CI">
                                </div>
                            </div>
                            <div class="form-row">

                                <div class="form-group col-sm-6">
                                    <label for="txtCodigo">Codigo</label>
                                    <input type="text" class="form-control input-sm model" id="txtCodigo" name="Codigo">
                                </div>
                                <div class="form-group col-sm-6">
                                    <label for="txtTelefono">Telefono</label>
                                    <input type="text" class="form-control input-sm model" id="txtTelefono" name="Telefono">
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="form-group col-sm-3">
                                    <label for="cboEstado">Estado</label>
                                    <select class="form-control" id="cboEstado">
                                        <option value="1">Activo</option>
                                        <option value="0">No Activo</option>
                                    </select>
                                </div>
                                <div class="form-group col-sm-9">
                                    <label for="txtApelocs">Seleccione Foto</label>
                                    <div class="custom-file">
                                        <input type="file" class="custom-file-input" id="txtFotoUr" accept="image/*">
                                        <label class="custom-file-label" for="txtFotoUr">Ningún archivo seleccionado</label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-4">
                            <div class="form-row h-100 d-flex align-items-center justify-content-center">
                                <div class="form-group col-sm-12 text-center">
                                    <img id="imgDirectReg" src="Imagenes/images.png" alt="Foto usuario" class="rounded-circle est-perfil">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-sm btn-secondary" data-dismiss="modal">Cerrar</button>
                    <button id="btnGuardarCambios" type="button" class="btn btn-sm btn-primary">Guardar Cambios</button>
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

   

    <script src="js/PageEstudiantes.js?v=<%= DateTime.Now.ToString("yyyyMMddHHmmss") %>" type="text/javascript"></script>
</asp:Content>
