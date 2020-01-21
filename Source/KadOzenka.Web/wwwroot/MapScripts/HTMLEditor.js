function insertCard(cartData, isLast) {
    document.getElementById("dataContentContainer").innerHTML += `
        <!--<div class="DataItemContainer ${getPropertyType(cartData.source)}">-->
        <div class="DataItemContainer">
            <div class="Container">
                <div class="Header">
                    <div class="Text">
                        ${SegmentType[cartData.segment].type}&nbsp;${getArea(cartData.segment, cartData.area, cartData.areaLand)}
                    </div>
                    <div class="Content">
                        <a style="margin-left: auto;" href="/ObjectCard?ObjId=${cartData.id}&RegisterViewId=MarketObjects&isVertical=true&useMasterPage=true">
                            <div class="Card"></div>
                        </a>
                        ${cartData.source != "Росреестр" ? `
                            <a style="margin-left: 3px;" href="${cartData.link}">
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
                    <div class="Name">Цена за ${getAreaType(cartData.segment, cartData.area, cartData.areaLand)}</div>
                    <div class="Value">${numberWithSpaces(Math.round(cartData.price / getAreaNumber(cartData.segment, cartData.area, cartData.areaLand)))}&nbsp;₽/${getAreaType(cartData.segment, cartData.area, cartData.areaLand)}</div>
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
                <div class="Value">${getArea(cartData.segment, cartData.area, cartData.areaLand)}</div>
            </div>
            ${!(cartData.areaLand != null && ((cartData.segment == 3 && cartData.area) || cartData.segment == 14)) ? "" : `
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
            ${(cartData.segment == 3 && !cartData.area) ? "" : `
                <div class="Container">
                    <div class="Name">Кадастровый&nbsp;номер${cartData.source == "Росреестр" ? "" : "&nbsp;здания" }</div>
                    <div class="Value">${cartData.cadastralNumber}</div>
                </div>  
            `}
            ${isLast ? "" : `<div class="Line"></div>`}
        </div>`;
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
    if (source) {
        imageUrlArray = source.split(',');
    }

    return imageUrlArray;
};

function checkImageUrl(url) {
    return new Promise((resolve, reject) => {
        let img = new Image();
        img.addEventListener('load', e => {
            resolve(url);
        });
        img.addEventListener('error', () => {
            reject(new Error(`Failed to load image's URL: ${url}`));
        });
        img.src = url;
    });
}

function getImageGallery(cartData) {
    let imgUrs = getValidImageUrlArray(cartData.images);
    let promises = imgUrs.map(url => {
        return checkImageUrl(url);
    });
    if (!Promise.allSettled) {
        Promise.allSettled = function (promises) {
            return Promise.all(promises.map(p => Promise.resolve(p).then(value => ({
                state: 'fulfilled',
                value: value
            }), error => ({
                state: 'rejected',
                reason: error
            }))));
        };
    }
    Promise.allSettled(promises)
        .then(results => {
            let existingImages = [];
            results.forEach((result, num) => {
                if (result.status == "fulfilled") {
                    existingImages.push(result.value);
                }
            });
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
            } else {
                document.getElementById("sliderGallery" + cartData.id).innerHTML = `Нет фото`;
            }
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
}

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
}

function onImageClick(cardId) {
   //TODO: future handler for image click event
};