$('.ui.dropdown').dropdown({});

$(document).ready(function () {
    var Sources = { items: [] };
    var Transfers = { items: [] };

    disableCompleteTransfer();

    $(".cancel-transfer").click(function (e) {
        clearForm();
    });

    $("#SourceItem_Amount").on("keyup change blur",
        function () {
            renderUI();
        });

    $('.sourceItem').dropdown({
        onChange: function (e) {
            setTimeout(function () {
                if ($('.sourceItem .selected').data('vat-override') === "True") {
                    $('.sourceVat').removeClass('disabled');
                    $('.sourceVat').dropdown('clear');
                    $('.sourceVat .search').prop('disabled', false);
                } else {
                    $('.sourceVat').removeClass('disabled');
                    $('.sourceVat').dropdown("set selected", $(".sourceItem .selected").data("vat-default-code"));
                    $('.sourceVat').addClass('disabled');
                    $('.sourceVat .search').prop('disabled', true);
                }
            },
                50
            );
        }
    });

    $('.targetItem').dropdown({
        onChange: function (e) {
            setTimeout(function () {
                if ($('.targetItem .selected').data('vat-override') === "True") {
                    $('.targetVat').removeClass('disabled');
                    $('.targetVat').dropdown('clear');
                    $('.targetVat .search').prop('disabled', false);
                } else {
                    $('.targetVat').dropdown("set selected", $(".targetItem .selected").data("vat-default-code"));
                    $('.targetVat').addClass('disabled');
                    $('.targetVat .search').prop('disabled', true);
                }

            },
                50
            );
        }
    });

    addTransaction = function (transactionRow, transactionArray, validator, messageContainerSelector) {


        var text = $(messageContainerSelector);
        var message = validator();

        if (message.length === 0) {

            var id = parseInt(0);

            if (transactionArray.items.length > 0) {
                id = parseInt(transactionArray.items[transactionArray.items.length - 1].id) + parseInt(1);
            }

            transactionRow.id = id;

            $.ajax({
                type: "POST",
                url: rootUrl + "/Validation/ValidateTransferItem",
                data: JSON.stringify(transactionRow),
                datatype: "JSON",
                contentType: "application/json; charset=utf-8",
                success: function (returnData) {
                    if (returnData.ok) {
                        paymentsAdmin.services.accountHolder.lookup(transactionRow,
                            function () {
                                transactionArray.items.push(transactionRow);

                                renderUI();

                                $('.ui.dropdown.sourceItem').dropdown('clear');
                                $(".ui.dropdown.sourceVat").dropdown('clear');
                                $('#SourceItem_AccountReference').val("");
                                $('#SourceItem_Amount').val("");

                                $('.ui.dropdown.targetItem').dropdown('clear');
                                $(".ui.dropdown.targetVat").dropdown('clear');
                                $('#TransferItem_AccountReference').val("");
                                $('#TransferItem_Amount').val("");
                                $('#TransferItem_Narrative').val("");

                                $(messageContainerSelector).hide();
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
            $(messageContainerSelector).show();
        }
    }

    $(".add-source").click(function (e) {
        e.preventDefault();

        var transactionRow = {
            fundCode: $('.ui.dropdown.sourceItem').dropdown('get value'),
            fundName: $('.ui.dropdown.sourceItem').dropdown('get text'),
            vatCode: $(".ui.dropdown.sourceVat").dropdown('get value'),
            accountReference: $.trim($('#SourceItem_AccountReference').val()),
            amount: parseFloat($('#SourceItem_Amount').val()).toFixed(2),
        }

        addTransaction(transactionRow, Sources, validateSourceInput, '.source-message');
    });

    $(".add-transfer").click(function (e) {
        e.preventDefault();

        var transactionRow = {
            fundCode: $('.ui.dropdown.targetItem').dropdown('get value'),
            fundName: $('.ui.dropdown.targetItem').dropdown('get text'),
            vatCode: $(".ui.dropdown.targetVat").dropdown('get value'),
            accountReference: $.trim($('#TransferItem_AccountReference').val()),
            amount: parseFloat($('#TransferItem_Amount').val()).toFixed(2),
            narrative: $('#TransferItem_Narrative').val()
        }

        addTransaction(transactionRow, Transfers, validateTargetInput, '.transfer-message');
    });

    $(".submit-transfer").click(function (e) {

        e.preventDefault();

        if (Sources.items.length <= 0 || Transfers.items.length <= 0) {
            showSubmitError('There are no transfers to submit');
            return;
        }

        $.ajax({
            type: "POST",
            url: rootUrl + "/Transfer/SubmitTransfers",
            data: JSON.stringify({
                transferItems: Transfers.items,
                sourceItems: Sources.items
            }),
            datatype: "JSON",
            contentType: "application/json; charset=utf-8",
            success: function (returnData) {
                if (returnData.ok) {
                    window.location = rootUrl + "Transaction/Details/" + returnData.message;
                }
                else {
                    if (returnData.message != null && returnData.message.length > 0) {
                        showSubmitError(returnData.message);
                    }
                    else {
                        showSubmitError('An error occured whilst saving the transfer');
                    }
                }
            },
            failure: function () {
                showSubmitError('An error occured whilst saving the transfer');
            },
            error: function () {
                showSubmitError('An error occured whilst saving the journal');
            }

        });

    });

    $("#SourceItem_AccountReference, #SourceItem_FundCode").on("change",
        function () {
            var sourceItem = {
                fundCode: $('.ui.dropdown.sourceItem').dropdown('get value'),
                accountReference: $.trim($('#SourceItem_AccountReference').val())
            }

            paymentsAdmin.services.accountHolder.lookup(sourceItem,
                function () {
                    if (sourceItem.name) {
                        $("#SourceAccountName").html("<strong>Name</strong> " + sourceItem.name);
                        $("#SourceAccountBalance")
                            .html("<strong>Outs. Balance</strong> £" + sourceItem.outstandingBalance);
                    } else {
                        $("#SourceAccountName").html("");
                        $("#SourceAccountBalance").html("");
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

        renderUI();
    }

    function validateSourceInput(message) {
        var result = true;

        var html = [];

        html.push('<ul>');

        if (!$('#SourceItem_FundCode').val()) {
            html.push('<li>You must select a source fund type</li>');
            result = false;
        }

        if (!$('#SourceItem_AccountReference').val()) {
            html.push('<li>You must select a source account reference</li>');
            result = false;
        }

        if (!$('#SourceItem_VatCode').val()) {
            html.push('<li>You must select a source VAT code</li>');
            result = false;
        }

        if (!$('#SourceItem_Amount').val()) {
            html.push('<li>You must supply an amount</li>');
            result = false;
        }

        html.push('</ul>');

        if (result) {
            return '';
        }
        else {
            return html.join('');
        }
    }

    function validateTargetInput(message) {
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
            html.push('<li>You must select a target VAT code</li>');
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


    $("#transfer-table").on("click", ".remove-transfer",
        function (event) {
            clearError();
            removeTransfer($(this).attr('data-id'));

        });

    $("#source-table").on("click", ".remove-source",
        function (event) {
            clearError();
            removeSource($(this).attr('data-id'));

        });


    function removeSource(id) {

        Sources.items = Sources.items.filter(function (el) {
            return el.id != id;
        });

        renderUI();
    }

    function removeTransfer(id) {

        Transfers.items = Transfers.items.filter(function (el) {
            return el.id != id;
        });

        renderUI();
    }

    function renderUI() {
        renderSourceItems();
        renderTransferItems();
        markErrorRows();
        showRemainingBalance();
        disableCompleteTransfer();
    }

    function renderSourceItems() {
        $('#source-table-body').empty();

        if (Sources.items.length > 0) {
            $(Sources.items).each(function (index) {
                $('#source-table-body').append('<tr><td>' + this.fundName + '</td><td>' + this.accountReference + '</td><td>' + this.name + '</td><td>' +
                    (this.outstandingBalance ? this.outstandingBalance.toFixed(2) : "") + '</td><td>' + this.vatCode + '</td><td>' + this.amount + '</td><td><a href=\'#\' class=\'ui red button right floated remove-source\' data-id=\'' + this.id + '\'>Remove</a></td></tr>');
            });

            $('#source-table').show();
        }

        $('#amount-available-to-transfer').text("£" + totalAvailableToTransfer().toFixed(2));

        showRemainingBalance();
        disableCompleteTransfer();        
    }

    function renderTransferItems() {
        $('#transfer-table-body').empty();

        if (Transfers.items.length > 0) {
            $(Transfers.items).each(function (index) {
                $('#transfer-table-body').append('<tr><td>' + this.fundName + '</td><td>' + this.accountReference + '</td><td>' + this.name + '</td><td>' +
                    (this.outstandingBalance ? this.outstandingBalance.toFixed(2) : "") + '</td><td>' + this.vatCode + '</td><td>' + this.amount + '</td><td>' + this.narrative + '</td><td><a href=\'#\' class=\'ui red button right floated remove-transfer\' data-id=\'' + this.id + '\'>Remove</a></td></tr>');
            });

            $('#transfer-table').show();
        }

        $('#amount-available-to-transfer').text("£" + totalAvailableToTransfer().toFixed(2));

        showRemainingBalance();
        disableCompleteTransfer();        
    }

    function markErrorRows() {
        if (!multipleSourcesMatchTargets()) {
            if (Sources.items.length == Transfers.items.length) {
                for (i = 0; i < Sources.items.length; i++) {
                    if (Sources.items[i].amount != Transfers.items[i].amount) {
                        $("#source-table-body tr").eq(i).addClass("error");
                        $("#transfer-table-body tr").eq(i).addClass("error");
                    } else {
                        $("#source-table-body tr").eq(i).removeClass("error");
                        $("#transfer-table-body tr").eq(i).removeClass("error");
                    }
                }
            } else {
                $("tr.error").removeClass("error");
            }
        } else {
            $("tr.error").removeClass("error");
        }
    }

    function totalAvailableToTransfer() {
        var totalSourceAmount = _.sumBy(Sources.items, function (i) { return parseFloat(i.amount) });
        var totalTargetAmount = _.sumBy(Transfers.items, function (i) { return parseFloat(i.amount) });

        return _.round(totalSourceAmount - totalTargetAmount, 2);
    }

    function multipleSourcesMatchTargets() {
        if (Sources.items.length == 1) return true;
        if (Sources.items.length != Transfers.items.length) return false;

        return _.isEqual(_.map(Sources.items, "amount"), _.map(Transfers.items, "amount"));
    }

    function disableCompleteTransfer() {
        if (totalAvailableToTransfer() == 0 && _.sumBy(Transfers.items, function (i) { return parseFloat(i.amount) }) > 0 && Transfers.items.length > 0 && multipleSourcesMatchTargets()) {
            $(".submit-transfer").removeClass("disabled");
        } else {
            $(".submit-transfer").addClass("disabled");
        }
    }

    function showRemainingBalance() {
        if (totalAvailableToTransfer() != 0 && !isNaN(totalAvailableToTransfer())) {
            $(".remaining-balance").show();
        } else {
            $(".remaining-balance").hide();
        }
    }

});