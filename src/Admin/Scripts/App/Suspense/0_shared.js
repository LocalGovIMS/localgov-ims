var Model = {
    suspenseItems: [],
    creditNotes: [],
    journals: []
}

function loadModel() {
    Model = JSON.parse(localStorage.getItem('suspenseModel'));
    if (Model === null) {
        Model = {
            suspenseItems: [],
            creditNotes: [],
            journals: []
        }
    }
    updateUI();
}

function saveModel() {
    localStorage.setItem('suspenseModel', JSON.stringify(Model));
    updateUI();
}

function clearModel() {
    Model = {
        suspenseItems: [],
        creditNotes: [],
        journals: []
    }
    saveModel();
    updateUI();
}

function clearTransfers() {
    Model.transfers = [];
    saveModel();
    updateUI();
}

function totalSuspenseItems() {
    return _.round(parseFloat(_.sumBy(Model.suspenseItems, "amount")), 2);
}

function totalCreditNotes() {
    return _.round(parseFloat(_.sumBy(Model.creditNotes, "amount")), 2);
}

function totalJournals() {
    return _.round(parseFloat(_.sumBy(Model.journals, "amount")), 2);
}

function totalAvailableToJournal() {
    return _.round(totalSuspenseItems() + totalCreditNotes(), 2);
}

function remainingAvailableToJournal() {
    return _.round(totalAvailableToJournal() - totalJournals(), 2);
}

function journalAmountMatchesTotalAvailable() {
    return _.round(remainingAvailableToJournal(), 2) == 0.00;
}

function handleFundTypeChange(querySelectorPrefix, val, vatCodeDropdown) {

    console.log('In shared.handleFundTypeChange()');

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

    console.log('In shared.showHideVatOptions()');

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

function showModalError(querySelectorPrefix, message) {

    console.log('In shared.showModalError();')

    var text = $(querySelectorPrefix + '-text');

    var html = [];

    html.push('<ul class=\'mb-0\'>');
    html.push('<li>' + message + '</li>');
    html.push('</ul>');

    text.empty();
    text.append(html.join(''));

    $(querySelectorPrefix).show();
}

function clearModalError(querySelectorPrefix, header) {

    console.log('In shared.clearModalError();')

    $(querySelectorPrefix + '-header').empty();
    $(querySelectorPrefix + '-header').text(header);
    $(querySelectorPrefix + '-text').empty();
    $(querySelectorPrefix).hide();
}