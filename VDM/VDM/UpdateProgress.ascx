<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UpdateProgress.ascx.vb" Inherits="VDM.UpdateProgress" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div id="divImage" runat="server" style="display:none; position:fixed; top:0px; left:0px; width:100%; height:100%; z-index:10000;">                    
                    <div style="width:100%; height:100%; position:absolute; background-color:Gray; filter:alpha(opacity=50); opacity: 0.50;" >
                    
                    </div>
                    <table style="width:100%; height:50%; position:absolute;">
                        <tr><td height="100%">&nbsp;</td></tr>
                        <tr>
                            <td align="center" style="color:White; font-weight:bold; font-size:18px;">
                                    <center id="ct_text" runat="server">... Processing ...</center>
                            </td>
                        </tr>
                        <tr><td height="50%">&nbsp;</td></tr>
                    </table>                     
                </div>                
            </ContentTemplate>
</asp:UpdatePanel>
    <script type="text/javascript" language="javascript">

            // Called when async postback begins
             function prm_InitializeRequest(sender, args) {
                 // get the divImage and set it to visible
                 var panelProg = $get('<%= divImage.ClientID %>');
                 panelProg.style.display = '';                
                 // Disable button that caused a postback
                 $get(args._postBackElement.id).disabled = true;
             }
 
             // Called when async postback ends
             function prm_EndRequest(sender, args) {
                 // get the divImage and hide it again
                 var panelProg = $get('<%= divImage.ClientID %>');
                 // Enable button that caused a postback
                 try
                 {$get(sender._postBackSettings.sourceElement.id).disabled = false;}
                 catch(err){}    
                 panelProg.style.display = 'none';             
             }
    // Add Event Handler 
    try {
    var prm = Sys.WebForms.PageRequestManager.getInstance();
        // Add initializeRequest and endRequest
        prm.add_initializeRequest(prm_InitializeRequest);
        prm.add_endRequest(prm_EndRequest);
   }catch(err){}
  
</script>