<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="WarehouseIssuNote.aspx.cs" Inherits="BiddingSystem.WarehouseIssuNote" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">
  <style type="text/css">
      body{
                 
             }

      
@media print{
          body{
                
      }
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
    .tablegv tr:nth-child(even){background-color: #f2f2f2;}
    .tablegv tr:hover {background-color: #ddd;}
    .tablegv th {
        padding-top: 12px;
        padding-bottom: 12px;
        text-align: left;
        background-color: #3C8DBC;
        color: white;
    }
   
      .tbldetail tr {
      background-color:#fafafa;
      
      }
      /*th{

          background-color:lightgray;
      }*/
      
      /*@page{
          size:A4;
          margin:0;
          size:portrait;
          -webkit-print-color-adjust:exact !important
      }*/
     
      
</style>
    <style media="print">
 @page {
  size: auto;
  margin: 0;
       }
</style>

 <script src="AdminResources/js/jquery.min.js" type="text/javascript"></script>
<section class="content-header">
    <h1>
       WAREHOUSE ISSUE NOTE
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="AdminDashboard.aspx"><i class="fa fa-home"></i> Home</a></li>
        <li class="active">View Warehouse Issue Note</li>
      </ol>
    </section>
    <br />
     <form runat="server" id="form1">
     

   <div class="content" style="position: relative;background: #fff;overflow:hidden; border: 1px solid #f4f4f4;padding: 20px;margin: 10px 25px;" id="divPrintPo" >    <!-- Main content -->
   <div class="row">
        <div class="col-xs-12">
          <h2 class="page-header" style="text-align:center;">
            <i class="fa fa-envelope"></i> WAREHOUSE ISSUE NOTE<%--<% =reprint %>--%>
          </h2>
        </div>
        <!-- /.col -->
      </div>
    

      <div class="row invoice-info">

          <div class="col-sm-4 invoice-col">
          
  <address>

               <table >
             <tr>
                 <td>Company&nbsp;</td>
                   <td>:&nbsp;</td>
                   <td><b><asp:Label ID="lblDeptName" runat="server" Text=""></asp:Label></b></td>
             </tr>
             <tr>
                 <td>MRN Code:&nbsp;</td>
                   <td>:&nbsp;</td>
                   <td><b><asp:Label ID="lblMrnCode" runat="server" Text=""></asp:Label></b></td>
             </tr>
                     
         </table>

          </address>



     
        </div>

        <div class="col-sm-4 invoice-col">
          
           <address>
              
               <table >
             <tr>
                 <%--<td>Requester Name:&nbsp;</td>
                   <td>:&nbsp;</td>
                   <td><b><asp:Label ID="lblRequesterName" runat="server" Text=""></asp:Label></b></td>--%>
             </tr>
            
         </table>

          </address>
        </div>
 
      
      </div>
   
   <div class="panel-body">
    <div class="co-md-12">
    <div class="table-responsive">
              <asp:GridView runat="server" ID="gvReceivedInventory" GridLines="None" CssClass="table table-responsive" AutoGenerateColumns="false" EmptyDataText="No records Found">
                    <Columns>
                       
                        <asp:BoundField DataField="ItemName"  HeaderText="Item Name" />
                        <asp:BoundField DataField="SubDepartmentName"  HeaderText="Department Name"/>
                        <asp:BoundField DataField="IssuedQty"  HeaderText="Issued QTY"/>
                        <asp:BoundField DataField="ShortCode"  HeaderText="Unit"/>
                        <asp:BoundField DataField="IssuedUser"  HeaderText="Issued By"/>
                        <asp:BoundField DataField="IssuedOn" HeaderText="Issued On" DataFormatString="{0:dd-MMM-yyyy hh:mm tt}"/>
                        <asp:BoundField DataField="DeliveredUser"  HeaderText="Delivered By"/>
                        <asp:BoundField DataField="deliveredOn" HeaderText="Delivered On" DataFormatString="{0:dd-MMM-yyyy hh:mm tt}"/>
                  
                        </Columns>
                </asp:GridView>
    </div>
    </div>
    </div>

      <br />
      <div id="tablesBom">
      
      </div>

       <div class="row no-print">
        <div class="col-xs-12">
          <button  class="btn btn-success" onclick="window.print();"><i class="fa fa-print" ></i> Print</button>
        </div>

      </div>
    </div> 
    <div id="model_table"  >

</div>
    </form>
   
   <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
    <script src="googleapis-jquery(2.1.1).js"></script>


   <%--<script type="text/javascript">
       var DataList = <%= getJsonReportPRBom() %>
       GetBomData();

       function GetBomData()
       {
       //alert(DataList);


       var arrayListForTables;

       var arrTable =  new Array();
        arrTable = DataList.split(',');


           var text = "";
           var field = "";
           var BidimgPath = "";
           var DepimgPath = "";
           var ListId = "";
           for (var i = 1; i <= DataList.length; i++) {
                field = DataList[i - 1].split('-');
                ListId = field[0]+"_"+field[1]+"_"+field[2];


           }
        //var htmlCode = 
            
       }

   </script>--%>
    
</asp:Content>
