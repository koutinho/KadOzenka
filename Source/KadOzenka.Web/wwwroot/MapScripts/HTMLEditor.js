function insertCard(cartData, isLast) {
    avaliableCIPJSOptions = avaliableCIPJSTypes.filter(function (x) { return x.code != cartData.propertyTypeCode }).map(function (x) { return `<option value="${x.code}">${x.value.length < 28 ? x.value : '...' + x.value.substr(0, 27)}</option>`; }).join('');
    avaliableSegmentOptions = avaliableSegments.filter(function (x) { return x.code != cartData.marketSegmentCode }).map(function (x) { return `<option value="${x.code}">${x.value.length < 28 ? x.value : '...' + x.value.substr(0, 27)}</option>`; }).join('');
    availableStatusOptions = avaliableStatuses.filter(function (x) { return x.code != cartData.statusCode }).map(function (x) { return `<option value="${x.code}">${x.value.length < 28 ? x.value : '...' + x.value.substr(0, 27)}</option>`; }).join('');
    document.getElementById("dataContentContainer").innerHTML += `
        <div class="DataItemContainer">
            <div class="Container">
                <div class="Header">
                    <div class="Text">${CIPJSType[cartData.segment][0].type}${cartData.area ? `&nbsp;${getArea(cartData.propertyType, cartData.area, cartData.areaLand)}` : ""}</div>
                    <div class="Content">
                        <a target="_blank" style="margin-left: auto;" href="/ObjectCard?ObjId=${cartData.id}&RegisterViewId=MarketObjects&isVertical=true&useMasterPage=true">
                            <div class="Card"></div>
                        </a>
                        ${cartData.source != "Росреестр" ? `
                            <a target="_blank" style="margin-left: 3px;" href="${cartData.link}">
                                <div class="Link ${getPropertyType(cartData.source)}"></div>
                            </a>
                        ` : `<div style="margin-left: 3px;" class="Link ${getPropertyType(cartData.source)}"></div>`}
                    </div>
                </div>
            </div>
            ${cartData.source != "Росреестр" ? `<div class="Container">${getImageGallery(cartData)}</div>` : ``}
            <div class="Container">
                <div class="Name">Тип сделки</div>
                <div class="Value">${cartData.dealType}</div>
            </div>
            <div class="Container">
                <div class="Name">Цена${(cartData.dealType != 'Предложение-продажа' && cartData.dealType != 'Сделка купли-продажи') ? "&nbsp;аренды" : ""}</div>
                <div class="Value">${numberWithSpaces(cartData.price)}&nbsp;₽${(cartData.dealType != 'Предложение-продажа' && cartData.dealType != 'Сделка купли-продажи') ? "/мес." : ""}</div>
            </div>
            ${(cartData.dealType != 'Предложение-продажа' && cartData.dealType != 'Сделка купли-продажи') ? "" : `
                <div class="Container">
                    <div class="Name">Цена за ${getAreaType(cartData.propertyType, cartData.area, cartData.areaLand)}</div>
                    <div class="Value">${(cartData.area || cartData.areaLand) ? `${numberWithSpaces(Math.round(cartData.price / getAreaNumber(cartData.propertyType, cartData.area, cartData.areaLand)))}&nbsp;₽/${getAreaType(cartData.propertyType, cartData.area, cartData.areaLand)}` : "—"}</div>
                </div>
            `}
            ${cartData.floor == null && cartData.floorCount == null ? "" : `
                <div class="Container">
                    <div class="Name">${cartData.floor == null ? "Количество&nbsp;этажей" : "Этаж"}</div>
                    <div class="Value">${getFloor(cartData.floor, cartData.floorCount)}</div>
                </div>
            `}
            <div class="Container">
                <div class="Name">Площадь</div>
                <div class="Value">${(cartData.area || cartData.areaLand) ? getArea(cartData.propertyType, cartData.area, cartData.areaLand) : "—"}</div>
            </div>
            ${!(cartData.areaLand != null && (cartData.segment == 0 && cartData.area)) ? "" : `
                <div class="Container">
                    <div class="Name">Площадь ЗУ</div>
                    <div class="Value">${numberWithSpaces(cartData.areaLand)}&nbsp;сот.</div>
                </div>
            `}
            ${cartData.metro == null ? "" : `
                <div class="Container">
                    <div class="Name">Метро</div>
                    <div class="Value">${cartData.metro.replace(/,/g, ", ")}</div>
                </div>`
            }
            <div class="Container">
                <div class="Name">Адрес</div>
                <div class="Value">${cartData.address}</div>
            </div>
            <div class="Container">
                <div class="Name">Дата&nbsp;актуального&nbsp;обновления</div>
                <div class="Value">${cartData.lastUpdateDate == null ? cartData.parserTime : cartData.lastUpdateDate}</div >
            </div>
            ${(cartData.segment == 0 && !cartData.area) ? "" : `
                <div class="Container">
                    <div class="Name">Кадастровый&nbsp;номер${cartData.source == "Росреестр" ? "" : "&nbsp;здания" }</div>
                    <div class="Value">${cartData.cadastralNumber}</div>
                </div>  
            `}
            <div class="EditContainer${editMode ? "" : " Hidden"}">
                <div class="EditLine">
                    <div class="MiniLine"></div>
                    <div class="Content">Редактировать</div>
                </div>
                <div class="Container">
                    <div class="Name">Долгота</div>
                    <div class="Value">
                        <input id="lngTextBox_${cartData.id}" class="EditData" type="text" value="${cartData.lng}">
                    </div>
                </div>
                <div class="Container">
                    <div class="Name">Широта</div>
                    <div class="Value">
                        <input id="latTextBox_${cartData.id}" class="EditData" type="text" value="${cartData.lat}">
                    </div>
                </div>
                <div class="Container">
                    <div class="Name">Тип ЦИПЖС</div>
                    <div class="Value">
                        <div class="EditDataSelectContainer">
                            <select id="typeSelect_${cartData.id}" class="EditDataSelect" dir="rtl">
                                <option value="${cartData.propertyTypeCode}">${cartData.propertyType.length < 28 ? cartData.propertyType : '...' + cartData.propertyType.substr(0, 27)}</option>
                                ${avaliableCIPJSOptions}
                            </select>
                        </div>
                    </div>
                </div>
                <div class="Container">
                    <div class="Name">Сегмент</div>
                    <div class="Value">
                        <div class="EditDataSelectContainer">
                            <select id="segmentSelect_${cartData.id}" class="EditDataSelect" dir="rtl">
                                <option value="${cartData.marketSegmentCode}">${cartData.marketSegment.length < 28 ? cartData.marketSegment : '...' + cartData.marketSegment.substr(0, 27)}</option>
                                ${avaliableSegmentOptions}
                            </select>
                        </div>
                    </div>
                </div>
                <div class="Container">
                    <div class="Name">Статус</div>
                    <div class="Value">
                        <div class="EditDataSelectContainer">
                            <select id="statusSelect_${cartData.id}" class="EditDataSelect" dir="rtl">
                                <option value="${cartData.statusCode}">${cartData.status.length < 28 ? cartData.status : '...' + cartData.status.substr(0, 27)}</option>
                                ${availableStatusOptions}
                            </select>
                        </div>
                    </div>
                </div>
                <div class="Container">
                    <div id="saveBtn_${cartData.id}" class="Button blocked">Сохранить</div>
                    <div id="undoBtn_${cartData.id}" class="Button blocked">Отменить</div>
                </div>
            </div>
            ${isLast ? "" : `<div class="Line"></div>`}
        </div>
    `;
};

