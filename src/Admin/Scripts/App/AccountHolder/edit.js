$(document).ready(function () {

    $('.fund-dropdown').dropdown({
        onChange: function (e) {
            configureStopMessage(e);
        }
    });

    $('.stop-message-dropdown').dropdown();

    configureStopMessage($('.fund-dropdown').val());
});

function configureStopMessage(fundCode) {
    var optionsExistForThisFundCode = false;

    $('.stop-message-dropdown').dropdown('clear');

    $('.stop-message-dropdown > .menu > .item').each(function () {
        if ($(this).data('fund-code') == fundCode) {
            $(this).show();
            optionsExistForThisFundCode = true;
        }
        else {
            $(this).hide();
        }
    });

    if (optionsExistForThisFundCode) {
        $('.stop-message-reference').disableSelection();
    }
    else {
        $('.stop-message-reference').enableSelection();
    }
}