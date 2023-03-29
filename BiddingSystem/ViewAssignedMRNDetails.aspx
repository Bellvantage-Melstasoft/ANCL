<%@ Page Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="ViewAssignedMRNDetails.aspx.cs" Inherits="BiddingSystem.ViewAssignedMRNDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">
    <style>
        #ContentSection_gvMRNItems tbody tr td {
            vertical-align: middle;
        }

            #ContentSection_gvMRNItems tbody tr td.lastColumn a {
                margin-top: 2px;
            }
    </style>
    <script src="AdminResources/js/jquery-1.10.2.min.js"></script>
    <div class="content-header">
        <h1>View Material Request Note</h1>
        <ol class="breadcrumb">
            <li><a href="AdminDashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li class="active">View Material Request Note </li>
        </ol>
    </div>
    <br />
    <section class="" id="divPrintPo">
    <div class="container-fluid">
        <form runat="server" id="frm1">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="Updatepanel1" runat="server" >
            <ContentTemplate>



            <div id="mdlViewIssueNote" class="modal fade" tabindex="-1" role="dialog" style="z-index: 3000;" aria-hidden="true">
                <div class="modal-dialog" style="width: 80%;">
                 <div class="modal-content">
                    <div class="modal-header" style="background-color: #3C8DBC; color: white">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="opacity: 1; color: white;">
                            <span aria-hidden="true" style="opacity: 1;">×</span></button>
                        <h4 class="modal-title">ISSUE NOTES</h4>
                    </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-xs-12">
                                    <div>
                                        &nbsp
                                    </div>
                                    <div style="color: black;">
                        <asp:GridView ID="gvIssueNote" runat="server" CssClass="table table-responsive" GridLines="None" AutoGenerateColumns="false"
                          EmptyDataText="No Issue Notes Found" RowStyle-BackColor="#f5f2f2" HeaderStyle-BackColor="#525252" HeaderStyle-ForeColor="White">
                             <Columns>                                                                   
                                    <asp:BoundField DataField="MrndInID"  HeaderText="MRNDIN ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                    <asp:BoundField DataField="MrndID"  HeaderText="MRND ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                    <asp:BoundField DataField="IssuedQty"  HeaderText="Issued QTY"/>
                                    <asp:BoundField DataField="measurementShortName"  HeaderText="Unit" NullDisplayText="Not Found"/>
                                    <asp:BoundField DataField="IssuedOn" HeaderText="Issued On"/>
                                    <asp:BoundField DataField="DeliveredUser"  HeaderText="Delivered By" NullDisplayText="Not Found"/>
                                    <asp:BoundField DataField="DeliveredOn" HeaderText="Delivered On" NullDisplayText="Not Found"/>
                                    <asp:BoundField DataField="ReceivedUser"  HeaderText="Received By" NullDisplayText="Not Found"/>
                                    <asp:TemplateField HeaderText="Received On">
                                    <ItemTemplate>
                                        <%# (DateTime)Eval("ReceivedOn") == DateTime.MinValue ? "Not Found" : string.Format("{0:MM-dd-yyyy}", (DateTime)Eval("ReceivedOn")) %>
                                    </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:Label
                                                runat="server"
                                                Visible='<%# Eval("Status").ToString() == "1" ? true : false %>'
                                                Text="Issued" CssClass="label label-info"/>
                                            <asp:Label
                                                runat="server"
                                                Visible='<%# Eval("Status").ToString() == "2" ? true : false %>'
                                                Text="Delivered" CssClass="label label-info"/>
                                            <asp:Label
                                                runat="server"
                                                Visible='<%# Eval("Status").ToString() == "3" ? true : false %>'
                                                Text="Received" CssClass="label label-success"/>
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
            </div>

            <div id="mdlLog" class="modal fade" tabindex="-1" role="dialog" style="z-index: 3000;" aria-hidden="true">
                <div class="modal-dialog" style="width: 60%;">
                    <div class="modal-content">
                        <div class="modal-header" style="background-color: #3C8DBC; color: white">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="opacity: 1; color: white;">
                                <span aria-hidden="true" style="opacity: 1;">×</span></button>
                            <h4 class="modal-title">Actions Log</h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-xs-12">
                                    <div class="table-responsive">
                                        <asp:GridView ID="gvStatusLog" runat="server" CssClass="table table-responsive" GridLines="None" AutoGenerateColumns="false"
                                            EmptyDataText="No Log Found" HeaderStyle-BackColor="#275591"  HeaderStyle-ForeColor="White">
                                            <Columns>
                                                <asp:BoundField
                                                    DataField="UserName"
                                                    HeaderText="Logged By" />
                                                <asp:BoundField
                                                    DataField="LoggedDate"
                                                    HeaderText="Logged Date and Time" />
                                                <asp:TemplateField  HeaderText="Current Status">
                                                    <ItemTemplate>
                                                      <asp:Label
                                                            runat="server"
                                                            Text='<%# Eval("LogName")%>' CssClass="label label-info"/>
                                                       

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
            </div>

            <div id="mdlTerminationDetails" class="modal fade" tabindex="-1" role="dialog" style="z-index: 3000;" aria-hidden="true">
                <div class="modal-dialog" style="width: 40%;">
                    <div class="modal-content">
                        <div class="modal-header" style="background-color: #3C8DBC; color: white">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="opacity: 1; color: white;">
                                <span aria-hidden="true" style="opacity: 1;">×</span></button>
                            <h4 class="modal-title">Termination Details</h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-xs-12 text-center">
                                    <asp:Image ID="imgMrndTerminatedBySignature" Style="width: 100px; height:50px;" runat="server" /><br />
                                    <asp:Label runat="server" ID="mrndTerminatedByName"></asp:Label><br />
                                    <asp:Label runat="server" ID="mrndTerminatedDate"></asp:Label><br />
                                    <b>Terminated By</b>
                                    <hr style="padding-left:10px; padding-right:10px;" />
                                    <strong>REMARKS</strong><br />
                                    <asp:Label runat="server" ID="lblMrndTerminationRemarks" CssClass="text-left" style="padding-left:10px;"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            
            <div class="modal fade" id="mdlFileUpload" role="dialog">
                    <div class="modal-dialog" style="width: 63%;">
                        <div class="modal-content">
                            <div class="modal-body">
                                <div class="panel panel-default">
                                    <div class="panel-body">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                                <h4 class="text-green text-bold">STANDARD IMAGES</h4>
                                            </div>
                                        </div>
                                        <hr />
                                        <div class="row">
                                                <div class="col-md-12">
                                                <div class="table-responsive">
                                           <asp:GridView ID="gvStandardImages" runat="server" CssClass="table table-responsive" GridLines="None" AutoGenerateColumns="false"
                                            EmptyDataText="No Images Found" HeaderStyle-BackColor="#275591" ShowHeader="true" ShowHeaderWhenEmpty="true" HeaderStyle-ForeColor="White">
                                                <Columns>
                                                        <asp:BoundField DataField="MrndId" HeaderStyle-CssClass="hidden"
                                                            ItemStyle-CssClass="hidden" />
                                                        <asp:BoundField DataField="FileId" HeaderStyle-CssClass="hidden"
                                                            ItemStyle-CssClass="hidden" />
                                                        <asp:BoundField DataField="FilePath" HeaderStyle-CssClass="hidden"
                                                            ItemStyle-CssClass="hidden" />
                                                     <asp:BoundField DataField="FileName" HeaderText="Image Name" />
                                                        <asp:TemplateField HeaderText="Image" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Image runat="server" ID="imgUploadImage" ImageUrl='<%# Eval("FilePath") %>'
                                                                    Height="80px" Width="100px" />
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
                        </div>
                    </div>
                </div>

      <div class="modal fade" id="mdlItemSpec" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-body">
                    <div class="panel panel-default">
                        <div class="panel-body">  
                            <div class="row">
                                            <div class="col-md-12">
                                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                                <h4 class="text-green text-bold">ITEM SPECIFICATION</h4>
                                            </div>
                                        </div>
                                        <hr />                         
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                         <asp:GridView ID="gvBOMDate" runat="server" CssClass="table table-responsive"
                                             GridLines="None" HeaderStyle-BackColor="#275591"  HeaderStyle-ForeColor="White"
                                            AutoGenerateColumns="false" ShowHeader="true" ShowHeaderWhenEmpty="true" EmptyDataText="No Specifications Found">
                                            <Columns>
                                                <asp:BoundField DataField="MrndId" HeaderStyle-CssClass="hidden"
                                                    ItemStyle-CssClass="hidden" />
                                                <asp:BoundField DataField="BomId" HeaderStyle-CssClass="hidden"
                                                    ItemStyle-CssClass="hidden" />
                                                <asp:BoundField DataField="Material" HeaderText="Material" />
                                                <asp:BoundField DataField="Description" HeaderText="Description" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>

    <div class="modal fade" id="mdlReplacementImages" role="dialog">
        <div class="modal-dialog" style="width: 63%;">
            <div class="modal-content">
                <div class="modal-body">
                    <div class="panel panel-default">
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    <h4 class="text-green text-bold">REPLACEMENT IMAGES</h4>
                                </div>
                            </div>
                            <hr />
                            <div class="row">
                                    <div class="col-md-12">
                                    <div class="table-responsive">
                                 <asp:GridView ID="gvViewReplacementImages" runat="server" CssClass="table table-responsive"
                                EmptyDataText="No Images Found" GridLines="None" 
                                AutoGenerateColumns="false" ShowHeader="true" ShowHeaderWhenEmpty="true" HeaderStyle-BackColor="#275591"  HeaderStyle-ForeColor="White">
                                <Columns>
                                    <asp:BoundField DataField="MrndId" HeaderStyle-CssClass="hidden"
                                        ItemStyle-CssClass="hidden" />
                                    <asp:BoundField DataField="FileId" HeaderStyle-CssClass="hidden"
                                        ItemStyle-CssClass="hidden" />
                                    <asp:BoundField DataField="filepath" HeaderStyle-CssClass="hidden"
                                        ItemStyle-CssClass="hidden" />
                                    <asp:BoundField DataField="FileName" HeaderText="Image Name" />
                                    <asp:TemplateField HeaderText="Image" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="text-center" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Image runat="server" ID="imgUploadImage" ImageUrl='<%# Eval("FilePath") %>'
                                                Height="80px" Width="100px" />
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
            </div>
        </div>
    </div>

            <div class="modal fade" id="mdlSupportiveDocs" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-body">
                    <div class="panel panel-default">
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    <h4 class="text-green text-bold">SUPPORTIVE DOCUMENTS</h4>
                                </div>
                            </div>
                            <hr />
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                         <asp:GridView ID="gvSupportiveDocuments" runat="server" CssClass="table table-responsive"
                                            GridLines="None"  HeaderStyle-BackColor="#275591"  HeaderStyle-ForeColor="White"
                                            AutoGenerateColumns="false" EmptyDataText="No Documents Found" ShowHeader="true" ShowHeaderWhenEmpty="true">
                                            <Columns>
                                                <asp:BoundField DataField="MrndId" HeaderStyle-CssClass="hidden"
                                                            ItemStyle-CssClass="hidden" />
                                                <asp:BoundField DataField="FileId" HeaderStyle-CssClass="hidden"
                                                    ItemStyle-CssClass="hidden" />
                                                <asp:BoundField DataField="FileName" HeaderText="File Name" />
                                                <asp:BoundField DataField="FilePath" HeaderStyle-CssClass="hidden"
                                                    ItemStyle-CssClass="hidden" />
                                                <asp:TemplateField HeaderText="Preview">
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" href='<%#Eval("FilePath")%>'>View</asp:LinkButton>
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
            </div>
        </div>
    </div>

            <div class="box box-info">
                    <div class="box-header">
                        <h3 class="text-center"><i class="fa fa-file-invoice"></i>&nbsp;&nbsp;&nbsp;Material Request Note</h3>
                        <hr>
                    </div>
                    <div class="box-body">
                        <div class="row">
                            <div class="col-xs-3">
                                <strong>FROM DEPARTMENT: </strong>
                                <br>
                                <asp:Label runat="server" ID="lblDepartmentName"></asp:Label><br>
                                <asp:Label runat="server" ID="lblDepartmentContact"></asp:Label>
                            </div>
                            <div class="col-xs-3">
                                <strong>TO WAREHOUSE: </strong>
                                <br>
                                <asp:Label Visible="false" runat="server" ID="WarehouseId"></asp:Label><br>
                                <asp:Label runat="server" ID="lblWarehouseName"></asp:Label><br>
                                <asp:Label runat="server" ID="lblWarehouseAddress"></asp:Label><br>
                                <asp:Label runat="server" ID="lblWarehouseContact"></asp:Label>
                            </div>
                            <div class="col-xs-3">
                                <strong>MRN CODE: </strong>
                                <asp:Label runat="server" ID="lblMrnCode"></asp:Label><br>
                                 <strong>EXPENSE TYPE: </strong>
                                <asp:Label runat="server" ID="lblExpenseType"></asp:Label><br>
                                <strong>CATEGORY: </strong>
                                <asp:Label runat="server" ID="lblCategory"></asp:Label><br>
                                 <strong>SUB-CATEGORY: </strong>
                                <asp:Label runat="server" ID="lblSubCategory"></asp:Label><br>
                                    <strong>EXPECTED DATE: </strong>
                                <asp:Label runat="server" ID="lblExpectedDate"></asp:Label><br>
                             </div>
                             <div class="col-xs-3">
                                <strong>MRN APPROVAL STATUS: </strong>
                                <asp:Label runat="server" ID="lblPending" CssClass="label label-warning" Visible="false" Text="Pending"></asp:Label>
                                <asp:Label runat="server" ID="lblApproved" CssClass="label label-success" Visible="false" Text="Approved"></asp:Label>
                                <asp:Label runat="server" ID="lblRejected" CssClass="label label-danger" Visible="false" Text="Rejected"></asp:Label><br>
                                <span id="spanMrnExpense" runat="server" visible="false">
                                <strong>EXPENSE APP. STATUS: </strong>
                                <asp:Label runat="server" ID="lblExpensePending" CssClass="label label-warning" Visible="false" Text="Pending"></asp:Label>
                                <asp:Label runat="server" ID="lblExpenseApproved" CssClass="label label-success" Visible="false" Text="Approved"></asp:Label>
                                <asp:Label runat="server" ID="lblExpenseRejected" CssClass="label label-danger" Visible="false" Text="Rejected"></asp:Label><br>
                                 </span>
                                <strong>MRN STATUS: </strong>
                                <asp:Label runat="server" ID="lblInfo" CssClass="label label-info"  ></asp:Label><br>
                                 <strong>PURCHASE TYPE: </strong>
                                 <asp:Label runat="server" ID="lblPurchaseType" CssClass="label label-info"></asp:Label><br>
                                
                               </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <br />
                                <br />
                                <div class="table-responsive">
                                    <asp:GridView runat="server" ID="gvMRNItems" AutoGenerateColumns="false" OnRowDataBound="gvMRNItems_RowDataBound"
                                        CssClass="table table-responsive" EmptyDataText="Not Active Item Found! " ShowHeader="true" ShowHeaderWhenEmpty="true" HeaderStyle-BackColor="LightGray" BorderColor="LightGray" EnableViewState="true">
                                        <Columns>
                                            <asp:TemplateField>                                                
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="CheckBox2" runat="server" onclick="CheckBoxChecked(this);" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CheckBox1" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="MrndID" HeaderText="MrndID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden MrndID" />  
                                             <asp:BoundField DataField="ItemId" HeaderText="ItemId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden ItemId" />  
                                            <asp:BoundField DataField="ItemName" HeaderText="Item Name" ItemStyle-CssClass="ItemName"/>
                                            <asp:BoundField DataField="RequestedQty" HeaderText="Requested QTY"  ItemStyle-CssClass="RequestedQty" />
                                            <asp:BoundField DataField="MeasurementShortName" HeaderText="Unit" NullDisplayText="Not Found" ItemStyle-CssClass="MeasurementShortName"/>
                                            <asp:BoundField DataField="Description" HeaderText="Description" />
                                            <asp:BoundField DataField="ReceivedQty"
                                                HeaderText="Received Quantity" />
                                            <asp:BoundField DataField="IssuedQty"
                                                HeaderText="Issued Quantity"  ItemStyle-CssClass="IssuedQty"/>
                                            <asp:BoundField DataField="WarehouseAvailableQty"
                                                HeaderText="Available Quantity" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden WarehouseAvailableQty" />
                                            <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                               <asp:Label
                                                    runat="server"
                                                    Text='<%#Eval("StatusName")%>' CssClass="label label-info"/>
                                                
                                                                                           
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="More Info" HeaderStyle-CssClass="no-print" ItemStyle-CssClass="no-print">
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" ID="lkRemark" ToolTip="Remarks" data-toggle="tooltip" data-placement="top" OnClick="lkRemark_Click" CssClass="text-aqua "><span class="glyphicon glyphicon-align-justify"></span></asp:LinkButton>
                                               <asp:LinkButton runat="server" ID="lkRepalacementImages" ToolTip="Replacement Images" data-toggle="tooltip" data-placement="top" OnClick="lkRepalacementImages_Click" CssClass="text-orange "><span class="glyphicon glyphicon-picture"></span></asp:LinkButton>
                                              <asp:LinkButton runat="server" ID="lkStandardImages" ToolTip="Standard Images" data-toggle="tooltip" data-placement="top" OnClick="lkStandardImages_Click" CssClass="text-green"><span class="glyphicon glyphicon-picture"></span></asp:LinkButton>
                                                <asp:LinkButton runat="server" ID="lkSupportiveDocument_Click" ToolTip="Supportive Documents" data-toggle="tooltip" data-placement="top" OnClick="lkSupportiveDocument_Click" CssClass="text-red"><span class="glyphicon glyphicon-file"></span></asp:LinkButton>
                                                <asp:LinkButton runat="server" ID="lkItemSpecification" ToolTip="Item Specifications" data-toggle="tooltip" data-placement="top" OnClick="lkItemSpecification_Click" CssClass="text-navy"><span class="glyphicon glyphicon-list"></span></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>      
                                        <asp:TemplateField HeaderText="Action" HeaderStyle-CssClass="no-print" ItemStyle-CssClass="no-print lastColumn">
                                            <ItemTemplate>

                                                <asp:Button runat="server" ID="btnIssueFromStock"
                                                            CssClass="btn btn-xs btn-info" style="margin:5px"
                                                            Width="120px" Text="Issue from Stock"
                                                            OnClientClick='<%# "issueFromInventory(event,"+Eval("ItemId").ToString()+","+Eval("WarehouseId").ToString()+","+Eval("MeasurementId").ToString()+","+Eval("StockMaintainingType").ToString()+","+Eval("RequestedQty").ToString()+","+Eval("IssuedQty").ToString()+","+Eval("MrnID").ToString()+","+Eval("MrndID").ToString()+")" %>'
                                                            Visible="false" >
                                                </asp:Button>                                                   
                                                <%-- <asp:LinkButton ForeColor="White"  Visible="false"  runat="server" ID="lbtnStockIssue" ToolTip="Issue From Stock" CssClass="btn btn-xs btn-warning" Text="Issue From Stock" OnClientClick="issueStock(event,this);"></asp:LinkButton>--%>
                                                <%--<asp:LinkButton ForeColor="White"  Visible="false"  runat="server" ID="lbtnStockIssue" ToolTip="Issue From Stock" CssClass="btn btn-xs btn-warning" Text="Issue From Stock" OnClientClick="issueStock(event,this);"></asp:LinkButton>--%>                       
                                                <asp:LinkButton ForeColor="White"   runat="server" ID="lbtnAddStock" ToolTip="Add Stock" CssClass="btn btn-xs btn-primary"  Text="Add Stock"  OnClientClick='<%#"addStock(event,this, "+Eval("StockMaintainingType").ToString()+" )" %>'  Visible="false" > </asp:LinkButton>
                                                <asp:LinkButton ForeColor="White"  runat="server" ID="lbtnMore" ToolTip="Terminate" CssClass="btn btn-xs btn-danger" Text="Terminate" OnClientClick="terminateItem(event,this);" Visible="false"></asp:LinkButton>
                                            <%--<asp:Label runat="server" ID="lblBtns" Visible="false" Text="Not Allowed"></asp:Label><br />--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                            <asp:BoundField DataField="ItemUnit" HeaderText="Item Unit" NullDisplayText="Not Found" ItemStyle-CssClass="hidden ItemUnit" HeaderStyle-CssClass="hidden"/>
                                            <asp:BoundField DataField="StockMaintainingType" HeaderText="Stock Maintaining Type" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden StockMaintainingType" />  
                                            
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                        <hr />
                        <div class="row">
                            <div class="col-xs-4 col-sm-4 text-center">
                                <asp:Image ID="imgCreatedBySignature" Style="width: 100px; height:50px;" runat="server" /><br />
                                <asp:Label runat="server" ID="lblCreatedByName"></asp:Label><br />
                                <asp:Label runat="server" ID="lblCreatedDate"></asp:Label><br />
                                <b>MRN Created By</b>
                                <hr style="padding-left:10px; padding-right:10px;" />
                            </div>
                            <asp:Panel ID="pnlApprovedByDetails" runat="server" Visible="false">
                                <div class="col-xs-4 col-sm-4 text-center">
                                     <asp:Image ID="ImgApprovedBySignature" Style="width: 100px; height:50px;" runat="server" /><br />
                                    <asp:Label runat="server" ID="lblApprovedByName"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblApprovedDate"></asp:Label><br />
                                    <b id="lblApprovalText" runat="server"></b>
                                    <hr style="padding-left:10px; padding-right:10px;" />
                                    <strong>REMARKS</strong><br />
                                    <asp:Label runat="server" ID="lblRemark" CssClass="text-left" style="padding-left:10px;"></asp:Label>
                                    
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="pnlExpenseApprovalByDetails" runat="server" Visible="false">
                                <div class="col-xs-4 col-sm-4 text-center">
                                     <asp:Image ID="imgExpApprovedBySignature" Style="width: 100px; height:50px;" runat="server" /><br />
                                    <asp:Label runat="server" ID="lblExpenseApprovedByName"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblExpenseApprovedDate"></asp:Label><br />
                                    <b id="lblExpenseApprovalText" runat="server"></b>
                                    <hr style="padding-left:10px; padding-right:10px;" />
                                    <strong>REMARKS</strong><br />
                                    <asp:Label runat="server" ID="lblExpenseRemark" CssClass="text-left" style="padding-left:10px;"></asp:Label>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="pnlTermination" runat="server" Visible="false">
                                <div class="col-xs-4 col-sm-4 text-center">
                                    <asp:Image ID="imgTerminatedBySignature" Style="width: 100px; height:50px;" runat="server" /><br />
                                    <asp:Label runat="server" ID="lblTerMinatedByName"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblTerminatedDate"></asp:Label><br />
                                    <b>Terminated By</b>
                                    <hr style="padding-left:10px; padding-right:10px;" />
                                    <strong>REMARKS</strong><br />
                                    <asp:Label runat="server" ID="lblTerminationRemarks" CssClass="text-left" style="padding-left:10px;"></asp:Label>
                                </div>
                            </asp:Panel>
                    </div>
                    </div>
                    <div class="box-footer no-print">
                        <asp:Button runat="server" ID="btnAddToPR" CssClass="btn btn-success"  Text="Add to PR" OnClientClick="AddToPR(event)"/>
                        <asp:Button runat="server" ID="btnTerminateMRN" CssClass="btn btn-danger" Text="Terminate" OnClientClick="terminateMRN(event)" />
                    </div>
                </div> 
            
            <div id="mdlIssueAvg" class="modal fade">
                    <div class="modal-dialog" style="width: 60%">
                        <div class="modal-content">
                            <div class="modal-header" style="background-color: #3c8dbc;">
                                <button type="button"
                                    class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                                <h4 class="modal-title" style="color: white; font-weight: bold;"></h4>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="panel panel-default text-center">
                                            <b>ITEM DETAIL</b><br />
                                            <p id="mdlAvgItem" class="text-uppercase"></p>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="panel panel-default text-center">
                                            <b>WAREHOUSE DETAIL</b><br />
                                            <p id="mdlAvgWarehouse" class="text-uppercase "></p>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6 col-md-push-3 text-center">
                                        <div class="form-group">
                                            <label>SELECT MEASUREMENT</label><br />
                                            <select id="ddlAvgMeasurement" class="form-control" style="width: 100%;" onchange="measurementChanged(this);"></select>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-3 text-center">
                                        <div class="panel panel-default">
                                            <div class="panel-body">
                                                <b>REQUESTED QTY</b><br />
                                                <p id="mdlAvgRequested">0</p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3 text-center">
                                        <div class="panel panel-default">
                                            <div class="panel-body">
                                                <b>ISSUED QTY</b>
                                                <p id="mdlAvgIssued">0</p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3 text-center">
                                        <div class="panel panel-default">
                                            <div class="panel-body">
                                                <b>PENDING QTY</b>
                                                <p id="mdlAvgPending">0</p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3 text-center">
                                        <div class="panel panel-default">
                                            <div class="panel-body">
                                                <b>AVAILABLE QTY </b>
                                                <p id="mdlAvgAvailable">0</p>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-6 col-md-push-3 text-center">
                                        <div class="form-group">
                                            <label>ISSUING QTY</label>
                                            <input id="mdlAvgIssuingQty" type="number" class="form-control" min="0" step="0.00001" onkeyup="checkAvailableQty()" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <button id="btnMdlAvgSubmit" class="btn btn-primary pull-right" style="margin-right: 10px" onclick="issueInventory(event);">SUBMIT</button>
                            </div>
                        </div>
                    </div>
                </div>

            <div id="mdlIssueBatch" class="modal fade">
                    <div class="modal-dialog" style="width: 60%">
                        <div class="modal-content">
                            <div class="modal-header" style="background-color: #3c8dbc;">
                                <button type="button"
                                    class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                                <h4 class="modal-title" style="color: white; font-weight: bold;">ISSUE INVENTORY</h4>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="panel panel-default text-center">
                                            <b>ITEM DETAIL</b><br />
                                            <p id="mdlBatchItem" class="text-uppercase"></p>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="panel panel-default text-center">
                                            <b>WAREHOUSE DETAIL</b><br />
                                            <p id="mdlBatchWarehouse" class="text-uppercase "></p>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6 col-md-push-3 text-center">
                                        <div class="form-group">
                                            <label>SELECT MEASUREMENT</label><br />
                                            <select id="ddlBatchMeasurement" class="form-control" style="width: 100%;" onchange="measurementChanged(this);"></select>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-3 text-center">
                                        <div class="panel panel-default">
                                            <div class="panel-body">
                                                <b>REQUESTED QTY</b><br />
                                                <p id="mdlBatchRequested">0</p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3 text-center">
                                        <div class="panel panel-default">
                                            <div class="panel-body">
                                                <b>ISSUED QTY</b>
                                                <p id="mdlBatchIssued">0</p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3 text-center">
                                        <div class="panel panel-default">
                                            <div class="panel-body">
                                                <b>PENDING QTY</b>
                                                <p id="mdlBatchPending">0</p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3 text-center">
                                        <div class="panel panel-default">
                                            <div class="panel-body">
                                                <b>AVAILABLE QTY </b>
                                                <p id="mdlBatchAvailable">0</p>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="panel panel-default">
                                    <div class="panel-body">
                                        <div class="row">
                                            <div class="col-md-12 text-center">
                                                <b>AVAILABLE BATCHES</b>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12">
                                                <span id="spnBatchOrder" style="color: #808080"></span>
                                                <br />
                                                <div class="table-responsive">
                                                    <table id="tblBatches" class="table">
                                                        <thead style="background-color: #3C8DBC; color: white; font-weight: bold;">
                                                            <tr>
                                                                <td class="hidden">BATCH ID</td>
                                                                <td>BATCH CODE</td>
                                                                <td>EXPIRY DATE</td>
                                                                <td>AVAILABLE QTY</td>
                                                                <td>ISSUING QTY</td>
                                                            </tr>
                                                        </thead>
                                                        <tbody id="tbBatches">
                                                        </tbody>
                                                        <tfoot style="background-color: #efefef; color: black; font-weight: bold;">
                                                            <tr>
                                                                <td class="text-right" colspan="3">TOTAL ISSUING QTY</td>
                                                                <td>
                                                                    <label id="lblTotalIssue" style="margin-left: 13px;">0</label>
                                                                </td>
                                                            </tr>
                                                        </tfoot>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <button id="btnMdlBatchSubmit" class="btn btn-primary pull-right" style="margin-right: 10px" onclick="issueInventory(event);">SUBMIT</button>
                            </div>
                        </div>
                    </div>
                </div>

            


            <asp:Button ID="btnTerminate" runat="server" OnClick="btnTerminate_Click" CssClass="hidden" />
            <asp:Button ID="btnIssueStockhnd" runat="server" onclick="btnIssueStock_Click"  CssClass="hidden" />
            <asp:Button ID="btnAddToStockhnd" runat="server" onclick="btnAddToStock_Click"  CssClass="hidden" />
            <asp:Button ID="btnTerminateItemhnd" runat="server" onclick="btnTerminateItemhnd_Click"  CssClass="hidden" />
            <asp:Button ID="btnAddToPRhnd" runat="server" onclick="btnAddToPR_Click"  CssClass="hidden" />
            <asp:HiddenField runat="server" ID="hdnRemarks" />
            <asp:HiddenField ID="hdnIssueStock" runat="server" />
            <asp:HiddenField ID="hdnAddStock" runat="server" />
            <asp:HiddenField ID="hdnAddStockPrice" runat="server" />
            <asp:HiddenField ID="hndMrndId" runat="server" />
            <asp:HiddenField ID="hndItemId" runat="server" />
            <asp:HiddenField ID="hndRequestedQty" runat="server" />
            <asp:HiddenField ID="hndPreviousIssuedQty" runat="server" />
            <asp:HiddenField ID="hndTerminationItemRemark" runat="server" />
            <asp:HiddenField ID="hdnStockMaintainType" runat="server" />
            <asp:HiddenField ID="hdnExpdate" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
        </form>
        
    </div>
        
    </section>

    <%-- popup --%>

    <%--<form runat="server">
        <asp:ScriptManager runat="server" EnablePartialRendering="true"></asp:ScriptManager>
        <asp:UpdatePanel ID="Updatepanel2" runat="server" ChildrenAsTriggers="true">
            <ContentTemplate>

                <div id="mdlIssueAvg" class="modal fade">
                    <div class="modal-dialog" style="width: 60%">
                        <div class="modal-content">
                            <div class="modal-header" style="background-color: #3c8dbc;">
                                <button type="button"
                                    class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                                <h4 class="modal-title" style="color: white; font-weight: bold;"></h4>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="panel panel-default text-center">
                                            <b>ITEM DETAIL</b><br />
                                            <p id="mdlAvgItem" class="text-uppercase"></p>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="panel panel-default text-center">
                                            <b>WAREHOUSE DETAIL</b><br />
                                            <p id="mdlAvgWarehouse" class="text-uppercase "></p>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6 col-md-push-3 text-center">
                                        <div class="form-group">
                                            <label>SELECT MEASUREMENT</label><br />
                                            <select id="ddlAvgMeasurement" class="form-control select2" style="width: 100%;" onchange="measurementChanged(this);"></select>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-3 text-center">
                                        <div class="panel panel-default">
                                            <div class="panel-body">
                                                <b>REQUESTED QTY</b><br />
                                                <p id="mdlAvgRequested">0</p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3 text-center">
                                        <div class="panel panel-default">
                                            <div class="panel-body">
                                                <b>ISSUED QTY</b>
                                                <p id="mdlAvgIssued">0</p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3 text-center">
                                        <div class="panel panel-default">
                                            <div class="panel-body">
                                                <b>PENDING QTY</b>
                                                <p id="mdlAvgPending">0</p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3 text-center">
                                        <div class="panel panel-default">
                                            <div class="panel-body">
                                                <b>AVAILABLE QTY </b>
                                                <p id="mdlAvgAvailable">20</p>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-6 col-md-push-3 text-center">
                                        <div class="form-group">
                                            <label>ISSUING QTY</label>
                                            <input id="mdlAvgIssuingQty" type="number" class="form-control" min="0" step="0.00001" onkeyup="checkAvailableQty()" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <button id="btnMdlAvgSubmit" class="btn btn-primary pull-right" style="margin-right: 10px" onclick="issueInventory(event);">SUBMIT</button>
                            </div>
                        </div>
                    </div>
                </div>

                <div id="mdlIssueBatch" class="modal fade">
                    <div class="modal-dialog" style="width: 60%">
                        <div class="modal-content">
                            <div class="modal-header" style="background-color: #3c8dbc;">
                                <button type="button"
                                    class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                                <h4 class="modal-title" style="color: white; font-weight: bold;">ISSUE INVENTORY</h4>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="panel panel-default text-center">
                                            <b>ITEM DETAIL</b><br />
                                            <p id="mdlBatchItem" class="text-uppercase"></p>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="panel panel-default text-center">
                                            <b>WAREHOUSE DETAIL</b><br />
                                            <p id="mdlBatchWarehouse" class="text-uppercase "></p>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6 col-md-push-3 text-center">
                                        <div class="form-group">
                                            <label>SELECT MEASUREMENT</label><br />
                                            <select id="ddlBatchMeasurement" class="form-control select2" style="width: 100%;" onchange="measurementChanged(this);"></select>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-3 text-center">
                                        <div class="panel panel-default">
                                            <div class="panel-body">
                                                <b>REQUESTED QTY</b><br />
                                                <p id="mdlBatchRequested">0</p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3 text-center">
                                        <div class="panel panel-default">
                                            <div class="panel-body">
                                                <b>ISSUED QTY</b>
                                                <p id="mdlBatchIssued">0</p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3 text-center">
                                        <div class="panel panel-default">
                                            <div class="panel-body">
                                                <b>PENDING QTY</b>
                                                <p id="mdlBatchPending">0</p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3 text-center">
                                        <div class="panel panel-default">
                                            <div class="panel-body">
                                                <b>AVAILABLE QTY </b>
                                                <p id="mdlBatchAvailable">0</p>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="panel panel-default">
                                    <div class="panel-body">
                                        <div class="row">
                                            <div class="col-md-12 text-center">
                                                <b>AVAILABLE BATCHES</b>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12">
                                                <span id="spnBatchOrder" style="color: #808080"></span>
                                                <br />
                                                <div class="table-responsive">
                                                    <table id="tblBatches" class="table">
                                                        <thead style="background-color: #3C8DBC; color: white; font-weight: bold;">
                                                            <tr>
                                                                <td class="hidden">BATCH ID</td>
                                                                <td>BATCH CODE</td>
                                                                <td>EXPIRY DATE</td>
                                                                <td>AVAILABLE QTY</td>
                                                                <td>ISSUING QTY</td>
                                                            </tr>
                                                        </thead>
                                                        <tbody id="tbBatches">
                                                        </tbody>
                                                        <tfoot style="background-color: #efefef; color: black; font-weight: bold;">
                                                            <tr>
                                                                <td class="text-right" colspan="3">TOTAL ISSUING QTY</td>
                                                                <td>
                                                                    <label id="lblTotalIssue" style="margin-left: 13px;">0</label>
                                                                </td>
                                                            </tr>
                                                        </tfoot>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <button id="btnMdlBatchSubmit" class="btn btn-primary pull-right" style="margin-right: 10px" onclick="issueInventory(event);">SUBMIT</button>
                            </div>
                        </div>
                    </div>
                </div>

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
                                    <div class="col-sm-3 text-center">
                                        <b>Requested QTY </b>
                                        <p id="requestedQtyShow"></p>
                                    </div>
                                    <div class="col-sm-3 text-center">
                                        <b>Issued QTY </b>
                                        <p id="issuedQtyShow"></p>
                                    </div>
                                    <div class="col-sm-3 text-center">
                                        <b>Pending QTY </b>
                                        <p id="pendingQtyShow"></p>
                                    </div>
                                    <div class="col-sm-3 text-center">
                                        <b>Available QTY </b>
                                        <p id="availableQtyShow"></p>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="co-md-12">
                                        <div class="table-responsive">
                                            <asp:GridView ID="gvWarehouseInventory" runat="server" CssClass="table table-responsive tablegv" GridLines="None" AutoGenerateColumns="false" EmptyDataText="No Records Found" AllowPaging="true" PageSize="20">
                                                <Columns>
                                                    <asp:BoundField DataField="WarehouseID" HeaderText="Warehouse ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                    <asp:BoundField DataField="ItemID" HeaderText="ItemID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                    <asp:BoundField DataField="Location" HeaderText="Location" />
                                                    <asp:BoundField DataField="AvailableQty" HeaderText="Avilable Qty" />
                                                    <asp:TemplateField HeaderText="Quantity">
                                                        <ItemTemplate>
                                                            <asp:TextBox runat="server" ID="IssuedQty" CssClass="IssuedQty" OnKeyUp="issuedQuantity(event)" min="0" step=".01"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:Button runat="server" Text="Issue" CssClass="btn btn-primary pull-right btnIssueCl" Style="margin-right: 10px" />
                            </div>
                        </div>
                    </div>
                </div>


            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnAddToTR" />
            </Triggers>
        </asp:UpdatePanel>
    </form>--%>

    <script src="AdminResources/js/jquery.min.js" type="text/javascript"></script>
    <script src="AdminResources/js/select2.full.min.js"></script>

    <script type="text/jscript">

        //new script
        var _ItemId, _MeasurementId, _WarehouseId, _StockMaintainingType, _RequestedQty, _IssuedQty, _MrnId, _MrndId, _IventoryDetails;

        function issueFromInventory(e, itemId, warehouseId, measurementId, stockMaintainingType, requestedQty, issuedQty, mrnId, mrndId) {
            e.preventDefault();

            _ItemId = itemId;
            _MeasurementId = measurementId;
            _WarehouseId = warehouseId;
            _StockMaintainingType = stockMaintainingType;
            _RequestedQty = requestedQty;
            _IssuedQty = issuedQty;
            _MrnId = mrnId;
            _MrndId = mrndId;

            populateMeasurements(itemId, measurementId, stockMaintainingType);
            populateInventory(measurementId);

            if (_StockMaintainingType == 1) {
                $('#mdlIssueAvg').modal('show');
            }
            else {
                $('#mdlIssueBatch').modal('show');
            }
        }

        function populateMeasurements(itemId, measurementId, stockMaintainingType) {
            $.ajax({
                type: "GET",
                url: 'ViewAssignedMRNDetails.aspx/GetItemMeasurements?ItemId=' + itemId,
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                success: function (response) {
                    response = JSON.parse(response.d);
                    if (response.Status == 200) {
                        var htmlText = ``;
                        for (var i = 0; i < response.Data.length; i++) {
                            if (response.Data[i].Id == measurementId) {
                                htmlText += `<option value='` + response.Data[i].Id + `' selected>` + response.Data[i].Name + `</option>`;
                            }
                            else {
                                htmlText += `<option value='` + response.Data[i].Id + `'>` + response.Data[i].Name + `</option>`;
                            }
                        }

                        if (stockMaintainingType == 1) {
                            $('#ddlAvgMeasurement').html(htmlText);
                        }
                        else {
                            $('#ddlBatchMeasurement').html(htmlText);
                        }

                        $('.select2').select2();
                    }
                },
                error: function (error) {
                }
            });
        }

        function populateInventory(measurementId) {
            debugger;
            $.ajax({
                type: "GET",
                url: 'ViewAssignedMRNDetails.aspx/GetStockInfo?ItemId=' + _ItemId + '&WarehouseId=' + _WarehouseId + '&MeasurementId=' + measurementId + '&RequestedQty=' + _RequestedQty + '&IssuedQty=' + _IssuedQty + '&RequestedMeasurement=' + _MeasurementId,
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                success: function (response) {
                    response = JSON.parse(response.d);
                    _IventoryDetails = response.Data;
                    

                    if (response.Status == 200) {
                        var htmlText = ``;

                        //$('#hdnStockMaintainType').val(response.Data.StockMaintainingType);

                        if (_StockMaintainingType == 1) {
                            $('#mdlAvgItem').html(response.Data.ItemName + " : " + response.Data.ItemCode);
                            $('#mdlAvgWarehouse').html(response.Data.WarehouseName);
                            var PendingQty = response.Data.RequestedQty - response.Data.IssuedQty;

                            $('#mdlAvgRequested').html(response.Data.RequestedQty);
                            $('#mdlAvgIssued').html(response.Data.IssuedQty);
                            if (PendingQty > 0) {
                                $('#mdlAvgPending').html(PendingQty);
                            }
                            else {
                                $('#mdlAvgPending').html("0.00");
                            }

                            $('#mdlAvgAvailable').html(response.Data.AvailableQty);
                            $('#mdlAvgIssuingQty').val('');
                        }
                        else {
                            $('#mdlBatchItem').html(response.Data.ItemName + " : " + response.Data.ItemCode);
                            $('#mdlBatchWarehouse').html(response.Data.WarehouseName);

                            $('#mdlBatchRequested').html(response.Data.RequestedQty);
                            $('#mdlBatchIssued').html(response.Data.IssuedQty);
                            $('#mdlBatchPending').html(response.Data.RequestedQty - response.Data.IssuedQty);
                            $('#mdlBatchAvailable').html(response.Data.AvailableQty);

                            for (var i = 0; i < response.Data.Batches.length; i++) {

                               
                                if (response.Data.Batches[i].ExpiryDate == "0001-01-01T00:00:00") {
                                    response.Data.Batches[i].ExpiryDate = response.Data.Batches[i].ExpiryDate.replace("0001-01-01", "Not Found")
                                }
                                
                                htmlText += `<tr>
                                                <td class="hidden batchIdCl">`+ response.Data.Batches[i].BatchchId + `</td>
                                                <td>`+ response.Data.Batches[i].BatchCode + `</td>
                                                <td>`+ response.Data.Batches[i].ExpiryDate.replace("T00:00:00", "") + `</td>
                                                <td class="availableQtyCl">`+ response.Data.Batches[i].AvailableStock + `</td>
                                                <td>
                                                    <input type="number" class="form-control txtQtyCl" onkeyup="calculateIssues();" min="0" step="0.00001" />
                                                </td>
                                            </tr>`
                            }

                            if (_StockMaintainingType == 2) {
                                $('#spnBatchOrder').html('Batches Ordered as per FIFO method');
                            }
                            else {
                                $('#spnBatchOrder').html('Batches Ordered as per FILO method');
                            }

                            $('#tbBatches').html(htmlText);
                            $('#lblTotalIssue').html('');
                        }
                    }
                },
                error: function (error) {
                }
            });
        }

        function measurementChanged(elm) {
            var measurementId = $(elm).find('option:selected').val();
            populateInventory(measurementId);
        }

        function checkAvailableQty() {
            if ($('#mdlAvgIssuingQty').val() != '') {
                if (parseFloat($('#mdlAvgIssuingQty').val()) > parseFloat($('#mdlAvgAvailable').text())) {
                    $('#mdlAvgIssuingQty').val($('#mdlAvgAvailable').text());
                }
            }
        }

        function issueInventory(e) {
            e.preventDefault();

            if (_StockMaintainingType == 1) {
                $('#mdlIssueAvg').modal('hide');
            }
            else {
                $('#mdlIssueBatch').modal('hide');
            }

            var issueNote;
            var mrndStatus;

            if (_StockMaintainingType == 1) {
                issueNote = {
                    MrndID: _MrndId,
                    ItemID: _ItemId,
                    WarehouseID: _WarehouseId,
                    IssuedQty: parseFloat($('#mdlAvgIssuingQty').val()),
                    IssuedStockValue: (_IventoryDetails.StockValue / (_IventoryDetails.AvailableQty + _IventoryDetails.HoldedQty)) * parseFloat($('#mdlAvgIssuingQty').val()),
                    MeasurementId: $('#ddlAvgMeasurement option:selected').val(),
                    RequestedMeasurementId: _MeasurementId,
                    IssuedBatches: []
                }



                if (issueNote.IssuedQty < parseFloat($('#mdlAvgPending').html()))
                    mrndStatus = 12;
                else
                    mrndStatus = 13;
            }
            else {
                issueNote = {
                    MrndID: _MrndId,
                    ItemID: _ItemId,
                    WarehouseID: _WarehouseId,
                    IssuedQty: 0,
                    IssuedStockValue: 0,
                    MeasurementId: $('#ddlBatchMeasurement option:selected').val(),
                    RequestedMeasurementId: _MeasurementId,
                    IssuedBatches: []
                }

                var totalIssuedQty = 0;
                var totalIssuedStockValue = 0;
                var rows = $('#tbBatches').find('tr');

                for (var i = 0; i < rows.length; i++) {
                    if ($(rows).eq(i).find('.txtQtyCl').val() != '') {
                        let issuedBatch = {
                            BatchId: parseInt($(rows).eq(i).find('.batchIdCl').text()),
                            IssuedQty: parseFloat($(rows).eq(i).find('.txtQtyCl').val()),
                            IssuedStockValue: (_IventoryDetails.Batches[i].StockValue / (_IventoryDetails.Batches[i].HoldedQty + _IventoryDetails.Batches[i].AvailableStock)) * parseFloat($(rows).eq(i).find('.txtQtyCl').val())
                        }
                        totalIssuedQty += parseFloat($(rows).eq(i).find('.txtQtyCl').val());

                        totalIssuedStockValue += issuedBatch.IssuedStockValue;
                        issueNote.IssuedBatches.push(issuedBatch);
                    }
                }

                issueNote.IssuedQty = totalIssuedQty;
                issueNote.IssuedStockValue = totalIssuedStockValue;

                if (issueNote.IssuedQty < parseFloat($('#mdlBatchPending').html()))
                    mrndStatus = 12;
                else
                    mrndStatus = 13;

            }

            var postData = {
                MrndId: _MrndId,
                MrndStatus: mrndStatus,
                MrnId: _MrnId,
                Note: issueNote
            };

            debugger;

            $.ajax({
                type: "POST",
                url: 'ViewAssignedMRNDetails.aspx/IssueInventory',
                data: JSON.stringify(postData),
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                success: function (response) {
                    response = JSON.parse(response.d);
                    if (response.Status == 200) {
                        swal({ type: 'success', title: 'Material Issued', text: 'Material Issued Successfully', showConfirmButton: true, closeOnConfirm: true }).then((result) => { window.location = 'ViewAssignedMRN.aspx' });
                    }
                    else if (response.Status == 500) {
                        swal({ type: 'error', title: 'Error', text: 'Material Issuing Failed!', showConfirmButton: true, closeOnConfirm: true }).then((result) => { window.location = 'ViewAssignedMRN.aspx' });
                    }
                    else if (response.Status == 600) {
                        swal({ type: 'error', title: 'Error', text: 'Issuing Quantity cannot be zero', showConfirmButton: true, closeOnConfirm: true });
                    }
                },
                error: function (error) {
                }
            });
        }

        function calculateIssues() {
            var totalIssue = 0;
            var rows = $('#tbBatches').find('tr');
            for (var i = 0; i < rows.length; i++) {
                if ($(rows).eq(i).find('.txtQtyCl').val() != '') {
                    if (parseFloat($(rows).eq(i).find('.txtQtyCl').val()) > parseFloat($(rows).eq(i).find('.availableQtyCl').text())) {
                        parseFloat($(rows).eq(i).find('.txtQtyCl').val($(rows).eq(i).find('.availableQtyCl').text()));
                    }

                    totalIssue += parseFloat($(rows).eq(i).find('.txtQtyCl').val());
                }
            }
            $('#lblTotalIssue').html(totalIssue);
        }



        //end
        function CheckBoxChecked(CheckBox) {
            //debugger;
            //get target base & child control.
            var TargetBaseControl = document.getElementById('<%= this.gvMRNItems.ClientID %>');
            var TargetChildControl = "CheckBox1";

            //get all the control of the type INPUT in the base control.
            var Inputs = TargetBaseControl.getElementsByTagName("input");

            for (var n = 0; n < Inputs.length; ++n) {
                if (Inputs[n].type == 'checkbox' && Inputs[n].id.indexOf(TargetChildControl, 0) >= 0 && Inputs[n].disabled == false) {
                    Inputs[n].checked = CheckBox.checked;
                    //CheckBox.checked ?Inputs[n].disabled = 'disabled' : Inputs[n].disabled = '';
                }
                else {

                }
            }
        }

        function printPage() {
            window.print();
        }

        function terminateMRN(e) {
            e.preventDefault();

            swal.fire({
                title: 'Are you sure?',
                html: "Are you sure you want Terminate this MRN?</br></br>"
                    + "<strong id='dd'>Remarks</strong>"
                    + "<input id='ss' type='text' class ='form-control' required='required'/></br>",
                type: 'warning',
                cancelButtonColor: '#d33',
                showCancelButton: true,
                showConfirmButton: true,
                confirmButtonText: 'Yes',
                cancelButtonText: 'No',
                allowOutsideClick: false,
                preConfirm: function () {
                    if ($('#ss').val() == '') {
                        $('#dd').prop('style', 'color:red');
                        swal.showValidationError('Remarks Required');
                        return false;
                    }
                    else {
                        $('#ContentSection_hdnRemarks').val($('#ss').val());
                    }

                }
            }
            ).then((result) => {
                if (result.value) {
                    $('#ContentSection_btnTerminate').click();
                }
            });
        }

        function AddToPR(e) {
            e.preventDefault();
            var flag = false;
            var rows = $("#ContentSection_gvMRNItems tbody tr");
            for (x = 1; x < rows.length; ++x) {
                var chk = $(rows[x]).find('td').find("input[type=checkbox]");
                if (chk.prop("checked")) {
                    flag = true;
                }
            }

            if (flag) {
                swal.fire({
                    title: 'Confirmation!',
                    html: "Click Yes to convert MRN to PR?</br></br>",
                    type: 'warning',
                    cancelButtonColor: '#d33',
                    showCancelButton: true,
                    showConfirmButton: true,
                    confirmButtonText: 'Yes',
                    cancelButtonText: 'No',
                    allowOutsideClick: false,
                    preConfirm: function () {
                    }
                }
                ).then((result) => {
                    if (result.value) {
                        $('#ContentSection_btnAddToPRhnd').click();
                    }
                });
            } else {
                swal.fire({
                    type: 'error',
                    title: 'ERROR',
                    text: 'Please check items ',
                    showConfirmButton: true,
                    timer: 4000
                });
            }
        }

        function terminateItem(e, obj) {
            e.preventDefault();
            $('#ContentSection_hndMrndId').val($(obj).closest("tr").find("td.MrndID").text());
            $('#ContentSection_hndItemId').val($(obj).closest("tr").find("td.ItemId").text());
            swal.fire({
                title: 'Are you sure?',
                html: "Are you sure you want Terminate this Item?</br></br>"
                    + "<strong id='dd'>Remarks</strong>"
                    + "<input id='ss' type='text' class ='form-control' required='required'/></br>",
                type: 'warning',
                cancelButtonColor: '#d33',
                showCancelButton: true,
                showConfirmButton: true,
                confirmButtonText: 'Yes',
                cancelButtonText: 'No',
                allowOutsideClick: false,
                preConfirm: function () {
                    if ($('#ss').val() == '') {
                        $('#dd').prop('style', 'color:red');
                        swal.showValidationError('Remarks Required');
                        return false;
                    } else {
                        $('#ContentSection_hndTerminationItemRemark').val($('#ss').val());
                    }

                }
            }
            ).then((result) => {
                if (result.value) {
                    $('#ContentSection_btnTerminateItemhnd').click();
                } else if (result.dismiss === Swal.DismissReason.cancel) {
                }
            });
        }


        function addStock(e, obj, StockMaintainingType) {
            e.preventDefault();
            $('#ContentSection_hndMrndId').val($(obj).closest("tr").find("td.MrndID").text());
            $('#ContentSection_hndItemId').val($(obj).closest("tr").find("td.ItemId").text());
            var itemName = $(obj).closest("tr").find("td.ItemName").text();
            var unit = $(obj).closest("tr").find("td.ItemUnit").text();
            var availableQty = $(obj).closest("tr").find("td.WarehouseAvailableQty").text();
            if (StockMaintainingType != 1) {
                swal.fire({
                    title: 'Add Stock',
                    html: "Please enter folowing details</br></br>"
                        + "<label>Item Name : </label><label>" + itemName + "</label><br/>"
                        + "<label>Available Qty : </label><label>" + availableQty + "</label><label>" + unit + "</label><br/>"
                        + "<div class='col-sm-12' style='padding-bottom: 5px;'>"
                        + "<strong style='float: left'>Item Quantity in " + unit + " : </strong>"
                        + "<input style='float: right;width: 70%;' id='ItemQty' type='number' min='0' onkeypress='return event.charCode != 45' class ='form-control' required='required'/> </div></br>"
                        + "<div class='col-sm-12'>"
                        + "<strong style='float: left'>Item Unit Price per " + unit + " : </strong>"
                        + "<input id='ItemPrice' style='float: right;width: 70%;' type='number' min='0' onkeypress='return event.charCode != 45' class ='form-control' required='required'/></div></br>"
                        + "<div class='col-sm-12'>"
                        + "<strong style='float: left'>Batch Expiry Date : </strong>"
                        + "<input id='expDate' style='float: right;width: 70%;' type='date' class ='form-control' required='required'/></div></br>",


                    type: 'warning',
                    cancelButtonColor: '#d33',
                    showCancelButton: true,
                    showConfirmButton: true,
                    confirmButtonText: 'Yes',
                    cancelButtonText: 'No',
                    allowOutsideClick: false,
                    preConfirm: function () {
                        if ($('#ItemQty').val() == '') {
                            $('#dd1').prop('style', 'color:red');
                            swal.showValidationError('Quantity Required');
                            return false;
                        } else {
                            $('#ContentSection_hdnAddStock').val($('#ItemQty').val());
                        }
                        if ($('#ItemPrice').val() == '') {
                            $('#dd2').prop('style', 'color:red');
                            swal.showValidationError('Price Required');
                            return false;
                        } else {
                            $('#ContentSection_hdnAddStockPrice').val($('#ItemPrice').val());
                        }
                        $('#ContentSection_hdnExpdate').val($('#expDate').val());
                    }
                }
                ).then((result) => {
                    if (result.value) {
                        $('#ContentSection_btnAddToStockhnd').click();
                    } else if (result.dismiss === Swal.DismissReason.cancel) {
                    }
                });
            }
            else {
                 swal.fire({
                    title: 'Add Stock',
                    html: "Please enter folowing details</br></br>"
                        + "<label>Item Name : </label><label>" + itemName + "</label><br/>"
                        + "<label>Available Qty : </label><label>" + availableQty + "</label><label>" + unit + "</label><br/>"
                        + "<div class='col-sm-12' style='padding-bottom: 5px;'>"
                        + "<strong style='float: left'>Item Quantity in " + unit + " : </strong>"
                        + "<input style='float: right;width: 70%;' id='ItemQty' type='number' min='0' onkeypress='return event.charCode != 45' class ='form-control' required='required'/> </div></br>"
                        + "<div class='col-sm-12'>"
                        + "<strong style='float: left'>Item Unit Price per " + unit + " : </strong>"
                        + "<input id='ItemPrice' style='float: right;width: 70%;' type='number' min='0' onkeypress='return event.charCode != 45' class ='form-control' required='required'/></div></br>",
                        
                    type: 'warning',
                    cancelButtonColor: '#d33',
                    showCancelButton: true,
                    showConfirmButton: true,
                    confirmButtonText: 'Yes',
                    cancelButtonText: 'No',
                    allowOutsideClick: false,
                    preConfirm: function () {
                        if ($('#ItemQty').val() == '') {
                            $('#dd1').prop('style', 'color:red');
                            swal.showValidationError('Quantity Required');
                            return false;
                        } else {
                            $('#ContentSection_hdnAddStock').val($('#ItemQty').val());
                        }
                        if ($('#ItemPrice').val() == '') {
                            $('#dd2').prop('style', 'color:red');
                            swal.showValidationError('Price Required');
                            return false;
                        } else {
                            $('#ContentSection_hdnAddStockPrice').val($('#ItemPrice').val());
                        }
                       
                    }
                }
                ).then((result) => {
                    if (result.value) {
                        $('#ContentSection_btnAddToStockhnd').click();
                    } else if (result.dismiss === Swal.DismissReason.cancel) {
                    }
                });
            }
        }


        function issueStock(e, obj) {
            e.preventDefault();
            $('#mdlIssueAvg').modal('show');
            //debugger;
            <%--$('#ContentSection_hndMrndId').val($(obj).closest("tr").find("td.MrndID").text());
            $('#ContentSection_hndItemId').val($(obj).closest("tr").find("td.ItemId").text());
            $('#ContentSection_hndRequestedQty').val($(obj).closest("tr").find("td.RequestedQty").text());
            var  requestedQty = parseFloat($(obj).closest("tr").find("td.RequestedQty").text());
            var  previouslyIssuedQty = parseFloat($(obj).closest("tr").find("td.IssuedQty").text());
            var warehouseStock = <%=ViewState["WarehouseStock"]  %>;
           // debugger;
            var warehouseitem = $.grep(warehouseStock, function (e) { return e.ItemID ==parseInt($(obj).closest("tr").find("td.ItemId").text()) });
            var warehouseAvailableQTY  = warehouseitem != null? warehouseitem[0].AvailableQty : 0;
            swal.fire({
                title: 'Issue Stock',
                html: "<b>Total avaiable Quantity in Stock " + warehouseAvailableQTY + "</b><br/>"
                    + "Please Provide Issue Quantity</br></br>" 
                    + "<strong id='dd'>QTY</strong>"
                    + "<input id='ss' type='number' class ='form-control' required='required'/></br>",
                type: 'warning',
                cancelButtonColor: '#d33',
                showCancelButton: true,
                showConfirmButton: true,
                confirmButtonText: 'Yes',
                cancelButtonText: 'No',
                allowOutsideClick: false,
                preConfirm: function () {
                   // debugger;
                    if ($('#ss').val() == '') {
                        $('#dd').prop('style', 'color:red');
                        swal.showValidationError('Quantity Required');
                        return false;
                    } else {
                        if(parseFloat($('#ss').val()) > warehouseAvailableQTY){
                            $('#dd').prop('style', 'color:red');
                            swal.showValidationError('Cannot exceed the available quantity');
                            return false;
                        }else if( parseFloat($('#ss').val()) > (requestedQty- previouslyIssuedQty) ){
                             $('#dd').prop('style', 'color:red');
                            swal.showValidationError('Cannot exceed the requested quantity , refer issue quantity');
                            return false;
                        }else{
                            $('#ContentSection_hdnIssueStock').val($('#ss').val());
                            $('#ContentSection_hndPreviousIssuedQty').val(previouslyIssuedQty);
                        }
                    }
                }
            }
            ).then((result) => {
               // debugger;
                if (result.value) {
                    $('#ContentSection_btnIssueStockhnd').click();
                } else if (result.dismiss === Swal.DismissReason.cancel) {
                }
          });--%>  
        }

    </script>

</asp:Content>
