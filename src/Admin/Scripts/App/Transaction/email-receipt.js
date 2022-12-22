$(document).ready(function () {

    $(".send-email").click(function (e) {

        e.preventDefault();

        $.ajax({
            type: "POST",
            url: rootUrl + "/Transaction/EmailReceipt",
            data: JSON.stringify({
                emailAddress: $('#EmailAddress').val(),
                pspReference: transcationDetails.mainPspReference
            }),
            datatype: "JSON",
            contentType: "application/json; charset=utf-8",
            success: function (returnData) {
                if (returnData.ok) {
                    $("#emailReceiptModal").modal('hide');
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

    $("#emailReceiptModal").on("hidden.bs.modal", function () {
        clearForm();
    });

    function clearForm() {
        $('#EmailAddress').val('');
        $('.email-receipt-message').hide();
    }

    function showSubmitError(message) {

        var text = $('.email-receipt-message-text');

        var html = [];

        html.push('<ul class=\"mb-0\">');
        html.push('<li>' + message + '</li>');
        html.push('</ul>');

        text.empty();
        text.append(html.join(''));

        $('.email-receipt-message').show();
        $("#email-receipt-dialog").modal('refresh');
    }
});