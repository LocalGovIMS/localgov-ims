var totalAnalysisValue = 0;
var totalTransactionValue = 0;
var dirty = false;

$('.ui.dropdown').dropdown({});
$('.ui.fluid.two.item.menu .item').tab();

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
});

$('form').submit(function () {    
    window.onbeforeunload = null;
});

function printAnalysis() {
    $.tab('change tab', 'analysis-tab');
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
        $("#cheque-rows").append("<tr><td><input name=\"Cheques[" + nextIndex + "].Name\" type=\"text\" maxlength=\"100\" /></td>" +
            "<td><input name=\"Cheques[" + nextIndex + "].Amount\" type=\"text\" class=\"analysis-value monetary-value\" /></td>" +
            "<td><a class=\"ui red icon button remove-cheque\" data-remove=\"" + nextIndex + "\"><i class=\"ui trash icon\"></i></a></td>" +
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
        $(".submit-button").removeClass("disabled");
    } else {
        $(".submit-button").addClass("disabled");
    }
}

function renderStatusColours() {
    if (transactionAndAnalysisValueMatch() && analysisValuesAreValid()) {
        $('.transaction-total').addClass("teal");
        $('.analysis-total').addClass("teal");
        $('.transaction-total').removeClass("red");
        $('.analysis-total').removeClass("orange");
        $('.analysis-total').removeClass("red");
    } else if (transactionAndAnalysisValueMatch() && !analysisValuesAreValid()) {
        $('.transaction-total').removeClass("teal");
        $('.analysis-total').removeClass("teal");
        $('.transaction-total').addClass("teal");
        $('.analysis-total').addClass("orange");
    } else {
        $('.transaction-total').removeClass("teal");
        $('.analysis-total').removeClass("orange");
        $('.analysis-total').removeClass("teal");
        $('.transaction-total').addClass("red");
        $('.analysis-total').addClass("red");
    }

}