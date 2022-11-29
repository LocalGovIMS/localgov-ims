var Model = {
    eReturns: []
}

function addEReturn(ereturn) {
    Model.eReturns.push(ereturn);
    saveModel();
    updateUI();
}

function removeEReturn(id) {
    _.remove(Model.eReturns,
        function(item) {
            return item.id == id;
        });
    saveModel();
    updateUI();
}

function loadModel() {
    Model = JSON.parse(localStorage.getItem('eReturnModel'));    
    if (Model === null) {
        Model = {
            eReturns: []
        }
    }
    updateUI();
}

function saveModel() {
    localStorage.setItem('eReturnModel', JSON.stringify(Model));
    updateUI();
}

function clearModel() {
    Model = {
        eReturns: []
    }
    saveModel();
    updateUI();
}

function totalEReturns() {
    return _.round(parseFloat(_.sumBy(Model.eReturns, "amount")),2);
}
