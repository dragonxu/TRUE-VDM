﻿<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Test_API_Interface.aspx.vb" Inherits="VDM.Test_API_Interface" %>

<%@ Register Src="~/Test_API/UC_Face_Recognition.ascx" TagPrefix="uc1" TagName="UC_Face_Recognition" %>
<%@ Register Src="~/Test_API/UC_Prepaid_Validate_Register.ascx" TagPrefix="uc1" TagName="UC_Prepaid_Validate_Register" %>
<%@ Register Src="~/Test_API/UC_Generate_Order_Id.ascx" TagPrefix="uc1" TagName="UC_Generate_Order_Id" %>
<%@ Register Src="~/Test_API/UC_Delete_File.ascx" TagPrefix="uc1" TagName="UC_Delete_File" %>
<%@ Register Src="~/Test_API/UC_Save_File.ascx" TagPrefix="uc1" TagName="UC_Save_File" %>
<%@ Register Src="~/Test_API/UC_Service_Flow_Create.ascx" TagPrefix="uc1" TagName="UC_Service_Flow_Create" %>
<%@ Register Src="~/Test_API/UC_Service_Flow_Finish.ascx" TagPrefix="uc1" TagName="UC_Service_Flow_Finish" %>
<%@ Register Src="~/Test_API/UC_Activity_Start.ascx" TagPrefix="uc1" TagName="UC_Activity_Start" %>
<%@ Register Src="~/Test_API/UC_Activity_End.ascx" TagPrefix="uc1" TagName="UC_Activity_End" %>
<%@ Register Src="~/Test_API/UC_Get_Product_Info.ascx" TagPrefix="uc1" TagName="UC_Get_Product_Info" %>
<%@ Register Src="~/Test_API/UC_Prepaid_Register.ascx" TagPrefix="uc1" TagName="UC_Prepaid_Register" %>
<%@ Register Src="~/Test_API/UC_Validate_Serial.ascx" TagPrefix="uc1" TagName="UC_Validate_Serial" %>
<%@ Register Src="~/Test_API/UC_Auto_Prepaid_Register.ascx" TagPrefix="uc1" TagName="UC_Auto_Prepaid_Register" %>
















<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
   
        <asp:Panel ID="pnl_btn" runat="server" Style =" margin-bottom :50px;">
            
                <asp:Button ID="btnFace_Recognition" runat="server" Text="Face_Recognition" />
                <asp:Button ID="btn_Prepaid_Validate_Register" runat="server" Text="Prepaid_Validate_Register" />
                <asp:Button ID="btn_Generate_Order_Id" runat="server" Text="Generate_Order_Id" />
                <asp:Button ID="btn_Delete_File" runat="server" Text="Delete_File" />
               
                <asp:Button ID="btn_Save_File" runat="server" Text="Save_File" />
                <asp:Button ID="btn_Service_Flow_Create" runat="server" Text="Service_Flow_Create" />
                <asp:Button ID="btn_Activity_Start" runat="server" Text="Activity_Start" />                    
                <asp:Button ID="btn_Activity_End" runat="server" Text="Activity_End" />
                <asp:Button ID="btn_Service_Flow_Finish" runat="server" Text="Service_Flow_Finish" />

                <asp:Button ID="btn_Get_Product_Info" runat="server" Text="Get_Product_Info" />
                <asp:Button ID="btn_Validate_Serial" runat="server" Text="Validate_Serial" />
                <asp:Button ID="btn_Prepaid_Register" runat="server" Text="Prepaid_Register" />
                 <asp:Button ID="btn_Auto_Prepaid_Register" runat="server" Text="Auto_Prepaid_Register" />
                
        </asp:Panel>
        
        
            <asp:Panel ID="pnl_face_recognition" runat="server">
                <uc1:UC_Face_Recognition runat="server" id="UC_Face_Recognition" />
            </asp:Panel>

            <asp:Panel ID="pnl_Prepaid_Validate_Register" runat="server">
                <uc1:UC_Prepaid_Validate_Register runat="server" ID="UC_Prepaid_Validate_Register" />
            </asp:Panel>

            <asp:Panel ID="pnl_Generate_Order_Id" runat="server">
                <uc1:UC_Generate_Order_Id runat="server" ID="UC_Generate_Order_Id" />
            </asp:Panel>

            <asp:Panel ID="pnl_Delete_File" runat="server">
                <uc1:UC_Delete_File runat="server" id="UC_Delete_File" />
            </asp:Panel>

            <asp:Panel ID="pnl_Save_File" runat="server">
                <uc1:UC_Save_File runat="server" id="UC_Save_File" />
            </asp:Panel>    

            <asp:Panel ID="pnl_Service_Flow_Create" runat="server">
                <uc1:UC_Service_Flow_Create runat="server" id="UC_Service_Flow_Create" />
            </asp:Panel>  
            
            <asp:Panel ID="pnl_Service_Flow_Finish" runat="server">
                <uc1:UC_Service_Flow_Finish runat="server" id="UC_Service_Flow_Finish" />
            </asp:Panel>
 
            <asp:Panel ID="pnl_Activity_Start" runat="server">
                <uc1:UC_Activity_Start runat="server" id="UC_Activity_Start" />
            </asp:Panel>

            <asp:Panel ID="pnl_Activity_End" runat="server">
                <uc1:UC_Activity_End runat="server" id="UC_Activity_End" />
            </asp:Panel>

            <asp:Panel ID="pnl_Get_Product_Info" runat="server">
                <uc1:UC_Get_Product_Info runat="server" id="UC_Get_Product_Info" />
            </asp:Panel>
             
            <asp:Panel ID="pnl_Prepaid_Register" runat="server"> 
                <uc1:UC_Prepaid_Register runat="server" id="UC_Prepaid_Register" />
            </asp:Panel>

            <asp:Panel ID="pnl_Validate_Serial" runat="server">
                <uc1:UC_Validate_Serial runat="server" id="UC_Validate_Serial" />
            </asp:Panel>

            <asp:Panel ID="pnl_Auto_Prepaid_Register" runat="server">
                <uc1:UC_Auto_Prepaid_Register runat="server" id="UC_Auto_Prepaid_Register" />
            </asp:Panel>
         </div>
    </form>
 

</body>
</html>
