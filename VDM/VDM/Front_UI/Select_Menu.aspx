<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Front_UI/MasterFront.Master" CodeBehind="Select_Menu.aspx.vb" Inherits="VDM.Select_Menu" %>

<%@ Register Src="~/Front_UI/UC_CommonUI.ascx" TagPrefix="uc1" TagName="UC_CommonUI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="home">
        <div class="frame">
            <%--<a href="devices.html">--%>
            <asp:LinkButton  ID="lnkDevice" runat ="server" >
                <div class="image-cropper">
                    <span>
                        <h3 class="true-l"><asp:Label ID="lblUI_Device" runat ="server" Text ="Devices"></asp:Label>   </h3>
                        <p class="true-l"><asp:Label ID="lblUI_Device_Desc" runat ="server" Text ="โทรศัพท์มือถือ<br />อุปกรณ์สมาร์ทดีไวซ์"></asp:Label></p>
                    </span>
                    <img src="images/pic-devices.jpg" />
                </div>
            </asp:LinkButton>
            <%--</a>--%>
        </div>
        <div class="frame" id="Accessories" runat ="server"  >
            <asp:LinkButton  ID="lnkAccessories" runat ="server" >
                <div class="image-cropper">
                    <span>
                        <h3 class="true-l"><asp:Label ID="lblUI_Accessories" runat ="server"  Text ="Accessories" Style ="color:#FFFFFF" ></asp:Label></h3>
                        <p class="true-l"><asp:Label ID="lblUI_Accessories_Desc" runat ="server" Text ="อุปกรณ์เสริมต่าง ๆ"  Style ="color:#FFFFFF"></asp:Label></p>
                      </span>
                    <img src="images/pic-accessories.jpg" />
                </div>
            </asp:LinkButton>
        </div>
        <div class="frame">
            <asp:LinkButton  ID="lnkSim" runat ="server" >
                <div class="image-cropper">
                    <span>
                        <h3 class="true-l"><asp:Label ID="lblUI_SIM" runat ="server" Text ="Sim&<br />Package"></asp:Label></h3>
                        <p class="true-l"><asp:Label ID="lblUI_SIM_Desc" runat ="server" Text ="เลือกซื้อซิมและแพ็กเกจ"></asp:Label></p>
                    </span>
                    <img src="images/pic-sim.jpg" />
                </div>
            </asp:LinkButton>
        </div>

    </div>
     <footer>
                <nav>
                    <div class="main">
                        <span class="col-md-12">
                            <asp:ImageButton ID="lnkBack" runat="server" ImageUrl="images/btu-prev.png" />
                        </span>
                    </div>
                </nav>
            </footer>
    <uc1:UC_CommonUI runat="server" ID="UC_CommonUI" />
</asp:Content>
