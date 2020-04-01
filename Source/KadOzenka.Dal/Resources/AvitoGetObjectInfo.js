
    window._result = null;
    var resultObject = {};

    resultObject.lat = document.querySelector("div[data-map-lat]") != null
        ? document.querySelector("div[data-map-lat]").getAttribute("data-map-lat")
        : null;
    resultObject.lon = document.querySelector("div[data-map-lon]") != null
        ? document.querySelector("div[data-map-lon]").getAttribute("data-map-lon")
        : null;
    resultObject.marketId = window.avito.item.id;
    resultObject.price = window.avito.item.price;
    resultObject.priceFormatted = window.avito.item.priceFormatted;

    resultObject.description = document.querySelector(
        "div[class*='item-description'] div[itemprop*='description']").textContent;

    resultObject.imageUrls =
        Array.from(document.querySelectorAll(
                "div[class*='gallery-imgs-container'] div[class*='gallery-img-frame']"),
        x => x.getAttribute("data-url"));

    resultObject.address = window.avito.item.location;
    resultObject.metroList = Array.from(document.querySelectorAll("div[class*='item-address-georeferences'] span[class*='item-address-georeferences-item'] span[class*='_content']"),
        x => x.textContent);

    resultObject.area = window.dataLayer.find(x => Object.keys(x).includes("area"))
        ? window.dataLayer.find(x => Object.keys(x).includes("area"))["area"]
        : null;
    resultObject.houseArea = window.dataLayer.find(x => Object.keys(x).includes("house_area"))
        ? window.dataLayer.find(x => Object.keys(x).includes("house_area"))["house_area"]
        : null;
    resultObject.siteArea = window.dataLayer.find(x => Object.keys(x).includes("site_area"))
        ? window.dataLayer.find(x => Object.keys(x).includes("site_area"))["site_area"]
        : null;

    resultObject.wallMaterial = window.dataLayer.find(x => Object.keys(x).includes("material_sten"))
        ? window.dataLayer.find(x => Object.keys(x).includes("material_sten"))["material_sten"]
        : null;
    resultObject.houseType = window.dataLayer.find(x => Object.keys(x).includes("house_type"))
        ? window.dataLayer.find(x => Object.keys(x).includes("house_type"))["house_type"]
        : null;
    resultObject.garage_type = window.dataLayer.find(x => Object.keys(x).includes("garage_type"))
        ? window.dataLayer.find(x => Object.keys(x).includes("garage_type"))["garage_type"]
        : null;

    resultObject.floorCount = window.dataLayer.find(x => Object.keys(x).includes("floors_count"))
        ? window.dataLayer.find(x => Object.keys(x).includes("floors_count"))["floors_count"]
        : null;
    resultObject.floorNumber = window.dataLayer.find(x => Object.keys(x).includes("floor"))
        ? window.dataLayer.find(x => Object.keys(x).includes("floor"))["floor"]
        : null;

    resultObject.buildingClass = window.dataLayer.find(x => Object.keys(x).includes("building_class"))
        ? window.dataLayer.find(x => Object.keys(x).includes("building_class"))["building_class"]
        : null;

    window._result = JSON.stringify(resultObject);