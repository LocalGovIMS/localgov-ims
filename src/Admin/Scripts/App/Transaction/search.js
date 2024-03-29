﻿$(".search").click(function (e) {

    if ($('#WildSearchAccountReference').is(':checked')) {

        var message = validateSearchInput();
        if (message.length === 0) {
            $('.source-message').hide();
        }
        else {
            $('.source-message').empty();
            $('.source-message').append(message);
            $('.source-message').show();
            e.preventDefault();
        }
    }
});

function validateSearchInput() {
    var result = true;

    var html = [];
    var startdate = undefined;
    var enddate = undefined;
    html.push('<ul class=\"mb-3\">');

    if (!$('#StartDate').val()) {
        html.push('<li>You must select a start date</li>');
        result = false;
    }
    else {
        startdate = new Date($('#StartDate').val());
    }

    if (!$('#EndDate').val()) {
        html.push('<li>You must select an end date</li>');
        result = false;
    } else {
        enddate = new Date($('#EndDate').val());
    }

    if (typeof startdate !== 'undefined' && typeof enddate !== 'undefined') {

        if (startdate !== null && enddate !== null) {
            if (startdate > enddate) {
                html.push('<li>Start date must be before end date</li>');
                result = false;
            }
            var difference = Math.floor((enddate - startdate) / (1000 * 60 * 60 * 24));
            if (difference > 31) {
                html.push('<li>Only 1 month date range allowed for part account reference searches</li>');
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