function validateJournal() {
    var result = true;

    var html = [];

    html.push('<ul class=\'mb-0\'>');

    if (!$('#JournalItem_FundCode').val()) {
        html.push('<li>You must select a fund type</li>');
        result = false;
    }

    if (!$('#JournalItem_AccountReference').val()) {
        html.push('<li>You must supply an account reference</li>');
        result = false;
    }

    if (!$('#JournalItem_VatCode').val()) {
        html.push('<li>You must select a VAT code</li>');
        result = false;
    }

    if (!$('#JournalItem_MopCode').val()) {
        html.push('<li>You must select a MOP code</li>');
        result = false;
    }

    if (!$('#JournalItem_Amount').val()) {
        html.push('<li>You must supply an amount</li>');
        result = false;
    }

    if (result) {
        if (!$.isNumeric($('#JournalItem_Amount').val())) {
            html.push('<li>The amount supplied is not valid</li>');
            result = false;
        }
    }

    if (result) {
        if (parseFloat($('#JournalItem_Amount').val()) <= 0) {
            html.push('<li>The amount to journal must be a positive number</li>');
            result = false;
        }
    }

    if (result) {
        if (parseFloat($('#JournalItem_Amount').val()) > remainingAvailableToJournal()) {
            html.push('<li>The amount entered exceeds the amount available to journal</li>');
            result = false;
        }
    }

    if (result) {
        if (parseFloat($('#JournalItem_AccountReference').val().length) > 30) {
            html.push('<li>The account reference is too long, it can be a maximum of 30 characters</li>');
            result = false;
        }
    }

    if (result) {
        if (parseFloat($('#JournalItem_Narrative').val().length) > 100) {
            html.push('<li>The narrative is too long, it can be a maximum of 100 characters</li>');
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