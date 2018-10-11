var txtBarcode;

var focusDelegate = function () {
    $('#' + txtBarcode + '').focus();
}

function startFocusBarcode() {
    setInterval(focusDelegate, 600);
}

function stopFocusBarcode() {
    clearInterval(focusDelegate);
}

