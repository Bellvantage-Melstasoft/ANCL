<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true"
    CodeBehind="CreateFunction.aspx.cs" Inherits="BiddingSystem.CreateFunction" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">


    <html>
        <head>
       
             <style>
#snackbar,#snackbarMobileNumber, #snackbarFileUpload {
    visibility: hidden;
    min-width: 250px;
    margin-left: -125px;
    background-color: #333;
    color: #fff;
    text-align: center;
    border-radius: 2px;
    padding: 16px;
    position: fixed;
    z-index: 1;
    left: 50%;
    bottom: 30px;
    font-size: 17px;
}

#snackbar.show , #snackbarMobileNumber.show, #snackbarFileUpload.show{
    visibility: visible;
    -webkit-animation: fadein 0.5s, fadeout 0.5s 2.5s;
    animation: fadein 0.5s, fadeout 0.5s 2.5s;
}

@-webkit-keyframes fadein {
    from {bottom: 0; opacity: 0;} 
    to {bottom: 30px; opacity: 1;}
}

@keyframes fadein {
    from {bottom: 0; opacity: 0;}
    to {bottom: 30px; opacity: 1;}
}

@-webkit-keyframes fadeout {
    from {bottom: 30px; opacity: 1;} 
    to {bottom: 0; opacity: 0;}
}

@keyframes fadeout {
    from {bottom: 30px; opacity: 1;}
    to {bottom: 0; opacity: 0;}
}
</style>
    <style type="text/css">
        .tablegv
        {
            font-family: "Trebuchet MS" , Arial, Helvetica, sans-serif;
            border-collapse: collapse;
            width: 100%;
        }
        .tablegv td, .tablegv th
        {
            border: 1px solid #ddd;
            padding: 8px;
        }
        .tablegv tr:nth-child(even)
        {
            background-color: #f2f2f2;
        }
        .tablegv tr:hover
        {
            background-color: #ddd;
        }
        .tablegv th
        {
            padding-top: 12px;
            padding-bottom: 12px;
            text-align: left;
            background-color: #3C8DBC;
            color: white;
        }
        .successMessage
        {
            color: white;
            font-size: medium;
        }
        
        .failMessage
        {
            color: white;
            font-size: medium;
        }
        .activePhase
        {
            text-align: center;
            border-radius: 3px;
        }
        .Grid td
        {
            background-color: #A1DCF2;
            color: black;
            font-size: 10pt;
            line-height:200%;
            border: 1px solid #ddd;
            text-align:center;
        }
        .Grid th
        {
            background-color: #3c8dbc;
            color: White;
            font-size: 10pt;
            line-height:200%;
            text-align :center;
            border: 1px solid #ddd;
            padding: 8px;
        }
        .ChildGrid td
        {
            background-color: #eee !important;
            color: black;
            font-size: 10pt;
            line-height:200%;
            text-align:center;
        }
        .ChildGrid th
        {
            color: White;
            font-size: 10pt;
            line-height:200%;
            padding-top: 12px;
            padding-bottom: 12px;
            text-align: center;
            background-color: #67778e !important;
            color: white;
        }
    </style>
    <style>
        /** {
  box-sizing: border-box;
}
body {
  font: 16px Arial;  
}

input {
  border: 1px solid transparent;
  background-color: #f1f1f1;
  padding: 10px;
  font-size: 16px;
}
input[type=text] {
  background-color: #f1f1f1;
  width: 100%;
}*/
    </style>
     <script src="AdminResources/js/jquery1.8.min.js"></script>
     <script type="text/javascript">
         $("[src*=plus]").live("click", function () {
             $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>")
             $(this).attr("src", "images/minus.png");
         });
         $("[src*=minus]").live("click", function () {
             $(this).attr("src", "images/plus.png");
             $(this).closest("tr").next().remove();
         });
    </script>
            </head>

        <body>


    <section class="content-header">
      <h1>
       Create Main Function Catagory 
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="AdminDashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
        <li class="active">Create Function</li>
      </ol>
    </section>
    <br />
   

    <form id="form1" runat="server">
    
    <section class="content">

        <div class="alert  alert-info  alert-dismissable" id="msg" runat="server" visible="false">
                            <a href="#" class="close" data-dismiss="alert" aria-label="close">×</a>
                            <strong>
                                <asp:Label ID="lbMessage" runat="server"></asp:Label>
                            </strong>
                        </div>


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
                <p>Are you sure you want to delete this record? Or? </p>
            </div>
            <div class="modal-footer">
                 <asp:Button ID="btnDelete" runat="server"  CssClass="btn btn-primary"  OnClick="lnkBtnDelete_Click" Text="Yes" ></asp:Button>
                <button id="btnNoDeleteYesNo" onclick="return hideDeleteModal();" type="button" class="btn btn-danger" >No</button>
            </div>
        </div>
    </div>
