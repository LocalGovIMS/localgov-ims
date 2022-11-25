// Run this before the dropdowns are generated - it will ensure the MOP is preselected
if ($('#MopCode').length > 0 && $(".basket__mop").length > 0) {
    $('#MopCode').val($(".basket__mop").first().data("mop"));
}

$(document).ready(function () {

    accessibleAutocomplete.enhanceSelectElement({
        displayMenu: 'overlay',
        autoSelect: false,
        confirmOnBlur: false,
        showAllValues: true,
        defaultValue: '',
        preserveNullOptions: true,
        placeholder: 'Select fund type',
        dropdownArrow: () => paymentsAdmin.controls.autocomplete.downarrow,
        selectElement: document.querySelector('#FundCode'),
        onConfirm: (val) => {
            handleFundTypeChange(val);
        }
    });

    accessibleAutocomplete.enhanceSelectElement({
        displayMenu: 'overlay',
        autoSelect: false,
        confirmOnBlur: false,
        showAllValues: true,
        defaultValue: '',
        preserveNullOptions: true,
        placeholder: 'Select MOP code',
        dropdownArrow: () => paymentsAdmin.controls.autocomplete.downarrow,
        selectElement: document.querySelector('#MopCode'),
        onConfirm: (val) => {
            handleMopCodeChange(val);
        }
    });

    accessibleAutocomplete.enhanceSelectElement({
        displayMenu: 'overlay',
        autoSelect: false,
        confirmOnBlur: false,
        showAllValues: true,
        defaultValue: '',
        preserveNullOptions: true,
        placeholder: 'Select VAT code',
        dropdownArrow: () => paymentsAdmin.controls.autocomplete.downarrow,
        selectElement: document.querySelector('#VatCode')
    });

});

function handleFundTypeChange(val) {

    // Get the selected option
    var option = getSelectedAccessibleAutocompleteOption('#FundCode', val);

    // Clear account reference validator
    var text = $('.account-reference-validator');
    text.empty();

    // Clear account reference
    $('#AccountReference').val('');

    // Toggle search button visibility
    showHideSearchButton(option.value);

    setTimeout(function () { showHideVatOptions(option.dataset.vatOverride === "True", option.dataset.vatDefaultCode); }, 50);

    $('#FundCode-select').val(option.value);

    loadAccountDetails();
}

function handleMopCodeChange(val) {

    setTimeout(function () {

        var option = getSelectedAccessibleAutocompleteOption('#MopCode', val);

        var minVal = option.dataset.mopMinimumAmount;
        var maxVal = option.dataset.mopMaximumAmount;
        var isAPaymentReversal = option.dataset.mopIsAPaymentReversal.toLowerCase() === 'true';

        if (isAPaymentReversal) {
            $('.payment-reversal-warning').css('display', 'flex');
        }
        else {
            $('.payment-reversal-warning').css('display', 'none');
        }

        $('#MopCode').data('mop-minimum-amount', minVal);
        $('#MopCode').data('mop-maximum-amount', maxVal);

        $('#MopCode-select').val(option.value);

        var errorMessage = validateAmount(null);
        showAmountErrorMessage(errorMessage);

    }, 50);

}

function showHideSearchButton(fundCode) {
    if ($.inArray(fundCode.toString(), paymentsAdmin.pages.payment.index.searchEnabledFundCodes) === -1) {
        $('.account-reference-search').hide()
        $('.account-reference-wrapper').removeClass('input-group');
    }
    else {
        $('.account-reference-search').show()
        $('.account-reference-wrapper').addClass('input-group');
    }
}

