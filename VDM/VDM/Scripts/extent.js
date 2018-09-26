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