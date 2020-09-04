(function ($) {
    function rowMenu(el, config) {
        this.element = el;
        this.$element = $(el);

        this.options = config;
        this.init(config);
    }

    rowMenu.prototype.init = function (config) {
        var self = this;

        self.baseUrl = config.baseUrl ? config.baseUrl : '/';
        self.loadContentUrl = self.baseUrl + 'RowMenu/GetRowMenuContent';
        self.getButtonsUrl = self.baseUrl + 'RowMenu/GetButtonsUrl';
        self.checkConditionsUrl = self.baseUrl + 'RowMenu/CheckConditions', // метод для проверки состояний контролов
        self.filter = config.filter;
        self.delay = config.delay;
        self.registerViewId = config.registerViewId;
        self.registerId = config.registerId;
        self.MainContentSelector = ".mainContent";
        self.registerViewSettings = window['registerViewSettings_' + config.registerId];

        self.menuContainer = $('<div class="rm-container" style="display: none"></div>');
        self.$element.append(self.menuContainer);

        self.target = undefined;
        self.grid = self.$element.closest('.register-grid').data('kendoGrid');

        LoadContent();

        /* События */

        self.$element.on('mouseover', self.filter, function (e) {
            var showMenu = () => self.menuContainer.show();
            var $this = $(this);

            if ($this.has('th').length)
                return;

            if (!$this.is(self.target)) {
                self.target = $this;
                $this.addClass('row-menu');

                var dataItem = self.grid.dataItem($this);

                var coord = getCoords(this);
                var dx = $(self.MainContentSelector).width() - self.menuContainer.width() - 60;

                self.menuContainer.css('top', coord.top)
                    .css('left', dx)
                    .css('height', $this.height() - 4);

                if (dataItem) {
                    updateStates(dataItem.ID, function () {
                        if (self.delay)
                            setTimeout(showMenu, self.delay);
                        else
                            showMenu();
                    });
                }
            }
        });

        self.$element.on('mouseleave', 'tbody tr:not(.row-menu)', function (e) {
            self.target = $(this);
            self.menuContainer.hide();
        });

        self.$element.on('click', '.rm-button', function (e) {
            var el = $(this).find('a');
            var url = el.attr('data-url');

            if (url) {
                if (el.data('open-in-radwindow')
                    && el.data('open-in-radwindow').toString().toLowerCase() === "true") {
                    var title = el.data("title");

                    Common.UI.ShowWindow(title, url, 'registerModalWindow',
                        el.data('need-refresh').toString().toLowerCase() === "true" && self.grid ?
                            function () { self.grid.dataSource.read(); }
                            : null,
                        el.data('window-width'),
                        el.data('window-height'));
                }
                else if (el.data('open-in-new-window')
                    && el.data('open-in-new-window').toString().toLowerCase() === "true") {
                    var newWindow = window.open(url, '_blank');
                    newWindow.focus();
                }
                else {
                    window.location.href = url;
                }
            }
        });

        /* end */

        function log(msg, warn) {
            if (window.console) {
                var message = '$.fn.rowMenu -> ' + msg;
                if (warn) {
                    console.warn(message);
                } else {
                    console.log(message);
                }
            }
        };

        function LoadContent() {
            if (self.loadContentUrl) {
                $.ajax({
                    url: self.loadContentUrl,
                    type: 'GET',
                    data: { registerViewId: self.registerViewId, uniqueSessionKey: self.registerViewSettings.UniqueSessionKey },
                    success: function (response) {
                        if (response) {
                            self.menuContainer.append(response.Value);
                            var li_els = self.menuContainer.find('li');
                            if (li_els.length)
                                self.menuContainer.css('width', 55 * li_els.length + 'px');

                            self.menuContainer.kendoTooltip({
                                position: "bottom",
                                filter: "li",
                                content: function (e) { return e.target.find('a').data("title") }
                            });

                            self.grid.bind("dataBound", function () {
                                self.menuContainer.hide();
                            });
                        }
                    },
                    error: function (request) {
                        log('ошибка загрузки контента, url: ' + self.loadContentUrl + ' ' + request.status + ' ' + request.statusText, true);
                    }
                });
            }
        };


        function getCoords(elem) {
            return {
                top: elem.offsetTop,
                left: elem.offsetLeft
            };

        };

        function toggleHideButton(el, state) {
            if (el.data('hidden') != state) {
                el.closest('li').toggle(!state);
                var $width = self.menuContainer.width();
                self.menuContainer.css('width', state ? $width - 60 : $width + 60);

                var leftPos = self.menuContainer.css('left');
                var $left = +leftPos.substr(0, leftPos.lastIndexOf("px"))
                self.menuContainer.css('left', state ? $left + 60 : $left - 60);
                el.data('hidden', state);
            }
        }

        function updateButtonsState(states, callBackFunc) {
            if (states) {
                for (var i = 0; i < states.length; i++) {
                    var buttonState = states[i],
                        buttonSelector = '[data-button-id=' + buttonState.Id + ']';

                    var buttonEl = null;

                    if (buttonState.OwnerDropdownButtonId) {
                        buttonEl = self.$element
                            .find('[data-button-id=' + buttonState.OwnerDropdownButtonId + ']')
                            .find(buttonSelector);

                        buttonEl.parent().toggleClass("hidden", buttonState.Hidden);
                    }
                    else {
                        buttonEl = self.$element
                            .find(buttonSelector)
                            .not('[data-owner-dropdown-button-id]');

                        toggleHideButton(buttonEl, buttonState.Hidden);
                    }


                    if (buttonState.Url) {
                        //не изменяем сам аттрибут, только jquery.cache
                        buttonEl.attr('data-url', buttonState.Url);
                    }
                    if (buttonState.WindowTitle) {
                        buttonEl.attr('data-window-title', buttonState.WindowTitle);
                    }

                    buttonEl.closest('li').toggleClass("rm-el-disable", !buttonState.Enable);
                }

                if (callBackFunc)
                    callBackFunc();
            }
        };

        function updateStates(objId, callBackFunc) {
            $.ajax({
                url: self.checkConditionsUrl,
                type: 'GET',
                data: { objectId: objId, registerViewId: self.registerViewSettings.CurrentRegisterViewId, uniqueSessionKey: self.registerViewSettings.UniqueSessionKey },
                dataType: 'json',
                success: function (states) {
                    if (states) {
                        updateButtonsState(states, callBackFunc);
                    }
                }
            })
        };
    }

    $.fn.rowMenu = function (config) {
        return this.each(function () {
            if (!$.data(this, 'rowMenu')) {
                $.data(this, 'rowMenu', new rowMenu(this, config));
            }
        });
    }
})(jQuery);