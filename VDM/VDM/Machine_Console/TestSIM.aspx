<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Machine_Console/MasterStaffConsole.Master" CodeBehind="TestSIM.aspx.vb" Inherits="VDM.TestSIM" %>

<%@ Register Src="~/Machine_Console/UC_SIM_Stock.ascx" TagPrefix="uc1" TagName="UC_SIM_Stock" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<asp:UpdatePanel ID="UDP" runat="server">
    <ContentTemplate>
         <uc1:UC_SIM_Stock runat="server" ID="SIMStock" />
    
    <asp:Button ID="btnSave" runat="server" Text="Save" />
    <asp:Button ID="btnReset" runat="server" Text="Reset" />
    </ContentTemplate>

    
</asp:UpdatePanel>
   
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContainer" runat="server">
</asp:Content>
