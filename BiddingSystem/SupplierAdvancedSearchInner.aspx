<%@ Page Title="" Language="C#" MasterPageFile="~/SupplierLoadingWebUIInner.Master" AutoEventWireup="true" CodeBehind="SupplierAdvancedSearchInner.aspx.cs" Inherits="BiddingSystem.SupplierAdvancedSearchInner" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class="services-breadcrumb" style="background-color: #005383">
		<div class="agile_inner_breadcrumb">
			<div class="container">
				<ul class="w3_short">
					<li>
						<a href="SupplierInitialFrontView.aspx">Home</a>
						<i>|</i>
                        <a href="#" style="color:Yellow;">Advanced Search</a>
					</li>
				</ul>
			</div>
		</div>
	</div>
    <div class="ads-grid">
		<div class="container">
			<!-- tittle heading -->
    <div class="side-bar col-md-3">
				<div class="search-hotel">
					<h3 class="agileits-sear-head">Search Here..</h3>
					<form action="#" method="post">
						<input type="search" placeholder="Item name..." name="search" required="">
						<input type="submit" value=" ">
					</form>
				</div>
				<!-- discounts -->
				<div class="left-side">
					<h3 class="agileits-sear-head">Companies</h3>
					<ul id="companyChk">

					</ul>
				</div>
				<!-- //discounts -->
				<!-- cuisine -->
				<div class="left-side">
					<h3 class="agileits-sear-head">Main Categories</h3>
					<ul id="categoriesAll">
						
					</ul>
				</div>
				<!-- //cuisine -->
                <br />
                <form runat="server" id="form1">
                <div class="left-side" style=" text-align: center; ">
                <asp:Button runat="server" ID="btnSearchAdvanced" CssClass="btn btn-success" Text="Advanced Search"/>
                </div>
                </form>
			</div>
        <div class="col-md-9">
        <div id="product-sec1">

   		</div>
        </div>
        </div>
        </div>

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
        var htmlcode = "" ;
        htmlcode =
              ' <div class="product-sec1" style="background-color:white"> ' + '' 
              text += htmlcode;
        for (var i = 1; i <= DataList.length; i++) {
            field = DataList[i - 1].split('-');
            ListId = field[0]+"_"+field[1]+"_"+field[9];
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
          debugger;
            htmlcode =
                  ' <div class="col-md-4 product-men" id='+ListId+' style="border-style:groove; margin-top:-0px;"> ' +
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
				  ' <span class="item_price"><img src='+DepimgPath+' alt="" style="width:40px;height:40px">OrderID: '+field[9]+'</span> ' +
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
     debugger;
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


    function Bid_ClickEvent(input)
    {
        var Supplier =<%=getJsonSupplier() %> ;

        var ratus = input;
            var jsonText = JSON.stringify({ data: ratus, sup: Supplier });
            $.ajax({
                type: "POST",
                url: "SupplierAdvancedSearchInner.aspx/GetBidPendingOrNot?data="+ratus,
                data: jsonText,
                contentType: "application/json",
                dataType: "json",
                success: function (msg) {
                    if (msg.d.length > 0) {
                        if(msg.d[0].CountPending == "1"){
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
   
    <script type="text/javascript">
    var SupplierCompanyList = <%= getJsonComapanyList() %>
    var SupplierMainCategoryList = <%= getJsonSupplierMainCategory() %>
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
			   ' <input type="checkbox" class="checked"> ' +
               ' <img src='+DepimgPath+' height="20" width="20" style=" margin-top: -8px; "/>&nbsp;&nbsp; ' +
			   ' <span class="span">'+fieldComp[1]+'</span> ' +
			   ' </li> ' + ''
            textComp += htmlcodeComp;
            }
            document.getElementById('companyChk').innerHTML = textComp;
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
          for (var i = 1; i <= SupplierMainCategoryList.length; i++) {
            fieldCompSup = SupplierMainCategoryList[i - 1].split('-');
            ListIdCompSup = fieldCompSup[0]+"_"+fieldCompSup[1];
            if(fieldCompSup[2] != "")
            {
                 DepimgPathSup = fieldCompSup[2].replace('~/','');
            }
            else{
                 DepimgPathSup = "images/noimage.png";
            }
            var htmlcodeCompSup =
               ' <li> ' +
			   ' <input type="checkbox" class="checked">&nbsp;&nbsp; ' +
               //' <img src='+DepimgPathSup+' height="20" width="20" style=" margin-top: -8px; "/>&nbsp;&nbsp; ' +
			   ' <span class="span">'+fieldCompSup[1]+'</span> ' +
			   ' </li> ' + ''
            textCompSup += htmlcodeCompSup;
            }
            document.getElementById('categoriesAll').innerHTML = textCompSup;
      }
 </script>
</asp:Content>
