loadModel();
highlightActiveRows();


$(".clear-selected").on("click",
    function() {
        clearModel();
    });

function highlightActiveRows() {
    _.each($("tr"),
        function(item) {
            $("tr .ui.checkbox").checkbox("uncheck");
            $(item).removeClass("selected");
        });
    _.each(Model.suspenseItems,
        function(item) {
            var box = $(".ui.checkbox[data-id=" + item.id + "]");
            $(box).checkbox("check");
            $(box).parents("tr").addClass("selected");
        });
}

function updateButtonValues() {
    $(".credit-button-value").text("(£" + totalCreditNotes().toFixed(2) + ")");
    $(".journal-button-value").text("(£" + totalAvailableToTransfer().toFixed(2) + ")");
}

function updateUI() {
    highlightActiveRows();
    updateButtonValues();
}

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



$('tr .ui.checkbox').on("click", function (e) {
    var checkbox = $(this);
    if (!($(checkbox).data('value') > 0)) return;

    if ($(checkbox).checkbox("is checked")) {
        $(checkbox).parent("tr").addClass("selected");
        /*
         // Allow multi select
         _.remove(Model.suspenseItems,
            function (n) {
                return n.id == $(checkbox).data('id');
            });
        */
        //  Reset selection
        Model.suspenseItems = [];
        Model.suspenseItems.push({
            id: $(checkbox).data('id'),
            amount: parseFloat($(checkbox).data('value'))
        });        
        saveModel();
    } else {
        $(checkbox).parent("tr").removeClass("selected");
        _.remove(Model.suspenseItems,
            function(n) {
                return n.id == $(checkbox).data('id');
            });
        saveModel();
    }    
});

$('tr .ui.button').click(function(event) {
    event.stopPropagation();    
});

$(window).on('scroll', function() {
    if ($(window).scrollTop() >= 70) {
        $(".page-actions--fix").addClass("page-actions--fixed");
    } else {
        $(".page-actions--fix").removeClass("page-actions--fixed");
    }
})