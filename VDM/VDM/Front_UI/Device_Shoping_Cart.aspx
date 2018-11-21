<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Device_Shoping_Cart.aspx.vb" Inherits="VDM.Device_Shoping_Cart" %>

<%@ Register Src="~/Front_UI/UC_CommonUI.ascx" TagPrefix="uc1" TagName="UC_CommonUI" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Page-Exit" content="revealTrans(Duration=5.0,Transition=0)">
    <meta http-equiv="Page-Enter" content="revealTrans(Duration=5.0,Transition=8)">

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1,user-scalable=no">

    <title>Kiosk</title>

    <link href="css/true.css" rel="stylesheet" type="text/css" />
    <link href="css/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="css/font-awesome.min.css" rel="stylesheet">
    <link href="css/bootstrap-select.css" rel="stylesheet">    
    <link href="css/true-popup.css" rel="stylesheet" type="text/css" />
    <link href="css/jquery.fancybox.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="css/jquery.mCustomScrollbar.css">

    <script type="text/javascript" src="../Scripts/jquery.min.js"></script>
    <script type="text/javascript" src="js/bootstrap.js"></script>
    <script type="text/javascript" src="js/jquery.fancybox.js"></script>
    <script src="js/jquery.mCustomScrollbar.js"></script>
    
    <!---------VDM----------->
    <script src="../Scripts/txtClientControl.js" type="text/javascript" ></script>
    <script src="../Scripts/extent.js" type="text/javascript"></script>

    <style type="text/css">
        header {
            position: relative;
        }

        .default {
            background: #5a5454 url(images/icon-cart.png) no-repeat left 40px top 10px !important;
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
                            <img src="images/pic-step1.png" />
                        </div>
                        <div class="description">
                            <div class="pic" style="padding: unset; text-align: center;">
                                <asp:Image ID="img" runat="server" Style="width: 60%;" ></asp:Image>
<%--                                <asp:Image ID="img" runat="server" Style="width: 70%; height:320px;"></asp:Image>--%>
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
                                    <i title="฿" id="i_Money" runat ="server" >
                                        <asp:Label ID="lblPrice_Money" runat="server" Text=""></asp:Label></i>
                                    <asp:Label ID="lblCurrency_Str" runat="server" Text="บาท"></asp:Label>

                                </h4>
                            </div>
                        </div>

                        <div class="term" style="height:460px; margin:unset;">
                            <h2 class="true-m"><asp:Label ID="lblUI_Term" runat="server" Text ="Term & Condition"></asp:Label></h2>
                            <div class="frame" style="height: 380px;">
                                <span id="content-d3" class="light" style="height:340px; overflow:hidden;">
                                    <p id="p_term" runat ="server"  class="true-l" style ="width :unset ;">
                                        <asp:Label ID="lblDescription" runat="server"></asp:Label>
                                    </p>
                                </span>
                            </div>
                        </div>

                <asp:UpdatePanel ID="UDPTerm" runat="server">
                <ContentTemplate>
                
                        <label class="check true-l" style="height:100px;">
                            <asp:Label ID="lblUI_Accept" runat="server" Text ="ข้าพเจ้าได้อ่านข้อกำหนดและเงื่อนไขทุกข้อแล้ว"></asp:Label>
                            <asp:CheckBox ID="chkActive" runat="server" Checked="false" AutoPostBack="true" />
                            <span class="checkmark"></span>
                        </label>

                        <div class="col-md-12" style="text-align: center;">
                                <asp:Button ID="btnConfirm" runat="server"  CssClass="order true-m default " Text="ชำระเงิน" />
                                  <%--  <a id="lnkSlip" class="test-click true-bs" href="#popupTH">ชำระเงิน</a>--%>
                               <%-- <a ID="lnkTH" runat="server"  href="#popupTH"  CssClass="order true-m default " >ชำระเงิน</a>--%>
                                <asp:Button ID="btnVerify" runat="server" CssClass="order true-m default " Text="สั่งซื้อ" />
                                <asp:Button ID="btnVerifySlip" runat="server" CssClass="order true-m default " Text="สั่งซื้อ" Style ="display:none;" />
                        </div>
                </ContentTemplate>
                </asp:UpdatePanel>

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
                <asp:TextBox ID="txtLocalControllerURL" runat="server" Style="display:none;"></asp:TextBox>

            </footer>
        </div>

        <uc1:UC_CommonUI runat="server" ID="UC_CommonUI" />

        
        <asp:UpdatePanel ID="UDBVerify" runat="server">
        <ContentTemplate>
        
            <div style="position:fixed; top:40px; left:-800px;"> <!--ID Card-->
                    <h4>IDCard</h4>
                    Citizenid : <asp:Textbox ID="id_Citizenid" runat="Server" /><br/>
                    Th_Prefix : <asp:Textbox ID="id_Th_Prefix" runat="Server" /><br/>
                    Th_Firstname : <asp:Textbox ID="id_Th_Firstname" runat="Server" /><br/>
                    Th_Middlename : <asp:Textbox ID="id_Th_Middlename" runat="Server" /><br/>
                    Th_Lastname : <asp:Textbox ID="id_Th_Lastname" runat="Server" /><br/>
                    En_Prefix : <asp:Textbox ID="id_En_Prefix" runat="Server" /><br/>
                    En_Firstname: <asp:Textbox ID="id_En_Firstname" runat="Server" /><br/>
                    En_Middlename : <asp:Textbox ID="id_En_Middlename" runat="Server" /><br/>
                    En_Lastname : <asp:Textbox ID="id_En_Lastname" runat="Server" /><br/>
                    Sex : <asp:Textbox ID="id_Sex" runat="Server" /><br/>
                    Birthday : <asp:Textbox ID="id_Birthday" runat="Server" /><br/>
                    Address : <asp:Textbox ID="id_Address" runat="Server" /><br/>
                    addrHouseNo : <asp:Textbox ID="id_addrHouseNo" runat="Server" /><br/>
                    addrVillageNo : <asp:Textbox ID="id_addrVillageNo" runat="Server" /><br/>
                    addrLane : <asp:Textbox ID="id_addrLane" runat="Server" /><br/>
                    addrRoad : <asp:Textbox ID="id_addrRoad" runat="Server" /><br/>
                    addrTambol : <asp:Textbox ID="id_addrTambol" runat="Server" /><br/>
                    addrAmphur : <asp:Textbox ID="id_addrAmphur" runat="Server" /><br/>
                    addrProvince : <asp:Textbox ID="id_addrProvince" runat="Server" /><br/>
                    Issue : <asp:Textbox ID="id_Issue" runat="Server" /><br/>
                    Issuer : <asp:Textbox ID="id_Issuer" runat="Server" /><br/>
                    Expire : <asp:Textbox ID="id_Expire" runat="Server" /><br/>
                    Photo : <asp:Textbox ID="id_Photo" runat="Server" TextMode="MultiLine" /><br/>
                    <h4>Passport</h4>
                    FirstName : <asp:Textbox ID="pass_FirstName" runat="Server" /><br/>
                    MiddleName : <asp:Textbox ID="pass_MiddleName" runat="Server" /><br/>
                    LastName : <asp:Textbox ID="pass_LastName" runat="Server" /><br/>
                    DocType : <asp:Textbox ID="pass_DocType" runat="Server" /><br/>
                    Nationality : <asp:Textbox ID="pass_Nationality" runat="Server" /><br/>
                    PassportNo : <asp:Textbox ID="pass_PassportNo" runat="Server" /><br/>
                    DateOfBirth : <asp:Textbox ID="pass_DateOfBirth" runat="Server" /><br/>
                    Sex : <asp:Textbox ID="pass_Sex" runat="Server" /><br/>
                    Expire : <asp:Textbox ID="pass_Expire" runat="Server" /><br/>
                    PersonalID : <asp:Textbox ID="pass_PersonalID" runat="Server" /><br/>
                    IssueCountry : <asp:Textbox ID="pass_IssueCountry" runat="Server" /><br/>
                    MRZ : <asp:Textbox ID="pass_MRZ" runat="Server" /><br/>
                    Photo : <asp:Textbox ID="pass_Photo" runat="Server" TextMode="MultiLine" /><br/>

                    <asp:Button ID="btnKeepInfo" runat="server" Text="KeepInfo" />
              </div>

        </ContentTemplate>
        </asp:UpdatePanel>

        <div id="popupIDCard" class="popup" >
          <div class="popup-frame" >
                <h3 class="true-m"><asp:Label ID="lblpopupIDCard_Header" runat="server" Text ="ขั้นตอนการยืนยันตัวบุคคล"></asp:Label></h3>
                <div class="icon"><img src="images/Popup/icon-idcard-gif.gif"/></div>
                <h4 class="true-b" style="padding-bottom:30px;"><asp:Label ID="lblpopupIDCard_Detail" runat="server" Text ="กรุณาสอดบัตรประชาชนของท่าน"></asp:Label></h4>
          </div>
        </div>
        <a id="clickIDCard" runat="server" style="display:none;" href="#popupIDCard"><asp:Label ID="lblpopupIDCard_btn" runat="server" Text ="สั่งซื้อ"></asp:Label></a>

        <div id="popupIDCardAlert" class="popup" >
            <div class="popup-frame" >
                <h3 class="true-m" id="idCardAlertReason"><asp:Label ID="lblpopupIDCardAlert_Header" runat="server" Text ="xxxxxxxxxxxxxxxx"></asp:Label></h3>
                <div class="icon"><img src="images/Popup/icon-idcard-alert-gif.gif"/></div>
                <h4 class="true-b"><asp:Label ID="lblpopupIDCardAlert_Detail" runat="server" Text ="กรุณาเสียบบัตรใหม่"></asp:Label>กรุณาเสียบบัตรใหม่</h4>
                <div class="bottom"><a class="btu true-l" onclick="$.fancybox.close();" href="javascript:;"><asp:Label ID="lblpopupIDCardAlert_btn" runat="server" Text ="ตกลง"></asp:Label></a></div>
            </div>
        </div>
        <a id="clickIDCardAlert" runat="server" style="display:none;"  href="#popupIDCardAlert">Click</a>

        <div id="popupIDCardCross" class="popup" >
            <div class="popup-frame" >
                <h3 class="true-m" id="idCardCrossReason"><asp:Label ID="lblpopupIDCardCross_Header" runat="server" Text ="xxxxxxxxxxxxxxxx"></asp:Label></h3>
                <div class="icon"><img src="images/Popup/icon-idcard-cross-gif.gif"/></div>
                <h4 class="true-b"><asp:Label ID="lblpopupIDCardCross_Detail" runat="server" Text ="กรุณาเปลี่ยนบัตรใหม่"></asp:Label></h4>
                <div class="bottom"><a class="btu true-l" onclick="$.fancybox.close();" href="javascript:;"><asp:Label ID="lblpopupIDCardCross_btn" runat="server" Text ="ตกลง"></asp:Label></a></div>
            </div>
        </div>
        <a id="clickIDCardCross" runat="server" style="display:none;"  href="#popupIDCardCross">Click</a>

        <div id="popupPassportAlert" class="popup" >
            <div class="popup-frame" >
                <h3 class="true-m" id="passportAlertReason"><asp:Label ID="lblpopupPassportAlert_Header" runat="server" Text ="xxxxxxxxxxxxxxxx"></asp:Label></h3>
                <div class="icon"><img src="images/Popup/icon-idcard-alert-gif.gif"/></div>
                <h4 class="true-b"><asp:Label ID="lblpopupPassportAlert_Detail" runat="server" Text ="Please try again"></asp:Label></h4>
                <div class="bottom"><a class="btu true-l" onclick="$.fancybox.close();" href="javascript:;"><asp:Label ID="lblpopupPassportAlert_btn" runat="server" Text ="OK"></asp:Label></a></div>
            </div>
        </div>
        <a id="clickPassportAlert" runat="server" style="display:none;"  href="#popupPassportAlert">Click</a>

        <div id="popupPassportCross" class="popup" >
            <div class="popup-frame" >
                <h3 class="true-m" id="passportCrossReason"><asp:Label ID="lblpopupPassportCross_Header" runat="server" Text ="xxxxxxxxxxxxxxxx"></asp:Label></h3>
                <div class="icon"><img src="images/Popup/icon-idcard-cross-gif.gif"/></div>
                <h4 class="true-b"><asp:Label ID="lblpopupPassportCross_Detail" runat="server" Text ="Please try another document"></asp:Label></h4>
                <div class="bottom"><a class="btu true-l" onclick="$.fancybox.close();" href="javascript:;"><asp:Label ID="lblpopupPassportCross_btn" runat="server" Text ="OK"></asp:Label></a></div>
            </div>
        </div>
        <a id="clickPassportCross" runat="server" style="display:none;"  href="#popupPassportCross">Click</a>


        <div id="popupSlip" class="popup">
            <div class="popup-frame">
                <h3 class="true-m half"><asp:Label ID="lblpopupSlip_Header" runat="server" Text ="หากท่านต้องการ<br />ใบเสร็จรับเงินฉบับจริง<br />หรือใบกำกับภาษี"></asp:Label></h3>
<%--                <h3 class="true-m half">หากท่านต้องการ<br />ใบเสร็จรับเงินฉบับจริง<br />หรือใบกำกับภาษี</h3>--%>

                <div class="icon half">
                    <img src="images/popup/icon-Tax-gif.gif" />
                </div>
                <h4 class="true-b"><asp:Label ID="lblpopupSlip_Detail" runat="server" Text ="กรุณาติดต่อพนักงานก่อนทำรายการ"></asp:Label></h4>
                <div class="bottom">  
                   <div class="bottom"><a id="btnNextSlip" runat="server" class="btu true-l"><asp:Label ID="lblpopupSlip_btn" runat="server" Text ="ดำเนินการต่อ"></asp:Label></a></div> 
                </div>
            </div>
        </div>
        <a id="lnkSlip" runat="server" style="display:none;" href="#popupSlip">Slip</a>

        <div id="popupCam" class="popup" >
            <div class="popup-frame" style="width:0px; height:0px;" >
            
            </div>
        </div>
        <a id="clickCamTrigger" runat="server" style="display:none;"  href="#popupCam">Click</a>
        <iframe id="frmCamTrigger" style="visibility:hidden;"></iframe>
        <asp:TextBox ID="txtCamData" runat="server" TextMode="MultiLine" style="display:none;"></asp:TextBox>
        <asp:Button ID="btnPostCam" runat="server" style="display:none;"></asp:Button>
</form>
</body>
    <script type="text/javascript">
        $("#lnkSlip").fancybox();
        $("#clickIDCard").fancybox();
        $("#clickIDCardAlert").fancybox();
        $("#clickIDCardCross").fancybox();
        $("#clickPassportAlert").fancybox();
        $("#clickPassportCross").fancybox();
        $("#clickCamTrigger").fancybox();

        (function ($) {
        $(window).on("load", function () {
                $.mCustomScrollbar.defaults.scrollButtons.enable = true; //enable scrolling buttons by default
                $.mCustomScrollbar.defaults.axis = "yx"; //enable 2 axis scrollbars by default
                $("#content-d3").mCustomScrollbar({ theme: "dark-3" });
                $("#content-d4").mCustomScrollbar({ theme: "dark-3" });

            });
        })(jQuery);
    </script>

    <script type="text/javascript">

    function requestIDCard() {
        // Gen URL
            var url =  $('#txtLocalControllerURL').val() + '/ScanIDCard.aspx?timeout=30&callback=updateIDCard&callbackError=verifyError';              
            var script = document.createElement('script');
            script.src = url;
            var body = document.getElementsByTagName('body')[0];
            body.appendChild(script);
        }
   
    function requestPassport() {
            // Gen URL
            var url = $('#txtLocalControllerURL').val() + '/ScanPassport.aspx?timeout=30&Mode=Scan&callback=updatePassport&callbackError=verifyError';
            var script = document.createElement('script');
            script.src = url;
            var body = document.getElementsByTagName('body')[0];
            body.appendChild(script);
    }

    function verifyError(Message) {
        $(".fancybox-close").click();
        $("#btnKeepInfo").click();        
    }

    function updateIDCard(Citizenid,
                            Th_Prefix,
                            Th_Firstname,
                            Th_Middlename,
                            Th_Lastname,
                            En_Prefix,
                            En_Firstname,
                            En_Middlename,
                            En_Lastname,
                            Sex,
                            Birthday,
                            Address,
                            addrHouseNo,
                            addrVillageNo,
                            addrLane,
                            addrRoad,
                            addrTambol,
                            addrAmphur,
                            addrProvince,
                            Issue,
                            Issuer,
                            Expire,
                            Photo){

                            $('#id_Citizenid').val(Citizenid);
                            $('#id_Th_Prefix').val(Th_Prefix);
                            $('#id_Th_Firstname').val(Th_Firstname);
                            $('#id_Th_Middlename').val(Th_Middlename);
                            $('#id_Th_Lastname').val(Th_Lastname);
                            $('#id_En_Prefix').val(En_Prefix);
                            $('#id_En_Firstname').val(En_Firstname);
                            $('#id_En_Middlename').val(En_Middlename);
                            $('#id_En_Lastname').val(En_Lastname);
                            $('#id_Sex').val(Sex);
                            $('#id_Birthday').val(Birthday);
                            $('#id_Address').val(Address);
                            $('#id_addrHouseNo').val(addrHouseNo);
                            $('#id_addrVillageNo').val(addrVillageNo);
                            $('#id_addrLane').val(addrLane);
                            $('#id_addrRoad').val(addrRoad);
                            $('#id_addrTambol').val(addrTambol);
                            $('#id_addrAmphur').val(addrAmphur);
                            $('#id_addrProvince').val(addrProvince);
                            $('#id_Issue').val(Issue);
                            $('#id_Issuer').val(Issuer);
                            $('#id_Expire').val(Expire);
                            $('#id_Photo').val(Photo);

                            cusAlias = En_Firstname;
            
             $(".fancybox-close").click();
             $("#btnKeepInfo").click();
        }

        function updatePassport(FirstName,
                            MiddleName,
                            LastName,
                            DocType,
                            Nationality,
                            PassportNo,
                            DateOfBirth,
                            Sex,
                            Expire,
                            PersonalID,
                            IssueCountry,
                            MRZ,
                            Photo){
                            $('#pass_FirstName').val(FirstName);
                            $('#pass_MiddleName').val(MiddleName);
                            $('#pass_LastName').val(LastName);
                            $('#pass_DocType').val(DocType);
                            $('#pass_Nationality').val(Nationality);
                            $('#pass_PassportNo').val(PassportNo);
                            $('#pass_DateOfBirth').val(DateOfBirth);
                            $('#pass_Sex').val(Sex);
                            $('#pass_Expire').val(Expire);
                            $('#pass_PersonalID').val(PersonalID);
                            $('#pass_IssueCountry').val(IssueCountry);
                            $('#pass_MRZ').val(MRZ.replaceAll('<','&lt'));                            
                            $('#pass_Photo').val(Photo);

                            cusAlias = FirstName;
           
             $(".fancybox-close").click();
             $("#btnKeepInfo").click();
        }

        var cusAlias = "";
    function triggerCamera(){        
        // Show Lightbox
        
        var url = 'CamCapture.aspx?CusName=' + cusAlias; // Send AliasName To Camera Detector
        setTimeout(function () { $('#frmCamTrigger').attr('src', url); }, 700);
        setTimeout(function () { $('#clickCamTrigger').click(); }, 900);

        $('#popupCam').find('.popup-frame').hide();
        $('#popupCam').find('.fancybox-close').hide();
    }

    function PostCustomerFace(blob) {
        $('#frmCamTrigger').attr("src", "");
        $(".fancybox-close").click();
        
        $('#txtCamData').val(blob);
        $('#btnPostCam').click();
    }

    function requestSlip() {
        $('#lnkSlip').click();
    }

    function requestVerifySlip() {
        $('#btnVerifySlip').click();
    }

    </script>
</html>
