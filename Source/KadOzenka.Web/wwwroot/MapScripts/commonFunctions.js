function numberWithSpaces(x) {
    var parts = x.toString().split(".");
    parts[0] = parts[0].replace(/\B(?=(\d{3})+(?!\d))/g, "&nbsp;");
    return parts.join(".");
}

function getArea(type, area, area_land) {
    if (type == 14) return `${numberWithSpaces(area_land)}&nbsp;сот.`;
    else return `${numberWithSpaces(area)}&nbsp;м²`;
}

function getFloor(floor, floorCount) {
    return floor == null ? `${floorCount}` : `${floor}&nbsp;из&nbsp;${floorCount}`;
}