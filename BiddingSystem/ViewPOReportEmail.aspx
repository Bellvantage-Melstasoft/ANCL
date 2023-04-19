<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="ViewPOReportEmail.aspx.cs" Inherits="BiddingSystem.ViewPOReportEmail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">

    <style type="text/css">
        body {
        }

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

        table, td, tr {
            border-color: black;
        }
    </style>

    <script src="AdminResources/js/jquery.min.js" type="text/javascript"></script>
    <section class="content-header">
        <h1>View Purchase Order
        <small></small>
        </h1>
        <ol class="breadcrumb">

            <li><a href="AdminDashboard.aspx"><i class="fa fa-home"></i>Home</a></li>
            <li class="active">View Purchase Order </li>
        </ol>
    </section>
    <br />

    <form runat="server" id="form1">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="Updatepanel1" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
            <ContentTemplate>
                <section class="content" style="padding-top: 0px" id="divPrintPo">

                    <div id="modalSelectCheckBox" class="modal fade">
                        <div class="modal-dialog">
                            <div class="modal-content">
                                <div class="modal-header" style="background-color: #e66657">
                                    <button type="button"
                                        class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                    <h2 id="lblTitle" style="color: white;" class="modal-title">Alert!!</h2>
                                </div>
                                <div class="modal-body" style="background-color: white">
                                    <h4>Atleast select one Item to Approve PO</h4>
                                </div>
                                <div class="modal-footer" style="background-color: white">
                                    <button id="btnOkAlert" type="button" class="btn btn-danger">OK</button>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="modalRejectSubmitCheckBox" class="modal fade">
                        <div class="modal-dialog">
                            <div class="modal-content">
                                <div class="modal-header" style="background-color: #e66657">
                                    <button type="button"
                                        class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                    <h2 id="lblTitleR" style="color: white;" class="modal-title">Alert!!</h2>
                                </div>
                                <div class="modal-body" style="background-color: white">
                                    <h4>Atleast select one Item to Reject PO</h4>
                                </div>
                                <div class="modal-footer" style="background-color: white">
                                    <button id="btnOkAlertR" type="button" class="btn btn-danger">OK</button>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div id="mdlAttachments" class="modal fade" tabindex="-1" role="dialog" style="z-index: 3000;"
                        aria-hidden="true">
                        <div class="modal-dialog">
                            <!-- Start : Modal Content -->
                            <div class="modal-content">
                                <!-- Start : Modal Header -->
                                <div class="modal-header" style="background-color: #3C8DBC; color: white">
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="opacity: 1; color: white;">
                                        <span aria-hidden="true" style="opacity: 1;">×</span></button>
                                    <h4 class="modal-title">Attachments Quotations</h4>
                                </div>
                                <!-- End : Modal Header -->
                                <!-- Start : Modal Body -->
                                <div class="modal-body">
                                    <div class="row">
                                        <div class="col-xs-12">
                                            <div class="form-group">
                                                <asp:Panel ID="pnlImages" runat="server">
                                                    <label for="fileImages">Uploded Images</label>
                                                    <asp:GridView ID="gvImages" runat="server" ShowHeader="False"
                                                        GridLines="None" AutoGenerateColumns="false" EmptyDataText="No Image Found">
                                                        <Columns>
                                                            <asp:BoundField DataField="QuotationImageId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:HyperLink runat="server" href='<%#Eval("ImagePath").ToString().Remove(0,2)%>' Target="_blank">
                                                                            <asp:Image runat="server" ImageUrl='<%#Eval("ImagePath")%>' style="max-height:50px; width:auto; margin:5px" />
                                                                    </asp:HyperLink>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </div>
                                            <div class="form-group">
                                                <asp:Panel ID="pnlDocs" runat="server" Width="100%">
                                                    <label for="fileImages">Uploded Documents</label>
                                                    <asp:GridView ID="gvDocs" runat="server" ShowHeader="False"
                                                        GridLines="None" AutoGenerateColumns="false" EmptyDataText="No Document Found" Width="100%">
                                                        <Columns>
                                                            <asp:BoundField DataField="QuotationFileId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                            <asp:TemplateField ItemStyle-Height="30px">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton
                                                                        Text='<%#Eval("FileName")%>' runat="server" href='<%#Eval("FilePath").ToString().Remove(0,2)%>' target="_blank" Style="margin-right: 5px;" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </div>
                                            <div class="form-group">
                                                <asp:Panel ID="pnlcondtion" runat="server" Width="100%">
                                                    <label for="fileImages">Terms And Conditons</label>
                                                    <asp:TextBox TextMode="MultiLine" Rows="10" ID="txtTermsAndConditions" Enabled="false" runat="server" CssClass="form-control text-bold"></asp:TextBox>
                                                </asp:Panel>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- End : Modal Body -->
                            </div>
                            <!-- End : Modal Content -->
                        </div>
                    </div>

                    <div id="myModal" class="modal modal-primary fade" tabindex="-1" role="dialog" aria-hidden="true">
                        <div class="modal-dialog">
                            <!-- Modal content-->
                            <div class="modal-content" style="background-color: #a2bdcc;">
                                <div class="modal-header" style="background-color: #7bd47dfa;">
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span></button>
                                    <h4 class="modal-title">Attachment</h4>
                                </div>
                                <div class="modal-body">
                                    <div class="login-w3ls">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="table-responsive">
                                                    <asp:GridView ID="gvUploadFiles" runat="server" CssClass="table table-responsive tablegv" Style="border-collapse: collapse; color: black;"
                                                        GridLines="None" AutoGenerateColumns="false">
                                                        <Columns>
                                                            <asp:BoundField DataField="ItemId" HeaderText="ItemId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                            <asp:BoundField DataField="PrId" HeaderText="PrId" />
                                                            <asp:BoundField DataField="FilePath" HeaderText="FilePath" />
                                                            <asp:BoundField DataField="FileName" HeaderText="FileName" />

                                                            <asp:TemplateField HeaderText="View">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="btnEdit" ImageUrl="~/images/view-icon-614x460.png" Style="width: 39px; height: 26px"
                                                                        runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Download">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="btnCancelRequest" ImageUrl="~/images/Downloads2.png" Style="width: 26px; height: 20px; margin-top: 4px;"
                                                                        runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                            <div>
                                                <label id="lbMailMessage" style="margin: 3px; color: maroon; text-align: center;"></label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>


                    <div id="mdlTerminated" class="modal fade" tabindex="-1" role="dialog" style="z-index: 3000;"
                        aria-hidden="true">
                        <div class="modal-dialog" style="width: 60%;">
                            <!-- Start : Modal Content -->
                            <div class="modal-content">
                                <!-- Start : Modal Header -->
                                <div class="modal-header" style="background-color: #3C8DBC; color: white">
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="opacity: 1; color: white;">
                                        <span aria-hidden="true" style="opacity: 1;">×</span></button>
                                    <h4 class="modal-title">Terminated PO</h4>
                                </div>
                                <!-- End : Modal Header -->
                                <!-- Start : Modal Body -->
                                <div class="modal-body">

                                    <div class="row">
                                        <div class="col-xs-12">


                                            <div>
                                                &nbsp
                                            </div>

                                            <!-- Start : Items Table -->
                                            <div style="color: black;">

                                                <asp:GridView ID="gvTerminatedDetails" runat="server"
                                                    CssClass="table table-responsive"
                                                    GridLines="None" AutoGenerateColumns="false"
                                                    EmptyDataText="No MRNs Found" RowStyle-BackColor="#f5f2f2" HeaderStyle-BackColor="#525252" HeaderStyle-ForeColor="White">
                                                    <Columns>

                                                        <asp:BoundField DataField="PodId" HeaderText="POD ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                        <asp:BoundField DataField="TerminationRemarks" HeaderText="Terminated Remarks" />
                                                        <asp:BoundField DataField="TerminatedByName" HeaderText="Terminated By" />
                                                        <asp:BoundField DataField="TerminatedOn" HeaderText="Terminated On" />


                                                    </Columns>
                                                </asp:GridView>

                                            </div>
                                            <!-- End : MRN Table -->
                                        </div>
                                    </div>
                                </div>
                                <!-- End : Modal Body -->
                            </div>
                            <!-- End : Modal Content -->
                        </div>
                    </div>


                    <%--  <div class="content" style="position: relative;background: #fff;overflow:hidden; border: 1px solid #f4f4f4;" id="divPrintPo" runat="server" >    <!-- Main content -->
                    --%>



                    <div class="box box-info">
                        <%--<div class="box-header">
                            <img src="AdminResources/images/logo.png" class="center-block" />--%>
                        <%--<img src="AdminResources/images/ImportLogo1.png" alt="" id="Logo1"/>    
                             <ASP:Image ID="ShippingLogo" Visibile="False" ImgUrl="AdminResources/images/ImportLogo1.png"  runat="server"/>--%>
                        <%-- <h3 class="text-center"><i class="fa fa-file-invoice"></i>&nbsp;&nbsp;&nbsp;Purchase Order</h3>--%>
                        <%--                            <ASP:Image id="myImage" Visibile="False" ImgUrl="AdminResources/images/logo.png" runat="server"/>
                        --%>
                        <%--</div>--%>
                        <div class="box-body">
                            <div class="row">
                                <div class="row">
                                    <div class="col-xs-6">
                                        <img src="AdminResources/images/logo.png" align="right" />



                                    </div>
                                    <div class="col-xs-6 ">
                                        <%--<strong>COMPANY: </strong>--%>
                                        <b>
                                            <asp:Label runat="server" ID="lblCompName" Font-Size="Medium"></asp:Label></b><br>
                                        <b>
                                            <asp:Label runat="server" ID="lblcompAdd" Font-Size="Medium"></asp:Label></b><br>
                                        <strong>TP: </strong>
                                        <asp:Label runat="server" ID="lblTpNo"></asp:Label><br>
                                        <strong>FAX: </strong>
                                        <asp:Label runat="server" ID="lblFax"></asp:Label><br>
                                        <strong>VAT: </strong>
                                        <asp:Label runat="server" ID="lblCompVatNo"></asp:Label><br>
                                    </div>
                                </div>
                                <h3 class="text-center"><i class="fa fa-file-invoice"></i>&nbsp;&nbsp;&nbsp;Purchase Order</h3>

                                <hr>
                            </div>
                            <div class="row">
                                <div class="col-md-4 col-xs-4">
                                    <strong>SUPPLIER: </strong>
                                    <br>
                                    <asp:Label runat="server" ID="lblSupName"></asp:Label><br>
                                    <asp:Label runat="server" ID="lblSupplierAddress"></asp:Label><br>
                                    <asp:Label runat="server" ID="lblSupplierContact"></asp:Label>
                                </div>

                                <%--<div class="col-md-3 col-xs-3">
                                    
                                    <strong>COMPANY: </strong>
                                    <asp:Label runat="server" ID="lblCompName"></asp:Label><br>
                                    <strong>VAT NO: </strong>
                                    <asp:Label runat="server" ID="lblCompVatNo"></asp:Label><br>
                                    <strong>TELEPHONE: </strong>
                                    <asp:Label runat="server" ID="lblTpNo"></asp:Label><br>
                                    <strong>FAX: </strong>
                                    <asp:Label runat="server" ID="lblFax"></asp:Label><br>
                                    <%--<strong>STORE KEEPER: </strong>
                                    <asp:Label runat="server" ID="lblSK"></asp:Label><br>--%>
                                <%--</div>--%>
                                <div class="col-md-4 col-xs-4">
                                    <strong>DELIVERING WAREHOUSE: </strong>
                                    <br>
                                    <asp:Label runat="server" ID="lblWarehouseName"></asp:Label><br>
                                    <asp:Label runat="server" ID="lblWarehouseAddress"></asp:Label><br>
                                    <asp:Label runat="server" ID="lblWarehouseContact"></asp:Label>

                                    <br>
                                    <strong>STORE KEEPER: </strong>
                                    <asp:Label runat="server" ID="lblStoreKeeper"></asp:Label><br>
                                    <br>
                                </div>
                                <div class="col-md-4 col-xs-4">
                                    <strong>DATE: </strong>
                                    <asp:Label runat="server" ID="lblDate"></asp:Label><br>
                                    <strong>PO CODE: </strong>
                                    <asp:Label runat="server" ID="lblPO"></asp:Label><br>
                                    <strong>BASED PR: </strong>
                                    <asp:Label runat="server" ID="lblPrCode"></asp:Label><br>
                                    <%-- <strong>QUOTATION FOR: </strong>
                                    <asp:Label runat="server" ID="lblQuotationFor"></asp:Label><br>--%>
                                    <strong>APPROVAL STATUS: </strong>
                                    <asp:Label runat="server" ID="lblPending" CssClass="label label-warning" Visible="false" Text="Pending"></asp:Label>
                                    <asp:Label runat="server" ID="lblApproved" CssClass="label label-success" Visible="false" Text="Approved"></asp:Label>
                                    <asp:Label runat="server" ID="lblRejected" CssClass="label label-danger" Visible="false" Text="Rejected"></asp:Label><br>
                                    <strong>PO TYPE: </strong>
                                    <asp:Label runat="server" ID="lblGeneral" CssClass="label label-success" Visible="false" Text="General PO"></asp:Label>
                                    <asp:Label runat="server" ID="lblCovering" CssClass="label label-info" Visible="false" Text="Covering PO"></asp:Label>
                                    <asp:Label runat="server" ID="lblModified" CssClass="label label-warning" Visible="false" Text="Modified PO"></asp:Label><br>
                                    <strong>PR PURCHASE TYPE: </strong>
                                    <asp:Label runat="server" ID="lblPurchaseType"></asp:Label><br>
                                    <asp:Panel runat="server" ID="pnlPaymentMethod" Visible="false">
                                        <strong>PAYMENT METHOD: </strong>
                                        <asp:Label runat="server" ID="lblPaymentType"></asp:Label>
                                    </asp:Panel>
                                    <strong>PO Purchase Type : </strong>
                                    <asp:Label ID="lblPoPurchaseType" runat="server" Text=""></asp:Label><br />
                                    <strong>Agent Name : </strong>
                                    <asp:Label ID="lblAgentName" runat="server" Text=""></asp:Label><br />
                                    <%--<asp:Panel runat="server" ID="pnlReason" Visible="false">
                                        <strong>REMARKS: </strong>
                                         <asp:Label runat="server" ID="lblRemarks"></asp:Label>
                                    </asp:Panel>--%>
                                </div>
                            </div>


                            <div class="row">
                                <div class="col-md-12">
                                    <br />
                                    <br />
                                    <div class="table-responsive">
                                        <asp:GridView runat="server" ID="gvPoItems" AutoGenerateColumns="false" OnRowDataBound="gvPOItems_RowDataBound"
                                            CssClass="table table-responsive" HeaderStyle-BackColor="LightGray" BorderColor="LightGray" EnableViewState="true">
                                            <Columns>
                                                <asp:BoundField DataField="PodId" HeaderText="POD ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                <asp:BoundField DataField="ItemId" HeaderText="Item Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                <asp:BoundField DataField="ItemName" HeaderText="Default Item Name" />
                                                <asp:BoundField DataField="SupplierMentionedItemName" HeaderText="Supplier mentioned Item Name" NullDisplayText="Not Found" />
                                                <%-- <asp:TemplateField HeaderText="Supplier mentioned Item Name">
                                                                            <ItemTemplate>
                                                                                <asp:Label
                                                                                    runat="server"
                                                                                    Text='<%# Eval("SupplierMentionedItemName").ToString() == "" ? "Not Found" : Eval("SupplierMentionedItemName").ToString() %>'/>
                                                                                
                                                                                 </ItemTemplate>
                                                               </asp:TemplateField>--%>

                                                <%--<asp:BoundField DataField="MeasurementShortName" HeaderText="Measurement" NullDisplayText="Not Found" />--%>
                                                <asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                        <asp:Label
                                                            runat="server"
                                                            Visible='<%# Eval("Status").ToString() == "0" ? true : false %>'
                                                            Text="Awaiting Receival" CssClass="label label-warning" />
                                                        <asp:Label
                                                            runat="server"
                                                            Visible='<%# Eval("Status").ToString() == "1" ? true : false %>'
                                                            Text="Partially Received" CssClass="label label-info" />
                                                        <asp:Label
                                                            runat="server"
                                                            Visible='<%# Eval("Status").ToString() == "2" ? true : false %>'
                                                            Text="Fully Received" CssClass="label label-success" />
                                                        <asp:LinkButton
                                                            runat="server" ID="btnMrn"
                                                            Visible='<%# Eval("Status").ToString() == "3" ? true : false %>'
                                                            Text="Terminated" CssClass="label label-danger" OnClick="btnTerminated_Click"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="TermName" HeaderText="Term" />
                                                <asp:BoundField DataField="MeasurementName" HeaderText="Unit" NullDisplayText="Not Found" />
                                                <asp:BoundField DataField="Quantity" HeaderText="Requested QTY" DataFormatString="{0:N2}" />
                                                <asp:BoundField DataField="UnitPriceForeign" HeaderText="Quoted Unit Price(Foreign)"
                                                    ItemStyle-Font-Bold="true" />
                                                <asp:BoundField DataField="UnitPriceLocal" HeaderText="Quoted Unit Price(Local)"
                                                    ItemStyle-Font-Bold="true" />
                                                <asp:BoundField DataField="ReceivedQty" HeaderText="Recieved QTY" DataFormatString="{0:N2}" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                <asp:BoundField DataField="WaitingQty" HeaderText="Waiting QTY" DataFormatString="{0:N2}" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                <asp:BoundField DataField="PendingQty" HeaderText="Pending QTY" DataFormatString="{0:N2}" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />

                                                <asp:BoundField DataField="ItemPrice" HeaderText="Quoted Price" DataFormatString="{0:N2}" />
                                                <asp:BoundField DataField="SubTotal" HeaderText="SubTotal" DataFormatString="{0:N2}" />
                                                <asp:BoundField DataField="NbtAmount" HeaderText="NBT" DataFormatString="{0:N2}" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                <asp:BoundField DataField="VatAmount" HeaderText="VAT" DataFormatString="{0:N2}" />
                                                <asp:BoundField DataField="TotalAmount" HeaderText="NetTotal" DataFormatString="{0:N2}" />
                                                <asp:TemplateField HeaderText="PO Purchase Type" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Text='<%# Eval("PoPurchaseType").ToString() == "1" ? "Local":"Import" %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="SupplierAgentName" HeaderText="Agent Name" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                <asp:BoundField DataField="SparePartNumber" HeaderText="Spare Part Number" />
                                                <asp:TemplateField HeaderText="Attachments" HeaderStyle-CssClass="no-print" ItemStyle-CssClass="no-print">
                                                    <ItemTemplate>
                                                        <asp:Button CssClass="btn btn-xs btn-default no-print" OnClick="btnViewAttachments_Click" runat="server"
                                                            Text="View"></asp:Button>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-8 col-xs-8">
                                    <asp:Panel ID="PanenImports" runat="server" Visible="false">
                                        <%--<div class="col-md-2">
                                    <img src="AdminResources/images/ImportLogo1.png" class="left-block" height="100" width="100" />
                                     </div>--%>
                                        <%--<asp:Panel ID="pnlLogo" runat="server" Visible="false">--%>
                                        <%--<img src="AdminResources/images/ImportLogo1.png" class="left-block" height="120" width="140" /><br>--%>
                                        <%--</asp:Panel>--%>

                                        <%-- <strong>PRICE TERMS: </strong>
                                    <asp:Label runat="server" ID="lblPriceTerms"></asp:Label><br>--%>
                                        <asp:Panel runat="server" ID="pnlLogo" Visible="false">
                                            <label>Shipping Mark : </label>
                                            <img src="AdminResources/images/ImportLogo1.png" height="80" width="120" /><br>
                                        </asp:Panel>
                                        <strong>CURRENCY: </strong>
                                        <asp:Label runat="server" ID="lblCurrency"></asp:Label><br>
                                        <strong>PAYMENT MODE: </strong>
                                        <asp:Label runat="server" ID="lblPaymentMode"></asp:Label><br>
                                    </asp:Panel>
                                    <asp:Panel ID="Remarks" runat="server">
                                        <div class="form-group">
                                            <label>Remarks : </label>
                                            <asp:Label TextMode="MultiLine" Rows="6" runat="server" ID="txtRemarks"></asp:Label>
                                        </div>
                                    </asp:Panel>
                                </div>

                                <div class="col-md-4 col-xs-4">
                                    <p class="lead">SUMMARY</p>
                                    <div class="table-responsive">
                                        <table class="table table-striped">
                                            <tbody>
                                                <tr>
                                                    <td><b>TOTAL</b></td>
                                                    <td id="tdSubTotal" class="text-right" runat="server"></td>
                                                </tr>

                                                <%--<tr>
                                                    <td><b>NBT</b></td>
                                                    <td id="tdNbt" class="text-right" runat="server"></td>
                                                </tr>--%>
                                                <tr>
                                                    <td><b>VAT</b></td>
                                                    <td id="tdVat" class="text-right" runat="server"></td>
                                                </tr>
                                                <tr>
                                                    <td><b>NETTOTAL</b></td>
                                                    <td id="tdNetTotal" class="text-right" runat="server"></td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                            <hr />
                            <div class="row">
                                <div class="col-xs-4 col-sm-4 text-center">
                                    <asp:Image ID="imgCreatedBySignature" Style="width: 100px; height: 50px;" runat="server" /><br />
                                    <%--<asp:Label runat="server" ID="lblCreatedByName"></asp:Label><br />--%>
                                    <asp:Label runat="server" ID="lblCreatedByDesignation"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblCreatedDate"></asp:Label><br />
                                    <b>PO Created By</b>
                                    <hr style="padding-left: 10px; padding-right: 10px;" />
                                </div>
                                <asp:Panel ID="pnlParentApprovedByDetails" runat="server" Visible="false">
                                    <div class="col-xs-4 col-sm-4 text-center">
                                        <asp:Image ID="imgParentApprovedBySignature" Style="width: 100px; height: 50px;" runat="server" /><br />
                                        <%-- <asp:Label runat="server" ID="lblParentApprovedByName"></asp:Label><br />--%>
                                        <asp:Label runat="server" ID="lblParentApprovedByDesignation"></asp:Label><br />
                                        <asp:Label runat="server" ID="lblParentApprovedDate"></asp:Label><br />
                                        <b id="lblParentApprovalText" runat="server"></b>
                                        <hr style="padding-left: 10px; padding-right: 10px;" />
                                        <strong>REMARKS</strong><br />
                                        <asp:Label runat="server" ID="lblParentApprovalRemarks" CssClass="text-left" Style="padding-left: 10px;"></asp:Label>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="pnlApprovedBy" runat="server" Visible="false">
                                    <div class="col-xs-4 col-sm-4 text-center">
                                        <asp:Image ID="imgApprovedBySignature" Style="width: 100px; height: 50px;" runat="server" /><br />
                                        <%--<asp:Label runat="server" ID="lblApprovedByName"></asp:Label><br />--%>
                                        <asp:Label runat="server" ID="lblApprovedByDesignation"></asp:Label><br />
                                        <asp:Label runat="server" ID="lblApprovedDate"></asp:Label><br />
                                        <b id="lblApprovalText" runat="server"></b>
                                        <hr style="padding-left: 10px; padding-right: 10px;" />
                                        <strong>REMARKS</strong><br />
                                        <asp:Label runat="server" ID="lblApprovalRemarks" CssClass="text-left" Style="padding-left: 10px;"></asp:Label>
                                    </div>
                                </asp:Panel>

                            </div>
                        </div>
                        <div class="box-footer no-print">

                            <%--<asp:Button runat="server" ID="btnPrint"  Text="Print PO" CssClass="btn btn-success" OnClick="btnPrint_Click" />--%>
                            <asp:Button runat="server" ID="btnPrint" Text="Print PO" CssClass="btn btn-success" OnClientClick="printPage()" Visible="false" />
                            <asp:Button ID="btnDownload" runat="server" Text="Send Email" class="btn btn-primary pull-right" Style="margin-left: 10px;" OnClick="btnDownload_Click" OnClientClick="disable();"></asp:Button>
                            <%--<asp:Button ID="ss" runat="server" Text="ss" class="btn btn-danger pull-right"  Style="margin-left: 10px;" OnClick="ss_Click"   ></asp:Button>--%>

                            <%--<div id="printerDiv"  style="display:none"></div>--%>
                        </div>
                    </div>



                    <%-- </div> --%>
                </section>
                <asp:Button ID="btnEmail" runat="server" OnClick="btnDownload_Click" CssClass="hidden" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
    <script type="text/jscript">
        function printPage() {
            window.print();
        }

        function disable() {


            document.getElementById('<%= btnDownload.ClientID %>').disabled = true;
            $('#ContentSection_btnEmail').click();


        }
    </script>

</asp:Content>
