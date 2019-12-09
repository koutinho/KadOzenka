function initHeatmap(coordinates) {
    heatmap = new ymaps.Heatmap(coordinates, {
        radius: HeatmapSettings.radius,
        dissipating: HeatmapSettings.dissipating,
        opacity: HeatmapSettings.opacity,
        intensityOfMidpoint: HeatmapSettings.intensityOfMidpoint,
        gradient: HeatmapSettings.gradient
    });
    createToggleHeatmapWidget();
    addToggleHeatmapWidget();
}

function toggleHeatmap() {
    if (heatmap.getMap()) {
        heatmap.setMap(null);
        refreshToggleHeatmapWidget(false);
    } else {
        heatmap.setMap(map);
        refreshToggleHeatmapWidget(true);
    }
}