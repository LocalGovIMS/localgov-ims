$(document).ready(function () {

    var sourceFundCode;
    var sourceVatCode;
    var transferFundCode;
    var transferVatCode;

    var Sources = { items: [] };
    var Transfers = { items: [] };

    disableCompleteTransfer();

    sourceVatCode = accessibleAutocomplete.enhanceSelectElement({
        displayMenu: 'overlay',
        autoSelect: false,
        confirmOnBlur: false,
        showAllValues: true,
        defaultValue: '',
        preserveNullOptions: true,
        placeholder: 'Select VAT code',
        dropdownArrow: () => paymentsAdmin.controls.autocomplete.downarrow,
        selectElement: document.querySelector('#SourceItem_VatCode')
    });

    sourceFundCode = accessibleAutocomplete.enhanceSelectElement({
        displayMenu: 'overlay',
        autoSelect: false,
        confirmOnBlur: false,
        showAllValues: true,
        defaultValue: '',
        preserveNullOptions: true,
        placeholder: 'Select fund type',
        dropdownArrow: () => paymentsAdmin.controls.autocomplete.downarrow,
        selectElement: document.querySelector('#SourceItem_FundCode'),
        onConfirm: (val) => {
            handleFundTypeChange('SourceItem', val, sourceVatCode);
        }
    });

    transferVatCode = accessibleAutocomplete.enhanceSelectElement({
        displayMenu: 'overlay',
        autoSelect: false,
        confirmOnBlur: false,
        showAllValues: true,
        defaultValue: '',
        preserveNullOptions: true,
        placeholder: 'Select VAT code',
        dropdownArrow: () => paymentsAdmin.controls.autocomplete.downarrow,
        selectElement: document.querySelector('#TransferItem_VatCode')
    });

    transferFundCode = accessibleAutocomplete.enhanceSelectElement({
        displayMenu: 'overlay',
        autoSelect: false,
        confirmOnBlur: false,
        showAllValues: true,
        defaultValue: '',
        preserveNullOptions: true,
        placeholder: 'Select fund type',
        dropdownArrow: () => paymentsAdmin.controls.autocomplete.downarrow,
        selectElement: document.querySelector('#TransferItem_FundCode'),
        onConfirm: (val) => {
            handleFundTypeChange('TransferItem', val, transferVatCode);
        }
    });

    $('#SourceItem_FundCode').attr('aria-labelledby', 'SourceItemFundCode');
    $('#SourceItem_VatCode').attr('aria-labelledby', 'SourceItemVatCode');
    $('#TransferItem_FundCode').attr('aria-labelledby', 'TransferItemFundCode');
    $('#TransferItem_VatCode').attr('aria-labelledby', 'TransferItemVatCode');

    $(".cancel-transfer").click(function (e) {
        clearForm();
    });

    $("#SourceItem_Amount").on("keyup change blur",
        function () {
            renderUI();
        });

    addTransaction = function (transactionRow, transactionArray, validator, messageContainerSelector, selectorPrefix) {

        clearError();

        var text = $(messageContainerSelector + '-text');
        var message = validator(selectorPrefix);

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

                    console.log(returnData);

                    if (returnData.ok) {
                        paymentsAdmin.services.accountHolder.lookup(transactionRow,
                            function () {
                                transactionArray.items.push(transactionRow);

                                clearError();
                                renderUI();

                                $('#SourceItem_AccountReference').val("");
                                $('#SourceItem_Amount').val("");

                                $('#TransferItem_AccountReference').val("");
                                $('#TransferItem_Amount').val("");
                                $('#TransferItem_Narrative').val("");

                            });
                    }
                    else {
                        if (returnData.message.length > 0) {
                            showError(returnData.message, messageContainerSelector);
                        }
                        else {
                            showError('An unknown error occured whilst saving the transfer', messageContainerSelector);
                        }
                    }
                },
                failure: function () {
                    showError('An unknown error occured whilst saving the transfer', messageContainerSelector);
                },
                error: function () {
                    showError('An unknown error occured whilst saving the transfer', messageContainerSelector);
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

        var fundCodeOption = paymentsAdmin.core.accessibleAutoComplete.getSelectedOptionByValue('#SourceItem_FundCode', $('#SourceItem_FundCode-select').val());
        var vatCodeOption = paymentsAdmin.core.accessibleAutoComplete.getSelectedOptionByValue('#SourceItem_VatCode', $('#SourceItem_VatCode-select').val());

        var transactionRow = {
            fundCode: fundCodeOption.value,
            fundName: fundCodeOption.text,
            vatCode: vatCodeOption.value,
            accountReference: $.trim($('#SourceItem_AccountReference').val()),
            amount: parseFloat($('#SourceItem_Amount').val()).toFixed(2),
        }

        addTransaction(transactionRow, Sources, validateInput, '.source-message', 'SourceItem');
    });

    $(".add-transfer").click(function (e) {
        e.preventDefault();

        var fundCodeOption = paymentsAdmin.core.accessibleAutoComplete.getSelectedOptionByValue('#TransferItem_FundCode', $('#TransferItem_FundCode-select').val());
        var vatCodeOption = paymentsAdmin.core.accessibleAutoComplete.getSelectedOptionByValue('#TransferItem_VatCode', $('#TransferItem_VatCode-select').val());

        var transactionRow = {
            fundCode: fundCodeOption.value,
            fundName: fundCodeOption.text,
            vatCode: vatCodeOption.value,
            accountReference: $.trim($('#TransferItem_AccountReference').val()),
            amount: parseFloat($('#TransferItem_Amount').val()).toFixed(2),
            narrative: $('#TransferItem_Narrative').val()
        }

        addTransaction(transactionRow, Transfers, validateInput, '.transfer-message', 'TransferItem');
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

        $(".source-message-header").empty();
        $(".source-message-header").text('Unable to add the source transaction');
        $(".source-message-text").empty();
        $(".source-message").hide();

        $(".transfer-message-header").empty();
        $(".transfer-message-header").text('Unable to add the transfer transaction');
        $(".transfer-message-text").empty();
        $(".transfer-message").hide();
    }

    function clearForm() {
        Transfers = { items: [] };

        $('#TransferItem_AccountReference').val('');
        $('#TransferItem_Amount').val('');

        $('.source-message').hide();
        $('.transfer-message').hide();

        renderUI();
    }

    function validateInput(selectorPrefix) {
        var result = true;

        var html = [];

        html.push('<ul class=\"mb-0\">');

        if (!$('#' + selectorPrefix + '_FundCode-select').val()) {
            html.push('<li>You must select a fund type</li>');
            result = false;
        }

        if (!$('#' + selectorPrefix + '_AccountReference').val()) {
            html.push('<li>You must supply an account reference</li>');
            result = false;
        }

        if (!$('#' + selectorPrefix + '_VatCode-select').val()) {
            html.push('<li>You must select a VAT code</li>');
            result = false;
        }

        if (!$('#' + selectorPrefix + '_Amount').val()) {
            html.push('<li>You must supply an amount</li>');
            result = false;
        }

        if (result) {
            if (!$.isNumeric($('#' + selectorPrefix + '_Amount').val())) {
                html.push('<li>The amount supplied is not valid</li>');
                result = false;
            }
        }

        if (result) {
            if (parseFloat($('#' + selectorPrefix + '_Amount').val()) <= 0) {
                html.push('<li>The amount to journal must be a positive number</li>');
                result = false;
            }
        }

        if (result) {
            if (parseFloat($('#' + selectorPrefix + '_AccountReference').val().length) > 30) {
                html.push('<li>The account reference is too long, it can be a maximum of 30 characters</li>');
                result = false;
            }
        }

        if (result) {
            if ($('#' + selectorPrefix + '_Narrative').length > 0) { // This control only exists in the TransferItem section, so see if it exists before checking it.
                if (parseFloat($('#' + selectorPrefix + '_Narrative').val().length) > 50) {
                    html.push('<li>The narrative is too long, it can be a maximum of 50 characters</li>');
                    result = false;
                }
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

        html.push('<ul class=\"mb-0\">');
        html.push('<li>' + message + '</li>');
        html.push('</ul>');

        text.empty();
        text.append(html.join(''));

        $('.transfer-message').show();
        $("#transfer-dialog").modal('refresh');
    }

    function showError(message, selector) {

        var text = $(selector + '-text');

        var html = [];

        html.push('<ul class=\"mb-0\">');
        html.push('<li>' + message + '</li>');
        html.push('</ul>');

        text.empty();
        text.append(html.join(''));

        $(selector).show();
        $("#transfer-dialog").modal('refresh');
    }

    function showErrorWithHeader(title, message, selector) {

        var header = $(selector + '-header');

        header.empty();
        header.append(title);

        showError(message, selector);
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
        showRemainingBalance();
        markErrorRows();
        disableCompleteTransfer();
    }

    function renderSourceItems() {
        $('#source-table-body').empty();

        if (Sources.items.length > 0) {
            $(Sources.items).each(function (index) {
                $('#source-table-body').append('<tr><td>' + this.fundName + '</td><td>' + this.accountReference + '</td><td>' + this.name + '</td><td class=\"text-end\">' +
                    (this.outstandingBalance ? this.outstandingBalance.toFixed(2) : "") + '</td><td>' + this.vatCode + '</td><td class=\"text-end\">' + this.amount + '</td><td class=\"text-end\"><a href=\'#\' class=\'btn btn-danger remove-source\' data-id=\'' + this.id + '\'>Remove</a></td></tr>');
            });

            $('#source-table').show();
        }

        $('#amount-available-to-transfer').text("£" + totalAvailableToTransfer().toFixed(2));
    }

    function renderTransferItems() {
        $('#transfer-table-body').empty();

        if (Transfers.items.length > 0) {
            $(Transfers.items).each(function (index) {
                $('#transfer-table-body').append('<tr><td>' + this.fundName + '</td><td>' + this.accountReference + '</td><td>' + this.name + '</td><td class=\"text-end\">' +
                    (this.outstandingBalance ? this.outstandingBalance.toFixed(2) : "") + '</td><td>' + this.vatCode + '</td><td class=\"text-end\">' + this.amount + '</td><td>' + this.narrative + '</td><td class=\"text-end\"><a href=\'#\' class=\'btn btn-danger remove-transfer\' data-id=\'' + this.id + '\'>Remove</a></td></tr>');
            });

            $('#transfer-table').show();
        }

        $('#amount-available-to-transfer').text("£" + totalAvailableToTransfer().toFixed(2));
    }

    function markErrorRows() {

        if (multipleSourcesMatchTargets()) {
            $("tr.table-danger").removeClass("table-danger");
            $("tr.table-danger").removeAttr("aria-invalid");
            $("tr.table-danger").removeAttr("aria-errormessage");
            return;
        }

        if (Sources.items.length != Transfers.items.length) {
            $("tr.table-danger").removeClass("table-danger");
            $("tr.table-danger").removeAttr("aria-invalid");
            $("tr.table-danger").removeAttr("aria-errormessage");
            return;
        }

        for (i = 0; i < Sources.items.length; i++) {
            if (Sources.items[i].amount != Transfers.items[i].amount) {
                $("#source-table-body tr").eq(i).addClass("table-danger");
                $("#source-table-body tr").eq(i).attr("aria-invalid", "true");
                $("#source-table-body tr").eq(i).attr("aria-errormessage", "source-message-text");

                $("#transfer-table-body tr").eq(i).addClass("table-danger");
                $("#transfer-table-body tr").eq(i).attr("aria-invalid", "true");
                $("#transfer-table-body tr").eq(i).attr("aria-errormessage", "transfer-message-text");

                showErrorWithHeader('Source and transfers don\'t match', 'Each source transaction must have a matching target transaction', '.source-message');
                showErrorWithHeader('Transfers and source don\'t match', 'Each target transaction must have a matching source transaction', '.transfer-message');

            } else {
                $("#source-table-body tr").eq(i).removeClass("table-danger");
                $("#source-table-body tr").eq(i).removeAttr("aria-invalid");
                $("#source-table-body tr").eq(i).removeAttr("aria-errormessage");

                $("#transfer-table-body tr").eq(i).removeClass("table-danger");
                $("#transfer-table-body tr").eq(i).removeAttr("aria-invalid");
                $("#transfer-table-body tr").eq(i).removeAttr("aria-errormessage");
            }
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

        if (totalAvailableToTransfer() == 0
            && _.sumBy(Transfers.items, function (i) { return parseFloat(i.amount) }) > 0
            && Transfers.items.length > 0
            && multipleSourcesMatchTargets()) {
            console.log('enabling complete button');
            $(".submit-transfer").removeAttr('disabled');
        } else {
            console.log('disabling complete button');
            $(".submit-transfer").attr('disabled', 'disabled');
        }

    }

    function showRemainingBalance() {
        if (totalAvailableToTransfer() != 0 && !isNaN(totalAvailableToTransfer())) {
            $(".remaining-balance").show();
        } else {
            $(".remaining-balance").hide();
        }
    }

    function handleFundTypeChange(querySelectorPrefix, val, vatCodeDropdown) {

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
});

