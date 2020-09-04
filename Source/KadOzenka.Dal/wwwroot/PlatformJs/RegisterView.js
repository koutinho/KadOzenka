// $_GET определаяется в scripts.js, но в IE 11 возникает ошибка, так как 2 JS-файла загружаются параллельно
var $_GET = $_GET || (function () {
    var get = {};
    if (document.location.toString().indexOf('?') !== -1) {
        var query = document.location
            .toString()
            // get the query string
            .replace(/^.*?\?/, '')
            // and remove any existing hash string (thanks, @vrijdenker)
            .replace(/#.*$/, '')
            .split('&');

        for (var i = 0, l = query.length; i < l; i++) {
            var aux = decodeURIComponent(query[i]).split('=');
            get[aux[0]] = aux[1];
        }
    }

    return get;
})();


var RegisterView = RegisterView ||
{

	Common: {
		/**
		*@namespace
		*@property {object}  Constant                       Объект хранящий свойства которые передаются при инициализации InitRegisterView
		*@property {string}  Constant.searchWindowSelector
		*/
		Constant: {
			searchWindowSelector: ""
		},

		/**
		 * 
		 * @param {string} windowSelector селектор kendo window
		 */
		ShowSearchPanel: function (windowSelector = null) {
			if (windowSelector === null) {
				console.error("Не указан селектор");
				return;
			}
			var searchWindow = $(windowSelector).data('kendoWindow');
			if (searchWindow)
				searchWindow.center().open();
		},

		/**
		 * 
		 * @param {string} windowSelector селектор kendo window
		 */
		DeleteCharactersMaskTextBox: function (windowSelector = null) {
			if (windowSelector === null) {
				console.error("Не указан селектор");
				return;
			};
			$(windowSelector).find('[data-role=maskedtextbox]').off('blur');
			$(windowSelector).find('[data-role=maskedtextbox]').on('blur', function () {
				$(this).val($(this).val().replace(/_/gi, ''));
			});
		}
	},

	InitRegisterView: function (registerViewSettings) {

		var clearSelection = false,
			pageChanged = false,
			isModelTransition = registerViewSettings.IsModelTransition,

			splitterSelector = "#verticalSplitter-" + registerViewSettings.CurrentRegisterId,
			gridSelector = "#Grid-" + registerViewSettings.CurrentRegisterId,
			tabStripSelector = "#TabStrip-" + registerViewSettings.CurrentRegisterId,
			gridToolbarSelector = "#GridToolBar-" + registerViewSettings.CurrentRegisterId,
			searchWindowSelector = "#SearchWindow-" + registerViewSettings.CurrentRegisterId,
			searchWindowToolbarSelector = "#SearchWindowToolBar-" + registerViewSettings.CurrentRegisterId,
			searchPanelContentSelector = "#menu-search-panel-content-" + registerViewSettings.CurrentRegisterId,
			sidePanelSelector = "#sidePanel-" + registerViewSettings.CurrentRegisterId,
			showRefreshButtonSelector = "#showRefreshButton-" + registerViewSettings.CurrentRegisterId,
			menuFiltersGridSelector = "#menu-filters_grid-" + registerViewSettings.CurrentRegisterId,
			footerStatusSelector = "#footerStatus-" + registerViewSettings.CurrentRegisterId,
			settingListBtnSelector = '#ShowSettingListWindow-' + registerViewSettings.CurrentRegisterId,

			registerId = registerViewSettings.RegisterId,
			registerViewId = registerViewSettings.RegisterViewId,
			searchPanelWindowWidth = registerViewSettings.SearchPanelWindowWidth,
			searchPanelWindowHeight = registerViewSettings.SearchPanelWindowHeight,
			filterRegisterId = registerViewSettings.FilterRegisterId,
			columnWidthType = registerViewSettings.ColumnWidthType,

			callBackFunction = $_GET['CallBackFunction'],
			splitter = $(splitterSelector).data('kendoSplitter'),
			gridEl = $(gridSelector),
			grid = gridEl.data('kendoGrid'),
			tabStripEl = $(tabStripSelector),
			tabStrip = tabStripEl.data('kendoTabStrip'),
			gridToolbar = $(gridToolbarSelector).data('kendoToolBar'),
			searchWindow = $(searchWindowSelector).data('kendoWindow');

		// Заполнение Common.Constant
		RegisterView.Common.Constant.searchWindowSelector = searchWindowSelector;

		///////////////////////////////////////////////
		// Перенесено из Index.cshtml

		$(window).on('resize',
			function () {
				grid.resize();
			});

		if (columnWidthType == '1') {
			$(".contentPanel").css("display", "block");
		}

		//менеджер фильтрации
		var registerFilters = window.registerFilters = new function () {
			var _currentFilters = [];

			var descriptorKeys = [
				'field',
				'operator',
				'value'
			];
			//TODO добавить флаг использовать Transion

			//?? неявно соответствует перечислению KapRemontWebMain._MVC.Model.FilterType
			this.filterTypes = {
				Dynamic: 0,
				Transition: 1
			};

			this.getAllFilters = function () {
				return _currentFilters;
			}

			this.set = function (filterType, filterDescriptor) {
				switch (filterType) {
					case this.filterTypes.Transition:
						_setTransitionFilter.call(this, filterDescriptor);
						break;
					case this.filterTypes.Dynamic:
						_setDynamicFilter.call(this, filterDescriptor);
						break;
				}
			};

			function _createFilterDescriptor(filterDescriptorConfig) {
				var filterDescriptor = {};

				for (key in filterDescriptorConfig) {
					var lowerKey = Common.Functions.LowercaseFirstLetter(key);
					if (descriptorKeys.indexOf(lowerKey) > -1) {
						filterDescriptor[lowerKey] = filterDescriptorConfig[key];
					}
				}

				if ($.isEmptyObject(filterDescriptor)) {
					return null;
				}
				return filterDescriptor;
			}

			function _setTransitionFilter(filterDescriptorConfig) {
				var filterDescriptor = _createFilterDescriptor.call(this, filterDescriptorConfig);
				if (filterDescriptor) {
					var currentTransionFilter = null;
					for (var i = 0; i < _currentFilters.length; i++) {
						if (_currentFilters[i].filterType = this.filterTypes.Transition) {
							currentTransionFilter = _currentFilters[i];
						}
					}

					if (!currentTransionFilter) {
						var length = _currentFilters.push({
							filterType: this.filterTypes.Transition
						});
						currentTransionFilter = _currentFilters[length - 1];
					}

					if (currentTransionFilter.filterDescriptors && currentTransionFilter.filterDescriptors.length > 0) {
						currentTransionFilter.filterDescriptors[0] = filterDescriptor;
					} else {
						currentTransionFilter.filterDescriptors = [filterDescriptor];
					}
				}
			}

			function _setDynamicFilter(filterDescriptorConfig) {
				var filterDescriptor = _createFilterDescriptor.call(this, filterDescriptorConfig);

				if (filterDescriptor && filterDescriptor.field) {
					var dynamicFilters = null;
					var currentFieldFilterDescription = null;

					for (var i = 0; i < _currentFilters.length; i++) {
						if (_currentFilters[i].filterType =
							this.filterTypes.Dynamic && _currentFilters[i].filterDescriptors) {
							dynamicFilters = _currentFilters[i];
						}
					}

					if (!dynamicFilters) {
						var length = _currentFilters.push({
							filterType: this.filterTypes.Dynamic
						});
						dynamicFilters = _currentFilters[length - 1];
					}

					if (dynamicFilters.filterDescriptors) {
						for (var i = 0; i < dynamicFilters.filterDescriptors.length; i++) {
							if (dynamicFilters.filterDescriptors[i].field === filterDescriptor.field) {
								currentFieldFilterDescription = dynamicFilters.filterDescriptors[i];
							}
						}
					} else {
						dynamicFilters.filterDescriptors = [];
					}

					if (currentFieldFilterDescription) {
						currentFieldFilterDescription = filterDescriptor;
					} else {
						dynamicFilters.filterDescriptors.push(filterDescriptor);
					}
				}
			}
		};

		//ресайзы панели поиска, грида, табов, фреймов и т.д.
		function setSearchWindowSize() {
			if (searchWindow) {
				var body = document.getElementsByTagName('body')[0];

				if (searchPanelWindowWidth) {
					var width;
					if (searchPanelWindowWidth.indexOf('%') > -1) {
						width = getPercentageWidth(body, searchPanelWindowWidth);
					} else {
						width = parseFloat(searchPanelWindowWidth);
					}

					if (width && !isNaN(width)) {
						searchWindow.wrapper.css({ width: width })
					}
				}

				if (searchPanelWindowHeight) {
					var height;
					if (searchPanelWindowHeight.indexOf('%') > -1) {
						height = getPercentageHeight(body, searchPanelWindowHeight);
					} else {
						height = parseFloat(searchPanelWindowHeight);
					}
					if (height && !isNaN(height)) {
						searchWindow.wrapper.css({ height: height })
					}
				}

				if (registerViewSettings.NeedOpenEmpty && !registerViewSettings.NewDisign) searchWindow.center().open();
			}
		}

		function getPercentageWidth(containerHtmlElement, percentageSizeString) {
			return getPercentageSize(containerHtmlElement.clientWidth, percentageSizeString);
		}

		function getPercentageHeight(containerHtmlElement, percentageSizeString) {
			return getPercentageSize(containerHtmlElement.clientHeight, percentageSizeString);
		}

		function getPercentageSize(containerSize, percentageString) {
			if (containerSize && !isNaN(containerSize)) {
				var percentage = parseFloat(percentageString.replace('%', ''));
				if (!isNaN(percentage)) {
					return containerSize * (percentage / 100);
				}
			}
			return null;
		}

		function resizeTabStripContent() {
			return;

			var iframeElements = tabStripEl.find('iframe.k-tabstrip-iframecontent'),
				//берем высоту текущего таба для всех
				activeTab = tabStripEl.find('.k-content.k-state-active');
			$.each(iframeElements,
				function () {
					//доработки для iframe
					var iframe = this,
						contentDocument = iframe.contentDocument || iframe.contentWindow.document;
					/*iframe.style.visibility = "hidden";*/
					//удаляем 10 пикселей для отсутствия скролла
					var newHeight = activeTab.height();
					var newWidth = activeTab.width();
					/*iframe.style.height = newHeight + "px";
					iframe.style.width = newWidth + "px";
					iframe.style.visibility = "visible";*/

					//доработка для бага хрома
					//https://bugs.chromium.org/p/chromium/issues/detail?id=641881
					if (contentDocument.body && contentDocument.body.scrollHeight > newHeight) {
						contentDocument.body.style.display = "none";
						$(contentDocument.body).css("overflowY", "auto");
						setTimeout(function () {
							contentDocument.body.style.display = "block";
						},
							0);
					}
				});
			tabStrip.resize(true);
		}

		function loadTabStripActiveTab() {
			var activeTabIframe = tabStripEl.find('.k-content.k-state-active .k-tabstrip-iframecontent');
			if (activeTabIframe.length > 0) {
				var currentSrc = activeTabIframe.attr('src'),
					dataSrc = activeTabIframe.data('src');

				if (dataSrc && dataSrc !== currentSrc) {
					activeTabIframe.attr('src', dataSrc);
				}
			}
		}

		//модальное окно kendo ui
		function OpenInKendoWindow(contentUrl, title, width, height, needRefresh) {
			var windowPlacehoder = $('<div id="registerModalWindow"></div>'),
				needMaximize = !width && !height,
				config = {
					title: title,
					iframe: true,
					modal: true,
					//appendTo: window.parent.$('body'),
					content: contentUrl,
					close: function (e) {
						if (needRefresh === true) {
							grid.dataSource.read();
						}
						this.destroy();
					}
				};

			config.height = height || "auto";
			config.width = width || "auto";

			//Нужно открывать окно в родительском фрейме !!!!!
			//прикрепляем окно к html body родительского окна
			$('body').append(windowPlacehoder);

			var kendoWindow = windowPlacehoder.kendoWindow(config).data('kendoWindow');

			//события
			/*window.element.find('iframe.k-content-frame').on('load', function () {
			    //доработки для iframe
			    var iframe = this,
			        contentDocument = iframe.contentDocument ? iframe.contentDocument : iframe.contentWindow.document;
			    iframe.style.visibility = "hidden";
			    //10 пикселей добавляем для отсутствия скролла
			    var newHeight = $(iframe).closest('.k-window-iframecontent').height();
			    iframe.style.height = newHeight + "px";
			    iframe.style.visibility = "visible";
			});*/

			kendo.ui.progress(windowPlacehoder, true);
			kendoWindow.bind('refresh',
				function () {
					kendo.ui.progress(windowPlacehoder, false);
				});

			kendoWindow.center();

			/*var documentWindow = $(window.parent);
			kendoWindow.setOptions({
			    position: {
			        top: documentWindow.scrollTop() + Math.max(0, (documentWindow.height() - windowPlacehoder.height()) / 2.1),
			        left: documentWindow.scrollLeft() + Math.max(0, (documentWindow.width() - windowPlacehoder.width()) / 2)
			    }
			});*/

			kendoWindow.open();

			/*var kwnd = kendoWindow.element.closest('.k-widget.k-window');
			var overlay = kwnd.prev('.k-overlay');
			if (kwnd.length && overlay.length) {
			    overlay.css('z-index', kwnd.css('z-index'));
			}*/

			if (needMaximize) {
				kendoWindow.maximize();
			}
		}

		//обновление состояния реестра после изменения строки
		function UpdateToolbarState(toolBarButtonStates) {
			$(gridSelector).find("input.dropdown-toolbar-button").each(function () {
				if ($(this).data("kendoDropDownList")) {
					$(this).data("kendoDropDownList").dataSource.data([]);
				}
			});

			if (gridToolbar && toolBarButtonStates) {
				for (var i = 0; i < toolBarButtonStates.length; i++) {
					var buttonState = toolBarButtonStates[i],
						toolbarButtonSelector = '[data-button-id=' + buttonState.Id + ']';

					if (buttonState.OwnerDropdownButtonId) {
						var dropDownEl = $(gridToolbar.element)
							.find('[data-button-id=' + buttonState.OwnerDropdownButtonId + ']')
							.not('[data-owner-dropdown-button-id]'),
							dropDown = dropDownEl.data("kendoDropDownList");

						if (dropDown) {
							var targetItem = dropDownEl.data('dropdown-items').filter(function (item) {
								if (item.htmlAttributes["data-button-id"] == buttonState.Id) {
									return item;
								}
							})[0];

							dropDown.dataSource.add(targetItem);
							dropDown.refresh();

							var buttonEl = dropDown.list.find(toolbarButtonSelector).closest('.k-item'),
								dataItem = dropDown.dataItem(buttonEl);

							if (buttonState.Hidden) {
								dropDown.dataSource.remove(dataItem);
							}

							dataItem.set('enable', buttonState.Enable);

							if (buttonState.Url) {
								dataItem.get('htmlAttributes')['data-url'] = buttonState.Url;
							}
							if (buttonState.WindowTitle) {
								dataItem.get('htmlAttributes')['data-window-title'] = buttonState.WindowTitle;
							}
						}
					} else {
						var buttonEl = $(gridToolbar.element)
							.find(toolbarButtonSelector)
							.not('[data-owner-dropdown-button-id]');

						//доработка для dropdown'ов
						if (buttonEl.hasClass('dropdown-toolbar-button')) {
							buttonEl = buttonEl.closest('[data-uid]');
						}

						if (buttonState.Hidden) {
							gridToolbar.hide(buttonEl);
						} else {
							gridToolbar.show(buttonEl);
						}

						if (buttonState.Url) {
							//не изменяем сам аттрибут, только jquery.cache
							buttonEl.attr('data-url', buttonState.Url);
						}

						if (buttonState.WindowTitle) {
							buttonEl.attr('data-window-title', buttonState.WindowTitle);
						}

						gridToolbar.enable(buttonEl, buttonState.Enable);
					}
				}
			}
		}

		function UpdateTabStripState(tabStripStates) {
			if (tabStripStates && tabStrip) {
				var selectedTabIndex = tabStrip.select().index();
				var selectedContentUrl;
				var selectedElement;

				var firstVisibleTabIndex;
				var firstVisibleContentUrl;
				var firstVisibleElement;


				var changeSelectedTabStrip;

				for (var i = 0; i < tabStripStates.length; i++) {
					var tabStripState = tabStripStates[i],
						tabSelector = '.k-item[role="tab"][data-tab-id=' + tabStripState.Id + ']',
						tabEl = $(tabStrip.element).find(tabSelector);

					if (tabEl.length > 0) {
						var tabContentId = tabEl.attr('aria-controls');
						if (tabContentId) {
							var tabContentEl = $(tabStrip.element).find('#' + tabContentId),
								tabIndex = tabContentEl.index() - 1;

							if (tabStripState.Visible) {
								tabEl.show();
								if (firstVisibleTabIndex == undefined) {
									firstVisibleTabIndex = tabIndex;
									firstVisibleContentUrl = tabStripState.ContentUrl;
									firstVisibleElement = tabContentEl;
								}

								if (tabIndex == selectedTabIndex) {
									selectedContentUrl = tabStripState.ContentUrl;
									selectedElement = tabContentEl;
								}
							} else {
								tabEl.hide();
								if (selectedTabIndex == tabIndex) {
									changeSelectedTabStrip = true;
								}
							}

							tabStrip.enable(tabEl, tabStripState.Enabled);

							tabContentEl.find('iframe.k-tabstrip-iframecontent').data('src', tabStripState.ContentUrl);
						}
					}
				}

				var contentUrl;

				if (changeSelectedTabStrip && firstVisibleElement) {
					//если url пустой загружаем пустую страницу браузера
					firstVisibleElement.find('iframe.k-tabstrip-iframecontent')
						.attr('src', firstVisibleContentUrl || 'about:blank');

					tabStrip.select(firstVisibleTabIndex);
				} else {
					if (selectedElement) {
						//если url пустой загружаем пустую страницу браузера
						selectedElement.find('iframe.k-tabstrip-iframecontent')
							.attr('src', selectedContentUrl || 'about:blank');
					}
				}

				tabStrip.trigger("contentLoad");
			}
		}

		//обработчики событий
		function onGridDataBound(e) {
			var rows = this.tbody.find('tr');
			if (rows.length > 0) {
				rows.first().addClass('k-state-selected');
			} else if (tabStrip && tabStrip.element) {
				$(tabStrip.element).find('.k-item[role = "tab"]').hide();

				//если грид пустой то, загружаем в карточку пустую страницу браузера
				$(tabStrip.element).find('iframe.k-tabstrip-iframecontent').each(function () {
					$(this).attr('src', 'about:blank');
				});
			}
			this.trigger("change");
		}

		function onGridSelectionChange(e) {
			var selectedRow = this.select(),
				dataItem = this.dataItem(selectedRow),
				objectId = dataItem ? dataItem.id : null,
				parameters = {
					registerId: registerId,
					objectId: objectId,
					registerViewId: registerViewSettings.RegisterViewId,
					uniqueSessionKey: registerViewSettings.UniqueSessionKey,
					callBackFunction: callBackFunction
				};

			if (filterRegisterId) {
				parameters.filterRegisterId = filterRegisterId
			}

			var requestUrl = $(gridSelector).parent().parent().parent().find('[name=RequestUrl]').val();

			var urlParameters = Common.GetUrlParameters(requestUrl);
			if (urlParameters.length > 0) {
				for (var i = 0; i < urlParameters.length; i++) {
					if (!Common.IsExistsKeyInObject(parameters, urlParameters[i].Name)) {
						parameters[urlParameters[i].Name] = urlParameters[i].Value;
					}
				}
			}

			$.ajax({
				url: registerViewSettings.RegistersUpdateStateUrl,
				type: 'GET',
				data: parameters,
				dataType: 'json',
				success: function (states) {
					if (states) {
						UpdateToolbarState(states.toolbarButtonStates);
						UpdateTabStripState(states.tabStripStates);
					}
				}
			});
		}

		function onSplitterResize(e) {
			resizeTabStripContent();
		}

		function onTabStripContentLoad(e) {
			resizeTabStripContent();
		}

		function onTabStripTabShow(e) {
			loadTabStripActiveTab();
			resizeTabStripContent();
		}

		function onGridToolbarItemClick(e) {
			var exportToFile = function (actionUrl, type) {
				var parameters = grid.GetDataFunc();
				parameters.CurrentLayoutId = registerViewSettings.CurrentLayoutId;
				var url = actionUrl +
					'?parametersJson=' +
					encodeURIComponent(JSON.stringify(parameters)) +
					'&coreExportType=' +
					type;
				window.open(url, '_blank');
				return;
			}

			e.preventDefault();

			if (e.target.data('command')) {
				var rowCount = grid.dataSource.total();
				var rowLimit = e.target.data('exportLimit');
				var type = 'Xlsx';

				if (e.target.data('command') === 'ExportToExcel' || e.target.data('command') === 'ExportToDBF') {
					if (e.target.data('command') === 'ExportToExcel')
						type = 'Xlsx'

					if (e.target.data('command') === 'ExportToDBF')
						type = 'Dbf';

					if (rowLimit && rowLimit > 0 && rowCount > rowLimit) {
						Common.UI.ShowConfirm({
							title: 'Внимание!',
							content:
								'Объем выгружаемых данных слишком большой. Запустить формирование выгрузки в фоновом режиме через менеджер выгрузок?',
							onSuccess: function (e) {
								var parameters = grid.GetDataFunc();
								parameters.CurrentLayoutId = registerViewSettings.CurrentLayoutId;
								var parametersJson = encodeURIComponent(JSON.stringify(parameters));

								$.ajax({
									type: 'POST',
									url: registerViewSettings.RegistersExportBackgroundProcessUrl,
									data: { parametersJson: parametersJson, coreExportType: type },
									success: function (result) {
										Common.UI.ShowDialog({
											content: 'Выгрузка успешно запущена',
											height: 130,
											icon: 'info',
											showCloseBtn: true
										});
									},
									error: function (result) {
										Common.UI.ShowErrorDialog(result.responseText);
									}
								});
							}
						});
					} else {
						exportToFile(registerViewSettings.RegistersExportFileUrl, type);
					}
				}
			}

			var url = e.target.attr('data-url');
			if (url) {
				if (e.target.data('open-in-radwindow') &&
					e.target.data('open-in-radwindow').toString().toLowerCase() === "true") {
					var title = e.target.attr('data-window-title')
						? e.target.attr('data-window-title')
						: e.target.html();

					OpenInKendoWindow(url,
						title,
						e.target.data('window-width'),
						e.target.data('window-height'),
						e.target.data('need-refresh').toString().toLowerCase() === "true"
					);
				} else if (e.target.data('open-in-new-window') &&
					e.target.data('open-in-new-window').toString().toLowerCase() === "true") {
					var newWindow = window.open(url, '_blank');
					newWindow.focus();
				} else {
					window.location.href = url;
				}
			}
		}

		//orientation = "horizontal" : "vertical"
		function changeObjectCardOrientation(orientation) {
			var splitter = $(splitterSelector).data("kendoSplitter");

			if (splitter.orientation == orientation)
				return;

			var oldMarker = splitter._marker;

			splitter.element.children(".k-splitbar").remove();
			splitter.orientation = splitter.options.orientation = orientation;
			$(splitterSelector).kendoSplitter({
				orientation: splitter.orientation,
				panes: [{ collapsible: true }, { collapsible: true }],
				resize: onSplitterResize,
				collapse: onSplitterResize,
				expand: onSplitterResize
			});

			if (orientation == "vertical") {
				$(splitterSelector + " .k-pane:last").css({ left: "0px" });
			}

			var pos = $._data($(window)[0], "events").resize.map(function (e) { return e.namespace; })
				.indexOf('kendoSplitter' + oldMarker);
			$._data($(window)[0], "events").resize.splice(pos, 1);

			updateSplitterSettings(orientation, getSplitterSize());

			var toolbar = $("#GridToolBar-" + registerViewSettings.CurrentRegisterId).data("kendoToolBar");

			toolbar.enable($("#objectCardRight-" + registerViewSettings.CurrentRegisterId),
				orientation != "horizontal");
			toolbar.enable($("#objectCardBottom-" + registerViewSettings.CurrentRegisterId), orientation != "vertical");

			// Почему-то без этого groupButton не закрывается после блокировки кнопки
			$("#gearButton-" + registerViewSettings.CurrentRegisterId + "_wrapper .k-split-button-arrow").click()
		}

		function InitSearchMethode() {
			if (registerViewSettings.NewDisign && registerViewSettings.UseMasterPage) {
				var $settingLayoutBtn = $('#ShowSettingLayoutWindow-' + registerViewSettings.CurrentRegisterId);
				if (!$settingLayoutBtn.length) {
					$(gridToolbarSelector).find('.k-split-button')
						.after('<a href="" id="ShowSettingLayoutWindow-' +
							registerViewSettings.CurrentRegisterId +
							'" title="Настройка раскладки" class="k-button k-button-icon"><span class="k-icon k-i-zoom"></span></a>');
					RegisterView.InitSettingsLayout(registerViewSettings);
				}

				if (registerViewSettings.ShowSearchPanelButton) {
					var $searchFilter = $('.search-filter');
					if ($searchFilter.length)
						$searchFilter.remove();

					var $searchBtn = $('#ShowSearchPanelButton-' + registerViewSettings.CurrentRegisterId);
					if ($searchBtn.length)
						$searchBtn.show();
					else {
						$(gridToolbarSelector)
							.find(settingListBtnSelector)
							.after('<a href="" id="ShowSearchPanelButton-' +
								registerViewSettings.CurrentRegisterId +
								'" title="Поиск" class="k-button k-button-icon" ><span class="k-icon k-i-zoom"></span></a>');

						$('#ShowSearchPanelButton-' + registerViewSettings.CurrentRegisterId).on('click',
							function () {
								RegisterView.Common.ShowSearchPanel('#SearchWindow-' + registerViewSettings.CurrentRegisterId);
						});
					}
					if ($searchBtn && registerViewSettings.NeedOpenEmpty) {
						setTimeout(function () {
							RegisterView.Common.ShowSearchPanel('#SearchWindow-' + registerViewSettings.CurrentRegisterId);
						}, 0);
					}
					RegisterView.Common.DeleteCharactersMaskTextBox('#SearchWindow-' + registerViewSettings.CurrentRegisterId);
				} else {
					if ($(searchWindowToolbarSelector + ' #ClearSearchButton').data('clear'))
						$(searchWindowToolbarSelector + ' #ClearSearchButton').data('clear')();

					$('#ShowSearchPanelButton-' + registerViewSettings.CurrentRegisterId).hide();

					$(gridToolbarSelector)
						.find('#ShowSettingLayoutWindow-' + registerViewSettings.CurrentRegisterId)
						.after(
							'<div class="search-filter" style="width: calc(100% - 120px);padding: 15px 15px 15px 0"></div>');

					$('.search-filter').registerViewSearch(
						{
							registerViewId: registerViewSettings.CurrentRegisterViewId,
							registerId: registerViewSettings.CurrentRegisterId,
							baseUrl: registerViewSettings.BaseUrl,
							filter: registerViewSettings.SearchFilter,
							needOpenEmpty: registerViewSettings.NeedOpenEmpty,
							registerSettings: registerViewSettings
						}
					);
				}
			}
		}

		$("#showSqlButton-" + registerViewSettings.CurrentRegisterId).bind("click",
			function (e) {

				var parameters = grid.GetDataFunc();
				parameters.CurrentLayoutId = registerViewSettings.CurrentLayoutId;

				var url = registerViewSettings.RegistersShowSqlUrl +
					'?parametersJson=' +
					fixedEncodeURIComponent(JSON.stringify(parameters));

				OpenInKendoWindow(url,
					"SQL-запрос",
					"500px",
					"600px",
					false
				);
			});

		$("#setLayoutButton-" + registerViewSettings.CurrentRegisterId).bind("click",
			function (e) {
				var url = registerViewSettings.CoreRegisterLayoutAllUrl;
				var title = 'Раскладки ' + registerViewSettings.CurrentRegisterViewTitle;
				Common.UI.ShowWindow(title, url);
			});

		$("#setQryButton-" + registerViewSettings.CurrentRegisterId).bind("click",
			function (e) {
				var url = registerViewSettings.CoreRegisterQryAllUrl;
				var title = 'Фильтры ' + registerViewSettings.CurrentRegisterViewTitle;
				Common.UI.ShowWindow(title, url);
			});

		$("#setListButton-" + registerViewSettings.CurrentRegisterId).bind("click",
			function (e) {
				var url = registerViewSettings.CoreRegisterListAllUrl;
				var title = 'Списки ' + registerViewSettings.CurrentRegisterViewTitle;
				Common.UI.ShowWindow(title, url);
			});

		$("#setPageSize-" + registerViewSettings.CurrentRegisterId).bind("click",
			function (e) {
				var windowPlacehoder = $('<div id="SetPageSizeWindow"></div>');
				$('body').append(windowPlacehoder);

				var url = registerViewSettings.CoreRegisterSetPageSize;
				var title = 'Установка количества строк на странице';

				var windowCfg = {
					title: title,
					content: url,
					iframe: false,
					modal: true,
					draggable: false,
					resizable: false,
					width: 400,
					height: 150,
					close: function () {
						this.destroy();
					},
					close2: function () {
						this.destroy();
					}
				};

				var modalWindow = windowPlacehoder.kendoWindow(windowCfg).data('kendoWindow');
				modalWindow.center().open();
			});

		$("#customSearch-" + registerViewSettings.CurrentRegisterId).bind("click",
			function (e) {
				updateSearchType(0);
				registerViewSettings.ShowSearchPanelButton = false;
				InitSearchMethode();
				var toolbar = $("#GridToolBar-" + registerViewSettings.CurrentRegisterId).data("kendoToolBar");
				if (toolbar) {
					toolbar.hide($("#customSearch-" + registerViewSettings.CurrentRegisterId));
					toolbar.show($("#fixedSearch-" + registerViewSettings.CurrentRegisterId));
				}
			});

		$("#fixedSearch-" + registerViewSettings.CurrentRegisterId).bind("click",
			function (e) {
				updateSearchType(1);
				registerViewSettings.ShowSearchPanelButton = true;
				InitSearchMethode();
				var toolbar = $("#GridToolBar-" + registerViewSettings.CurrentRegisterId).data("kendoToolBar");
				if (toolbar) {
					toolbar.hide($("#fixedSearch-" + registerViewSettings.CurrentRegisterId));
					toolbar.show($("#customSearch-" + registerViewSettings.CurrentRegisterId));
				}
			});

		$("#resetSortButton-" + registerViewSettings.CurrentRegisterId).bind("click",
			function (e) {
				grid.dataSource.sort({});
				if (grid && grid.resetSorting) {
					grid.resetSorting();
				}
			});

		$("#resetWidthButton-" + registerViewSettings.CurrentRegisterId).bind("click",
			function (e) {
				$("table", grid.element).css("width", "100%");
				$("col", grid.element).css("width", "100px");

				var $th = grid.element.find('th');
				$th.each(function (index) {
					grid.columns[index].width = $(this).width();
				});

				if (grid && grid.gridColumnResize) {
					grid.gridColumnResize();
				}
			});

		$("#useDefaultLayoutButton-" + registerViewSettings.CurrentRegisterId).bind("click",
			function (e) {
				window.location.href = registerViewSettings.UseDefaultLayoutUrl;
			});

		$("#objectCardRight-" + registerViewSettings.CurrentRegisterId).bind("click",
			function (e) {
				changeObjectCardOrientation("horizontal");
			});
		$("#objectCardBottom-" + registerViewSettings.CurrentRegisterId).bind("click",
			function (e) {
				changeObjectCardOrientation("vertical");
			});

		$("#objectCardOpen-" + registerViewSettings.CurrentRegisterId).bind("click",
			function (e) {
				var selectedItem = grid.dataItem(grid.select());

				var objId = selectedItem.id;
				var url = registerViewSettings.ObjectCardIndexUrl +
					'?ObjId=' +
					objId +
					'&RegisterViewId=' +
					registerViewSettings.CurrentRegisterViewId +
					'&useMasterPage=true';
				window.open(url, '_blank');
			});

		//подписи на события
		if (grid) {
			grid.bind("change", onGridSelectionChange);
			grid.bind("dataBound", onGridDataBound);
		}

		if (registerViewSettings.NeedOpenEmpty) {
			$(splitterSelector).hide();
			$(gridSelector).hide();
			if (searchWindow) {
			} else {
				var slideMenu = $('.movePanel[data-id="menu-search-panel"]');
				if (slideMenu === undefined || slideMenu == null || slideMenu.length <= 0) return;

				var isVisible = slideMenu.position().left > 0;
				if (isVisible) {
					kendo.fx(slideMenu).slideInLeft().play();
					slideMenu.removeClass("open");
				} else {
					slideMenu.addClass("open");
					kendo.fx(slideMenu).slideInLeft().reverse();
				}
			}
		}

		if (splitter) {
			splitter.bind("resize", onSplitterResize);
		}
		if (tabStrip) {
			tabStrip.bind("contentLoad", onTabStripContentLoad);
			tabStrip.bind("show", onTabStripTabShow);
		}

		if (gridToolbar) {
			gridToolbar.bind("click", onGridToolbarItemClick);
		}

		//обработка дропдаунов
		$(gridSelector).find(".dropdown-toolbar-button").each(function () {
			var dropDownEl = $(this);
			dropDownEl.kendoDropDownList({
				optionLabel: dropDownEl.data('dropdown-option-label'),
				dataTextField: "text",
				dataValueField: "url",
				dataSource: dropDownEl.data('dropdown-items'),
				autoWidth: true,
				height: "auto",
				template:
					'<span class="#: enable ? \'\' : \'k-state-disabled\' #" data-button-id="#: htmlAttributes[\'data-button-id\'] #">#: text #</span>',
				select: function (e) {
					e.preventDefault();

					if (e.dataItem.enable === true) {
						var url = e.dataItem.htmlAttributes['data-url'];
						if (url) {
							if (e.dataItem.htmlAttributes['data-open-in-radwindow'] === true) {
								OpenInKendoWindow(url,
									e.dataItem.text,
									e.dataItem.htmlAttributes['data-window-width'],
									e.dataItem.htmlAttributes['data-window-height'],
									e.dataItem.htmlAttributes['data-need-refresh']
								);
							} else if (e.dataItem.htmlAttributes['data-open-in-new-window'] === true) {
								var newWindow = window.open(url, '_blank');
								newWindow.focus();
							} else {
								window.location.href = url;
							}
						}
					}
				}
			});

			//скрываем название кнопки дропдауна из списка
			dropDownEl.getKendoDropDownList().optionLabel.hide();

			//доработка по скрытию элементов дропдауна при инициализации
			$(dropDownEl.getKendoDropDownList().listView.element).find('> li.k-item').each(function () {
				var dataItem = dropDownEl.getKendoDropDownList().dataSource.at($(this).index());
				if (dataItem && dataItem.hidden == true) {
					$(this).hide();
				}
			});
		});

		$(sidePanelSelector + ' #SearchWindowButton').on('click',
			function (e) {
				searchWindow.center().open();
			});

		setSearchWindowSize();

		$(searchWindowToolbarSelector + ' #SearchButton, #gearButton-' + registerViewSettings.CurrentRegisterId).on(
			'click',
			function (e) {

				// обнуляем счетчик загрузок данных
				if (registerViewSettings.UseDataReaderMode) {
					grid.content.scrollTop(0);
					registerViewSettings.ContentLoadCounter = 0;
				}

				//закрываем отдельное окно
				if (searchWindow) {
					searchWindow.close();
				}
				//закрываем слайдер
				var slideMenu = $('.movePanel.open[data-id="menu-search-panel"] .header .close');
				if (slideMenu.length > 0) {
					slideMenu.trigger('click');
				}

				if (registerViewSettings.NeedOpenEmpty) {
					$(splitterSelector).show();
					$(gridSelector).show();
					$("#MessageClickSearch").hide();
					registerViewSettings.NeedOpenEmpty = false;
				}

				if (grid) {
					if (grid.gridClearSelection != undefined && !registerViewSettings.SaveSelection)
						grid.gridClearSelection();
					searchAplied = true;
					grid.dataSource.read();
				}

				if (grid.element.closest(".k-splitter").length) {
					setTimeout(() => {
						$(window).resize();
					}, 100);
				}
			});

		$(searchWindowToolbarSelector).closest(searchPanelContentSelector + ',' + searchWindowSelector).keypress(
			function (e) {
				if (e.which == 13) {
					$(searchWindowToolbarSelector + ' #SearchButton').click();
				}
			});

		$(searchWindowToolbarSelector + ' #ClearSearchButton').on('click',
			function (e) {
				clearSearchElements();
			});
		$(searchWindowToolbarSelector + ' #ClearSearchButton').data('clear', clearSearchElements);


		$(searchWindowToolbarSelector + ' #CollapseAllBlocks').on('click',
			function (e) {
				collapseOrExpandAllBlocks();
			});


		$(searchWindowToolbarSelector + ' #DefaultSearchButton').on('click',
			function (e) {
				$.ajax({
					type: 'POST',
					url: registerViewSettings.RegistersGetDefaultValuesForSearchUrl,
					data: { registerId: registerId },
					dataType: 'json',
					success: function (result) {
						var elementList = $('[attributenumberid]');
						for (var i = 0; i < elementList.length; ++i) {
							if (elementList[i].getAttribute('data-role') == "dropdownlist") {
								$('#' + elementList[i].id).data('kendoDropDownList').value("");
							} else if (elementList[i].getAttribute('type') == "checkbox") {
								elementList[i].checked = false;
							} else {
								elementList[i].value = "";
							}
						}

						for (var item in result.Data) {
							var elem = $('#' + item);
							if (elem[0]) {
								if (elem[0].getAttribute('data-role') == "dropdownlist") {
									elem.data('kendoDropDownList').value(result.Data[item]);
								} else if (elem[0].getAttribute('type') == "checkbox") {
									elem[0].checked = (result.Data[item] == "true");
								} else if (elem[0].getAttribute('data-role') == 'datepicker') {
									elem.data('kendoDatePicker').value(kendo.parseDate(result.Data[item]));
								} else {
									elem.val(result.Data[item]);
								}
							}
						}
					},
					error: function (result) {
						Common.UI.ShowErrorDialog(result.responseText);
					}
				});
			});

		$(".footerPanel").hide();

		$(".window-footer-arrow-down").on('click',
			function (e) {
				$('.footerPanel').hide();
				$(".window-footer-arrow-up").show();
				if (splitter) {
					splitter.resize();
				}
				if (grid) {
					grid.resize();
				}
			});

		$(".window-footer-arrow-up").on('click',
			function (e) {
				$('.footerPanel').show();
				$(".window-footer-arrow-up").hide();
				if (splitter) {
					splitter.resize();
				}
				if (grid) {
					grid.resize();
				}
			});

		function clearSearchElements() {
			var elementList = $('[attributenumberid]');

			for (var i = 0; i < elementList.length; ++i) {
				if (elementList[i].getAttribute('data-role') == "dropdownlist") {
					$('#' + elementList[i].id).data('kendoDropDownList').value("");
				} else if (elementList[i].getAttribute('data-role') == 'combobox') {
					$('#' + elementList[i].id).data('kendoComboBox').value('');
				} else {
					elementList[i].value = "";
				}
			}
			clearSearchDynamicControls();
			clearSearchDynamicDateControls();
		}

		function clearSearchDynamicControls() {
			var elementList = $('[DynamicControlType]');

			for (var i = 0; i < elementList.length; ++i) {
				if (elementList[i].getAttribute('data-role') === 'numerictextbox') {
					$('#' + elementList[i].id).data("kendoNumericTextBox") &&
						$('#' + elementList[i].id).data("kendoNumericTextBox").value(null);
				} else {
					$('#' + elementList[i].id).val("");
				}
				var listData = $('#' + elementList[i].id).data('listData');
				if (listData) {
					$('#' + elementList[i].id).data('listData', { value: [] });
					$('#' + elementList[i].id + "_inputList").val(null);
				}
		

				var conditionElementSelector = '#' + elementList[i].id + '_condition';
				if ($(conditionElementSelector).data('kendoDropDownList')) {
					$(conditionElementSelector).data('kendoDropDownList').select(0);
				}
			}
		}

		/**
		 * очистка контролов дат
		 */
		function clearSearchDynamicDateControls() {
			var elementList = $('.dynamic-date-container');

			$.each(elementList, function(i, el) {
				$(el).find('[DynamicControlType]').length > 0 && $(el).find('[DynamicControlType]').data('kendoDropDownList').select(0);
				$(el).find('input[data-role="datepicker"]').length > 0 &&
					$(el).find('input[data-role="datepicker"]').toArray().forEach(function(dInput) {
						$(dInput).data('kendoDatePicker').value(null);
					});

				$(el).find('[DynamicControlType]').length > 0 &&
					$('#' + $(el).find('[DynamicControlType]')[0].id + '_quarter').data('kendoDropDownList').select(0);
			});
		}

		function collapseOrExpandAllBlocks() {
			var panels = $('#SearchWindow-' + registerViewSettings.CurrentRegisterId).find('.k-panelbar[id^="PanelBar-"]');

			var needExpand = true;
			if (panels.find('li[aria-expanded="true"]').length > 0 ? true : false) {
				needExpand = false;
			}


			for (var i = 0; i < panels.length; ++i) {
				if ($(panels[i]).data("kendoPanelBar")) {
					var panelBar = $(panels[i]).data("kendoPanelBar");

					needExpand
						? panelBar.expand($(panels[i]).find('li'))
						: panelBar.collapse($(panels[i]).find('li'));

				}
			}
		}

		window.registerStatusClick = function() {
			var url = registerViewSettings.CoreRegisterLayoutUrl;
			var currentRegisterId = registerViewSettings.CurrentRegisterId;
			if (currentRegisterId != "") {
				url += "&LayoutRegisterId=" + currentRegisterId;
			}
			var currentRegisterViewId = registerViewSettings.CurrentRegisterViewId;
			if (currentRegisterViewId != "") {
				url += "&RegisterViewId=" + currentRegisterViewId;
			}
			var layoutId = registerViewSettings.CurrentLayoutId;
			if (layoutId != "") {
				var layoutKey = "&CurrentLayoutId=" + layoutId;
				url += "&RequestObjectId=" + layoutId + layoutKey;
				url += "&Transition=1&93300100=" + layoutId;
			}
			var title = 'Раскладки ' + registerViewSettings.CurrentRegisterViewTitle;
			Common.UI.ShowWindow(title, url);
		}

		window.openQueriesWindow = function (id) {
			var url = registerViewSettings.CoreRegisterQryUrl;
			var currentRegisterId = registerViewSettings.CurrentRegisterId;
			if (currentRegisterId != "") {
				url += "&FilterRegisterId=" + currentRegisterId;
			}
			var currentRegisterViewId = registerViewSettings.CurrentRegisterViewId;
			if (currentRegisterViewId != "") {
				url += "&RegisterViewId=" + currentRegisterViewId;
			}

			if (id)
				url += "&Transition=1&93600100=" + id;

			var title = 'Фильтры ' + registerViewSettings.CurrentRegisterViewTitle;
			Common.UI.ShowWindow(title, url);
		}

		function getSplitterSize() {
			var splitter = $(splitterSelector).data("kendoSplitter");

			var splitterElement = splitter.element;
			var paneElement = splitterElement.find(".k-pane:first");

			var ratio;

			if (splitter.orientation == "vertical") {
				ratio = Math.ceil(paneElement.height() * 100 / splitterElement.height());
			} else {
				ratio = Math.ceil(paneElement.width() * 100 / splitterElement.width());
			}

			return ratio;
		}

		function onSplitterResize() {
			var splitter = $(splitterSelector).data("kendoSplitter");
			var newSize = getSplitterSize();

			updateSplitterSettings(splitter.orientation, newSize);
		}

		function updateSplitterSettings(newOrientation, newSize) {
			$.ajax({
				url: registerViewSettings.RegistersUpdateSplitterSettingsUrl,
				type: 'GET',
				data: {
					registerViewId: registerViewSettings.CurrentRegisterViewId,
					orientation: newOrientation,
					size: newSize
				},
				dataType: 'json'
			});
		}

		function updateSearchType(newType) {
			$.ajax({
				url: registerViewSettings.ToggleSearchTypeUrl,
				type: 'Post',
				data: {
					registerViewId: registerViewSettings.CurrentRegisterViewId,
					registerId: registerViewSettings.CurrentRegisterId,
					SearchType: newType
				},
				dataType: 'json'
			});
		}
	},

	InitGrid: function (registerViewSettings) {
		var clearSelection = false,
			pageChanged = false,
			isModelTransition = registerViewSettings.IsModelTransition,

			splitterSelector = "#verticalSplitter-" + registerViewSettings.CurrentRegisterId,
			gridSelector = "#Grid-" + registerViewSettings.CurrentRegisterId,
			tabStripSelector = "#TabStrip-" + registerViewSettings.CurrentRegisterId,
			gridToolbarSelector = "#GridToolBar-" + registerViewSettings.CurrentRegisterId,
			searchWindowSelector = "#SearchWindow-" + registerViewSettings.CurrentRegisterId,
			searchWindowToolbarSelector = "#SearchWindowToolBar-" + registerViewSettings.CurrentRegisterId,
			searchPanelContentSelector = "#menu-search-panel-content-" + registerViewSettings.CurrentRegisterId,
			sidePanelSelector = "#sidePanel-" + registerViewSettings.CurrentRegisterId,
			showRefreshButtonSelector = "#showRefreshButton-" + registerViewSettings.CurrentRegisterId,
			menuFiltersGridSelector = "#menu-filters_grid-" + registerViewSettings.CurrentRegisterId,
			footerStatusSelector = "#footerStatus-" + registerViewSettings.CurrentRegisterId,

			registerId = registerViewSettings.RegisterId,
			registerViewId = registerViewSettings.RegisterViewId,
			searchPanelWindowWidth = registerViewSettings.SearchPanelWindowWidth,
			searchPanelWindowHeight = registerViewSettings.SearchPanelWindowHeight,
			filterRegisterId = registerViewSettings.FilterRegisterId,
			columnWidthType = registerViewSettings.ColumnWidthType,

			callBackFunction = $_GET['CallBackFunction'],
			splitter = $(splitterSelector).data('kendoSplitter'),
			gridEl = $(gridSelector),
			grid = gridEl.data('kendoGrid'),
			tabStripEl = $(tabStripSelector),
			tabStrip = tabStripEl.data('kendoTabStrip'),
			gridToolbar = $(gridToolbarSelector).data('kendoToolBar'),
			searchWindow = $(searchWindowSelector).data('kendoWindow');

		////////////////////////////////////////////////////
		// Перенесено из Grid.cshtml

		var gridReadRequest;
		// признак, что надо выбирать все записи из основной таблицы
		grid.selectAllRecrods = false;
		// массив выбранных элементов из таблицы
		grid.checkedIds = [];

		$(window).resize(function () {
			grid.resize();
			if (grid.content)
				grid.content.css('height', '');
		});

		if (registerViewSettings.IsPartialView !== true && registerViewSettings.UseMasterPage === true)
			$(gridSelector).closest('.content').css('height', 'calc(100% - 52px)');

		InitSearchMethode();


		var linkTarget = Object.freeze({
			Modal: "_modal",
			ModalIframe: "_modalIframe"
		});

		var selectRow = function () {
			var checked = this.checked,
				dataItem = grid.dataItem($(this).closest("tr"));
			if (!grid.checkedIds.includes(dataItem.id) && checked) {
				grid.checkedIds.push(dataItem.id);
			} else if (grid.checkedIds.includes(dataItem.id) && !checked) {
				var index = grid.checkedIds.indexOf(dataItem.id);
				if (index > -1) {
					grid.checkedIds.splice(index, 1);
				}
			}
		};

		grid.bind('dataBound',
			function (e) {
				var view = this.dataSource.view(),
					gridThis = this;

				clearSelection = false;
				for (var i = 0; i < view.length; i++) {
					if (grid.checkedIds.includes(view[i].id)) {
						this.tbody.find("tr[data-uid='" + view[i].uid + "']").find(".k-checkbox").attr("checked", true);
					}
				}

				grid.table.on("click", ".k-checkbox", selectRow);

				//фильтр перехода
				var isTransition = $(sidePanelSelector + ' [name=IsTransition]').length > 0
					? $(sidePanelSelector + ' [name=IsTransition]').is(':checked')
					: isModelTransition;

				boldButton(GetSearchValues(), false, '#SearchWindowButton');
				boldButton(GetFilterValues(), isTransition, '#FiltersWindowButton');

				let $selectedItems = gridThis.SelectedFilter;
				if ($selectedItems && $selectedItems.length) {
					$.ajax({
						url: registerViewSettings.RegistersSelectRowListUrl,
						type: 'POST',
						data: { objectList: $selectedItems, uniqueSessionKey: registerViewSettings.UniqueSessionKey },
						success: function () {
							gridThis.trigger('change');
						}
					});
				}

				if (registerViewSettings.ShowRowMenu) {
					var $content;
					if (gridThis.content)
						$content = gridThis.content;
					else if (gridThis.element.find('tbody[role=rowgroup]').length)
						$content = gridThis.element;

					if ($content) {
						$content.rowMenu(
							{
								registerViewId: registerViewSettings.CurrentRegisterViewId,
								registerId: registerViewSettings.CurrentRegisterId,
								baseUrl: registerViewSettings.BaseUrl,
								filter: 'tr',
								delay: 100
							}
						);
					}
				}

				gridThis.element.find('input[id^=row-checkbox-]').on('click',
					function (e) {
						var checked = $(this).is(':checked'),
							row = $(this).closest('tr[role="row"]');

						var dataItem = gridThis.dataItem(row);
						dataItem['Selected'] = checked;
						dataItem.dirtyFields["Selected"] = checked;

						gridThis.element.find('#headerColumnCheckBox').prop('checked', false);

						$.ajax({
							url: registerViewSettings.RegistersSelectRowUrl,
							type: 'POST',
							data: {
								selected: checked,
								objectId: dataItem.id,
								uniqueSessionKey: registerViewSettings.UniqueSessionKey
							},
							success: function () {
								gridThis.trigger('change');
							}
						});
					});

				setFooterStatus();
				if (registerViewSettings.IsPartialView === true) {
					if (gridThis && gridThis.content) {
						gridThis.content.css('height', '100%');

						if (!gridThis.dataSource.data().length)
							gridThis.content.css('overflow', 'hidden');
					}
				}

				function ifViewLoadContent(elem) {
					if (!elem.hasClass("ImLoaded")) {
						elem.addClass("ImLoaded");
						var $totalRows = gridThis.element
							.find('.k-pager-wrap.k-grid-pager .reader-mode-pager-info .grid-total-rows').text();
						var $currentRows = gridThis.element
							.find('.k-pager-wrap.k-grid-pager .reader-mode-pager-info .grid-current-rows').text();

						if ($currentRows !== $totalRows) {
							kendo.ui.progress(elem.closest('div.register-grid'), true);
							$('div.register-grid .k-loading-mask').css('position', 'fixed');
							$.ajax({
								url: registerViewSettings.GetAddDataUrl,
								type: 'GET',
								data: {
									registerId: registerViewSettings.CurrentRegisterViewId,
									uniqueSessionKey: registerViewSettings.UniqueSessionKey
								},
								success: function (response) {
									kendo.ui.progress(elem.closest('div.register-grid'), false);
									if (response && response.Data.length) {
										gridThis.dataSource.data().push
											.apply(gridThis.dataSource.data(), response.Data);
										gridThis.element
											.find(
												'.k-pager-wrap.k-grid-pager .reader-mode-pager-info .grid-current-rows')
											.text(gridThis.dataSource.data().length);
										if (gridThis.content)
											gridThis.content.css('height', '');
									}
								},
								error: function () {
									kendo.ui.progress(elem.closest('div.register-grid'), false);
								}
							});
						}
					}
				}

				if (registerViewSettings.UseDataReaderMode && (registerViewSettings.ContentLoadCounter == 0 || registerViewSettings.ContentLoadCounter == 1)) {
					var scrollbarWidth = kendo.support.scrollbar();

					$(gridSelector + ' .k-grid-pager').find('a, ul').each(function (i) {
						$(this).remove()
					});

					gridEl.children('.k-grid-content').on('scroll', function (e) {
						if (e.target.scrollTop >= e.target.scrollHeight - e.target.offsetHeight - scrollbarWidth)
							ifViewLoadContent(gridThis.element.find('table[role=grid] tbody tr:last'));
					});

					var $pager = gridThis.element.find('.k-pager-wrap.k-grid-pager');

					if ($pager.length) {
						$pager.attr('style', 'position: sticky;bottom: 0;padding-top: 1em;padding-bottom: 1em;');
						$pager.find('.k-pager-info.k-label').remove();

						if ($pager.find('.reader-mode-pager-info').length == 0)
							$pager.append('<span class="reader-mode-pager-info">Отображены записи&nbsp;<span class="grid-current-rows"></span>&nbsp;из&nbsp;<span class="grid-total-rows"></span>');

						$pager.find('.grid-current-rows').text(gridThis.dataSource.data().length);
						$pager.find('.grid-total-rows').html('<span class="small-loader"></span>');
					}

					var parameters = grid.GetDataFunc();
					parameters.CurrentLayoutId = registerViewSettings.CurrentLayoutId;
					var url = registerViewSettings.GetCountUrl +
						'?parametersJson=' +
						encodeURIComponent(JSON.stringify(parameters));

					$.ajax({
						url: url,
						type: 'GET',
						data: {
							registerId: registerViewSettings.CurrentRegisterViewId,
							uniqueSessionKey: registerViewSettings.UniqueSessionKey
						},
						success: function (response) {
							var $val = response ? response : 0;
							gridThis.element.find('.k-pager-wrap.k-grid-pager .grid-total-rows').text($val);
						}
					});
				}

				if (view.length > 0) {
					$.ajax({
						url: registerViewSettings.RegistersGetGridSpecialStylesUrl,
						type: 'GET',
						data: {
							registerId: registerViewSettings.RegisterId,
							uniqueSessionKey: registerViewSettings.UniqueSessionKey
						},
						success: function (result) {
							if (result && result.length > 0) {
								var rows = grid.tbody.find("tr");
								var cells = grid.tbody.find("td");
								result.forEach(function (item, i, result) {
									if (item.RowIndex != null) {
										var row = rows[item.RowIndex];
										row.setAttribute("style", item.Config);
										if (item.ColumnIndex != null) {
											var cell = cells[grid.columns.length * i + item.ColumnIndex];
											cell.setAttribute("style", item.Config);
										}
									}
								});
							}
						}
					});
				}

				//Обработка события клик по ссылке в гриде, для открытия модального окна
				//Должен быть тег <a target="_modal" class="open-page-in-modal">
				function subscribeToOpenModalGridLink() {
					$('.open-page-in-modal').off('click');
					$('.open-page-in-modal').on('click',
						function (e) {
							e.preventDefault();
							var containerWindow = $('<div id="windowForGridLink"></div>');
							$('body').append(containerWindow);

							var width = $(this).data('width');
							var height = $(this).data('height');

							var useIframe = $(this).attr('target') === linkTarget.ModalIframe;

							var windowForGridLink = containerWindow.kendoWindow({
								title: "",
								visible: false,
								resizable: false,
								modal: true,
								iframe: useIframe,
								close: function (e) {
									this.destroy();
								},
								height: !!width && !height ? "90%" : height,
								width: !!height && !width ? "50%" : width
							}).data('kendoWindow');

							var url = $(this).attr('href');
							windowForGridLink.refresh(url);
							if (width === "" && height === "") {
								windowForGridLink.maximize().open();
								return;
							}
							windowForGridLink.center().open();
						});
				};

				subscribeToOpenModalGridLink();
			});

		grid.bind('columnResize', gridColumnResize);
		grid.bind('sort', onSorting);
		grid.bind('page', function () { pageChanged = true; });

		grid.setOptions({
			excelExport: function (e) {
				e.workbook.fileName = registerViewSettings.LayoutName +
					'_' +
					kendo.toString(new Date(), 'dd.MM.yyyy') +
					'.xlsx';
				KendoExtension.ExportGridWithTemplatesContent(e);
			},
			dataSource: {
				transport: {
					read: {
						data: function (e) {
							registerViewSettings.ContentLoadCounter++;
							return GetData();
						},
						beforeSend: function (xhr) {
							gridReadRequest = xhr;
						}
					}
				},
				requestStart: function (e) {
					if (registerViewSettings.NeedOpenEmpty === true) {
						e.preventDefault();
					}
				}
			},
		});

		grid.dataSource.bind('error', onError);

		$(document.body).keydown(function (e) {
			if (e && e.keyCode == 27 && gridReadRequest && gridReadRequest.abort) {
				gridReadRequest.abort();
			}
		});

		$(gridSelector).kendoTooltip({
			autoHide: true,
			showAfter: 3000,
			position: "right",
			filter: ".forTooltips",
			content: getRatingTooltip
		});

		grid.thead.kendoTooltip({
			filter: "th",
			content: function (e) {
				return e.target.text();
			},
			position: "top",
			autoHide: true,
			showAfter: 500
		});

		if (registerViewSettings.ShowActualDate) {
			if (registerViewSettings.NewDisign)
				$(gridToolbarSelector).css("width", "calc(100% - 270px)").css("margin-right", "0");

			$("#dpActualDate-" + registerViewSettings.CurrentRegisterId).kendoDatePicker({
				format: "dd.MM.yyyy",
				value: new Date()
			});

			if (registerViewSettings.ActualDateTopLimit) {
				var $dp = $("#dpActualDate-" + registerViewSettings.CurrentRegisterId).data("kendoDatePicker");

				if ($dp.value() > registerViewSettings.ActualDateTopLimit)
					$dp.value(registerViewSettings.ActualDateTopLimit);

				$dp.max(registerViewSettings.ActualDateTopLimit);
				$dp.bind("change",
					function (e) {
						var dt = e.sender;
						var value = dt.value();
						if (value === null) {
							value = kendo.parseDate(dt.element.val(), dt.options.parseFormats);
						}
						if (value > dt.max()) {
							dt.value(dt.max());
						}
					});
			}
		}

		$(showRefreshButtonSelector).on('click',
			function (e) {
				e.preventDefault();
				if (grid) {
					grid.dataSource.read();
				}
			});

		$(gridSelector + ' #headerColumnCheckBox').on('click',
			function (e) {
				clearSelection = !$(this).is(':checked');

				if (grid && grid.dataSource) {
					grid.dataSource.read();
				}
			});

		function InitSearchMethode() {
			if (registerViewSettings.NewDisign && registerViewSettings.UseMasterPage) {

				var $settingLayoutBtn = $('#ShowSettingLayoutWindow-' + registerViewSettings.CurrentRegisterId);
				if (!$settingLayoutBtn.length) {
					$(gridToolbarSelector)
						.find('.k-split-button')
						.after('<a href="" id="ShowSettingLayoutWindow-' +
							registerViewSettings.CurrentRegisterId +
							'" title="Настройка раскладки" class="k-button k-button-icon"><i class="fas fa-columns"></i></span></a>');
					RegisterView.InitSettingsLayout(registerViewSettings);
				}

				if (registerViewSettings.ShowSearchPanelButton) {
					var $searchFilter = $('.search-filter');
					if ($searchFilter.length)
						$searchFilter.remove();

					var $searchBtn = $('#ShowSearchPanelButton-' + registerViewSettings.CurrentRegisterId);
					if ($searchBtn.length)
						$searchBtn.show();
					else {
						$(gridToolbarSelector)
							.find('#ShowSettingLayoutWindow-' + registerViewSettings.CurrentRegisterId)
							.after('<a href="" id="ShowSearchPanelButton-' +
								registerViewSettings.CurrentRegisterId +
								'" title="Поиск" class="k-button k-button-icon" ><span class="k-icon k-i-zoom"></span></a>');

						$('#ShowSearchPanelButton-' + registerViewSettings.CurrentRegisterId).on('click',
							function () {
								RegisterView.Common.ShowSearchPanel('#SearchWindow-' + registerViewSettings.CurrentRegisterId);
							});
						if ($searchBtn && registerViewSettings.NeedOpenEmpty) {
							setTimeout(function () {
								RegisterView.Common.ShowSearchPanel('#SearchWindow-' + registerViewSettings.CurrentRegisterId);
							}, 0);
						}
						RegisterView.Common.DeleteCharactersMaskTextBox('#SearchWindow-' + registerViewSettings.CurrentRegisterId);
					}
				} else {
					if ($(searchWindowToolbarSelector + ' #ClearSearchButton').data('clear'))
						$(searchWindowToolbarSelector + ' #ClearSearchButton').data('clear')();

					$('#ShowSearchPanelButton-' + registerViewSettings.CurrentRegisterId).hide();

					$(gridToolbarSelector)
						.find('#ShowSettingLayoutWindow-' + registerViewSettings.CurrentRegisterId)
						.after(
							'<div class="search-filter" style="width: calc(100% - 120px);padding: 15px 15px 15px 0"></div>');

					$('.search-filter').registerViewSearch(
						{
							registerViewId: registerViewSettings.CurrentRegisterViewId,
							registerId: registerViewSettings.CurrentRegisterId,
							baseUrl: registerViewSettings.BaseUrl,
							filter: registerViewSettings.SearchFilter,
							needOpenEmpty: registerViewSettings.NeedOpenEmpty,
							registerSettings: registerViewSettings
						}
					);
				}
			}
		}

		function GetSearchValues() {
			var elementList = $(searchWindowToolbarSelector)
				.closest(searchPanelContentSelector + ',' + searchWindowSelector).find('[attributenumberid]');
			var search = [];
			for (var i = 0; i < elementList.length; ++i) {
				// val true всегда у checkbox, поэтому отдельную обработку делать должны мы
				if (elementList[i].getAttribute('class') == 'k-checkbox') {
					if ($('[name="' + elementList[i].id + '"]:checkbox').is(':checked')) {
						search.push({ key: elementList[i].id, value: true });
					}
				} else {
					var val = elementList[i].value;
					if (val && elementList[i].id) { //todo core
						search.push({ key: elementList[i].id, value: val });
					}
				}
			}
			return search;
		}

		/***
		 * @typedef {Object} queryOperation
		 * @property {string} IsNull
		 */
		var queryOperation = {
			IsNull: 'IsNull',
			NotNull: 'NotNull',
			List: 'list'
		};

		/**
		 * @typedef {Object} SearchDynamicControl
		 * @property {string} IdControl
		 * @property {string} To
		 * @property {string} From
		 * @property {controlType} ControlType
		 * @property {string} QueryOperation
		 * @property {string} IdAttribute
		 */

		/***
		 * @returns {Array<SearchDynamicControl>}
		 * Возвращает массив объектов с данными для поиска по динамичным контролам
		 */
		function fillSearchDynamicControlArray() {
			var elementList = $(searchWindowToolbarSelector)
				.closest(searchPanelContentSelector + ',' + searchWindowSelector).find('[DynamicControlType]');

			var conditionElementList = $(searchWindowToolbarSelector)
				.closest(searchPanelContentSelector + ',' + searchWindowSelector).find('[conditionFor]');

			var searchArray = [];
			for (var i = 0; i < elementList.length; ++i) {
				if (elementList[i].id) {
					fillDynamicData(searchArray, elementList[i], conditionElementList);
					fillDynamicDataConditionList(searchArray, elementList[i], conditionElementList);
					// заполнение параемтров поиска из динамеческого контрола даты
					fillDynamicDate(searchArray, elementList[i]);
				}
			}

			return searchArray;
		}

		/**
	 * 
	 * @param {Array<SearchDynamicControl>} searchArray Массив с данными для поиска 
	 * @param {Object} element html элемент инпута
	 * @param {Array<Object>} conditionElList Массив элементов с условиями поиска
	 */
		function fillDynamicData(searchArray, element, conditionElList) {
			var conditionEl = $.grep(conditionElList, (el) => el.getAttribute('conditionFor') === element.id);
			if (conditionEl.length > 0 && conditionEl[0].value !== queryOperation.List) {

				var value = element.value;
				if (conditionEl[0].value === queryOperation.IsNull || conditionEl[0].value === queryOperation.NotNull) {
					// Заглушка для того чтоб сформировалось условие поиска IsNull or NotNull, необходимо обязательно передавать значение или на сервере будет пропущено это услоаие
					//TODO возможно лучше это будет сделать на серваке, нужно исследовать
					value = 'nullOrNotNull';
				}

				if (value) {
					searchArray.push({
						IdControl: element.id,
						StringValue: value,
						ControlType: element.getAttribute('DynamicControlType'),
						QueryOperation: conditionEl[0].value
					});
				}
			}
		}

		/**
		 * 
		 * @param {Array<SearchDynamicControl>} searchArray Массив с данными для поиска
		 * @param {Object} element html элемент инпута
		 * @param {Array<Object>} conditionElList Массив элементов с условиями поиска
		 */
		function fillDynamicDataConditionList(searchArray, element, conditionElList) {
			var conditionEl = $.grep(conditionElList, (el) => el.getAttribute('conditionFor') === element.id);

			if (conditionEl.length > 0 && conditionEl[0].value === queryOperation.List) {
			
				var listData = $(element).data('listData');
				listData && listData.value.forEach(function(el) {
					searchArray.push({
						IdControl: element.id,
						StringValue: el,
						ControlType: element.getAttribute('DynamicControlType'),
						QueryOperation: conditionEl[0].value,
						IdAttribute: element.getAttribute('IdAttribute')
					});
				});
			}
		}

		/**
		 * *
		 * @param {Array<SearchDynamicControl>} searchArray Массив с данными для поиска
		 * @param {Object} element html элемент инпута
		 */
		function fillDynamicDate(searchArray, element) {

			var data = {
				IdControl: element.id,
				ControlType: element.getAttribute('DynamicControlType'),
				IdAttribute: element.getAttribute('IdAttribute')
			};

			var addRecord = false;
			switch (element.value) {
				case "day":
					addRecord = getDayCondition(element, data);
					break;
				case "year":
					addRecord = getYearCondition(element, data);
					break;
				case "month":
					addRecord = getMonthCondition(element, data);
					break;
				case "quarter":
					addRecord = getQuarterCondition(element, data);
					break;
				case "interval":
					addRecord = getIntervalCondition(element, data);
					break;
				default:
			}

			if (addRecord) {
				searchArray.push(data);
			}
		}

		/**
		 * Заполняем диапозон поиска для одного дня
		 * @param {Object} element html элемент инпута
		 * @param {SearchDynamicControl} dataSearch Массив с данными для поиска
		 * @returns {boolean} признак для записи в параметры поиска
		 */
		function getDayCondition(element, dataSearch) {
			var dayControl = $(`#${element.id}_day_value`).data('kendoDatePicker');
			if (dayControl) {
				var value = dayControl.value();
				if (value) {

					var onlyDay = kendo.toString(value, "MM/dd/yyyy");
					var dateFrom = new Date(onlyDay).setHours(0, 0, 0);
					var dateTo = new Date(onlyDay).setHours(23, 59, 59);

					dataSearch["From"] = kendo.toString(new Date(dateFrom), "MM/dd/yyyy HH:mm:ss");
					dataSearch["To"] = kendo.toString(new Date(dateTo), "MM/dd/yyyy HH:mm:ss");
					return true;
				}
			}
			return false;
		}

		/**
	 * Заполняем диапозон поиска для одного года
	 * @param {Object} element html элемент инпута
	 * @param {SearchDynamicControl} dataSearch Массив с данными для поиска
	 * @returns {boolean} признак для записи в параметры поиска
	 */
		function getYearCondition(element, dataSearch) {
			var yearControl = $(`#${element.id}_year_value`).data('kendoDatePicker');
			if (yearControl) {
				var value = yearControl.value();
				if (value) {
					var onlyYear = kendo.toString(value, "yyyy");
					var dateFrom = new Date(onlyYear).setHours(0, 0, 0);
					var dateTo = new Date(parseInt(onlyYear), 12, 0).setHours(23, 59, 59);

					dataSearch["From"] = kendo.toString(new Date(dateFrom), "MM/dd/yyyy HH:mm:ss");
					dataSearch["To"] = kendo.toString(new Date(dateTo), "MM/dd/yyyy HH:mm:ss");
					return true;
				}
			}
			return false;
		}

		/**
		* Заполняем диапозон поиска для одного месяца
		* @param {Object} element html элемент инпута
		* @param {SearchDynamicControl} dataSearch Массив с данными для поиска
		* @returns {boolean} признак для записи в параметры поиска
		*/
		function getMonthCondition(element, dataSearch) {
			var yearControl = $(`#${element.id}_month_value`).data('kendoDatePicker');
			if (yearControl) {

				var value = yearControl.value();
				if (value) {
					var onlyMonth = kendo.toString(value, "MM/dd/yyyy");
					var dateFrom = new Date(new Date(onlyMonth).setDate(1)).setHours(0, 0, 0);
					var dateTo = new Date(new Date(onlyMonth).getFullYear(), new Date(onlyMonth).getMonth() + 1, 0).setHours(23, 59, 59);

					dataSearch["From"] = kendo.toString(new Date(dateFrom), "MM/dd/yyyy HH:mm:ss");
					dataSearch["To"] = kendo.toString(new Date(dateTo), "MM/dd/yyyy HH:mm:ss");
					return true;
				}
			}
			return false;
		}

		/**
		* Заполняем диапозон поиска по кварталам
		* @param {Object} element html элемент инпута
		* @param {SearchDynamicControl} dataSearch Массив с данными для поиска
		* @returns {boolean} признак для записи в параметры поиска
		*/
		function getQuarterCondition(element, dataSearch) {
			var quarterValueControl = $(`#${element.id}_quarter_value`).data('kendoDatePicker');
			var quarterControl = $(`#${element.id}_quarter`).data('kendoDropDownList');
			if (quarterValueControl && quarterControl) {
				var value = quarterValueControl.value();
				var quarterValue = quarterControl.value();
				if (value && quarterValue) {

					var onlyYear = kendo.toString(value, "yyyy");
					switch (quarterValue) {
						case "first_quarter":
							{
								dataSearch["From"] = kendo.toString(new Date(new Date("01/01/" + onlyYear).setHours(0, 0, 0)),
									"MM/dd/yyyy HH:mm:ss");
								dataSearch["To"] = kendo.toString(new Date(new Date(parseInt(onlyYear), 3, 0).setHours(23, 59, 59)),
									"MM/dd/yyyy HH:mm:ss");
								break;
							}
						case "second_quarter":
							{
								dataSearch["From"] = kendo.toString(new Date(new Date("04/01/" + onlyYear).setHours(0, 0, 0)),
									"MM/dd/yyyy HH:mm:ss");
								dataSearch["To"] = kendo.toString(new Date(new Date(parseInt(onlyYear), 6, 0).setHours(23, 59, 59)),
									"MM/dd/yyyy HH:mm:ss");
								break;
							}
						case "third_quarter":
							{
								dataSearch["From"] = kendo.toString(new Date(new Date("07/01/" + onlyYear).setHours(0, 0, 0)),
									"MM/dd/yyyy HH:mm:ss");
								dataSearch["To"] = kendo.toString(new Date(new Date(parseInt(onlyYear), 9, 0).setHours(23, 59, 59)),
									"MM/dd/yyyy HH:mm:ss");
								break;
							}
						case "fourth_quarter":
							{
								dataSearch["From"] = kendo.toString(new Date(new Date("10/01/" + onlyYear).setHours(0, 0, 0)),
									"MM/dd/yyyy HH:mm:ss");
								dataSearch["To"] =
									kendo.toString(
										new Date(new Date(parseInt(onlyYear), 12, 0).setHours(23, 59, 59)),
										"MM/dd/yyyy HH:mm:ss");
								break;
							}
						default:
					}
					return true;
				}
			}
			return false;
		}

		/**
	* Заполняем диапозон поиска для интервала
	* @param {Object} element html элемент инпута
	* @param {SearchDynamicControl} dataSearch Массив с данными для поиска
	* @returns {boolean} признак для записи в параметры поиска
	*/
		function getIntervalCondition(element, dataSearch) {
			var intervalControlFrom = $(`#${element.id}_interval`).data('kendoDatePicker');
			var intervalControlTo = $(`#${element.id}_interval_value`).data('kendoDatePicker');

			if (intervalControlFrom && intervalControlTo) {

				var intervalFrom = intervalControlFrom.value();
				var intervalTo = intervalControlTo.value();
				if (intervalFrom || intervalTo) {
					if (intervalFrom) {
						var from = kendo.toString(intervalFrom, "MM/dd/yyyy"),
							dateFrom = new Date(new Date(from).setHours(0, 0, 0));
						dataSearch["From"] = kendo.toString(dateFrom, "MM/dd/yyyy HH:mm:ss");
					}
					if (intervalTo) {
						var to = kendo.toString(intervalTo, "MM/dd/yyyy"),
							dateTo = new Date(new Date(to).setHours(23, 59, 59));
						dataSearch["To"] = kendo.toString(dateTo, "MM/dd/yyyy HH:mm:ss");
					}

					return true;
				}
			}
			return false;
		}


		function GetFilterValues() { // toDo
			var databaseFiltersGrid = $(menuFiltersGridSelector).data('kendoGrid');
			var databaseFilters = [];
			//добавляем сохраненные фильтры
			if (databaseFiltersGrid) {
				var selectedDatabaseFilters = databaseFiltersGrid.select();
				for (var i = 0; i < selectedDatabaseFilters.length; i++) {
					var dataItem = databaseFiltersGrid.dataItem(selectedDatabaseFilters[i]);
					databaseFilters.push(dataItem.Id);
				}
			}

			if ($_GET["QueryId"]) {
				databaseFilters.push($_GET["QueryId"]);
			}

			return databaseFilters;
		}

		function GetData() {
			var parameters = {
				RegisterId: registerViewSettings.RegisterId,
				SearchApplied: searchAplied
			};

			parameters.PageChanged = pageChanged;
			pageChanged = false;

			var selectAll = $(gridSelector + ' #headerColumnCheckBox').is(':checked');
			parameters.SelectAll = selectAll;
			parameters.ClearSelection = clearSelection;

			if (registerViewSettings.LayoutIdHasValue == "True") {
				parameters.LayoutId = registerViewSettings.LayoutId;
			}

			if (registerViewSettings.ObjectRegisterIdHasValue == "True") {
				parameters.ObjectRegisterId = registerViewSettings.ObjectRegisterId;
			}

			if (registerViewSettings.ObjectIdHasValue == "True") {
				parameters.ObjectId = registerViewSettings.ObjectId;
			}

			if (registerViewSettings.RegisterViewIdIsNotEmpty == "True") {
				parameters.RegisterViewId = registerViewSettings.RegisterViewId;
				parameters.LayoutRegisterId = registerViewSettings.RegisterLayoutId;
				parameters.FilterRegisterId = registerViewSettings.RegisterLayoutId;
				parameters.ListRegisterId = registerViewSettings.RegisterLayoutId;
			}


			var registerViewSearch = $(gridToolbarSelector + ' .search-filter.rvs-main-content').data('registerViewSearch');
			if (registerViewSearch) {
				//настраиваемый поиск
				parameters.SearchDataNewDesign = registerViewSearch.filter
					? registerViewSearch.filter
					: encodeURIComponent(JSON.stringify(registerViewSearch.getStruct()));
			} else {
				// фиксированный поиск
				parameters.searchData = GetSearchValues();
				//  сбор данных из динамических контролов
				var data = JSON.stringify(fillSearchDynamicControlArray());

				parameters.SearchDynamicControlData = data;
			}

			// фильтры
			parameters.databaseFilters = GetFilterValues();

			//списки
			parameters.selectedLists = RegisterView.SearchPanel.getSelectedListItem(registerViewSettings.CurrentRegisterId);
			//фильтр перехода
			var isTransition = $(sidePanelSelector + ' [name=IsTransition]').length > 0
				? $(sidePanelSelector + ' [name=IsTransition]').is(':checked')
				: isModelTransition;

			var transitionQueryString =
				$(gridSelector).closest('.k-content').find('[name=TransitionQueryString]').val() ||
				$(gridSelector).closest('.mainContent').find('[name=TransitionQueryString]').val();
			var requestUrl = $(gridSelector).closest('.mainContent').find('[name=RequestUrl]').val() ||
				$(gridSelector).closest('.k-content').find('[name=RequestUrl]').val();

			if (isTransition) {
				parameters.isTransition = true;
				parameters.transitionQueryString = transitionQueryString;
			}

			//дата актуальности
			if (registerViewSettings.ShowActualDate) {
				var $dp = $("#dpActualDate-" + registerViewSettings.CurrentRegisterId).data("kendoDatePicker");
				if ($dp && $dp.value())
					parameters.actualDate = $dp.value().toJSON();
			}

			parameters.UniqueSessionKey = registerViewSettings.UniqueSessionKey;
			parameters.UniqueSessionKeySetManually = true;

			var urlParameters = Common.GetUrlParameters(requestUrl);
			if (urlParameters.length > 0) {
				for (var i = 0; i < urlParameters.length; i++) {
					if (!Common.IsExistsKeyInObject(parameters, urlParameters[i].Name)) {
						parameters[urlParameters[i].Name] = urlParameters[i].Value;
					}
				}
			}

			parameters.ContentLoadCounter = registerViewSettings.ContentLoadCounter;

			return parameters;
		}

		function boldButton(collection, isTransition, id) {
			$(id + ' span')
				.closest('button')
				.toggleClass('use-filter', collection.length || isTransition ? true : false);
		}

		function getRatingTooltip(e) {
			var image = $(e.target[0]).find('img');
			if (image.attr('toolTip') !== undefined) {
				return image.attr('toolTip');
			} else {
				return e.target[0].innerText;
			}
		}

		function gridColumnResize(arg) {
			var columnsWidth = [];

			for (var i = 0; i < grid.columns.length; ++i) {
				columnsWidth.push(grid.columns[i].width === undefined ? '' : grid.columns[i].width);
			}

			$.ajax({
				url: registerViewSettings.RegistersSaveGridLayoutsWidthUrl,
				type: 'POST',
				data: {
					columnsWidth: columnsWidth,
					registerId: registerViewSettings.RegisterId,
					UniqueSessionKey: registerViewSettings.UniqueSessionKey
				},
				error: function (result) {
					Common.UI.ShowErrorDialog(result.responseText);
				}
			});
		}

		function resetSorting() {
			$.ajax({
				url: registerViewSettings.RegistersResetGridSortUrl,
				type: 'POST',
				contentType: 'application/json',
				data: JSON.stringify({
					registerId: registerViewSettings.RegisterId,
					layoutId: registerViewSettings.CurrentLayoutId,
					UniqueSessionKey: registerViewSettings.UniqueSessionKey
				}),
				error: function (result) {
					Common.UI.ShowErrorDialog(result.responseText);
				}
			});
		}

		function onSorting(arg) {
			$.ajax({
				url: registerViewSettings.RegistersSaveGridSortUrl,
				type: 'POST',
				contentType: 'application/json',
				data: JSON.stringify({
					field: arg.sort.field,
					direction: (arg.sort.dir || "none"),
					registerId: registerViewSettings.RegisterId,
					layoutId: registerViewSettings.CurrentLayoutId,
					UniqueSessionKey: registerViewSettings.UniqueSessionKey
				}),
				error: function (result) {
					Common.UI.ShowErrorDialog(result.responseText);
				}
			});
		}

		function onError(e) {
			var errorText = e.status == 'abort' ? 'Поиск остановлен по требованию пользователя.' : e.xhr.responseText;

			Common.UI.ShowDialog({
				title: e.status != 'abort' ? 'Ошибка' : '',
				content: errorText,
				icon: 'error',
				height: e.status == 'abort' ? 135 : 170,
				showCloseBtn: false
			});

			setFooterStatus();
		}

		function setFooterStatus() {
			if ($(footerStatusSelector)[0]) {
				$.ajax({
					url: registerViewSettings.RegistersGetStatusUrl,
					type: 'GET',
					//contentType: 'application/json',
					data: {
						registerId: registerViewSettings.RegisterId,
						uniqueSessionKey: registerViewSettings.UniqueSessionKey
					},
					success: function (result) {
						$(footerStatusSelector)[0].innerHTML = result;
					}
				});
			}
		}

		grid.refreshGrid = function () {
			$(gridSelector).data('kendoGrid').dataSource.read();
		};

		grid.resetSorting = function () {
			resetSorting();
		};

		grid.gridColumnResize = function () {
			gridColumnResize();
		};

		grid.gridClearSelection = function () {
			clearSelection = true;
		};

		grid.dataSource.read();

		grid.GetDataFunc = function () {
			var parameters = GetData();
			if (this.dataSource.sort()) {
				parameters.Sort = '';
				$.each(this.dataSource.sort(),
					function (index, value) {
						parameters.Sort += (index !== 0 ? '~' : '') + value.field + '-' + value.dir;
					});
			}
			return parameters;
		}

	},

	SearchPanel: {
		/**
		* Инирциализация списков
	    * @param {RegisterViewSettings} registerViewSettings настройки реестра
		*/
		initLists: function (registerViewSettings) {
			var gridSelector = "#Grid-" + registerViewSettings.CurrentRegisterId,				
				createListWindowSelector = "#CreateList-" + registerViewSettings.CurrentRegisterId,
				templateOperationListSelector = "#templateOperationList-" + registerViewSettings.CurrentRegisterId,
				menuListsGridSelector = "#menu-lists_grid-" + registerViewSettings.CurrentRegisterId,
				menuListsGridToolbarSelector = "#menu-lists_grid-toolbar-" + registerViewSettings.CurrentRegisterId,
				selectRecordsFromSelectedListsBtnSelector =
					"#selectRecordsFromSelectedLists-" + registerViewSettings.CurrentRegisterId,
				createListFromSelectedRecordsBtnSelector =
					"#createListFromSelectedRecords-" + registerViewSettings.CurrentRegisterId,
				createAndApplyListFromCurrentSelectedBtnSelector =
					"#createAndApplyListFromCurrentSelected-" + registerViewSettings.CurrentRegisterId,
				addSelectedRecordsIntoListBtnSelector =
					"#addSelectedRecordsIntoList-" + registerViewSettings.CurrentRegisterId,
				delSelectedRecordsFromListBtnSelector =
					"#delSelectedRecordsFromList-" + registerViewSettings.CurrentRegisterId;

			var dataSource = new kendo.data.DataSource({
				transport: {
					read: {
						url: registerViewSettings.RegistersGetListsSettingUrl,
						dataType: "json",
						data: {
							UniqueSessionKey: registerViewSettings.UniqueSessionKey,
							UniqueSessionKeySetManually: true
						},
						schema: {
							model: {
								id: "Id",
								fields: {
									Name: { type: "string" }
								}
							}
						}
					}
				}
			});
			$(menuListsGridSelector).kendoGrid({
				columns: [
					{ selectable: true, width: "40px" },
					{
						field: "Name",
						title: "Наименование",
						filterable: {
							cell: {
								suggestionOperator: "contains"
							}
						}
					}
				],
				dataBinding: onDataBinding,
				//filterable: { mode: "row" },
				dataSource: dataSource,
				toolbar: [{ template: kendo.template($(templateOperationListSelector).html()) }],
				messages: {
					commands: {
						find: "Найти"
					}
				}
			});

			// кнопка "Найти" на панели "Списки"
			var searchListButton = function () {
				var registerGrid = $(gridSelector).data('kendoGrid');
				if (registerGrid) {
					searchAplied = true;
					registerGrid.dataSource.read();
					$(gridSelector).data('kendoGrid').content.scrollTop(0);
					registerViewSettings.ContentLoadCounter = 0;
				}
			};

			// выбор элементов в таблице, по элементам из списков
			var selectRecordsFromSelectedListsFunction = function () {
				var listOfLists = RegisterView.SearchPanel.getSelectedListItem(registerViewSettings.CurrentRegisterId);
				if (listOfLists.length > 0) {
					$.ajax({
						url: registerViewSettings.RegistersGetSelectedElemensFromListsUrl,
						type: 'POST',
						data: { ids: listOfLists },
						success: function (result) {
							grid = $(gridSelector).data('kendoGrid');
							grid.checkedIds = result;
							clearSelectedListItem();
							grid.dataSource.read()
						},
						error: function (result) {
							Common.UI.ShowErrorDialog('<div style="text-align:center;padding:10px;">' +
								result.responseText +
								'</div>');
						}
					});
				} else {
					Common.UI.ShowDialog({
						title: 'Внимание',
						content: 'Не выбрано ни одного списка!',
						icon: 'warning',
						showCloseBtn: true
					});
				}
			};

			// создать список на основе выбранных элементов
			var createListFromSelectedRecordsFunction = function () {
				registerViewSettings.selectAllRecrods = false;
				Common.UI.ShowWindow('Создать список',
					registerViewSettings.RegistersListIndexUrl + '&=UniqueSessionKey=' + registerViewSettings.UniqueSessionKey + '&UniqueSessionKeySetManually=true',
					'createListWnd', null, 515, 265);
			};

			// создать список на основе всех текущих элементов таблицы
			var createAndApplyListFromCurrentSelectedFunction = function () {
				registerViewSettings.selectAllRecrods = true;
				Common.UI.ShowWindow('Создать список',
					registerViewSettings.RegistersListIndexUrl + '&=UniqueSessionKey=' + registerViewSettings.UniqueSessionKey + '&UniqueSessionKeySetManually=true',
					'createListWnd', null, 515, 265);
			};

			// добавление в списки новых элементов, выбранных в таблице
			var addSelectedRecordsIntoListFunction = function () {
				grid = $(gridSelector).data("kendoGrid");
				if (grid && grid.checkedIds.length > 0) {
					var listOfLists = RegisterView.SearchPanel.getSelectedListItem(registerViewSettings.CurrentRegisterId);
					if (listOfLists.length > 0) {
						Common.UI.ShowConfirm({
							title: 'Подтверждение',
							content: 'Вы действительно хотите добавить элементы в списки?',
							onSuccess: function (e) {
								$.ajax({
									url: registerViewSettings.RegistersAddElementsIntoListsUrl,
									type: 'POST',
									data: { ids: listOfLists, selecetdElements: grid.checkedIds },
									success: function () {
										Common.UI.ShowDialog({
											content: 'Списки успешно обновлены',
											icon: 'ok',
											showCloseBtn: true
										});
										//clearSelectedListItem();
									},
									error: function (result) {
										Common.UI.ShowErrorDialog(result.responseText);
									}
								});
							}
						});
					} else {
						Common.UI.ShowDialog({
							title: 'Внимание',
							content: 'Не выбрано ни одного списка!',
							icon: 'warning',
							showCloseBtn: true
						});
					}
				} else {
					Common.UI.ShowDialog({
						title: 'Внимание',
						content: 'Не выбрано ни одной записи!',
						icon: 'warning',
						showCloseBtn: true
					});
				}
			};

			// удаление из списков элементов, выбранных в таблице
			var delSelectedRecordsFromListFunction = function () {
				grid = $(gridSelector).data("kendoGrid");
				if (grid && grid.checkedIds.length > 0) {
					var listOfLists = RegisterView.SearchPanel.getSelectedListItem(registerViewSettings.CurrentRegisterId);
					if (listOfLists.length > 0) {
						Common.UI.ShowConfirm({
							title: 'Подтверждение',
							content: 'Вы действительно хотите удалить элементы из списков?',
							onSuccess: function (e) {
								$.ajax({
									url: registerViewSettings.RegistersDelElementsFromListUrl,
									type: 'POST',
									data: { ids: listOfLists, selecetdElements: grid.checkedIds },
									success: function () {
										//clearSelectedListItem();
										Common.UI.ShowDialog({
											content: 'Списки успешно обновлены',
											icon: 'ok',
											showCloseBtn: true
										});
										grid.dataSource.read()
									},
									error: function (result) {
										Common.UI.ShowErrorDialog(result.responseText);
									}
								});
							}
						});
					} else {
						Common.UI.ShowDialog({
							title: 'Внимание',
							content: 'Не выбрано ни одного списка!',
							icon: 'warning',
							showCloseBtn: true
						});
					}
				} else {
					Common.UI.ShowDialog({
						title: 'Внимание',
						content: 'Не выбрано ни одной записи!',
						icon: 'warning',
						showCloseBtn: true
					});
				}
			};

			$(menuListsGridToolbarSelector).kendoToolBar({
				items: [
					{ type: "button", text: "Найти", click: searchListButton },
					{
						type: "splitButton",
						text: "Операции",
						menuButtons: [
							{
								text: "Выбрать записи из выбранных списков",
								id: selectRecordsFromSelectedListsBtnSelector.substring(1,
									selectRecordsFromSelectedListsBtnSelector.length),
								click: selectRecordsFromSelectedListsFunction
							},
							{
								text: "Создать список из выбранных записей",
								id: createListFromSelectedRecordsBtnSelector.substring(1,
									createListFromSelectedRecordsBtnSelector.length),
								click: createListFromSelectedRecordsFunction
							},
							{
								text: "Создать и применить список по текущей выборке",
								id: createAndApplyListFromCurrentSelectedBtnSelector.substring(1,
									createAndApplyListFromCurrentSelectedBtnSelector.length),
								click: createAndApplyListFromCurrentSelectedFunction
							},
							{
								text: "Добавить выбранные записи в список",
								id: addSelectedRecordsIntoListBtnSelector.substring(1,
									addSelectedRecordsIntoListBtnSelector.length),
								click: addSelectedRecordsIntoListFunction
							},
							{
								text: "Удалить выбранные записи из списка",
								id: delSelectedRecordsFromListBtnSelector.substring(1,
									delSelectedRecordsFromListBtnSelector.length),
								click: delSelectedRecordsFromListFunction
							}
						]
					}
				]
			});

			// очистка выбранных элементов в таблице списков
			function clearSelectedListItem() {
				var view = $(menuListsGridSelector).data('kendoGrid').dataSource.view();
				for (var i = 0; i < view.length; i++) {
					if ($(menuListsGridSelector).data('kendoGrid').tbody.find("tr[data-uid='" + view[i].uid + "']")
						.find(".k-checkbox").is(":checked")) {
						$(menuListsGridSelector).data('kendoGrid').tbody.find("tr[data-uid='" + view[i].uid + "']")
							.find(".k-checkbox").attr("checked", false);
					}
				}
			}			

			// Если таблица со списками пустая, то ряд пунктов меню должна быть неактивной
			function onDataBinding(arg) {
				if ($(menuListsGridSelector).data('kendoGrid').dataSource.view().length == 0) {
					$(selectRecordsFromSelectedListsBtnSelector).addClass('k-state-disabled');
					$(addSelectedRecordsIntoListBtnSelector).addClass('k-state-disabled');
					$(delSelectedRecordsFromListBtnSelector).addClass('k-state-disabled');
				}
			}
		},

		InitSearchPanel: function (registerViewSettings) {
			var sidePanelSelector = "#sidePanel-" + registerViewSettings.CurrentRegisterId,
				menuSettingsSelector = "#menu-setting_bar-" + registerViewSettings.CurrentRegisterId,
				menuFiltersGridSelector = "#menu-filters_grid-" + registerViewSettings.CurrentRegisterId,

				splitterSelector = "#verticalSplitter-" + registerViewSettings.CurrentRegisterId,
				gridSelector = "#Grid-" + registerViewSettings.CurrentRegisterId,				
				
				layoutsSettingBtnSelector = "#layoutsSettingBtn-" + registerViewSettings.CurrentRegisterId,
				filtersSettingBtnSelector = "#filtersSettingBtn-" + registerViewSettings.CurrentRegisterId,
				saveStateBtnSelector = "#saveStateBtn-" + registerViewSettings.CurrentRegisterId,
				restoreStateBtnSelector = "#restoreStateBtn-" + registerViewSettings.CurrentRegisterId,
				listSettingBtnSelector = "#listSettingBtn-" + registerViewSettings.CurrentRegisterId;

			var initSetting = function () {
				$(menuSettingsSelector).kendoPanelBar({
					expandMode: 'multiple',
					select: function () {
						var selectEl = $(menuSettingsSelector + ' .k-group.k-panel span.k-state-selected');
						selectEl.removeClass('k-state-selected');
						selectEl.removeClass('k-state-focused');
					}
				});

				$(menuSettingsSelector).data('kendoPanelBar').expand($(menuSettingsSelector + '>li.k-item'));
			};

			var initFilters = function () {
				var dataSource = new kendo.data.DataSource({
					transport: {
						read: {
							url: registerViewSettings.RegistersGetFiltersSettingUrl,
							dataType: "json",
							data: {
								UniqueSessionKey: registerViewSettings.UniqueSessionKey
							},
							schema: {
								model: {
									id: "Id",
									fields: {
										Name: { type: "string" }
									}
								}
							}
						}
					}
				});

				$(menuFiltersGridSelector).kendoGrid({
					columns: [
						{ selectable: true, width: "40px" },
						{
							field: "Name",
							title: "Наименование",
							filterable: {
								cell: {
									suggestionOperator: "contains"
								}
							}
						}
					],
					dataBound: onFilterDataBound,
					filterable: { mode: "row" },
					dataSource: dataSource,
					toolbar: [
						{
							name: "find",
							text: "Найти"
						},
						{
							name: "reset",
							text: "Сбросить"
						},
					]
				});

				//обработка кнопки "Найти"
				$(menuFiltersGridSelector + ' .k-button.k-grid-find').on('click',
					function (e) {
						e.preventDefault();
						var registerGrid = $('.register-grid').data('kendoGrid');
						if (registerGrid) {
							if (registerGrid.gridClearSelection != undefined)
								registerGrid.gridClearSelection();
							searchAplied = true;
							registerGrid.dataSource.read();
						}
					});

				//обработка кнопки "Сбросить"
				$(menuFiltersGridSelector + ' .k-button.k-grid-reset').on('click',
					function (e) {
						e.preventDefault();
						clearSelectedFilterItem();

						if (!window.location.origin) {
							// For IE
							window.location.origin = window.location.protocol +
								"//" +
								window.location.hostname +
								(window.location.port ? ':' + window.location.port : '');
						}
						history.pushState(null, null, window.location.origin + window.location.pathname);

						var registerGrid = $('.register-grid').data('kendoGrid');
						if (registerGrid) {
							if (registerGrid.gridClearSelection != undefined)
								registerGrid.gridClearSelection();
							searchAplied = true;
							registerGrid.dataSource.read();
						}
					});
			};

			function onFilterDataBound(arg) {
				if ($_GET["QueryId"]) {
					selectFilterItem(parseInt($_GET["QueryId"]));
				}
			}

			function selectFilterItem(queryId) {
				var filterGrid = $(menuFiltersGridSelector).data('kendoGrid');
				var rows = filterGrid.dataSource.view();

				if (rows.length > 0) {
					for (var i = 0; i < rows.length; i++) {
						if (rows[i].Id == queryId) {
							filterGrid.tbody.find("tr[data-uid='" + rows[i].uid + "']").find(".k-checkbox")
								.attr("checked", true);
						}
					}
				}
			}

			// очистка выбранных элементов в таблице фильтров
			function clearSelectedFilterItem() {
				var view = $(menuFiltersGridSelector).data('kendoGrid').dataSource.view();
				for (var i = 0; i < view.length; i++) {
					if ($(menuFiltersGridSelector).data('kendoGrid').tbody.find("tr[data-uid='" + view[i].uid + "']")
						.find(".k-checkbox").is(":checked")) {
						$(menuFiltersGridSelector).data('kendoGrid').tbody.find("tr[data-uid='" + view[i].uid + "']")
							.find(".k-checkbox").attr("checked", false);
					}
				}
				$(menuFiltersGridSelector).data('kendoGrid').clearSelection();
				delete $_GET.QueryId;
			}				

			$(sidePanelSelector + ' .movePanel .header .close').on('click',
				function () {
					var panel = $(sidePanelSelector + " .movePanel.open");

					kendo.fx(panel).slideInLeft().play();
					panel.removeClass("open");
				});

			initSetting();
			initFilters();
			RegisterView.SearchPanel.initLists(registerViewSettings);
			selectFilterItem(61);

			$(sidePanelSelector + " .sideButton").on('click',
				function (e) {
					var dataId = $(this).attr('data-id');
					var otherPanels = $(sidePanelSelector + " .movePanel.open[data-id!='" + dataId + "']");

					otherPanels.css("transform", "");
					otherPanels.removeClass("open");

					var panel = $(sidePanelSelector + " .movePanel[data-id='" + dataId + "']");
					if (panel === undefined || panel == null || panel.length <= 0) return;

					var isVisible = panel.position().left > 0;
					if (isVisible) {
						kendo.fx(panel).slideInLeft().play();
						panel.removeClass("open");
					} else {
						panel.addClass("open");
						kendo.fx(panel).slideInLeft().reverse();
					}

					return false;
				});

			$(sidePanelSelector + ' .sideButton').hover(function () {
				$(this).addClass('k-state-hover');
			},
				function () {
					$(this).removeClass('k-state-hover');
				});

			$(layoutsSettingBtnSelector).on('click',
				function () {
					url = registerViewSettings.CoreRegisterLayoutAllUrl;
					title = 'Раскладки ' + registerViewSettings.CurrentRegisterViewTitle;
					Common.UI.ShowWindow(title, url);
				});

			$(filtersSettingBtnSelector).on('click',
				function () {
					url = registerViewSettings.CoreRegisterQryAllUrl;
					title = 'Фильтры ' + registerViewSettings.CurrentRegisterViewTitle;
					Common.UI.ShowWindow(title, url);
				});

			$(listSettingBtnSelector).on('click',
				function () {
					url = registerViewSettings.CoreRegisterListAllUrl;
					title = 'Списки ' + registerViewSettings.CurrentRegisterViewTitle;
					Common.UI.ShowWindow(title, url);
				});

			$(saveStateBtnSelector).on('click',
				function () {
					kendo.ui.progress($('body'), true);
					$.ajax({
						url: registerViewSettings.RegistersSaveStateUrl,
						type: 'POST',
						contentType: 'application/json',
						data: JSON.stringify({
							UpperPanelSize: $(splitterSelector + ' .k-pane:first').height(),
							CardPanelSize: $(splitterSelector + ' .k-pane:last').height()
						}),
						success: function () {
							Common.UI.ShowMessage('Сохранение состояния',
								'Сохранение состояния представления произведено успешно');
							kendo.ui.progress($('body'), false);
						},
						error: function () {
							Common.UI.ShowMessage('Сохранение состояния', 'Произошла ошибка. Обратитесь к администратору');
							kendo.ui.progress($('body'), false);
						}
					});
				});

			$(restoreStateBtnSelector).on('click',
				function () {
					kendo.ui.progress($('body'), true);
					$.ajax({
						url: registerViewSettings.RegistersRestoreStateUrl,
						type: 'POST',
						contentType: 'application/json',
						success: function (result) {
							if (result !== undefined && result != null) {
								Common.UI.ShowMessage('Восстановление состояния',
									'Восстановление состояния представления произведено успешно');
							} else {
								var splitter = $(splitterSelector).data("kendoSplitter");

								splitter.expand(".k-pane:first");
								splitter.expand(".k-pane:last");

								if (result.UpperPanelSize == 0) {
									splitter.collapse(".k-pane:first");
								} else if (result.IsCollapseDownPanel) {
									splitter.collapse(".k-pane:last");
								} else {
									splitter.size(".k-pane:first", result.UpperPanelSize);
								}

								Common.UI.ShowMessage('Восстановление состояния', 'Отсутствует сохраненное состояние');
							}
							kendo.ui.progress($('body'), false);
						},
						error: function () {
							Common.UI.ShowMessage('Восстановление состояния',
								'Произошла ошибка. Обратитесь к администратору');
							kendo.ui.progress($('body'), false);
						}
					});
				});

			$(sidePanelSelector + ' [name=IsTransition]').on('change',
				function (e) {
					e.preventDefault();
					var registerGrid = $(gridSelector).data('kendoGrid');
					if (registerGrid) {
						registerGrid.dataSource.read();
					}
				});
		},
		/**
		* обработка пунктов меню на вкладке "Списки"
	    * получение списка выбранных элементов-списков (вынесено в отдельную функцию, так как используется в несколькиз местах)
	    * @param {RegisterId} registerId идентификатор реестра
		*/
		getSelectedListItem: function (registerId) {
			var menuListsGridSelector = "#menu-lists_grid-" + registerId;
			var menuListsGrid = $(menuListsGridSelector).data('kendoGrid');
			var listOfLists = [];
			if (menuListsGrid) {
				var view = menuListsGrid.dataSource.view();
				for (var i = 0; i < view.length; i++) {
					if ($(menuListsGridSelector).data('kendoGrid').tbody.find("tr[data-uid='" + view[i].uid + "']")
						.find(".k-checkbox").is(":checked")) {
						listOfLists.push(view[i].Id);
					}
				}
			}
			return listOfLists;
		}
	},

	InitFilterWindow: function (registerViewSettings) {
		var filterWindowSelector = "#filterWindow-" + registerViewSettings.CurrentRegisterId,
			menuFiltersGridSelector = "#menu-filters_grid-" + registerViewSettings.CurrentRegisterId,
			gridSelector = "#Grid-" + registerViewSettings.CurrentRegisterId,
			filterButtonSelector = "#filterButton-" + registerViewSettings.CurrentRegisterId,
			filterWindowContentClass = "filter-window-content",
			filterWindowFooter = "filter-window-footer";

		$.ajax({
			url: registerViewSettings.RegistersGetFiltersSettingUrl,
			type: 'GET',
			data: {
				UniqueSessionKey: registerViewSettings.UniqueSessionKey
			},
			dataType: 'json',
			success: function (data) {
				if (data && data.length > 0) {
					var initWindow = function () {
						var $dialogTemplate = $('<div class="' +
							filterWindowContentClass +
							'"><div class="k-window-actions"><span class="filter-window-title">Фильтры</span><a href="#" class="k-button k-bare k-button-icon k-button-close" style="float: right;"><span class="k-icon k-i-close" style="color: rgba(96, 101, 116, 0.5);"></span></a></div><div class="filter-window-grid-container"><div id="menu-filters_grid-' +
							registerViewSettings.CurrentRegisterId +
							'"></div></div><div class="' +
							filterWindowFooter +
							'"></div></div>');
						var $dialog = $dialogTemplate.kendoWindow({
							title: false,
							draggable: false,
							visible: false,
							resizable: false,
							width: '430px',
							height: '370px'
						}).data('kendoWindow');

						$('.' + filterWindowFooter).append(
							'<button type="button" class="k-button k-primary k-grid-find pull-right" style="width: 132px;">Найти</button><button type="button" class="k-button k-grid-reset pull-right" style="width: 105px;">Сбросить</button>')

						return $dialog;
					};


					var initFilters = function () {
						var dataSource = new kendo.data.DataSource({
							transport: {
								read: {
									url: registerViewSettings.RegistersGetFiltersSettingUrl,
									dataType: "json",
									data: {
										UniqueSessionKey: registerViewSettings.UniqueSessionKey
									},
									schema: {
										model: {
											id: "Id",
											fields: {
												Name: { type: "string" }
											}
										}
									}
								}
							}
						});

						$(menuFiltersGridSelector).kendoGrid({
							columns: [
								{ selectable: true, width: "40px" },
								{
									field: "Name",
									title: "Наименование"
								}
							],
							dataBound: onFilterDataBound,
							dataSource: dataSource
						});

						//обработка кнопки "Найти"
						$('.' + filterWindowFooter + ' .k-button.k-grid-find').on('click',
							function (e) {
								e.preventDefault();
								var registerGrid = $('.register-grid').data('kendoGrid');
								if (registerGrid) {
									if (registerGrid.gridClearSelection != undefined)
										registerGrid.gridClearSelection();
									searchAplied = true;
									registerGrid.dataSource.read();
								}
							});

						//обработка кнопки "Сбросить"
						$('.' + filterWindowFooter + ' .k-button.k-grid-reset').on('click',
							function (e) {
								e.preventDefault();
								clearSelectedFilterItem();

								if (!window.location.origin) {
									// For IE
									window.location.origin = window.location.protocol +
										"//" +
										window.location.hostname +
										(window.location.port ? ':' + window.location.port : '');
								}
								history.pushState(null, null, window.location.origin + window.location.pathname);

								var registerGrid = $('.register-grid').data('kendoGrid');
								if (registerGrid) {
									if (registerGrid.gridClearSelection != undefined)
										registerGrid.gridClearSelection();
									searchAplied = true;
									registerGrid.dataSource.read();
								}
							});
					};

					function onFilterDataBound(arg) {
						if ($_GET["QueryId"]) {
							selectFilterItem(parseInt($_GET["QueryId"]));
						}
					}

					function selectFilterItem(queryId) {
						var filterGrid = $(menuFiltersGridSelector).data('kendoGrid');
						if (filterGrid) {
							var rows = filterGrid.dataSource.view();

							if (rows.length > 0) {
								for (var i = 0; i < rows.length; i++) {
									if (rows[i].Id == queryId) {
										filterGrid.tbody.find("tr[data-uid='" + rows[i].uid + "']").find(".k-checkbox")
											.attr("checked", true);
									}
								}
							}
						}
					}

					// отчистка выбранных элементов в таблице фильтров
					function clearSelectedFilterItem() {
						var view = $(menuFiltersGridSelector).data('kendoGrid').dataSource.view();
						for (var i = 0; i < view.length; i++) {
							if ($(menuFiltersGridSelector).data('kendoGrid').tbody
								.find("tr[data-uid='" + view[i].uid + "']").find(".k-checkbox").is(":checked")) {
								$(menuFiltersGridSelector).data('kendoGrid').tbody
									.find("tr[data-uid='" + view[i].uid + "']").find(".k-checkbox")
									.attr("checked", false);
							}
						}
						$(menuFiltersGridSelector).data('kendoGrid').clearSelection();
						delete $_GET.QueryId;
					}

					var dialog = initWindow();
					initFilters();
					selectFilterItem(61);

					$(filterButtonSelector).on('click',
						function () {
							dialog.center().open();
						});

					$('.' + filterWindowContentClass + ' .k-button-close').on('click',
						function () {
							dialog.close();
						});

					$(filterWindowSelector + ' [name=IsTransition]').on('change',
						function (e) {
							e.preventDefault();
							var registerGrid = $(gridSelector).data('kendoGrid');
							if (registerGrid) {
								registerGrid.dataSource.read();
							}
						});
				} else {
					$(filterButtonSelector).addClass("k-state-disabled");
				}
			}
		});

	},
		
	SettingsList: {
		/**
		 *@param {Object} registerViewSettings Настройки списков
		 */
		InitSettingsList: function (registerViewSettings) {
			var gridToolbarSelector = '#GridToolBar-' + registerViewSettings.CurrentRegisterId;
				menuListsGridSelector = "#menu-lists_grid-" + registerViewSettings.CurrentRegisterId,
				settingListBtnSelector = '#ShowSettingListWindow-' + registerViewSettings.CurrentRegisterId,
				settingLayoutBtnSelector = '#ShowSettingLayoutWindow-' + registerViewSettings.CurrentRegisterId;
				settingListWndowSelector = '#settingListWndow-' + registerViewSettings.CurrentRegisterId;

			$('body').append('<div id="settingListWndow-' + registerViewSettings.CurrentRegisterId + '" style="overflow-x: hidden;display:none"><div id="menu-lists_grid-' + registerViewSettings.CurrentRegisterId + '" class="menu-lists-grid no-scrollbar"></div></div>')
			$('body').append('<script id="templateOperationList-' + registerViewSettings.CurrentRegisterId + '" type="text/x-kendo-template"><div id="menu-lists_grid-toolbar-' + registerViewSettings.CurrentRegisterId + '"></div></script>');
			RegisterView.SearchPanel.initLists(registerViewSettings);
			$(menuListsGridSelector).children(".k-grid-content").height(390);
			$(menuListsGridSelector).css('overflow-x', 'hidden');

			$(settingListWndowSelector).kendoWindow({
				title: "Настройка списков",
				visible: false,
				draggable: false,
				resizable: false,
				modal: true,
				iframe: false,
				appendTo: $('body'),
				width: '405px',
				height: '500px'
			}).data('kendoWindow');

			var $settingListBtn = $(settingListBtnSelector);
			if (!$settingListBtn.length) {
				$(gridToolbarSelector).find(settingLayoutBtnSelector)
					.after('<a href="" id="ShowSettingListWindow-' + registerViewSettings.CurrentRegisterId + '" ' + (registerViewSettings.ShowListsMenu ? '' : 'style="display:none" ') +
						'title="Настройка списков" class="k-button k-button-icon"><span class="fas fa-list-ol"></span></a>');
				RegisterView.InitSettingsLayout(registerViewSettings);

				$(settingListBtnSelector).on('click', function () {
					var listWnd = $(settingListWndowSelector).data('kendoWindow');
					if (listWnd)
						listWnd.center().open();
				});
			}
		}
	},

	/**
	 *@param {Object} registerViewSettings Настройки реестра, заполняются в IndexNewDisign.cshtml
	 */
	InitSettingsLayout: function(registerViewSettings) {
		var $settingButton = $('#ShowSettingLayoutWindow-' + registerViewSettings.CurrentRegisterId);

		if ($settingButton) {

			var oldWindow = $('#SettingLayoutWindowContainer').data('kendoWindow');
			if (oldWindow) {
				oldWindow.destroy();
			}


			var $windowContainer = $('<div id="SettingLayoutWindowContainer"></div>');
			$windowContainer.empty().append($('#SettingsLayoutTemplate').html());
			var $window = $windowContainer.kendoWindow({
				title: "Настройки раскладки",
				visible: false,
				draggable: false,
				resizable: false,
				modal: true,
				iframe: false,
				appendTo: $('body'),
				width: '900px',
				height: '600px'
			}).data('kendoWindow');

			initComponents($windowContainer);
			$settingButton.off('click');
			$settingButton.on('click',
				function() {
					$window.center().open();
					console.log('test');
				});
		}
		/**
		 * Инициализация компонентов, трей листа и лист бокса
		 * @param {Object} $windowContainer html элемент модального окна
		 */
		function initComponents($windowContainer) {

			var loadTree;
			var loadBoxList;
			var treeView = $windowContainer.find('#treeListBox').kendoTreeView({
				dataSource: new kendo.data.HierarchicalDataSource({
					transport: {
						read: {
							url: registerViewSettings.SettingsLayoutTreeViewUrl,
							contentType: 'application/json; charset=utf-8',
							dataType: 'json'
						}
					},
					schema: {
						model: {
							id: 'id',
							hasChildren: 'hasChildren',
							children: 'items'
						}
					}
				}),
				loadOnDemand: false,
				checkboxes: {
					checkChildren: true
				},
				dataTextField: 'name',
				select: function(e) {
					if (e.node === undefined) return;

					var item = treeView.dataItem(e.node);

					if (item.hasChildren === true) {
						treeView.collapse('.k-item[aria-expanded="true"]');
						treeView.expand(e.node);
					}
				},
				dataBound: function () {
					loadTree = true;
					checkDubleData();
					treeView.expand(".k-item");
				}
			}).data('kendoTreeView');

			var listBox = $windowContainer.find('#listBox').kendoListBox({
				width: '100%',
				height: '320px',
				dataTextField: "name",
				dataValueField: "id",
				draggable: true,
				dropSources: ["treeListBox"],
				dataSource: {
						type: "json",
						transport: {
							read: {
								url: registerViewSettings.SettingsLayoutListBox
							}
						}
				},
				dataBound: function (e) {
					loadBoxList = true;
					checkDubleData();
				}
			}).data("kendoListBox");

			/**
			 * Удаление данных из трей листа, если в лист боксе есть какие то данные после загрузки
			 */
			function checkDubleData() {
				if (loadTree && loadBoxList) {
					listBox.dataSource.data().forEach(function(item) {
						removeItemAll(treeView.dataSource.data(), item.AttributeName);
					});
				}
			}


			$windowContainer.find('#search').off('input');
			$windowContainer.find('#search').on('input', function (e) {
				var value = $(this)[0].value;

				treeView.dataSource.filter({
					field: 'name',
					operator: 'contains',
					value: value
				});

				if (value.length > 0) {
					treeView.select(null);
					treeView.expand('.k-item');
				} else {
					treeView.select(null);
					treeView.collapse('.k-item');
				}
			});

			$windowContainer.find("#buttons").kendoButtonGroup();


			$windowContainer.find('#editName').off('click');
			$windowContainer.find('#editName').on('click', function (e) {
				e.preventDefault();
				if (listBox.select().length > 0) {
					listBox.select().addClass('changed');
					listBox.select().hide();
					listBox.select().after($('#editorNameTemplate').html());
					listBox.wrapper.find('li.changeName input').val(listBox.select().text());
					subscribeToRenameButtons();
					listBox.select(null);
				}
			});

			function subscribeToRenameButtons() {

				listBox.wrapper.find('#renameAttribute').on('click',
					function (e) {
						e.preventDefault();
						var el = listBox.wrapper.find('.changed');
						if (el) {
							var dataItem = listBox.dataSource.getByUid(el.data('uid'));
							dataItem.name = listBox.wrapper.find('li.changeName input').val();
							el.text(dataItem.name);
							listBox.wrapper.find('li.changeName').remove();
							el.removeClass('changed');
							el.show();
						}
					});
				listBox.wrapper.find('#cancelRename').on('click',
					function (e) {
						e.preventDefault();
						var el = listBox.wrapper.find('.changed');
						if (el) {
							listBox.wrapper.find('li.changeName').remove();
							el.removeClass('changed');
							el.show();
						}
					});

				listBox.wrapper.find('li.changeName input').on('keydown', function (e) {
					e.stopPropagation();
				});
				
			}

			/**
			 * обработка переноса из трей листа
			 */
			$windowContainer.find('#toSelect').off('click');
			$windowContainer.find('#toSelect').on('click', function (e) {
				e.preventDefault();
				var attributes = [];
				checkedNodeIds(treeView.dataSource.view(), attributes);
				var data = treeView.dataSource.data();

				$windowContainer.find('#treeListBox .k-checkbox').prop('indeterminate', false);
				$windowContainer.find('#treeListBox li.k-item').prop('aria-checked', false);

				attributes.forEach(function (el) {
					var rec = Object.assign({}, el);

					delete rec['uid'];
					if (!rec.hasChildren) {
						listBox.dataSource.add(rec);
					}
					removeItemAll(data, el.name);
				});
			});

			/**
			* обработка переноса из лист бокса
			*/
			$windowContainer.find('#toUnSelect').off('click');
			$windowContainer.find('#toUnSelect').on('click', function (e) {
				e.preventDefault();
				var el = listBox.select();
				var item = listBox.dataItem(el);
				if (item) {
					var rec = Object.assign({}, item);
					listBox.dataSource.remove(item);

					rec['checked'] = false;
					rec['hasChildren'] = false;
					rec['name'] = rec['AttributeName'] || rec['name'];
		
					var parentNode = findOrCreateParentNode(rec.parentElement);
					treeView.append(rec, parentNode);
				}
			}); 

			/**
			 * обработка переноса из трей листа всех элементов
			 */
			$windowContainer.find('#selectAll').off('click');
			$windowContainer.find('#selectAll').on('click', function (e) {
				e.preventDefault();
				kendo.ui.progress($windowContainer, true);
				setTimeout(() => {
					$.each($windowContainer.find('#treeListBox .k-item'),
						function (i, el) {
							var uid = $(el).data('uid');
							var element = treeView.findByUid(uid);
							treeView.dataItem(element).set("checked", true);
						});

					var attributes = [];
					checkedNodeIds(treeView.dataSource.view(), attributes);

					attributes.forEach(function (el) {
						var rec = Object.assign({}, el);
						delete rec['uid'];
						if (!rec.hasChildren) {
							listBox.dataSource.add(rec);
						}
					});
					var dSource = new kendo.data.HierarchicalDataSource({
						data: []
					});
					treeView.setDataSource(dSource);
					kendo.ui.progress($windowContainer, false);
				}, 0);
			
			});

			/**
		 * обработка переноса в трей лист всех элементов
		 */
			$windowContainer.find('#unSelectAll').off('click');
			$windowContainer.find('#unSelectAll').on('click', function (e) {
				e.preventDefault();
				kendo.ui.progress($windowContainer, true);
				setTimeout(() => {
					var dataItems = listBox.dataItems() || [];

					dataItems.forEach(function (el) {
						var rec = Object.assign({}, el);
						listBox.dataSource.remove(el);

						rec['checked'] = false;
						rec['hasChildren'] = false;
						rec['name'] = rec['AttributeName'] || rec['name'];
						delete rec['uid'];

						var parentNode = findOrCreateParentNode(rec.parentElement);
						treeView.append(rec, parentNode);
					});

					kendo.ui.progress($windowContainer, false);
				}, 0);
			});

			$windowContainer.find('#toUp').off('click');
			$windowContainer.find('#toUp').on('click', function (e) {
				e.preventDefault();
				if (listBox.select().length > 0) {
					if (listBox.select().first().index() > 0) {
						var item = listBox.select().first();
						listBox.reorder(item, item.index() - 1);
					} else {
						listBox.select(null);
					}
				}
			});

			$windowContainer.find('#toDown').off('click');
			$windowContainer.find('#toDown').on('click', function (e) {
				e.preventDefault();
				if (listBox.select().length > 0) {
					if (listBox.select().first().index() < listBox.items().length - 1) {
						var item = listBox.select().first();
						listBox.reorder(item, item.index() + 1);
					} else {
						listBox.select(null);
					}
				}
			});

			/**
			 * Удаление элементов из треф листа
			 * @param {Array<any>} arr массив всех элементов
			 * @param {string} value признак покоторому удаляем
			 * @returns {Array<any>} возвращаем новый массив
			 */
			function removeItemAll(arr, value) {
				var i = 0;
				while (i < arr.length) {
					if (arr[i].items && arr[i].items.length > 0) {
						arr[i].items = removeItemAll(arr[i].items, value);
					}
					if (arr[i].name === value) {
						arr.splice(i, 1);
					} else {
						++i;
					}
				}
				return arr;
			}

			/**
			 * @typedef ParentNode родительский элемент
			 * @property {number} id ид элемента
			 * @property {string} name имя элемента
			 * @property {boolean} hasChildren признак что есть дети
			 */

			/**
			 * *
			 * @param {ParentNode} parentNode родительский элемент
			 * @returns {any} родительский элемент
			 */
			function findOrCreateParentNode(parentNode) {
				var node,
					nodeObj = treeView.dataItems().find(item => item.hasChildren === true && item.id.toString() === parentNode.id.toString());

				if (nodeObj) {
					node = treeView.findByUid(nodeObj.uid);
				}
				if (!node) {
					node = treeView.append(parentNode);
				}
				return node;
			}

			/**
			 * 
			 * @param {any} nodes элемнеты трей листа
			 * @param {Array<string>} checkedNodes пустой массив на вход для заполнения
			 */
			function checkedNodeIds(nodes, checkedNodes) {
				for (var i = 0; i < nodes.length; i++) {
					if (nodes[i].checked) {
						checkedNodes.push(nodes[i]);
					}

					if (nodes[i].hasChildren) {
						checkedNodeIds(nodes[i].children.view(), checkedNodes);
					}
				}
			}

			/**
			 * Устанавливаем порядок колонок
			 */
			function setOrdinalItems() {
				var liList = $windowContainer.find('#listBox').parents('.k-listbox').find('li') || [];
				
				$.each(liList, function (i, el) {
					var uid = $(el).data('uid');
					var item = listBox.dataSource.getByUid(uid);
					item['Ordinal'] = i;
				});
			}

			$windowContainer.find('#saveSettingLayout').on('click',
				function(e) {
					e.preventDefault();

					setOrdinalItems();
					var columnSettings = [];
					listBox.dataSource.data().forEach(function(el) {
						columnSettings.push({ DetailId: el["detailId"], AttributeId: el.id, Ordinal: el.Ordinal, HeaderName: el.name });
					});
					var data = {
						registerViewId: registerViewSettings.CurrentRegisterViewId,
						registerId: registerViewSettings.CurrentRegisterId,
						columnSettings
					};
					kendo.ui.progress($windowContainer, true);
					$.post(registerViewSettings.SettingsLayoutSave, data)
						.done(function(response) {
							document.location.reload(true);
						}).fail(function (data) {
							Common.ShowError(data.responseText);
						}).always(function() {
							kendo.ui.progress($windowContainer, false);
						});
				});
		}
	}
};

/**
 *
 * @typedef SectionType
 * @property {number} TwoSection
 * @property {number} OneSection
 */
var SectionType = {
	TwoSection: 2,
	OneSection: 1
};


/**
 * Скрывает все компоненты кроме выбора типа (год, день, месяц)
 * @param {string} id ид компонента без префиксов
 * @param {Object} $container контейнер в котором все элемнеты
 */
function hideAllComponents(id, $container) {
	var sectionOne = "#" + id + "_section_one",
		sectionTwo = "#" + id + "_section_two",
		sectionThree = "#" + id + "_section_three";
	
	$container.find(sectionOne) && $container.find(sectionOne).hide();
	$container.find(sectionTwo) && $container.find(sectionTwo).hide();
	$container.find(sectionThree) && $container.find(sectionThree).hide();

	var typeDynamicDate = ["year", "month", "day", "interval", "quarter"];
	typeDynamicDate.forEach(function (el) {
		$container.find(sectionOne + "_" + el) && $container.find(sectionOne + "_" + el).hide();
		$container.find(sectionTwo + "_" + el) && $container.find(sectionTwo + "_" + el).hide();
		$container.find(sectionThree + "_" + el) && $container.find(sectionThree + "_" + el).hide();
	});
}

/**
 *  Скрывает все ненужные компоненты кроме выбора типа (год, день, месяц)
 * @param {string} id ид компонента без префиксов
 * @param {string} value тип набора компонентов (дата, год, интервал...)
 * @param {SectionType} sectionType в какой секции наши компонеты
 * @param {Object} $container контейнер в котором все элемнеты
 */
function hideComponentsExceptVisible(id, value, sectionType, $container) {
	hideAllComponents(id, $container);
	var sectionOne = "#" + id + "_section_one",
		sectionTwo = "#" + id + "_section_two",
		sectionThree = "#" + id + "_section_three";

	if (sectionType === SectionType.TwoSection) {
		$container.find(sectionOne) && $container.find(sectionOne).show();
		$container.find(sectionTwo) && $container.find(sectionTwo).show();
		$container.find(sectionOne + "_" + value) && $container.find(sectionOne + "_" + value).show();
		$container.find(sectionTwo + "_" + value) && $container.find(sectionTwo + "_" + value).show();
		return;
	}

	$container.find(sectionThree) && $container.find(sectionThree).show();
	$container.find(sectionThree + "_" + value) && $container.find(sectionThree + "_" + value).show();
}

/**
 * Вызывается из Html хелпера на событие onCascade для контрола DynamicDate
 * @param {any} e Данные от контрола
 */
function changeDynamicDateType(e) {
	var $container = $(e.sender.element.parents('.form-group')[0]);
	var value = this.value();
	switch (value) {
	case "year":
		case "day":
		case "month":
			hideComponentsExceptVisible(this.element.attr("id"), value, SectionType.OneSection, $container); break;
		default:
			hideComponentsExceptVisible(this.element.attr("id"), value, SectionType.TwoSection, $container); break;
	}

}



/*Event a cascade handling for DynamicNumber and DynamicText controls */
//TODO возможно надо вынести в отдельный файл при рефакторинге RegisterView.js

/**
 *Вызывается из Html хелпера на событие onCascade для контрола DynamicNumber и DynamicText
 * @param {any} e Данные от контрола
 */
function changeDynamicType(e) {
	var $container = $(e.sender.element.parents('.form-group')[0]),
		value = this.value(),
		controlId = $container && $container.find('[DynamicControlType]') && $container.find('[DynamicControlType]').attr("id"),
		dynamicType = controlId && $container.find('[DynamicControlType]').attr("DynamicControlType"),
		controlEl = dynamicType === controlType.DynamicNumber ? $container.find("#" + controlId).parents('span')[1] : $container.find("#" + controlId),
		showTextAreaButton = $container.find("#" + controlId + "_buttonList"),
		dataInputTextArea = $container.find("#" + controlId + "_inputList");

	$(dataInputTextArea).attr('readonly', "");

	if (!$container || !controlId) {
		return;
	}
	if (value === "list") {
		$(controlEl).hide();
		showTextAreaButton.show();
		dataInputTextArea.show();
	}

	if (e.sender["_old"] === "list") {
		dataInputTextArea.hide();
		$(controlEl).show();
		showTextAreaButton.hide();
	}
};

/**
 * @typedef {Object} controlType
 * @property {string} DynamicNumber
 */
var controlType = {
	DynamicNumber: "DynamicNumber"
}

/**
 * 
 * @param {any} e Данные от контрола
 */
function openTextArea(e) {
	var selectorSearchWindow = RegisterView.Common.Constant.searchWindowSelector;
	var $textAreaContainer = $("<div id='textAreaContainer'></div>");
	var $parentFormGroup = $(this.element.parents('.form-group')[0]);

	$(selectorSearchWindow).append($textAreaContainer);
	var kendoDialog = $textAreaContainer.kendoDialog({
		width: "450px",
		title: $(this.element).attr("datacaption"),
		closable: false,
		modal: true,
		content: "<textarea  id='textArea' class='k-textbox' style='width: 100%; height: 300px; resize: none;'></textarea>",
		visible: false,
		buttonLayout: "normal",
		actions: [
			{ text: 'Отмена', action: function() {
				$textAreaContainer.remove();
				return true;
			}},
			{
				text: 'Применить', primary: true,
				action: function (e) {
					if (e.sender.element.find('#textArea')) {
						var value = e.sender.element.find('#textArea').val() || "";
						apply(value, $textAreaContainer, $parentFormGroup);
					}
					return true;
				}
			}
		]
	}).data("kendoDialog");

	var listData = $parentFormGroup.find('[DynamicControlType]').data('listData') || {value : []};
	$textAreaContainer.find('#textArea').val(listData.value.join('\n'));

	if ($parentFormGroup.find('[DynamicControlType]').attr('DynamicControlType') === controlType.DynamicNumber) {
		$textAreaContainer.find('#textArea').keypress(function (e) {
			var a = [];
			var k = e.which;
			a.push(13); //Enter
			a.push(46); //.
			a.push(44); //.

			for (i = 48; i < 58; i++)
				a.push(i);

			if (!(a.indexOf(k) >= 0))
				e.preventDefault();
		});
	}


	kendoDialog.open();
}

/**
 * 
 * @param {string} value строка значений из textArea
 * @param {any} $dialogContainer контейнер в котором размещается диалог
 * @param {Object} $parentFormGroup родительская форм группа
 */
function apply(value, $dialogContainer, $parentFormGroup) {
	var $inputControl = $parentFormGroup.find('[DynamicControlType]');
	$inputControl.data('listData', { value: value.split('\n') });
	var controlId = $inputControl.attr("id");


	var inputList = $parentFormGroup.find('#' + controlId + '_inputList');
	var visibleValue = value.replace(/\n/gi, '; ');
	var shortVisibleValue = visibleValue.length > 20 ? visibleValue.substring(0, 20) + '...' : visibleValue;

	if (inputList) {
		inputList.val(shortVisibleValue);
	} else {
		console.error("Не найден инпут для отображения значений из textArea");
	}


	var $toolTip = $parentFormGroup.find('.' + controlId + '_wrapper').data('kendoTooltip');
	if ($toolTip) {
		$toolTip.options.content = visibleValue;
		$toolTip.refresh();
	} else {
		console.error("Tooltip не найден");
	}

	$dialogContainer.remove();
}