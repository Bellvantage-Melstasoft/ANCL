<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="CreatePO.aspx.cs" Inherits="BiddingSystem.CreatePO" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server" ViewStateMode="Enabled">

    <style type="text/css">
        .ChildGrid>tbody>tr>td:not(table) {
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

        .ChildGridTwo>tbody>tr>td:not(table) {
            background-color: #f5f5f5 !important;
            color: black;
            border-bottom: 1px solid #d4d2d2;
        }


        .ChildGridTwo>tbody>tr {
            border: 1px solid #d4d2d2;
        }

        .ChildGridTwo th {
            color: white;
            font-size: 10pt;
            line-height: 200%;
            padding-top: 12px;
            padding-bottom: 12px;
            text-align: center;
            background-color: #808080 !important;
        }

        .ChildGridThree td {
            text-align: left;
        }

        input[type=number]::-webkit-inner-spin-button,
        input[type=number]::-webkit-outer-spin-button {
            -webkit-appearance: none;
            margin: 0;
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

    <link href="AdminResources/css/formStyleSheet.css" rel="stylesheet" />
    <script src="AdminResources/js/jquery-1.10.2.min.js"></script>
    <link href="AppResources/css/jquery-ui.css" rel="stylesheet" type="text/css" />

    <section class="content-header">
        <h1>
            Create Purchase Order
            <small></small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="AdminDashboard.aspx"><i class="fa fa-home"></i> Home</a></li>
            <li class="active">Create Purchase Order </li>
        </ol>
    </section>
    <br />

    <form runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="Updatepanel1" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
            <ContentTemplate>
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
                                                <asp:TextBox TextMode="MultiLine" Rows="10"  ID="txtTermsAndConditions" Enabled="false"  runat="server" CssClass="form-control text-bold"></asp:TextBox>
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
                <!-- Start : Section -->
                <section class="content" style="padding-top:0px">
                    <div class="row">
                        <div class="col-xs-12">
                            <!-- Start : Box -->
                            <div class="box box-info">
                                <!-- Start : Box Header-->
                                <div class="box-header with-border">
                                    <h3 class="box-title">Purchase Request Details</h3>
                                </div>
                                <!-- End : Box Header -->
                                <!-- Start : Box Body-->
                                <div class="box-body">
                            <div class="row">
                                <div class="col-md-4 col-sm-6 col-xs-12">
                                    <address>
                                        <strong>PR No : </strong>
                                        <asp:Label ID="lblPRNo" runat="server" Text=""></asp:Label><br />
                                        <strong>Created On : </strong>
                                        <asp:Label ID="lblCreatedOn" runat="server" Text=""></asp:Label><br />
                                        <strong>Created By : </strong>
                                        <asp:Label ID="lblCreatedBy" runat="server" Text=""></asp:Label><br />
                                    </address>
                                </div>
                                <div class="col-md-4 col-sm-6 col-xs-12">
                                    <address>
                                        <strong>Requested By : </strong>
                                        <asp:Label ID="lblRequestBy" runat="server" Text=""></asp:Label><br />
                                        <strong>Requested For : </strong>
                                        <asp:Label ID="lblRequestFor" runat="server" Text=""></asp:Label><br />
                                        <strong>Expense Type : </strong>
                                        <asp:Label ID="lblExpenseType" runat="server" Text=""></asp:Label><br />
                                    </address>
                                </div>
                                <div class="col-md-4 col-sm-6 col-xs-12">
                                    <address>
                                        <strong>Warehouse : </strong>
                                        <asp:Label ID="lblWarehouse" runat="server" Text=""></asp:Label><br /> 
                                        <strong>MRN No : </strong>
                                        <asp:Label ID="lblMrnId" runat="server" Text=""></asp:Label><br />   
                                        <strong>Department : </strong>
                                        <asp:Label ID="lblDepartment" runat="server" Text=""></asp:Label><br />                                      
                                    </address>
                                </div>
                            </div>
                                    <div class="row">
                                        <div class="col-xs-12">
                                            <div class="table-responsive">
                                                <asp:GridView ID="gvItems" runat="server" CssClass="table table-responsive"
                                                    GridLines="None" AutoGenerateColumns="false" Caption="Selected Items"
                                                    HeaderStyle-BackColor="#3C8DBC" HeaderStyle-ForeColor="White">
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="CheckBox1" runat="server" />
                                                            </ItemTemplate>
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="CheckBox2" runat="server" onclick="CheckBoxChecked(this);" />
                                                            </HeaderTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="TabulationDetailId" HeaderText="QuotationId"
                                                            HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                        <asp:BoundField DataField="TabulationId" HeaderText="BidId"
                                                            HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                        <asp:BoundField DataField="SupplierId" HeaderText="SupplierId"
                                                            HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                        <asp:BoundField DataField="SupplierName" HeaderText="Supplier Name"
                                                            ItemStyle-Font-Bold="true" />
                                                        <asp:BoundField DataField="QuotationId" HeaderText="QuotationId"
                                                            HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                        <asp:BoundField DataField="QuotationItemId" HeaderText="QuotationItemId"
                                                            HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                        <asp:BoundField DataField="ItemId" HeaderText="ItemId"
                                                            HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                          <asp:TemplateField HeaderText="Attachments">
                                                        <ItemTemplate>
                                                            <asp:Button CssClass="btn btn-xs btn-default" OnClick="btnViewAttachments_Click" runat="server"
                                                                Text="View"></asp:Button>
                                                        </ItemTemplate>
                                                      </asp:TemplateField>
                                                        <asp:BoundField DataField="ItemName" HeaderText="Item"
                                                            ItemStyle-Font-Bold="true" />
                                                        <asp:BoundField DataField="Description" HeaderText="Description"
                                                            ItemStyle-Font-Bold="true" />
                                                        <asp:BoundField DataField="Qty" HeaderText="Qty"
                                                            ItemStyle-Font-Bold="true" />
                                                        <asp:BoundField DataField="UnitPrice" HeaderText="Unit Price" DataFormatString="{0:N2}"
                                                            ItemStyle-Font-Bold="true" />
                                                        <asp:BoundField DataField="SubTotal" HeaderText="Total (Tax Exclusive)" DataFormatString="{0:N2}"
                                                            ItemStyle-Font-Bold="true" />
                                                        <asp:BoundField DataField="NbtAmount" HeaderText="NBT Amount" DataFormatString="{0:N2}"
                                                            ItemStyle-Font-Bold="true" />
                                                        <asp:BoundField DataField="VAtAmount" HeaderText="VAT Amount" DataFormatString="{0:N2}"
                                                            ItemStyle-Font-Bold="true" />
                                                        <asp:BoundField DataField="NetTotal" HeaderText="Total (Tax Inclusive)" DataFormatString="{0:N2}"
                                                            ItemStyle-Font-Bold="true" />
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- End : Box Body -->
                                <!-- Start : Box Footer -->
                                <div class="box-footer">

                                     <asp:Button runat="server" ID="btnCreatePO" CssClass="btn btn-info pull-right" Text="Create PO"
                                        OnClick="btnCreatePO_Click" style="margin-right:10px"></asp:Button>
                                   

                                </div>
                                <!-- End : Box Footer -->
                            </div>
                            <!-- End : Box -->
                        </div>
                    </div>
                </section>
                <!-- End : Section -->

               
            </ContentTemplate>
        </asp:UpdatePanel>

    </form>

    <script type="text/javascript">

        function CheckBoxChecked(CheckBox) {
            //get target base & child control.
            var TargetBaseControl = document.getElementById('<%= this.gvItems.ClientID %>');
            var TargetChildControl = "CheckBox1";

            //get all the control of the type INPUT in the base control.
            var Inputs = TargetBaseControl.getElementsByTagName("input");

            for (var n = 0; n < Inputs.length; ++n)
                if (Inputs[n].type == 'checkbox' && Inputs[n].id.indexOf(TargetChildControl, 0) >= 0) {
                    Inputs[n].checked = CheckBox.checked;
                }
                else {

                }
        }
        Sys.Application.add_load(function () {


            $(function () {
        

        $('.btnViewAttachmentsCl').on({
            click: function () {
                event.preventDefault();
                $('#mdlQuotations').modal('hide');

                var tableRow = $('#ContentSection_gvItems').find('> tbody > tr > td:not(table)');
                
                
                $('#ContentSection_btnViewAttachments').click();
            }
        });

    });
        });
    </script>

</asp:Content>