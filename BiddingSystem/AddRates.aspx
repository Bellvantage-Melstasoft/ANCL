<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="AddRates.aspx.cs"
    Inherits="BiddingSystem.AddRates" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">
    <style>
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
            .margin{
                margin-right: 5px;
            }
    </style>
    <section class="content-header">
      <h1>
       Add Rates
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="AdminDashboard.aspx"><i class="fa fa-home"></i> Home</a></li>
        <li class="active">Add Rates</li>
      </ol>
    </section>
    <br />
    <form id="form1" runat="server">



        <asp:ScriptManager runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">

            <ContentTemplate>

                <div class="box box-info">
                     <div class="box-header with-border">
                        <h3 class="box-title">Add Country</h3>

                    </div>

                    <div class="box-body">
                        <div class="row" style="padding-bottom: 40px;">
                            <div class="col-md-4">
                                 <asp:Label runat="server" Text="Country Name"></asp:Label>
                                <asp:TextBox ID="txtCountry" runat="server" CssClass="form-control"></asp:TextBox>
                             </div>
                            <div class="col-md-4">
                                 <asp:Button ID="btnCountry" runat="server" Text="Save" CssClass="btn btn-primary pull-right" Style="margin-right: 15px; margin-top: 5px;" OnClick="btnCountry_Click"></asp:Button>
                             </div>
                             </div>

                        <div class="row" style="padding-top: 5px; padding-bottom: 40px;">
                            <div class="col-md-12">
                                <div class="table-responsive">
                                    <asp:GridView ID="gvCountry" runat="server" CssClass="table table-responsive tablegv" GridLines="None" OnPageIndexChanging="gvCountry_PageIndexChanging" 
                                        PageSize="10" AllowPaging="true" AutoGenerateColumns="False">
                                        <Columns>
                                            <asp:BoundField DataField="Id" HeaderText="Currency Name"  HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"></asp:BoundField>
                                            <asp:BoundField DataField="Name" HeaderText="CountryName"></asp:BoundField>
                                           
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                         </div>
                     </div>



                <div class="box box-info">
                    <div class="box-header with-border">
                        <h3 class="box-title">Add Currency Type</h3>

                    </div>
                    <div class="box-body">
                        <div class="row" style="padding-bottom: 40px;">
                            <div class="col-md-4">
                                <asp:Label runat="server" Text="Currency Name"></asp:Label>
                                <asp:TextBox ID="txtCurrencyName" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>

                            <div class="col-md-4">
                                <asp:Label runat="server" Text="Currency Short Name"></asp:Label>
                                <asp:TextBox ID="txtCurrencyShortName" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>

                            <div class="col-md-4" style="margin-top: 15px;">
                                <asp:Button ID="btnSaveCurrencyType" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnSaveCurrencyType_Click" Style="margin-top: 5px;"></asp:Button>
                            </div>
                        </div>

                        <div class="box-header with-border">
                            <h3 class="box-title">Add Currency Rates</h3>
                        </div>
                        <div class="row" style="padding-top: 10px; padding-bottom: 40px;">
                            <div class="col-md-3">
                                <asp:Label runat="server" Text="Currency Type"></asp:Label>
                                <asp:DropDownList ID="ddlCurrencyType" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </div>

                            <div class="col-md-3">
                                <asp:Label runat="server" Text="Effective Date"></asp:Label>
                                <asp:TextBox ID="TxtEffectiveDate" type="date" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>

                            <div class="col-md-3">
                                <asp:Label runat="server" Text="Buying Rate"></asp:Label>
                                <asp:TextBox ID="txtBuyingRate" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>

                            <div class="col-md-3">
                                <asp:Label runat="server" Text="Selling Rate"></asp:Label>
                                <asp:TextBox ID="txtSellingRate" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>


                            <div class="row pull-right">
                                <div class="col-md-12" style="margin-right: 10px;">
                                    <asp:Button ID="btnSaveCurrencyRates" runat="server" Text="Save" CssClass="btn btn-primary pull-right" Style="margin-right: 15px; margin-top: 5px;" OnClick="btnSaveCurrencyRates_Click"></asp:Button>
                                </div>
                            </div>
                        </div>

                        <div class="row" style="padding-top: 5px; padding-bottom: 40px;">
                            <div class="col-md-12">
                                <div class="table-responsive">
                                    <asp:GridView ID="gvCurrency" runat="server" CssClass="table table-responsive tablegv" GridLines="None" OnPageIndexChanging="gvCurrency_PageIndexChanging" 
                                        PageSize="10" AllowPaging="true" AutoGenerateColumns="False">
                                        <Columns>
                                            <asp:BoundField DataField="CurrentcyName" HeaderText="Currency Name"></asp:BoundField>
                                            <asp:BoundField DataField="CurrentcyShortName" HeaderText="Currency Short Name"></asp:BoundField>
                                            <asp:TemplateField HeaderText="Effective Date">
                                                <ItemTemplate>
                                                    <%# (DateTime)Eval("Date") == DateTime.MinValue ? "Not Found" : string.Format("{0:MM-dd-yyyy}", (DateTime)Eval("Date")) %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="BuyingRate" HeaderText="Buying Rate" NullDisplayText="Not found"></asp:BoundField>
                                            <asp:BoundField DataField="SellingRate" HeaderText="Selling Rate" NullDisplayText="Not found"></asp:BoundField>

                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>

                        <div class="box-header with-border">
                            <h3 class="box-title">Duty Rates</h3>
                        </div>
                        <div class="row" >
                            <div class="col-sm-12">
                            <div class="col-md-3">
                                <div class="form-group">
                                <label class="control-label">
                                    Select File
                                </label>
                                    <asp:FileUpload ID="FileUpload1" runat="server" />
                            </div>
                                <div class="form-group">
                                   <asp:Button ID="btnUpload" CssClass="btn btn-primary" Width="100px" runat="server"
                                                Text="Upload" ValidationGroup="btnUpload" OnClick="btnUpload_Click" />
                                 </div>
                                 </div>
                            <div class="col-md-3">
                                
                                 </div>
                             </div>
                             </div>
                        <div class="row" >
                         <div class="col-sm-12">
                            <div class="form-group">
                                <asp:Label ID="lblMessage" runat="server" Font-Bold="True"></asp:Label>
                            </div>
                        </div>
                            </div>
                        <div class="row" style="padding-top: 10px;">
                            <div class="row" style="padding-left: 15px; margin-bottom: 10px">
                                <div class="col-md-3">
                                    <asp:Label runat="server" Text="HS Code"></asp:Label>
                                    <asp:DropDownList ID="ddlHsCode" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlHsCode_SelectedIndexChanged" ></asp:DropDownList>
                                </div>
                                <div class="col-md-3">
                                    <asp:Label runat="server" Text="HS Code"></asp:Label>
                                     <asp:TextBox ID="txtHsCode" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <asp:Label runat="server" Text="HS Code Name"></asp:Label>
                                    <asp:TextBox ID="txtHsCodeName" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <asp:Label runat="server" Text="XID Rate"></asp:Label>
                                <asp:TextBox ID="txtExerciseRate" runat="server" CssClass="form-control"></asp:TextBox>

                            </div>

                            <div class="col-md-3">
                                <asp:Label runat="server" Text="CID Rate"></asp:Label>
                                <asp:TextBox ID="txtDutyRate" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>

                            <div class="col-md-3">
                                <asp:Label runat="server" Text="PAL Rate"></asp:Label>
                                <asp:TextBox ID="txtPalRate" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>

                            <div class="col-md-3">
                                <asp:Label runat="server" Text="EIC Rate"></asp:Label>
                                <asp:TextBox ID="txtCessRate" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>

                        </div>
                        <div class="row pull-right" style="margin-right: 5px; margin-top: 5px; margin-bottom: 5px">
                            <asp:Button ID="btnDuty" runat="server" Text="Save" CssClass="btn btn-primary pull-right margin" OnClick="btnDuty_Click"></asp:Button>
                            <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-danger pull-right margin" OnClick="btnClear_Click"></asp:Button>

                        </div>
                        <!-- -->
                          <div class="row" style="padding-top: 5px; padding-bottom: 40px;">
                            <div class="col-md-12">
                                <div class="table-responsive">
                                    <asp:GridView ID="gvHSRates" runat="server" CssClass="table table-responsive tablegv" GridLines="None" 
                                        OnPageIndexChanging="gvHSRates_PageIndexChanging" 
                                        PageSize="10" AllowPaging="true" AutoGenerateColumns="False">
                                        <Columns>
                                            <asp:BoundField DataField="HsId" HeaderText="HS Code"></asp:BoundField>
                                             <asp:BoundField DataField="HsIdName" HeaderText="HS Code Name"></asp:BoundField>
                                            <asp:BoundField DataField="XID" HeaderText="XID Rate"></asp:BoundField>
                                            <asp:BoundField DataField="CID" HeaderText="CID Rate"></asp:BoundField>
                                            <asp:BoundField DataField="PAL" HeaderText="PAL Rate"></asp:BoundField>
                                            <asp:BoundField DataField="EIC" HeaderText="EIC Rate"></asp:BoundField>
                                            <asp:TemplateField HeaderText="Date">
                                                <ItemTemplate>
                                                    <%# (DateTime)Eval("Date") == DateTime.MinValue ? "Not Found" : string.Format("{0:MM-dd-yyyy}", (DateTime)Eval("Date")) %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>


                    </div>
                </div>
                </section>
            </ContentTemplate>
            <Triggers>
               <asp:PostBackTrigger ControlID="btnUpload" />
            </Triggers>
        </asp:UpdatePanel>

    </form>
    <script src="AdminResources/js/jquery1.8.min.js"></script>
    <link href="AdminResources/css/formStyleSheet.css" rel="stylesheet" />
    <script src="AdminResources/js/jquery-1.10.2.min.js"></script>
    <script src="AdminResources/js/autoCompleter.js"></script>

    <link href="AdminResources/css/htmldatecss.css" rel="stylesheet" />
    <script src="AdminResources/js/moment.min.js"></script>

    <script type="text/javascript">
</script>

</asp:Content>
