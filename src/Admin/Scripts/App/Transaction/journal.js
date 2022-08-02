$('.ui.dropdown').dropdown({});

$(document).ready(function () {

    var Transfers = { items: [] };

    $(".cancel-transfer").click(function (e) {
        clearForm();
    });

    $('.js-fund').dropdown({
        onChange: function (e) {
            setTimeout(function () {
                if ($('.js-fund .selected').data('vat-override') === "True") {
                    $('.js-vat').removeClass('disabled');
                    $('.js-vat').dropdown("clear");
                    $('.js-vat .search').prop('disabled', false);
                } else {
                    $('.js-vat').removeClass('disabled');
                    $('.js-vat').dropdown("set selected", $(".js-fund .selected").data("vat-default-code"));
                    $('.js-vat').addClass('disabled');
                    $('.js-vat .search').prop('disabled', true);
                }
            },
                50
            );
        }
    });

    $(".add-transfer").click(function (e) {

        e.preventDefault();

        var text = $('.transfer-message-text');
        var message = validateInput();

        if (message.length === 0) {

            var id = parseInt(0);

            if (Transfers.items.length > 0) {
                id = parseInt(Transfers.items[Transfers.items.length - 1].id) + parseInt(1);
            }

            var transferItem = {
                id: id,
                fundCode: $('.ui.dropdown.js-fund').dropdown('get value'),
                fundName: $('.ui.dropdown.js-fund').dropdown('get text'),
                vatCode: $(".js-vat").dropdown('get value'),
                accountReference: $.trim($('#TransferItem_AccountReference').val()),
                amount: parseFloat($('#TransferItem_Amount').val()).toFixed(2),
                narrative: $('#TransferItem_Narrative').val(),
            }

            console.log(transferItem);

            $.ajax({
                type: "POST",
                url: rootUrl + "/Validation/ValidateTransferItem",
                data: JSON.stringify(transferItem),
                datatype: "JSON",
                contentType: "application/json; charset=utf-8",
                success: function (returnData) {
                    if (returnData.ok) {
                        paymentsAdmin.services.accountHolder.lookup(transferItem,
                            function () {
                                Transfers.items.push(transferItem);

                                $('.ui.dropdown.js-fund').dropdown('clear');
                                $(".js-vat").dropdown('clear');
                                $('#TransferItem_AccountReference').val("");
                                $('#TransferItem_Amount').val("");
                                $('#TransferItem_Narrative').val("");

                                renderTransferItems();

                                $('.transfer-message').hide();
                                $("#transfer-dialog").modal('refresh');
                            });
                    }
                    else {
                        if (returnData.message.length > 0) {
                            showSubmitError(returnData.message);
                        }
                        else {
                            showSubmitError('An unknown error occured whilst saving the transfer');
                        }
                    }
                },
                failure: function () {
                    showSubmitError('An unknown error occured whilst saving the transfer');
                },
                error: function () {
                    showSubmitError('An unknown error occured whilst saving the transfer');
                }

            });
        }
        else {
            text.empty();
            text.append(message);
            $('.transfer-message').show();
        }

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
                    clearForm();
                    $("#transfer-dialog").modal('hide');
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
        $(".transfer-error").empty();
        $(".transfer-error").hide();
    }

    function clearForm() {
        Transfers = { items: [] };

        $('#TransferItem_AccountReference').val('');
        $('#TransferItem_Amount').val('');

        //var text = $('.transfer-message-text');
        //text.empty();
        $('.transfer-message').hide();

        renderTransferItems();
    }

    function validateInput(message) {
        var result = true;

        var html = [];

        html.push('<ul>');

        if (!$('#TransferItem_FundCode').val()) {
            html.push('<li>You must select a fund type</li>');
            result = false;
        }

        if (!$('#TransferItem_AccountReference').val()) {
            html.push('<li>You must supply an account reference</li>');
            result = false;
        }

        if (!$('#TransferItem_VatCode').val()) {
            html.push('<li>You must select a VAT code</li>');
            result = false;
        }

        if (!$('#TransferItem_Amount').val()) {
            html.push('<li>You must supply an amount</li>');
            result = false;
        }

        if (result) {
            if (!$.isNumeric($('#TransferItem_Amount').val())) {
                html.push('<li>The amount supplied is not valid</li>');
                result = false;
            }
        }

        if (result) {
            if (parseFloat($('#TransferItem_Amount').val()) <= 0) {
                html.push('<li>The amount to journal must be a positive number</li>');
                result = false;
            }
        }

        if (result) {
            if (parseFloat($('#TransferItem_Amount').val()) > totalAvailableToTransfer()) {
                html.push('<li>The amount entered exceeds the amount available to journal</li>');
                result = false;
            }
        }

        if (result) {
            if (parseFloat($('#TransferItem_AccountReference').val().length) > 30) {
                html.push('<li>The account reference is too long, it can be a maximum of 30 characters</li>');
                result = false;
            }
        }

        if (result) {
            if (parseFloat($('#TransferItem_Narrative').val().length) > 50) {
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

        html.push('<ul>');
        html.push('<li>' + message + '</li>');
        html.push('</ul>');

        text.empty();
        text.append(html.join(''));

        $('.transfer-message').show();
        $("#transfer-dialog").modal('refresh');
    }

    function unBindClickEvent() {
        $("#transfer-table tbody tr td").unbind()
    }

    function bindClickEvent() {
        $("#transfer-table tbody tr td").on("click", ".remove-transfer", function (event) {
            clearError();
            removeTransfer($(this).attr('data-id'))
        });
    }

    function removeTransfer(id) {

        Transfers.items = Transfers.items.filter(function (el) {
            return parseInt(el.id) !== parseInt(id);
        });

        renderTransferItems();
    }

    function renderTransferItems() {
        unBindClickEvent();

        $('#transfer-table-body').empty();

        if (Transfers.items.length > 0) {
            $(Transfers.items).each(function (index) {
                $('#transfer-table-body').append('<tr><td>' + this.fundName + '</td><td>' + this.accountReference + '</td><td>' + (this.name ? this.name : "") + '</td><td>' +
                    (this.outstandingBalance ? this.outstandingBalance.toFixed(2) : "") + '</td><td>' + this.vatCode + '</td><td>' + this.amount + '</td><td>' + this.narrative + '</td><td><a href=\'#\' class=\'ui red button right floated remove-transfer\' data-id=\'' + this.id + '\'>Remove</a></td></tr>');
            });

            $('#transfer-table').show();
        }

        $('#amount-available-to-transfer').text(totalAvailableToTransfer());

        bindClickEvent();
    }

    function totalAvailableToTransfer() {
        var total = 0;

        for (i = 0; i < Transfers.items.length; i++) {
            total += parseFloat(Transfers.items[i].amount);
        }

        return (transcationDetails.amountAvailableToTransfer - total).toFixed(2);
    }

});