</div>

   
 

      <!-- SELECT2 EXAMPLE -->
      <div class="box box-info">
        <div class="box-header with-border">
         <%-- <h3 class="box-title" >Create Company</h3>--%>

          <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
            <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
          </div>
        </div>
        <!-- /.box-header -->
        <div class="box-body">
           <asp:ScriptManager runat="server" ID="sm" EnablePartialRendering="true">
                </asp:ScriptManager>
                 <asp:UpdatePanel ID="Updatepanel1" runat="server">
                <ContentTemplate>
          <div class="row">
            <div class="col-md-6">         
                <div class="form-group">
                <label for="exampleInputEmail1">Main Function Catagory Name</label>  
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtCatagory"  ValidationGroup="btnSave" ID="RequiredFieldValidator1" ForeColor="Red">*</asp:RequiredFieldValidator>
                    <asp:TextBox runat="server" ID="txtCatagory" CssClass="form-control " autocomplete="off" ></asp:TextBox>
                </div>
                 
                <div class="form-group">
                <label for="exampleInputEmail1">Is Active</label>
             
                <asp:CheckBox ID="chkIsavtive"  runat="server"  CssClass="form-control" Checked></asp:CheckBox>
               
                </div>
               

              </div>
          </div>
         
        </div>
     </div>

        <div class="box-footer">
            <span class="pull-right">
                 <asp:Button ID="btnSave" runat="server" Text="Save"  CssClass="btn btn-primary" ValidationGroup="btnSave" onclick="btnSave_Click" ></asp:Button>
                 <asp:Button ID="btnClear"  runat="server" Text="Clear"  CssClass="btn btn-danger" OnClick="btnClear_Click" ></asp:Button>
                </span>
              </div>
        <!-- /.box-body -->
      </div>


      <div class="box box-info">
        <div class="box-header with-border">
         <%-- <h3 class="box-title" >Create Company</h3>--%>

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
                <label for="exampleInputEmail1">Select Main Function Catagory</label>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" InitialValue="0" ControlToValidate="ddlSystrmDivision" ValidationGroup="btnAdd" ForeColor="Red">*</asp:RequiredFieldValidator>
                 <asp:DropDownList ID="ddlSystrmDivision" runat="server" OnSelectedIndexChanged="ddlSystrmDivision_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control"></asp:DropDownList>
                </div>
                </div>

            <div class="col-md-6">         
                <div class="form-group">
                <label for="exampleInputEmail1">Sub Function Name</label>  
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtSubfunction"  ValidationGroup="btnSave2" ID="RequiredFieldValidator2" ForeColor="Red">*</asp:RequiredFieldValidator>
                    <asp:TextBox runat="server" ID="txtSubfunction" CssClass="form-control " autocomplete="off" ></asp:TextBox>
                </div>

               <div class="box-footer">
            <span class="pull-right">
                 <asp:Button ID="btnSave2" runat="server" Text="Save"  CssClass="btn btn-primary" ValidationGroup="btnSave2" onclick="btnSave2_Click" ></asp:Button>
                 <asp:Button ID="btnClear2"  runat="server" Text="Clear"  CssClass="btn btn-danger" OnClick="btnClear2_Click" ></asp:Button>
                </span>
              </div>
        <!-- /.box-body -->
      </div>
      <!-- /.box -->
    </section>
    <div class="panel-body">
        <div class="co-md-12">
            <div class="table-responsive">
                <asp:GridView ID="gvDepartments" runat="server" CssClass="table table-responsive tablegv Grid" GridLines="None" AutoGenerateColumns="false" EmptyDataText="No Records Found" AllowPaging="true" PageSize="10" DataKeyNames="systemDivisionId" OnPageIndexChanging="gvDepartments_PageIndexChanging" OnRowDataBound="OnRowDataBound" >
                    <Columns>
                <asp:TemplateField>
                <ItemTemplate>
                    <img alt = "" style="cursor: pointer" src="images/plus.png" />
                    <asp:Panel ID="pnlOrders" runat="server" Style="display: none">
                        <asp:GridView ID="gvSubcatgry" runat="server" AutoGenerateColumns="false" CssClass = "ChildGrid">
                            <Columns>
                                <asp:BoundField ItemStyle-Width="150px" DataField="functionId" HeaderText="" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                <asp:BoundField ItemStyle-Width="150px" DataField="systemDivisionId" HeaderText="" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                <asp:BoundField ItemStyle-Width="150px" DataField="functionName" HeaderText="Function Name" />
                                <asp:BoundField ItemStyle-Width="150px" DataField="CreatedDate" HeaderText="Created Date"  DataFormatString="{0:dd/MM/yyyy hh:mm tt}"/>
                                <asp:BoundField ItemStyle-Width="150px" DataField="CreatedBy" HeaderText="Created By" />
                                <asp:BoundField ItemStyle-Width="150px" DataField="UpdatedDate" HeaderText="Updated Date"  DataFormatString="{0:dd/MM/yyyy hh:mm tt}"/>
                                <asp:BoundField ItemStyle-Width="150px" DataField="UpdatedBy" HeaderText="Updated By" />
                        <asp:TemplateField HeaderText="Edit">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEditSubcategory" ImageUrl="~/images/document.png" OnClick="lnkBtnEditSubcategory_Click"
                                    Style="width: 26px; height: 20px" runat="server"  CssClass="editFunction" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Delete">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnDeleteSubCtagory"  OnClick="lnkBtndeleteSubcategory_Click" ImageUrl="~/images/delete.png" Enabled="true" ToolTip="Delete" MaintainScrollPositionOnPostBack="true" CssClass="deleteCompany" style="width:26px;height:20px;" runat="server"/>
                               <%-- <asp:ImageButton ID="btnDeleteCompany" ImageUrl="~/images/delete.png"  class="deleteCompany" Style="width: 26px; height: 20px;" runat="server"/>--%>
                            </ItemTemplate>
                        </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </asp:Panel>
                </ItemTemplate>
            </asp:TemplateField>
                         <asp:TemplateField HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" >
                            <ItemTemplate>
                                <asp:Label  runat="server" ID="lblCompanyId" Text='<%#Eval("systemDivisionId")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="systemDivisionId" HeaderText="ItemId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                        <asp:BoundField DataField="systemDivisionName" HeaderText="Main Catagory Name" />
                        <asp:BoundField DataField="CreatedDate" HeaderText="Created Date"  DataFormatString="{0:dd/MM/yyyy hh:mm tt}" />
                        <asp:BoundField DataField="CreatedBy" HeaderText="Created By" />
                        <asp:BoundField DataField="UpdatedDate" HeaderText="Updated Date"  DataFormatString="{0:dd/MM/yyyy hh:mm tt}" />
                        <asp:BoundField DataField="UpdatedBy" HeaderText="Updated By" />
                        <asp:TemplateField HeaderText="Active">
                            <ItemTemplate>
                                <asp:Label CssClass="activePhase" runat="server" ID="lblIsActive2" Text='<%#Eval("IsActive").ToString()=="1"?"Yes":"No"%>'
                                    Font-Bold="true" ForeColor='<%#Eval("IsActive").ToString()== "1"?System.Drawing.Color.Green:System.Drawing.Color.Red %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Edit">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEdit" ImageUrl="~/images/document.png" OnClick="lnkBtnEdit_Click"
                                    Style="width: 26px; height: 20px" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Delete">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnDeleteCompany" ImageUrl='<%#Eval("IsActive").ToString()== "1"?"~/images/delete.png":"~/images/dlt.png" %>' Enabled='<%#Eval("IsActive").ToString()== "1"?true:false %>'  ToolTip='<%#Eval("IsActive").ToString()== "1"?"Delete":"Deleted" %>'  CssClass="deleteCompany" style="width:26px;height:20px;" runat="server"/>
                               <%-- <asp:ImageButton ID="btnDeleteCompany" ImageUrl="~/images/delete.png"  class="deleteCompany" Style="width: 26px; height: 20px;" runat="server"/>--%>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
         <asp:HiddenField ID="hdnmainCatogory" runat="server" />
    <asp:HiddenField ID="hdnEdit" runat="server" />
    <asp:HiddenField ID="hdnfunction" runat="server" />
    <asp:HiddenField ID="hdnmaincatfunction" runat="server" />

    <div id="modalConfirmYesNo" class="modal fade">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" 
                class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
                <h4 id="H1" class="modal-title">Confirmation</h4>
            </div>
            <div class="modal-body">
                <p>Are you sure you want to delete this record? Or? </p>
            </div>
            <div class="modal-footer">
                 <asp:Button ID="Button1" runat="server"  CssClass="btn btn-primary"  OnClick="lnkBtnDelete_Click" Text="Yes" ></asp:Button>
                <button id="btncancel" onclick="return hideModal();" type="button" class="btn btn-danger" >No</button>
            </div>
        </div>
    </div>
