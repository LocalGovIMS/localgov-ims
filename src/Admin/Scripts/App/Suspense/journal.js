$(document).ready(function () {

    $(".journal").click(function () {

        $('#transfer-dialog').modal({
            onApprove: function () {
                return false;
            },
            onShow: function () {
                $.validator.unobtrusive.parse($("#transfer-dialog form"));
                $('#amount-available-to-transfer').text(parseFloat(remainingAvailableToTransfer() || 0).toFixed(2));
                renderJournalItems();
            }
        }).modal('show');

    });

    $(".cancel-transfer").click(function (e) {
        clearTransfers();
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

            if (Model.transfers.length > 0) {
                id = parseInt( Model.transfers.length);
            }

            var transferItem = {
                id: id,
                fundCode: $('.ui.dropdown.js-fund').dropdown('get value'),
                fundName: $('.ui.dropdown.js-fund').dropdown('get text'),
                vatCode: $(".js-vat").dropdown('get value'),
                mopCode: $(".js-mop").dropdown('get value'),
                mopName: $(".js-mop").dropdown('get text'),
                accountReference: $.trim($('#JournalItem_AccountReference').val()),
                amount: parseFloat($('#JournalItem_Amount').val()),
                name: "",
                outstandingBalance: "",
                narrative: $('#JournalItem_Narrative').val(),
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
                        paymentsAdmin.services.accountHolder.lookup(transferItem, function () {
                            Model.transfers.unshift(transferItem);
                            saveModel();

                            $('#JournalItem_AccountReference').val("");
                            $('#JournalItem_Amount').val("");
                            $('#JournalItem_Narrative').val();

                            renderJournalItems();

                            $('.transfer-message').hide();
                            $("#transfer-dialog").modal('refresh');
                        })
                    }
                    else {
                        if (returnData.message.length > 0) {
                            showSubmitError(returnData.message);
                        }
                        else {
                            showSubmitError('An unknown error occurred whilst saving the transfer');
                        }
                    }
                },
                failure: function () {
                    showSubmitError('An unknown error occurred whilst saving the transfer');
                },
                error: function () {
                    showSubmitError('An unknown error occurred whilst saving the transfer');
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

        if (Model.transfers.length <= 0) {
            showSubmitError('There are no journals to submit');
            return;
        }

        if (!transferAmountMatchesTotalAvailable()) {
            showSubmitError('The amount to journal must match the total amount of selected suspense items');
            return;
        }

        $.ajax({
            type: "POST",
            url: rootUrl + "/Suspense/SubmitTransfers",
            data: JSON.stringify({
                suspenseItems: _.map(Model.suspenseItems, 'id'),
                creditNotes: Model.creditNotes,
                journalItems: Model.transfers
            }),
            datatype: "JSON",
            contentType: "application/json; charset=utf-8",
            success: function (returnData) {
                if (returnData.ok) {
                    clearForm();
                    clearModel();
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
        $('#JournalItem_AccountReference').val('');
        $('#JournalItem_Amount').val('');

        //var text = $('.transfer-message-text');
        //text.empty();
        $('.transfer-message').hide();

        renderJournalItems();
    }

    function validateInput(message) {
        var result = true;

        var html = [];

        html.push('<ul>');

        if (!$('#JournalItem_FundCode').val()) {
            html.push('<li>You must select a fund code</li>');
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
            if (parseFloat($('#JournalItem_Amount').val()) > totalAvailableToTransfer()) {
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

        Model.transfers = Model.transfers.filter(function (el) {
            return parseInt(el.id) !== parseInt(id);
        });

        saveModel();

        renderJournalItems();
    }

    function lookupAccountHolder(transferItem, onCompleteCallback) {

    }

    function renderJournalItems() {
        unBindClickEvent();

        $('#transfer-table-body').empty();

        if (Model.transfers.length > 0) {
            $(Model.transfers).each(function (index) {
                $('#transfer-table-body').append('<tr><td>' + this.fundName + '</td><td>' + this.mopName + '</td><td>' + this.accountReference + '</td><td>' + this.name + '</td><td>' +
                    (this.outstandingBalance ? this.outstandingBalance.toFixed(2) : "") + '</td><td>' + this.vatCode + '</td><td>' + this.amount.toFixed(2) + '</td><td>' + this.narrative + '</td><td><a href=\'#\' class=\'ui red icon button right floated remove-transfer\' data-id=\'' + this.id + '\'><i class="ui delete icon"></i></a></td></tr>');
            });

            $('#transfer-table').show();
        }

        $('#amount-available-to-transfer').text(remainingAvailableToTransfer().toFixed(2));

        if (transferAmountMatchesTotalAvailable() && totalAvailableToTransfer() > totalCreditNotes()) {
            $(".submit-transfer").removeClass("disabled");
        } else {
            $(".submit-transfer").addClass("disabled");
        }

        bindClickEvent();
    }

});