var transcationDetails = {
    mainPspReference: null,
    selectedTransactionReference: null,
    amount: 0,
    amountAvailableToTransfer: 0
};

$(".transfer").click(function () {

    transcationDetails.selectedTransactionReference = $(this).attr('data-id');
    transcationDetails.amount = parseFloat($(this).attr('data-amount'));
    transcationDetails.amountAvailableToTransfer = parseFloat($(this).attr('data-amount-available-to-transfer'));

    $('#amount-available-to-transfer').text(paymentsAdmin.core.toCurrency(transcationDetails.amountAvailableToTransfer));

});

$(".email-receipt").click(function () {

    transcationDetails.selectedTransactionReference = $(this).attr('data-id');

});

$('.print').click(function () {
    window.print();
});