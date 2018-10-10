<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UC_CommonUI.ascx.vb" Inherits="VDM.UC_CommonUI" %>

<input type="button" style="position:fixed; border:none; bottom:0; left:0; width:10%; height:10%; background-color:transparent; z-index:10;" onclick="inputPasscodeLeft();" />
<input type="button" style="position:fixed; border:none; bottom:0; right:0; width:10%; height:10%; background-color:transparent; z-index:10;" onclick="inputPasscodeRight();" />
<script type="text/javascript">

    var userPasscode = "";
    var passcodeTime = 0;
    var backEndPasscode = "11221";
    var closePasscode = "11112222";

    function inputPasscodeLeft() {
        userPasscode += '1';
        checkPasscode();
    }

    function inputPasscodeRight() {
        userPasscode += '2';
        checkPasscode();
    }

    function checkPasscode() {
        if (userPasscode.indexOf(backEndPasscode)>-1) {
            location.href = '../Machine_Console/Login.aspx?KO_ID=<% Response.Write(KO_ID) %>';
        }
        else if (userPasscode.indexOf(closePasscode) > -1) {
            location.href = "about:blank"; /////// Raise cefSharp to close window
        }
        else if (userPasscode.length() > 40) {
            userPasscode = "";            
        }
    }


</script>



