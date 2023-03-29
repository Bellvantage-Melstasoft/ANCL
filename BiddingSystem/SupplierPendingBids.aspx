﻿<%@ Page Title="" Language="C#" MasterPageFile="~/SupplierLoadingWebUIInner.Master" AutoEventWireup="true" CodeBehind="SupplierPendingBids.aspx.cs" Inherits="BiddingSystem.SupplierPendingBids" %>
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
          color:Black;
      }
    </style>
    <div class="services-breadcrumb" style="background-color: #005383">
		<div class="agile_inner_breadcrumb">
			<div class="container">
				<ul class="w3_short">
					<li>
						<a href="SupplierInitialFrontViewInner.aspx" style="color:White">Home</a>
						<i>|</i>
                        <a href="#" style="color:Yellow">Pending Bid Submission</a>
					</li>
				</ul>
			</div>
		</div>
	</div>
    <br />
    <form runat="server" id="form1">
    <div class="faqs-w3l" style="background-color:White;margin-top: -22px;">
         <div class="container" style="background-color:White">
			<!-- //tittle heading -->
            <div class="row">
            <div class="col-md-12">
          
               <h3>PENDING BID SUBMISSION [ <small id="pendingBidCount" style="color:Black;"> </small> &nbsp;]</h3>
            <br />
                <hr class="soft" />
        <table class="table" id="customers">
            <thead>
                <tr>
                    <th>Product</th>
                    <th>Item Name</th>
                    <th>Expired On</th>
                    <th>Company</th>
                    <th>View</th>
                    <th>Apply</th>
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
    </form>
<script src="AppResources/themes/js/jquery.js" type="text/javascript"></script>
<script type="text/javascript">
 var DataList = <%= getJsonBiddingPendingItemList() %>
 var DataListPendingBidCount = <%= getJsonBiddingPendingBidCount() %>
 document.getElementById('pendingBidCount').innerHTML = DataListPendingBidCount + " item(s)";

 LoadSupplierPendingBids();

 function LoadSupplierPendingBids()
 {
    var text = "";
        var field = "";
        var BidimgPath = "";
        var ListId = "";
        for (var i = 1; i <= DataList.length; i++) {
            field = DataList[i - 1].split('-');
            ListId = field[0]+"_"+field[1]+"_"+field[6];
            if(field[4] != "")
            {
                 BidimgPath = field[4].replace('~/','');
            }
            else{
                 BidimgPath = "images/noimage.png";
            }
            var htmlcode =
                  ' <tr>' +
                  ' <td id='+ListId+'> ' +
                  ' <img width="60" src='+BidimgPath+' alt="" /></td>' +
                  ' <td>'+field[2]+'</td>' +
                  ' <td> <p id="demo'+i+'" class="demo" style="color:  Red;margin-bottom: 0px;margin-top: 0px;font-weight: bold;text-align:  center;"/></td>' +
                  ' <td>'+field[5]+'</td>' +
                  ' <td>' +
                  ' <button type="button" id='+ListId+' class="btn btn-warning" onclick="ViewBid_ClickEvent(this.id)">View</button>' +
                  ' </td>' +
                  ' <td>' +
                  ' <button type="button" id='+ListId+' class="btn btn-info" onclick="Bid_ClickEvent(this.id)">Apply Now!</button>' +
                  ' </td>'
                  ' </tr>' + ''
            text += htmlcode;
        }
        document.getElementById("tablePendingDetails").innerHTML = text;
     }

     var x = setInterval(function () {
     for (var c = 1; c <= DataList.length; c++) 
     {
      var fieldDate = "";
      fieldDate = DataList[c - 1].split('-');
      var endDate = new Date(fieldDate[3]);
      var d = endDate;
      var time01   =  (
                ("00" + (d.getMonth() + 1)).slice(-2) + " " + 
                ("00" + d.getDate()).slice(-2) + " ," + 
                d.getFullYear() + " " + 
                ("00" + d.getHours()).slice(-2) + ":" + 
                ("00" + d.getMinutes()).slice(-2) + ":" + 
                ("00" + d.getSeconds()).slice(-2)
            );

        var countDownDate = new Date(time01).getTime();
        // Get todays date and time
        var now = new Date().getTime();
        // Find the distance between now an the count down date
        var distance = countDownDate - now;
        // Time calculations for days, hours, minutes and seconds
        var days = Math.floor(distance / (1000 * 60 * 60 * 24));
        var hours = Math.floor((distance % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
        var minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));
        var seconds = Math.floor((distance % (1000 * 60)) / 1000);
        // Display the result in the element with id="demo"
        document.getElementById('demo'+c+'').innerHTML = days + "d " + hours + "h " + minutes + "m " + seconds + "s ";              
        // If the count down is finished, write some text 
        if (distance < 0) {
            clearInterval(x);
            document.getElementById('demo'+c+'').innerHTML = "EXPIRED";
        }
        }
      }, 1000);
    
     function Bid_ClickEvent(input)
     {
       window.location.replace("SupplierSubmitBidInner.aspx?Info="+input);
     }

     function ViewBid_ClickEvent(input)
     {
       window.location.replace("SupplierBidDetailInner.aspx?Info="+input+"&Status=P");
     }
</script>
</asp:Content>
