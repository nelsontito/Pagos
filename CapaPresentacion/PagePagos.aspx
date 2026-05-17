<%@ Page Title="" Language="C#" MasterPageFile="~/HomeMaster.Master" AutoEventWireup="true" CodeBehind="PagePagos.aspx.cs" Inherits="CapaPresentacion.PagePagos" %>
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
    Panel de Pagos
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <div class="row">
     <div class="col-lg-12">
         <div class="card shadow-sm border-0" id="loadinzero">
             <div class="card-body">
                 <input id="txtIdEstudiante" value="0" type="hidden" />
                                  <input id="txtCodigo" value="0" type="hidden" />


                  <div class="row mb-3">

    <div class="col-md-12">
        <div class="border rounded bg-light p-3 shadow-sm">

            <div class="row align-items-center">

                <div class="col-md-2">

                </div>

                <!-- Foto y datos -->
                <div class="col-md-5 d-flex align-items-center border-right">

                    <img id="imgEst"
                         src="Imagenes/images.png"
                         alt="Perfil"
                         class="rounded-circle shadow-sm mr-3"
                         style="width: 70px; height: 70px; object-fit: cover;">

                    <div>
                        <h5 id="lblNombres"
                            class="mb-1 text-primary font-weight-bold">
                            Esperando..
                        </h5>

                        <span id="lblDatos" class="badge badge-secondary">
                            Esperando..
                        </span>
                    </div>

                </div>

                <!-- Buscador -->
                <div class="col-md-3">

                    <label for="cboBuscarEstudiante"
                           class="small font-weight-bold text-muted mb-1">
                        <i class="fas fa-search mr-1"></i> Buscar Estudiante
                    </label>

                    <select class="form-control form-control-sm"
                            id="cboBuscarEstudiante"
                            style="width: 100%;">
                        <option value=""></option>
                    </select>

                </div>
                
                <div class="col-md-2">

                </div>
            </div>

        </div>
    </div>

</div>
                   <div class="row">
      <div class="col-lg-12">
          <div class="card shadow-sm border-0 mt-2">
              <div class="card-header bg-primary text-white py-2 px-4 border-0">
                  <h6 class="card-title m-0"><i class="fas fa-bookmark mr-2"></i>Reporte de Pagos</h6>
              </div>
              <div class="card-body">
                  <div class="table-responsive mt-2">
                      <table id="tbData" class="table table-sm table-hover table-bordered" cellspacing="0" width="100%">
                          <thead class="thead-light">
                              <tr>
                                  <%--<th>Id</th>--%>
                                  <th>Carrera</th>
                                  <th>Mes</th>
                                  <th>Estado</th>
                                  <th>Fecha</th>
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

<script src="js/PagePagos.js?v=<%= DateTime.Now.ToString("yyyyMMddHHmmss") %>" type="text/javascript"></script>
</asp:Content>
