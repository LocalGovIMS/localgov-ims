loadModel();
highlightActiveRows();
renderCreditNotes();
renderJournals();

$(".clear-selected").on("click",
    function() {
        clearModel();
    });

function highlightActiveRows() {
    _.each($("tr"),
        function (item) {

            $(item).removeClass("table-primary");

            $(item).find(".form-check-input").each(function () {
                $(this).prop('checked', false);
            });
        });

    _.each(Model.suspenseItems,
        function (item) {
            var box = $(".form-check-input[data-id=" + item.id + "]");

            $(box).prop('checked', true);
            $(box).parents("tr").addClass("table-primary");
        });
}

function updateButtonValues() {
    $(".credit-button-value").text("(" + paymentsAdmin.core.toCurrency(totalCreditNotes()) + ")");
    $(".journal-button-value").text("(" + paymentsAdmin.core.toCurrency(totalAvailableToJournal()) + ")");
}

function updateUI() {
    highlightActiveRows();
    updateButtonValues();
}

$('tr .form-check-input').on("click", function (e) {

    var checkbox = $(this);
    if (!($(checkbox).data('value') > 0)) return;

    if (this.checked) {
        _.remove(Model.suspenseItems,
            function (n) {
                return n.id == $(checkbox).data('id');
            });
        Model.suspenseItems.push({
            id: $(checkbox).data('id'),
            amount: parseFloat($(checkbox).data('value'))
        });
        saveModel();
    } else {
        _.remove(Model.suspenseItems,
            function (n) {
                return n.id == $(checkbox).data('id');
            });
        saveModel();
    }
});

$("#journal").on("click", function () {
    $('#amount-available-to-journal').text(paymentsAdmin.core.toCurrency(remainingAvailableToJournal()));
});