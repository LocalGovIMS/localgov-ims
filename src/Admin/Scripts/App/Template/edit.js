var templateRows = [];
var count = parseInt($("row-count").val());
var templateId = $("#Id").val();

$("#add-row").on("click",
    function () {

        
            templateRows.push({
                id: 0,
                reference: $("#reference").val(),
                reference_override: $("#reference_override:checked").val(),
                vatcode: $("#vat_code").val(),
                vat_override: $("#vat_override:checked").val(),
                description: $("#description").val(),
                description_override: $("#description_override:checked").val(),
            });
            $("#reference").val("");
            $("#reference_override").prop("checked", false);
            $("#vat_code").val();
            $("#vat_override").prop("checked", false);
            $("#description").val("");
            $("#description_override").prop("checked", false);
            renderRows();
        
    });

$("#template-rows").on("click", ".remove-row", 
    function() {
        templateRows.splice($(this).data('id'), 1);
        renderRows();
    });

$("#template-rows tr").each(function() {
    templateRows.push({
        id: $(this).find(".js-id").val(),
        reference: $(this).find(".js-reference").val(),
        reference_override: $(this).find(".js-reference-override:checked").val(),        
        description_override: $(this).find(".js-description-override:checked").val(),
        vatcode: $(this).find(".js-vatcode").val(),
        vat_override: $(this).find(".js-vat-override:checked").val(),
        description: $(this).find(".js-description").val()
    });
});

function vatOptions(selectedOption) {
    var options = $("#vat_code");        
    return options.html();
}


function renderRows() {

    $('#template-rows').html("");

    if (templateRows.length > 0) {
        
        $(templateRows).each(function (index) {
            var checkedReference = (this.reference_override == 'true') ? ' checked="checked" ' : '';
            var checkedDescription = (this.description_override == 'true') ? ' checked="checked" ' : '';
            var checkedVat = (this.vat_override == 'true') ? ' checked="checked" ' : '';

            console.log(index);

            if (this.Id == undefined) this.Id = 0;

            $('#template-rows').append('<tr><td><input type="hidden" name="TemplateRows[' + index + '].Id" value="' + this.id + '" /><input type="hidden" name="TemplateRows[' + index + '].TemplateId" value="' + templateId + '" />' +                
                '<input type="text" name="TemplateRows[' + index + '].Reference" value="' + this.reference + '" class="trim-pasted-data" /></td><td>' +                                
                '<input type="checkbox" name="TemplateRows[' + index + '].ReferenceOverride" value="true" '+checkedReference+'/></td><td>' +                                
                '<input type="text" name="TemplateRows[' + index + '].Description" value="' + this.description + '" /></td><td>' +                
                '<input type="checkbox" name="TemplateRows[' + index + '].DescriptionOverride" value="true" ' + checkedDescription +'/></td><td>' +                                
                '<select id="vat_row_' + index +'" name="TemplateRows[' + index + '].VatCode">' + vatOptions(this.vatcode) + '</select></td><td>' +
                '<input type="checkbox" name="TemplateRows[' + index + '].VatOverride" value="true" ' + checkedVat +'/></td><td>' +
                '<a href=\'#\' class=\'ui red button remove-row\' data-id=\'' +
                index +
                '\'>Remove</a>' +                
                '</td></tr>');


            $("#vat_row_" + index).val(this.vatcode);
        });
    }    
}