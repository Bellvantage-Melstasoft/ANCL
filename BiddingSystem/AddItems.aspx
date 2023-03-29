<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="AddItems.aspx.cs" Inherits="BiddingSystem.AddItems" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">
    <style type="text/css">
        #myModal .modal-dialog {
            width: 90%;
        }

        #myModalViewBom .modal-dialog {
            mdlAddBatch width: 50%;
        }

        .tablegv {
            font-family: "Trebuchet MS", Arial, Helvetica, sans-serif;
            border-collapse: collapse;
            width: 100%;
        }

            .tablegv td, .tablegv th {
                border: 1px solid #ddd;
                padding: 8px;
                color: black;
            }

            .tablegv tr:nth-child(even) {
                background-color: #f2f2f2;
                color: black;
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

        .tableCol {
            width: 100%;
            margin-bottom: 20px;
            border-collapse: collapse;
            background: #fff;
        }

        .tableCol, .thCol, .tdCol {
            border: 1px solid #cdcdcd;
            color: Black;
        }

            .tableCol .thCol, .tableCol .tdCol {
                padding: 10px;
                text-align: left;
                color: Black;
            }

        .TestTable {
            font-family: "Trebuchet MS", Arial, Helvetica, sans-serif;
            border-collapse: collapse;
            width: 100%;
        }

            .TestTable td, #TestTable th {
                border: 1px solid #ddd;
                padding: 8px;
            }

            .TestTable tr:nth-child(even) {
                background-color: #f2f2f2;
            }

            .TestTable tr:hover {
                background-color: #ddd;
            }

            .TestTable th {
                padding-top: 12px;
                padding-bottom: 12px;
                text-align: left;
                background-color: #4CAF50;
                color: white;
            }

        .auto-style1 {
            position: relative;
            min-height: 1px;
            float: left;
            width: 50%;
            left: 0px;
            top: 0px;
            height: 56px;
            padding-left: 15px;
            padding-right: 15px;
        }

        .margin_top {
            margin-top: 20px;
        }
    </style>
    <script src="AdminResources/js/jquery-1.10.2.min.js"></script>

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
                        <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-primary" OnClick="lnkBtnDelete_Click" Text="Yes"></asp:Button>
                        <button id="btnNoDeleteYesNo" onclick="return hideDeleteModal();" type="button" class="btn btn-danger">No</button>
                    </div>
                </div>
            </div>
        </div>

        <asp:ScriptManager runat="server" ID="sm"></asp:ScriptManager>
        <asp:UpdatePanel ID="Updatepanel1" runat="server">
            <ContentTemplate>


                <div id="mdlManageStock" class="modal fade" aria-hidden="true">
                    <div class="modal-dialog">
                        <div class="modal-content" style="width: 120%">
                            <div class="modal-header" style="background-color: #3c8dbc;">
                                <button type="button"
                                    class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                                <h4 class="modal-title" style="color: white; font-weight: bold;">Manage Stock
                                    <asp:Label ID="lblstock" runat="server"></asp:Label></h4>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <label id="lblInvalidData" class="text-danger hidden">*Invalid Data Found</label>
                                        <div class="table-responsive">
                                            <asp:GridView ID="gvWarehouseInventory" runat="server" CssClass="table table-responsive tablegv" GridLines="None" AutoGenerateColumns="false" EmptyDataText="No Warehouse Found" AllowPaging="true" PageSize="20">
                                                <Columns>
                                                    <asp:BoundField DataField="WarehouseID" HeaderText="Warehouse ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                    <asp:BoundField DataField="ItemID" HeaderText="Item Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                    <asp:TemplateField HeaderText="Location" ItemStyle-CssClass="Location">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblLocation" CssClass="lblLocationCl" Text='<%# Eval("Location").ToString() %>'></asp:Label>
                                                            <asp:Button runat="server" ID="btnAddBatch" CssClass="btn btn-xs btn-warning" Text="Add Batch" OnClick="btnAddBatch_Click" Visible="false" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Stock" ItemStyle-CssClass="Stock">
                                                        <ItemTemplate>
                                                            <asp:TextBox runat="server" ID="txtStock" CssClass="txtStockCl" type="number" min="0" step=".01" Text='<%# Eval("AvailableQty").ToString() %>'></asp:TextBox>
                                                            <asp:Label runat="server" ID="lblStock" CssClass="lblStockCl" Visible="false"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Stock Value" ItemStyle-CssClass="StockValue">
                                                        <ItemTemplate>
                                                            <asp:TextBox runat="server" ID="txtStockValue" CssClass="txtStockValueCl" type="number" min="0" step=".01" Text='<%# Eval("StockValue").ToString() %>'></asp:TextBox>
                                                            <asp:Label runat="server" ID="lblStockValue" CssClass="lblStockValueCl" Visible="false"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Reorder Level" ItemStyle-CssClass="Reorder">
                                                        <ItemTemplate>
                                                            <asp:TextBox runat="server" ID="txtReorderLevel" CssClass="txtReorderLevelCl" type="number" min="0" step=".01" Text='<%# Eval("ReorderLevel").ToString() %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label>Remarks</label>
                                            <asp:TextBox ID="txtRemarks" Rows="5" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:Button runat="server" ID="btnDone" OnClick="btnDone_Click" OnClientClick="hideStockModal()" class="btn btn-primary pull-right" Text="Done" Style="margin-right: 10px"></asp:Button>
                            </div>
                        </div>
                    </div>
                </div>

                <div id="mdlAddBatch" class="modal fade" role="dialog">
                    <div class="modal-dialog">
                        <div class="modal-content" style="width: 130%">
                            <div class="modal-header" style="background-color: #3c8dbc;">
                                <button type="button" class="close" onclick="closemdlAddBatch()">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                                <h4 class="modal-title" style="color: white; font-weight: bold;">Add Batch</h4>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="col-sm-3">
                                            <label>Add Stock </label>
                                            <asp:TextBox runat="server" type="number" min="0.01" step="any" ID="txtStock"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-3">
                                            <label>Add Stock Value </label>
                                            <asp:TextBox runat="server" type="number" min="0.01" step="any" ID="txtStockValue"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-3">
                                            <label>Expiry Date </label>
                                            <asp:TextBox runat="server" type="date" ID="txtexpdate"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:Button runat="server" ID="btnAddBatch" CssClass="btnAddBatchCl btn-primary margin_top" OnClick="btnAddBatch_Click1" Text="Add"></asp:Button>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="table-responsive">
                                            <asp:GridView ID="gvItemBatch" runat="server" ShowHeader="true" ShowHeaderWhenEmpty="true" CssClass="table table-responsive tablegv" GridLines="None" AutoGenerateColumns="false" EmptyDataText="No Batches Found">
                                                <Columns>
                                                    <asp:BoundField DataField="WarehouseID" HeaderText="Warehouse ID" HeaderStyle-CssClass="WarehouseID hidden" ItemStyle-CssClass="ItemID hidden" />
                                                    <asp:BoundField DataField="ItemID" HeaderText="Item ID" HeaderStyle-CssClass="ItemID hidden" ItemStyle-CssClass="ItemID hidden" />
                                                    <asp:BoundField DataField="BatchchId" HeaderText="BatchchId" HeaderStyle-CssClass="BatchchId hidden" ItemStyle-CssClass="BatchchId hidden" />

                                                    <asp:TemplateField HeaderText="Stock" HeaderStyle-CssClass="Stock" ItemStyle-CssClass="Stock">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblStock" CssClass="lblStockCl" Text='<%# Eval("AvailableStock").ToString() %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Stock Value" HeaderStyle-CssClass="StockValue" ItemStyle-CssClass="StockValue">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblStockValue" CssClass="lblStockValueCl" Text='<%# Eval("StockValue").ToString() %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Expiry Date" HeaderStyle-CssClass="ExpDate" ItemStyle-CssClass="ExpDate">
                                                        <ItemTemplate>
                                                            <%--<asp:Label runat="server" ID="lblExpDate" CssClass="lblExpDate" Text='<%# Eval("ExpiryDate", "{0:dd/MM/yyyy}").ToString() %>'></asp:Label>--%>
                                                            <asp:Label runat="server" ID="lblExpDate" CssClass="lblExpDate" Text='<%# (DateTime)Eval("ExpiryDate") == DateTime.MinValue ? "Not Found" : Eval("ExpiryDate", "{0:dd/MM/yyyy}") %>'></asp:Label>
                                                              
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Delete">
                                                        <ItemTemplate>
                                                            <asp:Button runat="server" ID="btnDeleteBatch" CssClass="btnAddBatchCl btn-danger btn-xs" OnClick="btnDeleteBatch_Click" Text="Delete"></asp:Button>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:Button runat="server" ID="btnAddBatchDone" OnClick="btnAddBatchDone_Click" OnClientClick="hideAddBatchModal()" class="btn btn-primary pull-right" Text="Done" Style="margin-right: 10px"></asp:Button>
                            </div>
                        </div>
                    </div>
                </div>

                <div id="mdlLog" class="modal fade">
                    <div class="modal-dialog">
                        <div class="modal-content" style="width: 150%">
                            <div class="modal-header" style="background-color: #3c8dbc;">
                                <button type="button"
                                    class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                                <h4 class="modal-title" style="color: white; font-weight: bold;">Stock Log</h4>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-md-12">

                                        <div class="table-responsive">
                                            <asp:GridView ID="gvStockLog" runat="server" CssClass="table table-responsive" GridLines="None" AutoGenerateColumns="false" EmptyDataText="No Log Found">
                                                <Columns>

                                                    <asp:BoundField DataField="LogId" HeaderText="Log Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                    <asp:BoundField DataField="ItemId" HeaderText="Item Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                    <asp:BoundField DataField="WarehouseId" HeaderText="Warehouse Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                    <asp:BoundField DataField="FirstName" HeaderText="Overridden By" />
                                                    <asp:BoundField DataField="Location" HeaderText="Warehouse" />
                                                    <asp:TemplateField HeaderText="Type">
                                                        <ItemTemplate>
                                                            <asp:Label
                                                                runat="server"
                                                                Visible='<%# Eval("OverriddingType").ToString() == "1" ? true : false %>'
                                                                Text="Manual Overide" />
                                                            <asp:Label
                                                                runat="server"
                                                                Visible='<%# Eval("OverriddingType").ToString() == "2" ? true : false %>'
                                                                Text="Physical stock Verification" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="ExistedQty" HeaderText="Existed Qty" />
                                                    <asp:BoundField DataField="ExistedStockValue" HeaderText="Existed Stock value" />
                                                    <asp:BoundField DataField="OverridingQty" HeaderText="Overridden Qty" />
                                                    <asp:BoundField DataField="OverridingStockValue" HeaderText="Overriding Stock value" />
                                                    <asp:BoundField DataField="OverriddenOn" HeaderText="Overridden On" />
                                                    <asp:BoundField DataField="Remark" HeaderText="Remark" />


                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>

                            </div>

                        </div>
                    </div>
                </div>
                <br />
                <asp:HiddenField ID="HiddenField2" runat="server" />
                <section class="content">
        <div class="alert  alert-info  alert-dismissable" id="msg" runat="server" visible="false">
            <a href="#" class="close" data-dismiss="alert" aria-label="close">×</a>
            <strong><asp:Label ID="lbMessage" ForeColor="White"  runat="server"></asp:Label></strong>
        </div>
        <div class="box box-info">
            <div class="box-header with-border">
          <h3 class="box-title" >Search From Master Items</h3>

          <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
            <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
          </div>
        </div>
        <div class="box-body">
          <div class="row">

            <div class="col-md-4">

                  <div class="form-group">
                <label for="exampleInputEmail1">Select Category</label> 
                     <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlItemMasterCategory" InitialValue="" ValidationGroup="btnSearchItem" ID="RequiredFieldValidator6" ForeColor="Red" >*</asp:RequiredFieldValidator>
                <asp:DropDownList ID="ddlItemMasterCategory" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlItemMasterCategory_SelectedIndexChanged" >
                </asp:DropDownList>
                </div>
            </div>
            <div class="col-md-4">
                 <div class="form-group">
                <label for="exampleInputEmail1">Select Sub Category</label> 
                     <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlItemMasterSubCategory"  InitialValue="" ValidationGroup="btnSearchItem" ID="RequiredFieldValidator7" ForeColor="Red" >*</asp:RequiredFieldValidator>
                <asp:DropDownList ID="ddlItemMasterSubCategory" runat="server" CssClass="form-control" >
                </asp:DropDownList>
                </div>
            </div>

             <div class="col-md-4">
                 <div class="form-group">
                <label for="exampleInputEmail1">Item Name</label>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtFindItemName" ValidationGroup="btnSearchItem" ForeColor="Red">*</asp:RequiredFieldValidator>
                 
                         <div class="input-group">
                      <asp:TextBox ID="txtFindItemName" runat="server" style="display:inline-block;" CssClass="form-control" autocomplete="off" onKeyDown="searchButton(event)"></asp:TextBox>
                    <span class="input-group-btn">
                      <asp:Button ID="btnSearch" runat="server" Text="Search" style="display:inline-block;" CssClass="btn btn-primary" ValidationGroup="btnSearchItem" OnClick="btnSearch_Click"></asp:Button> 
                    </span>
              </div>
              </div>


                </div>
          </div>
         
        </div>
        
      <div class="panel-body">
    <div class="co-md-12">
    <div class="table-responsive">
       <asp:GridView ID="gvMasterItemList" runat="server" CssClass="table table-responsive tablegv" GridLines="None" AutoGenerateColumns="false" PageSize="10" AllowPaging="true" EmptyDataText="No Records Found">
        <Columns>
              
            <asp:BoundField DataField="CategoryId" HeaderText="CategoryId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:BoundField DataField="SubCategoryId" HeaderText="SubCategoryId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:BoundField DataField="ItemId" HeaderText="ItemId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:BoundField DataField="ItemName" HeaderText="Sub Item Name" />
     
             