function showHideVatOptions(allowVatOverride, defaultVatCode) {

    if (allowVatOverride) {
        $('.vat-option').show();
        $('#VatCode-select').val('');
    }
    else {
        $('.vat-option').hide();
        if (defaultVatCode) {

            console.log('Here we set the default VAT code - but this does not work', defaultVatCode);
            $('#VatCode-select').val(defaultVatCode);
            $('#VatCode').val(defaultVatCode);
        }
    }
}

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

    var url = paymentsAdmin.pages.payment.index.searchAction + "?fundCode=" + getSelectedAccessibleAutocompleteOptionValue('#FundCode', fundCode);

    if (accountReference !== '') {
        url = url + "&accountReference=" + encodeURIComponent(accountReference.trim())
    }

    $('.account-reference-search').attr('href', url);

})

$('#AccountReference').change(function () {
    loadAccountDetails();
});

function loadAccountDetails() {

    var lookupItem = {
        fundCode: getSelectedAccessibleAutocompleteOptionValue('#FundCode', $('#FundCode').val()),
        accountReference: $.trim($('#AccountReference').val())
    }

    if (lookupItem.fundCode.length == 0 || lookupItem.accountReference.length == 0) return;

    paymentsAdmin.services.accountHolder.lookup(lookupItem,
        function () {
            if (lookupItem.name) {
                $("#AccountNameWrapper").html('<small><label class="fw-bold" for="AccountName">Name:</label> <span id="AccountName">' + lookupItem.name + '</span></small>');
                $("#AccountBalanceWrapper").html('<small><label class="fw-bold" for="OutstandingBalance">Outstanding balance:</label> £<span id="OutstandingBalance">' + lookupItem.outstandingBalance + '</span></small>');
            } else {
                $("#AccountNameWrapper").html("");
                $("#AccountBalanceWrapper").html("");
            }
        });
}

$('#Amount').blur(function (e) {

    var errorMessage = validateAmount(e);
    showAmountErrorMessage(errorMessage);

    if (errorMessage.length != 0) {
        e.preventDefault();
        return false;
    }

});

$(".add-to-basket").click(function (e) {

    var errorMessage = validateAmount(e);
    showAmountErrorMessage(errorMessage);

    console.log(errorMessage);

    if (errorMessage.length != 0) {
        e.preventDefault();
        return false;
    }

});

function validateAmount() {
    var result = true;
    var amount = $('#Amount').val();

    var html = [];

    if (parseFloat(amount) < parseFloat($("#MopCode").data("mop-minimum-amount"))) {
        html.push('You must enter an amount greater than £' + $("#MopCode").data("mop-minimum-amount"));
        result = false;
    }

    if (parseFloat(amount) > parseFloat($("#MopCode").data("mop-maximum-amount"))) {
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

function showAmountErrorMessage(errorMessage) {

    var text = $('.amount-validator');
    text.empty();

    if (errorMessage.length != 0) {
        text.append(errorMessage);
        text.addClass('field-validation-error');
        text.removeClass('field-validation-valid');
        $('.message').show();
        $('#Amount').addClass('input-validation-error');
        return false;
    }
    else {
        text.removeClass('field-validation-error');
        text.addClass('field-validation-valid');
        if ($('#Amount-error').length == 0) {
            $('#Amount').removeClass('input-validation-error');
        }
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

$.validator.setDefaults({ ignore: null });



// New generic methods

function getSelectedAccessibleAutocompleteOption(querySelector, selectedText) {
    const option = Array.from(document.querySelector(querySelector + '-select').querySelectorAll("option")).find(
        (o) => o.innerText === selectedText
    );

    return option;
}

function getSelectedAccessibleAutocompleteOptionValue(querySelector, selectedText) {
    const option = Array.from(document.querySelector(querySelector + '-select').querySelectorAll("option")).find(
        (o) => o.innerText === selectedText
    );

    if (option) {
        return option.value;
    }
    else {
        return '';
    }
}

function getAccessibleAutocompleteOptionByValue(querySelector, value) {

    const option = Array.from(document.querySelector(querySelector + '-select').querySelectorAll("option")).find(
        (o) => o.value === value.toString()
    );

    return option;
}