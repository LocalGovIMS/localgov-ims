$(document).ready(function () {

    $('.fund-dropdown').dropdown({
        onChange: function (e) {
            configureFundMessage(e);
        }
    });

    $('.fund-message-dropdown').dropdown();

    configureFundMessage($('.fund-dropdown').val());
});

function configureFundMessage(fundCode) {
    var optionsExistForThisFundCode = false;

    $('.fund-message-dropdown').dropdown('clear');

    $('.fund-message-dropdown > .menu > .item').each(function () {
        if ($(this).data('fund-code') == fundCode) {
            $(this).show();
            optionsExistForThisFundCode = true;
        }
        else {
            $(this).hide();
        }
    });

    if (optionsExistForThisFundCode) {
        $('.fund-message-reference').disableSelection();
    }
    else {
        $('.fund-message-reference').enableSelection();
    }
}