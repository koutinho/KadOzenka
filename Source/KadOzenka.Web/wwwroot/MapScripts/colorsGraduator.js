function hex(c) {
    var s = "0123456789abcdef";
    var i = parseInt(c);
    if (i == 0 || isNaN(c)) return "00";
    i = Math.round(Math.min(Math.max(0, i), 255));
    return s.charAt((i - i % 16) / 16) + s.charAt(i % 16);
}

function convertToHex(rgb) { return hex(rgb[0]) + hex(rgb[1]) + hex(rgb[2]); }

function trim(s) { return (s.charAt(0) == '#') ? s.substring(1, 7) : s }

function generateColor(colorStart, colorEnd, colorCount) {
    var alpha = 0.0;
    var result = [];
    for (i = 0; i < colorCount; i++) {
        var c = [];
        alpha += (1.0 / colorCount);
        c[0] = colorStart[0] * alpha + (1 - alpha) * colorEnd[0];
        c[1] = colorStart[1] * alpha + (1 - alpha) * colorEnd[1];
        c[2] = colorStart[2] * alpha + (1 - alpha) * colorEnd[2];
        result.push("#" + convertToHex(c));
    }
    return result;
}
