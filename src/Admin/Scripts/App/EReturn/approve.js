$(document).ready(function () {    

    $(".approve").click(function () {        
        $('#approve-dialog').modal({
            onApprove: function () {
                return false;
            },
            onShow: function () {                
                $('#amount-to-approve').text(parseFloat(totalEReturns() || 0).toFixed(2));
                clearError();
                renderApproveItems();
            }
        }).modal('show');

    });    
   

    $(".submit-approve").click(function (e) {

        e.preventDefault();

        if (Model.eReturns.length <= 0) {
            showSubmitError('There are no eReturns selected to approve');
            return;
        }

        $.ajax({
            type: "POST",
            url: rootUrl + "/EReturn/Authorise",
            data: JSON.stringify({                
                items: _.map(Model.eReturns, "id"),                
            }),
            datatype: "JSON",
            contentType: "application/json; charset=utf-8",
            success: function (returnData) {
                if (returnData.ok) {                    
                    clearModel();
                    clearError();
                    $("#approve-dialog").modal('hide');
                    location.reload();
                }
                else {
                    if (returnData.message.length > 0) {
                        showSubmitError(returnData.message);
                    }
                    else {

                        showSubmitError('An unknown error occured whilst saving the approval');
                    }
                }
            },
            failure: function () {
                showSubmitError('An unknown error occured whilst saving the approval');
            },
            error: function () {
                showSubmitError('An unknown error occured whilst saving the approval');
            }

        });

    });

    $("#approve-table-body").on("click",
        ".remove-ereturn",
        function() {
            removeEReturn($(this).data("id"));
            renderApproveItems();
        });

    function clearError() {
        $(".approve-message-text").empty();
        $(".approve-message").hide();
    }

    function showSubmitError(message) {

        var text = $('.approve-message-text');

        var html = [];

        html.push('<ul>');
        html.push('<li>' + message + '</li>');
        html.push('</ul>');

        text.empty();
        text.append(html.join(''));

        $('.approve-message').show();
        $("#approve-dialog").modal('refresh');
    }

    function renderApproveItems() {        

        $('#approve-table-body').empty();

        if (Model.eReturns.length > 0) {
            $(Model.eReturns).each(function (index) {
                $('#approve-table-body').append('<tr><td>' + this.num + '</td><td>' + this.template + '</td><td>' + this.submittedBy + '</td><td>' + this.type + '</td><td style="text-align:right;">' + this.amount.toFixed(2) + '</td><td><a href=\'#\' class=\'ui red button right floated remove-ereturn\' data-id=\'' + this.id + '\'>Remove</a></td></tr>');
            });

            $('#amount-to-approve').text(parseFloat(totalEReturns() || 0).toFixed(2));

            $('#approve-table').show();
        }
 
    }

});