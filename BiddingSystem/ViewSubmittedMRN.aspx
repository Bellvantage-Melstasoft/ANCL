<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="ViewSubmittedMRN.aspx.cs" Inherits="BiddingSystem.ViewSubmittedMRN" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">

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

  <style type="text/css">
    .ChildGrid td {
            background-color: #f5f5f5 !important;
            color: black;
            font-size: 10pt;
            line-height: 200%;
            text-align: center;
        }

        .ChildGrid th {
            color: White;
            font-size: 10pt;
            line-height: 200%;
            padding-top: 12px;
            padding-bottom: 12px;
            text-align: center;
            background-color: #3C8DBC !important;
            color: white;
        }

        .ChildGridTwo td {
            background-color: #dcd4d4 !important;
            color: black;
            font-size: 10pt;
            line-height: 200%;
            text-align: center;
        }

        .ChildGridTwo th {
            color: White;
            font-size: 10pt;
            line-height: 200%;
            padding-top: 12px;
            padding-bottom: 12px;
            text-align: center;
            background-color: #56585b !important;
            color: white;
        }

        .bg-teal-gradient > div{
            padding:4px;
        }
        .box-body.pull-left.bg-teal-gradient.form-inline div:last-child >input {
            margin-left: 11px;
        }
        .GridViewEmptyText{
            color:Red;
            font-weight:bold;
            font-size:14px;
        }
  </style>
  <script src="AdminResources/js/jquery-1.10.2.min.js"></script>
    <link href="AdminResources/css/formStyleSheet.css" rel="stylesheet" type="text/css" />
