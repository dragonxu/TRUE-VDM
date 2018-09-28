<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Front_UI/MasterFront.Master" CodeBehind="Home.aspx.vb" Inherits="VDM.Home" %>

<%@ Register Src="~/Front_UI/UC_Machine_Console.ascx" TagPrefix="uc1" TagName="UC_Machine_Console" %>


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
            <asp:LinkButton  ID="lnkSim" runat ="server" >
                <div class="image-cropper">
                    <span>
                        <h3 class="true-l">Sim&<br />
                            Package </h3>
                        <p class="true-l">เลือกซื้อซิมและแพ็กเกจ</p>
                    </span>
                    <img src="images/pic-sim.jpg" />
                </div>
            </asp:LinkButton>
        </div>







    </div>
     <footer>
                <nav>
                    <div class="main">
                        <span class="col-md-6">
                           <%-- <asp:ImageButton ID="lnkHome" runat="server" ImageUrl="images/btu-home.png" />--%>
                        </span>
                        <span class="col-md-6">
                            <asp:ImageButton ID="lnkBack" runat="server" ImageUrl="images/btu-prev.png" />
                        </span>
                    </div>
                </nav>
            </footer>
         <footer style="bottom: 0px ; width :100%;padding :20px 0;">                 
                        <uc1:UC_Machine_Console runat="server" id="UC_Machine_Console1" />                 
            </footer> 
</asp:Content>
