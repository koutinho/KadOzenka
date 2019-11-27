function BackTo(event) {
    window.location.href = "/";
}

function ChangeBounds(event) {
    const params = new window.URLSearchParams(window.location.search);
    const newCenter = event.get('newCenter');
    params.set('center', newCenter);
    const newZoom = event.get('newZoom');
    params.set('zoom', newZoom);
    if (window.history.pushState) {
        const newUrl = new URL(window.location.href);
        newUrl.search = params;
        window.history.pushState({ path: newUrl.href }, '', newUrl.href);
    }
    const newBounds = event.get('newBounds');
    GetData(newBounds, newZoom);
}

function clickOnCluster(event) {
    var ids = [];
    switch (event.get('target').options._name) {
        case ObjectTypes.cluster:
            event.get('target').properties.get('geoObjects').forEach(x => ids.push(x.properties.get('data')[0].id));
            break;
        case ObjectTypes.geoObject:
            ids.push(event.get('target').properties.get('data')[0].id);
            break;
    }
    GetRequiredInfo(ids);
}