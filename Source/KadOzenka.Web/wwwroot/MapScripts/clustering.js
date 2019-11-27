var selectedGeoObjectsIds = [];
var selectedGeoObjects = [];

function initCluster(coordinates) {
	map.geoObjects.removeAll();

	geoObjects = [];
	AllControllersData = coordinates.filter(function (item) {
		return selectedGeoObjectsIds.indexOf(item.id) === -1;
	});
	for (var i = 0, len = AllControllersData.length; i < len; i++) {
		var color = PropType[AllControllersData[i].type].color;
		geoObjects[i] = new ymaps.Placemark(
			AllControllersData[i].points, {
				data: [{ weight: 1, color: color, id: AllControllersData[i].id }]
			}, {
				iconColor: color,
				iconLayout: ClusterSettings.layout,
				iconPieChartRadius: ClusterSettings.pieChartRadius,
				iconPieChartCoreRadius: ClusterSettings.pieChartCoreRadius,
				iconPieChartStrokeWidth: ClusterSettings.pieChartStrokeWidth
			});
	}

	clusterer = new ymaps.Clusterer({
		clusterIconLayout: ClusterSettings.layout,
		clusterIconPieChartRadius: ClusterSettings.pieChartRadius,
		clusterIconPieChartCoreRadius: ClusterSettings.pieChartCoreRadius,
		clusterIconPieChartStrokeWidth: ClusterSettings.pieChartStrokeWidth,
		hasBalloon: ClusterSettings.balloon,
		clusterDisableClickZoom: ClusterSettings.disableClickZoom
	});
	clusterer.add(geoObjects);
	clusterer.events.add('click', function (e) { clickOnCluster(e); });
	map.geoObjects.add(clusterer);

	selectedClusterer = new ymaps.Clusterer({
		clusterIcons: [
			{
				href: SelectedClusterSettings.clusterIconHref,
				size: SelectedClusterSettings.clusterIconSize,
				offset: SelectedClusterSettings.clusterIconOffset
			}
		],
		clusterIconContentLayout: ymaps.templateLayoutFactory.createClass(
			'<div style="color: #FFFFFF; font-weight: bold;">{{ properties.geoObjects.length }}</div>'
		),
		hasBalloon: ClusterSettings.balloon,
		clusterDisableClickZoom: ClusterSettings.disableClickZoom

	});
	selectedClusterer.add(selectedGeoObjects);
	selectedClusterer.events.add('click', function (e) { clickOnCluster(e); });
	map.geoObjects.add(selectedClusterer);

	const url = new URL(window.location);
	const params = new window.URLSearchParams(url.search);
	if (!params.has('center')) {
		map.setBounds(clusterer.getBounds(), { checkZoomRange: true });
	}
}

function clickOnCluster(e) {
	for (let i = 0; i < selectedGeoObjects.length; i++) {
		selectedGeoObjects[i].options.unset('iconLayout');
		selectedGeoObjects[i].options.unset('iconImageHref');
		selectedGeoObjects[i].options.unset('iconImageSize');
		selectedGeoObjects[i].options.unset('iconImageOffset');

		selectedGeoObjects[i].options.set('iconLayout', ClusterSettings.layout);
		selectedGeoObjects[i].options.set('iconPieChartRadius', ClusterSettings.pieChartRadius);
		selectedGeoObjects[i].options.set('iconPieChartCoreRadius', ClusterSettings.pieChartCoreRadius);
		selectedGeoObjects[i].options.set('iconPieChartStrokeWidth', ClusterSettings.pieChartStrokeWidth);
	}
	selectedClusterer.remove(selectedGeoObjects);
	clusterer.add(selectedGeoObjects);

	selectedGeoObjects = [];
	selectedGeoObjectsIds = [];
	switch (e.get('target').options._name) {
		case ObjectTypes.cluster:
			selectedGeoObjects = e.get('target').properties.get('geoObjects');
			e.get('target').properties.get('geoObjects').forEach(x => selectedGeoObjectsIds.push(x.properties.get('data')[0].id));
			break;
		case ObjectTypes.geoObject:
			selectedGeoObjects.push(e.get('target'));
			selectedGeoObjectsIds.push(e.get('target').properties.get('data')[0].id);
			break;
	}

	for (let i = 0; i < selectedGeoObjects.length; i++) {
		selectedGeoObjects[i].options.unset('iconLayout');
		selectedGeoObjects[i].options.unset('iconPieChartRadius');
		selectedGeoObjects[i].options.unset('iconPieChartCoreRadius');
		selectedGeoObjects[i].options.unset('iconPieChartStrokeWidth');

		selectedGeoObjects[i].options.set('iconLayout', SelectedGeoObjectSettings.iconLayout);
		selectedGeoObjects[i].options.set('iconImageHref', SelectedGeoObjectSettings.iconImageHref);
		selectedGeoObjects[i].options.set('iconImageSize', SelectedGeoObjectSettings.iconImageSize);
		selectedGeoObjects[i].options.set('iconImageOffset', SelectedGeoObjectSettings.iconImageOffset);
	}
	clusterer.remove(selectedGeoObjects);
	selectedClusterer.add(selectedGeoObjects);

	GetRequiredInfo(selectedGeoObjectsIds);
}