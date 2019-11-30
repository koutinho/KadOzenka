function insertCard(cartData, isLast) {
    document.getElementById("dataContentContainer").innerHTML += `
        <div class="DataItemContainer ${getPropertyType(cartData.source)}">
            <div class="Container">
                <div class="Header">
                    ${!(cartData.roomsCount != null && cartData.roomsCount != 0 && (cartData.type == 10 || cartData.type == 11 || cartData.type == 18)) ? "" : `${cartData.roomsCount}-комн.&nbsp;`}${PropType[cartData.type].type}&nbsp;${getArea(cartData.type, cartData.area, cartData.areaLand)}
                </div >
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
            <div class="Container">
                <div class="Name">Ссылка</div>
                <div class="Value">
                    <a href="${cartData.link}" target="_blank">
                        <img  height="20" width="20" src="/MapIcons/link.svg" />
                    </a>
                </div>
            </div>
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
                <div class="Name">Карточка</div>
                <div class="Value">
                    <a href="/ObjectCard?ObjId=${cartData.id}&RegisterViewId=MarketObjects&isVertical=true&useMasterPage=true" target="_blank">
                        <img  height="20" width="20" src="/MapIcons/card.svg" />
                    </a>
                </div>
            </div>
            <div class="Container">
                <div class="Name">Дата&nbsp;внесения</div>
                <div class="Value">${cartData.parserTime}</div>
            </div>
            <div class="Container">
                <div class="Name">Последнее&nbsp;обновление</div>
                <div class="Value">${cartData.lastUpdateDate}</div>
            </div>
            <div class="Container">
                <div class="Name">Кадастровый&nbsp;номер</div>
                <div class="Value">${cartData.cadastralNumber}</div>
            </div>
            <div class="Logo"></div>
            ${isLast ? "" : `<div class="Line"></div>`}
        </div>`;
}

function clearCardContainer() { document.getElementById("dataContentContainer").innerHTML = ""; }