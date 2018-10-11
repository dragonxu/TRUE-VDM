
function initKeyboard() {
    $(function () {
        $('.osk-trigger').onScreenKeyboard({
            rewireReturn: 'submit',
            rewireTab: true
        });
    });
}
initKeyboard();
