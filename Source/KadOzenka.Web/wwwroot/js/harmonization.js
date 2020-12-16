
var globalRowNumber;

function clearCustomLevels() {
    var existedCustomLevels = $("div[class^='customLevelBlock-']");
    existedCustomLevels.each(function () {
        this.remove();
    });
}


function addNewCustomLevel(url, rowNumber) {
    kendo.ui.progress($('body'), true);
    var classWrapper = generateCustomRowFromTemplate(rowNumber);
    $(classWrapper).load(url, { rowNumber: rowNumber }, function (response) {
        $(classWrapper).show(250);
        kendo.ui.progress($('body'), false);
        try {
            addSelectionHandlersForDropDowns();
        } catch (e) {
            console.log(e);
        }
    });
}

async function addNewCustomLevels(url, startRowNumber, rowCount, rowValues) {
    if (rowCount === 0)
        return;

    var rowRange = `${startRowNumber}-${startRowNumber + rowCount - 1}`;
    var classWrapper = generateCustomRowFromTemplate(rowRange);
    await $.post(url,
        { startRowNumber: startRowNumber, rowCount: rowCount, rowValues: rowValues },
        function (data) {
            $(classWrapper).append(data);
            $(classWrapper).show(250);
            try {
                addSelectionHandlersForDropDowns();
            } catch (e) {
                console.log(e);
            }
        });
}

function generateCustomRowFromTemplate(rowNumber) {
    var data = [{ rowNumber }];

    var templateContent = $("#templateRow").html();
    var template = kendo.template(templateContent);
    var result = kendo.render(template, data);
    $("#levelsList").append(result);
    var classWrapper = `.customLevelBlock-${rowNumber}`;

    return classWrapper;
}


function addCustomLevelsToForm(formObject) {
    if (!formObject) {
        return formObject;
    }
    var customLevels = [];
    var formObjectKeys = Object.keys(formObject);
    for (var i = 1; i < globalRowNumber; i++) {
        var attributeName = formObjectKeys.find(key => key.includes('AttributeId_' + i));
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

function disableKendoMultiSelect(name) {
	$('#' + name).data("kendoMultiSelect").value([]);
	$('#' + name).data("kendoMultiSelect").enable(false);
}