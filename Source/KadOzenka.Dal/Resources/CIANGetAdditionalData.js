window._result = null;
var resultObject = {};

var bargainTermsItems = document.querySelectorAll("section[data-name*='BargainTermsBlock'] div[class*='item']");
for (var i = 0; i < bargainTermsItems.length; i++) {
    var bargainTermsChildrenArray = Array.from(bargainTermsItems[i].children);
    var bargainValueChild = null;
    if (bargainTermsChildrenArray.findIndex(x => x.innerText == 'Налог') !== -1) {
        bargainValueChild = bargainTermsChildrenArray.find(x => x.className && x.className.includes('value'));
        var bargainValueChildText = bargainValueChild ? bargainValueChild.innerText : null;
        if (bargainValueChildText && bargainValueChildText.includes('УСН')) {
            resultObject.vat = 'УСН';
        }
        if (bargainValueChildText && bargainValueChildText.includes('НДС включен')) {
            resultObject.vat = 'НДС включен';
        }
    }

    if (bargainTermsChildrenArray.findIndex(x => x.innerText == 'Коммунальные платежи') !== -1) {
        bargainValueChild = bargainTermsChildrenArray.find(x => x.className && x.className.includes('value'));
        if (bargainValueChild && bargainValueChild.innerText) {
            resultObject.isUtilitiesIncluded = bargainValueChild.innerText.includes('Включены') ? true : false;
        }
    }

    if (bargainTermsChildrenArray.findIndex(x => x.innerText == 'Эксплуатационные расходы') !== -1) {
        bargainValueChild = bargainTermsChildrenArray.find(x => x.className && x.className.includes('value'));
        if (bargainValueChild && bargainValueChild.innerText) {
            resultObject.isOperatingCostsIncluded = bargainValueChild.innerText.includes('Включены') ? true : false;
        }
    }
}

var featureItems = document.querySelectorAll(" li[data-name*='FeatureItem']");
for (var i = 0; i < featureItems.length; i++) {
    var childrenArray = Array.from(featureItems[i].children);

    if (childrenArray.findIndex(x => x.innerText == 'Тип помещения') !== -1) {
        var valueChild = childrenArray.find(x => x.className && x.className.includes('value'));
        resultObject.placementType = valueChild ? valueChild.innerText : null;
    }

    if (childrenArray.findIndex(x => x.innerText == 'Вход') !== -1) {
        var valueChild = childrenArray.find(x => x.className && x.className.includes('value'));
        resultObject.entranceType = valueChild ? valueChild.innerText : null;
    }

    if (childrenArray.findIndex(x => x.innerText == 'Состояние') !== -1) {
        var valueChild = childrenArray.find(x => x.className && x.className.includes('value'));
        resultObject.quality = valueChild ? valueChild.innerText : null;
    }
}

window._result = JSON.stringify(resultObject);