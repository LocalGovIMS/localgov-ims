var totalAnalysisValue = 0;
var totalTransactionValue = 0;
var dirty = false;

calculateAnalysisValue();
calculateTransactionValue();
renderStatusColours();
allowSubmit();
bindToChangeEvents();

function bindToChangeEvents() {
    $(".analysis-value, .transaction-value, .wildcard-value, .bag-number").change(function () {
        refreshEReturnUI();
    });
}

function refreshEReturnUI() {
    calculateAnalysisValue();
    calculateTransactionValue();
    analysisValuesAreValid();
    renderStatusColours();
    validateWildcardValues();
    allowSubmit();
    dirty = true;
}

window.onbeforeunload = confirmExit;
function confirmExit() {
    if (dirty) {
        return "You have attempted to leave this page.  If you have made any changes to the fields without clicking the Save button, your changes will be lost.  Are you sure you want to exit this page?";
    }
}

$('.print').click(function () {
    if ($("#reprint").length > 0) { 
        printAnalysis();
    } else {
        if (isValidForPrint()) {
            printAnalysis();
        }
    }

    $('form').append('<input type="hidden" name="action" value="Submit" /> ');
    $('form').trigger('submit');

});

$('.save-button').click(function () {
    // We need this to postback in the correct mode: 'Edit'
    $('form').append('<input type="hidden" name="action" value="Edit" /> ');

    $('form').trigger('submit');
});

$('.approve-button').click(function () {
    // We need this to postback in the correct mode: 'Edit'
    $('form').append('<input type="hidden" name="action" value="Approve" /> ');

    $('form').trigger('submit');
});

$('form').submit(function () {  
    window.onbeforeunload = null;
});

function printAnalysis() {
    $('#nav-analysis-tab').click();
    window.print();
}

function transactionAndAnalysisValueMatch() {
    return totalTransactionValue > 0 && _.round(totalAnalysisValue, 2) == _.round(totalTransactionValue, 2);
}

$(".delete-ereturn").on("click",
    function (e) {

        var status = $('#EReturn_EReturn_StatusId').val();
        var type = "delete";
        if (status == "1" || status == "2")
        {
            type = "void";
        }

        if (window.confirm("Are you sure you want to " + type + " this eReturn?")) {

        } else {
            e.preventDefault();
            e.stopPropogation();
            return false;
        }
    });

$(".add-cheque").on("click",
    function () {
        var nextIndex = $("#cheque-rows tr").length;
        $("#cheque-rows").append("<tr><td><input name=\"Cheques[" + nextIndex + "].Name\" type=\"text\" class=\"form-control\" maxlength=\"100\" /></td>" +
            "<td><input name=\"Cheques[" + nextIndex + "].Amount\" type=\"text\" class=\"analysis-value monetary-value form-control\" /></td>" +
            "<td class=\"text-end cheque-actions\" ><a class=\"btn btn-danger remove-cheque\" data-remove=\"" + nextIndex + "\">Remove<i class=\"ui trash icon\"></i></a></td>" +
            "</tr>");
        bindToChangeEvents();
        refreshEReturnUI();
    });

$("#cheque-rows").on("click", ".remove-cheque",
    function () {
        var remove = $(this).data("remove");
        $($("#cheque-rows tr")[remove]).remove();
        rebuildChequeIndexes();
        bindToChangeEvents();
        refreshEReturnUI();
    });

function rebuildChequeIndexes() {
    $("#cheque-rows tr").each(function (index) {
        var prefix = "Cheques[" + index + "]";
        $(this).find("input").each(function () {
            this.name = this.name.replace(/Cheques\[\d+\]/, prefix);            
        });
        $(this).find(".button").data("remove", index);
    });
}

function calculateTransactionValue() {
    totalTransactionValue = 0;
    $('.transaction-value').each(function () {
        totalTransactionValue += Number($(this).val());
    });
    $('.transaction-total').text('£' + totalTransactionValue.toFixed(2));
}

function calculateAnalysisValue() {
    totalAnalysisValue = 0;
    $('.analysis-value').each(function () {
        totalAnalysisValue += Number($(this).val());
    });
    $('.analysis-total').text('£' + totalAnalysisValue.toFixed(2));
}

function validateWildcardValues() {
    var isValid = true;
    $(".wildcard-value").each(function () {
        if (($(this).val().indexOf('*') >= 0
            || $(this).val().length != $(this).data("minlength"))
            && $("#Transactions_" + $(this).data('index') + "__Amount").val() > 0) {
            isValid = false;
            $(this).addClass("error");
        } else {
            $("#Transactions_" + $(this).data('index') + "__Reference").val($(this).data("prefix") + $(this).val());
            $(this).removeClass("error");
        }
    });
    return isValid;
}

function analysisValuesAreValid() {
    var isValid = true;
    $('.analysis-value[data-step]').each(function (index) {
        var value = _.round($(this).val()*100);
        var step = _.round($(this).data('step')*100);

        if (value > 0 && step > 0) {

            if (value % step != 0) {
                $(this).addClass("error");
                isValid = false;
            } else {
                $(this).removeClass("error");
            }
        } else {
            $(this).removeClass("error");
        }
    });

    console.log('analysisValuesAreValid', isValid)

    return isValid;
}

function bagNumberPresentIfRequired() {
    if ($(".bag-number").length) {
        if ($(".bag-number").val().length > 0) {
            return true;
        } else {
            return false;
        }
    }
    return true;
}

function isValidForPrint() {
    return transactionAndAnalysisValueMatch() &&
        analysisValuesAreValid() &&
        validateWildcardValues() &&
        bagNumberPresentIfRequired();
}

function allowSubmit() {
    if (transactionAndAnalysisValueMatch()
        && analysisValuesAreValid()
        && validateWildcardValues()
        //&& bagNumberPresentIfRequired() // TODO: I've left this in for now, and the method it calls, incase we wish to reintroduce it
    ) {
        $(".submit-button").removeAttr("disabled");
    } else {
        $(".submit-button").attr("disabled", "disabled");
    }
}

function renderStatusColours() {
    if (transactionAndAnalysisValueMatch() && analysisValuesAreValid()) {
        $('.transaction-total').addClass("text-bg-success");
        $('.analysis-total').addClass("text-bg-success");
        $('.transaction-total').removeClass("text-bg-warning");
        $('.analysis-total').removeClass("text-bg-warning");
        $('.transaction-total').removeClass("text-bg-danger");
        $('.analysis-total').removeClass("text-bg-danger");
    } else if (transactionAndAnalysisValueMatch() && !analysisValuesAreValid()) {
        $('.transaction-total').addClass("text-bg-success");
        $('.analysis-total').removeClass("text-bg-success");
        $('.transaction-total').removeClass("text-bg-warning");
        $('.analysis-total').addClass("text-bg-warning");
        $('.transaction-total').removeClass("text-bg-danger");
        $('.analysis-total').removeClass("text-bg-danger");
    } else {
        $('.transaction-total').removeClass("text-bg-success");
        $('.analysis-total').removeClass("text-bg-success");
        $('.transaction-total').removeClass("text-bg-warning");
        $('.analysis-total').removeClass("text-bg-warning");
        $('.transaction-total').addClass("text-bg-danger");
        $('.analysis-total').addClass("text-bg-danger");
    }

}