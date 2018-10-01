<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UC_Validate_Serial.ascx.vb" Inherits="VDM.UC_Validate_Serial" %>

<table>
      <tr>
        <td colspan ="3"><h3 style="text-align :center ;"> Validate_Serial</h3></td>
                         
   </tr>
    <tr>
        <td><h4 style="text-align :center ;">Request</h4></td>
        <td>&nbsp;</td>
        <td><h4 style="text-align :center ;">Response</h4></td>
   </tr>

    <tr style ="vertical-align :top;">
        <td style="padding:10px;"> 
                <table>                   

                    <tr>
                        <td>shopCode</td>
                        <td><asp:TextBox ID="shopCode" runat="server" ></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>Serial</td>
                        <td><asp:TextBox ID="Serial" runat="server" ></asp:TextBox></td>
                    </tr> 
                    <tr>
                        <td> </td>
                        <td><asp:Button ID="btn_Request" runat="server" Text="OK" /></td>
                    </tr> 
                    
                    <tr>
                        <td> </td>
                        <td><h4 style ="color :blue ;" >
                            <br />==Test Serial==<br>
                            1--3000024133--80000010--677555310003192--<br>
                            2--3000024133--80000010--677555310003193--<br>
                            3--3000024133--80000010--677555310003194--<br>
                            4--3000024133--80000010--677555310003195--<br>
                            5--3000024133--80000010--677555310003196--<br>
                            6--3000024133--80000010--677555310003197--<br>
                            7--3000036094--80000010--111100220100100122--<br>
                            8--3000036094--80000010--111100220100100123--<br>
                            9--3000036094--80000010--111100220100228--<br>
                            10--3000036094--80000010--111100220100229--<br>

                            </h4>

                        </td>
                    </tr>
                                       
                </table>
        </td>
        <td >&nbsp;</td>
        <td style="padding:10px;"> 
           <span>                

{<br>
"ReturnValues":["<asp:TextBox ID="CODE" runat="server"></asp:TextBox>",<asp:TextBox ID="IS_SIM" runat="server"></asp:TextBox>],<br/>
"IsError":<asp:TextBox ID="IsError" runat="server"></asp:TextBox>,"ErrorMessage":<asp:TextBox ID="ErrorMessage" runat="server"></asp:TextBox><br/>
,"IsNotTransaction":<asp:TextBox ID="IsNotTransaction" runat="server"></asp:TextBox><br/>
}
</span></td>
    </tr>
        <tr>
        <td colspan ="3"><asp:Label ID="lblErr_Msg" runat="server" Style =" color :red;"  ></asp:Label></td>
    </tr>
</table>