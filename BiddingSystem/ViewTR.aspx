<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="ViewTR.aspx.cs" Inherits="BiddingSystem.ViewTR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">



    <script src="AdminResources/js/jquery-1.10.2.min.js"></script>
    <section class="content-header">
    <h1>
      View Transfer Request Note
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="AdminDashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
        <li class="active">View Transfer Request Note </li>
      </ol>
    </section>
    <br />
    <section class="content" id="divPrintPo">


        <div class="container-fluid">
            <form runat="server" id="frm1">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="Updatepanel1" runat="server" >
                <ContentTemplate>



<!-- Start : Issue Notes Modal -->
                <div id="mdlViewIssueNote" class="modal fade" tabindex="-1" role="dialog" style="z-index: 3000;"
                    aria-hidden="true">
                    <div class="modal-dialog" style="width: 80%;">
                        <!-- Start : Modal Content -->
                        <div class="modal-content">
                            <!-- Start : Modal Header -->
                            <div class="modal-header" style="background-color: #3C8DBC; color: white">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="opacity: 1; color: white;">
                                    <span aria-hidden="true" style="opacity: 1;">×</span></button>
                                <h4 class="modal-title">ISSUE NOTES</h4>
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

                                         <asp:GridView ID="gvIssueNote" runat="server"
                                                                            CssClass="table table-responsive"
                                                                            GridLines="None" AutoGenerateColumns="false"
                                                                            EmptyDataText="No Issue Notes Found" RowStyle-BackColor="#f5f2f2" HeaderStyle-BackColor="#525252" HeaderStyle-ForeColor="White">
                                                                            <Columns>
                                                                   
                                                                     <asp:BoundField DataField="TRDInId"  HeaderText="TRDIn ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                                                     <asp:BoundField DataField="TRDId"  HeaderText="TRD ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                                                     <asp:BoundField DataField="IssuedQTY"  HeaderText="Issued QTY"/>
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
                                                                                    Text="Stock Delivered" CssClass="label label-info"/>
                                                                                <asp:Label
                                                                                    runat="server"
                                                                                    Visible='<%# Eval("Status").ToString() == "3" ? true : false %>'
                                                                                    Text="Stock Received" CssClass="label label-success"/>
                                                                     </ItemTemplate>
                                                               </asp:TemplateField>
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










                    

        <!-- Start : mrn Modal -->
                <div id="mdlLog" class="modal fade" tabindex="-1" role="dialog" style="z-index: 3000;"
                    aria-hidden="true">
                    <div class="modal-dialog" style="width: 60%;">
                        <!-- Start : Modal Content -->
                        <div class="modal-content">
                            <!-- Start : Modal Header -->
                            <div class="modal-header" style="background-color: #3C8DBC; color: white">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="opacity: 1; color: white;">
                                    <span aria-hidden="true" style="opacity: 1;">×</span></button>
                                <h4 class="modal-title">Actions Log</h4>
                            </div>
                            <!-- End : Modal Header -->
                            <!-- Start : Modal Body -->
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-xs-12">
                                        <div class="table-responsive">
                                            <asp:GridView ID="gvStatusLog"
                                                runat="server"
                                                CssClass="table table-responsive"
                                                GridLines="None"
                                                AutoGenerateColumns="false"
                                                EmptyDataText="No Log Found" HeaderStyle-BackColor="#275591"  HeaderStyle-ForeColor="White">
                                                <Columns>
                                                    <asp:BoundField
                                                        DataField="FirstName"
                                                        HeaderText="Logged By" />
                                                    <asp:BoundField
                                                        DataField="LoggedDate"
                                                        HeaderText="Logged Date and Time" />
                                                    <asp:TemplateField  HeaderText="Current Status">
                                                        <ItemTemplate>
                                                            <asp:Label
                                                                runat="server"
                                                                Visible='<%# Eval("Status").ToString() == "0" ? true : false %>'
                                                                Text="TR CREATED" CssClass="label label-info"/>
                                                            <asp:Label
                                                                runat="server"
                                                                Visible='<%# Eval("Status").ToString() == "1" ? true : false %>'
                                                                Text="TR APPROVED" CssClass="label label-success" />
                                                            <asp:Label
                                                                runat="server"
                                                                Visible='<%# Eval("Status").ToString() == "2" ? true : false %>'
                                                                Text="TR REJECTED" CssClass="label label-danger" />
                                                            <asp:Label
                                                                runat="server"
                                                                Visible='<%# Eval("Status").ToString() == "3" ? true : false %>'
                                                                Text="TR MODIFIED" CssClass="label label-warning" />
                                                            <asp:Label
                                                                runat="server"
                                                                Visible='<%# Eval("Status").ToString() == "4" ? true : false %>'
                                                                Text="TR TERMINATED" CssClass="label label-danger" />
                                                             <asp:Label
                                                                runat="server"
                                                                Visible='<%# Eval("Status").ToString() == "6" ? true : false %>'
                                                                Text="ADDED TO PR" CssClass="label label-info" />
                                                             <asp:Label
                                                                runat="server"
                                                                Visible='<%# Eval("Status").ToString() == "7" ? true : false %>'
                                                                Text="ISSUED FROM STOCK" CssClass="label label-success" />
                                                            <asp:Label
                                                                runat="server"
                                                                Visible='<%# Eval("Status").ToString() == "8" ? true : false %>'
                                                                Text="RECEIVED" CssClass="label label-success" />
                                                            
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!-- End : Modal Body -->
                        </div>
                        <!-- End : Modal Content -->
                    </div>
                </div>
        <div id="mdlTerminationDetails" class="modal fade" tabindex="-1" role="dialog" style="z-index: 3000;"
                    aria-hidden="true">
                    <div class="modal-dialog" style="width: 40%;">
                        <!-- Start : Modal Content -->
                        <div class="modal-content">
                            <!-- Start : Modal Header -->
                            <div class="modal-header" style="background-color: #3C8DBC; color: white">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="opacity: 1; color: white;">
                                    <span aria-hidden="true" style="opacity: 1;">×</span></button>
                                <h4 class="modal-title">Termination Details</h4>
                            </div>
                            <!-- End : Modal Header -->
                            <!-- Start : Modal Body -->
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-xs-12 text-center">
                                        <asp:Image ID="imgTRdTerminatedBySignature" Style="width: 100px; height:50px;" runat="server" /><br />
                                        <asp:Label runat="server" ID="trdTerminatedByName"></asp:Label><br />
                                        <asp:Label runat="server" ID="trdTerminatedDate"></asp:Label><br />
                                        <b>Terminated By</b>
                                        <hr style="padding-left:10px; padding-right:10px;" />
                                        <strong>REMARKS</strong><br />
                                        <asp:Label runat="server" ID="lblTRdTerminationRemarks" CssClass="text-left" style="padding-left:10px;"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <!-- End : Modal Body -->
                        </div>
                        <!-- End : Modal Content -->
                    </div>
                </div>


                    <div class="box box-info">
                        <div class="box-header">
                            <h3 class="text-center"><i class="fa fa-file-invoice"></i>&nbsp;&nbsp;&nbsp;Transfer Request Note</h3>
                            <hr>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-xs-4">
                                    <strong>FROM WAREHOUSE: </strong>
                                    <br>
                                    <asp:Label runat="server" ID="lblFromWarehouseName"></asp:Label><br>
                                     <asp:Label runat="server" ID="lbiFromWarehouseAddress"></asp:Label><br>
                                    <asp:Label runat="server" ID="lblFromWarehouseContact"></asp:Label>
                                </div>
                                <div class="col-xs-4">
                                    <strong>TO WAREHOUSE: </strong>
                                    <br>
                                    <asp:Label runat="server" ID="lblWarehouseName"></asp:Label><br>
                                    <asp:Label runat="server" ID="lblWarehouseAddress"></asp:Label><br>
                                    <asp:Label runat="server" ID="lblWarehouseContact"></asp:Label>
                                </div>
                                <div class="col-xs-4">
                                    <strong>TR CODE: </strong>
                                    <asp:Label runat="server" ID="lbltrCode"></asp:Label><br>
                                     <strong>EXPECTED DATE: </strong>
                                    <asp:Label runat="server" ID="lblExpectedDate"></asp:Label><br>
                                    <strong>APPROVAL STATUS: </strong>
                                    <asp:Label runat="server" ID="lblPending" CssClass="label label-warning" Visible="false" Text="Pending"></asp:Label>
                                    <asp:Label runat="server" ID="lblApproved" CssClass="label label-success" Visible="false" Text="Approved"></asp:Label>
                                    <asp:Label runat="server" ID="lblRejected" CssClass="label label-danger" Visible="false" Text="Rejected"></asp:Label><br>
                                    <strong>TR STATUS: </strong>
                                    <asp:Label runat="server" ID="lblCompletionPending" CssClass="label label-warning" Visible="false" Text="Completion Pending"></asp:Label>
                                    <asp:Label runat="server" ID="lblComplete" CssClass="label label-success" Visible="false" Text="Completed"></asp:Label>
                                    <asp:Label runat="server" ID="lblTerminated" CssClass="label label-danger" Visible="false" Text="Terminated"></asp:Label><br>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <br />
                                    <br />
                                    <div class="table-responsive">
                                        <asp:GridView runat="server" ID="gvTRItems" AutoGenerateColumns="false"
                                            CssClass="table table-responsive" HeaderStyle-BackColor="LightGray" BorderColor="LightGray" EnableViewState="true">
                                            <Columns>
                                                <asp:BoundField DataField="TRDId" HeaderText="TRD ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />  
                                                <asp:BoundField DataField="CategoryName" HeaderText="Category" />
                                                <asp:BoundField DataField="SubCategoryName" HeaderText="Sub-Category" />
                                                <asp:BoundField DataField="ItemName" HeaderText="Item Name" />
                                                <asp:BoundField DataField="RequestedQty" HeaderText="Requested QTY" />
                                                <asp:BoundField DataField="ShortName" HeaderText="Unit" NullDisplayText="Not Found" />
                                               <asp:BoundField DataField="Description" HeaderText="Description" />
                                                <asp:BoundField DataField="ReceivedQty"
                                                    HeaderText="Received Quantity" />
                                                <asp:BoundField DataField="IssuedQty"
                                                    HeaderText="Issued Quantity" />
                                                <asp:TemplateField HeaderText="Status">
                                                <ItemTemplate>
                                                    <asp:Label
                                                        runat="server"
                                                        Visible='<%# Eval("Status").ToString() == "0" ? true : false %>'
                                                        Text="Pending" CssClass="label label-warning"/>
                                                    <asp:Label
                                                        runat="server"
                                                        Visible='<%# Eval("Status").ToString() == "1" ? true : false %>'
                                                        Text="Added to PR" CssClass="label label-success"/>
                                                    <asp:Label
                                                        runat="server"
                                                        Visible='<%# Eval("Status").ToString() == "2" ? true : false %>'
                                                        Text="Partially Issued" CssClass="label label-info"/>
                                                    <asp:Label
                                                        runat="server"
                                                        Visible='<%# Eval("Status").ToString() == "3" ? true : false %>'
                                                        Text="Fully Issued" CssClass="label label-success"/>
                                                    <asp:Label
                                                        runat="server"
                                                        Visible='<%# Eval("Status").ToString() == "4" ? true : false %>'
                                                        Text="Delivered" CssClass="label label-success"/>
                                                    <asp:Label
                                                        runat="server"
                                                        Visible='<%# Eval("Status").ToString() == "5" ? true : false %>'
                                                        Text="Received" CssClass="label label-success"/>
                                                    <asp:Label
                                                        runat="server"
                                                        Visible='<%# Eval("Status").ToString() == "6" ? true : false %>'
                                                        Text="Terminated" CssClass="label label-danger"/>
                                                                                           
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                                
                                            <asp:TemplateField HeaderText="Action" HeaderStyle-CssClass="no-print" ItemStyle-CssClass="no-print">
                                                <ItemTemplate>
                                                    <asp:LinkButton Visible='<%# Eval("Status").ToString() == "6" ? true : false %>' ForeColor="Red"  runat="server" ID="lbtnMore" ToolTip="Termination Details" OnClick="lbtnMore_Click" CssClass="btn "><i class="fa fa-info-circle"></i></asp:LinkButton>
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
                                <div class="col-xs-4 col-sm-4 text-center">
                                    <asp:Image ID="imgCreatedBySignature" Style="width: 100px; height:50px;" runat="server" /><br />
                                    <asp:Label runat="server" ID="lblCreatedByName"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblCreatedDate"></asp:Label><br />
                                    <b>TR Created By</b>
                                    <hr style="padding-left:10px; padding-right:10px;" />
                                </div>
                                <asp:Panel ID="pnlApprovedByDetails" runat="server" Visible="false">
                                    <div class="col-xs-4 col-sm-4 text-center">
                                        <asp:Image ID="imgApprovedBySignature" Style="width: 100px; height:50px;" runat="server" /><br />
                                        <asp:Label runat="server" ID="lblApprovedByName"></asp:Label><br />
                                        <asp:Label runat="server" ID="lblApprovedDate"></asp:Label><br />
                                        <b id="lblApprovalText" runat="server"></b>
                                        <hr style="padding-left:10px; padding-right:10px;" />
                                        <strong>REMARKS</strong><br />
                                        <asp:Label runat="server" ID="lblRemark" CssClass="text-left" style="padding-left:10px;"></asp:Label>
                                    
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
                            <asp:Button runat="server" ID="btnPrint"  Text="Print TR" CssClass="btn btn-success" OnClientClick="printPage()" />
                            <asp:Button runat="server" ID="btnModify" CssClass="btn btn-warning" Text="Edit TR" OnClick="btnModify_Click" />
                             <asp:Button runat="server" ID="btnClone" CssClass="btn btn-info" Text="Clone" OnClick="btnClone_Click" />
                             <asp:Button runat="server" ID="btnTerminateTR" CssClass="btn btn-danger" Text="Terminate" OnClientClick="terminateTR(event)" />
                             </div>
                    </div>


                    
                <asp:Button ID="btnTerminate" runat="server" OnClick="btnTerminate_Click" CssClass="hidden" />
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

        function terminateTR(e) {
            e.preventDefault();

            swal.fire({
                title: 'Are you sure?',
                html: "Are you sure you want Terminate this TR?</br></br>"
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
    </script>

</asp:Content>
