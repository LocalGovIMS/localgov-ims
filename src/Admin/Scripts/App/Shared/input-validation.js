$('.monetary-amount').on('keypress', function (e) {

    var keypressValue = "";

    // IE keypress value is a keyname - e.g. on the keypad, pressing the '.' will give a value of 'Del' - so we have to use char.
    // However, Chrome char is always undefined!, so we have to use key - which isn't the name, but is '.'. 
    // Helpful...

    if (e.char === undefined) {
        keypressValue = e.key;
    }
    else {
        keypressValue = e.char;
    }

    var value = $(this).val();
    return paymentsAdmin.core.isValidMonetaryValue(value.substring(0, this.selectionStart) + keypressValue + value.substring(this.selectionStart));

});

$('.monetary-amount').on('paste', function (e) {

    clipboardData = e.clipboardData || window.clipboardData;
    pastedData = clipboardData.getData('Text');

    var value = this.value;
    return paymentsAdmin.core.isValidMonetaryValue(value.substring(0, this.selectionStart) + pastedData + value.substring(this.selectionStart));

});