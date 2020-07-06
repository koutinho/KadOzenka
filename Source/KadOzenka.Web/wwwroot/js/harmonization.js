
var globalRowNumber;

function clearCustomLevels() {
    var existedCustomLevels = $("div[class^='customLevelBlock-']");
    existedCustomLevels.each(function () {
        this.remove();
    });
}


function addNewCustomLevel(url, rowNumber, attributeId) {
    kendo.ui.progress($('body'), true);
    var data = [{ rowNumber: rowNumber }];
    var templateContent = $("#templateRow").html();
    var template = kendo.template(templateContent);
    var result = kendo.render(template, data);
    $("#levelsList").append(result);
    var classWrapper = ".customLevelBlock-" + rowNumber;
    $(classWrapper).load(url, { rowNumber: rowNumber }, function (response) {
        $(classWrapper).show(250);
        if (attributeId) {
            fillLevelFromTemplate(rowNumber, attributeId);
        } 
        kendo.ui.progress($('body'), false);
    });
}


function fillLevelFromTemplate(localRowNumber, attributeId) {
    var dropDownTree = $("#AttributeId_" + localRowNumber).data('kendoDropDownTree');
    if (dropDownTree) {
        dropDownTree.value(attributeId || '');
        dropDownTree.trigger('change');
    }
}


function addCustomLevelsToForm(formObject) {
    if (!formObject) {
        return formObject;
    }
    var customLevels = [];
    for (var i = 1; i < globalRowNumber; i++) {
        var attributeName = 'AttributeId_' + i;
        var levelName = 'LevelNumber_' + i;
        var rowName = 'RowNumber_' + i;
        customLevels.push({
            RowNumber: formObject[rowName],
            LevelNumber: formObject[levelName],
            AttributeId: formObject[attributeName]
        });
        delete formObject[attributeName];
    }
    return Object.assign({}, formObject, { AdditionalCustomLevels: customLevels });
}