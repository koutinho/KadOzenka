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
        self.loadContentUrl = self.baseUrl + 'CoreUi/GetRowMenuContent';
        self.filter = config.filter;
        self.delay = config.delay;
        self.registerViewId = config.registerViewId;
        self.registerId = config.registerId;
        self.registerViewSettings = window['registerViewSettings_' + config.registerId];

        self.menuContainer = $('<div class="rm-container" style="display: none"></div>');
        self.$element.append(self.menuContainer);

        self.target = undefined;
        self.grid = self.$element.closest('.register-grid').data('kendoGrid');

        LoadContent();

        /* События */

        self.$element.on('mouseover', self.filter, function (e) {
            if (!$(this).is(self.target)) {
                self.target = $(this);

                var dataItem = self.grid.dataItem($(this));

                if (dataItem) {
                    self.menuContainer.find('a[data-url]').each(function () {
                        $(this).attr('data-url', $(this).data('url').replace('[objectId]', dataItem.ID));
                    });
                }

                var coord = getCoords(this);
                var dx = $(this).width() - self.menuContainer.width() - 1;

                self.menuContainer.css('top', coord.top)
                    .css('left', dx)
                    .css('height', $(this).height() - 4);

                var showMenu = () => self.menuContainer.show();

                if (self.delay)
                    setTimeout(showMenu, self.delay);
                else
                    showMenu();
            }
        });

        self.$element.on('mouseleave', function (e) {
            self.target = $(this);
            self.menuContainer.hide();
        });

        self.$element.on('click', '.rm-button', function (e) {
            var el = $(this).find('a');
            var url = el.attr('data-url');

            if (url) {
                if (el.data('open-in-radwindow')
                    && el.data('open-in-radwindow').toString().toLowerCase() === "true") {
                    var title = el.attr("title");

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
            if(self.loadContentUrl) {
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
    }

    $.fn.rowMenu = function (config) {
        return this.each(function () {
            if (!$.data(this, 'rowMenu')) {
                $.data(this, 'rowMenu', new rowMenu(this, config));
            }
        });
    }
})(jQuery);