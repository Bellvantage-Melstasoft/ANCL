<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingSupplier.Master" AutoEventWireup="true"
    CodeBehind="ItemsDetailedWise.aspx.cs" Inherits="BiddingSystem.ItemsDetailedWise"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="span9">
        <ul class="breadcrumb">
            <li><a href="SupplierIndex.aspx">Home</a> <span class="divider">/</span></li>
            <li class="active">Special offers</li>
        </ul>
        <h4>
            Items Detailed <small class="pull-right"> </small>
        </h4>
        <hr class="soft" />
        <form class="form-horizontal span6">
        <div class="control-group">
            <label class="control-label alignL">
                Sort By
            </label>
            <select>
                <option>Priduct name A - Z</option>
                <option>Priduct name Z - A</option>
                <option>Price Lowest first</option>
            </select>
        </div>
        </form>
        <div id="myTab" class="pull-right">
            <a href="#listView" data-toggle="tab"><span class="btn btn-large"><i class="icon-list">
            </i></span></a><a href="#blockView" data-toggle="tab"><span class="btn btn-large btn-primary">
                <i class="icon-th-large"></i></span></a>
        </div>
        <br class="clr" />
        <div class="tab-content">
            <%--<div class="tab-pane" id="listView">
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
            </div>--%>
            <div class="tab-pane  active" id="blockView">
                <ul class="thumbnails">
                    <li class="span3">
                        <div class="thumbnail">
                            <a href="#">
                                <img src="AppResources/themes/images/LaptopImage01.png" alt="" style="height:160px; width=160px;"/></a>
                            <div class="caption">
                                <h5>
                                    HP Pavilion</h5>
                                <p id="demo" style="font-weight:bold; color:red"></p>
                                <p style="color:Black;"><img src="AppResources/images/ml.jpg" height="20" width="20" />Melsta Logistics</p>
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
                                <p id="demo2" style="font-weight:bold; color:red"></p>
                                <p style="color:Black"><img src="AppResources/images/ml.jpg" height="20" width="20" /> Melsta Logistics</p>
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
                                 <p id="demo3" style="font-weight:bold; color:red"></p>
                                <p style="color:Black"><img src="AppResources/images/lankabelg.gif" height="20" width="20" /> Lanka Bell</p>
                                
                                <h4 style="text-align: center">
                                    <a class="btn" href="#"><i class="icon-zoom-in"></i></a><a class="btn"
                                        href="#">BID <i class="icon-copy"></i></a><a class="btn btn-primary"
                                            href="#">18,000.00</a></h4>
                            </div>
                        </div>
                    </li>
                  </ul>
                 <ul class="thumbnails">
                    <li class="span3">
                        <div class="thumbnail">
                            <a href="#">
                                <img src="AppResources/themes/images/products/chairs.jpg" alt=""  style="height:160px; width=160px;" /></a>
                            <div class="caption">
                                <h5>
                                    Office Chair</h5>
                                <p id="demo4" style="font-weight:bold; color:red"></p>
                                <p style="color:Black;"><img src="AppResources/images/BV.jfif" height="20" width="20" /> Bellvantage(PVT)Ltd</p>
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
                                <p id="demo5" style="font-weight:bold; color:red"></p>
                                <p style="color:Black;"><img src="AppResources/images/BV.jfif" height="20" width="20" /> Bellvantage(PVT)Ltd</p>
                                
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
                                <p id="demo6" style="font-weight:bold; color:red"></p>
                                <p style="color:Black;"><img src="AppResources/images/BV.jfif" height="20" width="20" /> Bellvantage(PVT)Ltd</p>
                                <h4 style="text-align: center">
                                    <a class="btn" href="#"><i class="icon-zoom-in"></i></a><a class="btn"
                                        href="#">BID <i class="icon-copy"></i></a><a class="btn btn-primary"
                                            href="#">1000.00 Rs</a></h4>
                            </div>
                        </div>
                    </li>
                     </ul>
                <ul class="thumbnails">
                    <li class="span3">
                        <div class="thumbnail">
                            <a href="#">
                                <img src="AppResources/themes/images/products/sandiskPendrive.jpg" alt=""  style="height:160px; width=160px;" /></a>
                            <div class="caption">
                                <h5>
                                    Pen Drive</h5>
                                <p id="demo7" style="font-weight:bold; color:red"></p>
                                <p style="color:Black"><img src="AppResources/images/lankabelg.gif" height="20" width="20" /> Lanka Bell</p>
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
                                <p id="demo8" style="font-weight:bold; color:red"></p>
                                <p style="color:Black"><img src="AppResources/images/ml.jpg" height="20" width="20" />Melsta Logistics</p>
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
                                <p id="demo9" style="font-weight:bold; color:red"></p>
                                <p style="color:Black;"><img src="AppResources/images/BV.jfif" height="20" width="20" /> Bellvantage(PVT)Ltd</p>
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
        <a href="#" class="btn btn-large pull-right">Compair Product</a>
        <div class="pagination">
            <ul>
                <li><a href="#">&lsaquo;</a></li>
                <li><a href="#">1</a></li>
                <li><a href="#">2</a></li>
                <li><a href="#">3</a></li>
                <li><a href="#">4</a></li>
                <li><a href="#">...</a></li>
                <li><a href="#">&rsaquo;</a></li>
            </ul>
        </div>
        <br class="clr" />
    </div>
    <!-- MainBody End ============================= -->

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
                    document.getElementById("demo2").innerHTML = days + "d " + hours + "h " + minutes + "m " + seconds + "s ";

                    document.getElementById("demo3").innerHTML = days + "d " + hours + "h " + minutes + "m " + seconds + "s ";

                    document.getElementById("demo4").innerHTML = days + "d " + hours + "h " + minutes + "m " + seconds + "s ";
                    document.getElementById("demo5").innerHTML = days + "d " + hours + "h " + minutes + "m " + seconds + "s ";
                    document.getElementById("demo6").innerHTML = days + "d " + hours + "h " + minutes + "m " + seconds + "s ";

                    document.getElementById("demo7").innerHTML = days + "d " + hours + "h " + minutes + "m " + seconds + "s ";
                    document.getElementById("demo8").innerHTML = days + "d " + hours + "h " + minutes + "m " + seconds + "s ";
                    document.getElementById("demo9").innerHTML = days + "d " + hours + "h " + minutes + "m " + seconds + "s ";

                    // If the count down is finished, write some text 
                    if (distance < 0) {
                        clearInterval(x);
                        document.getElementById("demo").innerHTML = "EXPIRED";
                        document.getElementById("demo2").innerHTML = "EXPIRED";

                        document.getElementById("demo3").innerHTML = "EXPIRED";
                        document.getElementById("demo4").innerHTML = "EXPIRED";

                        document.getElementById("demo5").innerHTML = "EXPIRED";
                        document.getElementById("demo6").innerHTML = "EXPIRED";

                        document.getElementById("demo7").innerHTML = "EXPIRED";
                        document.getElementById("demo8").innerHTML = "EXPIRED";
                        document.getElementById("demo9").innerHTML = "EXPIRED";
                    }
                }, 1000);
</script>
</asp:Content>
