<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="CompanyCreateSupplierAgent.aspx.cs" Inherits="BiddingSystem.CompanyCreateSupplierAgent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">


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

            .tablegv tr:nth-child(even) {
                background-color: #f2f2f2;
            }

            .tablegv tr:hover {
                background-color: #ddd;
            }

            .tablegv th {
                padding-top: 12px;
                padding-bottom: 12px;
                text-align: left;
                background-color: #3C8DBC;
                color: white;
            }

        .successMessage {
            color: #1B6B0D;
            font-size: medium;
        }

        .failMessage {
            color: #C81A34;
            font-size: medium;
        }

        #snackbar, #snackbarMobileNumber, #snackbarFileUpload {
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

            #snackbar.show, #snackbarMobileNumber.show, #snackbarFileUpload.show {
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
            from {
                bottom: 0;
                opacity: 0;
            }

            to {
                bottom: 30px;
                opacity: 1;
            }
        }

        @keyframes fadein {
            from {
                bottom: 0;
                opacity: 0;
            }

            to {
                bottom: 30px;
                opacity: 1;
            }
        }

        @-webkit-keyframes fadeout {
            from {
                bottom: 30px;
                opacity: 1;
            }

            to {
                bottom: 0;
                opacity: 0;
            }
        }

        @keyframes fadeout {
            from {
                bottom: 30px;
                opacity: 1;
            }

            to {
                bottom: 0;
                opacity: 0;
            }
        }
    </style>
    <script src="AdminResources/js/jquery.min.js" type="text/javascript"></script>



    <div id="errorAlert" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header" style="background-color: #ff0000;">
                    <button type="button"
                        class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                    <h4 class="modal-title" style="color: white; font-weight: bold;">ERROR</h4>
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

    <div id="SuccessAlert" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header" style="background-color: #3c8dbc;">
                    <button type="button"
                        class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                    <h4 class="modal-title" style="color: white; font-weight: bold;">Success</h4>
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




    <form id="form1" runat="server" defaultbutton="btnSave">
        <div id="modalDeleteYesNo" class="modal fade">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                        <h4 id="lblTitleDeleteYesNo" class="modal-title">Confirmation</h4>
                    </div>
                    <div class="modal-body">
                        <p>Are you sure you want to delete this record ?</p>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="Button2" runat="server" CssClass="btn btn-primary" OnClick="lbtnDelete_Click" Text="Yes"></asp:Button>
                        <button id="btnNoDeleteYesNo1" onclick="return hideDeleteModal();" type="button" class="btn btn-danger">
                            No</button>
                    </div>
                </div>
            </div>
        </div>
        <section class="content-header">
      <h1>
       Create Supplier Agent (Only For Import)
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="AdminDashboard.aspx"><i class="fa fa-home"></i> Home</a></li>
        <li class="active">Create Supplier Agent</li>
      </ol>
    </section>




        <asp:ScriptManager runat="server" ID="sm">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="updatepanel1" runat="server">
            <ContentTemplate>
                <section class="content">
                     <asp:HiddenField ID="btnEdit" value="0" runat="server" />
          <asp:HiddenField ID="hndAgentId" value="0" runat="server" />
      <div class="row" style=" visible="false">
        <div class="col-sm-12">
           <div class="alert  alert-info  alert-dismissable" id="msg" runat="server" visible="false">
           <a href="#" class="close" data-dismiss="alert" aria-label="close">×</a>
           <strong>
               <asp:Label ID="lbMessage" ForeColor="White" runat="server"></asp:Label>
           </strong>
    </div>
        </div>
    </div>
    
       <div class="box box-info">
        <div class="box-header with-border">
          <h3 class="box-title" >Agent Details</h3>

          <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
            <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
          </div>
        </div>
          <div class="box-body">
              <div class="box-body table-responsive no-padding">
                    <asp:GridView runat="server" ID="gvSupplierAgentList"
                    CssClass="table table-responsive" AutoGenerateColumns="false"  PageSize="8" AllowPaging="true" OnPageIndexChanging="gvSupplierAgentList_PageIndexChanging"
                    GridLines="None" HeaderStyle-BackColor="#3C8DBC" HeaderStyle-ForeColor="White" >
                    <Columns>
                        <asp:BoundField DataField="SupplierId" HeaderText="SupplierId"  HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden SupplierId"/>
                           <asp:BoundField DataField="AgentId" HeaderText="AgentId"  HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden AgentId"/>
                         <asp:TemplateField HeaderText="Supplier Name">
                            <ItemTemplate>
                                <asp:Label ID="lblSupplierName" runat="server" Text='<%#listSupplier.Find(x=>x.SupplierId == Convert.ToInt32(Eval("SupplierId").ToString())).SupplierName %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField> 
                        <asp:BoundField DataField="AgentName" HeaderText="Agent Name" />                        
                        <asp:BoundField DataField="Address" HeaderText="Address" />
                        <asp:BoundField DataField="Email" HeaderText="Email Address" />
                        <asp:BoundField DataField="ContactNo" HeaderText="Contact No" />
                        <asp:TemplateField HeaderText="Edit">
                            <ItemTemplate>
                                <asp:LinkButton runat="server" ID="lbtnEdit"
                                    OnClick="lbtnEdit_Click">Edit
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                          <asp:TemplateField HeaderText="Delete">
                            <ItemTemplate>
                                <a  id="lbtnDelete"  onclick="showWarningModel(this)" style="cursor:pointer" >Delete</a>                            
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>                    
              </div>
          </div>
       </div>             
      
      <div class="box box-info">
        <div class="box-header with-border">
          <h3 class="box-title" >Add / Update Agent Details</h3>

          <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
            <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
          </div>
        </div>
        <!-- /.box-header -->
        <div class="box-body"id="DivEdit">
          <div class="row">
             <%-- <div class="col-sm-12">
                   <div class="form-group">
                     <div class="col-sm-6">
                <label for="exampleInputEmail1">Select Supplier</label>
                 <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ForeColor="Red" Font-Bold="true"  ControlToValidate="ddlSupplier" ValidationGroup="btnSave">*</asp:RequiredFieldValidator>
                      <asp:DropDownList ID="ddlSupplier" runat="server" CssClass="form-control" placeholder=""></asp:DropDownList>
                         </div>
                     <div class="col-sm-6">
                          <label for="exampleInputEmail1" style="display:none;">Select Supplier</label>
                      <asp:Button ID="btnSearch" runat="server" Text="Search"   CssClass="btn btn-primary " OnClick="btnSearch_Click"></asp:Button>
                     </div>
                </div>
              </div>--%>
            <div class="col-md-6">
                <div class="form-group">
                <label for="exampleInputEmail1">Select Supplier</label>
                 <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ForeColor="Red" Font-Bold="true"  ControlToValidate="ddlSupplier" ValidationGroup="btnSave">*</asp:RequiredFieldValidator>
                      <asp:DropDownList ID="ddlSupplier" runat="server" CssClass="form-control" placeholder=""></asp:DropDownList>
                         </div>
                     
                
                 <div class="form-group">
                <label for="exampleInputEmail1">Agent Name</label>
                 <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ForeColor="Red" Font-Bold="true"  ControlToValidate="txtAgentName" ValidationGroup="btnSave">*</asp:RequiredFieldValidator>
                      <asp:TextBox ID="txtAgentName" runat="server" CssClass="form-control" placeholder="" Text=""></asp:TextBox>
                </div>

                 <div class="form-group">
                <label for="exampleInputEmail1">Address</label>
                 <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator6" ForeColor="Red" Font-Bold="true"  ControlToValidate="txtAddress" ValidationGroup="btnSave">*</asp:RequiredFieldValidator>     
                <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" placeholder="" Text=""></asp:TextBox>
                </div>

                 <div class="form-group">
                <label for="exampleInputEmail1">Email Address</label>
                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator12" ForeColor="Red" Font-Bold="true"  ControlToValidate="txtEmailAddress" ValidationGroup="btnSave">*</asp:RequiredFieldValidator>     
                <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtEmailAddress"
                ForeColor="Red" ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
                ErrorMessage = "Invalid email address"/>
                
                     <asp:TextBox ID="txtEmailAddress" runat="server" type="email" CssClass="form-control" placeholder="" Text=""></asp:TextBox>
                </div>
               
                <div class="form-group">
                <label for="exampleInputEmail1">Contact No</label>
                 <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator7" ForeColor="Red" Font-Bold="true"  ControlToValidate="txtOfficeContactNo" ValidationGroup="btnSave">*</asp:RequiredFieldValidator>     
                 <asp:RegularExpressionValidator ID="RegularExpressionValidator2"
                                                runat="server" ControlToValidate="txtOfficeContactNo" ValidationGroup="btnSave"
                                                ErrorMessage="Contact no should has 10 Digits" ForeColor="Red"
                                                ValidationExpression="[0-9]{10}"></asp:RegularExpressionValidator>
                 
                    <asp:TextBox ID="txtOfficeContactNo" type="number" runat="server" CssClass="form-control" placeholder="" Text="" min="0"></asp:TextBox>
                </div>
            
           </div>
          <!-- /.row -->
        </div>
        <div class="box-footer">
            <span class="pull-right">
                 <asp:Button ID="btnSave" runat="server" Text="Submit"  ValidationGroup="btnSave" CssClass="btn btn-primary " OnClick="BtnSave_Click"></asp:Button>
                 <asp:Button ID="btnClear"  runat="server" Text="Clear"  CssClass="btn btn-danger" OnClick="btnClear_Click"></asp:Button>
               
            </span>
                 </div>
        <!-- /.box-body -->
      </div>
        </div>
      
         
    </section>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div id="snackbar">
            Office Contact no should has 10 Digits
        </div>
        <div id="snackbarMobileNumber">
            Mobile number should has 10 Digits
        </div>

    </form>


    <script type="text/javascript">

        $(function(){
            $(':input[type=number]').on('mousewheel',function(e){ $(this).blur(); });
        });

        function validateFields() {
            var email=document.getElementById('ContentSection_txtEmailAddress').value;
            var re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
            if(re.test(String(email).toLowerCase()))
            {
                return true;
            }
            else
            {
                document.getElementById('errorMessage').innerHTML = "Please enter a valid email address";
                $('#errorAlert').modal('show');
                return false;
            }

                    
        }

        $(function () {
            $('#<%=txtOfficeContactNo.ClientID%>').keypress(function (e) {
                if (e.which != 69 && e.which != 101 && e.which != 45 && e.which != 43 && e.which != 42) {
                    if(<%=txtOfficeContactNo.ClientID%>.value.length<10)
                    { }
                    else {
                        return false;
                    }
                } else {
                    return false;
                }
            });
        });

        function showWarningModel(obj){
            var agentId = $(obj).closest('tr').find('td.AgentId').html();
            $("#ContentSection_hndAgentId").val(agentId);
            var $confirm = $("#modalDeleteYesNo");
            $confirm.modal('show');
            event.preventDefault();

                  
            return this.false;
        }
        function hideDeleteModal() {
            var $confirm = $("#modalDeleteYesNo");
            $confirm.modal('hide');
            return this.false;
        }
     
    </script>


</asp:Content>
