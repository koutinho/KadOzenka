function insertCard(cartData) {
    document.getElementById("dataContentContainer").innerHTML +=`
        <div class="DataItemContainer">
            <div class="Container">
                <div class="Header">${cartData.type}</div>
            </div>
            <div class="Container">
                <div class="Name">Цена</div>
                <div class="Value">${numberWithSpaces(cartData.price)}&nbsp;₽</div>
            </div>
            <div class="Container">
                <div class="Name">Площадь</div>
                <div class="Value">${numberWithSpaces(cartData.area)}&nbsp;м²</div>
            </div>
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
                    <div class="Value">${cartData.metro.replace(",", ", ")}</div>
                </div>`}
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
            <div class="Line"></div>
        </div>`;
}

function clearCardContainer() { document.getElementById("dataContentContainer").innerHTML = ""; }