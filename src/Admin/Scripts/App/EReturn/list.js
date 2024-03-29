﻿loadModel();
highlightActiveRows();

function highlightActiveRows() {

    _.each($("tr"),
        function (item) {

            $(item).removeClass("table-primary");

            $(item).find(".form-check-input").each(function () {
                $(this).prop('checked', false);
            });
        });

    _.each(Model.eReturns,
        function (item) {
            var box = $(".form-check-input[data-id=" + item.id + "]");

            $(box).prop('checked', true);
            $(box).parents("tr").addClass("table-primary");
        });
}

function updateButtonValues() {
    $(".approve-button-value").text("(" + paymentsAdmin.core.toCurrency(totalEReturns()) + ")");
}

function updateUI() {
    highlightActiveRows();
    updateButtonValues();
}

$('tr .form-check-input').on("click", function (e) {

    var checkbox = $(this);
    if (!($(checkbox).data('value') > 0)) return;

    if (this.checked) {

        _.remove(Model.eReturns,
            function (n) {
                return n.id == $(checkbox).data('id');
            });
        Model.eReturns.push({
            id: $(checkbox).data('id'),
            num: $(checkbox).data('num'),
            template: $(checkbox).data('template'),
            submittedBy: $(checkbox).data('submittedby'),
            type: $(checkbox).data('type'),
            amount: parseFloat($(checkbox).data('value'))
        });
        saveModel();
    } else {
        _.remove(Model.eReturns,
            function (n) {
                return n.id == $(checkbox).data('id');
            });
        saveModel();
    }
});