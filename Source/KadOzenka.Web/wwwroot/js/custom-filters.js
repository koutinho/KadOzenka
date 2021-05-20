function getFilterTypeContainerSelector(prefix) {
    return '#' + prefix + '_filterTypeContainer';
}

function getFilterTypeField(prefix) {
    return "#" + prefix + "_Type";
}

function getStringFilterContainerSelector(prefix) {
    return '#' + prefix + '_stringFilterContainer';
}

function getStringFilteringTypeSelector(prefix) {
    return '#' + prefix + '_StringFilter_FilteringType';
}

function getNumberFilterContainerSelector(prefix) {
    return '#' + prefix + '_numberFilterContainer';
}

function getNumberFilteringTypeSelector(prefix) {
    return '#' + prefix + '_NumberFilter_FilteringType';
}

function getRefContainerSelector(prefix) {
    return '#' + prefix + '_refFilterContainer';
}

function getRefFilteringTypeSelector(prefix) {
    return '#' + prefix + '_ReferenceFilter_FilteringType';
}

function getBoolContainerSelector(prefix) {
    return '#' + prefix + '_boolFilterContainer';
}

function getBoolFilteringTypeSelector(prefix) {
    return '#' + prefix + '_BoolFilter_FilteringType';
}

function getDateContainerSelector(prefix) {
    return '#' + prefix + '_dateFilterContainer';
}

function getDateFilteringTypeSelector(prefix) {
    return '#' + prefix + '_DateFilter_FilteringType';
}

function bindUnusedFieldHider(prefix) {
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
    let filterTypeValue = $(filterType).val();
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
    let stringFilterTypeValue = $(stringFilterTypeField).val();
    let stringFilterValueContainer = '#' + prefix + '_stringFilterValueContainer';
    switch (stringFilterTypeValue) {
        case "Пусто":
        case "Не пусто":
            $(stringFilterValueContainer).hide();
            break;
        default:
            $(stringFilterValueContainer).show();
            break;
    }
}

function onDateFilterChange(prefix) {
    let dateFilterTypeField = getDateFilteringTypeSelector(prefix);
    let dateFilterTypeValue = $(dateFilterTypeField).val();
    let dateFilterValueContainer = '#' + prefix + '_dateFilterValueContainer';
    let dateFilterValue2Container = '#' + prefix + '_dateFilterValue2Container';
    switch (dateFilterTypeValue) {
        case "Пусто":
        case "Не пусто": {
            $(dateFilterValueContainer).hide();
            $(dateFilterValue2Container).hide();
        }
            break;
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
    let numberFilterTypeValue = $(numberFilterTypeField).val();
    let numberFilterValueContainer = '#' + prefix + '_numberFilterValueContainer';
    let numberFilterValue2Container = '#' + prefix + '_numberFilterValue2Container';
    switch (numberFilterTypeValue) {
        case "Пусто":
        case "Не пусто": {
            $(numberFilterValueContainer).hide();
            $(numberFilterValue2Container).hide();
        }
            break;
        case "В диапазоне":
        case "В диапазоне (включая границы)": {
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
    let refFilterTypeValue = $(refFilterTypeField).val();
    let refFilterValueContainer = '#' + prefix + '_referenceFilterValueContainer';
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
    let boolFilterTypeValue = $(boolFilterTypeField).val();
    let boolFilterValueContainer = '#' + prefix + '_referenceFilterValueContainer';
    switch (boolFilterTypeValue) {
        case "Пусто":
        case "Не пусто": {
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
    $('#'+prefix+'\\.StringFilter\\.Value').val(null);

    $('#'+prefix+'_NumberFilter_Value').data('kendoNumericTextBox').value(null);
    $('#'+prefix+'_NumberFilter_Value2').data('kendoNumericTextBox').value(null);

    $('#'+prefix+'_DateFilter_Value').data('kendoDatePicker').value(null);
    $('#'+prefix+'_DateFilter_Value2').data('kendoDatePicker').value(null);

    //$('#'+prefix+'_RefFilter_Value').data('').value(null);
}

function bindAttributeSelectorEvents(field){
    $(field).data('kendoDropDownTree').bind('change', (e)=> getAttributeInfo(e));
}

function getAttributeInfo(e){
    let attributeId = e?.sender?.value();
    let container = e.sender.element.parents('fieldset');
    let typeSelectors = e.sender.element.parents('fieldset').find("[id$='_Type']");
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
                    typeSelectors.toArray().forEach(x=>$(x).data('kendoDropDownList').value(convertedType));
                    typeSelectors.toArray().forEach(x=>$(x).trigger('change'));
                }
                kendo.ui.progress($(container), false);
            },
            error: function (request) {

            },
            always: function (){
                kendo.ui.progress($(container), false);
            }
        });
    }
}