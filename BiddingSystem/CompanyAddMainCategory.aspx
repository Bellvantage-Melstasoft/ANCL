<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="CompanyAddMainCategory.aspx.cs"
    Inherits="BiddingSystem.CompanyAddMainCategory" %>

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
            /*.tablegv tr:hover {background-color: #ddd;}*/
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

        select#ContentSection_multiselect > option {
            padding: 7px;
            border-bottom: 1px solid #dddddd;
        }

        select#ContentSection_multiselect_to > option {
            padding: 7px;
            border-bottom: 1px solid #dddddd;
        }

        #ContentSection_multiselect::-webkit-scrollbar {
            width: 12px; /* for vertical scrollbars */
            height: 12px; /* for horizontal scrollbars */
        }

        #ContentSection_multiselect::-webkit-scrollbar-track {
            background: rgba(0, 0, 0, 0.1);
        }

        #ContentSection_multiselect::-webkit-scrollbar-thumb {
            background: rgba(0, 0, 0, 0.5);
            border-radius: 20px;
        }

        .button-edit {
            background-image: url(../images/document.png); /* 16px x 16px */
            background-color: transparent; /* make the button transparent */
            background-repeat: no-repeat; /* make the background image appear only once */
            background-position: 0px 0px; /* equivalent to 'top left' */
            border: none; /* assuming we don't want any borders */
            cursor: pointer; /* make the cursor like hovering over an <a> element */
            height: 16px; /* make this the size of your image */
            padding-left: 16px; /* make text start to the right of the image */
            vertical-align: middle;
        }

        /*table#ContentSection_gvMainCategory tbody tr th:nth-child(2), td:nth-child(2) {
            width: 4%;
        }*/
    </style>


    <section class="content-header">
      <h1>
       Add Main Category
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="AdminDashboard.aspx"><i class="fa fa-home"></i> Home</a></li>
        <li class="active">Add Main Category</li>
      </ol>
    </section>
    <br />
    <form id="form1" runat="server">
        <div id="modalDeactivateYesNo" class="modal fade">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                        <h4 id="lblTitleDeactivateYesNo" class="modal-title">Confirmation</h4>
                    </div>
                    <div class="modal-body">
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btnDeactivate" runat="server" CssClass="btn btn-primary" OnClick="lnkBtnDeactivate_Click"
                            Text="Yes"></asp:Button>
                        <button id="btnNoDeactivateYesNo" onclick="return hideDeactivateModal();" type="button" class="btn btn-danger">
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
                        <h4 id="lblTitleDeleteYesNo" class="modal-title">Confirmation</h4>
                    </div>
                    <div class="modal-body">
                        <p>Are you sure you want to delete this record ?</p>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary" OnClick="lnkBtnDelete_Click"
                            Text="Yes"></asp:Button>
                        <button id="btnNoDeleteYesNo2" onclick="return hideDeleteModal();" type="button" class="btn btn-danger">
                            No</button>
                    </div>
                </div>
            </div>
        </div>

        <div id="modalLimitDeleteYesNo" class="modal fade">
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
                        <asp:Button ID="Button2" runat="server" CssClass="btn btn-primary" OnClick="lnkLimitBtnDelete_Click"
                            Text="Yes"></asp:Button>
                        <button id="btnNoDeleteYesNo1" onclick="return hideLimitDeleteModal();" type="button" class="btn btn-danger">
                            No</button>
                    </div>
                </div>
            </div>
        </div>

        <asp:ScriptManager runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">

            <ContentTemplate>
                <asp:HiddenField ID="hndCategoryId" runat="server" />
                <asp:HiddenField ID="hndAction" runat="server" Value="Save" />
                <asp:HiddenField ID="hndLimitId" runat="server" />
                <section class="content" style="padding-top: 0px">



        <div class="alert  alert-info  alert-dismissable" id="msg" runat="server" visible="false">
           <a href="#" class="close" data-dismiss="alert" aria-label="close">×</a>
           <strong>
               <asp:Label ID="lbMessage" ForeColor="White" runat="server"></asp:Label>
           </strong>
         </div>


          <div class="box box-info" style="display:none">
        <div class="box-header with-border">
          <h3 class="box-title" >Search Category</h3>

          <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
            <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
          </div>
        </div>
        <div class="box-body">
          <div class="row">

            <div class="col-md-6">
                 <div class="">
                <label for="exampleInputEmail1">Category Name</label>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtFindCategortName" ValidationGroup="btnSearch" ForeColor="Red" Font-Bold="true">* Required</asp:RequiredFieldValidator>
                    <div class="input-group">
                      <asp:TextBox ID="txtFindCategortName" runat="server" style="display:inline-block;" CssClass="form-control" autocomplete="off"></asp:TextBox>
                        <span class="input-group-btn">
                            <asp:Button ID="btnSearch" runat="server" Text="Search" style="display:inline-block;" CssClass="btn btn-primary" ValidationGroup="btnSearch" OnClick="btnSearch_Click1"></asp:Button> 
                        </span>
                    </div>
                </div>
                </div>
          </div>
         
        </div>
        

        <div class="panel-body">
    <div class="co-md-12">
    <div class="table-responsive">
       <asp:GridView ID="gvCategoryList" runat="server" CssClass="table table-responsive tablegv"
            GridLines="None" AutoGenerateColumns="false" OnPageIndexChanging="gvMainCategory_PageIndexChanging" 
           PageSize="10" AllowPaging="true" EmptyDataText="No Records Found" >
        <Columns>
              
            <asp:BoundField DataField="CategoryId" HeaderText="Category Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:BoundField DataField="CategoryName" HeaderText="Category Name" />
             <asp:TemplateField >
                  <ItemTemplate>
                      <asp:LinkButton ID="btnTake"  Text="Select" OnClick="btnTake_Click" runat="server"   />
                  </ItemTemplate>
                </asp:TemplateField>
        </Columns>
    </asp:GridView>
    </div>
    </div>
    </div>

      </div>

      <div class="box box-info">
        <div class="box-header with-border">
          <h3 class="box-title" >Category details</h3>

          <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
            <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
          </div>
        </div>
        <div class="box-body">
          <div class="row">

            <div class="col-md-6">
                 <div class="form-group">
                <label for="exampleInputEmail1">Category Name</label>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"  ControlToValidate="txtMainCategoryName" ValidationGroup="btnSave" ForeColor="Red" Font-Bold="true">* Required</asp:RequiredFieldValidator>
                 <asp:TextBox ID="txtMainCategoryName" runat="server" CssClass="form-control"  autocomplete="off" CausesValidation="true" placeholder="Enter Category Name"></asp:TextBox>
                </div>
                </div>
                <div class="col-md-6">
                <div class="form-group">
                <label for="exampleInputEmail1">Is Active</label>
                <div class="">
                <div class="checkbox">
                <label>
                <asp:CheckBox ID="chkIsavtive" runat="server" Checked></asp:CheckBox>
                </label>
                </div>
                </div>
                </div>
            </div>
         
          </div>
         
            <div class="box-header with-border">
                   <h3 class="box-title">Add Approval Limit</h3>
                </div>
            <div class="row" style="padding-top: 10px;">
                <div class="col-md-6">
                    <div class="form-group col-md-12" >
                        <div class="col-md-4" >  
                            <label for="exampleInputEmail1" style="padding-left: 13px;">Limit For</label> 
                        </div>
                       <div class="col-md-5" >                
                            <asp:DropDownList  ID="ddlLimitFor" runat="server" CssClass="form-control"  >
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group col-md-12" >
                        <div class="col-md-4" >  
                            <label for="exampleInputEmail1" style="padding-left: 13px;">Approval Type</label> 
                        </div>
                       <div class="col-md-5" >          
                            <asp:DropDownList  ID="ddlApprovalType" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlApprovalType_SelectedIndexChanged"    >
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="form-group col-md-12" id="divSelectCommittee" runat="server"  style="display:none">
                        <div class="col-md-4" >  
                            <label for="exampleInputEmail1" style="padding-left: 13px;">Committee</label> 
                        </div>
                       <div class="col-md-5" >                
                            <asp:DropDownList  ID="ddlCommittee" runat="server" CssClass="form-control"  >
                            </asp:DropDownList>
                        </div>
                    </div>

                     <div class="form-group col-md-12" id="divSelectDesignation" runat="server" style="display:none">
                        <div class="col-md-4" >  
                            <label for="exampleInputEmail1" style="padding-left: 13px;">Designation</label> 
                        </div>
                       <div class="col-md-5" >                
                            <asp:DropDownList  ID="ddlAnyUserDesignation" runat="server" CssClass="form-control"  >
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="form-group col-md-12" style="padding-top: 20px;">
                        <div class="col-md-4" >  
                            <label for="exampleInputEmail1" style="padding-left: 13px;">No Of People Can Approve</label> 
                        </div>
                       <div class="col-md-5" >                
                           <asp:TextBox  Type="number" value="1" ID="txtAllowedApprovalCount" runat="server"  CssClass="form-control"  CausesValidation="true"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group col-md-12" style="padding-top: 20px;">
                        <div class="col-md-4" >  
                            <label for="exampleInputEmail1" style="padding-left: 13px;">Type</label> 
                        </div>
                       <div class="col-md-5" >   
                           <asp:DropDownList ID="ddlLocalImport" runat="server" CssClass="form-control" >
                               <asp:ListItem Text="Local" Value="1"></asp:ListItem>
							   <asp:ListItem Text="Import" Value="2"></asp:ListItem>
                           </asp:DropDownList>
                           </div>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-group col-md-12">
                       <div class="col-md-4">
                        <label for="exampleInputEmail1">Can Overide</label>
                       </div>
                      <div class="col-md-5">
                        <asp:DropDownList  ID="ddlOveride" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlOveride_SelectedIndexChanged" >
                          <asp:ListItem Text="Yes" Value="1"> </asp:ListItem>
                          <asp:ListItem Text="NO" Value="0"> </asp:ListItem>
                        </asp:DropDownList>
                        </div>
                    </div>

                    <div class="form-group col-md-12"  id="divOverideDesignation" runat="server" style="display:none">
                       <div class="col-md-4">
                        <label for="exampleInputEmail1">Overide Desination</label>
                       </div>
                        <div class="col-md-5">
                      <asp:DropDownList  ID="ddlOverideDesignation" runat="server" CssClass="form-control">
                       </asp:DropDownList>
                    </div>
                    </div>

                    <div class="form-group col-md-12" id="divEffectiveDate" runat="server" style="display:none">
                        <div class="col-md-4" >  
                        <label for="exampleInputEmail1">Effective Date</label> 
                    </div>
                       <div class="col-md-5" > 
                           <asp:TextBox ID="effectiveDate" runat="server" AutoPostBack="true" type="date"  CssClass="form-control customDate" data-date="" data-date-format="DD MMMM YYYY" autocomplete="off" ></asp:TextBox> 
                           <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ForeColor="Red" Font-Bold="true" ControlToValidate="effectiveDate" ValidationGroup="btnSave" style="display:none">* Fill This Field</asp:RequiredFieldValidator>
                        </div>
                  </div>

                    </div>

                </div>
            
            <div class="row" style="padding-bottom: 10px;">
                <div class="col-md-12">
                    <div class="form-inline row" style="text-align: center;">
                    <div class="col-md-2" style="padding-left: 39px;">  
                        <label for="exampleInputEmail1">Minimum Value</label> 
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ForeColor="Red" Font-Bold="true" ControlToValidate="txtMinimumValue" ValidationGroup="btnSave">* Fill This Field</asp:RequiredFieldValidator>
                    </div>
                   <div class="col-md-3">                        
                        <asp:TextBox  Type="number" value="0" style="margin-right: 48px;" ID="txtMinimumValue" runat="server" CssClass="form-control"  CausesValidation="true"></asp:TextBox>
                    </div>
                     <div class="col-md-2">  
                         <label for="exampleInputEmail1">Maximum Value</label> 
                          <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ForeColor="Red" Font-Bold="true" ControlToValidate="txtMaximumValue" ValidationGroup="btnSave">* Fill This Field</asp:RequiredFieldValidator>
                     </div>
                   <div class="col-md-3">                       
                        <asp:TextBox ID="txtMaximumValue" runat="server" Type="number" CssClass="form-control" CausesValidation="true"></asp:TextBox>
                    </div>
                </div>
                </div>
               

        </div>
          <!-- -->

        <div class="box-footer">
            <span class="pull-right">
                 <asp:Button ID="btnSave" runat="server" Text="Save"   CssClass="btn btn-primary"  OnClientClick="return SaveItemCategory()"  OnClick="btnSave_Click"></asp:Button>
                 <asp:Button ID="btnClear"  runat="server" Text="Clear" AutoPostBack="true"     CssClass="btn btn-danger" onclick="btnClear_Click" > </asp:Button>
                </span>
              </div>
          <div class="panel-body">

    <div class="col-md-12">
            

         <div class="alert  alert-danger  alert-dismissable" id="divMessage" style="display:none">
             <a class="close" onclick="hideDivMessage()">×</a>
           <strong>
                <label id="message"  style="color:white" >Please expand row and edit. If new catergory or limit then type category name in above text box </label>
           </strong>
         </div>

    <div class="table-responsive">
       
       <asp:GridView ID="gvMainCategory" runat="server"  DataKeyNames="CategoryId" CssClass="table table-responsive tablegv" GridLines="None" AutoGenerateColumns="false" OnRowDataBound="OnRowDataBound1" OnPageIndexChanging="gvMainCategory_PageIndexChanging" PageSize="10" AllowPaging="true">
        <Columns>
              <asp:TemplateField HeaderText="IsActive"  HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                  <ItemTemplate>
                        <asp:Label Text='<%#Eval("CategoryId") %>' ID="lblCategoryId" runat="server"></asp:Label>
                  </ItemTemplate>
                  
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Local" > 
                <ItemTemplate>
                        <img alt="" class="plusMark1" style="cursor: pointer" src="images/plus.png" />
                        <asp:Panel ID="pnlApprovalLimits" runat="server" Style="display: none" >
                            <asp:GridView ID="gvApprovalLimits" DataKeyNames="LIMIT_ID" runat="server" AutoGenerateColumns="false" CssClass="ChildGrid" Caption="Limits for Local Purchases" >
                                <Columns>
                                    <asp:BoundField ItemStyle-Width="150px" DataField="LIMIT_ID" HeaderStyle-CssClass="hidden"  HeaderText="Limit No"  ItemStyle-CssClass="hidden LimitId" />
                                    <asp:BoundField ItemStyle-Width="150px" HeaderStyle-CssClass="hidden" DataField="CATEGORY_ID" HeaderText="Category Id"  ItemStyle-CssClass="hidden CategoryId" />
                                    <%--<asp:TemplateField HeaderText="Category Name" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden CategoryName" >
                                    <ItemTemplate>
                                        <asp:Label Text='<%#getAllMaincategoryList.Find(x=>x.CategoryId == Convert.ToInt32(Eval("CATEGORY_ID").ToString())).CategoryName %>' Font-Bold="true"  runat="server"></asp:Label>
                                    </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:BoundField ItemStyle-Width="150px" DataField="MINIMUM_AMOUNT" HeaderText="Minimum Amount" DataFormatString="{0:N2}" ItemStyle-CssClass="MinimumAmount"/>
                                    <asp:BoundField ItemStyle-Width="150px" DataField="MAXIMUM_AMOUNT" HeaderText="Maximum Amount" DataFormatString="{0:N2}" ItemStyle-CssClass="MaximumAmount" />
                                     <asp:BoundField ItemStyle-Width="150px" DataField="LIMIT_FOR" HeaderText="Limit For"  HeaderStyle-CssClass="hidden"  ItemStyle-CssClass="hidden LimitFor" />  
                                    <asp:TemplateField HeaderText="Limit For"  >
                                    <ItemTemplate>
                                        <asp:Label Text='<%#Eval("LIMIT_FOR").ToString() != null ? ListRefLimitFor.Find(x=>x.Id == Convert.ToInt32(Eval("LIMIT_FOR").ToString())).Description : "" %>' Font-Bold="true" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    </asp:TemplateField>                                 
                                    <asp:BoundField ItemStyle-Width="150px" DataField="APPROVAL_TYPE" HeaderText="Approval Type" HeaderStyle-CssClass="hidden"  ItemStyle-CssClass="hidden ApprovalType" />                                   
                                   <%-- <asp:TemplateField HeaderText="Approval Type"  >
                                    <ItemTemplate>
                                        <asp:Label Text='<%#Eval("APPROVAL_TYPE").ToString() != null && Eval("APPROVAL_TYPE").ToString() != "" ? ListRefApprovalTye.Find(x=>x.Id == Convert.ToInt32(Eval("APPROVAL_TYPE").ToString())).Description : ""%>' Font-Bold="true" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    </asp:TemplateField> --%>
                                    <asp:BoundField ItemStyle-Width="150px" DataField="committeeName" HeaderText="Approval Type"   ItemStyle-CssClass="CommitteeName" />                                   
                                     <asp:BoundField ItemStyle-Width="150px" DataField="COMMITTEE_ID" HeaderText="Committee Id" HeaderStyle-CssClass="hidden"  ItemStyle-CssClass="hidden CommitteeId" />                                    
                                     <asp:BoundField ItemStyle-Width="150px" DataField="DESIGNATION_ID" HeaderText="Desingation" HeaderStyle-CssClass="hidden"  ItemStyle-CssClass="hidden DesignationId" />                                    
                                     <asp:TemplateField HeaderText="Desingation" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"  >
                                    <ItemTemplate>
                                        <asp:Label Text='<%#Eval("DESIGNATION_ID").ToString() != null && Eval("OVERIDE_DESIGNATION").ToString() != "0" && Eval("DESIGNATION_ID").ToString() != "" ? listDesignation.Find(x=>x.DesignationId == Convert.ToInt32(Eval("DESIGNATION_ID").ToString())).DesignationName : ""%>' Font-Bold="true" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    </asp:TemplateField>  
                                    <asp:BoundField ItemStyle-Width="150px" DataField="APPROVAL_COUNT" HeaderText="No Of People"    ItemStyle-CssClass="ApprovalCount" />                                   
                                     <asp:BoundField ItemStyle-Width="150px" DataField="CAN_OVERIDE" HeaderStyle-CssClass="hidden"   HeaderText="Can Overide"    ItemStyle-CssClass="hidden CanOveride" />                                   
                                    <asp:TemplateField HeaderText="Can Overide"  >
                                    <ItemTemplate>
                                        <asp:Label Text='<%#Eval("CAN_OVERIDE").ToString() == "1" ? "Yes" : "No" %>' Font-Bold="true" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    </asp:TemplateField>  
                                    <asp:BoundField ItemStyle-Width="150px" DataField="OVERIDE_DESIGNATION" HeaderText="Overide Designation"  HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden OverideDesignation" /> 
                                    <asp:TemplateField HeaderText="Overide Designation"  >
                                    <ItemTemplate>
                                        <asp:Label Text='<%#Eval("OVERIDE_DESIGNATION").ToString() != null && Eval("OVERIDE_DESIGNATION").ToString() != "0" && Eval("OVERIDE_DESIGNATION").ToString() != "" ? listDesignation.Find(x=>x.DesignationId == Convert.ToInt32(Eval("OVERIDE_DESIGNATION").ToString())).DesignationName : ""%>' Font-Bold="true" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    </asp:TemplateField> 
                                    <asp:BoundField ItemStyle-Width="150px" DataField="effectiveDate" HeaderText="Effective Date"  DataFormatString='<%$ appSettings:datePattern %>' ItemStyle-CssClass="effectiveDate" />                                    
                                   <asp:BoundField ItemStyle-Width="150px" DataField="LIMIT_TYPE" HeaderText="Limit Type" HeaderStyle-CssClass="hidden"  ItemStyle-CssClass="hidden LimitType" />                                   
                                   
                                    <asp:TemplateField HeaderText="Edit" ItemStyle-CssClass="rowedit">
                                      <ItemTemplate>
                                          <asp:LinkButton ID="btnEdit" runat="server" Text="Edit" OnClientClick="EditCategoryLimit(this)" OnClick="btnEdit_Click"></asp:LinkButton>
                                           <%--<a type="button" id="btnEdit"  onclick="EditCategoryLimit(this)" style="cursor:pointer" >Edit</a>--%>
                                      </ItemTemplate>
                                    </asp:TemplateField> 
                                    <asp:TemplateField HeaderText="Delete" ItemStyle-CssClass="rowdelete">
                                      <ItemTemplate>
                                           <a type="button" id="btnDelete"  onclick="DeleteCategoryLimit(this)" style="cursor:pointer" >Delete</a>
                                      </ItemTemplate>
                                    </asp:TemplateField>                             
                                </Columns>
                            </asp:GridView>
                        </asp:Panel>
                   </ItemTemplate>
             </asp:TemplateField>
            <asp:BoundField DataField="CategoryId" HeaderText="CategoryId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden CategoryId"/>
            <asp:TemplateField HeaderText="Imports" > 
                <ItemTemplate>
                    <img alt="" class="plusMark1" style="cursor: pointer" src="images/plus.png" />
                    <asp:Panel ID="pnlApprovalLimitsImport" runat="server" Style="display: none" >
                    <asp:GridView ID="gvApprovalLimitsImport" DataKeyNames="LIMIT_ID" runat="server" AutoGenerateColumns="false" CssClass="ChildGrid" Caption="Limits for Import Purchases" >
                         <Columns>
                        <asp:BoundField ItemStyle-Width="150px" DataField="LIMIT_ID" HeaderStyle-CssClass="hidden"  HeaderText="Limit No"  ItemStyle-CssClass="hidden LimitId" />
                                    <asp:BoundField ItemStyle-Width="150px" HeaderStyle-CssClass="hidden" DataField="CATEGORY_ID" HeaderText="Category Id"  ItemStyle-CssClass="hidden CategoryId" />
                                    <%--<asp:TemplateField HeaderText="Category Name" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden CategoryName" >
                                    <ItemTemplate>
                                        <asp:Label Text='<%#getAllMaincategoryList.Find(x=>x.CategoryId == Convert.ToInt32(Eval("CATEGORY_ID").ToString())).CategoryName %>' Font-Bold="true"  runat="server"></asp:Label>
                                    </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:BoundField ItemStyle-Width="150px" DataField="MINIMUM_AMOUNT" HeaderText="Minimum Amount" DataFormatString="{0:N2}" ItemStyle-CssClass="MinimumAmount"/>
                                    <asp:BoundField ItemStyle-Width="150px" DataField="MAXIMUM_AMOUNT" HeaderText="Maximum Amount" DataFormatString="{0:N2}" ItemStyle-CssClass="MaximumAmount" />
                                     <asp:BoundField ItemStyle-Width="150px" DataField="LIMIT_FOR" HeaderText="Limit For"  HeaderStyle-CssClass="hidden"  ItemStyle-CssClass="hidden LimitFor" />  
                                    <asp:TemplateField HeaderText="Limit For"  >
                                    <ItemTemplate>
                                        <asp:Label Text='<%#Eval("LIMIT_FOR").ToString() != null ? ListRefLimitFor.Find(x=>x.Id == Convert.ToInt32(Eval("LIMIT_FOR").ToString())).Description : "" %>' Font-Bold="true" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    </asp:TemplateField>                                 
                                    <asp:BoundField ItemStyle-Width="150px" DataField="APPROVAL_TYPE" HeaderText="Approval Type" HeaderStyle-CssClass="hidden"  ItemStyle-CssClass="hidden ApprovalType" />                                   
                                   <%-- <asp:TemplateField HeaderText="Approval Type"  >
                                    <ItemTemplate>
                                        <asp:Label Text='<%#Eval("APPROVAL_TYPE").ToString() != null && Eval("APPROVAL_TYPE").ToString() != "" ? ListRefApprovalTye.Find(x=>x.Id == Convert.ToInt32(Eval("APPROVAL_TYPE").ToString())).Description : ""%>' Font-Bold="true" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    </asp:TemplateField> --%>
                                    <asp:BoundField ItemStyle-Width="150px" DataField="committeeName" HeaderText="Approval Type"   ItemStyle-CssClass="CommitteeName" />                                   
                                     <asp:BoundField ItemStyle-Width="150px" DataField="COMMITTEE_ID" HeaderText="Committee Id" HeaderStyle-CssClass="hidden"  ItemStyle-CssClass="hidden CommitteeId" />                                    
                                     <asp:BoundField ItemStyle-Width="150px" DataField="DESIGNATION_ID" HeaderText="Desingation" HeaderStyle-CssClass="hidden"  ItemStyle-CssClass="hidden DesignationId" />                                    
                                     <asp:TemplateField HeaderText="Desingation" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"  >
                                    <ItemTemplate>
                                        <asp:Label Text='<%#Eval("DESIGNATION_ID").ToString() != null && Eval("OVERIDE_DESIGNATION").ToString() != "0" && Eval("DESIGNATION_ID").ToString() != "" ? listDesignation.Find(x=>x.DesignationId == Convert.ToInt32(Eval("DESIGNATION_ID").ToString())).DesignationName : ""%>' Font-Bold="true" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    </asp:TemplateField>  
                                    <asp:BoundField ItemStyle-Width="150px" DataField="APPROVAL_COUNT" HeaderText="No Of People"    ItemStyle-CssClass="ApprovalCount" />                                   
                                     <asp:BoundField ItemStyle-Width="150px" DataField="CAN_OVERIDE" HeaderStyle-CssClass="hidden"   HeaderText="Can Overide"    ItemStyle-CssClass="hidden CanOveride" />                                   
                                    <asp:TemplateField HeaderText="Can Overide"  >
                                    <ItemTemplate>
                                        <asp:Label Text='<%#Eval("CAN_OVERIDE").ToString() == "1" ? "Yes" : "No" %>' Font-Bold="true" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    </asp:TemplateField>  
                                    <asp:BoundField ItemStyle-Width="150px" DataField="OVERIDE_DESIGNATION" HeaderText="Overide Designation"  HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden OverideDesignation" /> 
                                    <asp:TemplateField HeaderText="Overide Designation"  >
                                    <ItemTemplate>
                                        <asp:Label Text='<%#Eval("OVERIDE_DESIGNATION").ToString() != null && Eval("OVERIDE_DESIGNATION").ToString() != "0" && Eval("OVERIDE_DESIGNATION").ToString() != "" ? listDesignation.Find(x=>x.DesignationId == Convert.ToInt32(Eval("OVERIDE_DESIGNATION").ToString())).DesignationName : ""%>' Font-Bold="true" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    </asp:TemplateField> 
                                    <asp:BoundField ItemStyle-Width="150px" DataField="effectiveDate" HeaderText="Effective Date"  DataFormatString='<%$ appSettings:datePattern %>' ItemStyle-CssClass="effectiveDate" />                                    
                                    <asp:BoundField ItemStyle-Width="150px" DataField="LIMIT_TYPE" HeaderText="Limit Type" HeaderStyle-CssClass="hidden"  ItemStyle-CssClass="hidden LimitType" />  
                                    <asp:TemplateField HeaderText="Edit" ItemStyle-CssClass="rowedit">
                                      <ItemTemplate>
                                          <asp:LinkButton ID="btnEdit" runat="server" Text="Edit" OnClientClick="EditCategoryLimit(this)" OnClick="btnEdit_Click"></asp:LinkButton>
                                           <%--<a type="button" id="btnEdit"  onclick="EditCategoryLimit(this)" style="cursor:pointer" >Edit</a>--%>
                                      </ItemTemplate>
                                    </asp:TemplateField> 
                                    <asp:TemplateField HeaderText="Delete" ItemStyle-CssClass="rowdelete">
                                      <ItemTemplate>
                                           <a type="button" id="btnDelete"  onclick="DeleteCategoryLimit(this)" style="cursor:pointer" >Delete</a>
                                      </ItemTemplate>
                                    </asp:TemplateField>    

                                
                                    </Columns>
                            </asp:GridView>
                         </asp:Panel>
                    </ItemTemplate>
             </asp:TemplateField>
            <asp:BoundField DataField="CategoryName" HeaderText="Category Name" ItemStyle-CssClass="CategoryName" />
            <asp:BoundField DataField="CategorDate" DataFormatString='<%$ appSettings:dateTimePattern %>' HeaderText="CategorDate" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden CategorDate"/>
            <asp:BoundField DataField="CreatedBy" HeaderText="CreatedBy" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden CreatedBy"/>
            <asp:BoundField DataField="UpdatedDate" DataFormatString='<%$ appSettings:dateTimePattern %>' HeaderText="UpdatedDate" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:BoundField DataField="UpdatedBy" HeaderText="UpdatedBy" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:BoundField DataField="IsActive"  HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden IsActive" />
             
         <asp:TemplateField HeaderText="Active" ItemStyle-CssClass="rowstatus">
             <ItemTemplate>
                  <asp:Label Text='<%#Eval("IsActive").ToString()== "1"?"Yes":"No" %>' Font-Bold="true" 
                      ForeColor='<%#Eval("IsActive").ToString()== "1"?System.Drawing.Color.Green:System.Drawing.Color.Red %>' 
                      runat="server"></asp:Label>
             </ItemTemplate>
         </asp:TemplateField>
            <asp:TemplateField  HeaderText="Edit" ItemStyle-CssClass="rowedit" >
                <ItemTemplate>
                        <a   onclick="ShowEditMessageFromMainTable(this)" style="cursor:pointer"> Edit </a>
                </ItemTemplate>
            </asp:TemplateField>
             <%-- <asp:TemplateField HeaderText="Edit" ItemStyle-CssClass="rowedit">
                  <ItemTemplate>
                       <a type="button" id="btnEdit"  onclick="EditCategoryFromMainTable(this)" style="cursor:pointer" >Edit</a>
                  </ItemTemplate>
                </asp:TemplateField>--%>
            <asp:TemplateField HeaderText="Delete" ItemStyle-CssClass="hidden rowdelete" HeaderStyle-CssClass="hidden" >
                  <ItemTemplate>
                       <a id="btnDelete"  onclick="DeleteCategoryFromMainTable(this)" style="cursor:pointer"> Delete </a>
                  </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                  <ItemTemplate>
                      <asp:LinkButton ID="btnCancelRequest" Text='<%#Eval("IsActive").ToString()== "1"?"Deactivate":"Activate" %>'  OnClientClick="deactivateMainCategory(this)"  CssClass="deactivateMainCategory" runat="server" />
                  </ItemTemplate>
                </asp:TemplateField>
        </Columns>
    </asp:GridView>
    </div>
    </div>
        
    </div>
      </div>
   
    </section>
            </ContentTemplate>

        </asp:UpdatePanel>
        <asp:HiddenField ID="hdnMainCatecoryId" runat="server" />
        <asp:HiddenField ID="hdnStatus" runat="server" />
        <asp:HiddenField ID="LimitFor" runat="server" />
        <asp:HiddenField ID="MaximumAmount" runat="server" />
        <asp:HiddenField ID="MinimumAmount" runat="server" />
    </form>

    <script src="AdminResources/js/jquery1.8.min.js"></script>
    <script type="text/javascript">
        $("[src*=plus]").live("click", function () {
            $("#divMessage").css("display", "none");
            $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>")
            $(this).attr("src", "images/minus.png");
        });
        $("[src*=minus]").live("click", function () {
            $("#divMessage").css("display", "none");
            $(this).attr("src", "images/plus.png");
            $(this).closest("tr").next().remove();
        });
        function ShowEditMessageFromMainTable(obj) {
            var categoryName = $(obj).closest("tr").find("td.CategoryName")[0].innerText.trim()
            $("#ContentSection_txtMainCategoryName").val(categoryName);
            $("#divMessage").css("display", "block");
            $('#ContentSection_txtAllowedApprovalCount').val(1);
            $("#ContentSection_txtMinimumValue").val(0);
            $("#ContentSection_txtMaximumValue").val(0);
        }
        function hideDivMessage() {
            $("#divMessage").css("display", "none");
        }
    </script>
    <link href="AdminResources/css/formStyleSheet.css" rel="stylesheet" />
    <script src="AdminResources/js/jquery-1.10.2.min.js"></script>
    <script src="AdminResources/js/autoCompleter.js"></script>

    <link href="AdminResources/css/htmldatecss.css" rel="stylesheet" />
    <script src="AdminResources/js/moment.min.js"></script>


    <script type="text/javascript">
        //debugger;
        var categories = <%= getJsonCategoryList() %>;

        autocomplete(document.getElementById('ContentSection_txtMainCategoryName'), categories);


        $("#btnNoConfirmYesNo").on('click').click(function () {
            var $confirm = $("#modalConfirmYesNo");
            $confirm.modal('hide');
            return this.false;
        });

        function hideModal() {
            var $confirm = $("#modalConfirmYesNo");
            $confirm.modal('hide');
            return this.false;
        }
        function hideDeactivateModal() {
            var $confirm = $("#modalDeactivateYesNo");
            $confirm.modal('hide');
            return this.false;
        }
        function showDeactivateModal() {
            var $confirm = $("#modalDeactivateYesNo");
            $confirm.modal('show');
            event.preventDefault();
            return this.false;

        }

        function showDeleteModal() {
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

        function deactivateMainCategory(obj) {
            var categoryId = $(obj).closest('tr').find('td:first-child').find('span').html().trim(); // keep this alwas on first cell
            var status = $(obj).closest('tr').find('td.rowstatus').find('span').html().trim();
            $("#<%=hdnMainCatecoryId.ClientID%>").val(categoryId);
            $('#<%=hdnStatus.ClientID%>').val(status);
            if ($(obj).text() == "Deactivate") {
                $("#modalDeactivateYesNo .modal-body").html("<p>Are you sure you want to deactivate this record ?</p>");
            } else {
                $("#modalDeactivateYesNo .modal-body").html("<p>Are you sure you want to activate this record ?</p>");
            }
            showDeactivateModal();
        }


        function ShowHidePanel(obj) {
            if ($(obj).attr('class') == "plusMark2") {
                $(obj).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(obj).next().html() + "</td></tr>")
                $(obj).attr("src", "images/minus.png");
                $(obj).removeClass('plusMark2')
                $(obj).addClass('minusMark2')
            } else {
                $(obj).attr("src", "images/plus.png");
                $(obj).closest("tr").next().remove();
                $(obj).removeClass('minusMark2');
                $(obj).addClass('plusMark2');
            }
        }

        function DeleteCategoryFromMainTable(obj) {
            var categoryId = $(obj).closest('tr').find('td.CategoryId').text();
            $("#<%=hdnMainCatecoryId.ClientID%>").val(categoryId);
            showDeleteModal();
        }

        function EditCategoryFromMainTable(obj) {
            var categoryId = $(obj).closest('tr').find('td.CategoryId').html();
            var categoryName = $(obj).closest('tr').find('td.CategoryName').html();
            $("#ContentSection_hndCategoryId").val(categoryId);
            $("#ContentSection_hndAction").val("Update");
            $("#ContentSection_txtMainCategoryName").val(categoryName);
        }

        function EditCategoryLimit(obj) {
            //debugger;
            var limitId = $(obj).closest('tr').find('td.LimitId').html();
            var categoryId = $(obj).closest('tr').find('td.CategoryId').html();
            var categoryName = $(obj).closest('tr').find('td.CategoryName').text().trim();
            var limitFor = $(obj).closest('tr').find('td.LimitFor').html();
            var approveType = $(obj).closest('tr').find('td.ApprovalType').html();
            var designationId = $(obj).closest('tr').find('td.DesignationId').html();
            var allowedApprowedCount = $(obj).closest('tr').find('td.ApprovalCount').html();
            var canOveride = $(obj).closest('tr').find('td.CanOveride').html();
            var OverideDesignation = $(obj).closest('tr').find('td.OverideDesignation').html();
            var effectiveDate = $(obj).closest('tr').find('td.effectiveDate').html();
            var minimumValue = $(obj).closest('tr').find('td.MinimumAmount').html();
            var maximumValue = $(obj).closest('tr').find('td.MaximumAmount').html();
            var committeeId = $(obj).closest('tr').find('td.CommitteeId').html();
            var LimitType = $(obj).closest('tr').find('td.LimitType').html();

            $("#<%=LimitFor.ClientID%>").val(limitFor);
            $("#<%=MaximumAmount.ClientID%>").val(maximumValue);
            $("#<%=MinimumAmount.ClientID%>").val(minimumValue);

            if (approveType == "2") {
                // committee
                // debugger;
                $("#ContentSection_divSelectCommittee").css("display", "block");
                $("#ContentSection_divEffectiveDate").css("display", "block");
                $("#ContentSection_divSelectDesignation").css("display", "none");

                $('#ContentSection_txtAllowedApprovalCount').prop('readonly', false);

                effectiveDatet = new Date(effectiveDate);
                var dateString = new Date(effectiveDatet.getTime() - (effectiveDatet.getTimezoneOffset() * 60000)).toISOString().split("T")[0];
                $("#ContentSection_effectiveDate").val(dateString);
                $("#ContentSection_effectiveDate").attr('data-date', moment(dateString, 'YYYY-MM-DD').format($("#ContentSection_effectiveDate").attr('data-date-format')));

                $("#ContentSection_ddlCommittee").val(committeeId);
                if (canOveride == "0") {
                    $("#ContentSection_divOverideDesignation").css("display", "none");
                }
            } else if (approveType == "3") {
                //Any One User
                $("#ContentSection_divSelectDesignation").css("display", "block");
                $("#ContentSection_divSelectCommittee").css("display", "none");
                $("#ContentSection_divEffectiveDate").css("display", "block");

                $('#ContentSection_txtAllowedApprovalCount').val(1);
                $('#ContentSection_txtAllowedApprovalCount').prop('readonly', true);
                effectiveDatet = new Date(effectiveDate);
                var dateString = new Date(effectiveDatet.getTime() - (effectiveDatet.getTimezoneOffset() * 60000)).toISOString().split("T")[0];
                $("#ContentSection_effectiveDate").val(dateString);
                $("#ContentSection_effectiveDate").attr('data-date', moment(dateString, 'YYYY-MM-DD').format($("#ContentSection_effectiveDate").attr('data-date-format')));

            } else {
                // Initiated User
                $("#ContentSection_divSelectCommittee").css("display", "none");
                $("#ContentSection_divEffectiveDate").css("display", "none");
                $("#ContentSection_divSelectDesignation").css("display", "none");
                $('#ContentSection_txtAllowedApprovalCount').val(1);
                $('#ContentSection_txtAllowedApprovalCount').prop('readonly', true);
            }

            if (canOveride == "1") {
                $("#ContentSection_divOverideDesignation").css("display", "block");
                $("#ContentSection_divOverideDesignation").val(OverideDesignation);
            }

            // var tableRows = $(obj).closest('tbody').find('> tr:not(:has(>td>table))');

            //for (i = 1; i < tableRows.length; i++) {
            //    var limit = $(obj).closest('tr').find('td').eq(4).html();
            //    if (limitFor == limit) {
            //        alert("abc");
            //    }
            //}

            $("#ContentSection_hndLimitId").val(limitId);
            $("#ContentSection_hndCategoryId").val(categoryId);
            $("#ContentSection_hndAction").val("Update");
            $("#ContentSection_txtMainCategoryName").val(categoryName);
            $("#ContentSection_ddlLimitFor").val(limitFor);
            $("#ContentSection_ddlApprovalType").val(approveType);
            $("#ContentSection_txtAllowedApprovalCount").val(allowedApprowedCount);
            $("#ContentSection_txtMinimumValue").val(minimumValue.replace(/,/g, ''));
            $("#ContentSection_txtMaximumValue").val(maximumValue.replace(/,/g, ''));
            $("#ContentSection_ddlOveride").val(canOveride);
            $("#ContentSection_ddlOverideDesignation").val(OverideDesignation);
            $("#ContentSection_btnSave").val("Update");
            $("#ContentSection_btnSave").removeClass("btn-primary")
            $("#ContentSection_btnSave").addClass("btn-success");
            $("#ContentSection_ddlLocalImport").val(LimitType);
        }

        function SaveItemCategory() {
            //debugger;
            if ($("#ContentSection_txtMainCategoryName").val() == "") {
                $("#ContentSection_RequiredFieldValidator1").css("visibility", "visible");
                return false;
            }
            if ($("#ContentSection_txtMaximumValue").val() == "" || $("#ContentSection_txtMinimumValue").val() == "") {
                $("#ContentSection_RequiredFieldValidator4").css("visibility", "visible");
                $("#ContentSection_RequiredFieldValidator5").css("visibility", "visible");
                return false;
            }
            if ($("#ContentSection_ddlApprovalType").val() != "1") {
                if ($("#ContentSection_effectiveDate") != undefined) {
                    if ($("#ContentSection_effectiveDate").val() == "") {
                        $("#ContentSection_RequiredFieldValidator3").css("display", "block");
                        $("#ContentSection_RequiredFieldValidator3").css("visibility", "visible");
                        return false;
                    } else {
                        return true
                    }
                }
            } else {
                return true;
            }

        }

        function DeleteCategoryLimit(obj) {
            var limitId = $(obj).closest('tr').find('td.LimitId').html();
            var categoryId = $(obj).closest('tr').find('td.CategoryId').html();
            $("#ContentSection_hndLimitId").val(limitId);
            showLimitDeleteModal();
        }

        function hideLimitDeleteModal() {
            var $confirm = $("#modalLimitDeleteYesNo");
            $confirm.modal('hide');
            return this.false;
        }

        function showLimitDeleteModal() {
            var $confirm = $("#modalLimitDeleteYesNo");
            $confirm.modal('show');
            event.preventDefault();
            return this.false;
        }

        // This is used  while user change date from datepicker - some times this is handle from backend
        $(".customDate").on("change", function () {
            if (this.value) {
                $(this).attr('data-date', moment(this.value, 'YYYY-MM-DD').format($(this).attr('data-date-format')));
            } else {
                $(this).attr('data-date', '');
            }
        }).trigger("change")

        Sys.Application.add_load(function () {
            //onload set date value - used to set date if page refresh or went to backend and came
            var this1 = $(".customDate");
            if (this1.val() != undefined && this1.val() != "") {
                this1.attr('data-date', moment(this1.val(), 'YYYY-MM-DD').format(this1.attr('data-date-format')));
            }
            if ($("#ContentSection_hndCategoryId").val() != "") {
                // alert($("#ContentSection_hndCategoryId").val());
                $("#ContentSection_btnSave").val("Update");
                $("#ContentSection_btnSave").removeClass("btn-primary")
                $("#ContentSection_btnSave").addClass("btn-success");
            }
        });
    </script>
</asp:Content>