<section class="content-header">
    <h1>
      View Submitted Material Request Notes 
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="AdminDashboard.aspx"><i class="fa fa-home"></i> Home</a></li>
        <li class="active">Material Request Notes </li>
      </ol>
    </section>
    <br />


    <form runat="server">

        
    

    <asp:ScriptManager runat="server" EnablePartialRendering="true"></asp:ScriptManager>
    <asp:UpdatePanel ID="Updatepanel1" runat="server" ChildrenAsTriggers="true">
        <ContentTemplate>

            <div id="mdlIssueStock" class="modal fade">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header" style="background-color: #3c8dbc;">
                            <button type="button"
                                class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                            <h4 class="modal-title" style="color: white; font-weight: bold;">Issue Stock from Warehouse</h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-sm-3 text-center"><b>Requested QTY </b><p id="requestedQtyShow"></p></div>
                                <div class="col-sm-3 text-center"><b>Issued QTY </b><p id="issuedQtyShow"></p></div>
                                <div class="col-sm-3 text-center"><b>Pending QTY </b><p id="pendingQtyShow"></p></div>
                                <div class="col-sm-3 text-center"><b>Available QTY </b><p id="availableQtyShow"></p></div>
                            </div>
                            <div class="row">
                                <div class="co-md-12">
                                    <div class="table-responsive">
                                        <asp:GridView ID="gvWarehouseInventory" runat="server" CssClass="table table-responsive tablegv" GridLines="None" AutoGenerateColumns="false" EmptyDataText="No Records Found" AllowPaging="true" PageSize="20">
                                            <Columns>
                                                <asp:BoundField DataField="WarehouseID" HeaderText="Warehouse ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                <asp:BoundField DataField="ItemID" HeaderText="ItemID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                <asp:BoundField DataField="Location" HeaderText="Location" />
                                                <asp:BoundField DataField="AvailableQty" HeaderText="Avilable Qty"/>
                                                <asp:TemplateField HeaderText="Quantity">
                                                    <ItemTemplate>
                                                        <asp:TextBox runat="server" ID="IssuedQty" type="number" min="0"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                       <asp:Button ID="btnIssue" runat="server" Text="Issue" CssClass="btn btn-primary"  OnClick="btnIssue_Click" Style="margin-right: 10px"  />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                           
                        </div>
                    </div>
                </div>
            </div>

             <div id="mdladdtoinventory" class="modal fade">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header" style="background-color: #3c8dbc;">
                            <button type="button"
                                class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                            <h4 class="modal-title" style="color: white; font-weight: bold;">Add stock for items</h4>
                        </div>
                        <div class="modal-body">
                            
                            <div class="row ">
                                
                               
                                   <div class="co-md-6" style="width:70%;padding-left:20px;">
                                    <label for="exampleInputEmail1">Item Name</label><label id="validateItemName" style="color:red"></label>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtItemName" ValidationGroup="btnSave" ID="RequiredFieldValidator3" ForeColor="Red">*</asp:RequiredFieldValidator>                                
                                     <asp:TextBox ID="txtItemName" runat="server" CssClass="form-control" CausesValidation="true"></asp:TextBox>
                                    </div>

                                    <div class=" co-md-6" style="width:70%;padding-left:20px;">
                                    <label for="exampleInputEmail1">Item Quantity</label><label id="Label2" style="color:red"></label>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtQuantity" ValidationGroup="btnSave" ID="RequiredFieldValidator4" ForeColor="Red">*</asp:RequiredFieldValidator>                                
                                    <asp:TextBox runat="server" ID="txtQuantity" type="number" CssClass="form-control" CausesValidation="true"></asp:TextBox>
                                    </div>
                                    <div class=" co-md-6" style="width:70%;padding-left:20px;">
                                    <label for="exampleInputEmail1">UnitPrice</label><label id="Label2" style="color:red"></label>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtQuantity" ValidationGroup="btnSave" ID="RequiredFieldValidator1" ForeColor="Red">*</asp:RequiredFieldValidator>                                
                                    <asp:TextBox runat="server" ID="txtUnitPrice" CssClass="form-control" type="number"  step="0.01" Enabled="false" CausesValidation="true"></asp:TextBox>
                                    </div>
                               
                               
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnSave" runat="server" Text="Add to stock" CssClass="btn btn-primary pull-right" OnClick="btnSave_Click" Style="margin-right: 10px"  />
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
            


            
            <section class="content" style="padding-top:0px">
      <!-- SELECT2 EXAMPLE -->
      <div class="box box-info" id="panelforheadofWH" visible="false" runat="server">
        <div class="box-header with-border">
          <h3 class="box-title" >Submitted Material Requests</h3>

          <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
            <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
          </div>
        </div>
        <!-- /.box-header -->
        <div class="box-body">
          <div class="row">
            <div class="col-md-12">
            <div class="table-responsive">
              <asp:GridView runat="server" ID="gvviewMRNhead" GridLines="None" CssClass="table table-responsive"  AllowPaging="true" PageSize="10"
                  HeaderStyle-BackColor="#3C8DBC" HeaderStyle-ForeColor="White" OnPageIndexChanging="gvviewMRNhead_PageIndexChanging"  EmptyDataRowStyle-CssClass="GridViewEmptyText"
                  AutoGenerateColumns="false" DataKeyNames="MrnID" OnRowDataBound="gvviewMRNhead_RowDataBound" EmptyDataText="No records Found">
                    <PagerStyle HorizontalAlign = "Center" CssClass = "GridPager" />  
                  <Columns>
                    <asp:TemplateField  HeaderText="MR Item">
                    <ItemTemplate>
                    <img alt = "" style="cursor: pointer;margin-top: -6px;" src="images/plus.png" />
                    <asp:Panel ID="pnlgvviewMRNheadD" runat="server" Style="display: none">
                        <asp:GridView ID="gvviewMRNheadD"  runat="server" AutoGenerateColumns="false" CssClass = "ChildGrid">
                            <Columns>
                                <asp:BoundField DataField="Mrnd_ID" HeaderText="MRND ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                <asp:BoundField ItemStyle-Width="150px" DataField="CategoryName" HeaderText="Category"/>
                                <asp:BoundField ItemStyle-Width="150px" DataField="SubCategoryName"  HeaderText="Sub-Category" />
                                <asp:BoundField DataField="ItemID" HeaderText="Item ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                <asp:BoundField ItemStyle-Width="150px" DataField="ItemName"  HeaderText="Item"  />
                                <asp:BoundField ItemStyle-Width="150px" DataField="RequestedQty"  HeaderText="Requested Qty"  />
                                <asp:BoundField ItemStyle-Width="150px" DataField="IssuesQty"  HeaderText="Issued Qty"  />
                                <asp:BoundField ItemStyle-Width="150px" DataField="ReceivedQty"  HeaderText="Received Qty"  />
                                <asp:BoundField ItemStyle-Width="150px" DataField="AvailableQty"  HeaderText="Available Stock"  />  
                                <asp:BoundField ItemStyle-Width="150px" DataField="Description"  HeaderText="Description"  /> 
                                <asp:BoundField DataField="CategoryID" HeaderText="categoryID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                <asp:BoundField DataField="SubCategoryID" HeaderText="subCategoryID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                <asp:BoundField DataField="UnitPrice" HeaderText="Estimated Unit Price" DataFormatString="{0:N2}" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                <asp:BoundField DataField="MrnId" HeaderText="MrnID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />    
                                <asp:TemplateField HeaderText="Status" HeaderStyle-Font-Bold="true" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="150px">
                                 <ItemTemplate>
                                     <asp:Label runat="server" ID="txtMRNDStatus" Text='<%#Eval("Status").ToString()=="0"? "Pending" :Eval("Status").ToString()=="1"?"Added to PR": Eval("Status").ToString()=="2"?"Partially-Issued": Eval("Status").ToString()=="3"?"Fully-Issued": Eval("Status").ToString()=="4"?"Delivered":"Received" %>'></asp:Label>
                                 </ItemTemplate>
                                </asp:TemplateField>
                                 
                                  
                            </Columns>
                        </asp:GridView>
                    </asp:Panel>
                    </ItemTemplate>
                    </asp:TemplateField>

                        <asp:BoundField DataField="MrnID"  HeaderText="MRN ID"/>
                        <asp:BoundField DataField="SubDepartmentName"  HeaderText="Department" />
                        <asp:BoundField DataField="CreatedDate"  HeaderText="Created On" DataFormatString='<%$ appSettings:datePattern %>' />
                         <asp:BoundField DataField="QuotationFor"  HeaderText="Description" />
                        <asp:BoundField DataField="Fullname"  HeaderText="Created By" />                        
                        <asp:BoundField DataField="ExpectedDate"  HeaderText="Expected Date" DataFormatString='<%$ appSettings:datePattern %>' />
                       
                        <asp:TemplateField HeaderText="Status">
                                 <ItemTemplate>
                                     <asp:Label runat="server" ID="txtMRNStatus" Text='<%#Eval("Status").ToString()=="0"? "Pending":"Completed" %>' ForeColor='<%#Eval("Status").ToString()=="0"? System.Drawing.Color.Orange:System.Drawing.Color.Green%>'></asp:Label>
                                 </ItemTemplate>
                        </asp:TemplateField> 
                        <%--<asp:TemplateField HeaderText="Approved/Rejected">
                                 <ItemTemplate>
                                     <asp:Label runat="server" ID="txtMRNIsApproved" Text='<%#Eval("IsApproved").ToString()=="1"? "Approved":"Rejected"%>' ForeColor='<%#Eval("IsApproved").ToString()=="1"? System.Drawing.Color.Green:System.Drawing.Color.Red%>'></asp:Label>
                                 </ItemTemplate>
                        </asp:TemplateField> --%>
                         <asp:TemplateField HeaderText="MRN Type">
                                 <ItemTemplate>
                                     <asp:Label runat="server" ID="lblmrntype" Text='<%#Eval("MrntypeId").ToString()=="7"? "Stock":"Non-Stock"%>' ForeColor='<%#Eval("MrntypeId").ToString()=="7"? System.Drawing.Color.Maroon:System.Drawing.Color.Navy%>'></asp:Label>
                                 </ItemTemplate>
                        </asp:TemplateField> 
                         <asp:BoundField DataField="StoreKeeperId"  HeaderText="StoreKeeperId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                         <asp:TemplateField HeaderText="Store Keeper">
                                    <ItemTemplate>
                                          <asp:DropDownList ID="ddlStorekeeper" runat="server"></asp:DropDownList>  
                                    </ItemTemplate>
                                </asp:TemplateField> 
                                <asp:TemplateField HeaderText="Tag">
                                    <ItemTemplate>
                                        <asp:Button runat="server" ID="lbtntagstorekeeper" CssClass="btn btn-sm btn-success" style="width:90px;margin:5px;padding-left:3px;padding-right: 3px;" Text="Tag Store keeper" OnClick="lbtntagstorekeeper_Click"></asp:Button>
                                    </ItemTemplate>
                                </asp:TemplateField> 
                        <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <asp:Button runat="server" ID="btnAddToPR" CssClass="btn btn-sm btn-warning"  style="width:55px;margin:5px;padding-left:3px;padding-right:3px;" Text="Add to PR" OnClick="btnAddToPR_Click"></asp:Button>
                                    </ItemTemplate>
                                </asp:TemplateField> 
                    </Columns>
                </asp:GridView>
                </div>
            </div>         
          </div>
         
        </div>
        <!-- /.box-body -->
      </div>
      <div class="box box-info" id="panelApprovRejectMRN" visible="false" runat="server">
        <div class="box-header with-border">
          <h3 class="box-title" >Submitted Material Requests</h3>

          <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
            <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
          </div>
        </div>
        <!-- /.box-header -->
        <div class="box-body">
          <div class="row">
            <div class="col-md-12">
            <div class="table-responsive">
              <asp:GridView runat="server" ID="gvApprovRejectMRN"  GridLines="None" CssClass="table table-responsive" AutoGenerateColumns="false"
                   HeaderStyle-BackColor="#3C8DBC" HeaderStyle-ForeColor="White"
                   DataKeyNames="MrnID" OnRowDataBound="gvApprovRejectMRN_RowDataBound" EmptyDataText="No records Found">
                    <Columns>
                    <asp:TemplateField HeaderText="MR Item">
                    <ItemTemplate>
                    <img alt = "" style="cursor: pointer;margin-top: -6px;" src="images/plus.png" />
                    <asp:Panel ID="pnlApprovRejectMRND" runat="server" Style="display: none">
                        <asp:GridView ID="gvApprovRejectMRND" runat="server" AutoGenerateColumns="false" CssClass = "ChildGrid">
                            <Columns>
                                <asp:BoundField DataField="Mrnd_ID" HeaderText="MRND ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                <asp:BoundField ItemStyle-Width="150px" DataField="CategoryName" HeaderText="Category"/>
                                <asp:BoundField ItemStyle-Width="150px" DataField="SubCategoryName"  HeaderText="Sub-Category" />
                                <asp:BoundField DataField="ItemID" HeaderText="Item ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                <asp:BoundField ItemStyle-Width="150px" DataField="ItemName"  HeaderText="Item"  />
                                <asp:BoundField ItemStyle-Width="150px" DataField="RequestedQty"  HeaderText="Requested Qty"  />
                                <asp:BoundField ItemStyle-Width="150px" DataField="IssuesQty"  HeaderText="Issued Qty"  />
                                <asp:BoundField ItemStyle-Width="150px" DataField="ReceivedQty"  HeaderText="Received Qty"  />
                                <asp:BoundField ItemStyle-Width="150px" DataField="AvailableQty"  HeaderText="Available Stock"  />  
                                <asp:BoundField ItemStyle-Width="150px" DataField="Description"  HeaderText="Description"  /> 
                                <asp:BoundField DataField="CategoryID" HeaderText="categoryID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                <asp:BoundField DataField="SubCategoryID" HeaderText="subCategoryID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                <asp:BoundField DataField="UnitPrice" HeaderText="Estimated Unit Price" DataFormatString="{0:N2}" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                <asp:BoundField DataField="MrnId" HeaderText="MrnID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />    
                                <asp:TemplateField HeaderText="Status" HeaderStyle-Font-Bold="true" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="150px">
                                 <ItemTemplate>
                                     <asp:Label runat="server" ID="txtMRNDStatus" Text='<%#Eval("Status").ToString()=="0"? "Pending" :Eval("Status").ToString()=="1"?"Added to PR": Eval("Status").ToString()=="2"?"Partially-Issued": Eval("Status").ToString()=="3"?"Fully-Issued": Eval("Status").ToString()=="4"?"Delivered":"Received" %>'></asp:Label>
                                 </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <asp:Button runat="server" ID="btnIssueFromStock" CssClass="btn btn-sm btn-info" style="margin:5px" Width="120px" Text="Issue from Stock" OnClick="btnIssueFromStock_Click" Visible='<%#Eval("Status").ToString()=="0" && int.Parse(Eval("AvailableQty").ToString())>0? true :Eval("Status").ToString()=="1"&& int.Parse(Eval("AvailableQty").ToString())>0?true: Eval("Status").ToString()=="2" && int.Parse(Eval("AvailableQty").ToString())>0?true: false %>'></asp:Button>
                                        <asp:Button runat="server" ID="btnaddinventry" CssClass="btn btn-sm btn-info" style="margin:5px" Width="120px" Text="Add to inventory" OnClick="btnaddinventry_Click" Visible='<%#Eval("Status").ToString()=="0" && int.Parse(Eval("AvailableQty").ToString())==0&& Eval("MrntypeId").ToString()!="8"? true :Eval("Status").ToString()=="1"&& int.Parse(Eval("AvailableQty").ToString())==0&& Eval("MrntypeId").ToString()!="8"?true: Eval("Status").ToString()=="2" && int.Parse(Eval("AvailableQty").ToString())==0&& Eval("MrntypeId").ToString()!="8"?true:false  %>'></asp:Button>
                                    </ItemTemplate>
                                </asp:TemplateField> 
                            </Columns>
                        </asp:GridView>
                    </asp:Panel>
                    </ItemTemplate>
                    </asp:TemplateField>

                        <asp:BoundField DataField="MrnID"  HeaderText="MRN ID"/>                        
                        <asp:BoundField DataField="SubDepartmentName"  HeaderText="Department" />
                        <asp:BoundField DataField="CreatedByName"  HeaderText="Created By" />
                        <asp:BoundField DataField="CreatedDate"  HeaderText="Created On" DataFormatString='<%$ appSettings:dateTimePattern %>' />
                        <asp:BoundField DataField="ExpectedDate"  HeaderText="Expected Date" DataFormatString='<%$ appSettings:datePattern %>'/>
                        <asp:BoundField DataField="Description"  HeaderText="Description" />
                        <asp:TemplateField HeaderText="Status">
                                 <ItemTemplate>
                                     <asp:Label runat="server" ID="txtMRNStatus" Text='<%#Eval("Status").ToString()=="0"? "Pending":"Completed" %>' ForeColor='<%#Eval("Status").ToString()=="0"? System.Drawing.Color.Orange:System.Drawing.Color.Green%>'></asp:Label>
                                 </ItemTemplate>
                        </asp:TemplateField> 
                        <asp:TemplateField HeaderText="Approved/Rejected">
                                 <ItemTemplate>
                                     <asp:Label runat="server" ID="txtMRNIsApproved" Text='<%#Eval("IsApproved").ToString()=="1"? "Approved":"Rejected"%>' ForeColor='<%#Eval("IsApproved").ToString()=="1"? System.Drawing.Color.Green:System.Drawing.Color.Red%>'></asp:Label>
                                 </ItemTemplate>
                        </asp:TemplateField> 
                           <asp:TemplateField HeaderText="MRN Type">
                                 <ItemTemplate>
                                     <asp:Label runat="server" ID="lblmrntype" Text='<%#Eval("MrntypeId").ToString()=="7"? "Stock":"Non-Stock"%>' ForeColor='<%#Eval("MrntypeId").ToString()=="7"? System.Drawing.Color.Maroon:System.Drawing.Color.Navy%>'></asp:Label>
                                 </ItemTemplate>
                        </asp:TemplateField> 
                          <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <asp:Button runat="server" ID="btnAddToPR" CssClass="btn btn-sm btn-success" Width="120px" style="margin:5px" Text="Add to PR" OnClick="btnAddToPR_Click"></asp:Button>
                                    </ItemTemplate>
                                </asp:TemplateField> 
                    </Columns>
                </asp:GridView>
                </div>
            </div>         
          </div>
         
        </div>
        <!-- /.box-body -->
      </div>

   <div class="box box-info" id="pnlOtheruser" visible="false" runat="server">
        <div class="box-header with-border">
          <h3 class="box-title" >Submitted Material Requests</h3>

          <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
            <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
          </div>
        </div>
        <!-- /.box-header -->
        <div class="box-body">
          <div class="row">
            <div class="col-md-12">
            <div class="table-responsive">
              <asp:GridView runat="server" ID="gvotheruser"  GridLines="None" CssClass="table table-responsive"
                  HeaderStyle-BackColor="#3C8DBC" HeaderStyle-ForeColor="White"
                   AutoGenerateColumns="false" DataKeyNames="MrnID" OnRowDataBound="gvApprovRejectMRN_RowDataBound" EmptyDataText="No records Found">
                    <Columns>
                    <asp:TemplateField>
                    <ItemTemplate>
                    <img alt = "" style="cursor: pointer;margin-top: -6px;" src="images/plus.png" />
                    <asp:Panel ID="pnlApprovRejectMRND" runat="server" Style="display: none">
                        <asp:GridView ID="gvApprovRejectMRND" runat="server" AutoGenerateColumns="false" CssClass = "ChildGrid">
                            <Columns>
                                <asp:BoundField DataField="Mrnd_ID" HeaderText="MRND ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                <asp:BoundField ItemStyle-Width="150px" DataField="CategoryName" HeaderText="Category"/>
                                <asp:BoundField ItemStyle-Width="150px" DataField="SubCategoryName"  HeaderText="Sub-Category" />
                                <asp:BoundField DataField="ItemID" HeaderText="Item ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                <asp:BoundField ItemStyle-Width="150px" DataField="ItemName"  HeaderText="Item"  />
                                <asp:BoundField ItemStyle-Width="150px" DataField="RequestedQty"  HeaderText="Requested Qty"  />
                                <asp:BoundField ItemStyle-Width="150px" DataField="IssuesQty"  HeaderText="Issued Qty"  />
                                <asp:BoundField ItemStyle-Width="150px" DataField="ReceivedQty"  HeaderText="Received Qty"  />
                                <asp:BoundField ItemStyle-Width="150px" DataField="AvailableQty"  HeaderText="Available Stock"  />  
                                <asp:BoundField ItemStyle-Width="150px" DataField="Description"  HeaderText="Description"  /> 
                                <asp:BoundField DataField="CategoryID" HeaderText="categoryID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                <asp:BoundField DataField="SubCategoryID" HeaderText="subCategoryID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                <asp:BoundField DataField="UnitPrice" HeaderText="Estimated Unit Price" DataFormatString="{0:N2}" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                <asp:BoundField DataField="MrnId" HeaderText="MrnID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />    
                                <asp:TemplateField HeaderText="Status" HeaderStyle-Font-Bold="true" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="150px">
                                 <ItemTemplate>
                                     <asp:Label runat="server" ID="txtMRNDStatus" Text='<%#Eval("Status").ToString()=="0"? "Pending" :Eval("Status").ToString()=="1"?"Added to PR": Eval("Status").ToString()=="2"?"Partially-Issued": Eval("Status").ToString()=="3"?"Fully-Issued": Eval("Status").ToString()=="4"?"Delivered":"Received" %>'></asp:Label>
                                 </ItemTemplate>
                                </asp:TemplateField>
                              
                            </Columns>
                        </asp:GridView>
                    </asp:Panel>
                    </ItemTemplate>
                    </asp:TemplateField>

                        <asp:BoundField DataField="MrnID"  HeaderText="MRN ID" />
                        <asp:BoundField DataField="CreatedByName"  HeaderText="Created By" />
                        <asp:BoundField DataField="CreatedDate"  HeaderText="Created On" />
                        <asp:BoundField DataField="ExpectedDate"  HeaderText="Expected Date" />
                        <asp:BoundField DataField="Description"  HeaderText="Description" />
                        <asp:TemplateField HeaderText="Status">
                                 <ItemTemplate>
                                     <asp:Label runat="server" ID="txtMRNStatus" Text='<%#Eval("Status").ToString()=="0"? "Pending":"Completed" %>' ForeColor='<%#Eval("Status").ToString()=="0"? System.Drawing.Color.Orange:System.Drawing.Color.Green%>'></asp:Label>
                                 </ItemTemplate>
                        </asp:TemplateField> 
                        <asp:TemplateField HeaderText="Approved/Rejected">
                                 <ItemTemplate>
                                     <asp:Label runat="server" ID="txtMRNIsApproved" Text='<%#Eval("IsApproved").ToString()=="1"? "Approved":"Rejected"%>' ForeColor='<%#Eval("IsApproved").ToString()=="1"? System.Drawing.Color.Green:System.Drawing.Color.Red%>'></asp:Label>
                                 </ItemTemplate>
                        </asp:TemplateField> 
                           <asp:TemplateField HeaderText="MRN Type">
                                 <ItemTemplate>
                                     <asp:Label runat="server" ID="lblmrntype" Text='<%#Eval("MrntypeId").ToString()=="7"? "Stock":"Non-Stock"%>' ForeColor='<%#Eval("MrntypeId").ToString()=="7"? System.Drawing.Color.Maroon:System.Drawing.Color.Navy%>'></asp:Label>
                                 </ItemTemplate>
                        </asp:TemplateField> 
                         
                    </Columns>
                </asp:GridView>
                </div>
            </div>         
          </div>
         
        </div>
        <!-- /.box-body -->
      </div>
      <!-- /.box -->
    </section>


            
         

        </ContentTemplate>
        
    </asp:UpdatePanel>
    </form>
   <script src="AdminResources/js/jquery.min.js" type="text/javascript"></script>

    
</asp:Content>

