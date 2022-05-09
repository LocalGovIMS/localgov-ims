$('.funds-dropdown').dropdown({
    onChange: function (e) {

        var text = $('.account-reference-validator');
        text.empty();

        $('#AccountReference').val('');
        $('.account-reference-search').attr('href', paymentsAdmin.pages.payment.index.searchAction + "/" + e);

        showHideSearchButton(e);

        setTimeout(function () { showHideVatOptions(); }, 50);

    }

});

$('.mop-dropdown').dropdown({
    onChange: function (e) {
        setTimeout(function () {
            var minVal = $(".mop-dropdown .selected").data("mop-minimum-amount");
            var maxVal = $(".mop-dropdown .selected").data("mop-maximum-amount");

            $('#MopCode').data('mop-minimum-amount', minVal);
            $('#MopCode').data('mop-maximum-amount', maxVal);
        }, 50);
    }
});

function showHideSearchButton(fundCode) {

    if ($.inArray(fundCode.toString(), paymentsAdmin.pages.payment.index.searchEnabledFundCodes) === -1) {
        $('.account-reference-search').hide()
        $('.account-reference-wrapper').removeClass('action');
    }
    else {
        $('.account-reference-search').show()
        $('.account-reference-wrapper').addClass('action');
    }

}

function showHideVatOptions() {

    if ($('.funds-dropdown .selected').data('vat-override') === "True") {
        $('.vat-option').show();
        $('.vat-dropdown').dropdown("clear");
    }
    else {
        $('.vat-option').hide();
        $('.vat-dropdown').dropdown("set selected", $(".funds-dropdown .selected").data("vat-default-code"));
    }
}

$('#FundCode, #AccountReference').change(function() {        
    var lookupItem = {
        fundCode: $('.ui.dropdown.funds-dropdown').dropdown('get value'),
        accountReference: $.trim($('#AccountReference').val())
    }
    
    if (lookupItem.fundCode.length == 0 || lookupItem.accountReference.length == 0) return;

    paymentsAdmin.services.accountHolder.lookup(lookupItem,
        function() {
            if (lookupItem.name) {
                $("#AccountName").html("<strong>Name</strong> " + lookupItem.name);
                $("#AccountBalance").html("<strong>Outs. Balance</strong> £" + lookupItem.outstandingBalance);
            } else {
                $("#AccountName").html("");
                $("#AccountBalance").html("");
            }
        });
});

$('.account-reference-search').click(function (e) {

    var fundCode = $('#FundCode').val();
    var accountReference = $('#AccountReference').val();

    if (fundCode === "") {
        var html = [];

        html.push('<span class="field-validation-error" data-valmsg-replace="true" data-valmsg-for="AccountReference">');
        html.push('     <span for="Amount">You must select a fund before searching</span>');
        html.push('</span>');

        var text = $('.account-reference-validator');

        text.empty();
        text.append(html.join(''));

        return false;
    }


    var url = paymentsAdmin.pages.payment.index.searchAction + "?fundCode=" + fundCode;

    if (accountReference !== '') {
        url = url + "&accountReference=" + encodeURIComponent(accountReference.trim())
    }

    $('.account-reference-search').attr('href', url);

})

$('#Amount').blur(function (e) {
    var message = validateInput();

    var text = $('.amount-validator');

    text.empty();

    if (message.length != 0) {
        text.empty();
        text.append(message);
        text.addClass('field-validation-error');
        text.removeClass('field-validation-valid');
        $('.message').show();
        e.preventDefault();
        $('#Amount').addClass('input-validation-error');
        return false;
    }
    else {

        text.removeClass('field-validation-error');
        text.addClass('field-validation-valid');
        $('#Amount').removeClass('input-validation-error');
    }
});

$.validator.setDefaults({ ignore: null });


$(".add-to-basket").click(function (e) {

    var message = validateInput();

    var text = $('.amount-validator');

    text.empty();

    if (message.length != 0) {
        text.empty();
        text.append(message);
        text.addClass('field-validation-error');
        text.removeClass('field-validation-valid');
        $('.message').show();
        e.preventDefault();
        return false;
    }
    else {

        text.removeClass('field-validation-error');
        text.addClass('field-validation-valid');
    }
});

if ($(".mop-dropdown").length > 0 && $(".basket__mop").length > 0) {
    $(".mop-dropdown").dropdown("set selected", $(".basket__mop").first().data("mop"));
}

function validateInput(message) {
    var result = true;

    var html = [];

    if (parseFloat($('#Amount').val()) < parseFloat($("#MopCode").data("mop-minimum-amount"))) {
        html.push('You must enter an amount greater than £' + $("#MopCode").data("mop-minimum-amount"));
        result = false;
    }

    if (parseFloat($('#Amount').val()) > parseFloat($("#MopCode").data("mop-maximum-amount"))) {
        html.push('You must enter an amount less than £' + $("#MopCode").data("mop-maximum-amount"));
        result = false;
    }

    if (result) {
        return '';
    }
    else {
        return html.join('');
    }
}

$(".post-payment").on("click",
    function (e) {

        if (window.confirm("Are you sure you want to post this payment?")) {

        } else {
            e.preventDefault();
            return false;
        }
    });

$(window).on('scroll', function() {
    if ($(window).scrollTop() >= 70) {
        $(".page-actions--fix").addClass("page-actions--fixed");
    } else {
        $(".page-actions--fix").removeClass("page-actions--fixed");
    }
})