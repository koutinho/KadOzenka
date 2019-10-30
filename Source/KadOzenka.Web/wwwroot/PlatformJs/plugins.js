//class="ajax-data-loading" - все ajax-запросы
//[data-load-mode="delay"] - отложенный запрос 
//data-url - url
//data-param-<название_параметра> - произвольные параметры
//data-onsuccess|onerror|oncomplete 
//data-result-type = "json|text|html" 

//1. Если у элемента есть класс ajax-data-loading, запрос выполняется сразу
//2. Если необходимо отложить загрузку данных, использовать атрибут data-load-mode="delay"
//3. Для каждого элемента с классом ajax-data-loading выполняется ОДИН запрос с указанными параметрами
//4. По умолчанию после выполнения запроса результат подгружается в текущий контейнер 
if (typeof jQuery === 'undefined') {
    if (window.console) {
        window.console.warn('не подключена библиотека jQuery');
    }
}

/*$(document).ajaxError(function (event, request, settings) {
    if (request && request.statusText != 'OK') {
        console.log('url = ' + settings.url);
        console.log('params = ' + settings.data);
        console.log('type = ' + settings.type);
        console.log('statusText = ' + request.statusText);
        console.log('status = ' + request.status);
    }
});*/

$(function () {
    /*$('.nav').on('click', 'a', function () {
        var currentTab = $(this).attr('href');
        var $current = $(currentTab + ' .ajax-data-loading');

        $current.each(function () {
            var $this = $(this);
            var loadMode = $this.data('load-mode');
            if (loadMode != undefined && loadMode == 'tabopen') {
                $this.data('load-mode', undefined).removeData('load-mode').removeAttr('data-load-mode');
            }
        });
        $current.ajaxDataLoader();
    });*/

    $('.ajax-data-loading').ajaxDataLoader();
});

