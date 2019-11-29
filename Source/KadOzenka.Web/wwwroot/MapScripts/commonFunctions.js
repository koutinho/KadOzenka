function numberWithSpaces(x) {
    var parts = x.toString().split(".");
    parts[0] = parts[0].replace(/\B(?=(\d{3})+(?!\d))/g, "&nbsp;");
    return parts.join(".");
}

function getArea(type, area, area_land) {
    if (type == 14) return `${numberWithSpaces(area_land)}&nbsp;сот.`;
    else return `${numberWithSpaces(area)}&nbsp;м²`;
}

function getAreaNumber(type, area, area_land) { return type == 14 ? area_land : area; }

function getAreaType(type, area, area_land) { return type == 14 ? "сот." : "м²"; }

function getFloor(floor, floorCount) { return floor == null ? `${floorCount}` : `${floor}&nbsp;из&nbsp;${floorCount}`; }

function refreshCurrentToken() { currentToken = Math.random().toString(36).substring(2, 15) + Math.random().toString(36).substring(2, 15); }