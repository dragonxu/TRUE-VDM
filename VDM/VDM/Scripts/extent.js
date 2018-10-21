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

String.prototype.replaceAll = function (search, replacement) {
    var target = this;
    return target.split(search).join(replacement);
};