(function ($) {
    $.ajaxDataLoader = $.ajaxDataLoader || {};

    $.ajaxDataLoader.methods = {
        "pushJSONValues":
            function (data, $container, propertyInsert) {
                for (var prop in data) {
                    var value = data[prop];
                    var $propContainer = $container.find('[data-result-prop="' + prop + '"]');
                    if ($.isPlainObject(value)) {
                        $.ajaxDataLoader.methods["pushJSONValues"](value, $propContainer, propertyInsert);
                    }
                    else {
                        if (propertyInsert != undefined) {
                            $.ajaxDataLoader.methods[propertyInsert](prop, value, $propContainer);
                        }
                        else {
                            $propContainer.html($.trim(value));
                        }
                    }
                }
            }
    };

    //добавить метод
    $.ajaxDataLoader.addMethod = function (name, func) {
        if (typeof (name) != "string" || !$.isFunction(func)) {
            return;
        }

        if ($.ajaxDataLoader.methods[name] == undefined) {
            $.ajaxDataLoader.methods[name] = func;
        }
    };

    $.fn.initAjaxDataLoader = function () {
        var $elements = $(this);

        $elements.each(function () {
            var $this = $(this);
            var loadMode = $this.data('load-mode');
            if (loadMode != undefined && loadMode == 'tabopen') {
                $this.data('load-mode', undefined).removeData('load-mode').removeAttr('data-load-mode');
            }
        });

        $elements.ajaxDataLoader();
    };

    $.fn.ajaxDataLoader = function (config) {
        var $elements = $(this);

        if (!$elements.length) {
            return;
        }

        var methods = $.ajaxDataLoader.methods;


        function log(msg, warn) {
            if (window.console) {
                var message = '$.fn.ajaxDataLoader -> ' + msg;
                if (warn) {
                    console.warn(message);
                } else {
                    console.log(message);
                }
            }
        }

        //получение параметров запроса
        var getParameters = function getParameters($elem) {
            var params = {};
            var data = $elem.data();
            for (var prop in data) {
                //ищем параметры ajax-запроса
                if (/param/.test(prop)) {
                    var res = prop.substr(5, prop.length).toLowerCase();

                    if (/transition([0-9]+)/.test(res)) {
                        params[res.substring(10, res.length)] = data[prop];
                    }
                    else {
                        params[res] = data[prop];
                    }
                }
            }
            return params;
        };

        function ajaxRequest(url, requestType, resultType, resultParams, onSuccess, onError, onComplete, traditional, $currentElement) {
            if (url == undefined || url == "" || typeof (url) != "string") {
                log("не удалось выполнить ajax-запрос (не указан url)", true);
                return;
            }

            if (!$.isPlainObject(resultParams)) {
                log("не удалось выполнить ajax-запрос (неверно указаны параметры запроса)", true)
                return;
            }

            //DEBUG
            //log(url);

            return $.ajax({
                url: url,
                type: requestType,
                dataType: resultType || "html",
                data: resultParams,
                success: function (data) {
                    if (onSuccess != undefined) {
                        onSuccess(data, $elements, $currentElement);
                    }

                },
                error: function (request, error) {
                    if (onError != undefined) {
                        onError(request, error, $elements, $currentElement);
                    } else {
                        $elements.html('<div class="alert alert-error">Ошибка загрузки данных (' + request.status + ' ' + request.statusText + ')</div>');
                    }
                    log('ошибка загрузки данных, url: ' + url + ' ' + request.status + ' ' + request.statusText, true);
                },
                complete: function (data) {
                    if (onComplete != undefined) {
                        onComplete(data, $elements, $currentElement);
                    }
                },
                traditional: traditional ? traditional : false
            });
        }

        //базовые функции
        var base = {};
        base.getParameters = getParameters;

        if ($elements.length == $elements.filter('.ajax-data-loading').length) {
            for (var i = 0, len = $elements.length; i < len; i += 1) {
                var $elem = $elements.eq(i);

                var loadMode = $elem.data('load-mode');
                //не выполнять отложенные запросы
                if ($elem.data('load-mode') != undefined && loadMode != "always") {
                    continue;
                }

                //получить параметры запроса
                //добавить параметры из настроек
                var p = $.extend(getParameters($elem), config != undefined && config.data != undefined ? config.data : {});

                //удалить класс загрузки
                if (loadMode != "always") {
                    $elem.removeClass('ajax-data-loading');
                }

                //должен быть указан url
                var url = $elem.data('url');
                if (url == undefined && $.trim(url) == "") {
                    log("не указан url", true);
                    continue;
                }

                //тип запроса: post или get
                var requestType = $elem.data('request-type');
                if (requestType != undefined)
                    requestType = requestType.toUpperCase();

                if (requestType == undefined || (requestType != 'POST' && requestType != 'GET'))
                    requestType = 'POST';

                //лоадер
                var loader = $elem.data('loader');
                if (loader != undefined && loader == true) {
                    $elem.append('<div class="loader" style="height:60px;"></div>')
                    kendo.ui.progress($elem.find('.loader'), true);
                }

                var onSuccess = function (data, $elements, $currentElement) {
                    if ($currentElement.data("result-type") != undefined && $currentElement.data('result-type').toLowerCase() == "json") {

                        if ($currentElement.data('pushjsonvalues') == undefined) {
                            methods["pushJSONValues"](data, $currentElement, $currentElement.data('propertyinsert'));
                        } else {
                            methods[$currentElement.data('pushjsonvalues')](data, $currentElement, $currentElement.data('propertyinsert'));
                        }
                    } else {
                        kendo.ui.progress($currentElement.find('.loader'), false);
                        $currentElement.html(data);
                    }

                    if ($currentElement.data('onsuccess') != undefined) {
                        if (methods.hasOwnProperty([$currentElement.data('onsuccess')]))
                            methods[$currentElement.data('onsuccess')](data, $elements, $currentElement);
                    }
                };

                var onComplete = function (data, $elements, $currentElement) {
                    if ($currentElement.data('oncomplete') != undefined) {
                        methods[$currentElement.data('oncomplete')](data, $elements, $currentElement);
                    }
                    if (config != undefined && config.oncomplete != undefined) {
                        config.oncomplete(data, $elements, $currentElement);
                    }
                };

                var onError = function (request, error, $elements, $currentElement) {
                    if ($currentElement.data('onerror') != undefined) {
                        methods[$currentElement.data('onerror')](request, error, $elements, $currentElement);
                    }
                };

                ajaxRequest(url, requestType, $elem.data('result-type') || "html", p, onSuccess, onError, onComplete, false, $elem); //$elem- текущий элемент
            }
        } else {
            var resultParams = $.extend(config.data || {}, config.getRequestParams != undefined ? config.getRequestParams(base, $elements) : {});

            if (!config.url) {
                log("не указан url", true);
            }
            //перед выполнением запроса
            if (config.beforeAjaxRequest != undefined) {
                config.beforeAjaxRequest(resultParams);
            }
            ajaxRequest(config.url, config.resultType, resultParams, config.onSuccess, config.onError, config.onComplete, true);
        }
        return this;
    };
})(jQuery);

// плагин для получения типа элемента
// $(".element").getType(); - возвращает radio, text, checkbox, select, textarea, и т.д. (включая DIV, SPAN, все типы элементов)
// $(".elList").getType(); // вернет тип первого элемента
(function ($) {
    $.fn.getType = function () {
        if (this.length)
            return this[0].tagName == "INPUT" ? this[0].type.toLowerCase() : this[0].tagName.toLowerCase();

        return '';
    }
})(jQuery);

$.extend({
    ajaxRequest: function (url, args, typeRequest, onSuccess, onError) {
        typeRequest = (typeof typeRequest !== 'undefined') ? typeRequest : 'POST';

        $.ajax({
            type: typeRequest,
            url: url,
            data: args,
            success: function (e) {
                if (onSuccess)
                    onSuccess(e);
            }, error: function (e) {
                if (onError)
                    onError(e);
            }
        });
    }
});
