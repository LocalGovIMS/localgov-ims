$('.ui.dropdown').dropdown({});
$('.ui.checkbox').checkbox({});

var Model = {
    suspenseItems: [],
    creditNotes: [],
    transfers: []
}

function loadModel() {
    Model = JSON.parse(localStorage.getItem('suspenseModel'));    
    if (Model === null) {
        Model = {
            suspenseItems: [],
            creditNotes: [],
            transfers: []
        }
    }
    updateUI();
}

function saveModel() {
    localStorage.setItem('suspenseModel', JSON.stringify(Model));
    updateUI();
}

function clearModel() {
    Model = {
        suspenseItems: [],
        creditNotes: [],
        transfers: []
    }
    saveModel();
    updateUI();
}

function clearTransfers() {
    Model.transfers = [];
    saveModel();
    updateUI();
}


function totalAvailableToTransfer() {
    return _.round(_.round(parseFloat(_.sumBy(Model.suspenseItems, "amount")),2) + _.round(parseFloat(totalCreditNotes()),2), 2);
}

function totalCreditNotes() {
    return _.round(parseFloat(_.sumBy(Model.creditNotes, "amount")),2);
}

function remainingAvailableToTransfer() {    
    return _.round(totalAvailableToTransfer() - _.round(parseFloat(_.sumBy(Model.transfers, "amount")),2),2);
}

function transferAmountMatchesTotalAvailable() {
    return _.round(remainingAvailableToTransfer(),2) == 0.00;
}