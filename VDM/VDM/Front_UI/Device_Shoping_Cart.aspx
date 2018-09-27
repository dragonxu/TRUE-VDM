<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Device_Shoping_Cart.aspx.vb" Inherits="VDM.Device_Shoping_Cart" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Page-Exit" content="revealTrans(Duration=2.0,Transition=0)">

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1,user-scalable=no">
    <title>Kiosk</title>
    <link href="css/true.css" rel="stylesheet" type="text/css" />
    <link href="css/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="css/font-awesome.min.css" rel="stylesheet">
    <link href="css/bootstrap-select.css" rel="stylesheet">
    <script type="text/javascript" src="js/jquery-1.12.2.min.js"></script>
    <script type="text/javascript" src="js/bootstrap.js"></script>

    <link rel="stylesheet" href="css/jquery.mCustomScrollbar.css">


    <style>
        header {
            position: relative;
        }
    </style>
</head>
<body class="bg2">
    <form id="form1" runat="server">
        <div class="warp">
            <header>
                <img src="images/bg-top.png" />
            </header>
            <main>
                <div class="priceplan">
                    <div class="main3">
                        <div class="pic-step">
                            <p class="t-cart t-red true-b">SHOPING CART</p>
                            <p class="t-payment true-b">PAYMENT</p>
                            <p class="t-complete true-b">COMPLETE ORDER</p>
                            <img src="images/pic-step1.png" />
                        </div>
                        <div class="description">
                            <div class="pic">
                                <img src="images/iphonex-white.png" style="width: 70%;">
                            </div>
                            <figure class="col-md-6">
                                <div class="topic true-l">
                                    <h1 class="true-l" style="padding-bottom: 30px;">
                                        <asp:Label ID="lblDISPLAY_NAME" runat="server" Text="iPhone X" Style="font-size: 78pt;"></asp:Label>

                                    </h1>
                                    <asp:Panel ID="pnlCapacity" runat="server">
                                        <div class="capacity">
                                            <p>
                                                <asp:Label ID="lblCapacity" runat="server" Text="64GB"></asp:Label>
                                            </p>
                                            <div class="color true-m">
                                                <asp:Label ID="lblColor" runat="server" Text="SILVER"></asp:Label>
                                            </div>
                                        </div>
                                    </asp:Panel>


                                </div>
                                <span class="cart1 true-l">
                                    <p class="true-m">
                                        <asp:Label ID="lblHeader_Package" runat="server" Text="พร้อมสมัครแพ็กเกจ"></asp:Label>
                                    </p>
                                    <br />
                                    <asp:Label ID="lblDetail_Package" runat="server" Text="4G+ FUN Unlimited 1499 -1899"></asp:Label>
                                </span>
                                <span class="cart1 true-l">
                                    <p class="true-m">
                                        <asp:Label ID="lblHeader_Promotion" runat="server" Text="สิทธิพิเศษจากทรูยู"></asp:Label>
                                    </p>
                                    <br />
                                    <asp:Label ID="lblDetail_Promotion" runat="server" Text="ทรูแบลคการ์ด ซื้อเคส UAG ในราคา 500.-"></asp:Label>

                                </span>
                            </figure>
                        </div>

                        <div class="step-cart">
                            <div class="total" style="margin-bottom: 30px;">
                                <h4 class="true-b t-red">
                                    <asp:Label ID="lblPrice_str" runat="server" Text="ยอดชำระ"></asp:Label>
                                    <i title="฿">
                                        <asp:Label ID="lblPrice_Money" runat="server" Text="39,000"></asp:Label></i>
                                    <asp:Label ID="lblCurrency_Str" runat="server" Text="บาท"></asp:Label></h4>
                            </div>

                            <%--cart-step2.html--%>
                        </div>

                        <div class="term" style="margin: unset;">
                            <h2 class="true-m">Term & Condition</h2>
                            <div class="frame" >
                                <span id="content-d3" class="light">
                                    <p class="true-l">
                                       <%-- <asp:Label  class="true-l" ID="lblTerm_Condition" runat="server" Text=""></asp:Label>--%>
                                        ข้อกำหนดและเงื่อนไขรายการส่งเสริมการขาย 4G+ FUN Unlimited 299, 4G+ FUN Unlimited 399, 4G+ FUN Unlimited 499, 4G+ FUN Unlimited 599, 4G+ FUN Unlimited 699, 4G+ FUN Unlimited 899, 4G+ FUN Unlimited 1099, 4G+ FUN Unlimited 1299, 4G+ FUN Unlimited 1499, และ 4G+ FUN Unlimited 1899
                                        <br />
                                        <br />
                                        1. รายการส่งเสริมการขายนี้ สำหรับผู้สมัครใช้บริการเลขหมายใหม่โทรศัพท์เคลื่อนที่ ทรูมูฟ เอช ของบริษัท เรียล มูฟ จำกัด (“เรียลมูฟ”) ระบบรายเดือน ในนามบุคคลธรรมดา ตั้งแต่วันที่ 6 กุมภาพันธ์ 2559 ถึง 31 มีนาคม 2561 หรือจนกว่าจะมีการเปลี่ยนแปลง
                                        <br />
                                        2. รายละเอียดรายการส่งเสริมการขาย: อัตราค่าบริการยังไม่รวมภาษีมูลค่าเพิ่ม<br />
                                        2.1 รายการส่งเสริมการขาย 4G+ FUN Unlimited 299<br />
                                        สิทธิตามแพ็กเกจปกติ:<br />
                                        คิดอัตราค่าใช้บริการเหมาจ่ายขั้นต่ำรายเดือน 299 บาท ต่อเดือน ผู้ใช้บริการจะได้รับสิทธิใช้บริการ ดังนี้<br />
                                        (1) โทรทุกเครือข่ายจำนวน 100 นาทีต่อเดือน<br />
                                        (2) บริการ 4G ที่ความเร็วสูงสุด 300 เมกะบิตต่อวินาที (Mbps) และบริการ 3G/EDGE/GPRS ที่ความเร็วสูงสุด 42 เมกะบิตต่อวินาที (Mbps) เป็นจำนวน 1 กิกะไบต์ (GB)หลังจากนั้น จะใช้ได้ไม่จำกัดปริมาณที่ความเร็วสูงสุดไม่เกิน 128 กิโลบิตต่อวินาที (Kbps)<br />
                                        (3) บริการ Wi-Fi ที่ความเร็วสูงสุด 200 เมกะไบต์ต่อวินาที (Mbps) ไม่จำกัดปริมาณการใช้งาน<br />
                                        ได้ไม่จำกัดปริมาณการใช้งาน ที่ความเร็วสูงสุด 300 เมกะบิตต่อวินาที(Mbps)
                                        <br />
                                        <br />
                                        เป็นระยะเวลา 12 เดือน (รอบบิล) ผ่าน URL ที่เป็นไปตามข้อกำหนดและเงื่อนไขการใช้งานของแต่ละบริการ
                                        โดยสามารถรับไอเทมโค้ดได้ 1 ครั้ง
                                                    ผ่านแอปพลิเคชัน TrueID โดยลูกค้าจะต้องไม่มียอดค้างชำระตั้งแต่รอบบิลแรกเป็นต้นไป กล่อง TMH Exclusive Legend ประกอบด้วย Dragon Master Zuka (Exclusive Skin) หรือ ชิ้นส่วนฮีโร่ 40 ชิ้น หรือ ชิ้นส่วนฮีโร่ 20 ชิ้น หรือ ชิ้นส่วนฮีโร่ 10 ชิ้น โดยลูกค้าจะได้รับไอเทมใดไอเทมหนึ่ง
                                                    ไอเทมพิเศษที่ได้รับ มีจำนวนจำกัด ไม่สามารถแลกเปลี่ยนเป็นอย่างอื่นได้ บนโทรศัพท์เคลื่อนที่หรือแท๊บเล็ต ในกรณีที่ผู้ใช้บริการใช้ website อื่นหรือลิงค์หรือการแชร์ที่ปรากฏบนหน้าแอปพลิเคชันTrueID ผู้ใช้บริการจะต้องชำระค่าใช้บริการเพิ่มเติม(ถ้ามี)ตามอัตราที่ระบุในแพ็กเกจที่ผู้ใช้บริการเลือกใช้งานอยู่ ณ ขณะนั้น หรือหากในแพ็กเกจที่เลือกใช้ ไม่ได้ระบุไว้ ให้คิดค่าบริการตามเงื่อนไขที่บริษัทกำหนด
                                        เฉพาะบริการภายในแอปพลิเคชันเกม ROV บนโทรศัพทํเคลื่อนที่ หรือแท๊บเล็ต ในกรณีที่ ผู้ใช้บริการใช้ website อื่นหรือลิงค์หรือการแชร์ที่ปรากฏบนหน้าแอปพลิเคชันเกม ROV ผู้ใช้บริการจะต้องชำระค่าใช้บริการเพิ่มเติม (ถ้ามี) ตามอัตราที่ระบุในแพ็คเกจที่ผู้ใช้บริการเลือกใช้งานอยู่ ณ ขณะนั้น หรือหากในแพ็คเกจที่เลือกใช้ ไม่ได้ระบุไว้ ให้คิดค่าบริการตามเงื่อนไขที่บริษัทกำหนด
                                    </p>
                                </span>
                            </div>
                        </div>
                        <label class="check true-l">
                            ข้าพเจ้าได้อ่านข้อกำหนดและเงื่อนไขทุกข้อแล้ว
          <input type="checkbox" checked="checked">
                            <span class="checkmark"></span>
                        </label>


                        <div class="col-md-12" style="text-align: center;">
                            <asp:Button ID="btnConfirm_str" runat="server" class="order true-m" Text="ชำระเงิน" />
                        </div>



                    </div>
                </div>
            </main>
      <%--      <footer>
                <nav>
                    <div class="main">
                        <span class="col-md-6"><a href="home.html">
                            <img src="images/btu-home.png" /></a></span>
                        <span class="col-md-6"><a href="javascript:history.back();">
                            <img src="images/btu-prev.png" /></a></span>
                    </div>
                </nav>
            </footer>--%>
               <footer style="bottom: 0px">
                <nav>
                    <div class="main">
                        <span class="col-md-6">
                            <asp:ImageButton ID="lnkHome" runat="server" ImageUrl="images/btu-home.png" />
                        </span>
                        <span class="col-md-6">
                            <asp:ImageButton ID="lnkBack" runat="server" ImageUrl="images/btu-prev.png" />
                        </span>
                    </div>
                </nav>
            </footer>
        </div>

    </form>
</body>

<script src="js/jquery.mCustomScrollbar.js"></script>

<script>
    (function ($) {
        $(window).on("load", function () {

            $.mCustomScrollbar.defaults.scrollButtons.enable = true; //enable scrolling buttons by default
            $.mCustomScrollbar.defaults.axis = "yx"; //enable 2 axis scrollbars by default

            $("#content-d3").mCustomScrollbar({ theme: "dark-3" });

        });
    })(jQuery);
</script>
</html>

