$(document).ready(function () {

    var transferFundCode;
    var transferVatCode;

    var Transfers = { items: [] };

    transferVatCode = accessibleAutocomplete.enhanceSelectElement({
        displayMenu: 'overlay',
        autoSelect: false,
        confirmOnBlur: false,
        showAllValues: true,
        defaultValue: '',
        preserveNullOptions: true,
        placeholder: 'Select VAT code',
        dropdownArrow: () => paymentsAdmin.controls.autocomplete.downarrow,
        selectElement: document.querySelector('#TransferItem_VatCode')
    });

    transferFundCode = accessibleAutocomplete.enhanceSelectElement({
        displayMenu: 'overlay',
        autoSelect: false,
        confirmOnBlur: false,
        showAllValues: true,
        defaultValue: '',
        preserveNullOptions: true,
        placeholder: 'Select fund type',
        dropdownArrow: () => paymentsAdmin.controls.autocomplete.downarrow,
        selectElement: document.querySelector('#TransferItem_FundCode'),
        onConfirm: (val) => {
            handleFundTypeChange('TransferItem', val, transferVatCode);
        }
    });

    $('#TransferItem_FundCode').attr('aria-labelledby', 'TransferItemFundCode');
    $('#TransferItem_VatCode').attr('aria-labelledby', 'TransferItemVatCode');

    $("#transferModal").on("hidden.bs.modal", function () {
        clearForm();
    });

    addTransaction = function (transactionRow, transactionArray, validator, messageContainerSelector, selectorPrefix) {

        clearError();

        var text = $(messageContainerSelector + '-text');
        var message = validator(selectorPrefix);

        if (message.length === 0) {

            var id = parseInt(0);

            if (transactionArray.items.length > 0) {
                id = parseInt(transactionArray.items[transactionArray.items.length - 1].id) + parseInt(1);
            }

            transactionRow.id = id;

            $.ajax({
                type: "POST",
                url: rootUrl + "/Validation/ValidateTransferItem",
                data: JSON.stringify(transactionRow),
                datatype: "JSON",
                contentType: "application/json; charset=utf-8",
                success: function (returnData) {

                    console.log(returnData);

                    if (returnData.ok) {
                        paymentsAdmin.services.accountHolder.lookup(transactionRow,
                            function () {
                                transactionArray.items.push(transactionRow);

                                clearError();
                                renderUI();

                                $('#TransferItem_AccountReference').val("");
                                $('#TransferItem_Amount').val("");
                                $('#TransferItem_Narrative').val("");

                            });
                    }
                    else {
                        if (returnData.message.length > 0) {
                            showError(returnData.message, messageContainerSelector);
                        }
                        else {
                            showError('An unknown error occured whilst saving the transfer', messageContainerSelector);
                        }
                    }
                },
                failure: function () {
                    showError('An unknown error occured whilst saving the transfer', messageContainerSelector);
                },
                error: function () {
                    showError('An unknown error occured whilst saving the transfer', messageContainerSelector);
                }

            });
        }
        else {
            text.empty();
            text.append(message);
            $(messageContainerSelector).show();
        }
    }

    $(".add-transfer").click(function (e) {
        e.preventDefault();

        var fundCodeOption = paymentsAdmin.core.accessibleAutoComplete.getSelectedOptionByValue('#TransferItem_FundCode', $('#TransferItem_FundCode-select').val());
        var vatCodeOption = paymentsAdmin.core.accessibleAutoComplete.getSelectedOptionByValue('#TransferItem_VatCode', $('#TransferItem_VatCode-select').val());

        var transactionRow = {
            fundCode: fundCodeOption.value,
            fundName: fundCodeOption.text,
            vatCode: vatCodeOption.value,
            accountReference: $.trim($('#TransferItem_AccountReference').val()),
            amount: parseFloat($('#TransferItem_Amount').val()).toFixed(2),
            narrative: $('#TransferItem_Narrative').val()
        }

        addTransaction(transactionRow, Transfers, validateInput, '.transfer-message', 'TransferItem');
    });

    $(".submit-transfer").click(function (e) {

        e.preventDefault();

        if (Transfers.items.length <= 0) {
            showSubmitError('There are no journals to submit');
            return;
        }

        $.ajax({
            type: "POST",
            url: rootUrl + "/Transaction/SubmitTransfers",
            data: JSON.stringify({
                pspReference: $('#Transaction_PspReference').val(),
                transactionReference: transcationDetails.selectedTransactionReference,
                transferItems: Transfers.items
            }),
            datatype: "JSON",
            contentType: "application/json; charset=utf-8",
            success: function (returnData) {
                if (returnData.ok) {
                    $("#transfer-dialog").modal('hide');
                    clearForm();
                    location.reload();
                }
                else {
                    if (returnData.message.length > 0) {
                        showSubmitError(returnData.message);
                    }
                    else {

                        showSubmitError('An unknown error occured whilst saving the journal');
                    }
                }
            },
            failure: function () {
                showSubmitError('An unknown error occured whilst saving the journal');
            },
            error: function () {
                showSubmitError('An unknown error occured whilst saving the journal');
            }

        });

    });

    function clearError() {
        $(".transfer-message-header").empty();
        $(".transfer-message-header").text('Unable to add the transfer transaction');
        $(".transfer-message-text").empty();
        $(".transfer-message").hide();
    }

    function clearForm() {
        Transfers = { items: [] };

        $('#TransferItem_AccountReference').val('');
        $('#TransferItem_Amount').val('');
        $('#TransferItem_Narrative').val('');

        $('.transfer-message').hide();

        renderTransferItems();
    }

    function validateInput(selectorPrefix) {
        var result = true;

        var html = [];

        html.push('<ul>');

        if (!$('#' + selectorPrefix + '_FundCode-select').val()) {
            html.push('<li>You must select a fund type</li>');
            result = false;
        }

        if (!$('#' + selectorPrefix + '_AccountReference').val()) {
            html.push('<li>You must supply an account reference</li>');
            result = false;
        }

        if (!$('#' + selectorPrefix + '_VatCode').val()) {
            html.push('<li>You must select a VAT code</li>');
            result = false;
        }

        if (!$('#' + selectorPrefix + '_Amount').val()) {
            html.push('<li>You must supply an amount</li>');
            result = false;
        }

        if (result) {
            if (!$.isNumeric($('#' + selectorPrefix + '_Amount').val())) {
                html.push('<li>The amount supplied is not valid</li>');
                result = false;
            }
        }

        if (result) {
            if (parseFloat($('#' + selectorPrefix + '_Amount').val()) <= 0) {
                html.push('<li>The amount to journal must be a positive number</li>');
                result = false;
            }
        }

        if (result) {
            if (parseFloat($('#' + selectorPrefix + '_Amount').val()) > totalAvailableToTransfer()) {
                html.push('<li>The amount entered exceeds the amount available to journal</li>');
                result = false;
            }
        }

        if (result) {
            if (parseFloat($('#' + selectorPrefix + '_AccountReference').val().length) > 30) {
                html.push('<li>The account reference is too long, it can be a maximum of 30 characters</li>');
                result = false;
            }
        }

        if (result) {
            if (parseFloat($('#' + selectorPrefix + '_Narrative').val().length) > 50) {
                html.push('<li>The narrative is too long, it can be a maximum of 50 characters</li>');
                result = false;
            }
        }

        html.push('</ul>');


        if (result) {
            return '';
        }
        else {
            return html.join('');
        }
    }

    function showSubmitError(message) {

        var text = $('.transfer-message-text');

        var html = [];

        html.push('<ul class=\"mb-0\">');
        html.push('<li>' + message + '</li>');
        html.push('</ul>');

        text.empty();
        text.append(html.join(''));

        $('.submit-transfer').removeAttr('disabled');

        $('.transfer-message').show();
        $("#transfer-dialog").modal('refresh');
    }

    function showError(message, selector) {

        var text = $(selector + '-text');

        var html = [];

        html.push('<ul class=\"mb-0\">');
        html.push('<li>' + message + '</li>');
        html.push('</ul>');

        text.empty();
        text.append(html.join(''));

        $(selector).show();
        $("#transfer-dialog").modal('refresh');
    }

    $("#transfer-table").on("click", ".remove-transfer",
        function (event) {
            clearError();
            removeTransfer($(this).attr('data-id'));
        });

    function removeTransfer(id) {

        Transfers.items = Transfers.items.filter(function (el) {
            return parseInt(el.id) !== parseInt(id);
        });

        renderTransferItems();
    }

    function renderUI() {
        renderTransferItems();
    }

    function renderTransferItems() {

        $('#transfer-table-body').empty();

        if (Transfers.items.length > 0) {
            $(Transfers.items).each(function (index) {
                $('#transfer-table-body').append('<tr><td>' + this.fundName + '</td><td>' + this.accountReference + '</td><td>' + (this.name ? this.name : "") + '</td><td class=\"text-end\">' +
                    (this.outstandingBalance ? this.outstandingBalance.toFixed(2) : "") + '</td><td>' + this.vatCode + '</td><td class=\"text-end\">' + this.amount + '</td><td>' + this.narrative + '</td><td><a href=\'#\' class=\'btn btn-danger remove-transfer\' data-id=\'' + this.id + '\'>Remove</a></td></tr>');
            });

            $('#transfer-table').show();
        }

        $('#amount-available-to-transfer').text(totalAvailableToTransfer());
    }

    function totalAvailableToTransfer() {
        var total = 0;

        for (i = 0; i < Transfers.items.length; i++) {
            total += parseFloat(Transfers.items[i].amount);
        }

        return (transcationDetails.amountAvailableToTransfer - total).toFixed(2);
    }

    function handleFundTypeChange(querySelectorPrefix, val, vatCodeDropdown) {

        var option = paymentsAdmin.core.accessibleAutoComplete.getSelectedOption('#' + querySelectorPrefix + '_FundCode', val);

        if (option.dataset.vatDefaultCode) {
            showHideVatOptions(querySelectorPrefix, option.dataset.vatOverride === "True", option.dataset.vatDefaultCode, vatCodeDropdown);
        }
        else {
            showHideVatOptions(querySelectorPrefix, option.dataset.vatOverride === "True", '', vatCodeDropdown);
        }

        $('#' + querySelectorPrefix + '_FundCode-select').val(option.value);
    }

    function showHideVatOptions(querySelectorPrefix, allowVatOverride, defaultVatCode, vatCodeDropdown) {

        document.getElementById(querySelectorPrefix + '_VatCode').disabled = false;
        document.getElementById(querySelectorPrefix + '_VatCode').removeAttribute("aria-disabled");

        paymentsAdmin.core.accessibleAutoComplete.setSelectedOption('#' + querySelectorPrefix + '_VatCode', defaultVatCode, vatCodeDropdown);

        setTimeout(function () {

            if (allowVatOverride == false) {
                document.getElementById(querySelectorPrefix + '_VatCode').disabled = true;
                document.getElementById(querySelectorPrefix + '_VatCode').setAttribute("aria-disabled", true);
            }

            $('#' + querySelectorPrefix + '_FundCode').focus();

        }, 5);

    }

});