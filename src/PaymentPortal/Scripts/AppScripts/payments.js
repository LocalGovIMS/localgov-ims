// This is required because string.startsWith is ECMAScript 6 functionality, so some browsers wont have it yet
if (!String.prototype.startsWith) {
    String.prototype.startsWith = function (searchString, position) {
        position = position || 0;
        return this.indexOf(searchString, position) === position;
    };
}


var downarrow = '<svg version="1.1" xmlns="http://www.w3.org/2000/svg" class="autocomplete__dropdown-arrow-down" focusable="false"><g stroke="none" fill="outline" fill-rule="evenodd"><polygon fill="#000000" points="0 0 10 0 5 8"></polygon></g></svg>';

function paymentTypeChange() {

    var referenceFieldLabel = $("#PaymentType  option:selected").data("referencefieldlabel");
    var referenceFieldMessage = $("#PaymentType option:selected").data("referencefieldmessage");

    $('label[for="PaymentReference"]').text(referenceFieldLabel);
    $('span[for="paymentmessage"]').text(referenceFieldMessage);

}

$(document).ready(function () {

    $("#PaymentType").change(function () { paymentTypeChange(); })
    $("#PaymentReference").change(function () { paymentTypeChange(); })

    paymentTypeChange();

    $(".ui-send-email").click(function (e) {

        e.preventDefault();

        $.ajax({
            type: "POST",
            url: rootUrl + "Payment/EmailReceipt",
            data: JSON.stringify({
                emailAddress: $('#EmailAddress').val(),
                pspReference: $('#PspReference').val(),
                hash: $('#Hash').val()
            }),
            datatype: "JSON",
            contentType: "application/json; charset=utf-8",
            success: function (returnData) {
                if (returnData.ok) {
                    clearForm();
                    $('.email-receipt-message-success').show();
                }
                else {
                    showSubmitError('An unknown error occured whilst sending the e-mail');
                }
            },
            failure: function () {
                showSubmitError('An unknown error occured whilst sending the e-mail');
            },
            error: function () {
                showSubmitError('An unknown error occured whilst sending the e-mail');
            }

        });

    });

    function clearError() {
        $(".email-receipt-error").empty();
        $(".email-receipt-error").hide();
        $('.email-receipt-message-success').hide();
    }

    function clearForm() {
        $('.email-receipt-message').hide();
        $('.email-receipt-message-success').hide();
    }

    function showSubmitError(message) {

        var text = $('.email-receipt-message-text');

        var html = [];

        html.push('<ul class="mb-3">');
        html.push('<li>' + message + '</li>');
        html.push('</ul>');

        text.empty();
        text.append(html.join(''));

        $('.email-receipt-message').show();
    }
});