<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="SIM_List.aspx.vb" Inherits="VDM.SIM_List" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1,user-scalable=no">
    <title>TRUE</title>
    <link href="css/true.css" rel="stylesheet" type="text/css" />
    <link href="css/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="css/font-awesome.min.css" rel="stylesheet">
    <link href="css/bootstrap-select.css" rel="stylesheet">
    <script type="text/javascript" src="js/jquery-1.12.2.min.js"></script>
    <script type="text/javascript" src="js/bootstrap.js"></script>
    <link href="css/slick.css" rel="stylesheet" type="text/css" />
    <link href="css/slick-theme.css" rel="stylesheet" type="text/css" />
</head>
<body class="bg">
     <form id="form1" runat="server">
          <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="udpList" runat="server">
            <ContentTemplate>
    <div class="warp">
        <header>
            <img src="images/bg-top.png" /></header>
        <main>
            <div class="sim">
                <div class="main3">
                    <div class="pic-sim">
                        <h3 class="true-l">SIM&<br />
                            Package</h3>
                        <img src="images/pic-top_sim.png" /></div>
                    <ul  class="col-md-12" style ="text-align :center ;">
                        <h3 class="true-l">เติมเงิน</h3>

                        <asp:Repeater ID="rptList" runat="server" >
                            <ItemTemplate>
                                <li class="col-md-4">
                                    <a id="btnSIM" runat="server">
                                        <asp:Image ID="img" runat="server"></asp:Image>
                                    </a>
                                <asp:Button ID="btnSelect" runat="server" Style="display: none;" CommandName="Select" />

                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
 




                    </ul>
                </div>
            </div>


             <footer>
                <nav>
                    <div class="main">
                        <span class="col-md-6">
                            <asp:ImageButton ID="lnkHome" runat="server" ImageUrl="images/btu-home.png" />
                        </span>
                        <span class="col-md-6">
                            <%--<asp:ImageButton ID="lnkBack" runat="server" ImageUrl="images/btu-prev.png" />--%>
                        </span>
                    </div>
                </nav>
            </footer>

        </main>
 
    </div>

                </ContentTemplate>
            </asp:UpdatePanel>
         </form>
    <script type="text/javascript" src="js/slick.js"></script>
    <script>
        $(document).ready(function () {

            $('.card-slider').slick({
                infinite: true,
                slidesToShow: 3,
                slidesToScroll: 1,

            });

        });

    </script>
</body>
</html>

