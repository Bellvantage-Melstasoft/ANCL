<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="CompanyViewApprovedPR.aspx.cs" Inherits="BiddingSystem.CompanyViewApprovedPR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">

    <script src="AdminResources/js/jquery1.8.min.js"></script>
   

<style type="text/css">
        /*body
        {
            font-family: Arial;
            font-size: 10pt;
        }*/
        .Grid td
        {
            background-color: #A1DCF2;
            color: black;
            font-size: 10pt;
            line-height:200%;
            border: 1px solid #ddd;
            text-align:center;
        }
        .Grid th
        {
            background-color: #3c8dbc;
            color: White;
            font-size: 10pt;
            line-height:200%;
            text-align :center;
            border: 1px solid #ddd;
            padding: 8px;
        }
        .ChildGrid td
        {
            background-color: #eee !important;
            color: black;
            font-size: 10pt;
            line-height:200%;
            text-align:center;
        }
        .ChildGrid th
        {
            color: White;
            font-size: 10pt;
            line-height:200%;
            padding-top: 12px;
            padding-bottom: 12px;
            text-align: center;
            background-color: #67778e !important;
            color: white;
        }
        .AlgRgh
        {
          text-align:center;
        }
    </style>
    <section class="content-header">
    <h1>
       Approved Bids
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="AdminDashboard.aspx"><i class="fa fa-home"></i>Home</a></li>
        <li class="active">Approved PR</li>
      </ol>
    </section>
    <br />
    <form id="form1" runat="server">
     <section class="content">
      <%-- ******************************** --%>
      <div class="box box-danger" id="Div1" runat="server">
        <div class="box-header with-border">
         <h3 class="box-title" >Approved PR</h3>
          <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
            <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
          </div>
        </div>
        <!-- /.box-header -->
        <div class="box-body">
          <div class="row">
            <div class="col-md-12">
       
            </div>         
          </div>
         
        </div>

     <div id="myModal2" class="modal modal-primary fade" tabindex="-1" role="dialog"  aria-hidden="true">
				  <div class="modal-dialog">
					<!-- Modal content-->
					<div class="modal-content" style="background-color:#a2bdcc;">
					  <div class="modal-header" style="background-color:#7bd47dfa;">
						<button type="button" class="close" data-dismiss="modal" aria-label="Close" style="opacity: 1;color:white; "><span aria-hidden="true" style="opacity: 1; ">×</span></button>		
						<h4 class="modal-title">BOM (Bill of Meterial)</h4>
					  </div>
					  <div class="modal-body">
						<div class="login-w3ls">
						<div class="row">
                  <div class="col-md-12">
                    <div class="table-responsive">
       <asp:GridView ID="gvBOMDate" runat="server" CssClass="table table-responsive TestTable" style="border-collapse:collapse;color:  black;"
        GridLines="None" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField DataField="PrId" HeaderText="PrId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:BoundField DataField="ItemId" HeaderText="ItemId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:BoundField DataField="SeqId" HeaderText="Seq_ID"/>
            <asp:BoundField DataField="Meterial" HeaderText="Material" />
            <asp:BoundField DataField="Description" HeaderText="Description" />
        </Columns>
    </asp:GridView>
    </div>               
    </div> 
     <div>
          <label id="Label1"  style="margin:3px; color:maroon; text-align:center;"></label>
     </div>
     </div>	
	 </div>
	 </div>
	 </div>
	 </div>
     </div>

        <!-- /.box-body -->
         <div class="box-footer">
         <div class="col-md-12">
        <div class="form-group">
              <label for="exampleInputEmail1" style="visibility:hidden">Raise PO</label>
            <%--  <button class="btn btn-primary pull-right">Raise PO</button>--%>
              <asp:GridView ID="gvPRMastre" runat="server" AutoGenerateColumns="false" CssClass="Grid table table-responsive"
        DataKeyNames="prId" OnRowDataBound="OnRowDataBound">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <img alt = "" class="plusMark" style="cursor: pointer" src="images/plus.png" />
                    <asp:Panel ID="pnlOrders" runat="server" Style="display: none">
                        <asp:GridView ID="gvPRDetails" runat="server" AutoGenerateColumns="false" CssClass = "ChildGrid">
                            <Columns>
                                <asp:BoundField ItemStyle-Width="150px" DataField="PrId" HeaderText="PrId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                <asp:BoundField ItemStyle-Width="150px" DataField="ItemId" HeaderText="ItemId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                <asp:BoundField ItemStyle-Width="150px" DataField="CategoryName" HeaderText="Category Name" />
                                <asp:BoundField ItemStyle-Width="150px" DataField="SubCategoryName" HeaderText="SubCategoryName" />
                                <asp:BoundField ItemStyle-Width="150px" DataField="ItemName" HeaderText="Name" />
                                <asp:BoundField ItemStyle-Width="150px" DataField="ItemDescription" HeaderText="Description" />
                                <asp:BoundField ItemStyle-Width="150px" DataField="ItemQuantity" HeaderText="Quantity"/>
                                <asp:BoundField ItemStyle-Width="150px" DataField="Purpose" HeaderText="Purpose" />
                            </Columns>
                        </asp:GridView>
                    </asp:Panel>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField  DataField="PrId" HeaderText="PrID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:BoundField  DataField="PrCode" HeaderText="PR CODE" />
            <asp:BoundField ItemStyle-Width="150px"  DataField="RequestedBy" HeaderText="RequestedBy" />
            <asp:BoundField ItemStyle-Width="150px"  DataField="DateOfRequest" HeaderText="DateOfRequest" />
            <asp:BoundField ItemStyle-Width="150px"  DataField="QuotationFor" HeaderText="QuotationFor" />
        </Columns>
    </asp:GridView>
          </div>
          </div>
        </div>
        
      </div>
      <br />
      <asp:Label ID="lblMsg" runat="server" Text="" style="color:Green;font-weight:bold;"></asp:Label>
      <asp:Label ID="lblError" runat="server" Text="" style="color:Red;font-weight:bold;"></asp:Label>
    </section>
    </form>

        <%--<script src="AdminResources/js/jquery.min.js" type="text/javascript"></script>--%>
     <script type="text/javascript">
        //$("[src*=plus]").live("click", function () {
        //    $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>")
        //    $(this).attr("src", "images/minus.png");
        //});
        //$("[src*=minus]").live("click", function () {
        //    $(this).attr("src", "images/plus.png");
        //    $(this).closest("tr").next().remove();
         //});

         $("[src*=plus]").click("click", function () {
             if ($(this).hasClass('plusMark')) {
                 $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>")
                 $(this).attr("src", "images/minus.png");
                 $(this).removeClass('plusMark')
                 $(this).addClass('minusMark')
             } else {
                 $(this).attr("src", "images/plus.png");
                 $(this).closest("tr").next().remove();
                 $(this).removeClass('minusMark')
                 $(this).addClass('plusMark')
             }
         });
    </script>
</asp:Content>
