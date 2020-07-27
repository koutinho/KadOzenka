function createColorPicker(rgbValue, rgbPicker, rgbShowPanel) {
    fixScale(document);

    var val = document.getElementById(rgbValue);

    colorjoe.registerExtra('text', function (p, joe, o) { e(p, o.text ? o.text : 'text'); });

    function e(parent, text) {
        var elem = document.createElement('div');
        elem.innerHTML = text;
        parent.appendChild(elem);
    }

    colorjoe.rgb(rgbPicker).on('change', function (c) {
        document.getElementById(rgbShowPanel).style.background = c.hex();
        document.getElementById("splicedDeltaContent").innerHTML = document.getElementById("splicedDeltaController").value;
        createColorLegend(document.getElementById("splicedDeltaController").value,
            document.getElementById('rgbInitialShowPanel').style.background,
            document.getElementById('rgbResultShowPanel').style.background);
    }).update();
}
