$(document).ready(function () {

    var initialFundCodeValue = $('#FundCode').val();
    var initialFundMessageIdValue = $('#FundMessageId').val();

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
            const option = Array.from(document.querySelector('#FundCode-select').querySelectorAll("option")).find(
                (o) => o.innerText === val
            );

            if (option) {
                configureFundMessage(option.value, null);
            }

            $('#FundCode-select').val(option.value);
        }
    });

    configureFundMessage(initialFundCodeValue, initialFundMessageIdValue);

});

function configureFundMessage(fundCode, fundMessageId) {

    console.log(fundCode, fundMessageId);

    // 1. Clear the fund message dropdown - we've changed something, or are loading the page, so reset.
    $('#FundMessageIdAccessibleAutocompleteWrapper').empty();

    // 2. Set our FundMessageId - this may be null if we're rebuilding this due to a change to the Fund selection
    $('#FundMessageId').val(fundMessageId);

    // 3. Get options for the specified fund code
    var availableOptions = paymentsAdmin.pages.accountHolder.fundMessageOptions.filter(function (fundMessageOption) {
        return fundMessageOption.data.some((data) => data.Key == 'fundCode' && data.Value == fundCode)
    });

    var availableOptions = availableOptions.map(a => a.text);
    
    if (jQuery.isEmptyObject(availableOptions)) {
        $('.fund-message-selector').hide();
    }
    else {
        $('.fund-message-selector').show();

        availableOptions.unshift('None');

        let defaultValue = '';
        if (fundMessageId) {

            option = paymentsAdmin.pages.accountHolder.fundMessageOptions.find(function (fundMessageOption) {
                return fundMessageOption.id == fundMessageId
            });

            defaultValue = option.text;
        }

        accessibleAutocomplete({
            element: document.querySelector('#FundMessageIdAccessibleAutocompleteWrapper'),
            id: 'FundMessageId',
            source: availableOptions,
            displayMenu: 'overlay',
            autoSelect: false,
            confirmOnBlur: false,
            showAllValues: true,
            defaultValue: defaultValue,
            preserveNullOptions: true,
            placeholder: 'Select fund message',
            dropdownArrow: () => paymentsAdmin.controls.autocomplete.downarrow,
            onConfirm: (val) => {
                const option = paymentsAdmin.pages.accountHolder.fundMessageOptions.find(
                    (o) => o.text === val
                );

                console.log(option);

                if (option) {
                    $('#FundMessageId').val(option.id);
                }
                else {
                    $('#FundMessageId').val(null);
                }
            }
        });
    }
}