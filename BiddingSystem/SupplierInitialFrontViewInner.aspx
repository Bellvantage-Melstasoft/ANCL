<%@ Page Title="" Language="C#" MasterPageFile="~/SupplierLoadingWebUIInner.Master" AutoEventWireup="true" CodeBehind="SupplierInitialFrontViewInner.aspx.cs" Inherits="BiddingSystem.SupplierInitialFrontViewInner" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <style>
         .product-men {
    margin-top: 0.5em;
    margin-right: 0.25em;
    margin-left:0.25em;
}
@media (min-width: 992px) {
        .col-md-3 {
            width: 24%;
            /* height: 14%; */
        }
}



@media (min-width: 768px) {
  .container {
    width: 750px;
  }
}
@media (min-width: 992px) {
  .container {
    width: 970px;
  }
}
@media (min-width: 1200px) {
  .container {
    width: 1270px;
  }
}

.sidepanel  {
    width: 0;
    position: absolute;
    z-index: 1;
    height: 550px;
    top: 210px;
    right: 0;
    /*background-color: #cad1e0;*/
    background-color:#cad1e0;
    overflow-x: hidden;
    transition: 0.5s;
    padding-top: 60px;
    text-align:left;
    border-color:#336699;
    border-width:2px;
}
.sidepanel li{
    font-family:
    font-size:small;
    color:black;
    padding-left:5px;
    font-family:Arial;
}

.sidepanel a {
    padding: 8px 8px 8px 32px;
    text-decoration: none;
    font-size: 25px;
    color: #336699;
    display: block;
    transition: 0.3s;
    padding-left:2px;
    font-family:Arial;
    font-size:medium;
}

.sidepanel a:hover {
    color: #f1f1f1;
}

.sidepanel .closebtn {
    position: absolute;
    top: 0;
    right: 25px;
    font-size: 36px;
}

.openbtn {
    font-size: 20px;
    cursor: pointer;
    background-color: #111;
    color: white;
    padding: 10px 15px;
    border: none;
}

.openbtn:hover {
    background-color:#444;
}

/*@media screen and (max-width: 600px) {
    .column {
        width: 100%;
        height: auto;
    }*/
</style>

<%--   

    <script src="SupplierUIResources/js/jquery-2.1.4.min.js"></script>
    <!-- //jquery -->

	<!-- popup modal (for signin & signup)-->
	<script src="SupplierUIResources/js/jquery.magnific-popup.js"></script>
    <script src="SupplierUIResources/js/minicart.js"></script>
    <!-- for bootstrap working -->
	<script src="SupplierUIResources/js/bootstrap.js"></script>--%>
	<!-- //for bootstrap working -->

<form runat="server" id="form1">
<div class="services-breadcrumb" style="/*background-color: #005383*/background-color:#336699;">
		<div class="agile_inner_breadcrumb" style="height:70px">
			<div class="container">



                <div class="row">
                 <div class="col-md-8">

                      <div class="top_nav_left" >
				<nav class="navbar navbar-default" >
					<div class="container-fluid">
						<!-- Brand and toggle get grouped for better mobile display -->
						<div class="navbar-header">
							<button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1"
							    aria-expanded="false">
								<span class="sr-only">Toggle navigation</span>
								<span class="icon-bar"></span>
								<span class="icon-bar"></span>
								<span class="icon-bar"></span>
							</button>
						</div>

						<!-- Collect the nav links, forms, and other content for toggling -->
						<div class="collapse navbar-collapse menu--shylock" style="background:#336699;float:left; margin-top:10px" id="bs-example-navbar-collapse-1">
							<ul class="nav navbar-nav menu__list">
								<li class="active">
									<a class="nav-stylehead" style="color:white;" href="SupplierInitialFrontView.aspx">Home
										<span class="sr-only">(current)</span>
									</a>
								</li>
								<li class="">
                                   

                                    <a class="nav-stylehead" style="color:white;" href="PendingBidSubmission.aspx">Pending Bids</a>
									<%--<a class="nav-stylehead" href="SupplierAboutUs.aspx">About Us</a>--%>
								</li>

								<li class="">
                                    <a class="nav-stylehead" style="color:white;" href="SupplierRequestToCompany.aspx">Request Company</a>
									<%--<a class="nav-stylehead" href="#">Faqs</a>--%>
								</li>
								<li class="">
                                    <a class="nav-stylehead" style="color:white;" href="SupplierReceivedPO.aspx">Received PO</a>
									<%--<a class="nav-stylehead" href="SupplierContactUs.aspx">Contact</a>--%>
								</li>


                                <li class="">
                                    <li class=""><a class="nav-stylehead" style="color:white;" href="SupplierProfile.aspx">Update Profile</a></li>
									<%--<a class="nav-stylehead" href="SupplierContactUs.aspx">Contact</a>--%>
								</li>
							</ul>
						</div>
					</div>
				</nav>
			</div>



                 </div>
                 <div class="col-md-2">


                      <div <%--id="c2" --%>class="agileits_search" <%--style="margin-left:300px; width:800px;margin-top:0px;"--%> >
					    <%--<form action="#" method="post">--%>
						<input name="Search" type="search" id="myInput" placeholder="Search By Item Name" required=""style="width:210px;">
                       </div>

                     </div>
                 <div class="col-md-1">
						<button type="button" class="btn btn-default" onclick="filterBids();displayLatestBids();" aria-label="Left Align"style="height:40px;margin-top:17px">
							<span class="fa fa-search" aria-hidden="true"> </span>
						</button>
                        
					   <%-- </form>--%>
		              </div>

                 
                <div class="col-md-1">

                      <button type="button" class="btn btn-primary" style="margin-top:17px; margin-right:60px;height:40px;width:auto;/*float:right;*/" onclick="openNav()">
                        Filter</button>

                </div>

                </div>

              
                <%--<div class="agileits_search">
					<form action="#" method="post">
						<input name="Search" type="search" placeholder="Search By Item Name" required="">
						<button type="submit" class="btn btn-default" aria-label="Left Align">
							<span class="fa fa-search" aria-hidden="true"> </span>
						</button>
					</form>
				</div>--%>
				<%--<ul class="w3_short">
					<li>
						<a href="SupplierInitialFrontView.aspx">Home</a>
						<i>|</i>
					</li>
				</ul>--%>

               <%-- <button type="button" class="btn btn-default" onclick="" aria-label="Left Align"style="height:40px;width:100px;margin-bottom:10px; float:left;">
							<span class="" aria-hidden="true"> </span>
						</button>--%>
     

        <!--Search options button------------------------------------------>
               
           
              


                  
               <%-- <asp:Button ID="Button1" runat="server" Text="Advanced Search" CssClass="pull-right btn btn-warning" style=" margin-top: -40px; "/>--%>
			</div>
               
		</div>
	</div>
  <br />

    <!------Search Field in the side bar---------------------------------------->

     <div id="sidebar" class="span2" style="width:200px;float:left;">
		<%--<div class="well well-small"><a id="myCart" href="PendingBidSubmission.aspx">Pending Bid Count  <span style=" border-radius: 4px;" class="badge badge-warning pull-right" id="pendingcnt"></span></a></div>
        <div class="well well-small"><a id="myCart" href="#">Total Bid Count   <span style=" border-radius: 4px;" class="badge badge-warning pull-right">0</span></a></div>
        <div class="well well-small"><a id="myCart" href="#">Approved Bids   <span style=" border-radius: 4px;" class="badge badge-warning pull-right">0</span></a></div>
      --%> <%-- <ul id="sideManu" class="nav nav-tabs nav-stacked">
			
		</ul>--%>


     
   <br />
        
    <!--Search field -->
         

         






    <%--   <div style="border:none; padding:2px;border-radius:3px; background-color:#cad1e0;margin-left:5px" >
       <div ><span><h4><i class="icon-search" style=" margin-top: 6px; margin-left: 38px; "></i>&nbsp;&nbsp; Search</h4></span></div>
        <ul   class="nav nav-tabs nav-stacked">
			<li class="subMenu" ><a >  COMPANY WISE</a>
				<ul id="sideManu1">
				
				</ul>
			</li>
			
		</ul>
        <br />
        <ul  class="nav nav-tabs nav-stacked">
			<li class="subMenu " ><a >ITEM TYPE</a>
				<ul   class="nav nav-tabs nav-stacked" id="SearchCategory">
				             
				</ul>
			</li>
			
		</ul>
           <br />
        <ul  class="nav nav-tabs nav-stacked">
			<li class="subMenu " style=" text-align: center; ">
				<button type="button" onclick="getValue();displayLatestBids();" class="btn btn-danger">Search </button>
			</li>
			
		</ul>

        </div>--%>
		<br/>
	</div>




       <div id="mySidepanel" class="sidepanel">
          <a href="javascript:void(0)" class="closebtn" onclick="closeNav()">×</a>
                <%-- <a href="#">About</a>
                 <a href="#">Services</a>
                 <a href="#">Clients</a>
                 <a href="#">Contact</a>--%>


     <div <%--style="padding:2px;border-radius:3px;"--%>>
      <%-- <div ><span><h4><i class="icon-search" style="margin-top: 7px; margin-left: 38px; "></i>&nbsp;&nbsp; Search</h4></span></div>--%>
          <ul  <%-- class="nav nav-tabs nav-stacked"--%>>



			<li class="subMenu"><a style="margin-left:2px">  COMPANY WISE</a>
				<ul id="sideManu1" <%--style="display:none"--%> <%--class="nav nav-tabs nav-stacked"--%>>
				
				</ul>
			</li>
			
		</ul>
        <br />
        <ul  <%--class="nav nav-tabs nav-stacked"--%>>
			<li ><a style="margin-left:2px">ITEM TYPE</a>
				<ul class="nav nav-tabs nav-stacked"  id="SearchCategory">
				             
				</ul>
			</li>
			
		</ul>
           <br />
        <ul  class="nav nav-tabs nav-stacked">
			<li class="subMenu " style=" text-align: center; ">
				<button type="button" onclick="getValue();displayLatestBids();" class="btn btn-danger" style="width:200px;">Search </button>
			</li>
			
		</ul>
      </div>
     </div>


    <!----------Side bar ends--------------------------------------------------->

      <div  id="projects">
		<div class="container">
            <h3 class="title-w3l" >Latest Bids</h3>
            <br />
            
           
			<!-- tittle heading -->
			<%--<h3 class="tittle-w3l">Special Offers
				<span class="heading-style">
					<i></i>
					<i></i>
					<i></i>
				</span>
			</h3>--%>
			<!-- //tittle heading -->
			<div class="content-bottom-in">
                  
				<ul id="flexiselDemo1">
					
				</ul>
			</div>
		</div>
	</div>
	<!-- top Products -->
	<div class="product-sec1">
       
		<div class="container" >
              <h3 class="title-w3l" >All Bids</h3>
            </br>
			<!-- tittle heading -->
			<%--<h3 class="tittle-w3l">Our Top Products
				<span class="heading-style">
					<i></i>
					<i></i>
					<i></i>
				</span>
			</h3>--%>
			<!-- //tittle heading -->
			<!-- product left -->
			<!-- //product left -->
			<!-- product right -->
			<div class="agileinfo-ads-display col-md-12">
				<div class="wrapper">
                   <%-- <div class="product-sec1 product-sec2">
						
						<div class="col-xs-5 bg-right-nut">
							<img src="SupplierUIResources/images/MitsubishiMontero2003.png" alt="" style=" width: 390px; margin-left: 75px; ">
						</div>
						<div class="clearfix"></div>
					</div>--%>
					<!-- first section (nuts) -->
					<div id="product-sec1" >
                        <!-- second section (nuts special) -->
					
					    <!-- //second section (nuts special) -->
						<%--<h3 class="heading-tittle">Nuts</h3>--%>

						<%--<div class="clearfix"></div>--%>
					</div>
                    <div id="progress" style="display:none">
                      <h4>Loading...</h4>
                     </div>
				</div>
			</div>
			<!-- //product right -->
		</div>
	</div>
	<!-- //top products -->
