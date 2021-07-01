function getFilterTypeContainerSelector(prefix) {
    return '#' + prefix + '_filterTypeContainer';
}

function getFilterTypeField(prefix) {
    return '#' + replaceBrackets(prefix) + '_Type';
}

function getStringFilterContainerSelector(prefix) {
    return '#' + escapeBrackets(prefix) + '_stringFilterContainer';
}

function getStringFilteringTypeSelector(prefix) {
    return '#' + replaceBrackets(prefix) + '_StringFilter_FilteringType';
}

function getNumberFilterContainerSelector(prefix) {
    return '#' + escapeBrackets(prefix) + '_numberFilterContainer';
}

function getNumberFilteringTypeSelector(prefix) {
    return '#' + replaceBrackets(prefix) + '_NumberFilter_FilteringType';
}

function getRefContainerSelector(prefix) {
    return '#' + escapeBrackets(prefix) + '_refFilterContainer';
}

function getRefFilteringTypeSelector(prefix) {
    return '#' + replaceBrackets(prefix) + '_ReferenceFilter_FilteringType';
}

function getBoolContainerSelector(prefix) {
    return '#' + escapeBrackets(prefix) + '_boolFilterContainer';
}

function getBoolFilteringTypeSelector(prefix) {
    return '#' + replaceBrackets(prefix) + '_BoolFilter_FilteringType';
}

function getDateContainerSelector(prefix) {
    return '#' + escapeBrackets(prefix) + '_dateFilterContainer';
}

function getDateFilteringTypeSelector(prefix) {
    return '#' + replaceBrackets(prefix) + '_DateFilter_FilteringType';
}

function escapeBrackets(prefix){
    return prefix.replace('[','\\[').replace(']','\\]');
}

function replaceBrackets(prefix){
    return prefix.replace('[','_').replace(']','_');
}

function bindUnusedFieldHider(prefix) {
    console.log(prefix);
    hideAllFilterContainers(prefix);

    $(getFilterTypeField(prefix)).bind('change', () => onTypeChange(prefix));
    $(getStringFilteringTypeSelector(prefix)).bind('change', () => onStringFilterChange(prefix));
    $(getDateFilteringTypeSelector(prefix)).bind('change', () => onDateFilterChange(prefix));
    $(getNumberFilteringTypeSelector(prefix)).bind('change', () => onNumberFilterChange(prefix));
    $(getBoolFilteringTypeSelector(prefix)).bind('change', () => onBoolFilterChange(prefix));
}

function onTypeChange(prefix) {
    hideAllFilterContainers(prefix);
    resetAllFields(prefix);
    let filterType = getFilterTypeField(prefix);
    let filterTypeValue = $(filterType).data('kendoDropDownList').text();
    switch (filterTypeValue) {
        case "String":
            $(getStringFilterContainerSelector(prefix)).show();
            break;
        case "Number":
            $(getNumberFilterContainerSelector(prefix)).show();
            break;
        case "Date":
            $(getDateContainerSelector(prefix)).show();
            break;
        case "Reference":
            $(getRefContainerSelector(prefix)).show();
            break;
    }
}

function onStringFilterChange(prefix) {
    let stringFilterTypeField = getStringFilteringTypeSelector(prefix);
    let stringFilterTypeValue = $(stringFilterTypeField).data('kendoDropDownList').text();
    let stringFilterValueContainer = '#' + escapeBrackets(prefix) + '_stringFilterValueContainer';
    switch (stringFilterTypeValue) {
        case "Пусто":
        case "Не пусто":
        case "IsNull":
        case "IsNotNull":
            $(stringFilterValueContainer).hide();
            break;
        default:
            $(stringFilterValueContainer).show();
            break;
    }
}

function onDateFilterChange(prefix) {
    let dateFilterTypeField = getDateFilteringTypeSelector(prefix);
    let dateFilterTypeValue = $(dateFilterTypeField).data('kendoDropDownList').text();
    let dateFilterValueContainer = '#' + escapeBrackets(prefix) + '_dateFilterValueContainer';
    let dateFilterValue2Container = '#' + escapeBrackets(prefix) + '_dateFilterValue2Container';
    switch (dateFilterTypeValue) {
        case "IsNull":
        case "IsNotNull":
        case "Пусто":
        case "Не пусто": {
            $(dateFilterValueContainer).hide();
            $(dateFilterValue2Container).hide();
        }
            break;
        case "InRange":
        case "InRangeIncludingBoundaries":
        case "В диапазоне":
        case "В диапазоне (включительно)": {
            $(dateFilterValueContainer).show();
            $(dateFilterValue2Container).show();
        }
            break;
        default: {
            $(dateFilterValueContainer).show();
            $(dateFilterValue2Container).hide();
        }
            break;
    }
}

