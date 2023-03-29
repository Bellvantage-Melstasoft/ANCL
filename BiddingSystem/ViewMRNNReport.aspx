<%@ Page Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="ViewMRNNReport.aspx.cs" Inherits="BiddingSystem.ViewMRNNReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">
    <script src="AdminResources/js/jquery-1.10.2.min.js"></script>
    <section class="content-header">
        <h1>View Material Request Note</h1>
      <ol class="breadcrumb">
        <li><a href="AdminDashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
        <li class="active">View Material Request Note </li>
      </ol>
    </section>
    <br />
    <section class="content" id="divPrintPo">
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
                                    <asp:BoundField DataField="IssuedQty"  HeaderText="Issued QTY" DataFormatString="{0:N2}"/>
                                    <asp:BoundField DataField="ShortCode"  HeaderText="Unit" NullDisplayText="Not Found"/>
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
                 <div class="modal fade" id="mdlCapexDocs" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-body">
                    <div class="panel panel-default">
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    <h4 class="text-green text-bold">CAPEX DOCUMENTS</h4>
                                </div>
                            </div>
                            <hr />
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                         <asp:GridView ID="gvCapexDocs" runat="server" CssClass="table table-responsive"
                                            GridLines="None"  HeaderStyle-BackColor="#275591"  HeaderStyle-ForeColor="White"
                                            AutoGenerateColumns="false" EmptyDataText="No Documents Found" ShowHeader="true" ShowHeaderWhenEmpty="true">
                                            <Columns>
                                                <asp:BoundField DataField="MrnId" HeaderStyle-CssClass="hidden"
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
                                                       <%-- <asp:Label
                                                            runat="server"
                                                            Visible='<%# Eval("Status").ToString() == "1" ? true : false %>'
                                                            Text='<%# Eval("LogName")%>' CssClass="label label-success"/>
                                                        <asp:Label
                                                            runat="server"
                                                            Visible='<%# Eval("Status").ToString() == "10" ? true : false %>'
                                                            Text='<%# Eval("LogName")%>' CssClass="label label-danger" />
                                                        <asp:Label
                                                            runat="server"
                                                            Visible='<%# Eval("Status").ToString() == "11" ? true : false %>'
                                                            Text='<%# Eval("LogName")%>' CssClass="label label-success" />
                                                        <asp:Label
                                                            runat="server"
                                                            Visible='<%# Eval("Status").ToString() == "5" ? true : false %>'
                                                            Text='<%# Eval("LogName")%>' CssClass="label label-success" />
                                                        <asp:Label
                                                            runat="server"
                                                            Visible='<%# Eval("Status").ToString() == "6" ? true : false %>'
                                                            Text='<%# Eval("LogName")%>' CssClass="label label-danger" />
                                                        <asp:Label
                                                            runat="server"
                                                            Visible='<%# Eval("Status").ToString() == "3" ? true : false %>'
                                                            Text='<%# Eval("LogName")%>'  CssClass="label label-success"/>
                                                        <asp:Label
                                                            runat="server"
                                                            Visible='<%# Eval("Status").ToString() == "4" ? true : false %>'
                                                            Text='<%# Eval("LogName")%>'  CssClass="label label-danger"/>
                                                        <asp:Label
                                                            runat="server"
                                                            Visible='<%# Eval("Status").ToString() == "12" ? true : false %>'
                                                            Text='<%# Eval("LogName")%>' CssClass="label label-info" />
                                                        <asp:Label
                                                            runat="server"
                                                            Visible='<%# Eval("Status").ToString() == "9" ? true : false %>'
                                                            Text='<%# Eval("LogName")%>'  CssClass="label label-info"/>
                                                        <%--<asp:Label
                                                            runat="server"
                                                            Visible='<%# Eval("Status").ToString() == "8" ? true : false %>'
                                                            Text='<%# Eval("LogName")%>'  CssClass="label label-info"/>--%>
                                                     <%--   <asp:Label
                                                            runat="server"
                                                            Visible='<%# Eval("Status").ToString() == "7" ? true : false %>'
                                                            Text='<%# Eval("LogName")%>'  CssClass="label label-success"/>
                                                        <asp:Label
                                                            runat="server"
                                                            Visible='<%# Eval("Status").ToString() == "8" ? true : false %>'
                                                            Text='<%# Eval("LogName")%>'  CssClass="label label-success"/>
                                                        <asp:Label
                                                            runat="server"
                                                            Visible='<%# Eval("Status").ToString() == "13" ? true : false %>'
                                                            Text='<%# Eval("LogName")%>'  CssClass="label label-info"/>--%>
                                                    <asp:Label
                                                            runat="server"
                                                            Text='<%# Eval("LogName")%>'  CssClass="label label-info"/>
                                                        
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
                                <%--<asp:Label runat="server" ID="lblWarning" CssClass="label label-warning" Visible="false" ></asp:Label>
                                <asp:Label runat="server" ID="lblSuccess" CssClass="label label-success" Visible="false" ></asp:Label>
                                <asp:Label runat="server" ID="lblDanger" CssClass="label label-info" Visible="false" ></asp:Label>
                                --%><asp:Label runat="server" ID="lblInfo" CssClass="label label-info"></asp:Label><br>
                             <strong>PURCHASE TYPE: </strong>
                                 <asp:Label runat="server" ID="lblPurchaseType" CssClass="label label-info"></asp:Label><br>
                                           
                             </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <br />
                                <br />
                                <div class="table-responsive">
                                    <asp:GridView runat="server" ID="gvMRNItems" AutoGenerateColumns="false"
                                        CssClass="table table-responsive" HeaderStyle-BackColor="LightGray" BorderColor="LightGray" EnableViewState="true">
                                        <Columns>
                                            <asp:BoundField DataField="MrndID" HeaderText="MrndID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />  
                                            <asp:BoundField DataField="ItemName" HeaderText="Item Name" />
                                            <asp:BoundField DataField="RequestedQty" HeaderText="Requested QTY" DataFormatString="{0:N2}"/>
                                            <asp:BoundField DataField="MeasurementShortName" HeaderText="Unit" NullDisplayText="Not Found" />
                                            <asp:BoundField DataField="Description" HeaderText="Description" />
                                            <asp:BoundField DataField="ReceivedQty" DataFormatString="{0:N2}"
                                                HeaderText="Received Quantity" />
                                            <asp:BoundField DataField="IssuedQty" DataFormatString="{0:N2}"
                                                HeaderText="Issued Quantity" />
                                            <asp:TemplateField HeaderText="Import Item Type">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Visible='<%# Eval("PurchaseType").ToString() == "2" ? true : false %>'
                                                  Text='<%# Eval("ImportItemType").ToString() == "1" ? "Spare Parts" : "Material" %>' />
                                            </ItemTemplate>
                                            </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Spare Part No">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Visible='<%# Eval("PurchaseType").ToString() != "2" ? false : Eval("ImportItemType").ToString() == "1" ? true : false %>'
                                                  Text='<%# Eval("SparePartNo") %>' />
                                            </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <asp:Label
                                                    runat="server"
                                                    Text='<%#Eval("StatusName")%>' CssClass="label label-info"/>
                                               <%-- <asp:Label
                                                    runat="server"
                                                    Visible='<%# Eval("Status").ToString() == "3" ? true : false %>'
                                                    Text='<%#Eval("StatusName")%>' CssClass="label label-info"/>
                                                <asp:Label
                                                    runat="server"
                                                    Visible='<%# Eval("Status").ToString() == "7" ? true : false %>'
                                                    Text='<%#Eval("StatusName")%>' CssClass="label label-danger"/>
                                                <asp:Label
                                                    runat="server"
                                                    Visible='<%# Eval("Status").ToString() == "9" ? true : false %>'
                                                    Text='<%#Eval("StatusName")%>' CssClass="label label-success"/>
                                                <asp:Label
                                                    runat="server"
                                                    Visible='<%# Eval("Status").ToString() == "11" ? true : false %>'
                                                    Text='<%#Eval("StatusName")%>' CssClass="label label-success"/>
                                                <asp:Label
                                                    runat="server"
                                                    Visible='<%# Eval("Status").ToString() == "4" ? true : false %>'
                                                    Text='<%#Eval("StatusName")%>' CssClass="label label-danger"/>
                                                <asp:Label
                                                    runat="server"
                                                    Visible='<%# Eval("Status").ToString() == "1" ? true : false %>'
                                                    Text='<%#Eval("StatusName")%>' CssClass="label label-success"/>
                                                <asp:Label
                                                    runat="server"
                                                    Visible='<%# Eval("Status").ToString() == "2" ? true : false %>'
                                                    Text='<%#Eval("StatusName")%>' CssClass="label label-danger"/>
                                                 <asp:Label
                                                    runat="server"
                                                    Visible='<%# Eval("Status").ToString() == "5" ? true : false %>'
                                                    Text='<%#Eval("StatusName")%>' CssClass="label label-info"/>
                                                <asp:Label
                                                    runat="server"
                                                    Visible='<%# Eval("Status").ToString() == "6" ? true : false %>'
                                                    Text='<%#Eval("StatusName")%>' CssClass="label label-info"/>
                                                <asp:Label
                                                    runat="server"
                                                    Visible='<%# Eval("Status").ToString() == "12" ? true : false %>'
                                                    Text='<%#Eval("StatusName")%>' CssClass="label label-info"/>
                                                <asp:Label
                                                    runat="server"
                                                    Visible='<%# Eval("Status").ToString() == "13" ? true : false %>'
                                                    Text='<%#Eval("StatusName")%>' CssClass="label label-success"/>
                                                <asp:Label
                                                    runat="server"
                                                    Visible='<%# Eval("Status").ToString() == "14" ? true : false %>'
                                                    Text='<%#Eval("StatusName")%>' CssClass="label label-success"/>--%>
                                                                                           
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
                                        <asp:TemplateField HeaderText="Action" HeaderStyle-CssClass="no-print" ItemStyle-CssClass="no-print">
                                            <ItemTemplate>
                                                <asp:LinkButton Visible='<%# Eval("Status").ToString() == "7" ? true : false %>' ForeColor="Red"  runat="server" ID="lbtnMore" ToolTip="Termination Details" OnClick="lbtnMore_Click" CssClass="btn "><i class="fa fa-info-circle"></i></asp:LinkButton>
                                                <asp:LinkButton ForeColor="Orange"  runat="server" ID="lbtnLog" ToolTip="Action Log" OnClick="lbtnLog_Click" CssClass="btn "><i class="fa fa-history"></i></asp:LinkButton>
                                                <asp:LinkButton ForeColor="Green" Visible='<%# decimal.Parse(Eval("IssuedQty").ToString()) > 0 ? true : false %>' runat="server" OnClick="lbtnIssueNote_Click" ID="lbtnIssueNote" ToolTip="Issue Notes" CssClass="btn "><i class="fa fa-th-list"></i></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                        <hr />
                        <div class="row">
                            <div class="col-xs-3 col-sm-3 text-center">
                                <asp:Image ID="imgCreatedBySignature" Style="width: 100px; height:50px;" runat="server" /><br />
                                <asp:Label runat="server" ID="lblCreatedByName"></asp:Label><br />
                                <asp:Label runat="server" ID="lblCreatedDate"></asp:Label><br />
                                <b>MRN Created By</b>
                                <hr style="padding-left:10px; padding-right:10px;" />
                            </div>
                            <asp:Panel ID="pnlApprovedByDetails" runat="server" Visible="false">
                                <div class="col-xs-3 col-sm-3 text-center">
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
                                <div class="col-xs-3 col-sm-3 text-center">
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
                        <asp:Button runat="server" ID="btnPrint"  Text="Print MRN" CssClass="btn btn-success" OnClientClick="printPage()" />
                        <asp:Button runat="server" ID="btnCapexDocs" CssClass="btn btn-info" Text="Capex Docs" OnClick="btnCapexDocs_Click" />
                    </div>
                </div>                    
            
            <asp:HiddenField runat="server" ID="hdnRemarks" />
            </ContentTemplate>
        </asp:UpdatePanel>
        </form>
    </div>
    </section>
    <script type="text/jscript">
        function printPage() {
            window.print();
        }


    </script>

</asp:Content>
