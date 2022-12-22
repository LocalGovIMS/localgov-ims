$(document).ready(function () {

    var journalFundCode;
    var journalMopCode;
    var journalVatCode;

    var Journals = { items: [] };

    journalMopCode = accessibleAutocomplete.enhanceSelectElement({
        displayMenu: 'overlay',
        autoSelect: false,
        confirmOnBlur: false,
        showAllValues: true,
        defaultValue: '',
        preserveNullOptions: true,
        placeholder: 'Select MOP code',
        dropdownArrow: () => paymentsAdmin.controls.autocomplete.downarrow,
        selectElement: document.querySelector('#JournalItem_MopCode')
    });

    journalFundCode = accessibleAutocomplete.enhanceSelectElement({
        displayMenu: 'overlay',
        autoSelect: false,
        confirmOnBlur: false,
        showAllValues: true,
        defaultValue: '',
        preserveNullOptions: true,
        placeholder: 'Select fund type',
        dropdownArrow: () => paymentsAdmin.controls.autocomplete.downarrow,
        selectElement: document.querySelector('#JournalItem_FundCode'),
        onConfirm: (val) => {
            handleFundTypeChange('JournalItem', val, journalVatCode);
        }
    });

    journalVatCode = accessibleAutocomplete.enhanceSelectElement({
        displayMenu: 'overlay',
        autoSelect: false,
        confirmOnBlur: false,
        showAllValues: true,
        defaultValue: '',
        preserveNullOptions: true,
        placeholder: 'Select VAT code',
        dropdownArrow: () => paymentsAdmin.controls.autocomplete.downarrow,
        selectElement: document.querySelector('#JournalItem_VatCode')
    });

    $('#JournalItem_MopCode').attr('aria-labelledby', 'JournalItemMopCode');
    $('#JournalItem_FundCode').attr('aria-labelledby', 'JournalItemFundCode');
    $('#JournalItem_VatCode').attr('aria-labelledby', 'JournalItemVatCode');

    //paymentsAdmin.core.accessibleAutoComplete.setSelectedOption('#JournalItem_MopCode', 'JR', journalMopCode);
    
    addTransaction = function (transactionRow, transactionArray, validator, messageContainerSelector, selectorPrefix) {

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
                success: function (returnData)
                {
                    if (returnData.ok) {
                        paymentsAdmin.services.accountHolder.lookup(transactionRow,
                            function () {
                                transactionArray.push(transactionRow);

                                saveModel();
                                clearJournalInputs();
                                clearJournalError();
                                renderJournals();
                            });
                    }
                    else {
                        if (returnData.message.length > 0) {
                            showError(returnData.message);
                        }
                        else {
                            showError('An unknown error occured whilst saving the journal');
                        }
                    }
                },
                failure: function () {
                    showError('An unknown error occured whilst saving the journal');
                },
                error: function () {
                    showError('An unknown error occured whilst saving the journal');
                }

            });
        }
        else {
            text.empty();
            text.append(message);
            $(messageContainerSelector).show();
        }
    }

    $('.add-journal').click(function (e) {
        e.preventDefault();

        var fundCodeOption = paymentsAdmin.core.accessibleAutoComplete.getSelectedOptionByValue('#JournalItem_FundCode', $('#JournalItem_FundCode-select').val());
        var mopCodeOption = paymentsAdmin.core.accessibleAutoComplete.getSelectedOptionByValue('#JournalItem_MopCode', $('#JournalItem_MopCode-select').val());
        var vatCodeOption = paymentsAdmin.core.accessibleAutoComplete.getSelectedOptionByValue('#JournalItem_VatCode', $('#JournalItem_VatCode-select').val());

        var journal = {
            fundCode: fundCodeOption.value,
            fundName: fundCodeOption.text,
            mopCode: mopCodeOption.value,
            mopName: mopCodeOption.text,
            vatCode: vatCodeOption.value,
            accountReference: $.trim($('#JournalItem_AccountReference').val()),
            amount: parseFloat($('#JournalItem_Amount').val()),
            narrative: $('#JournalItem_Narrative').val(),
            name: '',
            outstandingBalance: ''
        };

        addTransaction(journal, Model.journals, validateJournal, '.journal-message', 'JournalItem');
    });   

    $('#journalModal').on('hidden.bs.modal', function () {
        saveModel();
        clearJournalError();
    });

    $('#clearAllJournals').click(function (e) {
        Model.journals = [];
        saveModel();
        clearJournalError();
        renderJournals();
    });

    $('.submit-journal').click(function (e) {

        e.preventDefault();

        if (Model.journals.length <= 0) {
            showSubmitError('There are no journals to submit');
            return;
        }

        if (!journalAmountMatchesTotalAvailable()) {
            showSubmitError('The amount to journal must match the total amount of selected suspense items');
            return;
        }

        $.ajax({
            type: 'POST',
            url: rootUrl + '/Suspense/SubmitJournals',
            data: JSON.stringify({
                suspenseItems: _.map(Model.suspenseItems, 'id'),
                creditNotes: Model.creditNotes,
                journalItems: Model.journals
            }),
            datatype: 'JSON',
            contentType: 'application/json; charset=utf-8',
            success: function (returnData) {
                if (returnData.ok) {

                    clearModel();

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

    function clearJournalError() {
        clearModalError('.journal-message', 'Unable to add the journal');
    }

    function clearJournalInputs() {
        $('#JournalItem_AccountReference').val('');
        $('#JournalItem_Amount').val('');
        $('#JournalItem_Narrative').val('');
    }

    function showSubmitError(message) {
        showError(message);
        $('.submit-journal').removeAttr('disabled');
    }

    function showError(message) {
        showModalError('.journal-message', message);
    }

    $('#journal-table').on('click', '.remove-journal',
        function () {
            clearJournalError();
            removeJournal($(this).attr('data-id'));
        });

    function removeJournal(id) {

        Model.journals = Model.journals.filter(function (el) {
            return parseInt(el.id) !== parseInt(id);
        });

        saveModel();

        renderJournals();
    }
});

function renderJournals() {
    $('#journal-table-body').empty();

    if (Model.journals.length > 0) {
        $(Model.journals).each(function (index) {
            $('#journal-table-body').append(
                '<tr>'
                + '<td>' + this.fundName + '</td>'
                + '<td>' + this.mopName + '</td>'
                + '<td>' + this.accountReference + '</td>'
                + '<td>' + this.name + '</td>'
                + '<td class="text-end">' + (this.outstandingBalance ? this.outstandingBalance.toFixed(2) : '') + '</td>'
                + '<td>' + this.vatCode + '</td>'
                + '<td class="text-end">' + this.amount.toFixed(2) + '</td>'
                + '<td>' + this.narrative + '</td>'
                + '<td><a href="#" class="btn btn-danger remove-journal" data-id="' + this.id + '">Remove</a></td>'
              + '</tr>');
        });

        $('#journal-table').show();
    }

    $('#amount-available-to-journal').text(remainingAvailableToJournal().toFixed(2));

    if (journalAmountMatchesTotalAvailable() && totalAvailableToJournal() > totalCreditNotes()) {
        $('.submit-journal').removeAttr('disabled');
    } else {
        $('.submit-journal').attr('disabled', 'disabled');
    }
}