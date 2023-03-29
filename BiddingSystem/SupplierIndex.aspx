<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingSupplier.Master" AutoEventWireup="true" CodeBehind="SupplierIndex.aspx.cs" Inherits="BiddingSystem.SupplierIndex" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<script src="http://ajax.aspnetcdn.com/ajax/jquery/jquery-1.7.js"></script>
<script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.10/jquery-ui.js"></script>
<style type="text/css">
    imgSize {
        width: 200px;
        height: 160px;
        object-fit: cover;
        }
</style>
    


    <div id="sidebar" class="span2" style="width:200px;">
		<div class="well well-small"><a id="myCart" href="PendingBidSubmission.aspx">Pending Bid Count  <span style=" border-radius: 4px;" class="badge badge-warning pull-right" id="pendingcnt"></span></a></div>
        <div class="well well-small"><a id="myCart" href="#">Total Bid Count   <span style=" border-radius: 4px;" class="badge badge-warning pull-right">0</span></a></div>
        <div class="well well-small"><a id="myCart" href="#">Approved Bids   <span style=" border-radius: 4px;" class="badge badge-warning pull-right">0</span></a></div>
       <%-- <ul id="sideManu" class="nav nav-tabs nav-stacked">
			
		</ul>--%>


        <br />

        
    <!--Search field -->

        <div style="border:none; padding:2px;border-radius:3px; background-color:#cad1e0;margin-left:5px ">
       <div ><span><h4><i class="icon-search" style=" margin-top: 6px; margin-left: 38px; "></i>&nbsp;&nbsp; Search</h4></span></div>
        <ul   class="nav nav-tabs nav-stacked">
			<li class="subMenu" ><a >  COMPANY WISE</a>
				<ul id="sideManu1" style="display:none" class="nav nav-tabs nav-stacked">
				
				</ul>
			</li>
			
		</ul>
        <br />
        <ul  class="nav nav-tabs nav-stacked">
			<li class="subMenu " ><a >ITEM TYPE</a>
				<ul  style="display:none" class="nav nav-tabs nav-stacked" id="SearchCategory">
				             
				</ul>
			</li>
			
		</ul>
           <br />
        <ul  class="nav nav-tabs nav-stacked">
			<li class="subMenu " style=" text-align: center; ">
				<button type="button" onclick='getValue()' class="btn btn-danger">Search </button>
			</li>
			
		</ul>

        </div>
		<br/>
	</div>
    <!--Search field -->

 <div class="span9">	
      <form class="form-inline navbar-search" method="post" action="#" >
		 <button type="" id="submitButton" class="btn" style="margin-top:-10px;color:black;font-weight: bold;margin-bottom:3px;">Sort By :</button>
		  <select class="srchTxt" style=" width: 180px;color:black;">
			<option>Select Sort Option</option>
			<option>Bid end time </option>
			<option>Price Lowest First </option>
			<option>Price Heighest First </option>
		</select> 
      </form>

     </br>


   
         
     

     </br>
     
      <div id="myTab" class="pull-right">
	      <a href="#" data-toggle="tab"><span class="btn btn-large btn-primary" style=" margin-top: -8px; " onclick="List_ClickEvent()" ><i class="icon-list"></i></span></a>
	  </div>
    <script type="text/javascript">
        document.getElementById("submitButton").disabled = true;
    </script>

	<div class="well well-small" style=" background-color: #d4d1d6;">

        <!-- Loading Latest Bid 10 Items Opened To Bid-->
		<h4>Latest Bids <small class="pull-right">
        <asp:LinkButton ID="lnkDetaild" runat="server"  style="color:Blue;visibility:hidden;" >Detailed</asp:LinkButton></small></h4>
		<div class="row-fluid">
		<div id="featured" class="carousel slide">
		<div class="carousel-inner">
              <div id = "DvLatestBid"></div>
		</div>
	         <a class="left carousel-control" href="#featured" data-slide="prev">‹</a>
	         <a class="right carousel-control" href="#featured" data-slide="next">›</a>
	    </div>
	    </div>
	</div>

    <!-- Loading All Items Opened To Bid-->
	<h4>All Items Enable </h4>
		  <ul class="thumbnails" id="UlBidListForAll"></ul>     
	</div>

    <!--load the values when search button is clicked-->
