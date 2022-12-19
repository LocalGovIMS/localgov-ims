
function validateCreditNote() {
    var result = true;

    var html = [];

    html.push("<ul class=\"mb-0\">");

    if (!$("#CreditNote_FundCode").val()) {
        html.push("<li>You must select a fund type</li>");
        result = false;
    }

    if (!$("#CreditNote_AccountReference").val()) {
        html.push("<li>You must supply an account reference</li>");
        result = false;
    }
    if (!$('#CreditNote_VatCode').val()) {
        html.push('<li>You must select a VAT code</li>');
        result = false;
    }

    if (!$("#CreditNote_Amount").val()) {
        html.push("<li>You must supply an amount</li>");
        result = false;
    }

    if (result) {
        if (!$.isNumeric($("#CreditNote_Amount").val())) {
            html.push("<li>The amount supplied is not valid</li>");
            result = false;
        }
    }

    if (result) {
        if (parseFloat($("#CreditNote_Amount").val()) <= 0) {
            html.push("<li>The credit note must be a positive number</li>");
            result = false;
        }
    }

    if (result) {
        if (parseFloat($("#CreditNote_AccountReference").val().length) > 30) {
            html.push("<li>The account reference is too long, it can be a maximum of 30 characters</li>");
            result = false;
        }
    }

    html.push("</ul>");


    if (result) {
        return "";
    }
    else {
        return html.join("");
    }
}