<%--                <asp:TemplateField HeaderText="Status">
                  <ItemTemplate>
                      <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("CompanyId") == null ? "Available" : "Taken" %>'  />
                  </ItemTemplate>
                </asp:TemplateField>--%>
             <%--<asp:TemplateField >
                  <ItemTemplate>
                      <asp:LinkButton ID="btnTake" Text="Select" OnClick="btnTake_Click" runat="server"  />
                  </ItemTemplate>
                </asp:TemplateField>--%>
        </Columns>
    </asp:GridView>
    </div>
    </div>
    </div>
               

      </div>
      <!-- SELECT2 EXAMPLE -->
      <div class="box box-info" id="DivEdit">
        <div class="box-header with-border">
          <h3 class="box-title" >Item Details</h3>

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
                <label for="exampleInputEmail1">Main Category</label><label id="validateMainCat" style="color:red">(*)</label>
               <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlMainCategory" InitialValue="" ValidationGroup="btnSave" ID="RequiredFieldValidator2" ForeColor="Red">* Please Select Main Item Catergory</asp:RequiredFieldValidator>
                <asp:DropDownList ID="ddlMainCategory" runat="server" CssClass="form-control" onselectedindexchanged="ddlMainCategory_SelectedIndexChanged" AutoPostBack="true"> </asp:DropDownList>
                </div>

                 <div class="form-group">
                <label for="exampleInputEmail1">Sub Category</label><label id="validateSubCat" style="color:red">(*)</label>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlSubCategory" InitialValue="" ValidationGroup="btnSave" ID="RequiredFieldValidator1" ForeColor="Red">* Please Select Sub Item Catergory</asp:RequiredFieldValidator>                
                <asp:DropDownList ID="ddlSubCategory" runat="server" CssClass="form-control" CausesValidation="true">
                </asp:DropDownList>
                </div>
               
                <div class="form-group">
                <label for="exampleInputEmail1">Item Name</label><label id="validateItemName" style="color:red">(*)</label>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtItemName" ValidationGroup="btnSave" ID="RequiredFieldValidator3" ForeColor="Red">* Please Fill This Field</asp:RequiredFieldValidator>                                
                 <asp:TextBox ID="txtItemName" runat="server" CssClass="form-control" CausesValidation="true" ></asp:TextBox>
                </div>

                <div class="form-group">
                    <label for="exampleInputEmail1">Item Code</label>
                    <asp:TextBox runat="server" ID="txtReferenceNo" CssClass="form-control" CausesValidation="true"></asp:TextBox>
                </div>



                <%--<div class="form-group">
                <label for="exampleInputEmail1">Reorder Level</label>                 
                <asp:TextBox runat="server" ID="txtreorder" type="number" min="0"  Text="0"   CssClass="form-control"></asp:TextBox>
                </div>--%>

                <%--<div class="form-group">
                <label for="exampleInputEmail1">Is Active</label>
                    <div class="">
                    <asp:CheckBox ID="chkIsavtive"  runat="server" Checked></asp:CheckBox>
                        </div>

                </div>--%>


               

            </div>
            
            <div class="col-md-6">
                  <div class="form-group">
                <label for="exampleInputEmail1">HS Code</label>
                    <asp:DropDownList ID="ddlHsCode" runat="server" CssClass="form-control" CausesValidation="true"></asp:DropDownList>
                </div>


                <div class="form-group">
                <label for="exampleInputEmail1">Model</label>                    
                <asp:TextBox runat="server" ID="txtModel" CssClass="form-control" CausesValidation="true" Text=""></asp:TextBox>
                </div>

                <div class="form-group">
                <label for="exampleInputEmail1">Part ID</label>                         
                <asp:TextBox runat="server" ID="txtPartId" CssClass="form-control" CausesValidation="true" Text=""></asp:TextBox>
                </div>

                <div class="form-group">
                  
                 <label for="exampleInputEmail1">Item Type</label>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlItemType" ValidationGroup="btnSave" ID="RequiredFieldValidator4" ForeColor="Red">* Please Fill This Field</asp:RequiredFieldValidator>                     
                 <asp:DropDownList ID="ddlItemType" runat="server" CssClass="form-control" > 
                     <asp:ListItem Value="1">Stock</asp:ListItem>  
                     <asp:ListItem Value="2">Non-Stock</asp:ListItem>  
                 </asp:DropDownList>
                 </div>

                 </div>
        </div>


             
          <asp:Panel ID="pnlMeasurement" runat="server">
           
                <div class="row">
                    <div class="col-md-12">
                        <div class="panel panel-default">
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-md-12 box-header with-border">
                                        <h4>Measurement Details</h4>
                                        <span class="text-danger small">*You can only add measurements from one Standard Measurement Type and Custom Measurement Type. </span>
                                        <hr />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Measurement Types</label>
                                            <asp:DropDownList ID="ddlUOMTypes" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlUOMTypes_SelectedIndexChanged"></asp:DropDownList>
                                           
                                        </div>
                                        <div class="form-group">
                                            <label>Measurements</label>                                                
                                            <asp:ListBox Rows="5" ID="lbUOMs" runat="server" CssClass="form-control" SelectionMode="Single"></asp:ListBox>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <asp:Button ID="btnAddUOM" runat="server" CssClass="btn btn-success btn-sm pull-right" Text="ADD" OnClick="btnAddUOM_Click" ValidationGroup="btnAddUom"/>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Added Measurements</label>
                                            <asp:ListBox Rows="9" ID="lbAddedUOM" runat="server" CssClass="form-control" SelectionMode="Single"></asp:ListBox>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <asp:Button ID="btnRemoveUOM" runat="server" CssClass="btn btn-danger btn-sm pull-right" Text="REMOVE" OnClick="btnRemoveUOM_Click" ValidationGroup="btnRemoveUom" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
           
           
            </asp:Panel> 
            <div class="row">
                <div class="col-md-12">
                    <div class="panel panel-default">
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <h4>Inventory Details</h4>
                                    <hr />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="exampleInputEmail1">Stock Maintaining Measurement</label>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlMeasurement" ValidationGroup="btnSave" ID="RequiredFieldValidator5" ForeColor="Red">*</asp:RequiredFieldValidator>                             
                                        <asp:DropDownList ID="ddlMeasurement" runat="server" CssClass="form-control" ></asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <label for="exampleInputEmail1">Stock Maintaining Type</label>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlStockMaintainingType" ValidationGroup="btnSave" ID="RequiredFieldValidator13" ForeColor="Red">*</asp:RequiredFieldValidator>                             
                                         <asp:DropDownList ID="ddlStockMaintainingType" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlStockMaintainingType_SelectedIndexChanged">
                                             <asp:ListItem Value="1">Average</asp:ListItem>
                                             <asp:ListItem Value="2">First In First Out</asp:ListItem>
                                             <asp:ListItem Value="3">First In Last Out</asp:ListItem>
                                         </asp:DropDownList>
                                       </div>
                                </div>
                                <div class="auto-style1">
                                    <div class="form-group">
                                        <label for="exampleInputEmail1">Inventory</label>
                                        <asp:Button runat="server" CssClass="btn btn-warning" Width="100%" Text="Manage" ID="btnManageStock" OnClick="btnManageStock_Click" />
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>


            <div class="row">
                 <div class="col-md-12">
                    <div class="panel panel-default">
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <h4>Item Description Details</h4>
                                    <hr />
                                </div>
                                </div>
                            <div class="row">
                                <div class="col-md-6">

                                
                                  <div class="form-group">
                      <asp:Label ID="Label1" runat="server" CssClass="control-label" Font-Bold="true">Item Image Upload (Jpg, Jpeg, Png, Gif)</asp:Label>
                      <asp:RegularExpressionValidator ID="regexValidator" runat="server"  ControlToValidate="fileUpload1"  ErrorMessage="Jpg, Jpeg, Png, Gif Only" ForeColor="Red"  ValidationExpression="([a-zA-Z0-9\s_\\.\-\)\(x:])+(.png|.PNG|.jpeg|.JPEG|.jpg|.JPG|.gif|.GIF)$" ValidationGroup="btnSave">  </asp:RegularExpressionValidator>

                      <div class="input-group margin">
                             <asp:FileUpload runat="server" style="display:inline;"    CssClass="form-control" ID="fileUpload1" onchange="readURL(this);"  ></asp:FileUpload>
                     <span class="input-group-btn">
                     <button class="btn btn-info btn-flat clear"  id="clearDocs" >Clear</button>
                    </span>
                     </div>                   
                      <div class="col-sm-12 row">
                             <div class="col-sm-6">
                                  <div class="panel" style=" background-color:transparent;">
                                  <div class="panel-body" >
                                <div>
                                    <asp:Label ID="lblFileUploadError" runat="server"></asp:Label>
                                     <img alt="" src="" runat="server" id="imageid" style="margin-top:10px;width:200px; height:200px; "   /> 
                                 </div>
                                  </div>
                                </div>
                          </div>
                      </div>
                  </div>

                                <asp:Panel ID="pnlSpec" runat="server">
                                    <div class="form-group">
                                        <label for="exampleInputEmail1">Item Specifications</label><label id="itemCount" runat="server" style="color:red; font-weight:bold;  border:solid 1px;  border-color:blue; border-radius:5px;"></label>
                                        <button type="button"  class="btn btn-group-justified" data-toggle="modal" data-target="#myModal">Add Item Description</button>
                                      </div>

                                     </asp:Panel>
                                   </div>
                               
                            </div>
                            </div>
                        </div>
                     </div
         </div>

        </div>


            
        <div class="box-footer">
            <span class="pull-right">
                 <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnSave_Click" ValidationGroup="btnSave" ></asp:Button>
                 <asp:Button ID="btnClear"  runat="server" Text="Clear"  CssClass="btn btn-danger" onclick="btnClear_Click"></asp:Button>
                </span>
              </div>
      </div>

         <div class="panel-body">
            <div class="row">
            <div class="col-md-6">     
            <div class="box box-info">
        <div class="box-header with-border">
          <h3 class="box-title" >Sort Items</h3>
            </div>
                   <div class="box-body">
                <div class="col-md-6">
             <div class="form-group">
                <label for="exampleInputEmail1">Main Category</label>
               <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlSortMainCateory" InitialValue="" ValidationGroup="btnSort" ID="RequiredFieldValidator8" ForeColor="Red">*</asp:RequiredFieldValidator>
               <asp:DropDownList ID="ddlSortMainCateory" runat="server" CssClass="form-control" CausesValidation="true" AutoPostBack="true" OnSelectedIndexChanged="ddlSortMainCateory_SelectedIndexChanged">
                </asp:DropDownList>
                </div>
                     </div>
                <div class="col-md-6">
                 <div class="form-group">
                <label for="exampleInputEmail1">Sub Category</label>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlSortSubCateory" InitialValue="" ValidationGroup="btnSort" ID="RequiredFieldValidator10" ForeColor="Red">*</asp:RequiredFieldValidator>
                                        
                <asp:DropDownList ID="ddlSortSubCateory" runat="server" CssClass="form-control" CausesValidation="true" AutoPostBack="true">
                </asp:DropDownList>
                </div>
                </div>
     
         
        </div>
                 <div class="box-footer">
            <span class="pull-right">
                 <asp:Button ID="btnSort" runat="server" Text="Sort" CssClass="btn btn-primary" OnClick="btnSort_Click" ValidationGroup="btnSort" ></asp:Button>
                 
                </span>
              </div>
         </div>
          </div>
                <div class="col-md-6">
                    
            <div class="box box-info">
        <div class="box-header with-border">
          <h3 class="box-title" >Search Items</h3>

         
        </div>
        <div class="box-body">
          <div class="col-md-6">
             <div class="form-group">
               <asp:RadioButton ID="rbtnItemName" runat="server"  GroupName="grpSearch" style="cursor:pointer"></asp:RadioButton>
                <label for="exampleInputEmail1">Item Name</label>
               <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlSortMainCateory" InitialValue="" ValidationGroup="btnSort" ID="RequiredFieldValidator11" ForeColor="Red">*</asp:RequiredFieldValidator>
               <asp:TextBox ID="txtSearchItemName" runat="server" CssClass="form-control" CausesValidation="true"></asp:TextBox>
                </div>
              </div>
               <div class="col-md-6">
             <div class="form-group">
                 <asp:RadioButton ID="rbtnItemCode" runat="server"  GroupName="grpSearch" style="cursor:pointer"></asp:RadioButton>
                <label for="exampleInputEmail1">Item Code</label> 
               <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlSortMainCateory" InitialValue="" ValidationGroup="btnSort" ID="RequiredFieldValidator12" ForeColor="Red">*</asp:RequiredFieldValidator>
               <asp:TextBox ID="txtSearchItemCode" runat="server" CssClass="form-control" CausesValidation="true"></asp:TextBox>
                </div>

               
         
        </div>
         
          </div>
                 <div class="box-footer">
            <span class="pull-right">
                 <asp:Button ID="btnSearchByNameOrCode" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnSearchByNameOrCode_Click"  ></asp:Button>
                 
                </span>
              </div>
                </div>
                    </div>
                
              <%--<div class="panel-body">
    <div class="co-md-12">
    <div class="table-responsive">
       <asp:GridView ID="GridView1" runat="server" CssClass="table table-responsive tablegv" GridLines="None" AutoGenerateColumns="false" PageSize="10" AllowPaging="true" EmptyDataText="No Records Found">
        <Columns>
              
            <asp:BoundField DataField="CategoryId" HeaderText="CategoryId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:BoundField DataField="SubCategoryId" HeaderText="SubCategoryId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:BoundField DataField="ItemId" HeaderText="ItemId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:BoundField DataField="ItemName" HeaderText="Sub Item Name" />
     
             