<script type="text/javascript">

    var DataList1 = <%= getJsonBiddingItemListAll() %>;
   
  

    var checkedValue = null; 
    var checkedValueItem=null;
    var SearchedAllBids="";

    function getValue(){
        checkedValue = null;
        checkedValueItem=null;
        
        var checkboxes = document.getElementsByClassName("messageCheckbox");
        var checkboxesItem = document.getElementsByClassName("messageCheckboxitem");
       
       
        var vals = "";
        var checkitem="";

        for (var i=0, n=checkboxes.length;i<n;i++) 
        {
            if (checkboxes[i].checked) 
            {
                checkedValue += ","+checkboxes[i].value;
            }
            
        }
        for (var i=0, n=checkboxesItem.length;i<n;i++) 
        {
            if (checkboxesItem[i].checked) 
            {
                checkedValueItem += ","+checkboxesItem[i].value;
            }
        }

        
       
        if(checkedValue==null && checkedValueItem==null){

        }
       
        
     //   if(checkedValue!=null || checkedValueItem!=null){

            SearchAllBidsLatest1();
           
     
            SearchAllBids();

        //}
        //else 
        //{

        
        //    SearchAllBidsLatest1();
           
     
        //    SearchAllBids();
        //}
       
    }
    
    
    //Load all bids when searched
    function SearchAllBids(){
        var text = "";
        var field = "";
        var BidimgPath = "";
        var DepimgPath = "";
        var ListId = "";

        if(checkedValue==null && checkedValueItem==null){

            text=' <p style="margin-left:50px">No records to display</p> ';}

           
            for (var i = 1; i <= DataList1.length; i++) {
                field = DataList1[i - 1].split('-');

                var company=field[3];
                var itemID=field[11]+"_"+field[12];

               
                if(checkedValue==null && checkedValueItem==null){

                    textLatestUl=' <p style="margin-left:50px">No records to display</p> ';
                    
                }
          

                if(checkedValue==null && checkedValueItem!=null){

                    if(checkedValueItem.includes(itemID)){

                        //  if(checkedValueItem.includes())

                        ListId = field[0]+"_"+field[1]+"_"+field[9];
                        if(field[6] != "")
                        {
                            BidimgPath = field[6].replace('~/','');
                        }
                        else{
                            BidimgPath = "images/noimage.jpg";
                        }
                        if(field[5] != "")
                        {
                            DepimgPath = field[5].replace('~/','');
                        }


            
                        var htmlcode =

                              '<li class="span3" id='+ListId+'>' +
                              '<div class="thumbnail" style="background-color:#ebf1f7;">' +
                              '<a  href="#"><img src='+BidimgPath+'  alt="" style="height:136px"/></a>' +
                              '<div class="caption">' + 
                                              '<div style="height:60px;">' +
                                              '<h5 style="color:Black;text-align: center;">'+field[2]+'</h5>' +
                                              '</div>' +
				               
                              '<p id="demo'+i+'" style="color:red; font-weight:bold;" ></p>' +
                              '<p style="color:Black;"><img src='+DepimgPath+' height="20" width="20" /> '+field[4]+'</p>' +
                              '<p style="color:Black;">OrderID: '+field[9]+'</p>' + '' 
                        text += htmlcode;

            
                        if(field[10] == "Edit Bid"){
                            htmlcode = '<h4 style="text-align:center"><input type="Button" id='+ListId+' class="btn btn-warning" onclick="Bid_ClickEvent(this.id)" value="'+field[10]+'"/></h4>' + ''
                            text += htmlcode;
                        }
                        if(field[10] == "Pending Bid"){
                            htmlcode = '<h4 style="text-align:center"><input type="Button" id='+ListId+' class="btn btn-danger" onclick="Bid_ClickEvent(this.id)" value="'+field[10]+'"/></h4>' + ''
                            text += htmlcode;
                        }
                        if(field[10] == "Bid"){
                            htmlcode = '<h4 style="text-align:center"><input type="Button" id='+ListId+' class="btn btn-primary" onclick="Bid_ClickEvent(this.id)" value="'+field[10]+'"/></h4>' + ''
                            text += htmlcode;
                        }
                        htmlcode = '</div>' +
                        '</div>' +
                        '</li>' + ''
                        text += htmlcode;

                    }

                    else{

                        text=' <p style="margin-left:50px">No records to display</p> ';

                    }
                }


        



                if(checkedValue!=null && checkedValueItem!=null){
                    if(checkedValue.includes(company) && checkedValueItem!=null){


                    //if company name and item id is equal

                    if(checkedValueItem.includes(itemID)){

                        ListId = field[0]+"_"+field[1]+"_"+field[9];
                        if(field[6] != "")
                        {
                            BidimgPath = field[6].replace('~/','');
                        }
                        else{
                            BidimgPath = "images/noimage.jpg";
                        }
                        if(field[5] != "")
                        {
                            DepimgPath = field[5].replace('~/','');
                        }


            
                        var htmlcode =

                              '<li class="span3" id='+ListId+'>' +
                              '<div class="thumbnail" style="background-color:#ebf1f7;">' +
                              '<a  href="#"><img src='+BidimgPath+'  alt="" style="height:136px"/></a>' +
                              '<div class="caption">' + 
                                              '<div style="height:60px;">' +
                                              '<h5 style="color:Black;text-align: center;">'+field[2]+'</h5>' +
                                              '</div>' +
				               
                              '<p id="demo'+i+'" style="color:red; font-weight:bold;" ></p>' +
                              '<p style="color:Black;"><img src='+DepimgPath+' height="20" width="20" /> '+field[4]+'</p>' +
                              '<p style="color:Black;">OrderID: '+field[9]+'</p>' + '' 
                        text += htmlcode;

            
                        if(field[10] == "Edit Bid"){
                            htmlcode = '<h4 style="text-align:center"><input type="Button" id='+ListId+' class="btn btn-warning" onclick="Bid_ClickEvent(this.id)" value="'+field[10]+'"/></h4>' + ''
                            text += htmlcode;
                        }
                        if(field[10] == "Pending Bid"){
                            htmlcode = '<h4 style="text-align:center"><input type="Button" id='+ListId+' class="btn btn-danger" onclick="Bid_ClickEvent(this.id)" value="'+field[10]+'"/></h4>' + ''
                            text += htmlcode;
                        }
                        if(field[10] == "Bid"){
                            htmlcode = '<h4 style="text-align:center"><input type="Button" id='+ListId+' class="btn btn-primary" onclick="Bid_ClickEvent(this.id)" value="'+field[10]+'"/></h4>' + ''
                            text += htmlcode;
                        }
                        htmlcode = '</div>' +
                        '</div>' +
                        '</li>' + ''
                        text += htmlcode;

                
                    }
                
                    else{
                        text=' <p style="margin-left:50px">No records to display</p> ';

                    }
                    }
                }
                
                else if(checkedValueItem==null && checkedValue!=null){


                    if(checkedValue.includes(company)){

                        ListId = field[0]+"_"+field[1]+"_"+field[9];
                        if(field[6] != "")
                        {
                            BidimgPath = field[6].replace('~/','');
                        }
                        else{
                            BidimgPath = "images/noimage.jpg";
                        }
                        if(field[5] != "")
                        {
                            DepimgPath = field[5].replace('~/','');
                        }


            
                        var htmlcode =

                              '<li class="span3" id='+ListId+'>' +
                              '<div class="thumbnail" style="background-color:#ebf1f7;">' +
                              '<a  href="#"><img src='+BidimgPath+'  alt="" style="height:136px"/></a>' +
                              '<div class="caption">' + 
                                              '<div style="height:60px;">' +
                                              '<h5 style="color:Black;text-align: center;">'+field[2]+'</h5>' +
                                              '</div>' +
				               
                              '<p id="demo'+i+'" style="color:red; font-weight:bold;" ></p>' +
                              '<p style="color:Black;"><img src='+DepimgPath+' height="20" width="20" /> '+field[4]+'</p>' +
                              '<p style="color:Black;">OrderID: '+field[9]+'</p>' + '' 
                        text += htmlcode;

            
                        if(field[10] == "Edit Bid"){
                            htmlcode = '<h4 style="text-align:center"><input type="Button" id='+ListId+' class="btn btn-warning" onclick="Bid_ClickEvent(this.id)" value="'+field[10]+'"/></h4>' + ''
                            text += htmlcode;
                        }
                        if(field[10] == "Pending Bid"){
                            htmlcode = '<h4 style="text-align:center"><input type="Button" id='+ListId+' class="btn btn-danger" onclick="Bid_ClickEvent(this.id)" value="'+field[10]+'"/></h4>' + ''
                            text += htmlcode;
                        }
                        if(field[10] == "Bid"){
                            htmlcode = '<h4 style="text-align:center"><input type="Button" id='+ListId+' class="btn btn-primary" onclick="Bid_ClickEvent(this.id)" value="'+field[10]+'"/></h4>' + ''
                            text += htmlcode;
                        }
                        htmlcode = '</div>' +
                        '</div>' +
                        '</li>' + ''
                        text += htmlcode;

                    }


                }
                else{
                    text=' <p style="margin-left:50px">No records to display</p> ';

                }
                //if(text!=" " && text!=null){
                //    SearchedAllBids= i+",";
                //}
            }
            
        document.getElementById("UlBidListForAll").innerHTML = text;
        
    }            // if(checkedValue.includes(company1)){

        //------Load searched latest bids according ascending order of the left bidding time
        //------------------Get Latest 10 Bids 
   

    //-----------------Check the logged supplier already Bided or Keep it as Later Bid or not yet Bid
    //-----------------If,  not yet Bid   -> Button Keep Text as "BID"
    //-----------------If,  already Bided -> Button Keep Text as "EDIT BID"
    //-----------------If,  Later Bid     -> Button Keep Text as "PENDING BID"
   <%-- function Bid_ClickEvent(input)
    {
        var Supplier =<%=getJsonSupplier() %> ;
        var ratus = input;
            var jsonText = JSON.stringify({ data: ratus, sup: Supplier });
            $.ajax({
                type: "POST",
                url: "SupplierIndex.aspx/GetBidPendingOrNot?data="+ratus,
                data: jsonText,
                contentType: "application/json",
                dataType: "json",
                success: function (msg) {
                    if (msg.d.length > 0) {
                        if(msg.d[0].CountPending == "1"){
                           window.location.replace("BidDetails.aspx?Info="+input+"&Status=P");
                        }
                        else if(msg.d[0].AlreadyBidCount == "1"){
                            window.location.replace("BidSubmission.aspx?Info="+input+"&Status=A");
                        }
                        else if(msg.d[0].AlreadyBidCount == "0" && msg.d[0].CountPending == "0"){
                        window.location.replace("BidDetails.aspx?Info="+input);
                       }
                    }
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

    //---------------Supplier Index Page Redirect To Detailed Wise Page (Same records but the UI displaying data is different, not like boxes)
    function List_ClickEvent()
    {
         window.location.replace("BidDetaildWise.aspx");
    }--%>

    //--------------Set Count DownTimer To Each and Every Bid
    //var x = setInterval(function () {
    //    for (var c = 1; c <= DataList1.length; c++) 
    //    {


    //        if(SearchedAllBids.includes(DataList1[c-1])){
    //            var fieldDate = "";
    //            fieldDate = DataList1[c - 1].split('-');
    //            var endDate = new Date(fieldDate[8]);
    //            var d = endDate;
    //            var time01   =  (
    //                      ("00" + (d.getMonth() + 1)).slice(-2) + " " + 
    //                      ("00" + d.getDate()).slice(-2) + " ," + 
    //                      d.getFullYear() + " " + 
    //                      ("00" + d.getHours()).slice(-2) + ":" + 
    //                      ("00" + d.getMinutes()).slice(-2) + ":" + 
    //                      ("00" + d.getSeconds()).slice(-2)
    //                  );
    //            var countDownDate = new Date(time01).getTime();
     
    //            // Get todays date and time
    //            var now = new Date().getTime();

    //            // Find the distance between now an the count down date
    //            var distance = countDownDate - now;

    //            // Time calculations for days, hours, minutes and seconds
    //            var days = Math.floor(distance / (1000 * 60 * 60 * 24));
    //            var hours = Math.floor((distance % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
    //            var minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));
    //            var seconds = Math.floor((distance % (1000 * 60)) / 1000);

    //            // Display the result in the element with id="demo" - innerHTML
    //            document.getElementById('demo1'+c+'').innerHTML = days + "d " + hours + "h " + minutes + "m " + seconds + "s ";
                          
    //            // If the count down is finished, write some text or you can fire an event (Eg : Page Reload)
    //            if (distance < 0) {
    //                clearInterval(x);
    //                document.getElementById('demo1'+c+'').innerHTML = "EXPIRED";
    //            }
    //        }}
    //    }, 1000);   
    </script>

  <script type="text/javascript">
    //----------------------------------------------Search all the latest bids--------------------------------------

    var DataListLatest1 = <%= getJsonBiddingItemListAllLatest() %>;
    var SupplierMainCategorySub = <%= getJsonSupplierMainCategoryAndSubCat() %>;
    var SearchedAllLatestBids="";
    var SearchedAllBids="";

    function SearchAllBidsLatest1()
    {
       
        var textLatestUl = "";
        var textLatest = "";
        var fieldLatest = "";
        var BidimgPathLatest = "";
        var DepimgPathLatest = "";
        var ListIdLatest = "";
        var htmlcodeLatest = "" ;
        
        var rounds = ((DataListLatest1.length))/4;
        var valueOfLoopRounds1 = (Math.ceil(rounds));

        if(checkedValue==null && checkedValueItem==null){

            textLatestUl=' <p style="margin-left:50px">No records to display</p> ';

            
                    
        }
          
        for (var i = 1; i <= valueOfLoopRounds1; i++) {
            if(i == 1){
                htmlcodeLatest =
                '<div class="item active" id='+i+'>'+
                '<ul class="thumbnails" id="myUls">'+
                '</ul>'+
                '</div>'+ ''
                textLatest += htmlcodeLatest;

                document.getElementById("DvLatestBid").innerHTML = textLatest; 

                for (var f = 1; f <= DataListLatest1.length; f++) {

                    fieldLatest = DataListLatest1[f - 1].split('-');
                    ListIdLatest = fieldLatest[0]+"_"+fieldLatest[1]+"_"+fieldLatest[9];

                    //When searched using company id
                    var companyID=fieldLatest[3];
                    var itemID1=fieldLatest[11]+"_"+fieldLatest[12];

                   

                    //if the company is not selected
                    if(checkedValue==null && checkedValueItem!=null){


                        if(checkedValueItem.includes(itemID1)){


                            if(fieldLatest[6] != "")
                            {
                                BidimgPathLatest = fieldLatest[6].replace('~/','');
                            }
                            else{
                                BidimgPathLatest = "images/noimage.jpg";
                            }
                            if(fieldLatest[5] != "")
                            {
                                DepimgPathLatest = fieldLatest[5].replace('~/','');
                            }
                            if(f >= 0 && f < 5 ){
                                var htmlcodeLatestUl =
                                   ' <li class="span3" id='+f+'> ' +
                                   ' <div class="thumbnail"> ' +
                                   ' <a href="#"><img src='+BidimgPathLatest+' alt="" style="width:160px;height:100px;"></a> ' +
                                   ' <div class="caption"> ' + 
                                   '<div style="height:60px;">' +
                                   '<h5 style="color:Black;text-align: center;">'+fieldLatest[2]+'</h5>' +
                                   '</div>' +
                                   ' <p id="P'+f+'" style="color:red; font-weight:bold;"></p> ' +
                                   ' <p style="color:Black"><img src='+DepimgPathLatest+' height="20" width="20" /> '+fieldLatest[4]+'</p> ' +
                                   '<p style="color:Black;">OrderID: '+fieldLatest[9]+'</p>' + ''
                                textLatestUl += htmlcodeLatestUl;
                                if(fieldLatest[10] == "Edit Bid"){
                                    htmlcodeLatestUl = '<h4 style="text-align:center"><input type="Button" id='+ListIdLatest+' class="btn btn-warning" onclick="Bid_ClickEvent(this.id)" value="'+fieldLatest[10]+'"/></h4>' + ''
                                    textLatestUl += htmlcodeLatestUl;
                                }
                                if(fieldLatest[10] == "Pending Bid"){
                                    htmlcodeLatestUl = '<h4 style="text-align:center"><input type="Button" id='+ListIdLatest+' class="btn btn-danger" onclick="Bid_ClickEvent(this.id)" value="'+fieldLatest[10]+'"/></h4>' + ''
                                    textLatestUl += htmlcodeLatestUl;
                                }
                                if(fieldLatest[10] == "Bid"){
                                    htmlcodeLatestUl = '<h4 style="text-align:center"><input type="Button" id='+ListIdLatest+' class="btn btn-primary" onclick="Bid_ClickEvent(this.id)" value="'+fieldLatest[10]+'"/></h4>' + ''
                                    textLatestUl += htmlcodeLatestUl;
                                }
                                htmlcodeLatestUl = '</div>' +
                                '</div>' +
                                '</li>' + ''
                                textLatestUl += htmlcodeLatestUl;
                            }
                        }
                        else{


                            textLatestUl=' <p style="margin-left:50px">No records to display</p> ';

                        }

                    }

                    //if the company is selected
                    else if(checkedValue!=null && checkedValueItem!=null){

                        if(checkedValue.includes(companyID) && checkedValueItem!=null){


                            if(checkedValueItem.includes(itemID1)){

                   
                                if(fieldLatest[6] != "")
                                {
                                    BidimgPathLatest = fieldLatest[6].replace('~/','');
                                }
                                else{
                                    BidimgPathLatest = "images/noimage.jpg";
                                }
                                if(fieldLatest[5] != "")
                                {
                                    DepimgPathLatest = fieldLatest[5].replace('~/','');
                                }
                                if(f >= 0 && f < 5 ){
                                    var htmlcodeLatestUl =
                                       ' <li class="span3" id='+f+'> ' +
                                       ' <div class="thumbnail"> ' +
                                       ' <a href="#"><img src='+BidimgPathLatest+' alt="" style="width:160px;height:100px;"></a> ' +
                                       ' <div class="caption"> ' + 
                                       '<div style="height:60px;">' +
                                       '<h5 style="color:Black;text-align: center;">'+fieldLatest[2]+'</h5>' +
                                       '</div>' +
                                       ' <p id="P'+f+'" style="color:red; font-weight:bold;"></p> ' +
                                       ' <p style="color:Black"><img src='+DepimgPathLatest+' height="20" width="20" /> '+fieldLatest[4]+'</p> ' +
                                       '<p style="color:Black;">OrderID: '+fieldLatest[9]+'</p>' + ''
                                    textLatestUl += htmlcodeLatestUl;
                                    if(fieldLatest[10] == "Edit Bid"){
                                        htmlcodeLatestUl = '<h4 style="text-align:center"><input type="Button" id='+ListIdLatest+' class="btn btn-warning" onclick="Bid_ClickEvent(this.id)" value="'+fieldLatest[10]+'"/></h4>' + ''
                                        textLatestUl += htmlcodeLatestUl;
                                    }
                                    if(fieldLatest[10] == "Pending Bid"){
                                        htmlcodeLatestUl = '<h4 style="text-align:center"><input type="Button" id='+ListIdLatest+' class="btn btn-danger" onclick="Bid_ClickEvent(this.id)" value="'+fieldLatest[10]+'"/></h4>' + ''
                                        textLatestUl += htmlcodeLatestUl;
                                    }
                                    if(fieldLatest[10] == "Bid"){
                                        htmlcodeLatestUl = '<h4 style="text-align:center"><input type="Button" id='+ListIdLatest+' class="btn btn-primary" onclick="Bid_ClickEvent(this.id)" value="'+fieldLatest[10]+'"/></h4>' + ''
                                        textLatestUl += htmlcodeLatestUl;
                                    }
                                    htmlcodeLatestUl = '</div>' +
                                    '</div>' +
                                    '</li>' + ''
                                    textLatestUl += htmlcodeLatestUl;
                                }
                            }
                    

                            else{
                                textLatestUl=' <p style="margin-left:50px">No records to display</p> ';

                            }
                        }}

                    else if(checkedValueItem==null && checkedValue!=null){

                        if(checkedValue.includes(companyID)){

                            if(fieldLatest[6] != "")
                            {
                                BidimgPathLatest = fieldLatest[6].replace('~/','');
                            }
                            else{
                                BidimgPathLatest = "images/noimage.jpg";
                            }
                            if(fieldLatest[5] != "")
                            {
                                DepimgPathLatest = fieldLatest[5].replace('~/','');
                            }
                            if(f >= 0 && f < 5 ){
                                var htmlcodeLatestUl =
                                   ' <li class="span3" id='+f+'> ' +
                                   ' <div class="thumbnail"> ' +
                                   ' <a href="#"><img src='+BidimgPathLatest+' alt="" style="width:160px;height:100px;"></a> ' +
                                   ' <div class="caption"> ' + 
                                   '<div style="height:60px;">' +
                                   '<h5 style="color:Black;text-align: center;">'+fieldLatest[2]+'</h5>' +
                                   '</div>' +
                                   ' <p id="P'+f+'" style="color:red; font-weight:bold;"></p> ' +
                                   ' <p style="color:Black"><img src='+DepimgPathLatest+' height="20" width="20" /> '+fieldLatest[4]+'</p> ' +
                                   '<p style="color:Black;">OrderID: '+fieldLatest[9]+'</p>' + ''
                                textLatestUl += htmlcodeLatestUl;
                                if(fieldLatest[10] == "Edit Bid"){
                                    htmlcodeLatestUl = '<h4 style="text-align:center"><input type="Button" id='+ListIdLatest+' class="btn btn-warning" onclick="Bid_ClickEvent(this.id)" value="'+fieldLatest[10]+'"/></h4>' + ''
                                    textLatestUl += htmlcodeLatestUl;
                                }
                                if(fieldLatest[10] == "Pending Bid"){
                                    htmlcodeLatestUl = '<h4 style="text-align:center"><input type="Button" id='+ListIdLatest+' class="btn btn-danger" onclick="Bid_ClickEvent(this.id)" value="'+fieldLatest[10]+'"/></h4>' + ''
                                    textLatestUl += htmlcodeLatestUl;
                                }
                                if(fieldLatest[10] == "Bid"){
                                    htmlcodeLatestUl = '<h4 style="text-align:center"><input type="Button" id='+ListIdLatest+' class="btn btn-primary" onclick="Bid_ClickEvent(this.id)" value="'+fieldLatest[10]+'"/></h4>' + ''
                                    textLatestUl += htmlcodeLatestUl;
                                }
                                htmlcodeLatestUl = '</div>' +
                                '</div>' +
                                '</li>' + ''
                                textLatestUl += htmlcodeLatestUl;
                            }
                        }

                        //if(textLatestUl!="" && textLatestUl!=null){
                        //    SearchedAllBids= i+",";
                        //}
                        
                        
                    }
                    //if(textLatestUl!=null){
                    //    SearchedAllLatestBids = DataListLatest1[f - 1];
                    //}

                    
                }  
                document.getElementById("myUls").innerHTML = textLatestUl;

                
            }
        

            else{
                htmlcodeLatest =
                '<div class="item" id='+i+'>'+
                '<ul class="thumbnails" id="myUls">'+
                '</ul>'+
                '</div>'+ ''
                textLatest += htmlcodeLatest;
            }
        
        }
        //========================================= 


    }

            //---------------------Count Down Timer For MovingSlider (Latest Bids)
            //var z = setInterval(function () {
            //    for (var v = 1; v <= DataListLatest1.length; v++) 
            //    {
            //        if(SearchedAllBids.includes([DataListLatest1[v-1]])){
            //            var fieldDateLatestS = "";
            //            fieldDateLatestS = DataListLatest1[v - 1].split('-');
            //            var endDateLatest = new Date(fieldDateLatestS[8]);
            //            var ti = endDateLatest;
            //            var time01   =  (
            //                      ("00" + (ti.getMonth() + 1)).slice(-2) + " " + 
            //                      ("00" + ti.getDate()).slice(-2) + " ," + 
            //                      ti.getFullYear() + " " + 
            //                      ("00" + ti.getHours()).slice(-2) + ":" + 
            //                      ("00" + ti.getMinutes()).slice(-2) + ":" + 
            //                      ("00" + ti.getSeconds()).slice(-2)
            //                  );
            //            var countDownDate1 = new Date(time01).getTime();
            //            // Get todays date and time
            //            var now1 = new Date().getTime();
            //            // Find the distance between now an the count down date
            //            var distance = countDownDate1 - now1;
            //            // Time calculations for days, hours, minutes and seconds
            //            var days1 = Math.floor(distance / (1000 * 60 * 60 * 24));
            //            var hours1 = Math.floor((distance % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
            //            var minutes1 = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));
            //            var seconds1 = Math.floor((distance % (1000 * 60)) / 1000);
            //            // Display the result in the element with id="demo"

            //            if(v!=null){
            //                document.getElementById('hh'+v+'').innerHTML = days1 + "d " + hours1 + "h " + minutes1 + "m " + seconds1 + "s "; }
                       
            //            // If the count down is finished, write some text (You can Fire the Event to Reload Page to disable the Bid when it will expired)
            //            if (distance < 0) {
            //                clearInterval(z);
            //                document.getElementById('hh'+v+'').innerHTML = "EXPIRED";
            //            }
            //        }}
            //}, 1000);


        
</script>

<script type="text/javascript">
    //---------------All Item Allocated For Supplier Accroding to selected Department Type
    //---------------Loading All Items Opened To Bid
    var DataList = <%= getJsonBiddingItemListAll() %>;
    var SearchedAllBids="";

    LoadAllBids();



    function LoadAllBids(){
       
        var text = "";
        var field = "";
        var BidimgPath = "";
        var DepimgPath = "";
        var ListId = "";
        for (var i = 1; i <= DataList.length; i++) {
            field = DataList[i - 1].split('-');
            ListId = field[0]+"_"+field[1]+"_"+field[9];
            if(field[6] != "")
            {
                 BidimgPath = field[6].replace('~/','');
            }
            else{
                
                 BidimgPath = "images/noimage.jpg";
            }
            if(field[5] != "")
            {
                 DepimgPath = field[5].replace('~/','');
            }
            
            var htmlcode =

                  '<li class="span3" id='+ListId+'>' +
				  '<div class="thumbnail" style="background-color:#ebf1f7;">' +
				  '<a  href="#"><img src='+BidimgPath+'  alt="" style="height:136px"/></a>' +
				  '<div class="caption">' + 
                                  '<div style="height:60px;">' +
                                  '<h5 style="color:Black;text-align: center;">'+field[2]+'</h5>' +
                                  '</div>' +
				               
				  '<p id="demo'+i+'" style="color:red; font-weight:bold;" ></p>' +
				  '<p style="color:Black;"><img src='+DepimgPath+' height="20" width="20" /> '+field[4]+'</p>' +
                  '<p style="color:Black;">OrderID: '+field[9]+'</p>' + '' 
                  text += htmlcode;
                  if(field[10] == "Edit Bid"){
                    htmlcode = '<h4 style="text-align:center"><input type="Button" id='+ListId+' class="btn btn-warning" onclick="Bid_ClickEvent(this.id)" value="'+field[10]+'"/></h4>' + ''
                     text += htmlcode;
                  }
                  if(field[10] == "Pending Bid"){
                    htmlcode = '<h4 style="text-align:center"><input type="Button" id='+ListId+' class="btn btn-danger" onclick="Bid_ClickEvent(this.id)" value="'+field[10]+'"/></h4>' + ''
                     text += htmlcode;
                  }
                  if(field[10] == "Bid"){
                    htmlcode = '<h4 style="text-align:center"><input type="Button" id='+ListId+' class="btn btn-primary" onclick="Bid_ClickEvent(this.id)" value="'+field[10]+'"/></h4>' + ''
                     text += htmlcode;
                  }
				  htmlcode = '</div>' +
				  '</div>' +
				  '</li>' + ''
				  text += htmlcode;

				  //if(text!="" && text!=null){
				  //    SearchedAllBids= i+",";
				  //}
        }
       
        document.getElementById("UlBidListForAll").innerHTML = text;
    }


    

    //-----------------Check the logged supplier already Bided or Keep it as Later Bid or not yet Bid
    //-----------------If,  not yet Bid   -> Button Keep Text as "BID"
    //-----------------If,  already Bided -> Button Keep Text as "EDIT BID"
    //-----------------If,  Later Bid     -> Button Keep Text as "PENDING BID"
    function Bid_ClickEvent(input)
    {
        debugger;
        var Supplier =<%=getJsonSupplier() %> ;
        var ratus = input;
            var jsonText = JSON.stringify({ data: ratus, sup: Supplier });
            $.ajax({
                type: "POST",
                url: "SupplierIndex.aspx/GetBidPendingOrNot?data="+ratus+"&sup="+Supplier,
                data: jsonText,
                contentType: "application/json",
                dataType: "json",
                success: function (msg) {
                    if (msg.d.length > 0) {
                        if(msg.d[0].CountPending == "1"){
                           window.location.replace("BidDetails.aspx?Info="+input+"&Status=P");
                        }
                        else if(msg.d[0].AlreadyBidCount == "1"){
                            window.location.replace("BidSubmission.aspx?Info="+input+"&Status=A");
                        }
                        else if(msg.d[0].AlreadyBidCount == "0" && msg.d[0].CountPending == "0"){
                        window.location.replace("BidDetails.aspx?Info="+input);
                       }
                    }
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

    //---------------Supplier Index Page Redirect To Detailed Wise Page (Same records but the UI displaying data is different, not like boxes)
    function List_ClickEvent()
    {
         window.location.replace("BidDetaildWise.aspx");
    }

    //--------------Set Count DownTimer To Each and Every Bid
     var x = setInterval(function () {
     for (var c = 1; c <= DataList.length; c++) 
     {

     //    if(SearchedAllBids.includes(DataList[c-1])){
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

             if(document.getElementById('demo'+c+'')!=null){
                 // Display the result in the element with id="demo" - innerHTML
                 document.getElementById('demo'+c+'').innerHTML = days + "d " + hours + "h " + minutes + "m " + seconds + "s ";}
                          
             // If the count down is finished, write some text or you can fire an event (Eg : Page Reload)
             if (distance < 0) {
                 clearInterval(x);
                 if(document.getElementById('demo'+c+'')!=null){
                     document.getElementById('demo'+c+'').innerHTML = "EXPIRED";}
             }
          }
      }, 1000);
    
</script>





<script type="text/javascript">
    //------------------Get Latest 10 Bids 
    var DataListLatest = <%= getJsonBiddingItemListAllLatest() %>;

    var SearchedAllLatestBids="";

    LoadAllBidsLatest();

    function LoadAllBidsLatest()
    {
        var textLatestUl = "";
        var textLatest = "";
        var fieldLatest = "";
        var BidimgPathLatest = "";
        var DepimgPathLatest = "";
        var ListIdLatest = "";
        var htmlcodeLatest = "" ;
        
        
        var rounds = ((DataListLatest.length))/4;
        var valueOfLoopRounds = (Math.ceil(rounds));

        

        for (var i = 1; i <= valueOfLoopRounds; i++) {
            if(i == 1){
                htmlcodeLatest =
                '<div class="item active" id='+i+'>'+
                '<ul class="thumbnails" id="myUl">'+
                '</ul>'+
                '</div>'+ ''
                textLatest += htmlcodeLatest;

                document.getElementById("DvLatestBid").innerHTML = textLatest; 

                for (var f = 1; f <= DataListLatest.length; f++) {

                    fieldLatest = DataListLatest[f - 1].split('-');
                    ListIdLatest = fieldLatest[0]+"_"+fieldLatest[1]+"_"+fieldLatest[9];

                   
                        if(fieldLatest[6] != "")
                        {
                            BidimgPathLatest = fieldLatest[6].replace('~/','');
                        }
                        else{
                            BidimgPathLatest = "images/noimage.jpg";
                        }
                        if(fieldLatest[5] != "")
                        {
                            DepimgPathLatest = fieldLatest[5].replace('~/','');
                        }
                        if(f >= 0 && f < 5 ){
                            var htmlcodeLatestUl =
                               ' <li class="span3" id='+f+'> ' +
                               ' <div class="thumbnail"> ' +
                               ' <a href="#"><img src='+BidimgPathLatest+' alt="" style="width:160px;height:100px;"></a> ' +
                               ' <div class="caption"> ' + 
                               '<div style="height:60px;">' +
                               '<h5 style="color:Black;text-align: center;">'+fieldLatest[2]+'</h5>' +
                               '</div>' +
                               ' <p id="P'+f+'" style="color:red; font-weight:bold;"></p> ' +
                               ' <p style="color:Black"><img src='+DepimgPathLatest+' height="20" width="20" /> '+fieldLatest[4]+'</p> ' +
                               '<p style="color:Black;">OrderID: '+fieldLatest[9]+'</p>' + ''
                            textLatestUl += htmlcodeLatestUl;
                            if(fieldLatest[10] == "Edit Bid"){
                                htmlcodeLatestUl = '<h4 style="text-align:center"><input type="Button" id='+ListIdLatest+' class="btn btn-warning" onclick="Bid_ClickEvent(this.id)" value="'+fieldLatest[10]+'"/></h4>' + ''
                                textLatestUl += htmlcodeLatestUl;
                            }
                            if(fieldLatest[10] == "Pending Bid"){
                                htmlcodeLatestUl = '<h4 style="text-align:center"><input type="Button" id='+ListIdLatest+' class="btn btn-danger" onclick="Bid_ClickEvent(this.id)" value="'+fieldLatest[10]+'"/></h4>' + ''
                                textLatestUl += htmlcodeLatestUl;
                            }
                            if(fieldLatest[10] == "Bid"){
                                htmlcodeLatestUl = '<h4 style="text-align:center"><input type="Button" id='+ListIdLatest+' class="btn btn-primary" onclick="Bid_ClickEvent(this.id)" value="'+fieldLatest[10]+'"/></h4>' + ''
                                textLatestUl += htmlcodeLatestUl;
                            }
                            htmlcodeLatestUl = '</div>' +
                            '</div>' +
                            '</li>' + ''
                            textLatestUl += htmlcodeLatestUl;


                            //if(textLatestUl!="" && textLatestUl!=null){
                            //    SearchedAllLatestBids= i+",";
                            //}
                        }
                        document.getElementById("myUl").innerHTML = textLatestUl;
                    }  

                
            }
        

            else{
              htmlcodeLatest =
              '<div class="item" id='+i+'>'+
              '<ul class="thumbnails" id="myUl">'+
              '</ul>'+
			  '</div>'+ ''
                textLatest += htmlcodeLatest;
            }
        
          }
        }

       //---------------------Count Down Timer For MovingSlider (Latest Bids)
       var z = setInterval(function () {
           for (var v = 1; v <= DataListLatest.length; v++) 
           {
               //  if(SearchedAllLatestBids.includes(DataListLatest[v-1])){
               var fieldDateLatestS = "";
               fieldDateLatestS = DataListLatest[v - 1].split('-');
               var endDateLatest = new Date(fieldDateLatestS[8]);
               var ti = endDateLatest;
               var time01   =  (
                         ("00" + (ti.getMonth() + 1)).slice(-2) + " " + 
                         ("00" + ti.getDate()).slice(-2) + " ," + 
                         ti.getFullYear() + " " + 
                         ("00" + ti.getHours()).slice(-2) + ":" + 
                         ("00" + ti.getMinutes()).slice(-2) + ":" + 
                         ("00" + ti.getSeconds()).slice(-2)
                     );
               var countDownDate1 = new Date(time01).getTime();
               // Get todays date and time
               var now1 = new Date().getTime();
               // Find the distance between now an the count down date
               var distance = countDownDate1 - now1;
               // Time calculations for days, hours, minutes and seconds
               var days1 = Math.floor(distance / (1000 * 60 * 60 * 24));
               var hours1 = Math.floor((distance % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
               var minutes1 = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));
               var seconds1 = Math.floor((distance % (1000 * 60)) / 1000);

               if(document.getElementById('P'+v+'') != null){
                   // Display the result in the element with id="demo"
                   document.getElementById('P'+v+'').innerHTML = days1 + "d " + hours1 + "h " + minutes1 + "m " + seconds1 + "s "; 
               }
                       
               // If the count down is finished, write some text (You can Fire the Event to Reload Page to disable the Bid when it will expired)
               if (distance < 0) {
                   clearInterval(z);
                   if(document.getElementById('P'+v+'') != null){
                       document.getElementById('P'+v+'').innerHTML = "EXPIRED";}
               }
            }
        }, 1000);
</script>



    <!------------------------------------Load the details to be searched------------------------------------------------->



     <script type="text/javascript">
    var SupplierMainCategory01 = <%= getJsonSupplierMainCategory() %>
    var SupplierMainCategorySub01 = <%= getJsonSupplierMainCategoryAndSubCat() %>
    var SupplierCompanyList = <%= getJsonComapanyList() %>
     LoadComapnies();
     GetMainCategoryListSearch();
    function LoadComapnies(){
        var textComp = "";
        var fieldComp = "";
        var ListIdComp = "";
        var DepimgPath = "";
        debugger;
        for (var i = 1; i <= SupplierCompanyList.length; i++) {
            fieldComp = SupplierCompanyList[i - 1].split('-');
            ListIdComp = fieldComp[0]+"_"+fieldComp[1];
            if(fieldComp[2] != "")
            {
                 DepimgPath = fieldComp[2].replace('~/','');
            }
            else{
                 DepimgPath = "images/noimage.png";
            }
            var htmlcodeComp =
                ' <li> ' +
                ' <a href="#"><p style="color:Black;"> ' +
                ' <input type="checkbox" class="messageCheckbox"  name="vehicle" value='+fieldComp[0]+' style="margin-top:4px;" class="pull-right"> ' +
                ' <img src='+DepimgPath+' height="20" width="20" />&nbsp;&nbsp;'+fieldComp[1]+
                ' </p> ' +
                ' </a> ' + 
                ' </li> ' + ''
            textComp += htmlcodeComp;
            }
            document.getElementById('sideManu1').innerHTML = textComp;
      }


    
     
      

      function GetMainCategoryListSearch()
      {
         var textCompSup = "";
         var fieldCompSup = "";
         var ListIdCompSup = "";
         var DepimgPathSup = "";
         var htmlcodeCompSup = "" ;
         var fieldCompSupSub = "";
         var ListIdCompSupSub = "";
         for (var z = 1; z <= SupplierMainCategory01.length; z++) {
            fieldCompSup = SupplierMainCategory01[z - 1].split('-');
            ListIdCompSup = fieldCompSup[0]+"_"+fieldCompSup[1];
            if(z == 1){
                htmlcodeCompSup =
                ' <li class="subMenu open"><a><i class="icon-chevron-right"></i>'+fieldCompSup[1]+'</a> ' +
                ' <ul id="myUl'+z+'" class="nav nav-tabs nav-stacked">' + ''
                textCompSup += htmlcodeCompSup;
            }
            if(z != 1){
                htmlcodeCompSup =
                ' <li class="subMenu"><a><i class="icon-chevron-right"></i>'+fieldCompSup[1]+'</a> ' +
                ' <ul id="myUl'+z+'" style="display:none" class="nav nav-tabs nav-stacked">' + ''
                textCompSup += htmlcodeCompSup;
            }
            
            for (var b = 1; b < SupplierMainCategorySub.length; b++) {
            fieldCompSupSub = SupplierMainCategorySub[b - 1].split('-');
            ListIdCompSupSub = fieldCompSupSub[0]+"_"+fieldCompSupSub[1];
                if(fieldCompSupSub[0] == fieldCompSup[0]){
                    htmlcodeCompSup =
                   ' <li><a><i class="icon-dash-right"></i>'+ ' <input type="checkbox" name="vehicle" class="messageCheckboxitem" value='+fieldCompSupSub[0]+"_"+fieldCompSupSub[1]+' style="margin-top:4px;" class="pull-right"> ' +fieldCompSupSub[2]+ '' +
                   
                   '</a></li> ' + ''
                   textCompSup += htmlcodeCompSup; 
                }
            }
             htmlcodeCompSup =  
                               ' </ul>' + 
                               ' </li> ' + ''
             textCompSup += htmlcodeCompSup; 
            }
         document.getElementById('SearchCategory').innerHTML = textCompSup;
      }

      
 </script>


        <!--Load Item Types-->

     <script type="text/javascript">
      var SupplierMainCategory = <%= getJsonSupplierMainCategory() %>
      var SupplierMainCategorySub = <%= getJsonSupplierMainCategoryAndSubCat() %>
      LoadMainCategory();
    function LoadMainCategory(){
        var text = "";
        var field = "";
        var ListId = "";
        var textSub = "";
        var fieldSub = "";
        var ListIdSub = "";
        var htmlcode = "";
        var htmlcodeSub = "";
        debugger;
        for (var i = 1; i <= SupplierMainCategory.length; i++) {
            field = SupplierMainCategory[i - 1].split('-');
            ListId = field[0]+"_"+field[1];
            htmlcode =
                ' <li class="subMenu" id='+field[0]+'><a> '+field[1]+'</a> ' + ''
			   
            text += htmlcode;
            for (var x = 1; x < SupplierMainCategorySub.length; x++) {
            fieldSub = SupplierMainCategorySub[x - 1].split('-');
            ListIdSub = fieldSub[0]+"_"+fieldSub[1];
                if(fieldSub[0] == field[0]){
                    htmlcode =
                   ' <ul id="myUl'+i+'">' +
                   ' <li><a href="#"><i class="icon-chevron-right"></i>'+fieldSub[2]+'</a></li> ' + 
                   ' </ul>' + ''
                   text += htmlcode; 
                }
            }
            htmlcode =  ' </li> ' + ''
             text += htmlcode; 
         document.getElementById("sideManu").innerHTML = text; 
        }
    }

    

 
 </script>
</asp:Content>
