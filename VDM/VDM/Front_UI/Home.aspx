<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Front_UI/MasterFront.Master" CodeBehind="Home.aspx.vb" Inherits="VDM.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="home">
        <div class="frame">
            <%--<a href="devices.html">--%>
            <asp:LinkButton  ID="lnkDevice" runat ="server" >
                <div class="image-cropper">
                    <span>
                        <h3 class="true-l">Devices</h3>
                        <p class="true-l">โทรศัพท์มือถือ<br />
                            อุปกรณ์สมาร์ทดีไวซ์</p>
                    </span>
                    <img src="images/pic-devices.jpg" />
                </div>
            </asp:LinkButton>
            <%--</a>--%>
        </div>
        <div class="frame">
            <a href="sim.html">
                <div class="image-cropper">
                    <span>
                        <h3 class="true-l">Sim&<br />
                            Package </h3>
                        <p class="true-l">เลือกซื้อซิมและแพ็กเกจ</p>
                    </span>
                    <img src="images/pic-sim.jpg" />
                </div>
            </a>
        </div>







    </div>
     <footer>
                <nav>
                    <div class="main">
                        <span class="col-md-6">
                            <asp:ImageButton ID="lnkHome" runat="server" ImageUrl="images/btu-home.png" />
                            

                        </span>
                        <span class="col-md-6"><a href="javascript:history.back();">
                            <img src="images/btu-prev.png" /></a></span>
                    </div>
                </nav>
            </footer>


</asp:Content>
