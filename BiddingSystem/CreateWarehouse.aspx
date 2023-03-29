<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true"
    CodeBehind="CreateWarehouse.aspx.cs" Inherits="BiddingSystem.CreateWarehouse" %>

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

input[type=number]::-webkit-inner-spin-button, 
input[type=number]::-webkit-outer-spin-button { 
  -webkit-appearance: none; 
  margin: 0; 
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

         .select2-container--default .select2-selection--multiple .select2-selection__rendered .select2-selection__choice {
            color: black;
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
<link href="AdminResources/css/select2.min.css" rel="stylesheet" />

    <section class="content-header">
      <h1>
       Create Warehouse
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="AdminDashboard.aspx"><i class="fa fa-home"></i> Home</a></li>
        <li class="active">Create Warehouse</li>
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
                <p>Are you sure you want to delete this record? </p>
            </div>
            <div class="modal-footer">
                 <asp:Button ID="btnDelete" runat="server"  CssClass="btn btn-primary"  OnClick="lnkBtnDelete_Click" OnClientClick="return hideDeleteModal();" Text="Yes" ></asp:Button>
                <button id="btnNoDeleteYesNo" onclick="return hideDeleteModal();" type="button" class="btn btn-danger">No</button>
            </div>
        </div>
    </div>
</div>
    <section class="content" style="padding-top:0px">

        


        

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
                <label for="exampleInputEmail1">Location Name</label>  
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtLocation"  ValidationGroup="btnSave" ID="RequiredFieldValidator1" ForeColor="Red">*</asp:RequiredFieldValidator>
                    <asp:TextBox runat="server" ID="txtLocation" CssClass="form-control " autocomplete="off" ></asp:TextBox>
                </div>



                      <div class="form-group">
                <label for="exampleInputEmail1">Phone No</label>  
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator7" ForeColor="Red" Font-Bold="true"  ControlToValidate="txtPhoneNo" ValidationGroup="btnSave">*</asp:RequiredFieldValidator>     
              <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtPhoneNo"  ValidationGroup="btnSave" ErrorMessage="Phone no should be in 10 Digits" ForeColor="Red" ValidationExpression="[0-9]{10}" ></asp:RegularExpressionValidator>
                    <asp:TextBox runat="server" ID="txtPhoneNo" CssClass="form-control"  type="number" autocomplete="off" min="0" ></asp:TextBox>
                </div>

                <div class="form-group">
                <label for="exampleInputEmail1">Address</label>  
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtAddress"  ValidationGroup="btnSave" ID="RequiredFieldValidator2" ForeColor="Red">*</asp:RequiredFieldValidator>
                    <asp:TextBox runat="server" ID="txtAddress" CssClass="form-control " autocomplete="off" Rows="5"></asp:TextBox>
                </div>
               

                </div>

              
            <div class="col-md-6">

                <div class="form-group">
                    <label for="exampleInputEmail1">Head of Warehouse</label>
                      <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ForeColor="Red" Font-Bold="true" InitialValue=""  ControlToValidate="ddlHeadOfWarehouse" ValidationGroup="btnSave">*</asp:RequiredFieldValidator>     
                     <asp:ListBox ID="ddlHeadOfWarehouse" SelectionMode="Multiple" runat="server" CssClass="select2 form-control"   style="color:black; width:100%">
                    </asp:ListBox>
                    <%--<asp:DropDownList ID="ddlHeadOfWarehouse" runat="server" CssClass="form-control">
                    </asp:DropDownList>--%>
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
     
      <!-- /.box -->
    </section>
                
    <div class="panel-body">
        <div class="co-md-12">
            <div class="table-responsive">
                <asp:GridView ID="gvWarehouses" runat="server" CssClass="table table-responsive tablegv" GridLines="None" AutoGenerateColumns="false" EmptyDataText="No Records Found" AllowPaging="true" PageSize="10" OnPageIndexChanging="gvDepartments_PageIndexChanging" >
                    <Columns>
                         <asp:TemplateField HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" >
                            <ItemTemplate>
                                <asp:Label  runat="server" ID="lblWarehouseId" Text='<%#Eval("WarehouseID")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="WarehouseID" HeaderText="ItemId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                        <asp:BoundField DataField="Location" HeaderText="Location" />
                        <asp:BoundField DataField="PhoneNo" HeaderText="Phone No" />
                        <asp:BoundField DataField="Address" HeaderText="Address" />
                        <asp:BoundField DataField="HeadOfWarehouseName" HeaderText="Head of Warehouse" />
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
                                <asp:ImageButton ID="btnDeleteCompany" ImageUrl='<%#Eval("IsActive").ToString()== "1"?"~/images/delete.png":"~/images/dlt.png" %>' Enabled='<%#Eval("IsActive").ToString()== "1"?true:false %>' OnClick="btnDeleteCompany_Click"  ToolTip='<%#Eval("IsActive").ToString()== "1"?"Delete":"Deleted" %>'  CssClass="deleteCompany" style="width:26px;height:20px;" runat="server"/>
                               <%-- <asp:ImageButton ID="btnDeleteCompany" ImageUrl="~/images/delete.png"  class="deleteCompany" Style="width: 26px; height: 20px;" runat="server"/>--%>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
        
         </ContentTemplate>
        </asp:UpdatePanel>
         <asp:HiddenField ID="hdnWarehouseId" runat="server" />
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

        $(function(){
            $(':input[type=number]').on('mousewheel',function(e){ $(this).blur(); });
        });

        $(function () {
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
        });

         var countries = <%= getJsonWarehouseList() %>;
        autocomplete(document.getElementById("ContentSection_txtLocation"), countries);



       
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
            <script src="AdminResources/js/select2.full.min.js"></script>
    <script src="AdminResources/js/datetimepicker/datetimepicker.min.js"></script>
    <script src="AdminResources/js/datetimepicker/datetimepicker.js"></script>
    <link href="AdminResources/css/datetimepicker/datetimepicker.base.css" rel="stylesheet" />
    <link href="AdminResources/css/datetimepicker/datetimepicker.css" rel="stylesheet" />
    <link href="AdminResources/css/datetimepicker/datetimepicker.themes.css" rel="stylesheet" />
    <script src="AdminResources/js/daterangepicker.js" type="text/javascript"></script>
   
       <script type="text/javascript">
           Sys.Application.add_load(function () {
               $(function () {
                   $('.select2').select2();


               })

           });
            
       
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
