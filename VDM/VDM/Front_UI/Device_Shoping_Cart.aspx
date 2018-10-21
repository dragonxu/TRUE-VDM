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
                            <p class="t-cart t-red true-b">SHOPING CART</p>
                            <p class="t-payment true-b">PAYMENT</p>
                            <p class="t-complete true-b">COMPLETE ORDER</p>
                            <img src="images/pic-step1.png" />
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

                        <div class="term" style="height:460px; margin:unset;">
                            <h2 class="true-m">Term & Condition</h2>
                            <div class="frame" style="height: 380px;">
                                <span id="content-d3" class="light" style="height:340px; overflow:hidden;">
                                    <p class="true-l">
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

                <asp:UpdatePanel ID="UDPTerm" runat="server">
                <ContentTemplate>
                
                        <label class="check true-l" style="height:100px;">
                            ข้าพเจ้าได้อ่านข้อกำหนดและเงื่อนไขทุกข้อแล้ว
                            <asp:CheckBox ID="chkActive" runat="server" Checked="false" AutoPostBack="true" />
                            <span class="checkmark"></span>
                        </label>

                        <div class="col-md-12" style="text-align: center;">
                                <asp:Button ID="btnConfirm" runat="server" CssClass="order true-m default " Text="ชำระเงิน" />
                                <asp:Button ID="btnVerify" runat="server" CssClass="order true-m default " Text="สั่งซื้อ" />
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
        
            <div style="position:fixed; top:40px; left:-500px;"> <!--ID Card-->
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
                    <!----------------------------->
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
                <h3 class="true-m">ขั้นตอนการยืนยันตัวบุคคล</h3>
                <div class="icon"><img src="images/Popup/icon-idcard.png"/></div>
                <h4 class="true-b" style="padding-bottom:30px;">กรุณาสอดบัตรประชาชนของท่าน</h4>
          </div>
        </div>
        <a id="clickIDCard" runat="server" style="display:none;" href="#popupIDCard">สั่งซื้อ</a>

        <div id="popupIDCardAlert" class="popup" >
            <div class="popup-frame" >
                <h3 class="true-m" id="idCardAlertReason">xxxxxxxxxxxxxxxx</h3>
                <div class="icon"><img src="images/Popup/icon-idcard-alert.png"/></div>
                <h4 class="true-b">กรุณาเสียบบัตรใหม่</h4>
                <div class="bottom"><a class="btu true-l" onclick="$.fancybox.close();" href="javascript:;">ตกลง</a></div>
            </div>
        </div>
        <a id="clickIDCardAlert" runat="server" style="display:none;"  href="#popupIDCardAlert">Click</a>

        <div id="popupIDCardCross" class="popup" >
            <div class="popup-frame" >
                <h3 class="true-m" id="idCardCrossReason">xxxxxxxxxxxxxxxxx</h3>
                <div class="icon"><img src="images/Popup/icon-idcard-cross.png"/></div>
                <h4 class="true-b">กรุณาเปลี่ยนบัตรใหม่</h4>
                <div class="bottom"><a class="btu true-l" onclick="$.fancybox.close();" href="javascript:;">ตกลง</a></div>
            </div>
        </div>
        <a id="clickIDCardCross" runat="server" style="display:none;"  href="#popupIDCardCross">Click</a>

        <div id="popupCam" class="popup" >
            <iframe id="frmCamTrigger" runat="server" style="visibility:hidden;"></iframe>
        </div>
        <a id="clickCamTrigger" runat="server" style="display:none;"  href="#popupCam">Click</a>

</form>
</body>
    <script type="text/javascript">
        $("#clickIDCard").fancybox();
        $("#clickIDCardAlert").fancybox();
        $("#clickIDCardCross").fancybox();
        $("#clickCamTrigger").fancybox();

        (function ($) {
        $(window).on("load", function () {
                $.mCustomScrollbar.defaults.scrollButtons.enable = true; //enable scrolling buttons by default
                $.mCustomScrollbar.defaults.axis = "yx"; //enable 2 axis scrollbars by default
                $("#content-d3").mCustomScrollbar({ theme: "dark-3" });
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
           
             $(".fancybox-close").click();
             $("#btnKeepInfo").click();
        }

    function triggerCamera(){        
        // Show Lightbox
        $('#clickCamTrigger').click();
        var url = $('#txtLocalControllerURL').val() + '/CamCapture.aspx';
        $('#frmCamTrigger').attr('src',url);
        alert($('#frmCamTrigger').attr('src'));
    }

    </script>
    
</html>
