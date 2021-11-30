$(document).ready(function () {

    $(".credit-notes").click(function () {

        $("#credit-dialog").modal({
            onApprove: function () {
                return false;
            },
            onShow: function () {
                $.validator.unobtrusive.parse($("#credit-dialog form"));
                $('#amount-credit').text(parseFloat(totalCreditNotes() || 0).toFixed(2));
                renderCreditNotes();
            }
        }).modal("show");

    });

    $(".cancel-credit").click(function (e) {
        clearCreditForm();
    });

    $('.js-credit-fund').dropdown({
        onChange: function (e) {
            setTimeout(function () {
                if ($('.js-credit-fund .selected').data('vat-override') === "True") {
                    $('.js-credit-vat').removeClass('disabled');
                    $('.js-credit-vat').dropdown("clear");
                    $('.js-credit-vat .search').prop('disabled', false);
                } else {
                    $('.js-credit-vat').removeClass('disabled');
                    $('.js-credit-vat').dropdown("set selected", $(".js-credit-fund .selected").data("vat-default-code"));
                    $('.js-credit-vat').addClass('disabled');
                    $('.js-credit-vat .search').prop('disabled', true);
                }
            },
                50
            );
        }
    });

    $(".add-credit").click(function (e) {

        e.preventDefault();

        var text = $(".credit-message-text");
        var message = validateInput();

        if (message.length === 0) {

            var id = parseInt(0);

            if (Model.creditNotes.length > 0) {
                id = parseInt(Model.creditNotes[Model.creditNotes.length - 1].id) + parseInt(1);
            }

            var creditItem = {
                id: id,
                fundCode: $(".ui.dropdown.js-credit-fund").dropdown("get value"),
                fundName: $(".ui.dropdown.js-credit-fund").dropdown("get text"),
                vatCode: $(".js-credit-vat").dropdown("get value"),
                accountReference: $.trim($("#CreditNote_AccountReference").val()),
                amount: parseFloat($("#CreditNote_Amount").val())
            }            

            $.ajax({
                type: "POST",
                url: rootUrl + "/Validation/ValidateTransferItem",
                data: JSON.stringify(creditItem),
                datatype: "JSON",
                contentType: "application/json; charset=utf-8",
                success: function (returnData) {
                    if (returnData.ok) {
                        Model.creditNotes.push(creditItem);

                        renderCreditNotes();

                        $(".credit-message").hide();
                        $("#credit-dialog").modal("refresh");
                    }
                    else {
                        if (returnData.message.length > 0) {
                            showCreditSubmitError(returnData.message);
                        }
                        else {
                            showCreditSubmitError("An unknown error occured whilst saving the credit");
                        }
                    }
                },
                failure: function () {
                    showCreditSubmitError("An unknown error occured whilst saving the credit");
                },
                error: function () {
                    showCreditSubmitError("An unknown error occured whilst saving the credit");
                }

            });
        }
        else {
            text.empty();
            text.append(message);
            $(".credit-message").show();
        }

    });

    $(".submit-credit").click(function (e) {

        e.preventDefault();

        saveModel();

        $("#credit-dialog").modal("hide");                

    });

    function clearCreditError() {
        $(".credit-error").empty();
        $(".credit-error").hide();
    }

    function clearCreditForm() {
        //Model.creditNotes = [];

        $("#CreditNote_AccountReference").val("");
        $("#CreditNote_Amount").val("");

        //var text = $('.credit-message-text');
        //text.empty();
        $(".credit-message").hide();

        renderCreditNotes();
    }

    function validateInput(message) {
        var result = true;

        var html = [];

        html.push("<ul>");

        if (!$("#CreditNote_FundCode").val()) {
            html.push("<li>You must select a fund code</li>");
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

    function showCreditSubmitError(message) {

        var text = $(".credit-message-text");

        var html = [];

        html.push("<ul>");
        html.push("<li>" + message + "</li>");
        html.push("</ul>");

        text.empty();
        text.append(html.join(""));

        $(".credit-message").show();
        $("#credit-dialog").modal("refresh");
    }

    function unBindClickEvent() {
        $("#credit-table tbody tr td").unbind()
    }

    function bindClickEvent() {
        $("#credit-table tbody tr td").on("click", ".remove-credit", function (event) {
            clearCreditError();
            removeCredit($(this).attr("data-id"))
        });
    }

    function removeCredit(id) {

        Model.creditNotes = Model.creditNotes.filter(function (el) {
            return parseInt(el.id) !== parseInt(id);
        });

        renderCreditNotes();
    }

    function renderCreditNotes() {
        unBindClickEvent();

        $("#credit-table-body").empty();

        if (Model.creditNotes.length > 0) {
            $(Model.creditNotes).each(function (index) {
                $("#credit-table-body").append("<tr><td>" + this.fundName + "</td><td>" + this.accountReference + "</td><td>" + this.vatCode + "</td><td>" + this.amount.toFixed(2) + "</td><td><a href='#' class='ui red button right floated remove-credit' data-id='" + this.id + "'>Remove</a></td></tr>");
            });

            $("#credit-table").show();
        }

        $("#amount-available-to-credit").text(totalCreditNotes().toFixed(2));       

        bindClickEvent();
    }


});