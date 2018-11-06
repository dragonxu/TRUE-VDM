<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Device_Verify.aspx.vb" Inherits="VDM.Device_Verify" %>


<html xmlns="http://www.w3.org/1999/xhtml">
<head>

    <meta http-equiv="Page-Exit" content="revealTrans(Duration=2.0,Transition=0)">
    <%--<meta http-equiv="Page-Enter" content="revealTrans(Duration=2.0,Transition=8)">--%>

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1,user-scalable=no">
    <title>Kiosk</title>
    <link href="css/true.css" rel="stylesheet" type="text/css" />
    <link href="css/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="css/font-awesome.min.css" rel="stylesheet">
    <link href="css/bootstrap-select.css" rel="stylesheet">
    <link href="css/true-popup.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="css/jquery.mCustomScrollbar.css">
     
     

    <link href="css/jquery.fancybox.css" rel="stylesheet" type="text/css" />
    <link href="css/slick.css" rel="stylesheet" type="text/css" />
    <link href="css/slick-theme.css" rel="stylesheet" type="text/css" />
    <link href="css/lightslider.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../Scripts/jquery.min.js"></script>
    <script type="text/javascript" src="js/bootstrap.js"></script>
    <script type="text/javascript" src="js/jquery.fancybox.js"></script>
    <script type="text/javascript" src="js/lightslider.js"></script>

    <!---------VDM----------->
    <script src="../Scripts/txtClientControl.js" type="text/javascript" ></script>
    <script src="../Scripts/extent.js" type="text/javascript"></script>

    <style>
        header {
            position: relative;
        }
    </style>
</head>
<body class="bg2">
    <form id="form1" runat="server">
<asp:ScriptManager ID="SCM" runat="server"></asp:ScriptManager>
        <div class="warp">
            <header>
                <img src="images/bg-top.png" />
            </header>
            <main>
                <div class="priceplan">
                    <div class="main3">
                        <div class="pic-step">
                            <p class="t-cart t-red true-b"><asp:Label ID="lblUI_SHOPINGCART" runat="server" Text ="SHOPPING CART"></asp:Label></p>
                            <p class="t-payment true-b"><asp:Label ID="lblUI_PAYMENT" runat="server" Text ="PAYMENT"></asp:Label></p>
                            <p class="t-complete true-b"><asp:Label ID="lblUI_COMPLETEORDER" runat="server" Text ="COMPLETE ORDER"></asp:Label></p>
                            <img src="images/pic-step2.png" />
                        </div>
                        <div class="description">
                            <div class="pic" style="padding: unset; text-align: center;">
                                <asp:Image ID="img" runat="server" Style="width: 70%; height:320px;"></asp:Image>
                            </div>
                            <figure class="col-md-6">
                                <div class="topic true-l">
                                    <h1 class="true-l" style="padding-bottom: 30px;">
                                        <asp:Label ID="lblDISPLAY_NAME" runat="server" Style="font-size: 60pt; line-height: 70px;"></asp:Label>
                                    </h1>
                                    <asp:Panel ID="pnlProduct" runat="server">
                                        <div class="capacity">
                                            <div style="float: left;">
                                                <asp:Panel ID="pnlCapacity" runat="server">
                                                    <p>
                                                        <asp:Label ID="lblCapacity" runat="server" Text=""></asp:Label>
                                                    </p>
                                                </asp:Panel>
                                            </div>
                                            <div class="color true-m" style="float: left;">
                                                <asp:Label ID="lblColor" runat="server" Text=""></asp:Label>
                                            </div>
                                        </div>
                                    </asp:Panel>


                                </div>

                            </figure>
                        </div>
                        <div class="step-cart">
                            <div class="total" style="margin-bottom: 30px;">
                                <h4 class="true-b t-red">
                                    <asp:Label ID="lblPrice_str" runat="server" Text="ยอดชำระ"></asp:Label>
                                    <i title="฿">
                                        <asp:Label ID="lblPrice_Money" runat="server" Text=""></asp:Label></i>
                                    <asp:Label ID="lblCurrency_Str" runat="server" Text="บาท"></asp:Label>

                                </h4>
                            </div>
                        </div>
                    </div>
                </div>
            </main>

 
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
        