function addEventsCard(cartData) {
    document.getElementById(`lngTextBox_${cartData.id}`).addEventListener('input', function () { dataChanged(cartData); });
    document.getElementById(`latTextBox_${cartData.id}`).addEventListener('input', function () { dataChanged(cartData); });
    document.getElementById(`typeSelect_${cartData.id}`).addEventListener('change', function () { dataChanged(cartData); });
    document.getElementById(`segmentSelect_${cartData.id}`).addEventListener('change', function () { dataChanged(cartData); });
    document.getElementById(`statusSelect_${cartData.id}`).addEventListener('change', function () { dataChanged(cartData); });
    document.getElementById(`saveBtn_${cartData.id}`).addEventListener('click', function () { saveDataChanges(cartData); });
    document.getElementById(`undoBtn_${cartData.id}`).addEventListener('click', function () { undoDataChanges(cartData); });
};

function clearCardContainer() {
    document.getElementById("data").classList.add("Hidden");
    document.getElementById("map").style.width = "100%";
    map.container.fitToViewport();
    document.getElementById("dataContentContainer").innerHTML = "";
};

function showCardContainer() {
    document.getElementById("data").classList.remove("Hidden");
    document.getElementById("map").style.width = "calc(100% - 350px)";
    map.container.fitToViewport();
};

