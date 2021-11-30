
$(function() {
    if ($('input[type="date"]').prop("type") !== "date") {
        $('input[type="date"]').datepicker({
            dateFormat: "yy-mm-dd",
            altFormat: "yy-mm-dd",
            changeMonth: true,
            changeYear: true
        });
    }
});