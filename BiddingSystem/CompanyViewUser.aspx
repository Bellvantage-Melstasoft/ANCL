<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true"
    CodeBehind="CompanyViewUser.aspx.cs" Inherits="BiddingSystem.CompanyViewUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">
    <html>
    <head>
        <style type="text/css">
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
        <link href="AdminResources/css/formStyleSheet.css" rel="stylesheet" type="text/css" />
        <script src="AdminResources/js/jquery-1.10.2.min.js"></script>
    </head>
    <body>
        <form runat="server">
        <div id="modalConfirmYesNo" class="modal fade">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                        <h4 id="lblTitleConfirmYesNo" class="modal-title">
                            Confirmation</h4>
                    </div>
                    <div class="modal-body">
                        <p>
                            Are you sure to submit your details ?</p>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btnYesConfirmYesNo" runat="server" CssClass="btn btn-primary" OnClick="BtnUpdate_Click"
                            Text="Yes"></asp:Button>
                        <button id="btnNoConfirmYesNo" type="button" class="btn btn-danger">
                            No</button>
                    </div>
                </div>
            </div>
        </div>
        <div id="modalDeleteYesNo" class="modal fade">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                        <h4 id="lblTitleDeleteYesNo" class="modal-title">
                            Confirmation</h4>
                    </div>
                    <div class="modal-body">
                        <p>
                            Are you sure you want to delete this record? Or ?</p>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-primary" OnClick="lbtnDelete_Click"
                            Text="Yes"></asp:Button>
                        <button id="btnNoDeleteYesNo" onclick="return hideDeleteModal();" type="button" class="btn btn-danger">
                            No</button>
                    </div>
                </div>
            </div>
        </div>
        <section class="content-header">
  
      <h1>
       Update User
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="AdminDashboard.aspx"><i class="fa fa-home"></i> Home</a></li>
        <li class="active">Update User</li>
      </ol>

    </section>
        <section class="content">



    

          <asp:ScriptManager runat="server" ID="sm"></asp:ScriptManager>
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


      <div class="" style="min-height:inherit;" >
             <div class="row" style="min-height:inherit;">
        <div class="col-xs-12">
          <div class="box">
            <div class="box-header">
              <h3 class="box-title">User Details</h3>

             <div class="box-tools">
                <div class="input-group input-group-sm" style="width: 162px;">
                  <asp:TextBox type="text" name="table_search" class="form-control pull-right" placeholder="Search by name" id="txtSearch" runat="server" Width="250px" />

                  <div class="input-group-btn">
                    <%--<asp:Button type="submit" class="btn btn-default" runat="server" autopostback="true" onclick ="SearchAll_Click"><i class="fa fa-search"></i></asp:Button>--%>
                      <asp:Button type="submit" Text="Search" class="btn btn-default fa fa-search" runat="server" autopostback="true" onclick ="SearchAll_Click"  />
                  </div>
                </div>
              </div>
            </div>

            <div class="box-body table-responsive no-padding">
             <asp:GridView runat="server" ID="gvCompanyUsers" CssClass="table table-responsive"  HeaderStyle-BackColor="#3C8DBC" HeaderStyle-ForeColor="White"
                  AutoGenerateColumns="false" GridLines="None" OnRowDataBound="gvCompanyUsers_RowDataBound" EmptyDataText="No Users Found" AllowPaging="true" OnPageIndexChanging="gvCompanyUsers_PageIndexChanging" PageSize="15">
                 <PagerStyle HorizontalAlign = "Center" CssClass = "GridPager" />
                 <Columns>
                       <asp:TemplateField ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                         <ItemTemplate >
                             <asp:Label  runat="server"  ID="lblUserId" Text='<%#Eval("UserId")%>'  ></asp:Label>
                         </ItemTemplate>
                     </asp:TemplateField>
                     <asp:BoundField  DataField="UserId" HeaderText="UserId" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"/>
                      <asp:BoundField  DataField="FirstName" HeaderText="Name"/>
                      <asp:BoundField  DataField="Username" HeaderText="User Name"/>
                    <asp:TemplateField HeaderText="Departments">
                      <ItemTemplate >
                             <asp:Label CssClass="activePhase" runat="server" ID="lblDepartment" Text='<%#Eval("SubDepartmentName")%>' Font-Bold="true" ></asp:Label>
                         </ItemTemplate>
                     </asp:TemplateField>
                     <asp:TemplateField HeaderText="Warehouses" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                      <ItemTemplate >
                             <asp:Label CssClass="activePhase" runat="server" ID="lblWarehouse" Text='<%#Eval("WarehouseName")%>' Font-Bold="true" ></asp:Label>
                         </ItemTemplate>
                     </asp:TemplateField>
                     <asp:TemplateField HeaderText="Designation">
                      <ItemTemplate >
                             <asp:Label  runat="server" ID="lblDesignation" Text='<%# Eval("DesignationId").ToString() != "0" ? listDesignation.Find(x=>x.DesignationId== Convert.ToInt32(Eval("DesignationId").ToString())).DesignationName  : "" %>'  ></asp:Label>
                         </ItemTemplate>
                     </asp:TemplateField>
                      <asp:TemplateField HeaderText="User Role">
                      <ItemTemplate >
                             <asp:Literal  runat="server" ID="lblUserRole"  ></asp:Literal>
                         </ItemTemplate>
                     </asp:TemplateField>
                      <asp:BoundField  DataField="CreatedDate" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" HeaderText="Created Date" DataFormatString='<%$ appSettings:datePattern %>'/>
                     <asp:TemplateField HeaderText="Active">
                         <ItemTemplate >
                             <asp:Label CssClass="activePhase" runat="server" ID="lblIsActive" Text='<%#Eval("IsActive").ToString()=="1"?"Yes":"No"%>' ForeColor='<%#Eval("IsActive").ToString()=="1"?System.Drawing.Color.Green:System.Drawing.Color.Red %>' Font-Bold="true" ></asp:Label>
                         </ItemTemplate>
                     </asp:TemplateField>
                     <asp:TemplateField HeaderText ="Edit">
                         <ItemTemplate>
                                <asp:ImageButton runat="server" ID="btnEdit" ImageUrl="~/images/document.png" OnClick="lbtnEdit_Click" OnClientClick="return scrollFunction()" style="width:26px;height:20px" />
                         </ItemTemplate>
                     </asp:TemplateField>
                       <asp:TemplateField HeaderText ="Delete">
                         <ItemTemplate>
                                <asp:ImageButton ID="lbtnDelete" ImageUrl='<%#Eval("IsActive").ToString()== "1"?"~/images/delete.png":"~/images/dlt.png" %>' Enabled='<%#Eval("IsActive").ToString()== "1"?true:false %>'  ToolTip='<%#Eval("IsActive").ToString()== "1"?"Delete":"Deleted" %>' OnClick="lbtnDelete_Click1" style="width:26px;height:20px;"  runat="server" />
                         </ItemTemplate>
                     </asp:TemplateField>
                 </Columns>
             </asp:GridView>
          </div>
              </div>
        </div>
      </div>
 
      </div>


                            <div id="divEditUser1">
                                  <div class="box box-info" id="divEditUser" runat="server" >
                                    <div class="box-header with-border">
                                      <h3 class="box-title" >Edit User</h3>

                                      <div class="box-tools pull-right">
                                        <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                                        <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
                                      </div>
                                    </div>
                                    <div class="box-body">
                                      <div class="row">
                                      <div class="col-md-12">
                                        <div class="col-md-6">
              

                                             <div class="form-group">
                                            <label for="exampleInputEmail1">Name</label>
                                             <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ForeColor="Red" Font-Bold="true"  ControlToValidate="txtName" ValidationGroup="btnSave">*</asp:RequiredFieldValidator>     
                                            <asp:TextBox ID="txtName" runat="server" CssClass="form-control" placeholder="" Text="" onKeyPress="javascript:scrollFunction();" ></asp:TextBox>
                                            </div>

                                             <div class="form-group">
                                            <label for="exampleInputEmail1">Email Address</label>
                                             <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator6" ForeColor="Red" Font-Bold="true"  ControlToValidate="txtEmilAddress" ValidationGroup="btnSave">*</asp:RequiredFieldValidator>     
                                              <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtEmilAddress" ValidationGroup="btnSave" ForeColor="Red" Font-Bold="true" ErrorMessage="Invalid Email Format"></asp:RegularExpressionValidator>
                                                  <asp:TextBox ID="txtEmilAddress" runat="server" CssClass="form-control" placeholder="" Text=""></asp:TextBox>
                                            </div>

                                                

                                            <div class="form-group">
                                            <label for="exampleInputEmail1">User name</label>
                                             <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ForeColor="Red" Font-Bold="true"  ControlToValidate="txtusername" ValidationGroup="btnSave">*</asp:RequiredFieldValidator>     
                                           <asp:RegularExpressionValidator Display = "Dynamic" ControlToValidate = "txtusername" ID="RegularExpressionValidator3" ValidationGroup="btnSave" ForeColor="Red" ValidationExpression = "^[\s\S]{4,15}$" runat="server" ErrorMessage="Minimum 4 and Maximum 15 characters required."></asp:RegularExpressionValidator>   
                                                  <asp:TextBox ID="txtusername" Enabled="false" runat="server" CssClass="form-control" placeholder="" Text=""></asp:TextBox>
                                            </div>

                                            <div class="form-group">
                                            <label for="exampleInputEmail1">Contatct No</label>
                                              <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtContactNo" ValidationGroup="btnSave"
                                                            ErrorMessage="Maximum 10 Digits" ForeColor="Red" ValidationExpression="[0-9]{10}" Display="Dynamic"></asp:RegularExpressionValidator>
                                                 <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator8" ForeColor="Red" Font-Bold="true"  ControlToValidate="txtContactNo" ValidationGroup="btnSave">*</asp:RequiredFieldValidator>  
                                                <asp:TextBox ID="txtContactNo" type="number" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>

                                               
                                       <%--  <div class="form-group">
                                             <label for="exampleInputEmail1">Password</label>    
                                             <asp:TextBox ID="txtpassword" runat="server" CssClass="form-control" ></asp:TextBox>
                                            </div>--%>
                                         <asp:Panel ID="pwdPanel" runat="server" Visible="false">
                                             <div class="form-group">
                                                <label for="exampleInputEmail1">Password</label>
                                                 <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator12" ForeColor="Red" Font-Bold="true"  ControlToValidate="txtpassword" ValidationGroup="btnSave">*</asp:RequiredFieldValidator>     
                                                 <asp:RegularExpressionValidator Display = "Dynamic" ControlToValidate = "txtpassword" ID="RegularExpressionValidator1" ValidationGroup="btnSave" ForeColor="Red" ValidationExpression = "^[\s\S]{4,}$" runat="server" ErrorMessage="Minimum 4 characters required"></asp:RegularExpressionValidator>
                                                      <asp:TextBox ID="txtpassword" runat="server" CssClass="form-control" placeholder=""  TextMode="Password"></asp:TextBox>
                                            </div>
                

                                             <div class="form-group">
                                                <label for="exampleInputEmail1">Confirm Password</label>
                                                 <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator16" ForeColor="Red" Font-Bold="true"  ControlToValidate="txtConfirmPassword" ValidationGroup="btnSave">*</asp:RequiredFieldValidator>     
                                                <asp:CompareValidator runat="server" ID="CompareValidator2" ForeColor="Red" Font-Bold="true" ControlToCompare="txtpassword"  ControlToValidate="txtConfirmPassword"  ValidationGroup="btnSave">Password doesn't match</asp:CompareValidator>     
                                                     <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="form-control"  TextMode="Password"></asp:TextBox>
                                            </div>
                                         </asp:Panel>
                                            

                                            </div>
                                        <div class="col-md-6">
                                            
                                            <div class="form-group">
                                            <label for="txtEmpNo">Employee No</label>
                                             <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ForeColor="Red" Font-Bold="true"  ControlToValidate="txtEmpNo" ValidationGroup="btnAdd">*</asp:RequiredFieldValidator>  
                                                <asp:TextBox ID="txtEmpNo" type="text" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            
                                         <%--   <asp:Panel Visible="true" runat="server">   
                                         <div class="form-group">
                                             <label for="exampleInputEmail1">Password</label>    
                                             <asp:TextBox ID="txtpassword" runat="server" CssClass="form-control" ></asp:TextBox>
                                            </div>
                                            </asp:Panel>--%>
                
                            <%--                 <div class="form-group">
                                            <label for="exampleInputEmail1">User Type</label>
                                                  <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ForeColor="Red" Font-Bold="true" InitialValue=""  ControlToValidate="ddluserType" ValidationGroup="btnSave">*</asp:RequiredFieldValidator>     
                                             <asp:DropDownList ID="ddluserType" runat="server" CssClass="form-control"  >
                                                 <asp:ListItem Value="" Text="Select User Type" ></asp:ListItem>
                                                 <asp:ListItem Value="A">Admin</asp:ListItem>
                                                 <asp:ListItem Value="M">Manager</asp:ListItem>
                                                   <asp:ListItem Value="U">User</asp:ListItem>
                                             </asp:DropDownList>
                                            </div>--%>

                                              <div class="form-group">
                                            <label for="exampleInputEmail1">Is Active</label>
                                             <asp:CheckBox ID="chkIsActive" runat="server" CssClass="form-control" placeholder="" TextMode="Password"></asp:CheckBox>
                                            </div>

                                                <div class="form-group">
                                                <label for="exampleInputEmail1">User Role</label>
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" InitialValue="0" ControlToValidate="ddlUserRoles" ValidationGroup="btnAdd" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                 <asp:DropDownList ID="ddlUserRoles" runat="server" CssClass="form-control"></asp:DropDownList>
                                                </div>
                                              

                   <div class="form-group">
                            <label for="lblDesigntion">Designation</label>
                
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator7" ForeColor="Red" Font-Bold="true"  ControlToValidate="ddlDesignation" ValidationGroup="btnSave">*</asp:RequiredFieldValidator>     
          
                            <div class="input-group margin">
                            <asp:DropDownList ID="ddlDesignation" runat="server" CssClass="form-control" 
                             AutoPostBack="true" ></asp:DropDownList>
                            <span class="input-group-btn">
                            <asp:Button runat="server" ID="btnCreateNewDesignation"   CssClass="btn btn-info"  
                            Text="Create New Designation" onclick="btnCreateNewDesignation_Click" Enabled="false"/>
                            <%--<asp:Button runat="server" ID="btnEditDesignation"   CssClass="btn btn-info"  
                            Text="Edit Designation" onclick="btnEditDesignation_Click" Enabled="false" />--%>
                            </span>
                            </div>
       
                </div>
                                        </div>
                                        </div>
                                       </div>
                                   





                                        <div class="row">
                                        <div class="col-md-6">
                                            <label for="exampleInputEmail1">Department Information</label>
                                            <div class="table-responsive">
                                                <asp:GridView ID="gvdepartments" runat="server" CssClass="table table-responsive TestTable"
                                                   Style="border-collapse: collapse; color: black;"
                                                    AutoGenerateColumns="false" GridLines="None">
                                                    <Columns>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                      <ItemTemplate>
                                                      <asp:CheckBox runat="server" ID="chkDepartments" Checked='<%# Eval("IsSelected").ToString() =="1"?true:false %>'/>
                                                      </ItemTemplate>
                                                      </asp:TemplateField>
                                                     <asp:BoundField DataField="DepartmentId" HeaderText="SubDepartmentId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"  />
                                                    <asp:BoundField DataField="DepartmentName" HeaderText="Deparment" /> 
                                                    <asp:TemplateField HeaderText="Is Head" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="centerHeaderText">
                                                       <ItemTemplate>
                                                       <asp:CheckBox runat="server" ID="chkIsHead" Checked='<%# Eval("IsHead").ToString() =="1"?true:false %>'/>
                                                       </ItemTemplate>
                                                       </asp:TemplateField>
                                                </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                   

                                      
                  <div class="col-md-6">
                                           <label for="exampleInputEmail1">Warehouse Information</label>
                                            <div class="table-responsive">
                                                <asp:GridView ID="gvwarehouse" runat="server" CssClass="table table-responsive TestTable"
                                                   Style="border-collapse: collapse; color: black;"
                                                    AutoGenerateColumns="false" GridLines="None" OnRowDataBound="gvwarehouse_RowDataBound">
                                                    <Columns>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                      <ItemTemplate>
                                                      <asp:CheckBox runat="server" ID="chkwarehouse" Checked='<%# Eval("IsSelected").ToString() =="1"?true:false %>'/>
                                                      </ItemTemplate>
                                                      </asp:TemplateField>
                                                    <asp:BoundField DataField="WrehouseId" HeaderText="WarehouseId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                    <asp:BoundField DataField="Location" HeaderText="Warehouse" />
                                                   <asp:TemplateField HeaderText="User Type" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="centerHeaderText">
                                                       <ItemTemplate>
                                                      <asp:DropDownList ID="ddlUserType" runat="server" CssClass="form-control" > 
                                                         <asp:ListItem Value="1" >Head of Warehouse</asp:ListItem>  
                                                         <asp:ListItem Value="2">Store Keeper</asp:ListItem>  
                                                       
                                                     </asp:DropDownList>
                                                       </ItemTemplate>
                                                       </asp:TemplateField>
                                                </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
       </div>


 </div>




      
                                    <div class="box-footer">
                                        <span class="pull-right">
                                             <asp:Button ID="btnUdate" runat="server" Text="Update"  ValidationGroup="btnSave" CssClass="btn btn-primary " OnClick="BtnUpdate_Click"></asp:Button>
                                             <asp:Button ID="btnCancel"  runat="server" Text="Cancel"  CssClass="btn btn-danger" OnClick="btnCancel_Click"></asp:Button>
                                        </span>
                                             </div>
            
                                  </div>
                                </div>   <div id="myModalAddDesignation" class="modal modal-primary fade in" tabindex="-1" role="dialog" aria-hidden="false">
                                <div class="modal-dialog">
                                    <!-- Modal content-->
                                    <div class="modal-content" style="background-color: #a2bdcc;">
                                        <div class="modal-header" style="background-color: #7bd47dfa;">
                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="False">×</span></button>
                                            <h4 class="modal-title">Create Designation</h4>
                                        </div>
                                        <div class="modal-body">
                                            <div class="login-3lsw">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                       
                    <div class="col-md-12">
                        <div class="form-group">
                        <label for="lbl1" style="color:Black;">Designation Name</label>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ForeColor="Red" Font-Bold="true"  ControlToValidate="txtDesignationName" ValidationGroup="btnSaveDesignation">*</asp:RequiredFieldValidator>     
                           
                            <asp:TextBox ID="txtDesignationName" runat="server" CssClass="form-control" ></asp:TextBox>
                        </div>
              
                        <span class="pull-right">

                            <asp:Button ID="btnSaveDesignation" runat="server" Text="Save Designation"  
                            ValidationGroup="btnSaveDesignation" CssClass="btn btn-primary  " OnClientClick ="RemoveBackdrop();"
                            onclick="btnSaveDesignation_Click" Visible="true" ></asp:Button>

                               <asp:Button ID="btnUpdateDesignation" runat="server" Text="Update Designation"  
                            ValidationGroup="btnUpdateDesignation" CssClass="btn btn-primary " OnClientClick ="RemoveBackdrop();"
                            onclick="btnUpdateDesignation_Click" Visible="false" ></asp:Button>
                  
                        </span>
               
                    </div>

                                                    </div>
                                                    
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnUdate" />
                <asp:PostBackTrigger ControlID="btnCancel" />
            </Triggers>
              </asp:UpdatePanel>
    </section>
        <asp:HiddenField runat="server" ID="hdnUserId" />
        </form>
    </body>
    </html>
    <script type="text/javascript">


        function scrollFunction() {
            document.getElementById("divEditUser1").scrollIntoView();
            return true;
        }

        function reply_click(clicked_id) {
            window.location.replace("CompanyUpdatingAndRatingSupplier.aspx?RequestId=" + clicked_id);
        }
        $("#btnNoConfirmYesNo").on('click').click(function () {
            var $confirm = $("#modalConfirmYesNo");
            $confirm.modal('hide');
            return this.false;
        });
    </script>
    <script src="AdminResources/js/autoCompleter.js"></script>
    <script type="text/javascript">


        function hideModal() {
            var $confirm = $("#modalConfirmYesNo");
            $confirm.modal('hide');
            return this.false;
        }
        function hideDeleteModal() {
            var $confirm = $("#modalDeleteYesNo");
            $confirm.modal('hide');
            return this.false;
        }
        function showDeleteModal() {
            $("#<%=hdnUserId.ClientID%>").val()
            var $confirm = $("#modalDeleteYesNo");
            $confirm.modal('show');
            return this.false;
        }
    </script>
    <script>
        function deleteUserClick(val) {
            var userID = val;
            $("#<%=hdnUserId.ClientID%>").val(userID);
            showDeleteModal();
        }

        Sys.Application.add_load(function () {

            $(".deleteUser").click(function () {
                var userID = $(this).parent().prev().prev().prev().prev().prev().prev().prev().prev().children().html();
                $("#<%=hdnUserId.ClientID%>").val(userID);
                showDeleteModal();
            });

            
                
            

        });
    </script>
    <script type="text/javascript">

        var rows;
        var coldata;
        //  $(document).ready(function () {
       <%-- $('#ContentSection_txtSearch').keyup(function () {
            $('#<%=gvCompanyUsers.ClientID%>').find('tr:gt(0)').hide();
            var data = $('#ContentSection_txtSearch').val();
            var len = data.length;
            if (len > 0) {
                $('#<%=gvCompanyUsers.ClientID%>').find('tbody tr').each(function () {
                    coldata = $(this).children().eq(2);
                    var temp = coldata.text().toUpperCase().indexOf(data.toUpperCase());
                    if (temp === 0) {
                        $(this).show();
                    }
                });
            } else {
                $('#<%=gvCompanyUsers.ClientID%>').find('tr:gt(0)').show();
            }         
        });--%>

        
        function RemoveBackdrop() {
            
            $('#myModalAddDesignation').hide();
            $('.modal-backdrop').remove();
            $('body').css("overflow", "auto");
            $('body').css("padding-right", "0");
        }
    </script>
</asp:Content>
