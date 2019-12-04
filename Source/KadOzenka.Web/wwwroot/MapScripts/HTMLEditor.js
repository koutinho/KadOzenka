function insertCard(cartData, isLast) {
    document.getElementById("dataContentContainer").innerHTML += `
        <!--<div class="DataItemContainer ${getPropertyType(cartData.source)}">-->
        <div class="DataItemContainer">
            <div class="Container">
                <div class="Header">
                    <div class="Text">
                        ${!(cartData.roomsCount != null && cartData.roomsCount != 0 && (cartData.type == 10 || cartData.type == 11 || cartData.type == 18)) ? "" : `${cartData.roomsCount}-комн.&nbsp;`}${PropType[cartData.type].type}&nbsp;${getArea(cartData.type, cartData.area, cartData.areaLand)}
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
                    <div class="Name">Цена за ${getAreaType(cartData.type, cartData.area, cartData.areaLand)}</div>
                    <div class="Value">${numberWithSpaces(Math.round(cartData.price / getAreaNumber(cartData.type, cartData.area, cartData.areaLand)))}&nbsp;₽/${getAreaType(cartData.type, cartData.area, cartData.areaLand)}</div>
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
                <div class="Value">${getArea(cartData.type, cartData.area, cartData.areaLand)}</div>
            </div>
            ${!(cartData.areaLand != null && (cartData.type == 16 || cartData.type == 15 || cartData.type == 8)) ? "" : `
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
                <div class="Value">${cartData.parserTime}</div>
            </div>
            ${cartData.type == 14 ? "" : `
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