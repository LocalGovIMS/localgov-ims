$(document).ready(function () {    

    var id = 0;

    $(".note").click(function () {

        $('#note-dialog').modal({
            onApprove: function () {
                return false;
            },
        }).modal('show');

        id = $(this).attr("data-id");
        console.log($(this).attr("data-note"));
        $('#Note').val($(this).attr("data-note"));

    });

    $(".cancel-note").click(function (e) {
        clearForm();
    });

    $(".save-note").click(function (e) {

        e.preventDefault();

        $.ajax({
            type: "POST",
            url: rootUrl + "/Suspense/SaveNote",
            data: JSON.stringify({
                id: id,
                note: $('#Note').val()
            }),
            datatype: "JSON",
            contentType: "application/json; charset=utf-8",
            success: function (returnData) {
                if (returnData.ok) {
                    clearForm();
                    // TODO: Update data-notes
                    $("#note-dialog").modal('hide');
                    location.reload();
                }
                else {
                    if (returnData.message.length > 0) {
                        showSubmitError(returnData.message);
                    }
                    else {

                        showSubmitError('An unknown error occured whilst saving the notes');
                    }
                }
            },
            failure: function () {
                showSubmitError('An unknown error occured whilst saving the notes');
            },
            error: function () {
                showSubmitError('An unknown error occured whilst saving the notes');
            }

        });

    });

    function clearError() {
        $(".note-error").empty();
        $(".note-error").hide();
        id = 0;
    }   

    function showSubmitError(message) {

        var text = $('.note-message-text');

        var html = [];

        html.push('<ul>');
        html.push('<li>' + message + '</li>');
        html.push('</ul>');

        text.empty();
        text.append(html.join(''));

        $('.note-message').show();
        $("#note-dialog").modal('refresh');
    }

    function clearForm() {
        $('#Note').val('');
    }

});