var rowNumber = 1;
function addNewRow(url) {
    kendo.ui.progress($('body'), true);
    var data = [{ rowNumber }];
    var templateContent = $("#templateRow").html();
    var template = kendo.template(templateContent);
    var result = kendo.render(template, data);
    $("#levelsList").append(result);
    var classWrapper = ".wrapper-" + rowNumber;
    $(classWrapper).load(url, { rowNumber: rowNumber }, function (response) {
        $(classWrapper).show(500);
        rowNumber++;
        kendo.ui.progress($('body'), false);
    });
}


function addCustomLevels(formObject) {
    if (!formObject) {
        return formObject;
    }
    var customLevels = [];
    for (var i = 1; i < rowNumber; i++) {
        var attributeName = 'AttributeId_' + i;
        var levelName = 'LevelNumber_' + i;
        customLevels.push({
            LevelNumber: formObject[levelName],
            AttributeId: formObject[attributeName]
        });
        delete formObject[attributeName];
    }
    return Object.assign({}, formObject, { AdditionalCustomLevels: customLevels });
}