$(document).ready(function () {

    $('.fund-dropdown').dropdown({
        onChange: function (e) {
            configureFundMessage(e, null);
        }
    });

    configureFundMessage($('#FundCode').val(), $('#FundMessageId').val());

    $('.fund-message-dropdown').dropdown('set selected', $('#FundMessageId').val());
});

function configureFundMessage(fundCode, fundMessageId) {
    var optionsExistForThisFundCode = false;

    if (fundMessageId == null) {
        $('.fund-message-dropdown').dropdown('clear');
    }

    $('.fund-message-dropdown > .menu > .item').each(function () {
        if ($(this).data('fund-code') == fundCode || $(this).data('fund-code') == null) {
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