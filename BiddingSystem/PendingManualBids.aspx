<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="PendingManualBids.aspx.cs" Inherits="BiddingSystem.PendingManualBids" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">
    <style type="text/css">
        .ChildGrid > tbody > tr > td:not(table) {
            color: black;
            font-size: 10pt;
            line-height: 200%;
            text-align: center;
            border-bottom: 1px solid #d4d2d2;
        }

        .ChildGrid th {
            color: White;
            font-size: 10pt;
            line-height: 200%;
            padding-top: 12px;
            padding-bottom: 12px;
            text-align: center;
            background-color: #67778e !important;
            color: white;
        }
    </style>

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
    <script src="AdminResources/js/jquery.min.js" type="text/javascript"></script>
    <form runat="server">
        <section class="content-header">
    <h1>
       Monitor Bids
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="AdminDashboard.aspx"><i class="fa fa-home"></i> Home</a></li>
        <li class="active">Monitor Bids</li>
      </ol>
    </section>
        <br />
        <section class="content">
      <div class="box box-info" id="panelPurchaseRequset" runat="server">
        <div class="box-header with-border">
          <h3 class="box-title" >In-Progress Bids</h3>

          <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
            <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
          </div>
        </div>
        <!-- /.box-header -->
         <div class="panel-body">
    <div class="co-md-12">
    <div class="table-responsive">
        <asp:GridView ID="gvBids" runat="server" CssClass="table table-responsive"
                                                    OnRowDataBound="gvBids_RowDataBound" GridLines="None"
                                                    AutoGenerateColumns="false" DataKeyNames="BidId" HeaderStyle-BackColor="#3C8DBC" HeaderStyle-ForeColor="White">
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <img alt="" style="cursor: pointer;margin-top: -6px;"
                                                                    src="images/plus.png" />
                                                                <asp:Panel ID="pnlBidItems" runat="server" Style="display: none">
                                                                    <asp:GridView ID="gvBidItems" runat="server"
                                                                        CssClass="table table-responsive ChildGrid"
                                                                        GridLines="None" AutoGenerateColumns="false"
                                                                        Caption="Items in Bid">
                                                                        <Columns>
                                                                            <asp:BoundField DataField="BiddingItemId"
                                                                                HeaderText="BidItemId"
                                                                                HeaderStyle-CssClass="hidden"
                                                                                ItemStyle-CssClass="hidden" />
                                                                            <asp:BoundField DataField="BidId"
                                                                                HeaderText="BidItemId"
                                                                                HeaderStyle-CssClass="hidden"
                                                                                ItemStyle-CssClass="hidden" />
                                                                            <asp:BoundField DataField="PrdId"
                                                                                HeaderText="PRDId" HeaderStyle-CssClass="hidden"
                                                                                ItemStyle-CssClass="hidden" />
                                                                            <asp:BoundField DataField="CategoryId"
                                                                                HeaderText="Item Id"
                                                                                HeaderStyle-CssClass="hidden"
                                                                                ItemStyle-CssClass="hidden" />
                                                                            <asp:BoundField DataField="CategoryName"
                                                                                HeaderText="Category Name" />
                                                                            <asp:BoundField DataField="SubCategoryId"
                                                                                HeaderText="Item Id"
                                                                                HeaderStyle-CssClass="hidden"
                                                                                ItemStyle-CssClass="hidden" />
                                                                            <asp:BoundField DataField="SubCategoryName"
                                                                                HeaderText="Sub-Category Name" />
                                                                            <asp:BoundField DataField="ItemId"
                                                                                HeaderText="Item Id"
                                                                                HeaderStyle-CssClass="hidden"
                                                                                ItemStyle-CssClass="hidden" />
                                                                            <asp:BoundField DataField="ItemName"
                                                                                HeaderText="Item Name" />
                                                                            <%--<asp:BoundField DataField="Qty" HeaderText="Quantity" />--%>
                                                                            <asp:TemplateField HeaderText="Quantity">
                                                                                    <ItemTemplate>
                                                                                        <asp:label ID="lblInventory"  type="text" runat="server" Text='<%# decimal.Parse(Eval("Qty").ToString()).ToString() + " " + Eval("UnitShortName").ToString() %>'></asp:label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            <asp:BoundField DataField="EstimatedPrice"
                                                                                HeaderText="Estimated Price (Unit)" />
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </asp:Panel>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="BidId" HeaderText="BidId"
                                                            HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                        <asp:BoundField DataField="PrId" HeaderText="PrId"
                                                            HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                        <asp:TemplateField HeaderText="PR Code">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" Text='<%# "PR-"+Eval("PrCode").ToString() %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Bid Code"
                                                            HeaderStyle-HorizontalAlign="Center"
                                                            ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" Text='<%# "B"+Eval("BidCode").ToString() %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Department">
			                                            <ItemTemplate>
				                                            <asp:Label runat="server" ID="lbldepartmentName" Text='<%# Eval("MRNRefNumber")== null || Eval("MRNRefNumber").ToString() == "" || Eval("MRNRefNumber").ToString() == "0"  ?"Stores":Eval("subDepartmentName") %>'></asp:Label>
			                                            </ItemTemplate>
			                                            </asp:TemplateField>
                                                        <asp:BoundField DataField="ItemName" HeaderText="Items" />
                                                        <asp:BoundField DataField="CreateDate" HeaderText="Created Date"
                                                             DataFormatString='<%$ appSettings:datePattern %>' />
                                                        <asp:BoundField DataField="StartDate" HeaderText="Start Date" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"
                                                             DataFormatString='<%$ appSettings:datePattern %>' />
                                                        <asp:BoundField DataField="EndDate" HeaderText="End Date"
                                                             DataFormatString='<%$ appSettings:datePattern %>'/>
                                                        <asp:TemplateField HeaderText="Bid Opened For">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" Text='<%# Eval("BidOpeningPeriod").ToString()+" Days" %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Bid Type">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" Text='<%# Eval("BidOpenType").ToString() =="1" ? "Online":Eval("BidOpenType").ToString() =="2" ? "Manual":"Online & Manual" %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="ParticipatedCount" HeaderText="Participants" 
                                                            HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" 
                                                            ItemStyle-HorizontalAlign="Center"/>
                                                        <asp:BoundField DataField="SubmittedQuotatiionsCount" HeaderText="No Of Quotation Submitted" 
                                                            HeaderStyle-HorizontalAlign="Center"
                                                            ItemStyle-HorizontalAlign="Center"/>
                                                        <%--<asp:BoundField DataField="PendingQuotatiionsCount" HeaderText="Pending Quotations" 
                                                            HeaderStyle-HorizontalAlign="Center"
                                                            ItemStyle-HorizontalAlign="Center"/>--%>
                                                        <asp:TemplateField HeaderText="Purchasing Type">
                                    <ItemTemplate>
                                        <asp:Label
                                            runat="server"
                                            Visible='<%# Eval("PurchaseType").ToString() == "1" ? true : false %>'
                                            Text="Local" CssClass="label label-warning"/>
                                        <asp:Label
                                            runat="server"
                                            Visible='<%# Eval("PurchaseType").ToString() == "2" ? true : false %>'
                                            Text="Import" CssClass="label label-success"/>
                                    </ItemTemplate>
                                </asp:TemplateField>    
                                                                    <asp:TemplateField HeaderText="Action">
                                                                        <ItemTemplate>
                                                                            <asp:Button CssClass="btn btn-sm btn-warning" runat="server" ID="btnExpire" style="margin-top:5px; width:80px;"
                                                                                Text="Expire"  OnClientClick='<%#"expire(event,"+Eval("BidId").ToString()+")" %>' ></asp:Button>
                                                                        <asp:Button CssClass="btn btn-sm btn-danger" style="margin-top:5px; width:80px;" runat="server" OnClientClick='<%#"Terminate(event,"+Eval("BidId").ToString()+")" %>'
                                                                                Text="Terminate"></asp:Button>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
    </div>
    </div>
    </div>
        <!-- /.box-body -->
      </div>

    <%--*******************--%>
    </section>
        <%-- ************* --%>
        <asp:Button ID="btnTerminateBid" runat="server" OnClick="btnTerminate_Click" CssClass="hidden" />
        <asp:Button ID="btnExpire1" runat="server" OnClick="btnExpire_Click" CssClass="hidden" />
        <asp:HiddenField ID="hdnBidId" runat="server" />
        <asp:HiddenField ID="hdnRemarks" runat="server" />
    </form>

    <script type="text/javascript">

        function Terminate(e, bidId) {
            e.preventDefault();
            $('#ContentSection_hdnBidId').val(bidId);

            swal.fire({
                title: 'Are you sure?',
                html: "Are you sure you want Terminate this Bid?</br></br>"
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
                    $('#ContentSection_btnTerminateBid').click();
                }
            });

        }

        function expire(e, bidId) {
            e.preventDefault();
            $('#ContentSection_hdnBidId').val(bidId);

            swal.fire({
                title: 'Are you sure?',
                html: "Are you sure you want Expire this Bid?</br></br>",

                type: 'warning',
                cancelButtonColor: '#d33',
                showCancelButton: true,
                showConfirmButton: true,
                confirmButtonText: 'Yes',
                cancelButtonText: 'No',
                allowOutsideClick: false,

            }
            ).then((result) => {
                if (result.value) {
                    $('#ContentSection_btnExpire1').click();
                }
            });

        }


    </script>
</asp:Content>
