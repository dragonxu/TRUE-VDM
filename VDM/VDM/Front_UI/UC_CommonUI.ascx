﻿<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UC_CommonUI.ascx.vb" Inherits="VDM.UC_CommonUI" %>

<input type="button" style="position:fixed; border:none; bottom:0; left:0; width:100px; height:100px; background-color:transparent; z-index:10;" onclick="inputPasscodeLeft();" />
<input type="button" style="position:fixed; border:none; bottom:0; right:0; width:100px; height:100px; background-color:transparent; z-index:10;" onclick="inputPasscodeRight();" />
<script type="text/javascript">

    var userPasscode = "";
    var passcodeTime = 0;
    var truePasscode = "11221";

    function inputPasscodeLeft() {
        userPasscode += '1';
        checkPasscode();
        clearInterval(passcodeInterval);
        passcodeInterval = setInterval(passcodeTimer, 3000);
    }

    function inputPasscodeRight() {
        userPasscode += '2';
        checkPasscode();
        clearInterval(passcodeInterval);
        passcodeInterval = setInterval(passcodeTimer, 3000);
    }

    function checkPasscode() {
        if (userPasscode == truePasscode) {
            location.href = '../Machine_Console/Login.aspx?KO_ID=<% Response.Write(KO_ID) %>';
        }
        else {
            if (userPasscode.length() == truePasscode.length())
                userPasscode = "";
        }
    }

    var passcodeTimer = function () {
        userPasscode = '';
    }
    var passcodeInterval = setInterval(passcodeTimer,3000);


</script>



