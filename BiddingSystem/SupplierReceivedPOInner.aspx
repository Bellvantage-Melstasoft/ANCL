<%@ Page Title="" Language="C#" MasterPageFile="~/SupplierLoadingWebUIInner.Master" AutoEventWireup="true" CodeBehind="SupplierReceivedPOInner.aspx.cs" Inherits="BiddingSystem.SupplierReceivedPOInner" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <style type="text/css">
      #customers {
          font-family: "Trebuchet MS", Arial, Helvetica, sans-serif;
          border-collapse: collapse;
          width: 100%;
      }
      
      #customers td, #customers th {
          border: 1px solid #ddd;
          padding: 8px;
      }
      
      #customers tr:nth-child(even){background-color: #f2f2f2;}
      
      #customers tr:hover {background-color: #ddd;}
      
      #customers th {
          padding-top: 12px;
          padding-bottom: 12px;
          text-align: center;
          background-color: #467394;
          color: white;
      }
      #customers td {
          padding-top: 12px;
          padding-bottom: 12px;
          text-align: center;
      }
    </style>
     <div class="services-breadcrumb" style="background-color: #005383">
		<div class="agile_inner_breadcrumb">
			<div class="container">
				<ul class="w3_short">
					<li>
						<a href="SupplierInitialFrontViewInner.aspx" style="color:White">Home</a>
						<i style="color:White">|</i>
                        <a href="#" style="color:Yellow">Received Purchase Orders</a>
					</li>
				</ul>
			</div>
		</div>
	</div>
     <div class="faqs-w3l" style="background-color:White">
         <div class="container">
			<!-- //tittle heading -->
            <div class="row">
            <div class="col-md-12">
        <h3>Received Purchase Orders</h3>
        <hr class="soft" />
        <table class="table" id="customers">
            <thead>
                <tr>
                    <th>PO Id</th>
                    <th>PO Code</th>
                    <th>Created Date</th>
                    <th></th>
                </tr>
            </thead>
            <tbody id="tablePendingDetails">

            </tbody>
        </table>
        <a href="#" class="btn btn-large pull-right" style="visibility:hidden;">Next <i class="icon-arrow-right"></i></a>
        </div>
        </div>
        </div>
    </div>

<script src="AppResources/themes/js/jquery.js" type="text/javascript"></script>
<script type="text/javascript">
  var DataList = <%= getJsonReceived() %>
  
  LoadSupplierPendingBids();

 function LoadSupplierPendingBids()
 {
    var text = "";
        var field = "";
        var BidimgPath = "";
        var ListId = "";
        for (var i = 1; i <= DataList.length; i++) {
            field = DataList[i - 1].split('-');
            ListId = field[0]+"_"+field[1]+"_"+field[2];

            var htmlcode =
                  ' <tr>' +
                  ' <td style="color:Black">'+field[0]+'</td>' +
                  ' <td style="color:Black"> <p id="demo'+i+'" class="demo" style="color:Black">'+field[1]+'</td>' +
                  ' <td style="color:Black">'+field[2]+'</td>' +
                  ' <td>' +
                  ' <button type="button" id='+field[0]+' class="btn btn-warning" onclick="ViewBid_ClickEvent(this.id)">View</button>' +
                  ' </td>' +
                  ' </tr>' + ''
            text += htmlcode;
        }
        document.getElementById("tablePendingDetails").innerHTML = text;
     }

     function ViewBid_ClickEvent(input)
     {
       window.location.replace("SupplierReceivedPOInnerDetail.aspx?Info="+input);
     }

</script>
</asp:Content>
