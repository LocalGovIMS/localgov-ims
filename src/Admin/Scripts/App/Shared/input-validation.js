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

jQuery.validator.addMethod('greaterthan', function (value, element, params) {
    var otherValue = $(params.element).val();

    return isNaN(value) && isNaN(otherValue) || (params.allowequality === 'True' ? parseFloat(value) >= parseFloat(otherValue) : parseFloat(value) > parseFloat(otherValue));
}, '');

jQuery.validator.unobtrusive.adapters.add('greaterthan', ['other', 'otherdisplayname', 'allowequality'], function (options) {
    var prefix = options.element.name.substr(0, options.element.name.lastIndexOf('.') + 1),
        other = options.params.other,
        fullOtherName = appendModelPrefix(other, prefix),
        element = $(options.form).find(':input[name=' + fullOtherName + ']')[0];

    options.rules['greaterthan'] = { allowequality: options.params.allowequality, element: element };

    console.log(options.message);

    if (options.message) {
        options.messages['greaterthan'] = options.message;
    }
});

function appendModelPrefix(value, prefix) {
    if (value.indexOf('*.') === 0) {
        value = value.replace('*.', prefix);
    }
    return value;
}