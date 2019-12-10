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