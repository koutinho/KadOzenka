function numberWithSpaces(x) {
    var parts = x.toString().split(".");
    parts[0] = parts[0].replace(/\B(?=(\d{3})+(?!\d))/g, "&nbsp;");
    return parts.join(".");
};

function getArea(segment, area, area_land) {
    if (segment == 12 || segment == 13) return `${numberWithSpaces(area_land)}&nbsp;сот.`;
    else return `${numberWithSpaces(area)}&nbsp;м²`;
};

function getAreaNumber(segment, area, area_land) { return (segment == 12 || segment == 13) ? area_land : area; };

function getAreaType(segment, area, area_land) { return (segment == 12 || segment == 13) ? "сот." : "м²"; };

function getFloor(floor, floorCount) { return floor == null ? `${floorCount}` : `${floor}&nbsp;из&nbsp;${floorCount}`; };

function refreshCurrentToken() { currentToken = Math.random().toString(36).substring(2, 15) + Math.random().toString(36).substring(2, 15); };

function getPropertyType(source) {
    switch (source) {
        case "ЦИАН": return "Cian";
        case "Росреестр": return "Rosreestr";
    }
};

function generateNewFilter(filterInfo) {
    var AllDealTypes = Array.from(document.getElementById("dealTypePanel").getElementsByClassName("filterButton")),
        AllPropertyTypes = Array.from(document.getElementById("propertyTypePanel").getElementsByClassName("filterButton")),
        AllMarketSegmentTypes =
            Array.from(document.getElementById("propertyMarketSegmentPanel").getElementsByClassName("filterButton")).concat(
            Array.from(document.getElementById("commercialMarketSegmentPanel").getElementsByClassName("filterButton")));
    var filterDealTypes = {}, filterPropertyTypes, filterMarketSegments = {}, result = [], selectedDealTypes = [], selectedPropertyTypes = [], selectedMarketSegments = [];
    AllDealTypes.forEach(x => { if (!x.classList.contains("inactive")) selectedDealTypes.push({ id: x.getAttribute("elementId"), text: x.getAttribute("elementValue") }); });
    AllPropertyTypes.forEach(x => { if (!x.classList.contains("inactive")) selectedPropertyTypes.push({ id: x.getAttribute("elementId"), text: x.getAttribute("elementValue") }) });
    AllMarketSegmentTypes.forEach(x => { if (!x.classList.contains("inactive")) selectedMarketSegments.push({ id: x.getAttribute("elementId"), text: x.getAttribute("elementValue") }); });
    if (selectedDealTypes.length == AllDealTypes.length) selectedDealTypes = [];
    if (selectedPropertyTypes.length == AllPropertyTypes.length) selectedPropertyTypes = [];
    if (selectedMarketSegments.length == AllMarketSegmentTypes.length) selectedMarketSegments = [];
    if (selectedDealTypes.length != 0) {
        filterDealTypes = {
            typeControl: filterInfo.dealTypeFilter.typeControl,
            type: filterInfo.dealTypeFilter.type,
            text: `${filterInfo.dealTypeFilter.text}: ${selectedDealTypes.map(function (x) { return x.text; }).join(', ')}`,
            value: selectedDealTypes.map(function (x) { return parseInt(x.id); }),
            referenceId: filterInfo.dealTypeFilter.referenceId,
            id: filterInfo.dealTypeFilter.id
        };
    }
    if (selectedPropertyTypes.length != 0) {
        filterPropertyTypes = {
            typeControl: filterInfo.propertyTypeFilter.typeControl,
            type: filterInfo.propertyTypeFilter.type,
            text: `${filterInfo.propertyTypeFilter.text}: ${selectedPropertyTypes.map(function (x) { return x.text; }).join(', ')}`,
            value: selectedPropertyTypes.map(function (x) { return parseInt(x.id); }),
            referenceId: filterInfo.propertyTypeFilter.referenceId,
            id: filterInfo.propertyTypeFilter.id
        };
    }
    if (selectedMarketSegments.length != 0) {
        filterMarketSegments = {
            typeControl: filterInfo.commertialMarketFilter.typeControl,
            type: filterInfo.commertialMarketFilter.type,
            text: `${filterInfo.commertialMarketFilter.text}: ${selectedMarketSegments.map(function (x) { return x.text; }).join(', ')}`,
            value: selectedMarketSegments.map(function (x) { return parseInt(x.id); }),
            referenceId: filterInfo.commertialMarketFilter.referenceId,
            id: filterInfo.commertialMarketFilter.id
        };
    }
    if (filterPropertyTypes) result.push(filterPropertyTypes);
    if (filterDealTypes) result.push(filterDealTypes);
    if (filterMarketSegments) result.push(filterMarketSegments);
    return JSON.stringify(result);
};