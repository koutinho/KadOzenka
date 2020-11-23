(function (w) {

    function _generateContainer() {
        var $container = $('<div id="overlayLoader"></div>');
        $container.css({
            background: "#e9e9e9",
            position: "absolute",
            top: "0",
            left: "0",
            bottom: "0",
            right: "0",
            opacity: "0.5"
        });
        var $containerLoader = $('<div id="myProgress" style="background-color: grey;"></div>');
        var width = 50; //ширина полоски загрузки, проценты
        $containerLoader.css({
            position: "relative",
            top: "50%",
            left: ((100 - width) / 2).toString() + "%",
            width: width.toString() + '%'
        });
        var $barLoader = $('<div id="myBar" style="width: 1%; background-color: grey; height: 5px; background-color: green;"></div>');
        $containerLoader.append($barLoader);
        $container.append($containerLoader);
        return $container;
    }

    function _moveInInterval(myBar, progress, cWidth) {
        var currentWidth = cWidth;
        if (progress > parseFloat(currentWidth)) {
                var idInterval = null;
                idInterval = setInterval(() => {
                    if (currentWidth !== progress) {
                        currentWidth++;
                        myBar.style.width = currentWidth + '%';
                    } else {
                        clearInterval(idInterval);
                    }
                }, 30);
            }
        }
 

    var progressLoader = {
        loader: function ($el, isShow) {
            var self = this;
            if ($el.length > 0 && isShow) {
                var $container = _generateContainer();
                $el.find('#overlayLoader').remove();
                $el.append($container);
            }

            if ($el.length > 0 && !isShow) {
                self.move($el, 100, false);
                $el.find('#overlayLoader').remove();
            }
        },
        /**
         * 
         * @param {any} $el джеквери элемент на котором прогресс бар
         * @param {number} progress состояние процесса
         * @param {boolean} useInterval использовать проход по каждому проценту через setInterval
         */
        move: function ($el, progress, useInterval = true) {
            if ($el.length > 0 && $el.find('#overlayLoader').length > 0) {
                var myBar = $el.find('#myBar')[0];
                var cWidth = myBar.style.width.replace('%','');
                if (progress > parseFloat(cWidth)) {
                    if (useInterval) {
                        _moveInInterval(myBar, progress, cWidth);
                    } else {
                        progress = progress > 100 ? 100 : progress;
                        myBar.style.width = progress + '%';
                    }
                }
            }

        }
    };
    w.progressLoader = progressLoader;
})(window)