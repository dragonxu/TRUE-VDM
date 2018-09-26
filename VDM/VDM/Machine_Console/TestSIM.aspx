<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Machine_Console/MasterStaffConsole.Master" CodeBehind="TestSIM.aspx.vb" Inherits="VDM.TestSIM" %>

<%@ Register Src="~/Machine_Console/UC_SIM_Dispenser.ascx" TagPrefix="uc1" TagName="UC_SIM_Dispenser" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<div class="col-md-6" > <!--- Test With Page---->
   
    <uc1:UC_SIM_Dispenser runat="server" ID="Dispenser" />

</div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContainer" runat="server">
</asp:Content>
