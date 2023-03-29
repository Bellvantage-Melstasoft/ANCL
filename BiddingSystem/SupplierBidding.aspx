<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingSupplier.Master" AutoEventWireup="true" CodeBehind="SupplierBidding.aspx.cs" Inherits="BiddingSystem.SupplierBidding" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="span9">
    <ul class="breadcrumb">
    <li><a href="SupplierIndex.aspx">Home</a> <span class="divider">/</span></li>
    <li class="active"><a href="#">Supplier Bid</a></li>
    </ul>	
	<div class="row">	  
			<div id="gallery" class="span3">
            <a href="AppResources/themes/images/LaptopImage01.png" title="HP Pavilion 15 - CC1">
				<img src="AppResources/themes/images/LaptopImage01.png" style="width:100%" alt="HP Pavilion 15 - CC1"/>
            </a>
			<div id="differentview" class="moreOptopm carousel slide">
                <div class="carousel-inner">
                  <div class="item active">
                   <a href="AppResources/themes/images/LaptopImage01.png"> <img style="width:29%"  src="AppResources/themes/images/LaptopImage01.png" alt=""/></a>
                   <a href="AppResources/themes/images/LaptopImage01.png"> <img style="width:29%"  src="AppResources/themes/images/LaptopImage01.png" alt=""/></a>
                   <a href="AppResources/themes/images/LaptopImage01.png" > <img style="width:29%" src="AppResources/themes/images/LaptopImage01.png" alt=""/></a>
                  </div>
                  <div class="item">
                   <a href="AppResources/themes/images/LaptopImage01.png" > <img style="width:29%" src="AppResources/themes/images/LaptopImage01.png" alt=""/></a>
                   <a href="AppResources/themes/images/LaptopImage01.png"> <img style="width:29%"  src="AppResources/themes/images/LaptopImage01.png" alt=""/></a>
                   <a href="AppResources/themes/images/LaptopImage01.png"> <img style="width:29%"  src="AppResources/themes/images/LaptopImage01.png" alt=""/></a>
                  </div>
                </div>
              <!--  
			  <a class="left carousel-control" href="#myCarousel" data-slide="prev">‹</a>
              <a class="right carousel-control" href="#myCarousel" data-slide="next">›</a> 
			  -->
              </div>
			  
			 <div class="btn-toolbar">
			  <div class="btn-group">
				<span class="btn"><i class="icon-envelope"></i></span>
				<span class="btn" ><i class="icon-print"></i></span>
				<span class="btn" ><i class="icon-zoom-in"></i></span>
				<span class="btn" ><i class="icon-star"></i></span>
				<span class="btn" ><i class=" icon-thumbs-up"></i></span>
				<span class="btn" ><i class="icon-thumbs-down"></i></span>
			  </div>
			</div>
            <p id="demo" style="color:  Black;margin-bottom: 0px;margin-top: 0px;font-weight: bold;text-align:  center;"/>
			</div>
			<div class="span6">
				<h3>HP Pavilion 15 - CC1 Laptop  </h3>
				<small>- (Intel Core i7 | 8GB | 1TB HD) 4GB NVidia</small>
				<hr class="soft"/>
				<form class="form-horizontal qtyFrm">
				  <div class="control-group">
                    
					<label class="control-label" style="color:Black;font-weight:bold;"><span>LKR 132,000.00</span></label>
                     
                    <br />
					<div class="controls">

         <fieldset>
          <div class="control-group">
                <select class="span2">
						  <option>Black</option>
						  <option>Red</option>
						  <option>Blue</option>
						  <option>Brown</option>
						</select>
              
          </div>
          <div class="control-group">
           
              <input type="text" placeholder="Quantity" class="input-xlarge"/>
           
          </div>
          <div class="control-group">
           
              <input type="text" placeholder="Delivery" class="input-xlarge"/>
           
          </div>
          <div class="control-group">
           
              <input type="text" placeholder="Tax" class="input-xlarge"/>
           
          </div>
		   <div class="control-group">
           
              <input type="text" placeholder="Discount" class="input-xlarge"/>
           
          </div>
		   <div class="control-group">
           
              <input type="text" placeholder="Price" class="input-xlarge"/>
          </div>
        </fieldset>
					  <button type="submit" class="btn btn-large btn-primary pull-right"> Bid <i class=" icon-copy"></i></button> 
					</div>
				  </div>
				</form>
                <br />
				<hr class="soft clr"/>
				<p>
				Intel® Core™ i7-8550U (1.8 GHz base frequency, up to 4 GHz with Intel® Turbo Boost Technology, 8 MB cache, 4 cores).8 GB DDR4-2400 SDRAM (1 x 8 GB)
                NVIDIA® GeForce® MX130 (4 GB DDR3 dedicated), 1 TB 5400 rpm SATA, 15.6" diagonal FHD SVA anti-glare WLED-backlit (1920 x 1080), 
                Windows 10, DVD-Writer, Wi-Fi® and Bluetooth® 4.2 Combo, HD webcam, card reader, 1 HDMI; 1 RJ-45; 1 headphone/microphone combo; 2 USB 3.1 Gen 1, 1 USB 3.1 Type-C™ Gen 1, Full-size island-style keyboard with numeric keypad, 3-cell, 41 Wh Li-ion Battery, Color- Gold
				</p>
				<a class="btn btn-small pull-right" href="#detail">More Details</a>
				<br class="clr"/>
			<a href="#" name="detail"></a>
			<hr class="soft"/>
			</div>
			
			<div class="span9">
            <ul id="productDetail" class="nav nav-tabs">
              <li class="active"><a href="#home" data-toggle="tab">Product Details</a></li>
              <li><a href="#profile" data-toggle="tab">Related Products</a></li>
            </ul>
            <div id="myTabContent" class="tab-content" style="overflow:auto;">
              <div class="tab-pane fade active in" id="home">
			  <h4>Product Information</h4>
                <table class="table table-bordered">
				<tbody>
				<tr class="techSpecRow"><th colspan="2">Product Details</th><th colspan="1">Comply</th><th colspan="1">Remark</th></tr>
				<tr class="techSpecRow"><td class="techSpecTD1">Model: </td><td class="techSpecTD2">HP Pavilion 15 - CC154TX</td><td><input type="checkbox" style="text-align:center;"/></td><td><input type="text"  /></td></tr>
				<tr class="techSpecRow"><td class="techSpecTD1">Processor:</td><td class="techSpecTD2">8th Gen Intel Core i7 - 8550U (1.8GHz Up to 4.0GHz, 8MB Cache)</td><td><input type="checkbox"/></td><td><input type="text"  /></td></tr>
				<tr class="techSpecRow"><td class="techSpecTD1">Ram:</td><td class="techSpecTD2"> 8GB DDR4</td><td><input type="checkbox"/></td><td><input type="text"  /></td></tr>
				<tr class="techSpecRow"><td class="techSpecTD1">Hard Disk:</td><td class="techSpecTD2"> 1TB HDD</td><td><input type="checkbox"/></td><td><input type="text"  /></td></tr>
				<tr class="techSpecRow"><td class="techSpecTD1">Optical Drive:</td><td class="techSpecTD2">DVD/RW+DL</td><td><input type="checkbox"/></td><td><input type="text"  /></td></tr>
                <tr class="techSpecRow"><td class="techSpecTD1">Options:</td><td class="techSpecTD2">Wifi/CR/BT</td><td><input type="checkbox"/></td><td><input type="text"  /></td></tr>
                <tr class="techSpecRow"><td class="techSpecTD1">Screen:</td><td class="techSpecTD2">15.6" FHD</td><td><input type="checkbox"/></td><td><input type="text"  /></td></tr>
                <tr class="techSpecRow"><td class="techSpecTD1">Graphics:</td><td class="techSpecTD2">4GB NVidia GeForce MX130</td><td><input type="checkbox"/></td><td><input type="text"  /></td></tr>
                <tr class="techSpecRow"><td class="techSpecTD1">Operating System :</td><td class="techSpecTD2">Genuine Windows 10 Home 64Bit</td><td><input type="checkbox"/></td><td><input type="text"  /></td></tr>
                <tr class="techSpecRow"><td class="techSpecTD1">Warranty:</td><td class="techSpecTD2">2YR + 2YR Service</td><td><input type="checkbox"/></td><td><input type="text"  /></td></tr>
                <tr class="techSpecRow"><td class="techSpecTD1">Webcam :</td><td class="techSpecTD2">Yes</td><td><input type="checkbox"/></td><td><input type="text"  /></td></tr>
				</tbody>
				</table>
				
				<h5>Features</h5>
				<p>
				Intel® Core™ i7-8550U (1.8 GHz base frequency, up to 4 GHz with Intel® Turbo Boost Technology, 8 MB cache, 4 cores).8 GB DDR4-2400 SDRAM (1 x 8 GB)
                NVIDIA® GeForce® MX130 (4 GB DDR3 dedicated).
				</p>

			
              </div>
		<div class="tab-pane fade" id="profile">
		<div id="myTab" class="pull-right">
		 <a href="#listView" data-toggle="tab"><span class="btn btn-large"><i class="icon-list"></i></span></a>
		 <a href="#blockView" data-toggle="tab"><span class="btn btn-large btn-primary"><i class="icon-th-large"></i></span></a>
		</div>
		<br class="clr"/>
		<hr class="soft"/>
		 <div class="tab-content">
            <div class="tab-pane" id="listView">
                <div class="row">
                    <div class="span2">
                        <img src="AppResources/themes/images/LaptopImage01.png"" alt="" />
                    </div>
                    <div class="span4">
                        <h3>
                            New Bid | Available</h3>
                        <hr class="soft" />
                        <h5>
                            HP Pavilion
                        </h5>
                        <p>
                           Windows 10 Home 64 ,AMD Quad-Core A10 APU ,8 GB memory; 1 TB HDD storage,
                           AMD Radeon™ R5 Graphics
                        </p>
                        <a class="btn btn-small pull-right" href="#">View Details</a>
                        <br class="clr" />
                    </div>
                    <div class="span3 alignR">
                        <form class="form-horizontal qtyFrm">
                        <h3>
                            130,000.00 Rs</h3>
                        <label class="checkbox">
                            <input type="checkbox">
                            Adds product to compair
                        </label>
                        <br />
                        <a href="#" class="btn btn-large btn-primary">BID <i class=" icon-copy">
                        </i></a><a href="#" class="btn btn-large"><i class="icon-zoom-in">
                        </i></a>
                        </form>
                    </div>
                </div>
                <hr class="soft" />
                <div class="row">
                    <div class="span2">
                        <img src="AppResources/themes/images/products/BufferYaris.jpg" alt="" />
                    </div>
                    <div class="span4">
                        <h3>
                            New Bid | Available</h3>
                        <hr class="soft" />
                        <h5>
                            Toyota Yaris Buffer
                        </h5>
                        <p>
                            Power Rear Shock Absorber Coil Spring Cushion Buffer for Toyota Yaris ...
                        </p>
                        <a class="btn btn-small pull-right" href="#">View Details</a>
                        <br class="clr" />
                    </div>
                    <div class="span3 alignR">
                        <form class="form-horizontal qtyFrm">
                        <h3>
                            20,000.00 Rs</h3>
                        <label class="checkbox">
                            <input type="checkbox">
                            Adds product to compair
                        </label>
                        <br />
                        <a href="#" class="btn btn-large btn-primary">BID <i class=" icon-copy">
                        </i></a><a href="#" class="btn btn-large"><i class="icon-zoom-in">
                        </i></a>
                        </form>
                    </div>
                </div>
                <hr class="soft" />
                <div class="row">
                    <div class="span2">
                        <img src="AppResources/themes/images/products/DamroOfficeFur01.png" alt="" />
                    </div>
                    <div class="span4">
                        <h3>
                            New Bid | Available</h3>
                        <hr class="soft" />
                        <h5>
                            Office Table and Chair
                        </h5>
                        <p>
                            OFFICE TABLES · damro. View Products. OFFICE CHAIRS · damro. View Products · Office Cupboards and Racks · damro. View Products. Study Desks & COMPUTER Tables · damro. View Products · workstations ·
                        </p>
                        <a class="btn btn-small pull-right" href="#">View Details</a>
                        <br class="clr" />
                    </div>
                    <div class="span3 alignR">
                        <form class="form-horizontal qtyFrm">
                        <h3>
                            18,000.00 Rs</h3>
                        <label class="checkbox">
                            <input type="checkbox">
                            Adds product to compair
                        </label>
                        <br />
                        <a href="#" class="btn btn-large btn-primary">BID <i class=" icon-copy">
                        </i></a><a href="#" class="btn btn-large"><i class="icon-zoom-in">
                        </i></a>
                        </form>
                    </div>
                </div>
                <hr class="soft" />
                <div class="row">
                    <div class="span2">
                        <img src="AppResources/themes/images/products/chairs.jpg" alt="" />
                    </div>
                    <div class="span4">
                        <h3>
                            New Bid | Available</h3>
                        <hr class="soft" />
                        <h5>
                            Office Chair
                        </h5>
                        <p>
                        Piece (Min. Order) ,Size: W74cm*D64cm*H104cm-113cm ,Material: Mesh
                        Style: Executive Chair,Lift Chair,Mesh Chair,Swivel Chair ,Type: Office Furniture ,Folded: No ,General Use: Commercial Furniture
                        </p>
                        <a class="btn btn-small pull-right" href="#">View Details</a>
                        <br class="clr" />
                    </div>
                    <div class="span3 alignR">
                        <form class="form-horizontal qtyFrm">
                        <h3>
                            4500.00 Rs.</h3>
                        <label class="checkbox">
                            <input type="checkbox">
                            Adds product to compair
                        </label>
                        <br />
                        <a href="#" class="btn btn-large btn-primary">BID <i class=" icon-copy">
                        </i></a><a href="#" class="btn btn-large"><i class="icon-zoom-in">
                        </i></a>
                        </form>
                    </div>
                </div>
                <hr class="soft" />
                <div class="row">
                    <div class="span2">
                        <img src="AppResources/themes/images/products/6.jpg" alt="" />
                    </div>
                    <div class="span4">
                        <h3>
                            New Bid | Available</h3>
                        <hr class="soft" />
                        <h5>
                            Memory Card
                        </h5>
                        <p>
                           SanDisk Original Extreme PRO for DSLR Camera Up to 95MB/s Memory Card U3 Class 10 256GB 128GB 64GB 32GB 16GB 4K Memory Card
                        </p>
                        <a class="btn btn-small pull-right" href="#">View Details</a>
                        <br class="clr" />
                    </div>
                    <div class="span3 alignR">
                        <form class="form-horizontal qtyFrm">
                        <h3>
                            1000.00 Rs</h3>
                        <label class="checkbox">
                            <input type="checkbox">
                            Adds product to compair
                        </label>
                        <br />
                        <a href="#" class="btn btn-large btn-primary">Bid <i class=" icon-copy">
                        </i></a><a href="#" class="btn btn-large"><i class="icon-zoom-in">
                        </i></a>
                        </form>
                    </div>
                </div>
                <hr class="soft" />
                <div class="row">
                    <div class="span2">
                        <img src="AppResources/themes/images/products/7.jpg" alt="" />
                    </div>
                    <div class="span4">
                        <h3>
                            New Bid | Available</h3>
                        <hr class="soft" />
                        <h5>
                            Chip Reader
                        </h5>
                        <p>
                            Interface: Bluetooth/USB ,Dimension: 94.0mm (L)x60.0mm (W)x12.0mm(H)
                            Place of Origin: CN;GUA, Brand Name: HCCTG, Model Number: Bluetooth Portable Smart IC Chip Card Reader And Writer ACR3901U-S1
                            ,Name: Bluetooth Portable Smart IC Ch
                        </p>
                        <a class="btn btn-small pull-right" href="#">View Details</a>
                        <br class="clr" />
                    </div>
                    <div class="span3 alignR">
                        <form class="form-horizontal qtyFrm">
                        <h3>
                            1000.00 Rs</h3>
                        <label class="checkbox">
                            <input type="checkbox">
                            Adds product to compair
                        </label>
                        <br />
                        <a href="#" class="btn btn-large btn-primary">Bid <i class=" icon-copy">
                        </i></a><a href="#" class="btn btn-large"><i class="icon-zoom-in">
                        </i></a>
                        </form>
                    </div>
                </div>
                <hr class="soft" />
            </div>
            <div class="tab-pane  active" id="blockView">
                <ul class="thumbnails">
                    <li class="span3">
                        <div class="thumbnail">
                            <a href="#">
                                <img src="AppResources/themes/images/LaptopImage01.png" alt="" style="height:160px; width=160px;"/></a>
                            <div class="caption">
                                <h5>
                                    HP Pavilion</h5>
                                <p id="P1" style="font-weight:bold"></p>
                                <p style="color:Black; font-weight:bold;">Item Code : I004111</p>
                                <h4 style="text-align: center">
                                    <a class="btn" href="#"><i class="icon-zoom-in"></i></a><a class="btn"
                                        href="#">BID <i class="icon-copy"></i></a><a class="btn btn-primary"
                                            href="#">130,000.00 Rs</a></h4>
                            </div>
                        </div>
                    </li>
                    <li class="span3">
                        <div class="thumbnail">
                            <a href="#">
                                <img src="AppResources/themes/images/products/BufferYaris.jpg" alt=""  style="height:160px; width=160px;"/></a>
                            <div class="caption">
                                <h5>
                                   Toyota Yaris Buffer</h5>
                                <p id="demo2" style="font-weight:bold">
                                    
                                </p>
                                <p style="color:Black; font-weight:bold;">Item Code : I004145</p>
                                <h4 style="text-align: center">
                                    <a class="btn" href="#"><i class="icon-zoom-in"></i></a><a class="btn"
                                        href="#">BID <i class="icon-shopping-cart"></i></a><a class="btn btn-primary"
                                            href="#">20,000.00 Rs</a></h4>
                            </div>
                        </div>
                    </li>
                    <li class="span3">
                        <div class="thumbnail">
                            <a href="#">
                                <img src="AppResources/themes/images/products/DamroOfficeFur01.png" alt=""   style="height:160px; width=160px;" /></a>
                            <div class="caption">
                                <h5>
                                    Office Table and Chair</h5>
                                 <p id="demo3" style="font-weight:bold">
                                    
                                </p>
                                <p style="color:Black; font-weight:bold;">Item Code : I004174</p>
                                <h4 style="text-align: center">
                                    <a class="btn" href="#"><i class="icon-zoom-in"></i></a><a class="btn"
                                        href="#">BID <i class="icon-copy"></i></a><a class="btn btn-primary"
                                            href="#">18,000.00</a></h4>
                            </div>
                        </div>
                    </li>
                    <li class="span3">
                        <div class="thumbnail">
                            <a href="#">
                                <img src="AppResources/themes/images/products/chairs.jpg" alt=""  style="height:160px; width=160px;" /></a>
                            <div class="caption">
                                <h5>
                                    Office Chair</h5>
                                <p id="demo4" style="font-weight:bold">
                                </p>
                                <p style="color:Black; font-weight:bold;">Item Code : I004350</p>
                                <h4 style="text-align: center">
                                    <a class="btn" href="#"><i class="icon-zoom-in"></i></a><a class="btn"
                                        href="#">BID <i class="icon-copy"></i></a><a class="btn btn-primary"
                                            href="#">4,500.00 Rs</a></h4>
                            </div>
                        </div>
                    </li>
                    <li class="span3">
                        <div class="thumbnail">
                            <a href="#">
                                <img src="AppResources/themes/images/products/10.jpg" alt=""  style="height:160px; width=160px;" /></a>
                            <div class="caption">
                                <h5>
                                    Memory Card</h5>
                                <p id="demo5" style="font-weight:bold">
                                </p>
                                <p style="color:Black; font-weight:bold;">Item Code : I004178</p>
                                <h4 style="text-align: center">
                                    <a class="btn" href="#"><i class="icon-zoom-in"></i></a><a class="btn"
                                        href="#">BID <i class="icon-copy"></i></a><a class="btn btn-primary"
                                            href="#">1000.00 Rs</a></h4>
                            </div>
                        </div>
                    </li>
                    <li class="span3">
                        <div class="thumbnail">
                            <a href="#">
                                <img src="AppResources/themes/images/products/7.jpg" alt=""   style="height:160px; width=160px;"/></a>
                            <div class="caption">
                                <h5>
                                    Chip Reader</h5>
                                <p id="demo6" style="font-weight:bold">
                                </p>
                                <p style="color:Black; font-weight:bold;">Item Code : I004255</p>
                                <h4 style="text-align: center">
                                    <a class="btn" href="#"><i class="icon-zoom-in"></i></a><a class="btn"
                                        href="#">BID <i class="icon-copy"></i></a><a class="btn btn-primary"
                                            href="#">1000.00 Rs</a></h4>
                            </div>
                        </div>
                    </li>
                    <li class="span3">
                        <div class="thumbnail">
                            <a href="#">
                                <img src="AppResources/themes/images/products/sandiskPendrive.jpg" alt=""  style="height:160px; width=160px;" /></a>
                            <div class="caption">
                                <h5>
                                    Pen Drive</h5>
                                <p id="demo7" style="font-weight:bold">
                                   
                                </p>
                                <p style="color:Black; font-weight:bold;">Item Code : I004186</p>
                                <h4 style="text-align: center">
                                    <a class="btn" href="#"><i class="icon-zoom-in"></i></a><a class="btn"
                                        href="#">BID <i class="icon-copy"></i></a><a class="btn btn-primary"
                                            href="#">1000.00 Rs</a></h4>
                            </div>
                        </div>
                    </li>
                    <li class="span3">
                        <div class="thumbnail">
                            <a href="#">
                                <img src="AppResources/themes/images/products/toyotaYarisHeadLight.jpg" alt=""  style="height:160px; width=160px;"/></a>
                            <div class="caption">
                                <h5>
                                    Toyota Yaris Head Light</h5>
                                <p id="demo8" style="font-weight:bold">
                                  
                                </p>
                                <p style="color:Black; font-weight:bold;">Item Code : I004182</p>
                                <h4 style="text-align: center">
                                    <a class="btn" href="#"><i class="icon-zoom-in"></i></a><a class="btn"
                                        href="#">BID <i class="icon-copy"></i></a><a class="btn btn-primary"
                                            href="#">60,000.00 Rs</a></h4>
                            </div>
                        </div>
                    </li>
                    <li class="span3">
                        <div class="thumbnail">
                            <a href="#">
                                <img src="AppResources/themes/images/products/stationary.png" alt=""  style="height:160px; width=160px;" /></a>
                            <div class="caption">
                                <h5>
                                    Pens and Books</h5>
                                <p id="demo9" style="font-weight:bold">
                                  
                                </p>
                                <p style="color:Black; font-weight:bold;">Item Code : I004800</p>
                                <h4 style="text-align: center">
                                    <a class="btn" href="#"><i class="icon-zoom-in"></i></a><a class="btn"
                                        href="#">BID <i class="icon-copy"></i></a><a class="btn btn-primary"
                                            href="#">5000.00 Rs</a></h4>
                            </div>
                        </div>
                    </li>
                </ul>
                <hr class="soft" />
            </div>
        </div>
				<br class="clr">
					 </div>
		</div>
          </div>

	</div>
</div>
<script type="text/javascript">
    // Set the date we're counting down to
    var countDownDate = new Date("May 2, 2018 13:37:25").getTime();

    // Update the count down every 1 second
    var x = setInterval(function () {

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
        document.getElementById("demo").innerHTML = days + "d " + hours + "h " + minutes + "m " + seconds + "s ";

        // If the count down is finished, write some text 
        if (distance < 0) {
            clearInterval(x);
            document.getElementById("demo").innerHTML = "EXPIRED";
        }
    }, 1000);
</script>
</asp:Content>