<asp:UpdatePanel ID="UDPTerm" runat="server">
        <ContentTemplate>


        <div style="position: absolute; top: 300px; right: 100px; display: none;" class="row">

            <div style="display: block;" class="col-lg-3">
                ID_number :<asp:TextBox ID="id_number" runat="server" AutoPostBack="true"></asp:TextBox>
            </div>
            <div style="display: block;" class="col-lg-3">
                Face_cust_certificate :<asp:TextBox ID="Face_cust_certificate" runat="server" AutoPostBack="true" TextMode="MultiLine" Style="margin: 0px; width: 161px; height: 131px;"></asp:TextBox>
            </div>
            <div style="display: block;" class="col-lg-3">
                Face_cust_capture :<asp:TextBox ID="Face_cust_capture" runat="server" AutoPostBack="true" TextMode="MultiLine" Style="margin: 0px; width: 161px; height: 131px;"></asp:TextBox>
            </div>

            <%--ID Card--%>
            <div style="display: block;" class="col-lg-3">
                CUS_TITLE :<asp:TextBox ID="CUS_TITLE" runat="server" AutoPostBack="true"></asp:TextBox>
            </div>
            <div style="display: block;" class="col-lg-3">
                CUS_NAME :<asp:TextBox ID="CUS_NAME" runat="server" AutoPostBack="true"></asp:TextBox>
            </div>
            <div style="display: block;" class="col-lg-3">
                CUS_SURNAME :<asp:TextBox ID="CUS_SURNAME" runat="server" AutoPostBack="true"></asp:TextBox>
            </div>
            <div style="display: block;" class="col-lg-3">
                NAT_CODE :<asp:TextBox ID="NAT_CODE" runat="server" AutoPostBack="true"></asp:TextBox>
            </div>
            <div style="display: block;" class="col-lg-3">
                CUS_GENDER :<asp:TextBox ID="CUS_GENDER" runat="server" AutoPostBack="true"></asp:TextBox>
            </div>
            <div style="display: block;" class="col-lg-3">
                CUS_BIRTHDATE :<asp:TextBox ID="CUS_BIRTHDATE" runat="server" AutoPostBack="true"></asp:TextBox>
            </div>
            <div style="display: block;" class="col-lg-3">
                CUS_PID :<asp:TextBox ID="CUS_PID" runat="server" AutoPostBack="true"></asp:TextBox>
            </div>
            <div style="display: block;" class="col-lg-3">
                CUS_PASSPORT_ID :<asp:TextBox ID="CUS_PASSPORT_ID" runat="server" AutoPostBack="true"></asp:TextBox>
            </div>
            <div style="display: block;" class="col-lg-3">
                CUS_PASSPORT_START :<asp:TextBox ID="CUS_PASSPORT_START" runat="server" AutoPostBack="true"></asp:TextBox>
            </div>
            <div style="display: block;" class="col-lg-3">
                CUS_PASSPORT_EXPIRE :<asp:TextBox ID="CUS_PASSPORT_EXPIRE" runat="server" AutoPostBack="true"></asp:TextBox>
            </div>
            <div style="display: block;" class="col-lg-3">
                CUS_IMAGE :<asp:TextBox ID="CUS_IMAGE" runat="server" AutoPostBack="true"></asp:TextBox>
            </div>
            
            <asp:Button ID="btnFace_Recognition" runat="server" />
        </div>



        <div id="popupVerifing" class="popup">
          <div class="popup-frame">
            <h3 class="true-m">กำลังตรวจสอบความตรงกันของใบหน้า</h3>
            <div class="icon"><img src="images/Popup/icon-scanface-gif.gif"/></div>
            <h4 class="true-b">กรุณารอสักครู่</h4>
          </div>
        </div><a id="clickVerifing" href="#popupVerifing" style="display:none;">Click</a>

        <div id="popupCannotVerify" class="popup">
            <div class="popup-frame" >
                <h3 class="true-m" id="idCardAlertReason">ขออภัย ใบหน้าไม่ตรงกับบัตร</h3>
                <div class="icon"><img src="images/Popup/icon-warning-face-gif.gif"/></div>
                <h4 class="true-b">กรุณาสแกนใบหน้าอีกครั้ง</h4>
                <div class="bottom"><a class="btu true-l" onclick="backToScan();" href="javascript:;">ตกลง</a></div>
            </div>
        </div> <a id="clickCannotVerify" href="#popupCannotVerify" style="display:none;">Click</a>
 
        <div id="popupNetworkProblem" class="popup">
            <div class="popup-frame" >
                <h3 class="true-m" id="idCardAlertReason">ไม่สามารถเชื่อมต่อระบบเครือข่าย</h3>
                <div class="icon"><img src="images/Popup/icon-warning-face-gif.gif"/></div>
                <h4 class="true-b">กรุณาสแกนใบหน้าอีกครั้ง</h4>
                <div class="bottom"><a class="btu true-l" onclick="backToScan();" href="javascript:;">ตกลง</a></div>
            </div>
        </div> <a id="clickNetworkProblem" href="#popupNetworkProblem" style="display:none;">Click</a>

        </ContentTemplate>
</asp:UpdatePanel>


        <script type="text/javascript">


            $("#clickVerifing").fancybox();
            $("#clickCannotVerify").fancybox();
            $("#clickNetworkProblem").fancybox();

            function showFaceProblem() {
                $('#clickCannotVerify').click();
                $('#popupCannotVerify').find(".fancybox-close").on('click', function () {
                    backToScan();
                });
            }

            function showNetworkProblem() {
                $('#clickNetworkProblem').click();
                $('#popupNetworkProblem').find(".fancybox-close").on('click', function () {
                    backToScan();
                });
            }

            function backToScan() {
                $.fancybox.close(); 
                setTimeout(function () { history.back(); }, 300);
            }

            // เริ่มจ่าย 
            function Face_Recognition() {
                $('#btnFace_Recognition').click();
            }

            //--------- เริ่มที่นี่ --------------//
            setTimeout(function () { $("#clickVerifing").click(); }, 200);
            setTimeout(function () { Face_Recognition(); }, 300);


        </script>


    </form>
</body>
</html>