function onNumberFilterChange(prefix) {
    let numberFilterTypeField = getNumberFilteringTypeSelector(prefix);
    let numberFilterTypeValue = $(numberFilterTypeField).data('kendoDropDownList').text();
    let numberFilterValueContainer = '#' + escapeBrackets(prefix) + '_numberFilterValueContainer';
    let numberFilterValue2Container = '#' + escapeBrackets(prefix) + '_numberFilterValue2Container';
    switch (numberFilterTypeValue) {
        case "Пусто":
        case "Не пусто":
        case "Equal":
        case "NotEqual": {
            $(numberFilterValueContainer).hide();
            $(numberFilterValue2Container).hide();
        }
            break;
        case "В диапазоне":
        case "В диапазоне (включая границы)":
        case "InRange":
        case "InRangeIncludingBoundaries": {
            $(numberFilterValueContainer).show();
            $(numberFilterValue2Container).show();
        }
            break;
        default: {
            $(numberFilterValueContainer).show();
            $(numberFilterValue2Container).hide();
        }
            break;
    }
}

function onRefFilterChange(prefix){
    let refFilterTypeField = getRefFilteringTypeSelector(prefix);
    let refFilterTypeValue = $(refFilterTypeField).data('kendoDropDownList').text();
    let refFilterValueContainer = '#' + escapeBrackets(prefix) + '_referenceFilterValueContainer';
    switch (refFilterTypeValue) {
        case "Пусто":
        case "Не пусто": {
            $(refFilterValueContainer).hide();
        }
            break;
        default: {
            $(refFilterValueContainer).show();
        }
            break;
    }
}

function onBoolFilterChange(prefix){
    let boolFilterTypeField = getBoolFilteringTypeSelector(prefix);
    let boolFilterTypeValue = $(boolFilterTypeField).data('kendoDropDownList').text();
    let boolFilterValueContainer = '#' + escapeBrackets(prefix) + '_referenceFilterValueContainer';
    switch (boolFilterTypeValue) {
        case "Пусто":
        case "Не пусто":
        case "Equal":
        case "NotEqual":{
            $(boolFilterValueContainer).hide();
        }
            break;
        default: {
            $(boolFilterValueContainer).show();
        }
            break;
    }
}

function hideAllFilterContainers(prefix) {
    let stringContainer = getStringFilterContainerSelector(prefix);
    let numberContainer = getNumberFilterContainerSelector(prefix);
    let dateContainer = getDateContainerSelector(prefix);
    let refContainer = getRefContainerSelector(prefix);
    let boolContainer = getBoolContainerSelector(prefix);

    $(stringContainer).hide();
    $(numberContainer).hide();
    $(dateContainer).hide();
    $(refContainer).hide();
    $(boolContainer).hide();
}

function resetAllFields(prefix){
    $('#'+replaceBrackets(prefix)+'\\.StringFilter\\.Value').val(null);

    $('#'+replaceBrackets(prefix)+'_NumberFilter_Value').data('kendoNumericTextBox').value(null);
    $('#'+replaceBrackets(prefix)+'_NumberFilter_Value2').data('kendoNumericTextBox').value(null);

    $('#'+replaceBrackets(prefix)+'_DateFilter_Value').data('kendoDatePicker').value(null);
    $('#'+replaceBrackets(prefix)+'_DateFilter_Value2').data('kendoDatePicker').value(null);

    //$('#'+replaceBrackets(prefix)+'_RefFilter_Value').data('').value(null);
}

function bindAttributeSelectorEvents(field){
    console.log(field);
    $(field).data('kendoDropDownTree').bind('change', (e)=> getAttributeInfo(e));
}

function getAttributeInfo(e){
    let attributeId = e?.sender?.value();
    let container = e.sender.element.parents('fieldset').first();
    let typeSelectors = e.sender.element.parents('fieldset').find("[id$='_Type']").toArray();
    if (attributeId) {
        kendo.ui.progress($(container), true);
        return $.ajax({
            url: '/GbuObject/GetRegisterAttribute',
            type: 'GET',
            dataType: "html",
            data: { attributeId: attributeId },
            success: function (data) {
                if (data) {
                    let parsedData = JSON.parse(data);
                    let type = parsedData?.Type;
                    var convertedType = "None";
                    let reference = parsedData?.ReferenceId;
                    if (reference){
                        // Нет поддержки референсов на данный момент
                        convertedType = "Reference";
                    }
                    else {
                        switch (type) {
                            case 2:
                                convertedType = "Number";
                                break;
                            case 3:
                                convertedType = "Boolean";
                                break;
                            case 4:
                                convertedType = "String";
                                break;
                            case 5:
                                convertedType = "Date";
                                break;
                        }
                    }
                    typeSelectors.forEach(x=>$(x).data('kendoDropDownList').text(convertedType));
                    typeSelectors.forEach(x=>$(x).trigger('change'));
                }
                kendo.ui.progress($(container), false);
            },
            error: function (request) {
                kendo.ui.progress($(container), false);
            },
            always: function (){
                kendo.ui.progress($(container), false);
            }
        });
    }
}