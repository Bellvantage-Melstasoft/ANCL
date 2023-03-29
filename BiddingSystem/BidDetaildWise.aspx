<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingSupplier.Master" AutoEventWireup="true" CodeBehind="BidDetaildWise.aspx.cs" Inherits="BiddingSystem.BidDetaildWise" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <div id="myModal" class="modal modal-primary fade" tabindex="-1" role="dialog"  aria-hidden="true">
       <div class="modal-dialog">
		<!-- Modal content-->
		<div class="modal-content" style="background-color:#a2bdcc;">
		<div class="modal-header" style="background-color:#148690;color:White;">
		<button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span></button>		
		<h4 class="modal-title">Item Specifications</h4>
	    </div>
	    <div class="modal-body" style=" background-color: white; ">
		<div class="login-w3ls">
        <div class="modal-body">
         <div class="row" style=" margin-left: 10px; ">
          <div class="col-md-12">
          <table class="table" id="customers">
            <thead>
                <tr>
                    <th>Seq Id</th>
                    <th>Material</th>
                    <th>Description</th>
                </tr>
            </thead>
            <tbody id="tablePendingDetails">
            </tbody>
        </table>
          </div>
          </div>
          </div>
        <div class="modal-footer">
            <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
        </div>
        </div>
        </div>
     </div>
     </div>
     </div>
      <div class="span9">	
      <form class="form-inline navbar-search" method="post" action="#" >
		 <button type="" id="submitButton" class="btn" style=" margin-top: -10px;font-weight:bold; color:Black; ">Sort By :</button>
		  <select class="srchTxt" style=" width: 180px;color:Black; ">
			<option>Select Sort Option</option>
			<option>Bid end time </option>
			<option>Price Lowest First </option>
			<option>Price Heighest First </option>
		</select> 
       
        <div id="myTab" class="pull-right">
	    <a href="#blockView" data-toggle="tab"><span class="btn btn-large btn-primary" onclick="List_ClickEvent()"><i class="icon-th-large"></i></span></a>
	    </div>
    </form>
      <br /><br />
      <asp:ScriptManager runat="server" ID="sm"></asp:ScriptManager>
      <asp:UpdatePanel ID="Updatepanel1" runat="server">
      <ContentTemplate>
      <div class="tab-content">
    	<div class="tab-pane active" id="listView">
            <div id="DetailedDiv"> 
            </div>
    	</div>
    </div>
     </ContentTemplate>
     </asp:UpdatePanel>
    </div>
    <asp:HiddenField ID="HiddenField1" runat="server" />
<script src="AppResources/js/jquery-min.js" type="text/javascript"></script>
<script type="text/javascript">
   //---------------All Item Allocated For Supplier Accroding to selected Department Type
    var DataList = <%= getJsonBiddingItemListAll() %>
    LoadAllBids();

    function LoadAllBids(){
        var text = "";
        var field = "";
        var BidimgPath = "";
        var DepimgPath = "";
        var ListId = "";
        for (var i = 1; i <= DataList.length; i++) {
            field = DataList[i - 1].split('-');
            ListId = field[0]+"_"+field[1]+"_"+field[10];
            if(field[6] != "")
            {
                 BidimgPath = field[6].replace('~/','');
            }
            else{
                 BidimgPath = "images/noimage.png";
            }
            if(field[5] != "")
            {
                 DepimgPath = field[5].replace('~/','');
            }
          
            var htmlcode =
                ' <div class="row" id='+ListId+'> ' +
			    ' <div class="span2"> ' +
			    ' <img src='+BidimgPath+' alt="" style=" margin-top: 40px;"> ' +
			    ' </div> ' +
			    ' <div class="span4"> ' +
				' <h3>'+field[2]+'</h3> ' +				
				' <hr class="soft" style="border-top-color: #1218ca;"> ' +
				' <h5> <img src='+DepimgPath+' height="40" width="40" />&nbsp;&nbsp;'+field[4]+' </h5> ' +
				' <p>Item Description :&nbsp;&nbsp;  '+field[9]+' </p> ' +
                ' <p>OrderId :&nbsp;&nbsp;  '+field[10]+' </p> ' +
				' <a class="btn btn-small btn-success pull-right" href="#" id='+ListId+' onclick="ViewBoM_ClickEvent(this.id) ">View Details</a> ' +
				' <br class="clr"> ' +
			    ' </div> ' +
			    ' <div class="span3 alignR"> ' +
			    ' <form class="form-horizontal qtyFrm"> ' +
			    ' <h3 id="demo'+i+'" style="font-size:22px;color:Red;"> </h3> ' +
			    ' <label class="checkbox" style="visibility:hidden;"> <input type="checkbox">  Adds product to compair </label> ' +
                '<br> ' +
			    ' <a href="#" class="btn btn-large btn-primary" id='+ListId+' onclick="Bid_ClickEvent(this.id)"> BID </a> ' +
			    ' </form> ' +
			    ' </div> ' +
	            ' </div> ' +
	            ' <hr class="soft" style=" border-bottom-color: red;">  ' + ''
            text += htmlcode;
        }
        document.getElementById("DetailedDiv").innerHTML = text;
    }

    function Bid_ClickEvent(input)
    {
       window.location.replace("BidDetails.aspx?Info="+input);
    }

    function List_ClickEvent()
    {
         window.location.replace("SupplierIndex.aspx");
    }

    function ViewBoM_ClickEvent(inputDetail)
    {
       var text = "";
       var ratus = inputDetail;
            var jsonText = JSON.stringify({ data: ratus });
            $.ajax({
                type: "POST",
                url: "BidDetaildWise.aspx/GetPRIdAndItemId?data="+ratus,
                data: jsonText,
                contentType: "application/json",
                dataType: "json",
                success: function (msg) {
                    if (msg.d.length > 0) {
                        for (var i = 0; i < msg.d.length; i++) {
                        var htmlcode =
                         ' <tr>' +
                         ' <td>'+ msg.d[i].SeqId +'</td>' +
                         ' <td>'+ msg.d[i].Meterial +'</td>' +
                         ' <td>' + msg.d[i].Description +'</td>' +
                         ' </tr>' + ''
                         text += htmlcode;
                        }
                      document.getElementById("tablePendingDetails").innerHTML = text;
                    }
                     var $popup = $("#myModal");
                     $popup.modal('show');
                },
                error: function (result) {
                },
                failure: function (response) {
                    alert(response.d);
                },
                error: function (response) {
                    alert(response.d);
                }
            });
       }

     var x = setInterval(function () {
     for (var c = 1; c <= DataList.length; c++) 
     {
      var fieldDate = "";
      fieldDate = DataList[c - 1].split('-');
      var endDate = new Date(fieldDate[8]);
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

     function BidDetailWise_ClickEvent(input)
     {
       window.location.replace("BidDetails.aspx?Info="+input);
     }

</script>

</asp:Content>
