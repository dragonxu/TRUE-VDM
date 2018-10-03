<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UC_CommonUI.ascx.vb" Inherits="VDM.UC_CommonUI" %>

<input type="button" style="position:absolute; bottom:0; left:0; width:100px; height:100px; background-color:red; z-index:10;" onclick="inputPasscodeLeft();" />
<input type="button" style="position:absolute; bottom:0; right:0; width:100px; height:100px; background-color:red; z-index:10;" onclick="inputPasscodeRight();" />
<script type="text/javascript">

    var userPasscode = "";
    var passcodeTime = 0;
    var truePasscode = "11221";

    function resetPasscode() {
      
    }

    function inputPasscodeLeft() {
        userPasscode += '1';
    }

    function inputPasscodeRight() {
        userPasscode += '2';
    }

    function checkPasscode() {
        if (userPasscode == truePasscode) {
            location.href = '../Machine_Console/Login.aspx?KO_ID=<% Response.Write(KO_ID) %>';
        }
    }

    var passcodeTimer = function () {
        userPasscode = '';
    }
    setInterval(passcodeTime, 5000);


</script>