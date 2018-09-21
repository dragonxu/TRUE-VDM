/**
 * Noty notifications demo page
 */
(function ($) {
    'use strict';
    $('.showToastr').on('click', function () {
        var msg = $('#toastrMessage').val(),
          type = $('#toastrType').val().toLowerCase(),
          position = $('#toastrPosition').val(),
          timeout = $('#toastrTimeout').val();
        noty({
            theme: 'app-noty',
            text: msg,
            type: type,
            timeout: timeout,
            layout: position,
            closeWith: ['button', 'click'],
            animation: {
                open: 'in',
                close: 'out'
            },
        });
    });
})(jQuery);