<%--                <asp:TemplateField HeaderText="Status">
                  <ItemTemplate>
                      <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("CompanyId") == null ? "Available" : "Taken" %>'  />
                  </ItemTemplate>
                </asp:TemplateField>--%>
           <%--  <asp:TemplateField >
                  <ItemTemplate>
                      <asp:LinkButton ID="btnTake" Text="Select" OnClick="btnTake_Click" runat="server"  />
                  </ItemTemplate>
                </asp:TemplateField>
        </Columns>
    </asp:GridView>
    </div>
    </div>
    </div>--%>
      </div>

                <%--<div class="panel-body">--%>
                    <div class="co-md-12">
                        <div class="table-responsive">
                            <asp:GridView ID="gvAddItems" runat="server" CssClass="table table-responsive tablegv" EmptyDataText="No Records Found"
                                GridLines="None" AutoGenerateColumns="False" OnPageIndexChanging="gvAddItems_OnPageIndexChanging" PageSize="50" AllowPaging="True">
                                <Columns>
                                    <asp:TemplateField HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" Text='<%#Eval("ItemId").ToString() %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="hidden" />
                                        <ItemStyle CssClass="hidden" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                        <ItemTemplate>
                                            <asp:Label ID="Label2" Text='<%#Eval("CategoryId").ToString() %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="hidden" />
                                        <ItemStyle CssClass="hidden" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                        <ItemTemplate>
                                            <asp:Label ID="Label3" Text='<%#Eval("SubCategoryId").ToString() %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="hidden" />
                                        <ItemStyle CssClass="hidden" />
                                    </asp:TemplateField>

                                    <asp:BoundField DataField="ItemId" HeaderText="ItemId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"></asp:BoundField>
                                    <asp:BoundField DataField="CategoryId" HeaderText="CategoryId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"></asp:BoundField>
                                    <asp:BoundField DataField="ItemName" HeaderText="Item Name" HtmlEncode="false" />
                                    <asp:BoundField DataField="SubCategoryName" HeaderText="Sub Category Name" HtmlEncode="false" />
                                    <asp:BoundField DataField="CategoryName" HeaderText="Category Name" HtmlEncode="false" />
                                    <asp:BoundField DataField="SubCategoryId" HeaderText="SubCategoryId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"></asp:BoundField>

                                    <asp:BoundField DataField="ReferenceNo" HeaderText="Item Code" />
                                    <asp:BoundField DataField="IsActive" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                        <HeaderStyle CssClass="hidden" />
                                        <ItemStyle CssClass="hidden" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CreatedDateTime" HeaderText="CreatedDateTime" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                        <HeaderStyle CssClass="hidden" />
                                        <ItemStyle CssClass="hidden" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CreatedBy" HeaderText="CreatedBy" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                        <HeaderStyle CssClass="hidden" />
                                        <ItemStyle CssClass="hidden" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="UpdatedDateTime" HeaderText="UpdatedDateTime" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                        <HeaderStyle CssClass="hidden" />
                                        <ItemStyle CssClass="hidden" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="UpdatedBy" HeaderText="UpdatedBy" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                        <HeaderStyle CssClass="hidden" />
                                        <ItemStyle CssClass="hidden" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ImagePath" HeaderText="ImagePath" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">

                                        <HeaderStyle CssClass="hidden" />
                                        <ItemStyle CssClass="hidden" />
                                    </asp:BoundField>

                                   

                                    <asp:TemplateField HeaderText="Active">
                                        <ItemTemplate>
                                            <asp:Label ID="Label4" CssClass="status" Text='<%#Eval("IsActive").ToString()== "1"?"Yes":"No" %>' Font-Bold="true" ForeColor='<%#Eval("IsActive").ToString()== "1"?System.Drawing.Color.Green:System.Drawing.Color.Red %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    

                                    <asp:TemplateField HeaderText="Edit">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnEdit" ImageUrl="~/images/document.png" OnClick="btnEdit_Click1" Style="width: 26px; height: 20px"
                                                runat="server"   />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Specification" HeaderStyle-CssClass="" ItemStyle-CssClass="" >
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnViewBOM" OnClick="btnViewBOM_Click" CssClass="showSpecification"  runat="server" Text="Edit"/>

                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Delete">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnCancelRequest" CssClass="deleteItem" Text='<%#Eval("IsActive").ToString()== "1"?"Deactivate":"Activate" %>'
                                                runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   


                                    <%--         <asp:TemplateField HeaderText="Specification"> 
               <ItemTemplate>
                  <%--  <asp:LinkButton ID="btnViewBOM" OnClick="btnViewBOM_Click" CssClass="showSpecification"  runat="server" Text="View Item Specifications"/> 
                </ItemTemplate>
            </asp:TemplateField>  --%>

                                    <%-- <asp:BoundField DataField="ReorderLevel" HeaderText="Reorder Level" />--%>
                                    <asp:BoundField DataField="measurementId" HeaderText="measurement" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"></asp:BoundField>
                                      <asp:BoundField DataField="OrderCode" HeaderText="Order Code" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"></asp:BoundField>
                                     <asp:TemplateField HeaderText="Stock Overrides">
                                        <ItemTemplate>
                                            <asp:Button CssClass="btn btn-xs btn-primary btnViewLog" runat="server" OnClick ="btnViewLog_Click"
                                                Text="View Log" Style="margin-right: 4px; margin-bottom: 4px;" ></asp:Button>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Custom Measuremnts">
                                        <ItemTemplate>
                                            <asp:LinkButton  runat="server" OnClick ="btnCustomview_Click"
                                                Text="View" Style="margin-right: 4px; margin-bottom: 4px;" ></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
    </section>


                <div class="modal modal-primary fade" id="myModal">
                    <div class="modal-dialog">
                        <div class="modal-content" style="background-color: White;">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span></button>
                                <h4 class="modal-title" style="text-align: center;">Item Specifications</h4>
                                <asp:Label ID="lblItemName" runat="server" Text=""></asp:Label>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-md-2"></div>
                                    <div class="col-md-8">
                                        <div class="row">
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label for="exampleInputEmail1" style="color: Black;">Material</label>
                                                    <input type="text" id="meterial" placeholder="Material" class="form-control" autocomplete="off" />
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label for="exampleInputEmail1" style="color: Black;">Description</label>
                                                    <input type="text" id="description" placeholder="Description" class="form-control" autocomplete="off" />
                                                </div>
                                            </div>
                                            <div class="col-md-4" style="margin-left: -120px; margin-top: 30px;">
                                                <div class="form-group">
                                                    <label for="exampleInputEmail1" style="visibility: hidden;">Select Main Category</label>
                                                    <a class="add-row" style="color: Red; cursor: pointer;" onclick="addRowItemSpecification()">
                                                        <img src="images/PlusSign.gif" border="0" style="margin-right: 4px; vertical-align: middle;" alt="Add Row" />Add Row</a>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="table-responsive">
                                            <div class="form">
                                            </div>
                                            <table class="tableCol" id="tableCols">
                                                <thead>
                                                    <tr>
                                                        <th class="thCol">Action</th>
                                                        <th class="thCol">Material</th>
                                                        <th class="thCol">Description</th>
                                                    </tr>
                                                </thead>
                                                <tbody class="tbodyCol" id="tbodyCol">
                                                </tbody>

                                            </table>

                                            <a class="delete-row" style="color: Red; cursor: pointer;" onclick="deleteRowItemSpecification()">
                                                <img src="images/dlt.png" border="0" style="margin-right: 4px; vertical-align: middle; width: 20px; margin-top: -4px;" alt="Delete Row" />Delete Row</a>
                                            <br />
                                            <div class="col-md-12">
                                                <div class="col-md-6">
                                                    <!-- hyper lin button for add row in above table which call the javascript function create above-->
                                                    <div style="margin-top: 5px;">
                                                    </div>
                                                </div>
                                            </div>


                                        </div>
                                    </div>
                                    <div class="col-md-2"></div>
                                </div>

                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-danger pull-right" data-dismiss="modal" onclick="RemoveBackdrop();">Close</button>&nbsp;
                                <button type="button" id="hiddenBack" class="btn btn-info pull-right" style="display: none;" data-dismiss="modal" onclick="myModalViewBom();">Submit</button>
                                <%--  <button type="button" class="btn btn-outline">Submit</button>--%>
                            </div>
                        </div>
                        <!-- /.modal-content -->
                    </div>
                    <!-- /.modal-dialog -->
                </div>




                <div class="modal modal-primary fade" id="mdlCustom">
                    <div class="modal-dialog">
                        <div class="modal-content" style="background-color: White;">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span></button>
                                <h4 class="modal-title" style="text-align: center;">Custom Measurements</h4>
                                <asp:Label ID="Label5" runat="server" Text=""></asp:Label>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="row">
                                        <div class="col-md-12">

                                            <div class="table-responsive">
                                                <asp:GridView runat="server" ID="gvCustomMeasurement" AutoGenerateColumns="false" EmptyDataText="No Custom Measurements for the Item"
                                                    CssClass="table table-responsive tablegv">
                                                    <Columns>
                                                        <asp:BoundField DataField="ItemId" HeaderText="ItemId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                        
                                                        <asp:TemplateField HeaderText="From" HeaderStyle-CssClass="hidden">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblFromQty" runat="server" Text="1"></asp:Label>
                                                                <asp:Label ID="lblfromName" runat="server" Text='<%#Eval("FromName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="To" HeaderStyle-CssClass="hidden">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblToQty" runat="server" Text='<%#Eval("Multiplier", "{0:N2}").ToString() %>'></asp:Label>
                                                                <asp:Label ID="lblToName" runat="server" Text='<%#Eval("ToName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>

                                </div>

                            </div>

                        </div>
                        <!-- /.modal-content -->
                    </div>
                    <!-- /.modal-dialog -->
                </div>


                <div id="mdlBOM" class="modal modal-primary fade" tabindex="-1" role="dialog" aria-hidden="true">
                    <div class="modal-dialog">
                        <!-- Modal content-->
                        <div class="modal-content" style="background-color: #a2bdcc;">
                            <div class="modal-header" style="background-color: #7bd47dfa;">
                                <button type="button" class="close " data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">×</span></button>
                                <h4 class="modal-title">Specification Details</h4>
                            </div>
                            <div class="modal-body">
                                <div class="login-w3ls">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="table-responsive">

                                                <div class="box box-info">
                                                    <div class="box-body">
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="form-group">
                                                                    <asp:Label ID="lblMaterial" runat="server" Text="Material" Style="color: black"></asp:Label>
                                                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtMaterial" InitialValue="" ValidationGroup="btnAdd" ID="RequiredFieldValidator14" ForeColor="Red">* Please Fil this Field</asp:RequiredFieldValidator>
                                                                    <asp:TextBox ID="txtMaterial" runat="server" Style="color: black" CssClass="form-control"></asp:TextBox>
                                                                </div>

                                                                <div class="form-group">
                                                                    <asp:Label ID="lblDescription" runat="server" Text="Description" Style="color: black"></asp:Label>
                                                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDescription" InitialValue="" ValidationGroup="btnAdd" ID="RequiredFieldValidator15" ForeColor="Red">* Please Fil this Field</asp:RequiredFieldValidator>
                                                                    <asp:TextBox ID="txtDescription" runat="server" Style="color: black" CssClass="form-control"></asp:TextBox>
                                                                </div>


                                                            </div>

                                                        </div>

                                                        <div class="col-md-12">
                                                            <div class="form-group">
                                                                <div class="form-group">
                                                                    <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-primary pull-right margin" Text="Add" OnClick="btnAddBom_Click" />
                                                                </div>
                                                            </div>

                                                        </div>

                                                        <div class="row">
                                                            <div class="col-md-12">

                                                                <div class="table-responsive">
                                                                    <asp:GridView runat="server" ID="gvBom" AutoGenerateColumns="false" EmptyDataText="No Records Found"
                                                                        CssClass="table table-responsive tablegv">
                                                                        <Columns>
                                                                            <asp:BoundField DataField="num" HeaderText="Random Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                                            <asp:BoundField DataField="Material" HeaderText="Material" />
                                                                            <asp:BoundField DataField="Description" HeaderText="Description" />

                                                                            <asp:TemplateField HeaderText="Action">
                                                                                <ItemTemplate>
                                                                                    <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-danger" Text="Delete" OnClick="btnDelete_Click" />

                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </div>
                                                            </div>
                                                        </div>

                                                    </div>
                                                    <div class="box-footer">
                                                        <asp:Button ID="btnBomDone" runat="server" CssClass="btn btn-primary pull-right margin right " Text="Done" OnClick="btnDoneBom_Click" OnClientClick="Done()" Visible="false" />
                                                        <%-- <asp:Button id="btnPrevInv" runat="server" CssClass="btn btn-primary pull-right margin right" Text="Previous Invoices" OnClick="btnPrevInv_Click" />
                                                        --%>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>








                <div id="myModalViewBom" class="modal modal-primary fade">
                    <div class="modal-dialog">
                        <!-- Modal content-->
                        <div class="modal-content" style="background-color: #a2bdcc;">
                            <div class="modal-header" style="background-color: #7bd47dfa;">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close" onclick="RemoveBackdrop();"><span aria-hidden="False">×</span></button>
                                <h4 class="modal-title">View Specification</h4>
                            </div>
                            <div class="modal-body">
                                <div class="login-3lsw">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="table-responsive">
                                                <asp:GridView ID="gvTempBoms" runat="server" CssClass="table table-responsive TestTable" ShowHeader="true" ShowHeaderWhenEmpty="true" EmptyDataText="No Specifications Found" Style="border-collapse: collapse; color: black;"
                                                    GridLines="None" AutoGenerateColumns="false">
                                                    <Columns>
                                                        <asp:BoundField DataField="itemId" HeaderText="Item Id" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                                        <asp:BoundField DataField="SeqNo" HeaderText="Seq_ID" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />

                                                        <asp:TemplateField HeaderText="Meterial">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtMaterial" CssClass="form-control" Text='<%#Eval("Material").ToString() %>' runat="server"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Description">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtDescription" CssClass="form-control" Text='<%#Eval("Description").ToString() %>' runat="server"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Edit">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="btnEditSpec" ImageUrl="~/images/document.png" Style="margin-right: 4px; vertical-align: middle; width: 20px; margin-top: 5px;"
                                                                    runat="server" OnClick="btnEditSpec_Click" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%--<asp:BoundField DataField="Meterial" HeaderText="Material" />
                                                <asp:BoundField DataField="Description" HeaderText="Description" />--%>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>

                                            <br />
                                            <br />
                                            <div class="form-group">
                                                <button type="button" class="btn btn-info btn-group-justified" data-toggle="modal" onclick="showMyModel()">Add Item Description</button>
                                            </div>
                                            <div class="form-group">
                                                <asp:Button ID="btnSpecSave" CssClass="btn btn-success pull-right" runat="server" Text="Save" OnClick="btnSpecSave_Click" />
                                            </div>


                                        </div>
                                        <div>
                                            <label id="lbMailMessage1" style="margin: 3px; color: maroon; text-align: center;"></label>
                                        </div>
                                    </div>
                                </div>

                                <asp:Panel ID="msgpanel" runat="server" Visible="false" CssClass="align-middle">

                                    <asp:Label ID="lblSuccess" runat="server" Text="Successfully Updated..!!" Visible="false" CssClass="badge badge-success "></asp:Label>

                                    <asp:Label ID="lblDanger" runat="server" Text="Error..!!" Visible="false" CssClass="badge badge-danger"></asp:Label>

                                </asp:Panel>
                            </div>

                            <div class="modal-footer">
                                <button type="button" class="btn btn-danger pull-right" data-dismiss="modal" onclick="RemoveBackdrop();">Close</button>
                                <%--  <button type="button" class="btn btn-outline">Submit</button>--%>
                            </div>
                        </div>
                    </div>
                </div>

                <asp:HiddenField ID="hdnImgPathEdit" runat="server" />
                <asp:HiddenField ID="hdnField" runat="server" />
                <asp:HiddenField ID="hdnItemId" runat="server" />
                <asp:HiddenField ID="hdnCatecoryIdId" runat="server" />
                <asp:HiddenField ID="hdnSubCategoryId" runat="server" />
                <asp:HiddenField ID="hdnStatus" runat="server" />
                <asp:HiddenField ID="HiddenField1" runat="server" />
                <asp:HiddenField ID="hndIsEdit" runat="server" Value="0" />
                <asp:HiddenField ID="hndAddNewItem" runat="server" Value="0" />
                <asp:HiddenField ID="hdnMultiplier" runat="server" />
                <asp:HiddenField ID="hdnSelectedMeasurement" runat="server" />

                <asp:Button ID="btnConversionBetweenCustom" runat="server" CssClass="hidden" OnClick="btnConversionBetweenCustom_Click" />
                <asp:Button ID="btnAddFirstCustomAfterStandard" runat="server" CssClass="hidden" OnClick="btnAddFirstCustomAfterStandard_Click" />
                <asp:Button ID="btnAddFirstStandardAfterCustom" runat="server" CssClass="hidden" OnClick="btnAddFirstStandardAfterCustom_Click" />
                <asp:Button ID="btnRemoveAllCustomMeasurements" runat="server" CssClass="hidden" OnClick="btnRemoveAllCustomMeasurements_Click" />
                <asp:Button ID="btnDone1" runat="server" CssClass="hidden" OnClick="btnDone_Click" />


            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnSave" />

            </Triggers>
        </asp:UpdatePanel>
    </form>



    <script type="text/javascript">

        function readURL(input) {

            if (input.files && input.files[0]) {
                var filePath = input.value;
                var allowedExtensions = /(\.jpg|\.jpeg|\.png|\.gif)$/i;

                if (!allowedExtensions.exec(filePath)) {

                    $("<%=fileUpload1.ClientID%>").remove();
                    <%--   $("#<%=fileUpload1.ClientID %>").css('border-color', 'red');--%>
                    document.getElementById('<%= imageid.ClientID %>').src = 'LoginResources/images/noItem.png';
                }
                else {
                    var reader = new FileReader();
                    reader.onload = function (e) {
                        document.getElementById('<%= imageid.ClientID %>').src = e.target.result;
                        <%--     $("#<%=fileUpload1.ClientID %>").css('border-color', '#d2d6de');--%>
                    }
                    reader.readAsDataURL(input.files[0]);
                }
            }
        }

        function showMyModel() {
            var $confirm = $("#myModal");
            var $editSpec = $("#myModalViewBom");

            $('#hiddenBack').show();

            $editSpec.modal('hide');
            $confirm.modal('show');
            return this.false;
        }

        function myModalViewBom() {
            var $confirm = $("#myModal");
            var $editSpec = $("#myModalViewBom");

            $('#hiddenBack').hide();

            $confirm.modal('hide');
            $editSpec.modal('show');
            return this.false;
        }


        //script for troggle view item specification        
        function addRowItemSpecification() {
            $("#meterial").css('background-color', '#ffffff');
            $("#description").css('background-color', '#ffffff');
            var meterial = $("#meterial").val();
            var description = $("#description").val();
            if (meterial != "" && description != "") {
                var markup = "<tr><td class='tdCol'><input type='checkbox' name='record'></td><td class='tdCol'>" + meterial + "</td><td class='tdCol'>" + description + "</td></tr>";
                $(".tableCol .tbodyCol").append(markup);
                $("#meterial").val("");
                $("#description").val("");
            }
            else {
                meterial == "" ? $("#meterial").css('background-color', '#f5cece') : $("#meterial").css('background-color', '#ffffff');
                description == "" ? $("#description").css('background-color', '#f5cece') : $("#description").css('background-color', '#ffffff');
            }
            var rowCount = $('#tableCols tr').length - 1;
            $("#ContentSection_itemCount").text(rowCount);
            BindToList();
        }

        // Find and remove selected table rows
        function deleteRowItemSpecification() {
            $(".tableCol .tbodyCol").find('input[name="record"]').each(function () {
                if ($(this).is(":checked")) {
                    $(this).parents("tr").remove();
                }
            });
            var rowCount = $('#tableCols tr').length - 1;
            $("#ContentSection_itemCount").text(rowCount);
        }


        function BindToList() {
            var myTableArray = [];
            $("#tableCols tr").each(function () {
                var arrayOfThisRow = [];
                var tableData = $(this).find('td');
                if (tableData.length > 0) {
                    tableData.each(function () { arrayOfThisRow.push($(this).text()); });
                    myTableArray.push(arrayOfThisRow);
                }
            });

            $("#ContentSection_hdnField").val(myTableArray);
            return true;
        }


        Sys.Application.add_load(function () {

            $(".deleteItem").click(function () {
                var row = $(this).closest('tr').find('td');

                var ItemId = $(row).eq(3).html();
                var categoryID = $(row).eq(4).html();
                var subCategoryId = $(row).eq(8).html();
                var status = $(row).eq(16).find(".status").text();
                $("#<%=hdnItemId.ClientID%>").val(ItemId);
                $("#<%=hdnCatecoryIdId.ClientID%>").val(categoryID);
                $("#<%=hdnSubCategoryId.ClientID%>").val(subCategoryId);
                $("#<%=hdnStatus.ClientID%>").val(status);

                showDeleteModal();
            });

        });


        function hideDeleteModal() {
            var $confirm = $("#modalDeleteYesNo");
            $confirm.modal('hide');
            return this.false;
        }
        function showDeleteModal() {
            var $confirm = $("#modalDeleteYesNo");
            $confirm.modal('show');
            return this.false;
        }
        function scrolltoDiv() {
            if ($("#ContentSection_ddlItemMasterCategory").val() != "" & $("#ContentSection_ddlItemMasterSubCategory").val() != "") {
                document.getElementById('DivEdit').scrollIntoView(true);
            }
        }



        function hideStockModal() {

            var txtStocks = document.getElementsByClassName('txtStockCl');
            var txtStockValues = document.getElementsByClassName('txtStockValueCl');
            var txtReorderLevel = document.getElementsByClassName('txtReorderLevelCl');

            var isVald = true;

            for (var i = 0; i < txtReorderLevel.length; i++) {

                if (!(txtReorderLevel[i].checkValidity())) {
                    isVald = false;
                    break;
                }


            }


            for (var i = 0; i < txtStocks.length; i++) {

                if (!(txtStocks[i].checkValidity())) {
                    isVald = false;
                    break;
                }
            }

            if (isVald) {

                for (var i = 0; i < txtStockValues.length; i++) {
                    if (!(txtStockValues[i].checkValidity())) {
                        isVald = false;
                        break;
                    }
                }
            }

            if (isVald) {
                $('#lblInvalidData').addClass('hidden');
                $('#mdlManageStock').modal('hide');
            }
            else {
                $('#lblInvalidData').removeClass('hidden');
                event.preventDefault();
            }

            //$('#ContentSection_btnDone1').click();
        }

        
        function RemoveBackdrop() {
            $('#hiddenBack').hide();
            $('.modal-backdrop').remove();
        }
        function Done() {
            $('#mdlBOM').modal('hide');

        }
    </script>
</asp:Content>
