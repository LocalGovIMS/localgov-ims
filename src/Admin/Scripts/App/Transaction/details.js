var transcationDetails = {
    mainPspReference: null,
    selectedTransactionReference: null,
    amount: 0,
    amountAvailableToTransfer: 0
};

//$(".refund").click(function () {
//    $("#refund-dialog").modal({
//        onApprove: function () {
//            return false;
//        },
//        onShow: function() {
//            $.validator.unobtrusive.parse($("#refund-dialog form"));
//        }
//    }).modal('show');
//});

$(".transfer").click(function () {

    transcationDetails.selectedTransactionReference = $(this).attr('data-id');
    transcationDetails.amount = parseFloat($(this).attr('data-amount'));
    transcationDetails.amountAvailableToTransfer = parseFloat($(this).attr('data-amount-available-to-transfer'));

    //$('#transfer-dialog').modal({
    //    onApprove: function () {
    //        return false;
    //    },
    //    onShow: function () {
    //        $.validator.unobtrusive.parse($("#transfer-dialog form"));
            $('#amount-available-to-transfer').text((transcationDetails.amountAvailableToTransfer).toFixed(2));
    //    }
    //}).modal('show');

});

$(".email-receipt").click(function () {

    transcationDetails.selectedTransactionReference = $(this).attr('data-id');

});