function getValidImageUrlArray(source) {
    let imageUrlArray = [];
    if (source) imageUrlArray = source.split(',');
    return imageUrlArray;
};

function checkImageUrl(url) {
    return new Promise((resolve, reject) => {
        let img = new Image();
        img.addEventListener('load', e => { resolve(url); });
        img.addEventListener('error', () => { reject(new Error(`Failed to load image's URL: ${url}`)); });
        img.src = url;
    });
};

function getImageGallery(cartData) {
    let imgUrs = getValidImageUrlArray(cartData.images);
    let promises = imgUrs.map(url => {
        return checkImageUrl(url);
    });
    if (!Promise.allSettled) {
        Promise.allSettled = function (promises) {
            return Promise.all(promises.map(p => Promise.resolve(p).then(value => ({ state: 'fulfilled', value: value }), error => ({ state: 'rejected', reason: error }))));
        };
    }
    Promise.allSettled(promises)
        .then(results => {
            let existingImages = [];
            results.forEach((result, num) => { if (result.status == "fulfilled") existingImages.push(result.value); });
            if (existingImages.length > 0) {
                document.getElementById("sliderGallery" + cartData.id).innerHTML =
                    `
                        <div class='imageGallery visibleImageGallery' order="0" onclick="onImageClick(${cartData.id});" style="background-image:url(${existingImages[0]});"></div>`;
                for (let i = 1; i < existingImages.length; i++) {
                    document.getElementById("sliderGallery" + cartData.id).innerHTML +=
                        `<div class='imageGallery' order="${i}" onclick="onImageClick(${cartData.id});" style="background-image:url(${existingImages[i]});"></div>`;
                }
                if (existingImages.length > 1) {
                    document.getElementById("sliderGallery" + cartData.id).innerHTML +=
                        `<div class="arrowsContainer">
                             <div class="arrowLeftContainer" onclick="onLeftButtonClick(${cartData.id},${existingImages.length});">
                                 <span class="fas fa-arrow-left"></span>
                             </div>
                             <div class="arrowRightContainer" onclick="onRightButtonClick(${cartData.id},${existingImages.length});">
                                 <span class="fas fa-arrow-right"></span>
                             </div>
                         </div>`;
                }
                document.getElementById("sliderGallery" + cartData.id).innerHTML +=
                    `<div class="imageCountsContainer" onclick="onImageClick(${cartData.id});">
                        <div class="imageCounts">1/${existingImages.length}</div>
                     </div>`;
            } else document.getElementById("sliderGallery" + cartData.id).innerHTML = `Нет фото`;
        });

    return `${
            `<div class="sliderGalleryContainer">
                <div class="sliderGallery" id ="sliderGallery${cartData.id}" >
                    <div class="preloaderImg"></div>
                </div>
             </div>
            `}`;
};

function onLeftButtonClick(cardId, imgCount) {
    let parent = document.getElementById("sliderGallery" + cardId);
    let imageCount = $(parent).find(".imageCounts")[0];
    let visibleImage = $(parent).find(".visibleImageGallery")[0];
    $(visibleImage).removeClass("visibleImageGallery");
    if (visibleImage.getAttribute("order") == 0) {
        let newVisibleImage = $(parent).find(".imageGallery")[imgCount - 1];
        $(newVisibleImage).addClass("visibleImageGallery");
        imageCount.innerHTML = imgCount + "/" + imgCount;
    } else {
        let newVisibleImage = visibleImage.previousSibling;
        $(newVisibleImage).addClass("visibleImageGallery");
        imageCount.innerHTML = (parseInt(newVisibleImage.getAttribute("order")) + 1)  + "/" + imgCount;
    }
};

function onRightButtonClick(cardId, imgCount) {
    let parent = document.getElementById("sliderGallery" + cardId);
    let imageCount = $(parent).find(".imageCounts")[0];
    let visibleImage = $(parent).find(".visibleImageGallery")[0];
    $(visibleImage).removeClass("visibleImageGallery");
    if (visibleImage.getAttribute("order") == imgCount - 1) {
        let newVisibleImage = $(parent).find(".imageGallery")[0];
        $(newVisibleImage).addClass("visibleImageGallery");
        imageCount.innerHTML = 1 + "/" + imgCount;
    } else {
        let newVisibleImage = visibleImage.nextSibling;
        $(newVisibleImage).addClass("visibleImageGallery");
        imageCount.innerHTML = (parseInt(newVisibleImage.getAttribute("order")) + 1) + "/" + imgCount;
    }
};

function onImageClick(cardId) {
   //TODO: future handler for image click event
};