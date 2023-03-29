<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true"
    CodeBehind="WarehouseReleaseStock.aspx.cs" Inherits="BiddingSystem.WarehouseReleaseStock" %>

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

            </head>

        <body>


    <section class="content-header">
      <h1>
       Transfer Inventory
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="AdminDashboard.aspx"><i class="fa fa-home"></i> Home</a></li>
        <li class="active">Transfer Inventory</li>
      </ol>
    </section>
    <br />


   

    <form id="form1" runat="server">
    <asp:HiddenField ID="hdnImgPathEdit" runat="server" />
            <asp:HiddenField ID="hdnTGermsPathEdit" runat="server" />

    <asp:ScriptManager runat="server" ID="sm">
     </asp:ScriptManager>
        <asp:UpdatePanel ID="Updatepanel1" runat="server">
            <ContentTemplate>
        <div id="SuccessAlert" class="modal fade">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header" style="background-color: #3c8dbc;">
                                <button type="button"
                                    class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                                <h4 class="modal-title" style="color:white; font-weight: bold;">Success</h4>
                            </div>
                            <div class="modal-body">
                                <p id="successMessage" style="font-weight: bold; font-size: medium;"></p>
                            </div>
                            <div class="modal-footer">
                                <span class="btn btn-info" data-dismiss="modal" aria-label="Close">OK</span>
                                <%--<button id="btnOki" class="btn btn-success">OK</button>--%>
                            </div>
                        </div>
                    </div>
                </div>

                <div id="errorAlert" class="modal fade">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header" style="background-color: #ff0000;">
                                <button type="button"
                                    class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                                <h4 class="modal-title" style="color:white; font-weight: bold;">ERROR</h4>
                            </div>
                            <div class="modal-body">
                                <p id="errorMessage" style="font-weight: bold; font-size: medium;"></p>
                            </div>
                            <div class="modal-footer">
                                <span class="btn btn-danger" data-dismiss="modal" aria-label="Close">OK</span>
                                <%--<button id="btnOki" class="btn btn-success">OK</button>--%>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="alert  alert-info  alert-dismissable" id="msg" runat="server" visible="false">
                            <a href="#" class="close" data-dismiss="alert" aria-label="close">×</a>
                            <strong>
                                <asp:Label ID="lbMessage" runat="server"></asp:Label>
                            </strong>
                        </div>

                <%--<div id="modalDeleteYesNo" class="modal fade">
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
                                <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-primary" OnClick="lnkBtnDelete_Click" OnClientClick="return hideDeleteModal();" Text="Yes"></asp:Button>
                                <button id="btnNoDeleteYesNo" onclick="return hideDeleteModal();" type="button" class="btn btn-danger">No</button>
                            </div>
                        </div>
                    </div>
                </div>--%>
    <section class="content">

        


        

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
          <div class="row">
            <div class="col-md-6">
            


               <div class="form-group">
                    <label for="exampleInputEmail1">Warehouse</label>
                      <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ForeColor="Red" Font-Bold="true" InitialValue=""  ControlToValidate="ddlWarehouse" ValidationGroup="btnSave">*</asp:RequiredFieldValidator>     
                    <asp:DropDownList ID="ddlWarehouse" runat="server" CssClass="form-control"   
                        AutoPostBack="true" 
                        onselectedindexchanged="ddlWarehouse_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
            
                <div class="form-group">
                    <label for="exampleInputEmail1">Item</label>
                      <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ForeColor="Red" Font-Bold="true" InitialValue=""  ControlToValidate="ddlItems" ValidationGroup="btnSave">*</asp:RequiredFieldValidator>     
                    <asp:DropDownList ID="ddlItems" runat="server" CssClass="form-control" AutoPostBack="true" 
                        onselectedindexchanged="ddlItems_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>


              <%--   <div class="form-group">
                <label for="exampleInputEmail1">Address2</label>  
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtAddrss2"  ValidationGroup="btnSave" ID="RequiredFieldValidator3" ForeColor="Red">*</asp:RequiredFieldValidator>
                    <asp:TextBox runat="server" ID="txtAddrss2" CssClass="form-control " autocomplete="off" ></asp:TextBox>
                </div>--%>


                      <div class="form-group">
                <label for="exampleInputEmail1">Quantity</label>  
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ForeColor="Red" Font-Bold="true"  ControlToValidate="txtQty" ValidationGroup="btnSave">*</asp:RequiredFieldValidator>     
                    <asp:TextBox runat="server" ID="txtQty" CssClass="form-control"  type="number" autocomplete="off" min="1" ></asp:TextBox>
                </div>

                

                </div>
              <div class="col-md-6">

                  

                  <asp:Panel ID="pnlReleaseTo" CssClass="form-group" runat="server">
                    <label for="ddlReleasedTo">Release To</label>
                      <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ForeColor="Red" Font-Bold="true" InitialValue=""  ControlToValidate="ddlReleasedTo" ValidationGroup="btnSave">*</asp:RequiredFieldValidator>     
                    <asp:DropDownList ID="ddlReleasedTo" runat="server" CssClass="form-control">
                    </asp:DropDownList>
                </asp:Panel>

                  <div class="form-group">
                <label for="exampleInputEmail1">Description</label>  
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator6" ForeColor="Red" Font-Bold="true"  ControlToValidate="txtDescription" ValidationGroup="btnSave">*</asp:RequiredFieldValidator>     
                    <asp:TextBox runat="server" ID="txtDescription" CssClass="form-control"  type="text" autocomplete="off"></asp:TextBox>
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
     
      <!-- /.box -->
    </section>
                
    <%--<div class="panel-body">
        <div class="co-md-12">
            <div class="table-responsive">
                <asp:GridView ID="gvDepartments" runat="server" CssClass="table table-responsive tablegv" GridLines="None" AutoGenerateColumns="false" EmptyDataText="No Records Found" AllowPaging="true" PageSize="10" OnPageIndexChanging="gvDepartments_PageIndexChanging" >
                    <Columns>
                         <asp:TemplateField HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" >
                            <ItemTemplate>
                                <asp:Label  runat="server" ID="lblDepartmentId" Text='<%#Eval("SubDepartmentID")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="SubDepartmentID" HeaderText="ItemId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                        <asp:BoundField DataField="SubDepartmentName" HeaderText="Department Name" />
                        <asp:BoundField DataField="PhoneNo" HeaderText="Phone No" />
                        <asp:TemplateField HeaderText="Active">
                            <ItemTemplate>
                                <asp:Label CssClass="activePhase" runat="server" ID="lblIsActive" Text='<%#Eval("IsActive").ToString()=="1"?"Yes":"No"%>'
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
                               <%-- <asp:ImageButton ID="btnDeleteCompany" ImageUrl="~/images/delete.png"  class="deleteCompany" Style="width: 26px; height: 20px;" runat="server"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>--%>
        
         </ContentTemplate>
        </asp:UpdatePanel>
         <asp:HiddenField ID="hdnDepartmentId" runat="server" />
         <asp:HiddenField ID="hdnTermsConditionUrl" runat="server" />
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <div id="snackbar">
        Phone number should has 10 Digits</div>
    <div id="snackbarMobileNumber">
        Mobile number should has 10 Digits</div>
    <div id="snackbarFileUpload">
        Please upload file having extensions .jpeg/.jpg/.png/.gif only.</div>
    </form>
    <script src="adminresources/js/jquery.min.js" type="text/javascript"></script>
    <script src="AdminResources/js/autoCompleter.js"></script>
    <script type="text/javascript">

        <%--$(function () {
            $('#<%=txtPhoneNo.ClientID%>').keypress(function (e) {
                if (e.which != 69 && e.which != 101 && e.which != 45 && e.which != 43 && e.which != 42 )  {
                    if(<%=txtPhoneNo.ClientID%>.value.length<10)
                    { }
                    else {
                        return false;
                    }
                } else {
                    return false;
                }
            });
        });--%>

         <%--var countries = <%= getJsonDepartmentList() %>;
        autocomplete(document.getElementById("ContentSection_txtDepartmentName"), countries);--%>



       
    </script>
    <%--<script type="text/javascript">
        $("#<%=btnSave1.ClientID %>").click(function () {

            var fileInput = $("<%=fileUpload1.ClientID%>").val();
            var phoneNo = $("#<%=txtPhoneNo.ClientID %>").val();
            var mobileNo = $("#<%=txtMobileNo.ClientID %>").val();

            if (phoneNo.length != 10) {
                var x = document.getElementById("snackbar");
                x.className = "show";
                setTimeout(function () { x.className = x.className.replace("show", ""); }, 3000);
                $("#<%=txtPhoneNo.ClientID %>").css('border-color', 'red');
                return false;
            }
            else {
                $("#<%=txtPhoneNo.ClientID %>").css('border-color', '#d2d6de');

                if (mobileNo != "") {
                    if (mobileNo.length != 10) {
                        var x = document.getElementById("snackbarMobileNumber");
                        x.className = "show";
                        setTimeout(function () { x.className = x.className.replace("show", ""); }, 3000);
                        $("#<%=txtMobileNo.ClientID %>").css('border-color', 'red');
                        return false;
                    }
                    else {
                        $("#<%=txtMobileNo.ClientID %>").css('border-color', '#d2d6de');

                        if (fileInput != "") {
                            var filePath = input.value;
                            var allowedExtensions = /(\.jpg|\.jpeg|\.png|\.gif)$/i;

                            if (!allowedExtensions.exec(filePath)) {
                                var x = document.getElementById("snackbarFileUpload");
                                x.className = "show";
                                setTimeout(function () { x.className = x.className.replace("show", ""); }, 3000);
                                return false;
                            }
                        }
                        else {
                            return true;
                        }

                        return true;
                    }
                } else $("#<%=txtMobileNo.ClientID %>").css('border-color', '#d2d6de');
            }
        });
    </script>--%>
    <script type="text/javascript">
        
        
    </script>

     <script>

        <%--Sys.Application.add_load(function() {

            $(".deleteCompany").click(function () {
                var companyId = $(this).parent().prev().prev().prev().prev().prev().prev().children().html();
                $("#<%=hdnDepartmentId.ClientID%>").val(companyId);
                showDeleteModal();
                event.preventDefault();
            });
        });--%>
    </script>
       <script type="text/javascript">

       
        function hideModal()
        {
            var $confirm = $("#modalConfirmYesNo");
            $confirm.modal('hide');
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

<script>
    

</script>

            
        </body>





        </html>

</asp:Content>
