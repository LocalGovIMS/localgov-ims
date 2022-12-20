$(document).ready(function () {

    creditNoteFundCode = accessibleAutocomplete.enhanceSelectElement({
        displayMenu: 'overlay',
        autoSelect: false,
        confirmOnBlur: false,
        showAllValues: true,
        defaultValue: '',
        preserveNullOptions: true,
        placeholder: 'Select fund type',
        dropdownArrow: () => paymentsAdmin.controls.autocomplete.downarrow,
        selectElement: document.querySelector('#CreditNote_FundCode'),
        onConfirm: (val) => {
            handleFundTypeChange('CreditNote', val, creditNoteVatCode);
        }
    });

    creditNoteVatCode = accessibleAutocomplete.enhanceSelectElement({
        displayMenu: 'overlay',
        autoSelect: false,
        confirmOnBlur: false,
        showAllValues: true,
        defaultValue: '',
        preserveNullOptions: true,
        placeholder: 'Select VAT code',
        dropdownArrow: () => paymentsAdmin.controls.autocomplete.downarrow,
        selectElement: document.querySelector('#CreditNote_VatCode')
    });

    addCreditNote = function (transactionRow, transactionArray, validator, messageContainerSelector, selectorPrefix) {

        var text = $(messageContainerSelector + '-text');
        var message = validator();

        if (message.length === 0) {

            var id = parseInt(0);

            if (transactionArray.length > 0) {
                id = parseInt(transactionArray[transactionArray.length - 1].id) + parseInt(1);
            }

            transactionRow.id = id;

            $.ajax({
                type: 'POST',
                url: rootUrl + '/Validation/ValidateTransferItem',
                data: JSON.stringify(transactionRow),
                datatype: 'JSON',
                contentType: 'application/json; charset=utf-8',
                success: function (returnData) {
                    if (returnData.ok) {
                        transactionArray.push(transactionRow);

                        saveModel();
                        clearCreditInputs();
                        clearCreditError();
                        renderCreditNotes();
                    }
                    else {
                        if (returnData.message.length > 0) {
                            showError(returnData.message);
                        }
                        else {
                            showError('An unknown error occured whilst saving the credit');
                        }
                    }
                },
                failure: function () {
                    showError('An unknown error occured whilst saving the credit');
                },
                error: function () {
                    showError('An unknown error occured whilst saving the credit');
                }

            });
        }
        else {
            text.empty();
            text.append(message);
            $(messageContainerSelector).show();
        }

    }

    $('.add-credit').click(function (e) {
        e.preventDefault();

        var fundCodeOption = paymentsAdmin.core.accessibleAutoComplete.getSelectedOptionByValue('#CreditNote_FundCode', $('#CreditNote_FundCode-select').val());
        var vatCodeOption = paymentsAdmin.core.accessibleAutoComplete.getSelectedOptionByValue('#CreditNote_VatCode', $('#CreditNote_VatCode-select').val());

        var creditNote = {
            fundCode: fundCodeOption.value,
            fundName: fundCodeOption.text,
            vatCode: vatCodeOption.value,
            accountReference: $.trim($('#CreditNote_AccountReference').val()),
            amount: parseFloat($('#CreditNote_Amount').val()),
        };

        addCreditNote(creditNote, Model.creditNotes, validateCreditNote, '.credit-message', 'CreditNote');
    });

    $('#creditNoteModal').on('hidden.bs.modal', function () {
        saveModel();
        clearCreditError();
    });

    $('#clearAllCreditNotes').click(function (e) {
        Model.creditNotes = [];
        saveModel();
        clearCreditError();
        renderCreditNotes();
    });

    function clearCreditInputs() {
        $('#CreditNote_AccountReference').val('');
        $('#CreditNote_Amount').val('');
    }

    function clearCreditError() {
        clearModalError('.credit-message', 'Unable to add the credit note');
    }

    function showError(message) {
        showModalError('.credit-message', message);
    }

    $('#credit-table').on('click', '.remove-credit',
        function () {
            clearCreditError();
            removeCredit($(this).attr('data-id'));
        });

    function removeCredit(id) {
        Model.creditNotes = Model.creditNotes.filter(function (el) {
            return parseInt(el.id) !== parseInt(id);
        });

        saveModel();

        renderCreditNotes();
    }
});

function renderCreditNotes() {
    $('#credit-table-body').empty();

    if (Model.creditNotes.length > 0) {
        $(Model.creditNotes).each(function (index) {
            $('#credit-table-body').append(
                '<tr>'
                + '<td>' + this.fundName + '</td>'
                + '<td>' + this.accountReference + '</td>'
                + '<td>' + this.vatCode + '</td>'
                + '<td class="text-end">' + this.amount.toFixed(2) + '</td>'
                + '<td><a href="#" class="btn btn-danger remove-credit" data-id="' + this.id + '">Remove</a></td>'
              + '</tr>');
        });

        $('#credit-table').show();
    }

    $('#amount-available-to-credit').text(totalCreditNotes().toFixed(2));
}