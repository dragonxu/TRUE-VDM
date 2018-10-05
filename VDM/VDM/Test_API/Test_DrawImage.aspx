<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Test_DrawImage.aspx.vb" Inherits="VDM.Test_DrawImage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <h3>Image To Base64String</h3>
        <div class ="col-sm-5" style="width: 40%; float: left;">
            <asp:FileUpload ID="FileUpload1" runat="server" />
        <asp:Image ID="Image1" Visible="false" runat="server"  Width="10%" />
             <asp:Label ID="lblImage1" runat ="server" ></asp:Label>
             <asp:TextBox ID="txtImage1" runat ="server" TextMode ="MultiLine" ></asp:TextBox>
        </div>
        <div class ="col-sm-5" style="width: 40%; float: left;">
            <asp:FileUpload ID="FileUpload2" runat="server" />
        <asp:Image ID="Image2" Visible="false" runat="server" Width="10%" />
             <%--<asp:Label ID="lblImage2" runat ="server" ></asp:Label>--%>
            <asp:TextBox ID="txtImage2" runat ="server" TextMode ="MultiLine" ></asp:TextBox>
        </div>
        <div class ="col-sm-2" style="width: 20%; float: left;">
            <asp:Button ID="btnUpload" runat="server" Text="Upload" />
        </div>

        
        <div class ="row">
        <h3>Merge</h3>

        <asp:Image ID="Image_Merge" Visible="false" runat="server" Width="20%" />
        <asp:TextBox ID="txtImage_Merge" runat ="server" TextMode ="MultiLine" ></asp:TextBox>

        </div>

        <br />
        <asp:Label ID="lblMerge_base64String" runat ="server" ></asp:Label>
        <br />
        <asp:Panel ID="panel1" runat="server"></asp:Panel>
    </form>
</body>
</html>
