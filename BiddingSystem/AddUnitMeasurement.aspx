<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="AddUnitMeasurement.aspx.cs" Inherits="BiddingSystem.AddUnitMeasurement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">
 
    <section class="content-header">
      <h1>
       Add Measurement
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="AdminDashboard.aspx"><i class="fa fa-home"></i> Home</a></li>
        <li class="active">Add Measurement</li>
      </ol>
    </section>
    <br />
     <style type="text/css">
    .tablegv {
        font-family: "Trebuchet MS", Arial, Helvetica, sans-serif;
        border-collapse: collapse;
        width: 100%;
    }
    .tablegv td, .tablegv th {
        border: 1px solid #ddd;
        padding: 8px;
    }
    .tablegv tr:nth-child(even){background-color: #f2f2f2;}
    /*.tablegv tr:hover {background-color: #ddd;}*/
    .tablegv th {
        padding-top: 12px;
        padding-bottom: 12px;
        text-align: left;
        background-color: #3C8DBC;
        color: white;
    }
    .successMessage
        {
            color: #1B6B0D;
            font-size: medium;
        }
        
        .failMessage
        {
            color: #C81A34;
            font-size: medium;
        }
</style>

    

     <script src="AdminResources/js/jquery-1.10.2.min.js"></script>
     <script src="AdminResources/js/jquery.min.js" type="text/javascript"></script>
    <form id="form1" runat="server">

            <div id="modalDeleteYesNo" class="modal fade">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" 
                class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
                <h4 id="lblTitleDeleteYesNo" class="modal-title">Confirmation</h4>
            </div>
            <div class="modal-body">
                <p>Are you sure to Delete this Record ?</p>
            </div>
            <div class="modal-footer">
                 <asp:Button ID="btnDelete" runat="server"  CssClass="btn btn-primary"  OnClick="btnDelete_Click1" Text="Yes" ></asp:Button>
                <button id="btnNoDeleteYesNo" onclick="return hideDeleteModal();" type="button" class="btn btn-danger" >No</button>
            </div>
        </div>
    </div>
</div>
          
  <%--   <asp:ScriptManager runat="server" ID="sm">
                </asp:ScriptManager>
                 <asp:UpdatePanel ID="Updatepanel1" runat="server">
                <ContentTemplate>--%>

                     <asp:HiddenField  ID="hdnImgPathEdit" runat="server"/>
        <section class="content">

        <div class="alert  alert-info  alert-dismissable" id="msg" runat="server" visible="false">
                            <a href="#" class="close" data-dismiss="alert" aria-label="close">×</a>
                            <strong>
                                <asp:Label ID="lbMessage" ForeColor="White"  runat="server"></asp:Label>
                            </strong>
                        </div>


      <!-- SELECT2 EXAMPLE -->
      <div class="box box-info">
        <div class="box-header with-border">
          <h3 class="box-title" >Measurement Details</h3>

          <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
            <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
          </div>
        </div>
        <!-- /.box-header -->
        <div class="box-body">
          
          <div class="row">
            <div class="col-md-6">
              
                <div class="form-group">
                <label for="exampleInputEmail1">Measurement Name</label><label id="validateItemName" style="color:red"></label>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtMeasurementName" ValidationGroup="btnSave" ID="RequiredFieldValidator3" ForeColor="Red">*</asp:RequiredFieldValidator>                                
                 <asp:TextBox ID="txtMeasurementName" runat="server" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="form-group">
                <label for="exampleInputEmail1">Measurement Short Code</label><label id="Label2" style="color:red"></label>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtMeasuremenCode" ValidationGroup="btnSave" ID="RequiredFieldValidator4" ForeColor="Red">*</asp:RequiredFieldValidator>                                
                <asp:TextBox runat="server" ID="txtMeasuremenCode" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="form-group">
                    <div class="pull-left">
                <label for="exampleInputEmail1">Is Active</label>
                        </div>
                <div class="col-sm-1">
                <div class="checkbox">
                <label>
                <asp:CheckBox ID="chkIsavtive"  runat="server" Checked></asp:CheckBox>
                </label>
                </div>
                </div>
                </div>

            </div>
         
          </div>
                   
        </div>
            
        <div class="box-footer">
            <span class="pull-right">
                 <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnSave_Click"  ValidationGroup="btnSave" ></asp:Button>
                 <asp:Button ID="btnClear"  runat="server" Text="Clear"  
                CssClass="btn btn-danger" onclick="btnClear_Click"></asp:Button>
                </span>
              </div>
      </div>


    </section>

     <div class="panel-body">
            <div class="co-md-12">
                <div class="table-responsive">
                    <asp:GridView ID="gvUnitMeasuremnts" runat="server" CssClass="table table-responsive tablegv"
                        GridLines="None" AutoGenerateColumns="false" OnPageIndexChanging="gvUnitMeasuremnts_PageIndexChanging" PageSize="200" AllowPaging="true">
                        <Columns>
                            <asp:TemplateField HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                <ItemTemplate>
                                    <asp:Label CssClass="measurentIdCl" Text='<%#Eval("DetailId").ToString() %>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:BoundField DataField="DetailId" HeaderText="Measurent Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                            <asp:BoundField DataField="CompanyId" HeaderText="Company Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                            <asp:BoundField DataField="ShortCode" HeaderText="Measurement Code" />
                            <asp:BoundField DataField="MeasurementName" HeaderText="Measurement Name" />
                            <asp:BoundField DataField="CreatedDate" HeaderText="Created Date" DataFormatString="{0:dd/MM/yyyy}" />
                            <asp:BoundField DataField="CreatedByName" HeaderText="Created By" />


                            <asp:TemplateField HeaderText="Active">
                                <ItemTemplate>
                                    <asp:Label Text='<%#Eval("IsActive").ToString()== "1"?"Yes":"No" %>' Font-Bold="true" ForeColor='<%#Eval("IsActive").ToString()== "1"?System.Drawing.Color.Green:System.Drawing.Color.Red %>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--                            <asp:TemplateField HeaderText="Edit">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnEdit" ImageUrl="~/images/document.png" OnClick="btnEdit_Click" Style="width: 26px; height: 20px"
                                        runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="Delete">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnDelete" CssClass="deleteItem" ImageUrl="~/images/dlt.png" Visible='<%#Eval("IsActive").ToString()== "1"?true:false %>' ToolTip="Delete" Style="width: 26px; height: 20px;"
                                        runat="server" />
                                    <asp:ImageButton ID="btnRestore" ToolTip="Restore" CssClass="restoreItem" ImageUrl="~/images/document.png" Visible='<%#Eval("IsActive").ToString()== "0"?true:false %>' OnClick="btnRestore_Click" Style="width: 26px; height: 20px;"
                                        runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
                   
      <%--    </ContentTemplate>

                      <Triggers>
         <asp:PostBackTrigger ControlID="btnSave" />
      </Triggers>
        </asp:UpdatePanel>--%>

         <asp:HiddenField ID="hdMeasurementId" runat="server" />
        
    <asp:HiddenField ID="HiddenField1" runat="server" />

    </form>

  


         <script type="text/javascript">

        function hideDeleteModal()
        {
            var $confirm = $("#modalDeleteYesNo");
            $confirm.modal('hide');
            return this.false;
        }
        function showDeleteModal()
        {
            var $confirm = $("#modalDeleteYesNo");
            $confirm.modal('show');
            return this.false;
        }
    </script>

    
  
    <script>

        // Sys.Application.add_load(function() {

        $(".deleteItem").click(function () {
            var measurementId = $(this).closest('tr').find('td').eq(1).html();
            $("#<%=hdMeasurementId.ClientID%>").val(measurementId);

            showDeleteModal();
            event.preventDefault();
        });
        $(".restoreItem").click(function () {
            var measurementId = $(this).closest('tr').find('td').eq(1).html();
            $("#<%=hdMeasurementId.ClientID%>").val(measurementId);

        });
      //  });
    </script>

    
</asp:Content>