</div>

                 
               </ContentTemplate>
               </asp:UpdatePanel>
                  
              </div>
          </div>     
        

           </form>
    <script src="adminresources/js/jquery.min.js" type="text/javascript"></script>
    <script src="AdminResources/js/autoCompleter.js"></script>

      <script type="text/javascript">

          Sys.Application.add_load(function () {

              $(".deleteCompany").click(function () {
                  debugger;
                  var companyId = $(this).parent().prev().prev().prev().prev().prev().prev().prev().prev().prev().children().html();
                  $("#<%=hdnmainCatogory.ClientID%>").val(companyId);
                  showDeleteModal();
                  
                  event.preventDefault();
              });
          });
             
    </script>

       <script type="text/javascript">

        $(".editFunction").click(function () {
                  var functionId = $(this).parent().prev().prev().prev().prev().prev().prev().children().html();
                  $("#<%=hdnEdit.ClientID%>").val(functionId);
                  console.log($("#<%=hdnEdit.ClientID%>").val());

              });
       
        
        function hideModal()
        {
            var $confirm = $("#modalConfirmYesNo");
            $confirm.modal('hide');
            return this.false;
        }
        function showModal()
        {
            var $confirm = $("#modalConfirmYesNo");
            $confirm.modal('show');
            return this.false;
         
        }
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

            
        </body>


        </html>

</asp:Content>
