var paymentsAdmin = {
    core: {

        isValidMonetaryValue: function (e) {

            // If we've just got a negative sign - that's fine.
            if (e === "-") return true;
            if (e === ".") return true;

            // 1. Check it's numeric'
            var result = !isNaN(parseFloat(e)) && isFinite(e);

            // 2. Check it's only got 2 decimal places
            if (result) {
                result = this.decimalPlaces(e) <= 2;
            }

            return result;
        },

        decimalPlaces: function (num) {
            var match = ('' + num).match(/(?:\.(\d+))?(?:[eE]([+-]?\d+))?$/);
            if (!match) { return 0; }
            return Math.max(
                0,
                // Number of digits right of decimal point.
                (match[1] ? match[1].length : 0)
                // Adjust for scientific notation.
                - (match[2] ? +match[2] : 0));
        },

        preventMultipleFormSubmissions: function () {
            $('.one-click-submit-button').each(function () {
                var $theButton = $(this);
                var $theForm = $theButton.closest('form');
                
                // Hide the button and submit the form
                function tieButtonToForm() {
                    $theButton.one('click', function () {
                        $theButton.prop('disabled', true);
                        $theForm.submit();
                    });
                }

                tieButtonToForm();

                // This handler will re-wire the event when the form is invalid.
                $theForm.submit(function (event) {
                    if (!$(this).valid()) {
                        $theButton.prop('disabled', false);
                        event.preventDefault();
                        tieButtonToForm();
                    }
                });
            });
        }
    },

    pages: {
        payment: {
            index: {
                searchAction: null,
                searchEnabledFundCodes: []
            }
        },
        accountHolder: {
            fundMessageOptions: []
        }
    },

    controls: {
        autocomplete: {
            downarrow: '<svg version="1.1" xmlns="http://www.w3.org/2000/svg" class="autocomplete__dropdown-arrow-down" focusable="false"><g stroke="none" fill="outline" fill-rule="evenodd"><polygon fill="#000000" points="0 0 10 0 5 8"></polygon></g></svg>'
        }
    },

    services: {
        accountHolder: {
            lookup: function(transferItem, onCompleteCallback) {
                $.ajax({
                    type: "POST",
                    url: rootUrl + "/AccountHolder/Lookup",
                    data: JSON.stringify(transferItem),
                    datatype: "JSON",
                    contentType: "application/json; charset=utf-8",
                    success: function (returnData) {
                        if (returnData.ok) {
                            transferItem.name = returnData.data.name;
                            transferItem.outstandingBalance = returnData.data.outstandingBalance;
                            onCompleteCallback();
                        }
                        else {
                            if (returnData.message.length > 0) {
                                console.log(returnData.message);
                            }
                            else {
                                console.log('We were unable to fetch account holder information');
                            }
                            onCompleteCallback();
                        }
                    },
                    failure: function () {
                        console.log("We were unable to fetch account holder information");
                        onCompleteCallback();
                    },
                    error: function () {
                        console.log("We were unable to fetch account holder information");
                        onCompleteCallback();
                    }
                });
            }
        }
    }
};

paymentsAdmin.core.preventMultipleFormSubmissions();