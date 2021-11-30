loadModel();
highlightActiveRows();


$(".clear-selected").on("click",
    function () {
        clearModel();
    });

function highlightActiveRows() {
    _.each($("tr"),
        function (item) {
            $("tr .ui.checkbox").checkbox("uncheck");
            $(item).removeClass("selected");
        });
    _.each(Model.eReturns,
        function (item) {
            var box = $(".ui.checkbox[data-id=" + item.id + "]");
            $(box).checkbox("check");
            $(box).parents("tr").addClass("selected");
        });
}

function updateButtonValues() {
    $(".approve-button-value").text("(£" + totalEReturns().toFixed(2) + ")");    
}

function updateUI() {
    highlightActiveRows();
    updateButtonValues();
}

$('tr .ui.checkbox').on("click", function (e) {
    var checkbox = $(this);
    $(this).find(".ui.checkbox").checkbox("toggle");    
    if (!($(checkbox).data('value') > 0)) return;

    if ($(checkbox).checkbox("is checked")) {
        $(this).addClass("selected");
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
        $(this).removeClass("selected");
        _.remove(Model.eReturns,
            function (n) {
                return n.id == $(checkbox).data('id');
            });
        saveModel();
    }
});

$('.selectable tr .ui.button').click(function (event) {
    event.stopPropagation();
});

$(function () {
    if ($('input[type="date"]').prop("type") !== "date") {
        $('input[type="date"]').datepicker({
            dateFormat: "yy-mm-dd",
            altFormat: "yy-mm-dd",
            changeMonth: true,
            changeYear: true
        });
    }
});