</form> 
    
    <%--  <script>

        var pageSize = 4;
        var pageIndex = 1;

      
          //  var a = setInterval(function () {
       // $(widows).load(function (){
        $(document).ready(function () {
            LoadAllBids();

            $(window).scroll(function () {
                if ($(window).scrollTop() == 
                   $(document).height() - $(window).height()) {
                    LoadAllBids();
                }
            });
        });

      


        function LoadAllBids() {
         
            var obj = {};
            obj.pageIndex=pageIndex;
            obj.pageSize=pageSize;
         
            var jsonText = JSON.stringify(obj);
           
           

            $.ajax({
                type: "POST",
                url: "SupplierInitialFrontViewInner.aspx/getJsonSearchBids1",
                data:jsonText,
                contentType: "application/json",
                dataType: "json",
                success: function (result) {

                    if (result != null) {





                        var DataList1=result.d;
                        //  Console.log(result1);

                        // var result1= $(result).text().split('|');
                        //  flexelChange();
                  
                        var text = "";
                        var field = "";
                        var BidimgPath = "";
                        var DepimgPath = "";
                        var ListId = "";
                        var htmlcode = "" ;
                        var BidTime3="";
                        var BidTime2="";
                        var remainingTimeE="";




                        for(var i=0; i<DataList1.length;i++)
                        {
                            var obj=DataList1[i];
                            ListId = obj.SubCategoryID+"_"+obj.BidID+"_"+obj.itemID;

                            if(obj.DisplayImg != "")
                            {
                                BidimgPath = obj.DisplayImg.replace('~/','');
                            }
                            else{

                                BidimgPath ="LoginResources/images/noItem.png";
                                //BidimgPath = "images/noimage.png";
                            }
                            if(obj.DepImgPath != "")
                            {
                                DepimgPath = obj.DepImgPath.replace('~/','');
                            }
                            debugger;
                            htmlcode =
                        
                                  ' <div class="col-md-3 product-men" id='+ListId+' style="border-style: groove;padding:2px;background-color:white;"> ' +
                                  ' <div class="men-pro-item simpleCart_shelfItem"> ' +
                                  ' <div class="men-thumb-item"> ' +
                                  ' <img src='+BidimgPath+' alt="" style="height:136px"> ' +
                                  ' <div class="men-cart-pro"> ' +
                                  ' <div class="inner-men-cart-pro"> ' +
                                  ' <a href="single.html" class="link-product-add-cart">BID NOW</a> ' +
                                  ' </div> ' +
                                  ' </div> ' +
				  
                                  ' </div> ' +
                                  ' <div class="item-info-product "> ' + ''
                            text += htmlcode;

                            if(obj.itemID.length > 0 && obj.itemID.length < 15)
                            {
                                htmlcode =
                                ' <h4 style=" padding-bottom: 36px; "> ' + 
                                ' <a href="single.html">'+obj.ItemName+'</a> ' + 
                                ' <h4 /> '  + ''
                                text += htmlcode;
                            }
                            if(obj.itemID.length <= 42 && obj.itemID.length > 15)
                            {
                                htmlcode =
                                ' <h4 style=" padding-bottom: 18px; "> ' + 
                                ' <a href="single.html">'+obj.ItemName+'</a> ' + 
                                ' <h4 /> '  + ''
                                text += htmlcode;
                            }
                            if(obj.itemID.length >= 43 )
                            {
                                htmlcode =
                               ' <h4> ' + 
                               ' <a href="single.html">'+obj.ItemName+'</a> ' +  
                               ' </h4> ' + ''
                                text += htmlcode;
                            }
                            htmlcode =
                            ' </h4> ' +
                            ' <div class="info-product-price"> ' +
                            ' <span class="item_price">OrderID: '+obj.BidID+'</span> ' +
                            ' <p id="demo'+i+'" style="color:red; font-weight:bold;text-align:center"></p> ' +
                            ' </div> ' +
                            ' <div class="snipcart-details top_brand_home_details item_add single-item hvr-outline-out"> ' +
                            ' <form action="#" method="post"> ' +
                            ' <fieldset> ' +
                            ' <input type="hidden" name="cmd" value="_cart" /> ' +
                            ' <input type="hidden" name="add" value="1" /> ' +
                            ' <input type="hidden" name="business" value=" " /> ' +
                            ' <input type="hidden" name="item_name" value="Almonds, 100g" /> ' +
                            ' <input type="hidden" name="amount" value="149.00" /> ' +
                            ' <input type="hidden" name="discount_amount" value="1.00" /> ' +
                            ' <input type="hidden" name="currency_code" value="USD" /> ' +
                            ' <input type="hidden" name="return" value=" " /> ' +
                            ' <input type="hidden" name="cancel_return" value=" " /> ' + ''
                            text += htmlcode;
                            if(obj.Action == "Edit Bid"){
                                htmlcode =
                                 ' <input type="button" name="submit" value="Edit Bid" class="button" id='+ListId+' onclick="Bid_ClickEvent(this.id)" value="'+obj.BidID+'"/> ' + ''
                                text += htmlcode;
                            }
                            if(obj.Action == "Pending Bid"){
                                htmlcode =
                                ' <input type="button" name="submit" value="Pending Bid" class="button" id='+ListId+' onclick="Bid_ClickEvent(this.id)" value="'+obj.BidID+'"/> ' + ''
                                text += htmlcode;
                            }
                            if(obj.Action == "Bid"){
                                htmlcode =
                               ' <input type="button" name="submit" value="Bid" class="button" id='+ListId+' onclick="Bid_ClickEvent(this.id)" value="'+obj.BidID+'"/> ' + ''
                                text += htmlcode;
                            }
                            htmlcode = 
                            ' <p style="padding-top: 4px;"></p> ' +
                            ' </fieldset> ' +
                            ' </form> ' +
                            ' </div> ' +
                            ' </div> ' +
                            ' </div> ' +
                            ' </div> ' + '' 
                            text += htmlcode;
                        }
                        htmlcode =  
                        ' <div class="clearfix"></div> ' +
                        ' </div> ' + '' 
                        text += htmlcode;
                      //  document.getElementById("product-sec1").innerHTML="";
                        document.getElementById("product-sec1").innerHTML = text;
                    
                   
               
                       
                        pageIndex++;
                    }

                   

                    
            
                },
                beforeSend : function () {
                    $("#progress").show();
                },
                complete : function () {
                    $("#progress").hide();
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





    </script>  --%>
  
    <script type="text/javascript">
        




        function searchBids() {
             var Supplier =<%=getJsonSupplier() %> ;

            //var DataListLatest = <%= getJsonBiddingItemListAllLatest() %>;
        

           var input = document.getElementById("myInput").value;

           
           var jsonText = JSON.stringify({ searchText: input, supID: Supplier });

            $.ajax({
                type: "POST",
                url: "SupplierInitialFrontViewInner.aspx/getJsonSearchBids",
                data:jsonText,
                contentType: "application/json",
                dataType: "json",
                success: function (result) {

                    var DataList1=result.d;
                  //  Console.log(result1);

                   // var result1= $(result).text().split('|');
                  //  flexelChange();
                  
                        var text = "";
                        var field = "";
                        var BidimgPath = "";
                        var DepimgPath = "";
                        var ListId = "";
                        var htmlcode = "" ;
                        var BidTime3="";
                        var BidTime2="";
                        var remainingTimeE="";




                        for(var i=0; i<DataList1.length;i++)
                        {
                            var obj=DataList1[i];
                            ListId = obj.SubCategoryID+"_"+obj.BidID+"_"+obj.itemID;

                            if(obj.DisplayImg != "")
                            {
                                BidimgPath = obj.DisplayImg.replace('~/','');
                            }
                            else{

                                BidimgPath ="LoginResources/images/noItem.png";
                                //BidimgPath = "images/noimage.png";
                            }
                            if(obj.DepImgPath != "")
                            {
                                DepimgPath = obj.DepImgPath.replace('~/','');
                            }
                            debugger;
                            htmlcode =
                        
                                  ' <div class="col-md-3 product-men" id='+ListId+' style="border-style: groove;padding:2px;background-color:white;"> ' +
                                  ' <div class="men-pro-item simpleCart_shelfItem"> ' +
                                  ' <div class="men-thumb-item"> ' +
                                  ' <img src='+BidimgPath+' alt="" style="height:136px"> ' +
                                  ' <div class="men-cart-pro"> ' +
                                  ' <div class="inner-men-cart-pro"> ' +
                                  ' <a href="single.html" class="link-product-add-cart">BID NOW</a> ' +
                                  ' </div> ' +
                                  ' </div> ' +
				  
                                  ' </div> ' +
                                  ' <div class="item-info-product "> ' + ''
                            text += htmlcode;

                            if(obj.itemID.length > 0 && obj.itemID.length < 15)
                            {
                                htmlcode =
                                ' <h4 style=" padding-bottom: 36px; "> ' + 
                                ' <a href="single.html">'+obj.ItemName+'</a> ' + 
                                ' <h4 /> '  + ''
                                text += htmlcode;
                            }
                            if(obj.itemID.length <= 42 && obj.itemID.length > 15)
                            {
                                htmlcode =
                                ' <h4 style=" padding-bottom: 18px; "> ' + 
                                ' <a href="single.html">'+obj.ItemName+'</a> ' + 
                                ' <h4 /> '  + ''
                                text += htmlcode;
                            }
                            if(obj.itemID.length >= 43 )
                            {
                                htmlcode =
                               ' <h4> ' + 
                               ' <a href="single.html">'+obj.ItemName+'</a> ' +  
                               ' </h4> ' + ''
                                text += htmlcode;
                            }
                            htmlcode =
                            ' </h4> ' +
                            ' <div class="info-product-price"> ' +
                            ' <span class="item_price">OrderID: '+obj.BidID+'</span> ' +
                            ' <p id="demo'+i+'" style="color:red; font-weight:bold;text-align:center"></p> ' +
                            ' </div> ' +
                            ' <div class="snipcart-details top_brand_home_details item_add single-item hvr-outline-out"> ' +
                            ' <form action="#" method="post"> ' +
                            ' <fieldset> ' +
                            ' <input type="hidden" name="cmd" value="_cart" /> ' +
                            ' <input type="hidden" name="add" value="1" /> ' +
                            ' <input type="hidden" name="business" value=" " /> ' +
                            ' <input type="hidden" name="item_name" value="Almonds, 100g" /> ' +
                            ' <input type="hidden" name="amount" value="149.00" /> ' +
                            ' <input type="hidden" name="discount_amount" value="1.00" /> ' +
                            ' <input type="hidden" name="currency_code" value="USD" /> ' +
                            ' <input type="hidden" name="return" value=" " /> ' +
                            ' <input type="hidden" name="cancel_return" value=" " /> ' + ''
                            text += htmlcode;
                            if(obj.Action == "Edit Bid"){
                                htmlcode =
                                 ' <input type="button" name="submit" value="Edit Bid" class="button" id='+ListId+' onclick="Bid_ClickEvent(this.id)" value="'+obj.BidID+'"/> ' + ''
                                text += htmlcode;
                            }
                            if(obj.Action == "Pending Bid"){
                                htmlcode =
                                ' <input type="button" name="submit" value="Pending Bid" class="button" id='+ListId+' onclick="Bid_ClickEvent(this.id)" value="'+obj.BidID+'"/> ' + ''
                                text += htmlcode;
                            }
                            if(obj.Action == "Bid"){
                                htmlcode =
                               ' <input type="button" name="submit" value="Bid" class="button" id='+ListId+' onclick="Bid_ClickEvent(this.id)" value="'+obj.BidID+'"/> ' + ''
                                text += htmlcode;
                            }
                            htmlcode = 
                            ' <p style="padding-top: 4px;"></p> ' +
                            ' </fieldset> ' +
                            ' </form> ' +
                            ' </div> ' +
                            ' </div> ' +
                            ' </div> ' +
                            ' </div> ' + '' 
                            text += htmlcode;
                        }
                    htmlcode =  
                    ' <div class="clearfix"></div> ' +
                    ' </div> ' + '' 
                    text += htmlcode;
                    document.getElementById("product-sec1").innerHTML="";
                    document.getElementById("product-sec1").innerHTML = text;
                    
                   
                        


               

                    
            
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
    </script>

    <script type="text/javascript">

        function LoadAllBidsSearch( ){
            var text = "";
            var field = "";
            var BidimgPath = "";
            var DepimgPath = "";
            var ListId = "";
            var htmlcode = "" ;
            var BidTime3="";
            var BidTime2="";
            var remainingTimeE="";
            

           
           
            var DataList1=arguments;
            //var DataList1 = DataList[i - 1].split('-');

            //htmlcode =
            //      ' <div class="product-sec1" style="background-color:white"> ' + '' 
            //text += htmlcode;
            for (var i = 1; i <= DataList1.length; i++) {
                field = DataList1[i - 1].split('-');
                ListId = field[0]+"_"+field[1]+"_"+field[9];
                if(field[6] != "")
                {
                    BidimgPath = field[6].replace('~/','');
                }
                else{

                    BidimgPath ="LoginResources/images/noItem.png";
                    //BidimgPath = "images/noimage.png";
                }
                if(field[5] != "")
                {
                    DepimgPath = field[5].replace('~/','');
                }
                debugger;
                htmlcode =
                        
                      ' <div class="col-md-3 product-men" id='+ListId+' style="border-style: groove;padding:2px;background-color:white;"> ' +
                      ' <div class="men-pro-item simpleCart_shelfItem"> ' +
                      ' <div class="men-thumb-item"> ' +
                      ' <img src='+BidimgPath+' alt="" style="height:136px"> ' +
                      ' <div class="men-cart-pro"> ' +
                      ' <div class="inner-men-cart-pro"> ' +
                      ' <a href="single.html" class="link-product-add-cart">BID NOW</a> ' +
                      ' </div> ' +
                      ' </div> ' +
				  
                      ' </div> ' +
                      ' <div class="item-info-product "> ' + ''
                text += htmlcode;

                if(field[2].length > 0 && field[2].length < 15)
                {
                    htmlcode =
                    ' <h4 style=" padding-bottom: 36px; "> ' + 
                    ' <a href="single.html">'+field[2]+'</a> ' + 
                    ' <h4 /> '  + ''
                    text += htmlcode;
                }
                if(field[2].length <= 42 && field[2].length > 15)
                {
                    htmlcode =
                    ' <h4 style=" padding-bottom: 18px; "> ' + 
                    ' <a href="single.html">'+field[2]+'</a> ' + 
                    ' <h4 /> '  + ''
                    text += htmlcode;
                }
                if(field[2].length >= 43 )
                {
                    htmlcode =
                   ' <h4> ' + 
                   ' <a href="single.html">'+field[2]+'</a> ' +  
                   ' </h4> ' + ''
                    text += htmlcode;
                }
                htmlcode =
                ' </h4> ' +
                ' <div class="info-product-price"> ' +
                ' <span class="item_price">OrderID: '+field[9]+'</span> ' +
                ' <p id="demo'+i+'" style="color:red; font-weight:bold;text-align:center"></p> ' +
                ' </div> ' +
                ' <div class="snipcart-details top_brand_home_details item_add single-item hvr-outline-out"> ' +
                ' <form action="#" method="post"> ' +
                ' <fieldset> ' +
                ' <input type="hidden" name="cmd" value="_cart" /> ' +
                ' <input type="hidden" name="add" value="1" /> ' +
                ' <input type="hidden" name="business" value=" " /> ' +
                ' <input type="hidden" name="item_name" value="Almonds, 100g" /> ' +
                ' <input type="hidden" name="amount" value="149.00" /> ' +
                ' <input type="hidden" name="discount_amount" value="1.00" /> ' +
                ' <input type="hidden" name="currency_code" value="USD" /> ' +
                ' <input type="hidden" name="return" value=" " /> ' +
                ' <input type="hidden" name="cancel_return" value=" " /> ' + ''
                text += htmlcode;
                if(field[10] == "Edit Bid"){
                    htmlcode =
                     ' <input type="button" name="submit" value="Edit Bid" class="button" id='+ListId+' onclick="Bid_ClickEvent(this.id)" value="'+field[10]+'"/> ' + ''
                    text += htmlcode;
                }
                if(field[10] == "Pending Bid"){
                    htmlcode =
                    ' <input type="button" name="submit" value="Pending Bid" class="button" id='+ListId+' onclick="Bid_ClickEvent(this.id)" value="'+field[10]+'"/> ' + ''
                    text += htmlcode;
                }
                if(field[10] == "Bid"){
                    htmlcode =
                   ' <input type="button" name="submit" value="Bid" class="button" id='+ListId+' onclick="Bid_ClickEvent(this.id)" value="'+field[10]+'"/> ' + ''
                    text += htmlcode;
                }
                htmlcode = 
                ' <p style="padding-top: 4px;"></p> ' +
                ' </fieldset> ' +
                ' </form> ' +
                ' </div> ' +
                ' </div> ' +
                ' </div> ' +
                ' </div> ' + '' 
                text += htmlcode;
            }
            htmlcode =  
            ' <div class="clearfix"></div> ' +
            ' </div> ' + '' 
            text += htmlcode;
            document.getElementById("product-sec1").innerHTML="";
            document.getElementById("product-sec1").innerHTML = text;
        }

       

        


    </script>
    
      
  
    <script type="text/javascript">


             // --------------To filter the datalist when searching----------

        function filterBids() {

            var DataListLatest = <%= getJsonBiddingItemListAllLatest() %>;
            var DataList = <%= getJsonBiddingItemListAll() %>;

           

           // flexelChange();

            var input, filter, ul, li, a, i;
            var DataListLatestSearch=[];
            var DataListSearch=[];

            input = document.getElementById("myInput").value;
            filter = input.toUpperCase();
            //ul = document.getElementById("myUL");
            //li = ul.getElementsByTagName("li");

            if(input!=null){
                //for (i = 1; i <= DataListLatest.length; i++) {
                //    var fieldLatest = DataListLatest[i - 1].split('-');
                //    if(fieldLatest[2]!=null){
                //        var a = fieldLatest[2];

                //    }
                //    var SearchValue=a.toUpperCase();
                //    if (SearchValue.indexOf(filter) > -1) {
                //        var nValue=DataListLatest[i-1];
                //        DataListLatestSearch.push(nValue);
                //    } else {

                        
                   
                       
                //    }
                //}
                for (i = 1; i <= DataList.length; i++) {
                    var fieldLatest = DataList[i - 1].split('-');
                    if(fieldLatest[2]!=null){
                        var b = fieldLatest[2];}
                    var SearchValue1=b.toUpperCase();
                    if (SearchValue1.indexOf(filter) > -1) {
                        var mValue=DataList[i-1];
                        DataListSearch.push(mValue);
                    } else {
                   
                        
                       
                    }
                }

                
               

             //   DataListLatest =DataListLatestSearch;
                DataList = DataListSearch;


             //   LoadAllBidsLatestSearch();
                LoadAllBidsSearch();

            }else{

                LoadAllBidsLatest();
                LoadAllBids();

            }
        


            //function LoadAllBidsLatestSearch()
            //{
    
            //    var textLatestUl = "";
            //    var textLatest = "";
            //    var fieldLatest = "";
            //    var BidimgPathLatest = "";
            //    var DepimgPathLatest = "";
            //    var ListIdLatest = "";
            //    var htmlcodeLatest = "" ;
            //    var BidTime1="";
            //    var BidTime2="";
            //    var remainingTime="";
            //    document.getElementById("flexiselDemo1").innerHTML="";
                
               

           

            //    var rounds = ((DataListLatest.length))/4;
            //    var valueOfLoopRounds = (Math.ceil(rounds));


            //    for (var i = 1; i <= valueOfLoopRounds; i++) {
            //        if(i == 1){
            //            for (var f = 1; f <= DataListLatest.length; f++) {

            //                textLatest="";
            //                fieldLatest = DataListLatest[f - 1].split('-');
            //                ListIdLatest = fieldLatest[0]+"_"+fieldLatest[1]+"_"+fieldLatest[9];
            //                if(fieldLatest[6] != "")
            //                {
            //                    BidimgPathLatest = fieldLatest[6].replace('~/','');
            //                }
            //                else{
            //                    BidimgPathLatest ="LoginResources/images/noItem.png";
            //                   // BidimgPathLatest = "images/noimage.png";
            //                }
            //                if(fieldLatest[5] != "")
            //                {
            //                    DepimgPathLatest = fieldLatest[5].replace('~/','');
            //                }
            //                if(f >= 0 && f < 5 ){
            //                    htmlcodeLatest =
            //                    ' <li> ' +
            //                    ' <div class="w3l-specilamk" style="background-color:white;border-color: #005383;" > ' +
            //                    ' <div class="speioffer-agile" style="background-color:white"> ' +
            //                    ' <a href="single.html"> ' +
            //                    ' <img src='+BidimgPathLatest+' alt="" style="width:150px;height:150px"> ' +
            //                    ' </a> ' +
            //                    ' </div> ' +
            //                    ' <div class="product-name-w3l"> ' + ''
            //                    textLatest += htmlcodeLatest;
            //                    if(fieldLatest[2].length <= 30 )
            //                    {
            //                        htmlcodeLatest =
            //                           ' <h4 style=" padding-bottom: 22px; "> ' +
            //                           ' <a href="single.html">'+fieldLatest[2]+'</a> ' +
            //                           ' </h4> ' + ''
            //                        textLatest += htmlcodeLatest;
            //                    }
            //                    if(fieldLatest[2].length >= 31 )
            //                    {
            //                        htmlcodeLatest =
            //                         ' <h4> ' +
            //                         ' <a href="single.html">'+fieldLatest[2]+'</a> ' +
            //                         ' </h4> ' + ''
            //                        textLatest += htmlcodeLatest;
            //                    }
            //                    htmlcodeLatest =
            //                    ' <div class="w3l-pricehkj" style="position:inherit">' +
            //                    ' <h4 style="text-align:center;color:Red">  OrderID: '+fieldLatest[9]+'</h4> ' +
            //                    ' <p id="P'+f+'" style="color:red; font-weight:bold;text-align:center"></p> ' +
            //                    ' </div> ' +
            //                    ' <div class="snipcart-details   single-item hvr-outline-out"> ' +
            //                    ' <form action="#" method="post"> ' +
            //                    ' <fieldset> ' +
            //                    ' <input type="hidden" name="cmd" value="_cart" /> ' +
            //                    ' <input type="hidden" name="add" value="1" /> ' +
            //                    ' <input type="hidden" name="business" value=" " /> ' +
            //                    ' <input type="hidden" name="item_name" value="Aashirvaad, 5g" /> ' +
            //                    ' <input type="hidden" name="amount" value="220.00" /> ' +
            //                    ' <input type="hidden" name="discount_amount" value="1.00" /> ' +
            //                    ' <input type="hidden" name="currency_code" value="USD" /> ' +
            //                    ' <input type="hidden" name="return" value=" " /> ' +
            //                    ' <input type="hidden" name="cancel_return" value=" " /> ' + ''
            //                    textLatest += htmlcodeLatest;
            //                    if(fieldLatest[10] == "Edit Bid"){
            //                        htmlcodeLatest =
            //                         ' <input type="button" name="submit" value="Edit Bid" class="button" id='+ListIdLatest+' onclick="Bid_ClickEvent(this.id)" value="'+fieldLatest[10]+'"/> ' + ''
            //                        textLatest += htmlcodeLatest;
            //                    }
            //                    if(fieldLatest[10] == "Pending Bid"){
            //                        htmlcodeLatest =
            //                        ' <input type="button" name="submit" value="Pending Bid" class="button" id='+ListIdLatest+' onclick="Bid_ClickEvent(this.id)" value="'+fieldLatest[10]+'"/> ' + ''
            //                        textLatest += htmlcodeLatest;
            //                    }
            //                    if(fieldLatest[10] == "Bid"){
            //                        htmlcodeLatest =
            //                       ' <input type="button" name="submit" value="Bid" class="button" id='+ListIdLatest+' onclick="Bid_ClickEvent(this.id)" value="'+fieldLatest[10]+'"/> ' + ''
            //                        textLatest += htmlcodeLatest;
            //                    }
            //                    htmlcodeLatest = 
            //                    ' <span class="product-new-top">New</span> ' +
            //                    ' </fieldset> ' +
            //                    ' </form> ' +
            //                    ' </div> ' +
            //                    ' </div> ' +
            //                    ' </div> ' +
            //                    ' </li> ' + ''
            //                    textLatest += htmlcodeLatest;
            //                }
                            
            //                //document.getElementById("flexiselDemo1").innerHTML = textLatest; 
            //                $('#flexiselDemo1').append(textLatest);
            //            }
            //        }
            //    }
            //}


               
            function LoadAllBidsSearch(){
                var text = "";
                var field = "";
                var BidimgPath = "";
                var DepimgPath = "";
                var ListId = "";
                var htmlcode = "" ;
                var BidTime3="";
                var BidTime2="";
                var remainingTimeE="";


                


                //htmlcode =
                //      ' <div class="product-sec1" style="background-color:white"> ' + '' 
                //text += htmlcode;
                for (var i = 1; i <= DataList.length; i++) {
                    field = DataList[i - 1].split('-');
                    ListId = field[0]+"_"+field[1]+"_"+field[9];
                    if(field[6] != "")
                    {
                        BidimgPath = field[6].replace('~/','');
                    }
                    else{

                        BidimgPath ="LoginResources/images/noItem.png";
                        //BidimgPath = "images/noimage.png";
                    }
                    if(field[5] != "")
                    {
                        DepimgPath = field[5].replace('~/','');
                    }
                    debugger;
                    htmlcode =
                        
                          ' <div class="col-md-3 product-men" id='+ListId+' style="border-style: groove;padding:2px;background-color:white;"> ' +
                          ' <div class="men-pro-item simpleCart_shelfItem"> ' +
                          ' <div class="men-thumb-item"> ' +
                          ' <img src='+BidimgPath+' alt="" style="height:136px"> ' +
                          ' <div class="men-cart-pro"> ' +
                          ' <div class="inner-men-cart-pro"> ' +
                          ' <a href="single.html" class="link-product-add-cart">BID NOW</a> ' +
                          ' </div> ' +
                          ' </div> ' +
				  
                          ' </div> ' +
                          ' <div class="item-info-product "> ' + ''
                    text += htmlcode;

                    if(field[2].length > 0 && field[2].length < 15)
                    {
                        htmlcode =
                        ' <h4 style=" padding-bottom: 36px; "> ' + 
                        ' <a href="single.html">'+field[2]+'</a> ' + 
                        ' <h4 /> '  + ''
                        text += htmlcode;
                    }
                    if(field[2].length <= 42 && field[2].length > 15)
                    {
                        htmlcode =
                        ' <h4 style=" padding-bottom: 18px; "> ' + 
                        ' <a href="single.html">'+field[2]+'</a> ' + 
                        ' <h4 /> '  + ''
                        text += htmlcode;
                    }
                    if(field[2].length >= 43 )
                    {
                        htmlcode =
                       ' <h4> ' + 
                       ' <a href="single.html">'+field[2]+'</a> ' +  
                       ' </h4> ' + ''
                        text += htmlcode;
                    }
                    htmlcode =
                    ' </h4> ' +
                    ' <div class="info-product-price"> ' +
                    ' <span class="item_price">OrderID: '+field[9]+'</span> ' +
                    ' <p id="demo'+i+'" style="color:red; font-weight:bold;text-align:center"></p> ' +
                    ' </div> ' +
                    ' <div class="snipcart-details top_brand_home_details item_add single-item hvr-outline-out"> ' +
                    ' <form action="#" method="post"> ' +
                    ' <fieldset> ' +
                    ' <input type="hidden" name="cmd" value="_cart" /> ' +
                    ' <input type="hidden" name="add" value="1" /> ' +
                    ' <input type="hidden" name="business" value=" " /> ' +
                    ' <input type="hidden" name="item_name" value="Almonds, 100g" /> ' +
                    ' <input type="hidden" name="amount" value="149.00" /> ' +
                    ' <input type="hidden" name="discount_amount" value="1.00" /> ' +
                    ' <input type="hidden" name="currency_code" value="USD" /> ' +
                    ' <input type="hidden" name="return" value=" " /> ' +
                    ' <input type="hidden" name="cancel_return" value=" " /> ' + ''
                    text += htmlcode;
                    if(field[10] == "Edit Bid"){
                        htmlcode =
                         ' <input type="button" name="submit" value="Edit Bid" class="button" id='+ListId+' onclick="Bid_ClickEvent(this.id)" value="'+field[10]+'"/> ' + ''
                        text += htmlcode;
                    }
                    if(field[10] == "Pending Bid"){
                        htmlcode =
                        ' <input type="button" name="submit" value="Pending Bid" class="button" id='+ListId+' onclick="Bid_ClickEvent(this.id)" value="'+field[10]+'"/> ' + ''
                        text += htmlcode;
                    }
                    if(field[10] == "Bid"){
                        htmlcode =
                       ' <input type="button" name="submit" value="Bid" class="button" id='+ListId+' onclick="Bid_ClickEvent(this.id)" value="'+field[10]+'"/> ' + ''
                        text += htmlcode;
                    }
                    htmlcode = 
                    ' <p style="padding-top: 4px;"></p> ' +
                    ' </fieldset> ' +
                    ' </form> ' +
                    ' </div> ' +
                    ' </div> ' +
                    ' </div> ' +
                    ' </div> ' + '' 
                    text += htmlcode;
                }
                htmlcode =  
                ' <div class="clearfix"></div> ' +
                ' </div> ' + '' 
                text += htmlcode;
                document.getElementById("product-sec1").innerHTML="";
                document.getElementById("product-sec1").innerHTML = text;
            }

       

        }

        </script>




    


  <%--  <!-- cart-js -->
	<script src="SupplierUIResources/js/minicart.js"></script>
	<script>
	   // paypalm.minicartk.render(); //use only unique class names other than paypalm.minicartk.Also Replace same class name in css and minicart.min.js

	    paypalm.minicartk.cart.on('checkout', function (evt) {
	        var items = this.items(),
				len = items.length,
				total = 0,
				i;

	        // Count the number of each item in the cart
	        for (i = 0; i < len; i++) {
	            total += items[i].get('quantity');
	        }

	        if (total < 3) {
	            alert('The minimum order quantity is 3. Please add more to your shopping cart before checking out');
	            evt.preventDefault();
	        }
	    });
	</script>
	<!-- //cart-js -->

	<!-- price range (top products) -->
	<script src="SupplierUIResources/js/jquery-ui.js"></script>
	<script>
		//<![CDATA[
	    function displayLatestBids1(){
	        $("#slider-range").slider({
	            range: true,
	            min: 0,
	            max: 9000,
	            values: [50, 6000],
	            slide: function (event, ui) {
	                $("#amount").val("$" + ui.values[0] + " - $" + ui.values[1]);
	            }
	        });
	        $("#amount").val("$" + $("#slider-range").slider("values", 0) + " - $" + $("#slider-range").slider("values", 1));

	    } //]]>
	</script>
	<!-- //price range (top products) -->--%>




        <!-- flexisel (for special offers) -->
	<!-- flexisel (for special offers) -->

	<script type="text/javascript" >
	    //function displayLatestBids() {
	    //    $("#flexiselDemo1").flexisel({
	    //        visibleItems: 3,
	    //        animationSpeed: 1000,
	    //        autoPlay: true,
	    //        autoPlaySpeed: 3000,
	    //        pauseOnHover: true,
	    //        enableResponsiveBreakpoints: true,
	    //        responsiveBreakpoints: {
	    //            portrait: {
	    //                changePoint: 480,
	    //                visibleItems: 1
	    //            },
	    //            landscape: {
	    //                changePoint: 640,
	    //                visibleItems: 2
	    //            },
	    //            tablet: {
	    //                changePoint: 768,
	    //                visibleItems: 2
	    //            }
	    //        }
	    //    });

	    //}
	</script>
	<!-- //flexisel (for special offers) -->





    <script type="text/javascript">
        var DataListLatest = <%= getJsonBiddingItemListAllLatest() %>
       


      


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
        var BidTime1="";
        var BidTime2="";
        var remainingTime="";
        

    

        var rounds = ((DataListLatest.length))/4;
        var valueOfLoopRounds = (Math.ceil(rounds));




        for (var i = 1; i <= valueOfLoopRounds; i++) {
            if(i == 1){
            for (var f = 1; f <= DataListLatest.length; f++) {

                 fieldLatest = DataListLatest[f - 1].split('-');
                 ListIdLatest = fieldLatest[0]+"_"+fieldLatest[1]+"_"+fieldLatest[9];
                 if(fieldLatest[6] != "")
                 {
                      BidimgPathLatest = fieldLatest[6].replace('~/','');
                 }
                 else{

                     BidimgPathLatest ="LoginResources/images/noItem.png";
                     // BidimgPathLatest = "images/noimage.png";
                 }
                 if(fieldLatest[5] != "")
                 {
                      DepimgPathLatest = fieldLatest[5].replace('~/','');
                 }
              if(f >= 0 && f < 5 ){
              htmlcodeLatest =
              ' <li> ' +
    		  ' <div class="w3l-specilamk" style="height:fixed;background-color:white;border-color: #005383;" > ' +
    		  ' <div class="speioffer-agile" style="background-color:white"> ' +
    		  ' <a href="single.html"> ' +
    		  ' <img src='+BidimgPathLatest+' alt="" style="width:150px;height:150px"> ' +
    		  ' </a> ' +
    		  ' </div> ' +
    		  ' <div class="product-name-w3l"> ' + ''
              textLatest += htmlcodeLatest;
                if(fieldLatest[2].length <= 30 )
              {
    		  htmlcodeLatest =
    		     ' <h4 style=" padding-bottom: 22px; "> ' +
    	         ' <a href="single.html">'+fieldLatest[2]+'</a> ' +
    		     ' </h4> ' + ''
                 textLatest += htmlcodeLatest;
              }
              if(fieldLatest[2].length >= 31 )
              {
                htmlcodeLatest =
    		     ' <h4> ' +
    	         ' <a href="single.html">'+fieldLatest[2]+'</a> ' +
    		     ' </h4> ' + ''
                 textLatest += htmlcodeLatest;
              }
              htmlcodeLatest =
    		  ' <div class="w3l-pricehkj">' +
    		  ' <h4 style="text-align:center;color:Red">  OrderID: '+fieldLatest[9]+'</h4> ' +
    		  ' <p id="P'+f+'" style="color:red; height:50px;font-weight:bold;text-align:center"></p> ' +
    		  ' </div> ' +
    		  ' <div class="snipcart-details   single-item hvr-outline-out"> ' +
    		  ' <form action="#" method="post"> ' +
    		  ' <fieldset> ' +
    		  ' <input type="hidden" name="cmd" value="_cart" /> ' +
    		  ' <input type="hidden" name="add" value="1" /> ' +
    		  ' <input type="hidden" name="business" value=" " /> ' +
    		  ' <input type="hidden" name="item_name" value="Aashirvaad, 5g" /> ' +
    		  ' <input type="hidden" name="amount" value="220.00" /> ' +
    		  ' <input type="hidden" name="discount_amount" value="1.00" /> ' +
    		  ' <input type="hidden" name="currency_code" value="USD" /> ' +
    		  ' <input type="hidden" name="return" value=" " /> ' +
    		  ' <input type="hidden" name="cancel_return" value=" " /> ' + ''
                textLatest += htmlcodeLatest;
                if(fieldLatest[10] == "Edit Bid"){
                 htmlcodeLatest =
                  ' <input type="button" name="submit" value="Edit Bid" class="button" id='+ListIdLatest+' onclick="Bid_ClickEvent(this.id)" value="'+fieldLatest[10]+'"/> ' + ''
                 textLatest += htmlcodeLatest;
                }
                if(fieldLatest[10] == "Pending Bid"){
                  htmlcodeLatest =
                  ' <input type="button" name="submit" value="Pending Bid" class="button" id='+ListIdLatest+' onclick="Bid_ClickEvent(this.id)" value="'+fieldLatest[10]+'"/> ' + ''
                  textLatest += htmlcodeLatest;
                }
                if(fieldLatest[10] == "Bid"){
                  htmlcodeLatest =
                 ' <input type="button" name="submit" value="Bid" class="button" id='+ListIdLatest+' onclick="Bid_ClickEvent(this.id)" value="'+fieldLatest[10]+'"/> ' + ''
                 textLatest += htmlcodeLatest;
                }
    		  htmlcodeLatest = 
              ' <span class="product-new-top">New</span> ' +
    		  ' </fieldset> ' +
    		  ' </form> ' +
    		  ' </div> ' +
    		  ' </div> ' +
    		  ' </div> ' +
    		  ' </li> ' + ''
              textLatest += htmlcodeLatest;
              }
              document.getElementById("flexiselDemo1").innerHTML = textLatest; 
          }
        }
        }
        }
       
        //---------------------Count Down Timer For MovingSlider
       var z = setInterval(function () {
       for (var v = 1; v <= DataListLatest.length; v++) 
       {
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

            if(document.getElementById('P'+v+'')!=null){
                // Display the result in the element with id="demo"
                document.getElementById('P'+v+'').innerHTML = days1 + "d " + hours1 + "h " + minutes1 + "m " + seconds1 + "s ";            }
            // If the count down is finished, write some text 
            if (distance < 0) {
                clearInterval(z);
                document.getElementById('P'+v+'').innerHTML = "EXPIRED";
            }
            }
       }, 1000);


    </script>
    <!--Add a infinite scroll to the page loader-->
     <%-- <script src="~/Scripts/jquery-1.10.2.js"></script>--%>
  


    <script type="text/javascript">


   var DataList = <%= getJsonBiddingItemListAll() %>
    LoadAllBids();

        function LoadAllBids(){
            var text = "";
            var field = "";
            var BidimgPath = "";
            var DepimgPath = "";
            var ListId = "";
            var htmlcode = "" ;
            //htmlcode =
            //      ' <div class="product-sec1" style="background-color:white"> ' + '' 
            //      text += htmlcode;
            for (var i = 1; i <= DataList.length; i++) {
                field = DataList[i - 1].split('-');
                ListId = field[0]+"_"+field[1]+"_"+field[9];
                if(field[6] != "")
                {
                    BidimgPath = field[6].replace('~/','');
                }
                else{
                
                    BidimgPath ="LoginResources/images/noItem.png";

                    //  BidimgPath = "images/noimage.png";
                }
                if(field[5] != "")
                {
                    DepimgPath = field[5].replace('~/','');
                }
                debugger;
                htmlcode =
                      ' <div class="col-md-3 product-men" id='+ListId+' style="border-style: groove;padding:2px;background-color:white;"> ' +
                      ' <div class="men-pro-item simpleCart_shelfItem"> ' +
                      ' <div class="men-thumb-item"> ' +
                      ' <img src='+BidimgPath+' alt="" style="height:136px"> ' +
                      ' <div class="men-cart-pro"> ' +
                      ' <div class="inner-men-cart-pro"> ' +
                      ' <a href="single.html" class="link-product-add-cart">BID NOW</a> ' +
                      ' </div> ' +
                      ' </div> ' +
				  
                      ' </div> ' +
                      ' <div class="item-info-product "> ' + ''
                text += htmlcode;

                if(field[2].length > 0 && field[2].length < 15)
                {
                    htmlcode =
                    ' <h4 style=" padding-bottom: 36px; "> ' + 
                    ' <a href="single.html">'+field[2]+'</a> ' + 
                    ' <h4 /> '  + ''
                    text += htmlcode;
                }
                if(field[2].length <= 42 && field[2].length > 15)
                {
                    htmlcode =
                    ' <h4 style=" padding-bottom: 18px; "> ' + 
                    ' <a href="single.html">'+field[2]+'</a> ' + 
                    ' <h4 /> '  + ''
                    text += htmlcode;
                }
                if(field[2].length >= 43 )
                {
                    htmlcode =
                   ' <h4> ' + 
                   ' <a href="single.html">'+field[2]+'</a> ' +  
                   ' </h4> ' + ''
                    text += htmlcode;
                }
                htmlcode =
                ' </h4> ' +
                ' <div class="info-product-price"> ' +
                ' <span class="item_price">OrderID: '+field[9]+'</span> ' +
                ' <p id="demo'+i+'" style="color:red; font-weight:bold;text-align:center"></p> ' +
                ' </div> ' +
                ' <div class="snipcart-details top_brand_home_details item_add single-item hvr-outline-out"> ' +
                ' <form action="#" method="post"> ' +
                ' <fieldset> ' +
                ' <input type="hidden" name="cmd" value="_cart" /> ' +
                ' <input type="hidden" name="add" value="1" /> ' +
                ' <input type="hidden" name="business" value=" " /> ' +
                ' <input type="hidden" name="item_name" value="Almonds, 100g" /> ' +
                ' <input type="hidden" name="amount" value="149.00" /> ' +
                ' <input type="hidden" name="discount_amount" value="1.00" /> ' +
                ' <input type="hidden" name="currency_code" value="USD" /> ' +
                ' <input type="hidden" name="return" value=" " /> ' +
                ' <input type="hidden" name="cancel_return" value=" " /> ' + ''
                text += htmlcode;
                if(field[10] == "Edit Bid"){
                    htmlcode =
                     ' <input type="button" name="submit" value="Edit Bid" class="button" id='+ListId+' onclick="Bid_ClickEvent(this.id)" value="'+field[10]+'"/> ' + ''
                    text += htmlcode;
                }
                if(field[10] == "Pending Bid"){
                    htmlcode =
                    ' <input type="button" name="submit" value="Pending Bid" class="button" id='+ListId+' onclick="Bid_ClickEvent(this.id)" value="'+field[10]+'"/> ' + ''
                    text += htmlcode;
                }
                if(field[10] == "Bid"){
                    htmlcode =
                   ' <input type="button" name="submit" value="Bid" class="button" id='+ListId+' onclick="Bid_ClickEvent(this.id)" value="'+field[10]+'"/> ' + ''
                    text += htmlcode;
                }
                htmlcode = 
                ' <p style="padding-top: 4px;"></p> ' +
                ' </fieldset> ' +
                ' </form> ' +
                ' </div> ' +
                ' </div> ' +
                ' </div> ' +
                ' </div> ' + '' 
                text += htmlcode;
            }
            htmlcode =  
            ' <div class="clearfix"></div> ' +
            ' </div> ' + '' 
            text += htmlcode;
            document.getElementById("product-sec1").innerHTML = text;
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

          if(document.getElementById('demo'+c+'')!=null){
              // Display the result in the element with id="demo"
              document.getElementById('demo'+c+'').innerHTML = days + "d " + hours + "h " + minutes + "m " + seconds + "s ";}
                          
          // If the count down is finished, write some text 
          if (distance < 0) {
              clearInterval(x);
              document.getElementById('demo'+c+'').inneHrTML = "EXPIRED";
          }
          }
     }, 1000);


    <%-- var DataListAll = <%= getJsonSearchBidsAll() %>;

     

     var newCountDown = setInterval(function () {
         for (var c = 0; c < DataListAll.length; c++) 
         {
          

             var obj=DataListAll[c];
     
             //var fieldDate = "";
             //fieldDate = DataList[c - 1].split('-');
             var endDate = new Date(obj.EndDate);
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
                 // Display the result in the element with id="demo"
                 document.getElementById('demo'+c+'').innerHTML = days + "d " + hours + "h " + minutes + "m " + seconds + "s ";}
                          
             // If the count down is finished, write some text 
             if (distance < 0) {
                 clearInterval(x);
                 document.getElementById('demo'+c+'').innerHTML = "EXPIRED";
             }
         }
     }, 1000);--%>


    function Bid_ClickEvent(input)
    {
        var Supplier =<%=getJsonSupplier() %> ;
        debugger;
        var ratus = input;
            var jsonText = JSON.stringify({ data: ratus, sup: Supplier });
            $.ajax({
                type: "POST",
                url: "SupplierInitialFrontViewInner.aspx/GetBidPendingOrNot?data="+ratus,
                data: jsonText,
                contentType: "application/json",
                dataType: "json",
                success: function (msg) {
                    if (msg.d.length > 0) {
                        if(msg.d[0].CountPending >= "1"){
                           window.location.replace("SupplierBidDetailInner.aspx?Info="+input+"&Status=P");
                        }
                        else if(msg.d[0].AlreadyBidCount == "1"){
                            window.location.replace("SupplierSubmitBidInner.aspx?Info="+input+"&Status=A");
                        }
                        else if(msg.d[0].AlreadyBidCount == "0" && msg.d[0].CountPending == "0"){
                        window.location.replace("SupplierBidDetailInner.aspx?Info="+input);
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
</script>


    <!----Load the data to be searched----------------------------------->

    <script type="text/javascript">
         
         debugger;
    var SupplierMainCategory01 = <%= getJsonSupplierMainCategory() %>
    var SupplierMainCategorySub01 = <%= getJsonSupplierMainCategoryAndSubCat() %>
    var SupplierCompanyList=<%= getJsonComapanyList() %>
    var SupplierMainCategorySub = <%= getJsonSupplierMainCategoryAndSubCat() %>;
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
                ' <p style="color:black; margin-left:5px;font-size:small;"> ' +
                ' <input type="checkbox" class="messageCheckbox"  name="vehicle" value='+fieldComp[0]+' style="margin-top:4px;" class="pull-right"> ' +fieldComp[1]+
                //' <img src='+DepimgPath+' height="20" width="20" />&nbsp;&nbsp;'+
                ' </p> ' +
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
                ' <li class="subMenu open"><a style="color:black;margin-left:0px;font-size:small;"><i class="icon-chevron-right"></i>'+fieldCompSup[1]+'</a> ' +
                ' <ul id="myUl'+z+'" class="nav nav-tabs nav-stacked">' + ''
                textCompSup += htmlcodeCompSup;
            }
            if(z != 1){
                htmlcodeCompSup =
                ' <li class="subMenu"><a style="color:black;margin-left:0px;font-size:small;"><i class="icon-chevron-right"></i>'+fieldCompSup[1]+'</a> ' +
                ' <ul id="myUl'+z+'" style="display:none" class="nav nav-tabs nav-stacked">' + ''
                textCompSup += htmlcodeCompSup;
            }
            
            for (var b = 1; b < SupplierMainCategorySub.length; b++) {
                fieldCompSupSub = SupplierMainCategorySub[b - 1].split('-');
                ListIdCompSupSub = fieldCompSupSub[0]+"_"+fieldCompSupSub[1];
                if(fieldCompSupSub[0] == fieldCompSup[0]){
                    htmlcodeCompSup =
                   ' <li><a style="font-size:small;color:black;margin-left:0px;"><i class="icon-dash-right"></i>'+ ' <input type="checkbox" name="vehicle" class="messageCheckboxitem" value='+fieldCompSupSub[0]+"_"+fieldCompSupSub[1]+' style="margin-top:4px;" class="pull-right"> ' +fieldCompSupSub[2]+ '' +
                   
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


<!------- Call the search methods when the search button is clicked-------------------->

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

        //    SearchAllBidsLatest1();
           
     
            SearchAllBids();

       
       
    }


    var DataListLatest1 = <%= getJsonBiddingItemListAllLatest() %>;
    var SupplierMainCategorySub = <%= getJsonSupplierMainCategoryAndSubCat() %>;
    var SearchedAllLatestBids="";
    var SearchedAllBids="";

    //function SearchAllBidsLatest1(){


    //                var textLatestUl = "";
    //                var textLatest = "";
    //                var fieldLatest = "";
    //                var BidimgPathLatest = "";
    //                var DepimgPathLatest = "";
    //                var ListIdLatest = "";
    //                var htmlcodeLatest = "" ;
    //                var BidTime1="";
    //                var BidTime2="";
    //                var remainingTime="";
        

    

    //                var rounds = ((DataListLatest.length))/4;
    //                var valueOfLoopRounds = (Math.ceil(rounds));




    //                for (var i = 1; i <= valueOfLoopRounds; i++) {
    //                    if(i == 1){
    //                        for (var f = 1; f <= DataListLatest.length; f++) {

    //                            fieldLatest = DataListLatest[f - 1].split('-');
    //                            ListIdLatest = fieldLatest[0]+"_"+fieldLatest[1]+"_"+fieldLatest[9];

    //                            //When searched using company id
    //                            var companyID=fieldLatest[3];
    //                            var itemID1=fieldLatest[11]+"_"+fieldLatest[12];






                                
    //                            if(checkedValue==null && checkedValueItem==null){
    //                            }


    //                            else if(checkedValue==null && checkedValueItem!=null){


    //                                if(checkedValueItem.includes(itemID1)){

    //                                    if(fieldLatest[6] != "")
    //                                    {
    //                                        BidimgPathLatest = fieldLatest[6].replace('~/','');
    //                                    }
    //                                    else{

    //                                        BidimgPathLatest ="LoginResources/images/noItem.png";
    //                                        // BidimgPathLatest = "images/noimage.png";
    //                                    }
    //                                    if(fieldLatest[5] != "")
    //                                    {
    //                                        DepimgPathLatest = fieldLatest[5].replace('~/','');
    //                                    }
    //                                    if(f >= 0 && f < 5 ){
    //                                        htmlcodeLatest =
    //                                        ' <li> ' +
    //                                        ' <div class="w3l-specilamk" style="background-color:white;border-color: #005383;" > ' +
    //                                        ' <div class="speioffer-agile" style="background-color:white"> ' +
    //                                        ' <a href="single.html"> ' +
    //                                        ' <img src='+BidimgPathLatest+' alt="" style="width:150px;height:150px"> ' +
    //                                        ' </a> ' +
    //                                        ' </div> ' +
    //                                        ' <div class="product-name-w3l"> ' + ''
    //                                        textLatest += htmlcodeLatest;
    //                                        if(fieldLatest[2].length <= 30 )
    //                                        {
    //                                            htmlcodeLatest =
    //                                               ' <h4 style=" padding-bottom: 22px; "> ' +
    //                                               ' <a href="single.html">'+fieldLatest[2]+'</a> ' +
    //                                               ' </h4> ' + ''
    //                                            textLatest += htmlcodeLatest;
    //                                        }
    //                                        if(fieldLatest[2].length >= 31 )
    //                                        {
    //                                            htmlcodeLatest =
    //                                             ' <h4> ' +
    //                                             ' <a href="single.html">'+fieldLatest[2]+'</a> ' +
    //                                             ' </h4> ' + ''
    //                                            textLatest += htmlcodeLatest;
    //                                        }
    //                                        htmlcodeLatest =
    //                                        ' <div class="w3l-pricehkj">' +
    //                                        ' <h4 style="text-align:center;color:Red"> OrderID: '+fieldLatest[9]+'</h4> ' +
    //                                        ' <p id="P'+f+'" style="color:red; font-weight:bold;text-align:center"></p> ' +
    //                                        ' </div> ' +
    //                                        ' <div class="snipcart-details   single-item hvr-outline-out"> ' +
    //                                        ' <form action="#" method="post"> ' +
    //                                        ' <fieldset> ' +
    //                                        ' <input type="hidden" name="cmd" value="_cart" /> ' +
    //                                        ' <input type="hidden" name="add" value="1" /> ' +
    //                                        ' <input type="hidden" name="business" value=" " /> ' +
    //                                        ' <input type="hidden" name="item_name" value="Aashirvaad, 5g" /> ' +
    //                                        ' <input type="hidden" name="amount" value="220.00" /> ' +
    //                                        ' <input type="hidden" name="discount_amount" value="1.00" /> ' +
    //                                        ' <input type="hidden" name="currency_code" value="USD" /> ' +
    //                                        ' <input type="hidden" name="return" value=" " /> ' +
    //                                        ' <input type="hidden" name="cancel_return" value=" " /> ' + ''
    //                                        textLatest += htmlcodeLatest;
    //                                        if(fieldLatest[10] == "Edit Bid"){
    //                                            htmlcodeLatest =
    //                                             ' <input type="button" name="submit" value="Edit Bid" class="button" id='+ListIdLatest+' onclick="Bid_ClickEvent(this.id)" value="'+fieldLatest[10]+'"/> ' + ''
    //                                            textLatest += htmlcodeLatest;
    //                                        }
    //                                        if(fieldLatest[10] == "Pending Bid"){
    //                                            htmlcodeLatest =
    //                                            ' <input type="button" name="submit" value="Pending Bid" class="button" id='+ListIdLatest+' onclick="Bid_ClickEvent(this.id)" value="'+fieldLatest[10]+'"/> ' + ''
    //                                            textLatest += htmlcodeLatest;
    //                                        }
    //                                        if(fieldLatest[10] == "Bid"){
    //                                            htmlcodeLatest =
    //                                           ' <input type="button" name="submit" value="Bid" class="button" id='+ListIdLatest+' onclick="Bid_ClickEvent(this.id)" value="'+fieldLatest[10]+'"/> ' + ''
    //                                            textLatest += htmlcodeLatest;
    //                                        }
    //                                        htmlcodeLatest = 
    //                                        ' <span class="product-new-top">New</span> ' +
    //                                        ' </fieldset> ' +
    //                                        ' </form> ' +
    //                                        ' </div> ' +
    //                                        ' </div> ' +
    //                                        ' </div> ' +
    //                                        ' </li> ' + ''
    //                                        textLatest += htmlcodeLatest;
    //                                    }


                          
                          
    //                                }
    //                            }

    //                            else if(checkedValue!=null && checkedValueItem!=null){

    //                                if(checkedValue.includes(companyID) && checkedValueItem!=null){


    //                                    if(checkedValueItem.includes(itemID1)){

    //                                        if(fieldLatest[6] != "")
    //                                        {
    //                                            BidimgPathLatest = fieldLatest[6].replace('~/','');
    //                                        }
    //                                        else{

    //                                            BidimgPathLatest ="LoginResources/images/noItem.png";
    //                                            // BidimgPathLatest = "images/noimage.png";
    //                                        }
    //                                        if(fieldLatest[5] != "")
    //                                        {
    //                                            DepimgPathLatest = fieldLatest[5].replace('~/','');
    //                                        }
    //                                        if(f >= 0 && f < 5 ){
    //                                            htmlcodeLatest =
    //                                            ' <li> ' +
    //                                            ' <div class="w3l-specilamk" style="background-color:white;border-color: #005383;" > ' +
    //                                            ' <div class="speioffer-agile" style="background-color:white"> ' +
    //                                            ' <a href="single.html"> ' +
    //                                            ' <img src='+BidimgPathLatest+' alt="" style="width:150px;height:150px"> ' +
    //                                            ' </a> ' +
    //                                            ' </div> ' +
    //                                            ' <div class="product-name-w3l"> ' + ''
    //                                            textLatest += htmlcodeLatest;
    //                                            if(fieldLatest[2].length <= 30 )
    //                                            {
    //                                                htmlcodeLatest =
    //                                                   ' <h4 style=" padding-bottom: 22px; "> ' +
    //                                                   ' <a href="single.html">'+fieldLatest[2]+'</a> ' +
    //                                                   ' </h4> ' + ''
    //                                                textLatest += htmlcodeLatest;
    //                                            }
    //                                            if(fieldLatest[2].length >= 31 )
    //                                            {
    //                                                htmlcodeLatest =
    //                                                 ' <h4> ' +
    //                                                 ' <a href="single.html">'+fieldLatest[2]+'</a> ' +
    //                                                 ' </h4> ' + ''
    //                                                textLatest += htmlcodeLatest;
    //                                            }
    //                                            htmlcodeLatest =
    //                                            ' <div class="w3l-pricehkj">' +
    //                                            ' <h4 style="text-align:center;color:Red">  OrderID: '+fieldLatest[9]+'</h4> ' +
    //                                            ' <p id="P'+f+'" style="color:red; font-weight:bold;text-align:center"></p> ' +
    //                                            ' </div> ' +
    //                                            ' <div class="snipcart-details   single-item hvr-outline-out"> ' +
    //                                            ' <form action="#" method="post"> ' +
    //                                            ' <fieldset> ' +
    //                                            ' <input type="hidden" name="cmd" value="_cart" /> ' +
    //                                            ' <input type="hidden" name="add" value="1" /> ' +
    //                                            ' <input type="hidden" name="business" value=" " /> ' +
    //                                            ' <input type="hidden" name="item_name" value="Aashirvaad, 5g" /> ' +
    //                                            ' <input type="hidden" name="amount" value="220.00" /> ' +
    //                                            ' <input type="hidden" name="discount_amount" value="1.00" /> ' +
    //                                            ' <input type="hidden" name="currency_code" value="USD" /> ' +
    //                                            ' <input type="hidden" name="return" value=" " /> ' +
    //                                            ' <input type="hidden" name="cancel_return" value=" " /> ' + ''
    //                                            textLatest += htmlcodeLatest;
    //                                            if(fieldLatest[10] == "Edit Bid"){
    //                                                htmlcodeLatest =
    //                                                 ' <input type="button" name="submit" value="Edit Bid" class="button" id='+ListIdLatest+' onclick="Bid_ClickEvent(this.id)" value="'+fieldLatest[10]+'"/> ' + ''
    //                                                textLatest += htmlcodeLatest;
    //                                            }
    //                                            if(fieldLatest[10] == "Pending Bid"){
    //                                                htmlcodeLatest =
    //                                                ' <input type="button" name="submit" value="Pending Bid" class="button" id='+ListIdLatest+' onclick="Bid_ClickEvent(this.id)" value="'+fieldLatest[10]+'"/> ' + ''
    //                                                textLatest += htmlcodeLatest;
    //                                            }
    //                                            if(fieldLatest[10] == "Bid"){
    //                                                htmlcodeLatest =
    //                                               ' <input type="button" name="submit" value="Bid" class="button" id='+ListIdLatest+' onclick="Bid_ClickEvent(this.id)" value="'+fieldLatest[10]+'"/> ' + ''
    //                                                textLatest += htmlcodeLatest;
    //                                            }
    //                                            htmlcodeLatest = 
    //                                            ' <span class="product-new-top">New</span> ' +
    //                                            ' </fieldset> ' +
    //                                            ' </form> ' +
    //                                            ' </div> ' +
    //                                            ' </div> ' +
    //                                            ' </div> ' +
    //                                            ' </li> ' + ''
    //                                            textLatest += htmlcodeLatest;
    //                                        }
                          
    //                                    }




    //                                }
    //                            }

    //                            else if(checkedValueItem==null && checkedValue!=null){

    //                                if(checkedValue.includes(companyID)){

    //                                    if(fieldLatest[6] != "")
    //                                    {
    //                                        BidimgPathLatest = fieldLatest[6].replace('~/','');
    //                                    }
    //                                    else{

    //                                        BidimgPathLatest ="LoginResources/images/noItem.png";
    //                                        // BidimgPathLatest = "images/noimage.png";
    //                                    }
    //                                    if(fieldLatest[5] != "")
    //                                    {
    //                                        DepimgPathLatest = fieldLatest[5].replace('~/','');
    //                                    }
    //                                    if(f >= 0 && f < 5 ){
    //                                        htmlcodeLatest =
    //                                        ' <li> ' +
    //                                        ' <div class="w3l-specilamk" style="background-color:white;border-color: #005383;" > ' +
    //                                        ' <div class="speioffer-agile" style="background-color:white"> ' +
    //                                        ' <a href="single.html"> ' +
    //                                        ' <img src='+BidimgPathLatest+' alt="" style="width:150px;height:150px"> ' +
    //                                        ' </a> ' +
    //                                        ' </div> ' +
    //                                        ' <div class="product-name-w3l"> ' + ''
    //                                        textLatest += htmlcodeLatest;
    //                                        if(fieldLatest[2].length <= 30 )
    //                                        {
    //                                            htmlcodeLatest =
    //                                               ' <h4 style=" padding-bottom: 22px; "> ' +
    //                                               ' <a href="single.html">'+fieldLatest[2]+'</a> ' +
    //                                               ' </h4> ' + ''
    //                                            textLatest += htmlcodeLatest;
    //                                        }
    //                                        if(fieldLatest[2].length >= 31 )
    //                                        {
    //                                            htmlcodeLatest =
    //                                             ' <h4> ' +
    //                                             ' <a href="single.html">'+fieldLatest[2]+'</a> ' +
    //                                             ' </h4> ' + ''
    //                                            textLatest += htmlcodeLatest;
    //                                        }
    //                                        htmlcodeLatest =
    //                                        ' <div class="w3l-pricehkj">' +
    //                                        ' <h4 style="text-align:center;color:Red">  OrderID: '+fieldLatest[9]+'</h4> ' +
    //                                        ' <p id="P'+f+'" style="color:red; font-weight:bold;text-align:center"></p> ' +
    //                                        ' </div> ' +
    //                                        ' <div class="snipcart-details   single-item hvr-outline-out"> ' +
    //                                        ' <form action="#" method="post"> ' +
    //                                        ' <fieldset> ' +
    //                                        ' <input type="hidden" name="cmd" value="_cart" /> ' +
    //                                        ' <input type="hidden" name="add" value="1" /> ' +
    //                                        ' <input type="hidden" name="business" value=" " /> ' +
    //                                        ' <input type="hidden" name="item_name" value="Aashirvaad, 5g" /> ' +
    //                                        ' <input type="hidden" name="amount" value="220.00" /> ' +
    //                                        ' <input type="hidden" name="discount_amount" value="1.00" /> ' +
    //                                        ' <input type="hidden" name="currency_code" value="USD" /> ' +
    //                                        ' <input type="hidden" name="return" value=" " /> ' +
    //                                        ' <input type="hidden" name="cancel_return" value=" " /> ' + ''
    //                                        textLatest += htmlcodeLatest;
    //                                        if(fieldLatest[10] == "Edit Bid"){
    //                                            htmlcodeLatest =
    //                                             ' <input type="button" name="submit" value="Edit Bid" class="button" id='+ListIdLatest+' onclick="Bid_ClickEvent(this.id)" value="'+fieldLatest[10]+'"/> ' + ''
    //                                            textLatest += htmlcodeLatest;
    //                                        }
    //                                        if(fieldLatest[10] == "Pending Bid"){
    //                                            htmlcodeLatest =
    //                                            ' <input type="button" name="submit" value="Pending Bid" class="button" id='+ListIdLatest+' onclick="Bid_ClickEvent(this.id)" value="'+fieldLatest[10]+'"/> ' + ''
    //                                            textLatest += htmlcodeLatest;
    //                                        }
    //                                        if(fieldLatest[10] == "Bid"){
    //                                            htmlcodeLatest =
    //                                           ' <input type="button" name="submit" value="Bid" class="button" id='+ListIdLatest+' onclick="Bid_ClickEvent(this.id)" value="'+fieldLatest[10]+'"/> ' + ''
    //                                            textLatest += htmlcodeLatest;
    //                                        }
    //                                        htmlcodeLatest = 
    //                                        ' <span class="product-new-top">New</span> ' +
    //                                        ' </fieldset> ' +
    //                                        ' </form> ' +
    //                                        ' </div> ' +
    //                                        ' </div> ' +
    //                                        ' </div> ' +
    //                                        ' </li> ' + ''
    //                                        textLatest += htmlcodeLatest;
    //                                    }
                           
    //                                }
    //                            }



                               
    //                            document.getElementById("flexiselDemo1").innerHTML = textLatest; 
    //                        }
    //                    }
    //                }
            
    //}



    function SearchAllBids(){



        





        var text = "";
        var field = "";
        var BidimgPath = "";
        var DepimgPath = "";
        var ListId = "";
        var htmlcode = "" ;
        var BidTime3="";
        var BidTime2="";
        var remainingTimeE="";


                


        //htmlcode =
        //      ' <div class="product-sec1" style="background-color:white"> ' + '' 
        //text += htmlcode;
        for (var i = 1; i <= DataList.length; i++) {
            field = DataList[i - 1].split('-');
            ListId = field[0]+"_"+field[1]+"_"+field[9];

            var company=field[3];
            var itemID=field[11]+"_"+field[12];




            if(checkedValue==null && checkedValueItem==null){
                break;
            }


            else if(checkedValue==null && checkedValueItem!=null){


                if(checkedValueItem.includes(itemID)){

                    if(field[6] != "")
                    {
                        BidimgPath = field[6].replace('~/','');
                    }
                    else{

                        BidimgPath ="LoginResources/images/noItem.png";
                        //BidimgPath = "images/noimage.png";
                    }
                    if(field[5] != "")
                    {
                        DepimgPath = field[5].replace('~/','');
                    }
                    debugger;
                    htmlcode =
                          ' <div class="col-md-3 product-men" id='+ListId+' style="border-style: groove;padding:2px;background-color:white;"> ' +
                          ' <div class="men-pro-item simpleCart_shelfItem"> ' +
                          ' <div class="men-thumb-item"> ' +
                          ' <img src='+BidimgPath+' alt="" style="height:136px"> ' +
                          ' <div class="men-cart-pro"> ' +
                          ' <div class="inner-men-cart-pro"> ' +
                          ' <a href="single.html" class="link-product-add-cart">BID NOW</a> ' +
                          ' </div> ' +
                          ' </div> ' +
				  
                          ' </div> ' +
                          ' <div class="item-info-product "> ' + ''
                    text += htmlcode;

                    if(field[2].length > 0 && field[2].length < 15)
                    {
                        htmlcode =
                        ' <h4 style=" padding-bottom: 36px; "> ' + 
                        ' <a href="single.html">'+field[2]+'</a> ' + 
                        ' <h4 /> '  + ''
                        text += htmlcode;
                    }
                    if(field[2].length <= 42 && field[2].length > 15)
                    {
                        htmlcode =
                        ' <h4 style=" padding-bottom: 18px; "> ' + 
                        ' <a href="single.html">'+field[2]+'</a> ' + 
                        ' <h4 /> '  + ''
                        text += htmlcode;
                    }
                    if(field[2].length >= 43 )
                    {
                        htmlcode =
                       ' <h4> ' + 
                       ' <a href="single.html">'+field[2]+'</a> ' +  
                       ' </h4> ' + ''
                        text += htmlcode;
                    }
                    htmlcode =
                    ' </h4> ' +
                    ' <div class="info-product-price"> ' +
                    ' <span class="item_price">OrderID: '+field[9]+'</span> ' +
                    ' <p id="demo'+i+'" style="color:red; font-weight:bold;text-align:center"></p> ' +
                    ' </div> ' +
                    ' <div class="snipcart-details top_brand_home_details item_add single-item hvr-outline-out"> ' +
                    ' <form action="#" method="post"> ' +
                    ' <fieldset> ' +
                    ' <input type="hidden" name="cmd" value="_cart" /> ' +
                    ' <input type="hidden" name="add" value="1" /> ' +
                    ' <input type="hidden" name="business" value=" " /> ' +
                    ' <input type="hidden" name="item_name" value="Almonds, 100g" /> ' +
                    ' <input type="hidden" name="amount" value="149.00" /> ' +
                    ' <input type="hidden" name="discount_amount" value="1.00" /> ' +
                    ' <input type="hidden" name="currency_code" value="USD" /> ' +
                    ' <input type="hidden" name="return" value=" " /> ' +
                    ' <input type="hidden" name="cancel_return" value=" " /> ' + ''
                    text += htmlcode;
                    if(field[10] == "Edit Bid"){
                        htmlcode =
                         ' <input type="button" name="submit" value="Edit Bid" class="button" id='+ListId+' onclick="Bid_ClickEvent(this.id)" value="'+field[10]+'"/> ' + ''
                        text += htmlcode;
                    }
                    if(field[10] == "Pending Bid"){
                        htmlcode =
                        ' <input type="button" name="submit" value="Pending Bid" class="button" id='+ListId+' onclick="Bid_ClickEvent(this.id)" value="'+field[10]+'"/> ' + ''
                        text += htmlcode;
                    }
                    if(field[10] == "Bid"){
                        htmlcode =
                       ' <input type="button" name="submit" value="Bid" class="button" id='+ListId+' onclick="Bid_ClickEvent(this.id)" value="'+field[10]+'"/> ' + ''
                        text += htmlcode;
                    }
                    htmlcode = 
                    ' <p style="padding-top: 4px;"></p> ' +
                    ' </fieldset> ' +
                    ' </form> ' +
                    ' </div> ' +
                    ' </div> ' +
                    ' </div> ' +
                    ' </div> ' + '' 
                    text += htmlcode;

                
                }
            }

            else if(checkedValue!=null && checkedValueItem!=null){

                if(checkedValue.includes(company) && checkedValueItem!=null){


                    if(checkedValueItem.includes(itemID)){
                        if(field[6] != "")
                        {
                            BidimgPath = field[6].replace('~/','');
                        }
                        else{

                            BidimgPath ="LoginResources/images/noItem.png";
                            //BidimgPath = "images/noimage.png";
                        }
                        if(field[5] != "")
                        {
                            DepimgPath = field[5].replace('~/','');
                        }
                        debugger;
                        htmlcode =
                              ' <div class="col-md-3 product-men" id='+ListId+' style="border-style: groove;padding:2px;background-color:white;"> ' +
                              ' <div class="men-pro-item simpleCart_shelfItem"> ' +
                              ' <div class="men-thumb-item"> ' +
                              ' <img src='+BidimgPath+' alt="" style="height:136px"> ' +
                              ' <div class="men-cart-pro"> ' +
                              ' <div class="inner-men-cart-pro"> ' +
                              ' <a href="single.html" class="link-product-add-cart">BID NOW</a> ' +
                              ' </div> ' +
                              ' </div> ' +
				  
                              ' </div> ' +
                              ' <div class="item-info-product "> ' + ''
                        text += htmlcode;

                        if(field[2].length > 0 && field[2].length < 15)
                        {
                            htmlcode =
                            ' <h4 style=" padding-bottom: 36px; "> ' + 
                            ' <a href="single.html">'+field[2]+'</a> ' + 
                            ' <h4 /> '  + ''
                            text += htmlcode;
                        }
                        if(field[2].length <= 42 && field[2].length > 15)
                        {
                            htmlcode =
                            ' <h4 style=" padding-bottom: 18px; "> ' + 
                            ' <a href="single.html">'+field[2]+'</a> ' + 
                            ' <h4 /> '  + ''
                            text += htmlcode;
                        }
                        if(field[2].length >= 43 )
                        {
                            htmlcode =
                           ' <h4> ' + 
                           ' <a href="single.html">'+field[2]+'</a> ' +  
                           ' </h4> ' + ''
                            text += htmlcode;
                        }
                        htmlcode =
                        ' </h4> ' +
                        ' <div class="info-product-price"> ' +
                        ' <span class="item_price">OrderID: '+field[9]+'</span> ' +
                        ' <p id="demo'+i+'" style="color:red; font-weight:bold;text-align:center"></p> ' +
                        ' </div> ' +
                        ' <div class="snipcart-details top_brand_home_details item_add single-item hvr-outline-out"> ' +
                        ' <form action="#" method="post"> ' +
                        ' <fieldset> ' +
                        ' <input type="hidden" name="cmd" value="_cart" /> ' +
                        ' <input type="hidden" name="add" value="1" /> ' +
                        ' <input type="hidden" name="business" value=" " /> ' +
                        ' <input type="hidden" name="item_name" value="Almonds, 100g" /> ' +
                        ' <input type="hidden" name="amount" value="149.00" /> ' +
                        ' <input type="hidden" name="discount_amount" value="1.00" /> ' +
                        ' <input type="hidden" name="currency_code" value="USD" /> ' +
                        ' <input type="hidden" name="return" value=" " /> ' +
                        ' <input type="hidden" name="cancel_return" value=" " /> ' + ''
                        text += htmlcode;
                        if(field[10] == "Edit Bid"){
                            htmlcode =
                             ' <input type="button" name="submit" value="Edit Bid" class="button" id='+ListId+' onclick="Bid_ClickEvent(this.id)" value="'+field[10]+'"/> ' + ''
                            text += htmlcode;
                        }
                        if(field[10] == "Pending Bid"){
                            htmlcode =
                            ' <input type="button" name="submit" value="Pending Bid" class="button" id='+ListId+' onclick="Bid_ClickEvent(this.id)" value="'+field[10]+'"/> ' + ''
                            text += htmlcode;
                        }
                        if(field[10] == "Bid"){
                            htmlcode =
                           ' <input type="button" name="submit" value="Bid" class="button" id='+ListId+' onclick="Bid_ClickEvent(this.id)" value="'+field[10]+'"/> ' + ''
                            text += htmlcode;
                        }
                        htmlcode = 
                        ' <p style="padding-top: 4px;"></p> ' +
                        ' </fieldset> ' +
                        ' </form> ' +
                        ' </div> ' +
                        ' </div> ' +
                        ' </div> ' +
                        ' </div> ' + '' 
                        text += htmlcode;

                    }
                }
            }

            else if(checkedValueItem==null && checkedValue!=null){

                if(checkedValue.includes(company)){
                    if(field[6] != "")
                    {
                        BidimgPath = field[6].replace('~/','');
                    }
                    else{

                        BidimgPath ="LoginResources/images/noItem.png";
                        //BidimgPath = "images/noimage.png";
                    }
                    if(field[5] != "")
                    {
                        DepimgPath = field[5].replace('~/','');
                    }
                    debugger;
                    htmlcode =
                          ' <div class="col-md-3 product-men" id='+ListId+' style="border-style: groove;padding:2px;background-color:white;"> ' +
                          ' <div class="men-pro-item simpleCart_shelfItem"> ' +
                          ' <div class="men-thumb-item"> ' +
                          ' <img src='+BidimgPath+' alt="" style="height:136px"> ' +
                          ' <div class="men-cart-pro"> ' +
                          ' <div class="inner-men-cart-pro"> ' +
                          ' <a href="single.html" class="link-product-add-cart">BID NOW</a> ' +
                          ' </div> ' +
                          ' </div> ' +
				  
                          ' </div> ' +
                          ' <div class="item-info-product "> ' + ''
                    text += htmlcode;

                    if(field[2].length > 0 && field[2].length < 15)
                    {
                        htmlcode =
                        ' <h4 style=" padding-bottom: 36px; "> ' + 
                        ' <a href="single.html">'+field[2]+'</a> ' + 
                        ' <h4 /> '  + ''
                        text += htmlcode;
                    }
                    if(field[2].length <= 42 && field[2].length > 15)
                    {
                        htmlcode =
                        ' <h4 style=" padding-bottom: 18px; "> ' + 
                        ' <a href="single.html">'+field[2]+'</a> ' + 
                        ' <h4 /> '  + ''
                        text += htmlcode;
                    }
                    if(field[2].length >= 43 )
                    {
                        htmlcode =
                       ' <h4> ' + 
                       ' <a href="single.html">'+field[2]+'</a> ' +  
                       ' </h4> ' + ''
                        text += htmlcode;
                    }
                    htmlcode =
                    ' </h4> ' +
                    ' <div class="info-product-price"> ' +
                    ' <span class="item_price">OrderID: '+field[9]+'</span> ' +
                    ' <p id="demo'+i+'" style="color:red; font-weight:bold;text-align:center"></p> ' +
                    ' </div> ' +
                    ' <div class="snipcart-details top_brand_home_details item_add single-item hvr-outline-out"> ' +
                    ' <form action="#" method="post"> ' +
                    ' <fieldset> ' +
                    ' <input type="hidden" name="cmd" value="_cart" /> ' +
                    ' <input type="hidden" name="add" value="1" /> ' +
                    ' <input type="hidden" name="business" value=" " /> ' +
                    ' <input type="hidden" name="item_name" value="Almonds, 100g" /> ' +
                    ' <input type="hidden" name="amount" value="149.00" /> ' +
                    ' <input type="hidden" name="discount_amount" value="1.00" /> ' +
                    ' <input type="hidden" name="currency_code" value="USD" /> ' +
                    ' <input type="hidden" name="return" value=" " /> ' +
                    ' <input type="hidden" name="cancel_return" value=" " /> ' + ''
                    text += htmlcode;
                    if(field[10] == "Edit Bid"){
                        htmlcode =
                         ' <input type="button" name="submit" value="Edit Bid" class="button" id='+ListId+' onclick="Bid_ClickEvent(this.id)" value="'+field[10]+'"/> ' + ''
                        text += htmlcode;
                    }
                    if(field[10] == "Pending Bid"){
                        htmlcode =
                        ' <input type="button" name="submit" value="Pending Bid" class="button" id='+ListId+' onclick="Bid_ClickEvent(this.id)" value="'+field[10]+'"/> ' + ''
                        text += htmlcode;
                    }
                    if(field[10] == "Bid"){
                        htmlcode =
                       ' <input type="button" name="submit" value="Bid" class="button" id='+ListId+' onclick="Bid_ClickEvent(this.id)" value="'+field[10]+'"/> ' + ''
                        text += htmlcode;
                    }
                    htmlcode = 
                    ' <p style="padding-top: 4px;"></p> ' +
                    ' </fieldset> ' +
                    ' </form> ' +
                    ' </div> ' +
                    ' </div> ' +
                    ' </div> ' +
                    ' </div> ' + '' 
                    text += htmlcode;

                }
            }



           








        }
        htmlcode =  
        ' <div class="clearfix"></div> ' +
        ' </div> ' + '' 
        text += htmlcode;
        document.getElementById("product-sec1").innerHTML="";
        document.getElementById("product-sec1").innerHTML = text;
    }





    
    

    

        
        
    </script>
    <script>
function openNav() {
    document.getElementById("mySidepanel").style.width = "250px";
}

function closeNav() {
    document.getElementById("mySidepanel").style.width = "0";
}
</script>




</asp:Content>
