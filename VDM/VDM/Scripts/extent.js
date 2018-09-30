var txtBarcode;

var focusDelegate = function () {
    $('#' + txtBarcode + '').focus();
}

function startFocusBarcode() {
    setInterval(focusDelegate, 700);
}

function stopFocusBarcode() {
    clearInterval(focusDelegate);
}

function moveArmPosition(POS_ID) {

    var xhr;
    var dataList;
    xhr = new XMLHttpRequest();
    xhr.open('GET', 'http://localhost/Arm.aspx?POS_ID=' + POS_ID + '&callback=arm_callback', true);
    xhr.send();
    xhr.onreadystatechange = function () {
        if (xhr.readyState == 4) {
            console.log(dataList);
        }
    };
}

arm_callback = function (data) {
    alert(data.satus);
}