<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Device_Verify.aspx.vb" Inherits="VDM.Device_Verify" %>

<%@ Register Src="~/Front_UI/UC_Scan_IDCart.ascx" TagPrefix="uc1" TagName="UC_Scan_IDCart" %>
<%@ Register Src="~/Front_UI/UC_Scan_Face_Recognition.ascx" TagPrefix="uc1" TagName="UC_Scan_Face_Recognition" %>



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
                            <p class="t-cart t-red true-b">SHOPING CART</p>
                            <p class="t-payment t-red true-b">PAYMENT</p>
                            <p class="t-complete true-b">COMPLETE ORDER</p>
                            <img src="images/pic-step2.png" />
                        </div>
                        <div class="step" id="formStep" runat="server">
                            <div class="step-payment">
                                <%--<h3 class="t-red true-b">รวมยอดชำระ <i title="฿">27,000</i></h3>--%>
                                <h3 class="true-b t-red">
                                    <asp:Label ID="lblPrice_str" runat="server" Text="ยอดชำระ"></asp:Label>
                                    <i title="฿">
                                        <asp:Label ID="lblPrice_Money" runat="server" Text="39,000"></asp:Label></i>
                                    <asp:Label ID="lblCurrency_Str" runat="server" Text="บาท"></asp:Label></h3>
                                <asp:Panel ID="pnlScanIDCard" runat="server">

                                    <uc1:UC_Scan_IDCart runat="server" ID="UC_Scan_IDCart" />



                                </asp:Panel>
                                <asp:Panel ID="pnlFace_Recognition" runat="server">
                                    <uc1:UC_Scan_Face_Recognition runat="server" ID="UC_Scan_Face_Recognition" />
                                </asp:Panel>
                                <div class="col-md-12">
                                    <asp:Button ID="btnSkip_ScanIDCard" runat="server" class="btu true-bs" Style="background: #635b5b; padding: 0 50px 0 50px; float: right; margin-top: 100px;" Text="ข้าม" />
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </main>

            <asp:Panel ID="pnlModul_IDCard_Success" runat="server">

                <div class="fancybox-overlay fancybox-overlay-fixed" style="width: auto; height: auto; display: block;">
                    <div class="fancybox-wrap fancybox-desktop fancybox-type-inline fancybox-opened" tabindex="-1" style="width: 818px; height: auto; position: absolute; top: 250px; left: 122px; opacity: 1; overflow: visible;">
                        <div class="fancybox-skin" style="padding: 0px; width: auto; height: auto;">
                            <div class="fancybox-outer">
                                <div class="fancybox-inner" style="overflow: auto; width: 818px; height: auto;">
                                    <div id="popup2" style="display: block;">

                                        <div class="privilege">

                                            <h3 class="true-m" style="margin: 20px 0 60px 0;">สำเร็จ</h3>
                                            <div class="idcard">
                                                <img src="images/pic-idcard2.png">
                                            </div>

                                            <%--<h4 class="true-m t-red">อย่าลืมเก็บบัตรของท่าน</h4>--%>
                                            <div class="icon" style="margin: 0px 0 50px 0">
                                                <asp:LinkButton ID="btnStart_Take_Photos" runat="server" class="btu true-l" Text="เริ่มแสกนใบหน้า"></asp:LinkButton>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </div>
                            <%--<a title="Close" class="fancybox-item fancybox-close" href="javascript:;"></a>--%>
                            <asp:LinkButton ID="lnkClose" runat="server" class="fancybox-item fancybox-close"></asp:LinkButton>
                        </div>
                    </div>
                </div>

            </asp:Panel>

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
            <%--<div style="display: block;" class="col-lg-3">
                                                Partner_code :
                                                <asp:TextBox ID="partner_code" runat="server" AutoPostBack ="true" ></asp:TextBox>
                                            </div>--%>
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
        </ContentTemplate>
</asp:UpdatePanel>

        <div id="popupVerifing">
          <div class="popup-frame">
            <h3 class="true-m">กำลังดำเนินการตรวจสอบ</h3>
            <div class="icon"><img src="images/Popup/icon-scanface.png"/></div>
            <h4 class="true-b">กรุณารอสักครู่</h4>
          </div>
        </div><a id="clickVerifing" href="#popupVerifing" style="display:none;">Click</a>
        
        <div id="popupCannotVerify">
          <div class="popup-frame">
            <h3 class="true-m">ขออภัย ระบบไม่สามารถอ่านใบหน้าได้</h3>
            <div class="icon"><img src="images/Popup/icon-warning-face.png"/></div>
            <h4 class="true-b">กรุณาสแกนใบหน้าอีกครั้ง</h4>
            <div class="bottom"><a class="btu true-l" onclick="backToScan();" href="javascript:;">ตกลง</a></div>
          </div>
        </div>
        <a id="clickCannotVerify" href="#popupCannotVerify" style="display:none;">Click</a>

        <script type="text/javascript">


            $("#clickVerifing").fancybox();
            $("#clickCannotVerify").fancybox();


            function showFaceProblem() {
                $('#clickCannotVerify').click();
                $('#popupCannotVerify').find(".fancybox-close").on('click', function () {
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

