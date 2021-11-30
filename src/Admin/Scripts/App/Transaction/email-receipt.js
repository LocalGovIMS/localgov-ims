$('.ui.dropdown').dropdown({});

$(document).ready(function () {

    $(".cancel-email").click(function (e) {
        clearForm();
    });

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

                console.log(returnData);

                if (returnData.ok) {
                    clearForm();
                    $("#email-receipt-dialog").modal('hide');
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
    }

    function clearForm() {
        Transfers = { items: [] };

        $('#TransferItem_AccountReference').val('');
        $('#TransferItem_Amount').val('');

        //var text = $('.transfer-message-text');
        //text.empty();
        $('.email-receipt-message').hide();
    }

    function showSubmitError(message) {

        var text = $('.email-receipt-message-text');

        var html = [];

        html.push('<ul>');
        html.push('<li>' + message + '</li>');
        html.push('</ul>');

        text.empty();
        text.append(html.join(''));

        $('.email-receipt-message').show();
        $("#email-receipt-dialog").modal('refresh');
    }

});